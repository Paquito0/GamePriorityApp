Namespace Services

    Public Module AppLogger
        Private ReadOnly LogPath As String = IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "GamePriorityApp",
            "app.log")

        Public Sub Info(message As String)
            Write("INFO", message)
        End Sub

        Public Sub Warn(message As String)
            Write("WARN", message)
        End Sub

        Public Sub LogError(message As String, Optional ex As Exception = Nothing)
            Dim line = If(ex Is Nothing, message, $"{message} | {ex.GetType().Name}: {ex.Message}")
            Write("ERROR", line)
        End Sub

        Private Sub Write(level As String, message As String)
            Try
                Dim dir = IO.Path.GetDirectoryName(LogPath)
                If Not IO.Directory.Exists(dir) Then IO.Directory.CreateDirectory(dir)
                Dim line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}{Environment.NewLine}"
                IO.File.AppendAllText(LogPath, line)
            Catch
            End Try
        End Sub
    End Module

End Namespace
