Imports System.Globalization
Imports System.Resources

Namespace Resources

    Public NotInheritable Class Strings
        Private Shared ReadOnly _manager As New ResourceManager(
            "GamePriorityApp.Resources.Strings", GetType(Strings).Assembly)

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property WindowTitle As String
            Get
                Return _manager.GetString("WindowTitle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property AppTitle As String
            Get
                Return _manager.GetString("AppTitle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property AppSubtitle As String
            Get
                Return _manager.GetString("AppSubtitle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property GamesListLabel As String
            Get
                Return _manager.GetString("GamesListLabel", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property SearchPlaceholder As String
            Get
                Return _manager.GetString("SearchPlaceholder", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property NoGames As String
            Get
                Return _manager.GetString("NoGames", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property NoResults As String
            Get
                Return _manager.GetString("NoResults", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property BtnAdd As String
            Get
                Return _manager.GetString("BtnAdd", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property BtnSave As String
            Get
                Return _manager.GetString("BtnSave", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property BtnRemove As String
            Get
                Return _manager.GetString("BtnRemove", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property BtnRefresh As String
            Get
                Return _manager.GetString("BtnRefresh", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property BtnDetectGames As String
            Get
                Return _manager.GetString("BtnDetectGames", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property BtnConfig As String
            Get
                Return _manager.GetString("BtnConfig", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property ConfigTitle As String
            Get
                Return _manager.GetString("ConfigTitle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property PriorityGroupTitle As String
            Get
                Return _manager.GetString("PriorityGroupTitle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property DetectGroupTitle As String
            Get
                Return _manager.GetString("DetectGroupTitle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property DoubleClickHint As String
            Get
                Return _manager.GetString("DoubleClickHint", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property LabelCpu As String
            Get
                Return _manager.GetString("LabelCpu", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property LabelIo As String
            Get
                Return _manager.GetString("LabelIo", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property LabelPage As String
            Get
                Return _manager.GetString("LabelPage", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property LabelAudit As String
            Get
                Return _manager.GetString("LabelAudit", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property CpuIdle As String
            Get
                Return _manager.GetString("CpuIdle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property CpuNormal As String
            Get
                Return _manager.GetString("CpuNormal", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property CpuHigh As String
            Get
                Return _manager.GetString("CpuHigh", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property CpuBelowNormal As String
            Get
                Return _manager.GetString("CpuBelowNormal", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property CpuAboveNormal As String
            Get
                Return _manager.GetString("CpuAboveNormal", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property IoVeryLow As String
            Get
                Return _manager.GetString("IoVeryLow", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property IoLow As String
            Get
                Return _manager.GetString("IoLow", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property IoNormal As String
            Get
                Return _manager.GetString("IoNormal", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property PageVeryLow As String
            Get
                Return _manager.GetString("PageVeryLow", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property PageLow As String
            Get
                Return _manager.GetString("PageLow", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property PageMedium As String
            Get
                Return _manager.GetString("PageMedium", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property PageBelowNormal As String
            Get
                Return _manager.GetString("PageBelowNormal", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property PageNormal As String
            Get
                Return _manager.GetString("PageNormal", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property AuditDisabled As String
            Get
                Return _manager.GetString("AuditDisabled", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property AuditEnabled As String
            Get
                Return _manager.GetString("AuditEnabled", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property ConfigFoldersLabel As String
            Get
                Return _manager.GetString("ConfigFoldersLabel", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property CustomFoldersLabel As String
            Get
                Return _manager.GetString("CustomFoldersLabel", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property BtnCancel As String
            Get
                Return _manager.GetString("BtnCancel", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property BtnAddFolder As String
            Get
                Return _manager.GetString("BtnAddFolder", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property BtnRemoveFolder As String
            Get
                Return _manager.GetString("BtnRemoveFolder", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property BtnBrowse As String
            Get
                Return _manager.GetString("BtnBrowse", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property StatusOk As String
            Get
                Return _manager.GetString("StatusOk", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property StatusNotFound As String
            Get
                Return _manager.GetString("StatusNotFound", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property BrowseFolderDescription As String
            Get
                Return _manager.GetString("BrowseFolderDescription", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property OpenFileDialogTitle As String
            Get
                Return _manager.GetString("OpenFileDialogTitle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property OpenFileDialogFilter As String
            Get
                Return _manager.GetString("OpenFileDialogFilter", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgGameAdded As String
            Get
                Return _manager.GetString("MsgGameAdded", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgSettingsSaved As String
            Get
                Return _manager.GetString("MsgSettingsSaved", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgSelectGameModify As String
            Get
                Return _manager.GetString("MsgSelectGameModify", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgSelectGameRemove As String
            Get
                Return _manager.GetString("MsgSelectGameRemove", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgConfirmTitle As String
            Get
                Return _manager.GetString("MsgConfirmTitle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgSuccessTitle As String
            Get
                Return _manager.GetString("MsgSuccessTitle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgWarningTitle As String
            Get
                Return _manager.GetString("MsgWarningTitle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgErrorTitle As String
            Get
                Return _manager.GetString("MsgErrorTitle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgSearchingGames As String
            Get
                Return _manager.GetString("MsgSearchingGames", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgPleaseWait As String
            Get
                Return _manager.GetString("MsgPleaseWait", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgNoGamesFound As String
            Get
                Return _manager.GetString("MsgNoGamesFound", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgCheckConfig As String
            Get
                Return _manager.GetString("MsgCheckConfig", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgCheckFolders As String
            Get
                Return _manager.GetString("MsgCheckFolders", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgCheckFoldersExist As String
            Get
                Return _manager.GetString("MsgCheckFoldersExist", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgCheckConfigFile As String
            Get
                Return _manager.GetString("MsgCheckConfigFile", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgFoldersNotFound As String
            Get
                Return _manager.GetString("MsgFoldersNotFound", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgSaveAnyway As String
            Get
                Return _manager.GetString("MsgSaveAnyway", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared ReadOnly Property MsgFoldersNotFoundTitle As String
            Get
                Return _manager.GetString("MsgFoldersNotFoundTitle", CultureInfo.CurrentUICulture)
            End Get
        End Property

        Public Shared Function MsgConfirmRemove(gameName As String) As String
            Return String.Format(_manager.GetString("MsgConfirmRemove", CultureInfo.CurrentUICulture), gameName)
        End Function

        Public Shared Function MsgGameAddedWithName(name As String) As String
            Return String.Format(_manager.GetString("MsgGameAddedWithName", CultureInfo.CurrentUICulture), name)
        End Function

        Public Shared Function MsgGamesFound(count As Integer) As String
            Return String.Format(_manager.GetString("MsgGamesFound", CultureInfo.CurrentUICulture), count)
        End Function

        Public Shared Function SelectFolderFor(serviceName As String) As String
            Return String.Format(_manager.GetString("SelectFolderFor", CultureInfo.CurrentUICulture), serviceName)
        End Function
    End Class

End Namespace
