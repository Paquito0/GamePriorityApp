Imports Microsoft.Win32

Public Class GamePriorityManager
    Private Const DefaultCpuPriority As Integer = 2
    Private Const DefaultIoPriority As Integer = 2
    Private Const DefaultPagePriority As Integer = 5
    Private Const DefaultAuditLevel As Integer = 0

    Private Shared ReadOnly PastaJogos As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GamePriorityApp")
    Private Shared ReadOnly ArquivoJogos As String = IO.Path.Combine(PastaJogos, "jogos.txt")

    Private Shared PastaSteam As String = "C:\Program Files (x86)\Steam\steamapps\common"
    Private Shared PastaEpic As String = "C:\Program Files\Epic Games"
    Private Shared PastaGOG As String = "C:\GOG Games"
    Private Shared PastaOrigin As String = "C:\Program Files\Origin Games"

    Private Shared ArquivoConfig As String = IO.Path.Combine(PastaJogos, "config.txt")

    Public Shared Function LerConfiguracaoPastas() As Dictionary(Of String, String)
        Dim config As New Dictionary(Of String, String)()
        If IO.File.Exists(ArquivoConfig) Then
            Dim linhas = IO.File.ReadAllLines(ArquivoConfig)
            For Each linha In linhas
                Dim parts = linha.Split("="c)
                If parts.Length = 2 Then
                    config(parts(0).Trim()) = parts(1).Trim()
                End If
            Next
        End If
        Return config
    End Function

    Public Shared Sub SalvarConfiguracaoPastas(servicos As Dictionary(Of String, String), personalizadas As List(Of String))
        Dim linhas As New List(Of String)()
        For Each kvp In servicos
            Dim valor = kvp.Value
            If String.IsNullOrWhiteSpace(valor) Then
                Select Case kvp.Key
                    Case "Steam" : valor = PastaSteam
                    Case "Epic" : valor = PastaEpic
                    Case "GOG" : valor = PastaGOG
                    Case "Origin" : valor = PastaOrigin
                End Select
            End If
            linhas.Add(kvp.Key & "=" & valor)
        Next
        IO.File.WriteAllLines(ArquivoConfig, linhas.ToArray())

        Dim linhasTxt As New List(Of String)()
        linhasTxt.Add("# Pastas configuradas via app")
        For Each p In personalizadas
            linhasTxt.Add("PASTA:" & p)
        Next
        Dim txtContent = If(IO.File.Exists(ArquivoJogos), IO.File.ReadAllText(ArquivoJogos), "")
        Dim linhasOriginais = txtContent.Split(Environment.NewLine).Where(Function(l) Not l.StartsWith("PASTA:")).ToArray()
        IO.File.WriteAllLines(ArquivoJogos, linhasOriginais.Concat(linhasTxt).ToArray())
    End Sub

    Public Shared Function GetPastasPadrao() As List(Of Tuple(Of String, String))
        Dim pastas As New List(Of Tuple(Of String, String))()
        Dim config = LerConfiguracaoPastas()
        Dim steamPath = If(config.ContainsKey("Steam") AndAlso Not String.IsNullOrWhiteSpace(config("Steam")), config("Steam"), PastaSteam)
        Dim epicPath = If(config.ContainsKey("Epic") AndAlso Not String.IsNullOrWhiteSpace(config("Epic")), config("Epic"), PastaEpic)
        Dim gogPath = If(config.ContainsKey("GOG") AndAlso Not String.IsNullOrWhiteSpace(config("GOG")), config("GOG"), PastaGOG)
        Dim originPath = If(config.ContainsKey("Origin") AndAlso Not String.IsNullOrWhiteSpace(config("Origin")), config("Origin"), PastaOrigin)
        pastas.Add(Tuple.Create("Steam", steamPath))
        pastas.Add(Tuple.Create("Epic", epicPath))
        pastas.Add(Tuple.Create("GOG", gogPath))
        pastas.Add(Tuple.Create("Origin", originPath))
        Return pastas
    End Function

    Public Shared Function LerPastasPersonalizadas() As List(Of String)
        Dim pastas As New List(Of String)()
        IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "ArquivoJogos path: " & ArquivoJogos & Environment.NewLine)
        IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "ArquivoJogos existe: " & IO.File.Exists(ArquivoJogos) & Environment.NewLine)
        If IO.File.Exists(ArquivoJogos) Then
            Dim linhas = IO.File.ReadAllLines(ArquivoJogos)
            IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "=== Lendo jogos.txt, linhas: " & linhas.Length & Environment.NewLine)
            For Each linha In linhas
                Dim trimmed = linha.Trim()
                IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "Linha: '" & trimmed & "'" & Environment.NewLine)
                If trimmed.StartsWith("PASTA:", StringComparison.OrdinalIgnoreCase) OrElse trimmed.StartsWith("FOLDER:", StringComparison.OrdinalIgnoreCase) Then
                    IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "  -> Encontrado prefixo PASTA" & Environment.NewLine)
                    Dim pos As Integer = trimmed.IndexOf(":"c)
                    Dim caminho = trimmed.Substring(pos + 1).Trim()
                    IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "  -> Caminho: '" & caminho & "'" & Environment.NewLine)
                    IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "  -> Existe: " & IO.Directory.Exists(caminho) & Environment.NewLine)
                    If Not String.IsNullOrWhiteSpace(caminho) AndAlso IO.Directory.Exists(caminho) Then
                        pastas.Add(caminho)
                    End If
                End If
            Next
        Else
            IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "jogos.txt não existe!" & Environment.NewLine)
        End If
        Return pastas
    End Function

    Public Shared Function GetJogosTxtPath() As String
        Return ArquivoJogos
    End Function

    Public Shared Sub CriarArquivoJogosSeNaoExistir()
        If Not IO.Directory.Exists(PastaJogos) Then
            IO.Directory.CreateDirectory(PastaJogos)
        End If
        If Not IO.File.Exists(ArquivoJogos) Then
            Dim conteudo = "# =========================================" & Environment.NewLine &
                           "# LISTA DE EXECUTÁVEIS (filtro opcional)" & Environment.NewLine &
                           "# Adicione nomes de executáveis para filtrar" & Environment.NewLine &
                           "# Exemplos:" & Environment.NewLine &
                           "# cyberpunk.exe" & Environment.NewLine &
                           "# GTA5.exe" & Environment.NewLine &
                           "#" & Environment.NewLine &
                           "# =========================================" & Environment.NewLine &
                           "# PASTAS PERSONALIZADAS" & Environment.NewLine &
                           "# Adicione pastas para scanhear (use PASTA: ou FOLDER:)" & Environment.NewLine &
                           "# Exemplos:" & Environment.NewLine &
                           "# PASTA: D:\MeusJogos" & Environment.NewLine &
                           "# PASTA: C:\Jogos" & Environment.NewLine &
                           "#" & Environment.NewLine &
                           "# ========================================="
            IO.File.WriteAllText(ArquivoJogos, conteudo)
        End If
    End Sub

    Public Shared Function LerJogosDoTxt() As List(Of String)
        Dim jogos As New List(Of String)()
        If IO.File.Exists(ArquivoJogos) Then
            Dim linhas = IO.File.ReadAllLines(ArquivoJogos)
            For Each linha In linhas
                Dim trimmed = linha.Trim()
                If Not String.IsNullOrWhiteSpace(trimmed) AndAlso
                   Not trimmed.StartsWith("#") AndAlso
                   Not trimmed.StartsWith("PASTA:", StringComparison.OrdinalIgnoreCase) AndAlso
                   Not trimmed.StartsWith("FOLDER:", StringComparison.OrdinalIgnoreCase) AndAlso
                   Not trimmed.StartsWith("'") Then
                    jogos.Add(trimmed.ToLower())
                End If
            Next
        End If
        Return jogos
    End Function

    Private Shared Function ScannerPasta(pasta As String, jogosPermitidos As List(Of String)) As List(Of Tuple(Of String, String))
        Dim resultados As New List(Of Tuple(Of String, String))()
        If Not IO.Directory.Exists(pasta) Then
            Return resultados
        End If

        IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "Scanner: Tentando escanear " & pasta & Environment.NewLine)
        Try
            Dim todosExes = IO.Directory.GetFiles(pasta, "*.exe", IO.SearchOption.AllDirectories)
            IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "Scanner: Total de .exe encontrados: " & todosExes.Length & Environment.NewLine)
            For Each exe In todosExes
                Dim nomeExe = IO.Path.GetFileName(exe).ToLower()
                Dim caminhoExe = exe.ToLower()
                Dim nomePasta = IO.Path.GetFileName(IO.Path.GetDirectoryName(exe)).ToLower()

                Dim incluir = False
                If jogosPermitidos.Count = 0 Then
                    incluir = True
                Else
                    incluir = jogosPermitidos.Any(Function(j) nomeExe.Contains(j) OrElse caminhoExe.Contains(j) OrElse nomePasta.Contains(j))
                End If

                If incluir Then
                    IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "Scanner: Incluindo: " & nomeExe & " (jogosPermitidos.Count=" & jogosPermitidos.Count & ")" & Environment.NewLine)
                    resultados.Add(Tuple.Create(IO.Path.GetFileName(exe), exe))
                Else
                    IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "Scanner: EXCLUÍDO: " & nomeExe & " (jogosPermitidos.Count=" & jogosPermitidos.Count & ")" & Environment.NewLine)
                End If
            Next
        Catch ex As Exception
            IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "Scanner ERRO: " & ex.Message & Environment.NewLine)
            System.Diagnostics.Debug.WriteLine("Erro ao escanear " & pasta & ": " & ex.Message)
        End Try

        Return resultados
    End Function

    Public Shared Function DetectarJogosInstalados() As List(Of Tuple(Of String, String))
        Dim jogosPermitidos As New List(Of String)()

        If IO.File.Exists(ArquivoJogos) Then
            jogosPermitidos = LerJogosDoTxt()
        End If

        Dim resultados As New List(Of Tuple(Of String, String))()
        Dim seen As New HashSet(Of String)()

        Dim pastas = GetPastasPadrao()

        Dim pastasPersonalizadas = LerPastasPersonalizadas()
        IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "Pastas personalizadas encontradas: " & pastasPersonalizadas.Count & Environment.NewLine)
        IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "Jogos para filtrar (jogosPermitidos): " & jogosPermitidos.Count & " => " & String.Join(", ", jogosPermitidos) & Environment.NewLine)
        For Each pp In pastasPersonalizadas
            IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "  - " & pp & " existe: " & IO.Directory.Exists(pp) & Environment.NewLine)
            pastas.Add(Tuple.Create("Personalizado", pp))
        Next

        System.Diagnostics.Debug.WriteLine("=== DEBUG: Pastas a escanear ===")
        For Each p In pastas
            System.Diagnostics.Debug.WriteLine("Pasta: " & p.Item1 & " = " & p.Item2)
        Next

        For Each pasta In pastas
            System.Diagnostics.Debug.WriteLine("Escaneando: " & pasta.Item2)
            IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "Escaneando: " & pasta.Item2 & " - Existe: " & IO.Directory.Exists(pasta.Item2) & Environment.NewLine)
            Dim encontrados = ScannerPasta(pasta.Item2, jogosPermitidos)
            System.Diagnostics.Debug.WriteLine("Encontrados: " & encontrados.Count)
            IO.File.AppendAllText(IO.Path.Combine(PastaJogos, "debug.txt"), "Encontrados: " & encontrados.Count & Environment.NewLine)
            For Each item In encontrados
                If Not seen.Contains(item.Item1) Then
                    seen.Add(item.Item1)
                    resultados.Add(Tuple.Create(item.Item1 & " [" & pasta.Item1 & "]", item.Item2))
                End If
            Next
        Next

        Return resultados.OrderBy(Function(x) x.Item1).ToList()
    End Function

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
        If cleanName.Contains("[") Then
            cleanName = cleanName.Split("["c)(0).Trim()
        End If
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
