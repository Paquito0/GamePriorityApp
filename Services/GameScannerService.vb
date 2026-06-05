Imports System.IO
Imports GamePriorityApp.Models

Namespace Services

    Public Class GameScannerService
        Public Shared Async Function DetectInstalledGamesAsync() As Task(Of List(Of DetectedGame))
            Return Await Task.Run(Function() DetectInstalledGamesSync())
        End Function

        Public Shared Function DetectInstalledGamesSync() As List(Of DetectedGame)
            Dim results As New List(Of DetectedGame)()
            Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

            Dim folders = BuildSearchFolders()
            For Each folder In folders
                For Each game In ScanFolder(folder.Item2, folder.Item1)
                    If seen.Add(game.FullPath) Then
                        results.Add(game)
                    End If
                Next
            Next

            Return results.OrderBy(Function(g) g.DisplayName).ToList()
        End Function

        Private Shared Function BuildSearchFolders() As List(Of (String, String))
            Dim folders As New List(Of (String, String))()
            For Each svc In AppConfigService.GetServiceFolders()
                folders.Add((svc.Key, svc.Path))
            Next
            For Each custom In AppConfigService.GetCustomFolders()
                folders.Add(("Personalizado", custom))
            Next
            Return folders
        End Function

        Private Shared Function ScanFolder(folderPath As String, source As String) As List(Of DetectedGame)
            Dim results As New List(Of DetectedGame)()
            If String.IsNullOrWhiteSpace(folderPath) Then Return results
            If Not Directory.Exists(folderPath) Then Return results

            Try
                Dim exes = Directory.GetFiles(folderPath, "*.exe", SearchOption.AllDirectories)
                For Each exePath In exes
                    results.Add(New DetectedGame(
                        $"{Path.GetFileName(exePath)} [{source}]",
                        exePath,
                        source))
                Next
            Catch ex As Exception
                AppLogger.Warn($"Erro ao escanear {folderPath}: {ex.Message}")
            End Try

            Return results
        End Function
    End Class

End Namespace
