Imports System.IO
Imports Microsoft.Win32
Imports GamePriorityApp.Models

Namespace Services

    Public Class GameRegistryService
        Private Const IfeoRoot As String = "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options"

        Public Shared Function LoadGames() As List(Of String)
            Dim games As New List(Of String)()
            Try
                Using ifeoKey = Registry.LocalMachine.OpenSubKey(IfeoRoot, False)
                    If ifeoKey Is Nothing Then Return games
                    For Each exeName In ifeoKey.GetSubKeyNames()
                        Using pOpt = ifeoKey.OpenSubKey($"{exeName}\PerfOptions", False)
                            If pOpt IsNot Nothing AndAlso pOpt.GetValue("CpuPriorityClass") IsNot Nothing Then
                                games.Add(exeName)
                            End If
                        End Using
                    Next
                End Using
            Catch ex As Exception
                AppLogger.LogError("Falha ao listar jogos do Registro", ex)
                Throw New InvalidOperationException("Erro ao carregar jogos do Registro: " & ex.Message, ex)
            End Try
            Return games
        End Function

        Public Shared Sub AddGame(exePathOrName As String, settings As PrioritySettings)
            Dim cleanName = SanitizeExecutableName(exePathOrName)
            WritePerfOptions(cleanName, settings)
        End Sub

        Public Shared Sub UpdateGame(exeName As String, settings As PrioritySettings)
            Dim cleanName = Path.GetFileName(exeName)
            WritePerfOptions(cleanName, settings)
        End Sub

        Public Shared Sub RemoveGame(exeName As String)
            Dim nameWithExt = Path.GetFileName(exeName)
            Dim nameWithoutExt = Path.GetFileNameWithoutExtension(exeName)

            Dim ex = If(DeleteSubKeyTree(nameWithExt), DeleteSubKeyTree(nameWithoutExt))
            If ex IsNot Nothing Then
                Throw New Exception("Falha ao remover Jogo do Registro: " & ex.Message, ex)
            End If
        End Sub

        Public Shared Function GetPerfOptions(exeName As String) As PrioritySettings
            Dim cleanName = Path.GetFileName(exeName)
            Dim settings As New PrioritySettings()

            Try
                Using pKey = Registry.LocalMachine.OpenSubKey($"{IfeoRoot}\{cleanName}\PerfOptions", False)
                    If pKey Is Nothing Then Return settings

                    Dim cpuVal = pKey.GetValue("CpuPriorityClass")
                    If cpuVal IsNot Nothing Then settings.Cpu = SafeToEnum(Of CpuPriority)(cpuVal, settings.Cpu)

                    Dim ioVal = pKey.GetValue("IoPriority")
                    If ioVal IsNot Nothing Then settings.Io = SafeToEnum(Of IoPriority)(ioVal, settings.Io)

                    Dim pageVal = pKey.GetValue("PagePriority")
                    If pageVal IsNot Nothing Then settings.Page = SafeToEnum(Of PagePriority)(pageVal, settings.Page)

                    Dim auditVal = pKey.GetValue("AuditLevel")
                    If auditVal IsNot Nothing Then settings.Audit = SafeToEnum(Of AuditLevel)(auditVal, settings.Audit)
                End Using
            Catch ex As Exception
                AppLogger.Warn($"Aviso ao ler PerfOptions de {cleanName}: {ex.Message}")
            End Try

            Return settings
        End Function

        Private Shared Function SafeToEnum(Of TEnum As Structure)(value As Object, fallback As TEnum) As TEnum
            Try
                Dim intVal As Integer
                If Integer.TryParse(value.ToString(), intVal) Then
                    Return CType([Enum].ToObject(GetType(TEnum), intVal), TEnum)
                End If
            Catch
            End Try
            Return fallback
        End Function

        Private Shared Sub WritePerfOptions(cleanName As String, settings As PrioritySettings)
            Try
                Using pKey = Registry.LocalMachine.CreateSubKey($"{IfeoRoot}\{cleanName}\PerfOptions")
                    pKey.SetValue("CpuPriorityClass", CInt(settings.Cpu), RegistryValueKind.DWord)
                    pKey.SetValue("IoPriority", CInt(settings.Io), RegistryValueKind.DWord)
                    pKey.SetValue("PagePriority", CInt(settings.Page), RegistryValueKind.DWord)
                    pKey.SetValue("AuditLevel", CInt(settings.Audit), RegistryValueKind.DWord)
                End Using
            Catch ex As Exception
                AppLogger.LogError($"Falha ao gravar PerfOptions para {cleanName}", ex)
                Throw New Exception("Falha ao gravar no Registro: " & ex.Message, ex)
            End Try
        End Sub

        Private Shared Function DeleteSubKeyTree(name As String) As Exception
            Try
                Registry.LocalMachine.DeleteSubKeyTree($"{IfeoRoot}\{name}", False)
                Return Nothing
            Catch ex As Exception
                Return ex
            End Try
        End Function

        Private Shared Function SanitizeExecutableName(exePathOrName As String) As String
            Dim cleanName = Path.GetFileName(exePathOrName)
            If cleanName.Contains("[") Then
                cleanName = cleanName.Split("["c)(0).Trim()
            End If
            If String.IsNullOrWhiteSpace(cleanName) Then
                Throw New ArgumentException("Nome do executável inválido.")
            End If
            If cleanName.Contains("..") OrElse cleanName.Contains("/") OrElse cleanName.Contains("\") Then
                Throw New ArgumentException("Caminho inválido: apenas o nome do .exe é permitido.")
            End If
            If Not cleanName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) Then
                Throw New ArgumentException("Apenas arquivos .exe são permitidos.")
            End If
            Return cleanName
        End Function
    End Class

End Namespace
