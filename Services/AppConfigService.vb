Imports System.IO
Imports System.Text.Json

Namespace Services

    Public Class AppConfig
        Public Property SteamPath As String = ""
        Public Property EpicPath As String = ""
        Public Property GogPath As String = ""
        Public Property OriginPath As String = ""
        Public Property CustomFolders As New List(Of String)()
    End Class

    Public Class ServiceFolder
        Public Property Key As String
        Public Property Path As String
        Public Property DefaultPath As String

        Public Sub New(key As String, currentPath As String, defaultPath As String)
            Me.Key = key
            Me.Path = If(String.IsNullOrWhiteSpace(currentPath), defaultPath, currentPath)
            Me.DefaultPath = defaultPath
        End Sub
    End Class

    Public Class AppConfigService
        Public Shared ReadOnly AppFolder As String = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "GamePriorityApp")

        Public Shared ReadOnly ConfigFilePath As String = Path.Combine(AppFolder, "config.json")

        Private Shared ReadOnly DefaultSteam As String = "C:\Program Files (x86)\Steam\steamapps\common"
        Private Shared ReadOnly DefaultEpic As String = "C:\Program Files\Epic Games"
        Private Shared ReadOnly DefaultGOG As String = "C:\GOG Games"
        Private Shared ReadOnly DefaultOrigin As String = "C:\Program Files\Origin Games"

        Public Shared Function Load() As AppConfig
            Try
                If File.Exists(ConfigFilePath) Then
                    Dim json = File.ReadAllText(ConfigFilePath)
                    Dim cfg = JsonSerializer.Deserialize(Of AppConfig)(json)
                    If cfg IsNot Nothing Then Return cfg
                End If
            Catch ex As Exception
                AppLogger.LogError("Falha ao ler config.json", ex)
            End Try
            Return New AppConfig()
        End Function

        Public Shared Sub Save(config As AppConfig)
            Try
                If Not Directory.Exists(AppFolder) Then
                    Directory.CreateDirectory(AppFolder)
                End If
                Dim json = JsonSerializer.Serialize(config,
                    New JsonSerializerOptions With {.WriteIndented = True})
                File.WriteAllText(ConfigFilePath, json)
            Catch ex As Exception
                AppLogger.LogError("Falha ao salvar config.json", ex)
                Throw
            End Try
        End Sub

        Public Shared Sub EnsureExists()
            If Not Directory.Exists(AppFolder) Then
                Directory.CreateDirectory(AppFolder)
            End If
            If Not File.Exists(ConfigFilePath) Then
                Save(New AppConfig())
            End If
        End Sub

        Public Shared Function GetServiceFolders() As List(Of ServiceFolder)
            Dim cfg = Load()
            Return New List(Of ServiceFolder) From {
                New ServiceFolder("Steam", cfg.SteamPath, DefaultSteam),
                New ServiceFolder("Epic", cfg.EpicPath, DefaultEpic),
                New ServiceFolder("GOG", cfg.GogPath, DefaultGOG),
                New ServiceFolder("Origin", cfg.OriginPath, DefaultOrigin)
            }
        End Function

        Public Shared Function GetCustomFolders() As List(Of String)
            Return Load().CustomFolders
        End Function

        Public Shared Sub UpdateFolders(services As List(Of ServiceFolder), customFolders As List(Of String))
            Dim cfg = Load()
            For Each svc In services
                Select Case svc.Key
                    Case "Steam" : cfg.SteamPath = svc.Path
                    Case "Epic" : cfg.EpicPath = svc.Path
                    Case "GOG" : cfg.GogPath = svc.Path
                    Case "Origin" : cfg.OriginPath = svc.Path
                End Select
            Next
            cfg.CustomFolders = customFolders
            Save(cfg)
        End Sub
    End Class

End Namespace
