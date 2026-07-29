Imports System.Windows.Forms
Imports GamePriorityApp.Forms
Imports GamePriorityApp.Models
Imports GamePriorityApp.Resources
Imports GamePriorityApp.Services

Public Class Form1
    Inherits Form

    Private lstJogos As ListBox
    Private txtSearch As TextBox
    Private btnAdicionar As Button
    Private btnRemover As Button
    Private btnRefresh As Button
    Private btnSalvar As Button
    Private lblTitulo As Label
    Private lblInfo As Label
    Private dlgAbrir As OpenFileDialog
    Private grpExtras As GroupBox
    Private lblCpu As Label
    Private lblIo As Label
    Private lblPage As Label
    Private lblAudit As Label
    Private cboCpuPriority As ComboBox
    Private cboIoPriority As ComboBox
    Private cboPagePriority As ComboBox
    Private cboAuditLevel As ComboBox
    Private tipCpu As ToolTip
    Private tipIo As ToolTip
    Private tipPage As ToolTip
    Private tipAudit As ToolTip
    Private btnDetectarInstalados As Button
    Private btnConfig As Button
    Private lstProcessos As ListBox
    Private grpProcessos As GroupBox

    Private _allGames As New List(Of String)()

    Public Sub New()
        AppConfigService.EnsureExists()
        InitializeComponent()
        LoadGames()
    End Sub

    Private Sub InitializeComponent()
        ApplyFormSettings()
        CreateHeader()
        CreateGamesList()
        CreateActionsPanel()
        CreatePriorityGroup()
        CreateDetectionGroup()
        CreateTooltips()
        CreateDialogs()
        WireEvents()
    End Sub

    Private Sub ApplyFormSettings()
        Me.Text = Strings.WindowTitle
        Me.Size = New Drawing.Size(520, 770)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = AppTheme.BackColor
    End Sub

    Private Sub CreateHeader()
        lblTitulo = New Label()
        lblTitulo.Text = Strings.AppTitle
        lblTitulo.Font = AppTheme.TitleFont
        lblTitulo.ForeColor = AppTheme.AccentColor
        lblTitulo.Location = New Drawing.Point(20, 15)
        lblTitulo.AutoSize = True

        lblInfo = New Label()
        lblInfo.Text = Strings.AppSubtitle
        lblInfo.Font = AppTheme.SubtitleFont
        lblInfo.ForeColor = AppTheme.MutedColor
        lblInfo.Location = New Drawing.Point(20, 42)
        lblInfo.AutoSize = True

        Dim lblJogos As New Label()
        lblJogos.Text = Strings.GamesListLabel
        lblJogos.Font = AppTheme.GroupFont
        lblJogos.ForeColor = AppTheme.ForeColor
        lblJogos.Location = New Drawing.Point(20, 68)
        lblJogos.AutoSize = True
        Me.Controls.Add(lblJogos)

        txtSearch = New TextBox()
        txtSearch.Location = New Drawing.Point(20, 88)
        txtSearch.Size = New Drawing.Size(470, 22)
        txtSearch.BackColor = AppTheme.TextBackColor
        txtSearch.ForeColor = AppTheme.ForeColor
        txtSearch.BorderStyle = BorderStyle.FixedSingle
        txtSearch.Font = AppTheme.LabelFont
        txtSearch.PlaceholderText = Strings.SearchPlaceholder
        txtSearch.Tag = False
        AddHandler txtSearch.TextChanged, AddressOf TxtSearch_TextChanged
        Me.Controls.Add(txtSearch)
    End Sub

    Private Sub CreateGamesList()
        lstJogos = New ListBox()
        lstJogos.Location = New Drawing.Point(20, 112)
        lstJogos.Size = New Drawing.Size(470, 160)
        lstJogos.BackColor = AppTheme.ControlBackColor
        lstJogos.ForeColor = AppTheme.ForeColor
        lstJogos.BorderStyle = BorderStyle.FixedSingle
        lstJogos.Font = AppTheme.ListFont
    End Sub

    Private Sub CreateActionsPanel()
        Dim panel As New FlowLayoutPanel()
        panel.Location = New Drawing.Point(20, 278)
        panel.Size = New Drawing.Size(470, 40)
        panel.FlowDirection = FlowDirection.LeftToRight
        panel.AutoSize = True

        btnAdicionar = CreateActionButton(Strings.BtnAdd, AppTheme.PrimaryButton, 0)
        btnSalvar = CreateActionButton(Strings.BtnSave, AppTheme.SuccessButton, 0)
        btnRemover = CreateActionButton(Strings.BtnRemove, AppTheme.DangerButton, 0)
        btnRefresh = CreateActionButton(Strings.BtnRefresh, AppTheme.NeutralButton, 0)

        panel.Controls.Add(btnAdicionar)
        panel.Controls.Add(btnSalvar)
        panel.Controls.Add(btnRemover)
        panel.Controls.Add(btnRefresh)
        Me.Controls.Add(panel)
    End Sub

    Private Function CreateActionButton(text As String, backColor As Drawing.Color, padding As Integer) As Button
        Dim btn As New Button()
        btn.Text = text
        btn.Size = New Drawing.Size(110, 32)
        btn.BackColor = backColor
        btn.ForeColor = AppTheme.ForeColor
        btn.FlatStyle = FlatStyle.Flat
        btn.Font = AppTheme.ButtonFont
        btn.Margin = New Padding(padding, 0, 4, 0)
        Return btn
    End Function

    Private Sub CreatePriorityGroup()
        grpExtras = New GroupBox()
        grpExtras.Text = Strings.PriorityGroupTitle
        grpExtras.ForeColor = AppTheme.WarnColor
        grpExtras.Font = AppTheme.GroupFont
        grpExtras.Location = New Drawing.Point(20, 330)
        grpExtras.Size = New Drawing.Size(470, 140)
        grpExtras.BackColor = AppTheme.PanelBackColor

        lblCpu = CreateFieldLabel(Strings.LabelCpu, New Drawing.Point(15, 25))
        cboCpuPriority = CreatePriorityComboBox(New Drawing.Point(65, 23))
        PopulateCpuOptions()
        cboCpuPriority.SelectedIndex = 2

        lblIo = CreateFieldLabel(Strings.LabelIo, New Drawing.Point(15, 55))
        cboIoPriority = CreatePriorityComboBox(New Drawing.Point(65, 53))
        PopulateIoOptions()
        cboIoPriority.SelectedIndex = 2

        lblPage = CreateFieldLabel(Strings.LabelPage, New Drawing.Point(15, 85))
        cboPagePriority = CreatePriorityComboBox(New Drawing.Point(65, 83))
        PopulatePageOptions()
        cboPagePriority.SelectedIndex = 4

        lblAudit = CreateFieldLabel(Strings.LabelAudit, New Drawing.Point(15, 115))
        cboAuditLevel = CreatePriorityComboBox(New Drawing.Point(65, 113))
        PopulateAuditOptions()
        cboAuditLevel.SelectedIndex = 0

        grpExtras.Controls.AddRange(New Control() {
            lblCpu, cboCpuPriority,
            lblIo, cboIoPriority,
            lblPage, cboPagePriority,
            lblAudit, cboAuditLevel
        })
    End Sub

    Private Function CreateFieldLabel(text As String, location As Drawing.Point) As Label
        Dim lbl As New Label()
        lbl.Text = text
        lbl.ForeColor = AppTheme.ForeColor
        lbl.Font = AppTheme.LabelFont
        lbl.Location = location
        lbl.Size = New Drawing.Size(45, 18)
        Return lbl
    End Function

    Private Function CreatePriorityComboBox(location As Drawing.Point) As ComboBox
        Dim cbo As New ComboBox()
        cbo.DropDownStyle = ComboBoxStyle.DropDownList
        cbo.BackColor = AppTheme.InputBackColor
        cbo.ForeColor = AppTheme.ForeColor
        cbo.Font = AppTheme.LabelFont
        cbo.Location = location
        cbo.Size = New Drawing.Size(380, 22)
        Return cbo
    End Function

    Private Sub PopulateCpuOptions()
        cboCpuPriority.Items.AddRange(New Object() {
            Strings.CpuIdle,
            Strings.CpuNormal,
            Strings.CpuHigh,
            Strings.CpuBelowNormal,
            Strings.CpuAboveNormal
        })
    End Sub

    Private Sub PopulateIoOptions()
        cboIoPriority.Items.AddRange(New Object() {
            Strings.IoVeryLow,
            Strings.IoLow,
            Strings.IoNormal
        })
    End Sub

    Private Sub PopulatePageOptions()
        cboPagePriority.Items.AddRange(New Object() {
            Strings.PageVeryLow,
            Strings.PageLow,
            Strings.PageMedium,
            Strings.PageBelowNormal,
            Strings.PageNormal
        })
    End Sub

    Private Sub PopulateAuditOptions()
        cboAuditLevel.Items.AddRange(New Object() {
            Strings.AuditDisabled,
            Strings.AuditEnabled
        })
    End Sub

    Private Sub CreateDetectionGroup()
        grpProcessos = New GroupBox()
        grpProcessos.Text = Strings.DetectGroupTitle
        grpProcessos.ForeColor = AppTheme.SuccessColor
        grpProcessos.Font = AppTheme.GroupFont
        grpProcessos.Location = New Drawing.Point(20, 485)
        grpProcessos.Size = New Drawing.Size(470, 130)
        grpProcessos.BackColor = AppTheme.PanelBackColor

        lstProcessos = New ListBox()
        lstProcessos.Location = New Drawing.Point(15, 25)
        lstProcessos.Size = New Drawing.Size(270, 95)
        lstProcessos.BackColor = AppTheme.TextBackColor
        lstProcessos.ForeColor = AppTheme.ForeColor
        lstProcessos.BorderStyle = BorderStyle.FixedSingle
        lstProcessos.Font = AppTheme.SmallListFont

        btnDetectarInstalados = New Button()
        btnDetectarInstalados.Text = Strings.BtnDetectGames
        btnDetectarInstalados.Location = New Drawing.Point(295, 25)
        btnDetectarInstalados.Size = New Drawing.Size(100, 30)
        btnDetectarInstalados.BackColor = AppTheme.DetectButton
        btnDetectarInstalados.ForeColor = AppTheme.ForeColor
        btnDetectarInstalados.FlatStyle = FlatStyle.Flat
        btnDetectarInstalados.Font = AppTheme.ButtonFont

        btnConfig = New Button()
        btnConfig.Text = Strings.BtnConfig
        btnConfig.Location = New Drawing.Point(295, 60)
        btnConfig.Size = New Drawing.Size(100, 30)
        btnConfig.BackColor = AppTheme.ConfigButton
        btnConfig.ForeColor = AppTheme.ForeColor
        btnConfig.FlatStyle = FlatStyle.Flat
        btnConfig.Font = AppTheme.ButtonFont

        Dim lblDica As New Label()
        lblDica.Text = Strings.DoubleClickHint
        lblDica.Font = AppTheme.HintFont
        lblDica.ForeColor = AppTheme.HintColor
        lblDica.Location = New Drawing.Point(295, 95)
        lblDica.AutoSize = True

        grpProcessos.Controls.AddRange(New Control() {
            lstProcessos, btnDetectarInstalados, btnConfig, lblDica
        })
    End Sub

    Private Sub CreateTooltips()
        tipCpu = New ToolTip() With {.IsBalloon = True, .ToolTipTitle = "Info"}
        tipIo = New ToolTip() With {.IsBalloon = True, .ToolTipTitle = "Info"}
        tipPage = New ToolTip() With {.IsBalloon = True, .ToolTipTitle = "Info"}
        tipAudit = New ToolTip() With {.IsBalloon = True, .ToolTipTitle = "Info"}

        AddHandler cboCpuPriority.MouseHover, Sub() ShowTip(tipCpu, cboCpuPriority, PriorityDescriptions.CpuDescription(cboCpuPriority.SelectedIndex), 4000)
        AddHandler cboCpuPriority.SelectedIndexChanged, Sub() ShowTip(tipCpu, cboCpuPriority, PriorityDescriptions.CpuDescription(cboCpuPriority.SelectedIndex), 3000)

        AddHandler cboIoPriority.MouseHover, Sub() ShowTip(tipIo, cboIoPriority, PriorityDescriptions.IoDescription(cboIoPriority.SelectedIndex), 4000)
        AddHandler cboIoPriority.SelectedIndexChanged, Sub() ShowTip(tipIo, cboIoPriority, PriorityDescriptions.IoDescription(cboIoPriority.SelectedIndex), 3000)

        AddHandler cboPagePriority.MouseHover, Sub() ShowTip(tipPage, cboPagePriority, PriorityDescriptions.PageDescription(cboPagePriority.SelectedIndex), 4000)
        AddHandler cboPagePriority.SelectedIndexChanged, Sub() ShowTip(tipPage, cboPagePriority, PriorityDescriptions.PageDescription(cboPagePriority.SelectedIndex), 3000)

        AddHandler cboAuditLevel.MouseHover, Sub() ShowTip(tipAudit, cboAuditLevel, PriorityDescriptions.AuditDescription(cboAuditLevel.SelectedIndex), 4000)
        AddHandler cboAuditLevel.SelectedIndexChanged, Sub() ShowTip(tipAudit, cboAuditLevel, PriorityDescriptions.AuditDescription(cboAuditLevel.SelectedIndex), 3000)
    End Sub

    Private Shared Sub ShowTip(tip As ToolTip, ctrl As Control, text As String, durationMs As Integer)
        tip.Show(text, ctrl, ctrl.Width, 0, durationMs)
    End Sub

    Private Sub CreateDialogs()
        dlgAbrir = New OpenFileDialog()
        dlgAbrir.Filter = Strings.OpenFileDialogFilter
        dlgAbrir.Title = Strings.OpenFileDialogTitle
    End Sub

    Private Sub WireEvents()
        AddHandler btnAdicionar.Click, AddressOf BtnAdicionar_Click
        AddHandler btnSalvar.Click, AddressOf BtnSalvar_Click
        AddHandler btnRemover.Click, AddressOf BtnRemover_Click
        AddHandler btnRefresh.Click, AddressOf BtnRefresh_Click
        AddHandler lstJogos.SelectedIndexChanged, AddressOf LstJogos_SelectedIndexChanged
        AddHandler btnDetectarInstalados.Click, AddressOf BtnDetectarInstalados_Click
        AddHandler btnConfig.Click, AddressOf BtnConfig_Click
        AddHandler lstProcessos.DoubleClick, AddressOf LstProcessos_DoubleClick

        Me.Controls.Add(lblTitulo)
        Me.Controls.Add(lblInfo)
        Me.Controls.Add(lstJogos)
        Me.Controls.Add(grpExtras)
        Me.Controls.Add(grpProcessos)
    End Sub

    Private Sub LoadGames()
        lstJogos.Items.Clear()
        _allGames.Clear()
        Try
            Dim games = GameRegistryService.LoadGames()
            For Each g In games
                _allGames.Add(g)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ApplyFilter()
    End Sub

    Private Sub ApplyFilter()
        Dim filter = txtSearch.Text.Trim()
        lstJogos.Items.Clear()
        For Each g In _allGames
            If String.IsNullOrEmpty(filter) OrElse g.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 Then
                lstJogos.Items.Add(g)
            End If
        Next

        If lstJogos.Items.Count = 0 Then
            If _allGames.Count = 0 Then
                lstJogos.Items.Add(Strings.NoGames)
            Else
                lstJogos.Items.Add(Strings.NoResults)
            End If
            lstJogos.ForeColor = AppTheme.GrayColor
        Else
            lstJogos.ForeColor = AppTheme.ForeColor
        End If
    End Sub

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs)
        ApplyFilter()
    End Sub

    Private Function HasSelectedGame() As Boolean
        Return lstJogos.SelectedItem IsNot Nothing AndAlso
               lstJogos.SelectedItem.ToString() <> Strings.NoGames AndAlso
               lstJogos.SelectedItem.ToString() <> Strings.NoResults
    End Function

    Private Function GetSelectedGameName() As String
        Return If(HasSelectedGame(), lstJogos.SelectedItem.ToString(), Nothing)
    End Function

    Private Function GetCurrentSettings() As PrioritySettings
        Return New PrioritySettings(
            PrioritySettings.CpuFromIndex(cboCpuPriority.SelectedIndex),
            PrioritySettings.IoFromIndex(cboIoPriority.SelectedIndex),
            PrioritySettings.PageFromIndex(cboPagePriority.SelectedIndex),
            PrioritySettings.AuditFromIndex(cboAuditLevel.SelectedIndex))
    End Function

    Private Sub ApplySettingsToUI(settings As PrioritySettings)
        cboCpuPriority.SelectedIndex = SafeIndex(settings.CpuIndex(), cboCpuPriority.Items.Count, 2)
        cboIoPriority.SelectedIndex = SafeIndex(settings.IoIndex(), cboIoPriority.Items.Count, 2)
        cboPagePriority.SelectedIndex = SafeIndex(settings.PageIndex(), cboPagePriority.Items.Count, 4)
        cboAuditLevel.SelectedIndex = SafeIndex(settings.AuditIndex(), cboAuditLevel.Items.Count, 0)
    End Sub

    Private Function SafeIndex(idx As Integer, maxItems As Integer, defaultIdx As Integer) As Integer
        Return If(idx >= 0 AndAlso idx < maxItems, idx, defaultIdx)
    End Function

    Private Sub ResetToDefaults()
        cboCpuPriority.SelectedIndex = 2
        cboIoPriority.SelectedIndex = 2
        cboPagePriority.SelectedIndex = 4
        cboAuditLevel.SelectedIndex = 0
    End Sub

    Private Sub BtnAdicionar_Click(sender As Object, e As EventArgs)
        If dlgAbrir.ShowDialog() <> DialogResult.OK Then Return
        Try
            GameRegistryService.AddGame(dlgAbrir.FileName, GetCurrentSettings())
            MessageBox.Show(Strings.MsgGameAdded, Strings.MsgSuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadGames()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Strings.MsgErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnSalvar_Click(sender As Object, e As EventArgs)
        Dim gameName = GetSelectedGameName()
        If gameName Is Nothing Then
            MessageBox.Show(Strings.MsgSelectGameModify, Strings.MsgWarningTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            GameRegistryService.UpdateGame(gameName, GetCurrentSettings())
            MessageBox.Show(Strings.MsgSettingsSaved, Strings.MsgSuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Strings.MsgErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnRemover_Click(sender As Object, e As EventArgs)
        Dim gameName = GetSelectedGameName()
        If gameName Is Nothing Then
            MessageBox.Show(Strings.MsgSelectGameRemove, Strings.MsgWarningTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim result = MessageBox.Show(Strings.MsgConfirmRemove(gameName), Strings.MsgConfirmTitle,
            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result <> DialogResult.Yes Then Return
        Try
            GameRegistryService.RemoveGame(gameName)
            LoadGames()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Strings.MsgErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        LoadGames()
    End Sub

    Private Sub LstJogos_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim gameName = GetSelectedGameName()
        If gameName Is Nothing Then
            ResetToDefaults()
            Return
        End If
        ApplySettingsToUI(GameRegistryService.GetPerfOptions(gameName))
    End Sub

    Private Async Sub BtnDetectarInstalados_Click(sender As Object, e As EventArgs)
        btnDetectarInstalados.Enabled = False
        lstProcessos.Items.Clear()
        lstProcessos.Items.Add(Strings.MsgSearchingGames)
        lstProcessos.Items.Add(Strings.MsgPleaseWait)
        lstProcessos.Refresh()

        Try
            Dim games = Await GameScannerService.DetectInstalledGamesAsync()
            lstProcessos.Items.Clear()
            If games.Count = 0 Then
                lstProcessos.Items.AddRange(New Object() {
                    Strings.MsgNoGamesFound,
                    "",
                    Strings.MsgCheckConfig,
                    Strings.MsgCheckFolders,
                    Strings.MsgCheckFoldersExist,
                    Strings.MsgCheckConfigFile
                })
            Else
                lstProcessos.Items.Add(Strings.MsgGamesFound(games.Count))
                For Each g In games
                    lstProcessos.Items.Add(g.DisplayName)
                Next
                lstProcessos.Items.Add("")
                lstProcessos.Items.Add(Strings.DoubleClickHint)
            End If
        Catch ex As Exception
            lstProcessos.Items.Clear()
            lstProcessos.Items.Add("Erro: " & ex.Message)
        Finally
            btnDetectarInstalados.Enabled = True
        End Try
    End Sub

    Private Sub BtnConfig_Click(sender As Object, e As EventArgs)
        Using cfg As New ConfigForm()
            cfg.ShowDialog()
        End Using
    End Sub

    Private Sub LstProcessos_DoubleClick(sender As Object, e As EventArgs)
        If lstProcessos.SelectedItem Is Nothing Then Return
        Dim texto = lstProcessos.SelectedItem.ToString()
        If texto.StartsWith("(") OrElse texto.StartsWith("Verifique") OrElse texto.StartsWith("Nenhum") OrElse texto.StartsWith("Erro") Then Return
        If texto.StartsWith("Duplo") OrElse texto.StartsWith("Encontrados") Then Return
        If String.IsNullOrWhiteSpace(texto) Then Return

        Dim nomeJogo = texto.Split("["c)(0).Trim()
        If Not nomeJogo.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) Then
            nomeJogo &= ".exe"
        End If
        Try
            GameRegistryService.AddGame(nomeJogo, GetCurrentSettings())
            MessageBox.Show(Strings.MsgGameAddedWithName(nomeJogo), Strings.MsgSuccessTitle,
                MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadGames()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Strings.MsgErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
