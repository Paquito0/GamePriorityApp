Imports Microsoft.Win32

Public Class GamePriorityManager
    Private Const DefaultCpuPriority As Integer = 2
    Private Const DefaultIoPriority As Integer = 2
    Private Const DefaultPagePriority As Integer = 5
    Private Const DefaultAuditLevel As Integer = 0

    Public Shared Function LoadGames() As List(Of String)
        Dim gamesList As New List(Of String)()
        Try
            Dim ifeoKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options", False)
            If ifeoKey IsNot Nothing Then
                Dim subKeys As String() = ifeoKey.GetSubKeyNames()
                For Each exeName As String In subKeys
                    Dim pOpt As RegistryKey = ifeoKey.OpenSubKey(exeName & "\PerfOptions", False)
                    If pOpt IsNot Nothing Then
                        Dim pClass = pOpt.GetValue("CpuPriorityClass")
                        If pClass IsNot Nothing Then
                            gamesList.Add(exeName)
                        End If
                        pOpt.Close()
                    End If
                Next
                ifeoKey.Close()
            End If
        Catch ex As Exception
            Throw New InvalidOperationException("Erro ao carregar jogos do Registro: " & ex.Message, ex)
        End Try
        Return gamesList
    End Function

    Public Shared Sub AddGamePriority(exePath As String, cpuPriority As Integer, ioPriority As Integer, pagePriority As Integer, auditLevel As Integer)
        Dim cleanName As String = IO.Path.GetFileName(exePath)
        If String.IsNullOrWhiteSpace(cleanName) Then Throw New ArgumentException("Nome do executável inválido.")
        If cleanName.Contains("..") OrElse cleanName.Contains("/") OrElse cleanName.Contains("\") Then
            Throw New ArgumentException("Caminho inválido: apenas o nome do .exe é permitido.")
        End If
        If Not cleanName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) Then
            Throw New ArgumentException("Apenas arquivos .exe são permitidos.")
        End If

        Try
            Dim baseKey As String = "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\" & cleanName & "\PerfOptions"
            Using pKey As RegistryKey = Registry.LocalMachine.CreateSubKey(baseKey)
                pKey.SetValue("CpuPriorityClass", cpuPriority, RegistryValueKind.DWord)
                pKey.SetValue("IoPriority", ioPriority, RegistryValueKind.DWord)
                pKey.SetValue("PagePriority", pagePriority, RegistryValueKind.DWord)
                pKey.SetValue("AuditLevel", auditLevel, RegistryValueKind.DWord)
            End Using
        Catch ex As Exception
            Throw New Exception("Falha ao adicionar Jogo no Registro: " & ex.Message)
        End Try
    End Sub

    Public Shared Function GetPerfOptions(exeName As String) As Tuple(Of Integer, Integer, Integer, Integer)
        Dim cpuPriority = DefaultCpuPriority
        Dim ioPriority = DefaultIoPriority
        Dim pagePriority = DefaultPagePriority
        Dim auditLevel = DefaultAuditLevel

        Try
            Dim baseKey As String = "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\" & exeName & "\PerfOptions"
            Using pKey As RegistryKey = Registry.LocalMachine.OpenSubKey(baseKey, False)
                If pKey IsNot Nothing Then
                    Dim cpuVal = pKey.GetValue("CpuPriorityClass")
                    If cpuVal IsNot Nothing Then cpuPriority = CInt(cpuVal)

                    Dim ioVal = pKey.GetValue("IoPriority")
                    If ioVal IsNot Nothing Then ioPriority = CInt(ioVal)

                    Dim pageVal = pKey.GetValue("PagePriority")
                    If pageVal IsNot Nothing Then pagePriority = CInt(pageVal)

                    Dim auditVal = pKey.GetValue("AuditLevel")
                    If auditVal IsNot Nothing Then auditLevel = CInt(auditVal)
                End If
            End Using
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Aviso ao ler PerfOptions: " & ex.Message)
        End Try

        Return Tuple.Create(cpuPriority, ioPriority, pagePriority, auditLevel)
    End Function

    Public Shared Sub UpdateGamePriority(exeName As String, cpuPriority As Integer, ioPriority As Integer, pagePriority As Integer, auditLevel As Integer)
        If String.IsNullOrWhiteSpace(exeName) Then Throw New ArgumentException("Nome do executável inválido.")
        If exeName.Contains("..") OrElse exeName.Contains("/") OrElse exeName.Contains("\") Then
            Throw New ArgumentException("Nome inválido: caracteres não permitidos.")
        End If
        If Not exeName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) Then
            Throw New ArgumentException("Apenas arquivos .exe são permitidos.")
        End If

        Try
            Dim baseKey As String = "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\" & exeName & "\PerfOptions"
            Using pKey As RegistryKey = Registry.LocalMachine.CreateSubKey(baseKey)
                pKey.SetValue("CpuPriorityClass", cpuPriority, RegistryValueKind.DWord)
                pKey.SetValue("IoPriority", ioPriority, RegistryValueKind.DWord)
                pKey.SetValue("PagePriority", pagePriority, RegistryValueKind.DWord)
                pKey.SetValue("AuditLevel", auditLevel, RegistryValueKind.DWord)
            End Using
        Catch ex As Exception
            Throw New Exception("Falha ao atualizar configurações: " & ex.Message)
        End Try
    End Sub

    Public Shared Sub RemoveGamePriority(exePath As String)
        Dim cleanName As String = IO.Path.GetFileName(exePath)
        If String.IsNullOrWhiteSpace(cleanName) Then Return

        Try
            Dim baseKey As String = "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\" & cleanName
            Registry.LocalMachine.DeleteSubKeyTree(baseKey, False)
        Catch ex As Exception
            Throw New Exception("Falha ao remover Jogo do Registro: " & ex.Message)
        End Try
    End Sub

End Class
