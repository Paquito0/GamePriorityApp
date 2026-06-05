Namespace Models

    Public Class DetectedGame
        Public Property DisplayName As String
        Public Property FullPath As String
        Public Property Source As String

        Public Sub New()
        End Sub

        Public Sub New(displayName As String, fullPath As String, source As String)
            Me.DisplayName = displayName
            Me.FullPath = fullPath
            Me.Source = source
        End Sub

        Public Function ExecutableName() As String
            Return IO.Path.GetFileName(FullPath)
        End Function
    End Class

End Namespace
