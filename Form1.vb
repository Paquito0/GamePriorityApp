Imports System.Windows.Forms

Public Class Form1
    Inherits Form

    Private lstJogos As ListBox
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
    Private btnDetectar As Button
    Private btnAbrirTxt As Button
    Private btnDetectarInstalados As Button
    Private btnConfig As Button
    Private lstProcessos As ListBox
    Private grpProcessos As GroupBox

    Public Sub New()
        GamePriorityManager.CriarArquivoJogosSeNaoExistir()
        InitializeComponent()
        LoadGames()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Game Priority Manager"
        Me.Size = New Drawing.Size(520, 750)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Drawing.Color.FromArgb(25, 25, 28)

        lblTitulo = New Label()
        lblTitulo.Text = "🎮 Game Priority Manager"
        lblTitulo.Font = New Drawing.Font("Segoe UI", 16, Drawing.FontStyle.Bold)
        lblTitulo.ForeColor = Drawing.Color.FromArgb(0, 200, 255)
        lblTitulo.Location = New Drawing.Point(20, 15)
        lblTitulo.AutoSize = True

        lblInfo = New Label()
        lblInfo.Text = "Gerencie prioridades de processos para jogos"
        lblInfo.Font = New Drawing.Font("Segoe UI", 9)
        lblInfo.ForeColor = Drawing.Color.FromArgb(120, 120, 120)
        lblInfo.Location = New Drawing.Point(20, 42)
        lblInfo.AutoSize = True

        Dim lblJogos As New Label()
        lblJogos.Text = "Jogos Configurados:"
        lblJogos.Font = New Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Bold)
        lblJogos.ForeColor = Drawing.Color.White
        lblJogos.Location = New Drawing.Point(20, 68)
        lblJogos.AutoSize = True
        Me.Controls.Add(lblJogos)

        lstJogos = New ListBox()
        lstJogos.Location = New Drawing.Point(20, 92)
        lstJogos.Size = New Drawing.Size(470, 180)
        lstJogos.BackColor = Drawing.Color.FromArgb(35, 35, 40)
        lstJogos.ForeColor = Drawing.Color.White
        lstJogos.BorderStyle = BorderStyle.FixedSingle
        lstJogos.Font = New Drawing.Font("Consolas", 9)

        Dim panelBotoesJogo As New FlowLayoutPanel()
        panelBotoesJogo.Location = New Drawing.Point(20, 278)
        panelBotoesJogo.Size = New Drawing.Size(470, 40)
        panelBotoesJogo.FlowDirection = FlowDirection.LeftToRight
        panelBotoesJogo.AutoSize = True

        btnAdicionar = New Button()
        btnAdicionar.Text = "➕ Adicionar"
        btnAdicionar.Size = New Drawing.Size(110, 32)
        btnAdicionar.BackColor = Drawing.Color.FromArgb(0, 140, 220)
        btnAdicionar.ForeColor = Drawing.Color.White
        btnAdicionar.FlatStyle = FlatStyle.Flat
        btnAdicionar.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)

        btnSalvar = New Button()
        btnSalvar.Text = "💾 Salvar"
        btnSalvar.Size = New Drawing.Size(110, 32)
        btnSalvar.BackColor = Drawing.Color.FromArgb(0, 170, 90)
        btnSalvar.ForeColor = Drawing.Color.White
        btnSalvar.FlatStyle = FlatStyle.Flat
        btnSalvar.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)

        btnRemover = New Button()
        btnRemover.Text = "🗑️ Remover"
        btnRemover.Size = New Drawing.Size(110, 32)
        btnRemover.BackColor = Drawing.Color.FromArgb(200, 60, 60)
        btnRemover.ForeColor = Drawing.Color.White
        btnRemover.FlatStyle = FlatStyle.Flat
        btnRemover.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)

        btnRefresh = New Button()
        btnRefresh.Text = "🔄 Atualizar"
        btnRefresh.Size = New Drawing.Size(110, 32)
        btnRefresh.BackColor = Drawing.Color.FromArgb(80, 80, 90)
        btnRefresh.ForeColor = Drawing.Color.White
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)

        panelBotoesJogo.Controls.Add(btnAdicionar)
        panelBotoesJogo.Controls.Add(btnSalvar)
        panelBotoesJogo.Controls.Add(btnRemover)
        panelBotoesJogo.Controls.Add(btnRefresh)

        grpExtras = New GroupBox()
        grpExtras.Text = "⚙️ Configurações de Prioridade"
        grpExtras.ForeColor = Drawing.Color.FromArgb(255, 180, 0)
        grpExtras.Font = New Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Bold)
        grpExtras.Location = New Drawing.Point(20, 330)
        grpExtras.Size = New Drawing.Size(470, 140)
        grpExtras.BackColor = Drawing.Color.FromArgb(30, 30, 35)

        lblCpu = New Label()
        lblCpu.Text = "CPU:"
        lblCpu.ForeColor = Drawing.Color.White
        lblCpu.Font = New Drawing.Font("Segoe UI", 9)
        lblCpu.Location = New Drawing.Point(15, 25)
        lblCpu.Size = New Drawing.Size(45, 18)

        cboCpuPriority = New ComboBox()
        cboCpuPriority.DropDownStyle = ComboBoxStyle.DropDownList
        cboCpuPriority.BackColor = Drawing.Color.FromArgb(50, 50, 55)
        cboCpuPriority.ForeColor = Drawing.Color.White
        cboCpuPriority.Font = New Drawing.Font("Segoe UI", 9)
        cboCpuPriority.Location = New Drawing.Point(65, 23)
        cboCpuPriority.Size = New Drawing.Size(380, 22)
        cboCpuPriority.Items.Add("1 - Idle (Ocioso)")
        cboCpuPriority.Items.Add("2 - Normal (Padrão)")
        cboCpuPriority.Items.Add("3 - High (Alta)")
        cboCpuPriority.Items.Add("5 - Below Normal")
        cboCpuPriority.Items.Add("6 - Above Normal")
        cboCpuPriority.SelectedIndex = 2

        lblIo = New Label()
        lblIo.Text = "I/O:"
        lblIo.ForeColor = Drawing.Color.White
        lblIo.Font = New Drawing.Font("Segoe UI", 9)
        lblIo.Location = New Drawing.Point(15, 55)
        lblIo.Size = New Drawing.Size(45, 18)

        cboIoPriority = New ComboBox()
        cboIoPriority.DropDownStyle = ComboBoxStyle.DropDownList
        cboIoPriority.BackColor = Drawing.Color.FromArgb(50, 50, 55)
        cboIoPriority.ForeColor = Drawing.Color.White
        cboIoPriority.Font = New Drawing.Font("Segoe UI", 9)
        cboIoPriority.Location = New Drawing.Point(65, 53)
        cboIoPriority.Size = New Drawing.Size(380, 22)
        cboIoPriority.Items.Add("0 - Very Low (Muito Baixa)")
        cboIoPriority.Items.Add("1 - Low (Baixa)")
        cboIoPriority.Items.Add("2 - Normal (Padrão)")
        cboIoPriority.SelectedIndex = 2

        lblPage = New Label()
        lblPage.Text = "RAM:"
        lblPage.ForeColor = Drawing.Color.White
        lblPage.Font = New Drawing.Font("Segoe UI", 9)
        lblPage.Location = New Drawing.Point(15, 85)
        lblPage.Size = New Drawing.Size(45, 18)

        cboPagePriority = New ComboBox()
        cboPagePriority.DropDownStyle = ComboBoxStyle.DropDownList
        cboPagePriority.BackColor = Drawing.Color.FromArgb(50, 50, 55)
        cboPagePriority.ForeColor = Drawing.Color.White
        cboPagePriority.Font = New Drawing.Font("Segoe UI", 9)
        cboPagePriority.Location = New Drawing.Point(65, 83)
        cboPagePriority.Size = New Drawing.Size(380, 22)
        cboPagePriority.Items.Add("1 - Very Low")
        cboPagePriority.Items.Add("2 - Low")
        cboPagePriority.Items.Add("3 - Medium")
        cboPagePriority.Items.Add("4 - Below Normal")
        cboPagePriority.Items.Add("5 - Normal (Padrão)")
        cboPagePriority.SelectedIndex = 4

        lblAudit = New Label()
        lblAudit.Text = "Log:"
        lblAudit.ForeColor = Drawing.Color.White
        lblAudit.Font = New Drawing.Font("Segoe UI", 9)
        lblAudit.Location = New Drawing.Point(15, 115)
        lblAudit.Size = New Drawing.Size(45, 18)

        cboAuditLevel = New ComboBox()
        cboAuditLevel.DropDownStyle = ComboBoxStyle.DropDownList
        cboAuditLevel.BackColor = Drawing.Color.FromArgb(50, 50, 55)
        cboAuditLevel.ForeColor = Drawing.Color.White
        cboAuditLevel.Font = New Drawing.Font("Segoe UI", 9)
        cboAuditLevel.Location = New Drawing.Point(65, 113)
        cboAuditLevel.Size = New Drawing.Size(380, 22)
        cboAuditLevel.Items.Add("0 - Desativado")
        cboAuditLevel.Items.Add("1 - Ativado")
        cboAuditLevel.SelectedIndex = 0

        grpExtras.Controls.Add(lblCpu)
        grpExtras.Controls.Add(cboCpuPriority)
        grpExtras.Controls.Add(lblIo)
        grpExtras.Controls.Add(cboIoPriority)
        grpExtras.Controls.Add(lblPage)
        grpExtras.Controls.Add(cboPagePriority)
        grpExtras.Controls.Add(lblAudit)
        grpExtras.Controls.Add(cboAuditLevel)

        grpProcessos = New GroupBox()
        grpProcessos.Text = "🔍 Detectar Jogos Instalados"
        grpProcessos.ForeColor = Drawing.Color.FromArgb(100, 220, 100)
        grpProcessos.Font = New Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Bold)
        grpProcessos.Location = New Drawing.Point(20, 485)
        grpProcessos.Size = New Drawing.Size(470, 130)
        grpProcessos.BackColor = Drawing.Color.FromArgb(30, 30, 35)

        lstProcessos = New ListBox()
        lstProcessos.Location = New Drawing.Point(15, 25)
        lstProcessos.Size = New Drawing.Size(270, 95)
        lstProcessos.BackColor = Drawing.Color.FromArgb(40, 40, 45)
        lstProcessos.ForeColor = Drawing.Color.White
        lstProcessos.BorderStyle = BorderStyle.FixedSingle
        lstProcessos.Font = New Drawing.Font("Consolas", 8)

        btnDetectar = New Button()
        btnDetectar.Text = "🔎 Detectar"
        btnDetectar.Location = New Drawing.Point(295, 25)
        btnDetectar.Size = New Drawing.Size(75, 30)
        btnDetectar.BackColor = Drawing.Color.FromArgb(150, 150, 50)
        btnDetectar.ForeColor = Drawing.Color.White
        btnDetectar.FlatStyle = FlatStyle.Flat
        btnDetectar.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)

        btnAbrirTxt = New Button()
        btnAbrirTxt.Text = "📝 Editar Lista"
        btnAbrirTxt.Location = New Drawing.Point(295, 60)
        btnAbrirTxt.Size = New Drawing.Size(75, 30)
        btnAbrirTxt.BackColor = Drawing.Color.FromArgb(80, 80, 120)
        btnAbrirTxt.ForeColor = Drawing.Color.White
        btnAbrirTxt.FlatStyle = FlatStyle.Flat
        btnAbrirTxt.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)

        btnDetectarInstalados = New Button()
        btnDetectarInstalados.Text = "🎮 Ver Jogos"
        btnDetectarInstalados.Location = New Drawing.Point(375, 25)
        btnDetectarInstalados.Size = New Drawing.Size(80, 30)
        btnDetectarInstalados.BackColor = Drawing.Color.FromArgb(0, 180, 100)
        btnDetectarInstalados.ForeColor = Drawing.Color.White
        btnDetectarInstalados.FlatStyle = FlatStyle.Flat
        btnDetectarInstalados.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)

        btnConfig = New Button()
        btnConfig.Text = "⚙️ Config"
        btnConfig.Location = New Drawing.Point(375, 60)
        btnConfig.Size = New Drawing.Size(80, 30)
        btnConfig.BackColor = Drawing.Color.FromArgb(100, 100, 130)
        btnConfig.ForeColor = Drawing.Color.White
        btnConfig.FlatStyle = FlatStyle.Flat
        btnConfig.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)

        Dim lblDica As New Label()
        lblDica.Text = "Duplo clique para adicionar"
        lblDica.Font = New Drawing.Font("Segoe UI", 8)
        lblDica.ForeColor = Drawing.Color.FromArgb(100, 100, 100)
        lblDica.Location = New Drawing.Point(295, 95)
        lblDica.AutoSize = True

        grpProcessos.Controls.Add(lstProcessos)
        grpProcessos.Controls.Add(btnDetectar)
        grpProcessos.Controls.Add(btnAbrirTxt)
        grpProcessos.Controls.Add(btnDetectarInstalados)
        grpProcessos.Controls.Add(btnConfig)
        grpProcessos.Controls.Add(lblDica)

        tipCpu = New ToolTip()
        tipCpu.IsBalloon = True
        tipCpu.ToolTipTitle = "Info"
        AddHandler cboCpuPriority.MouseHover, AddressOf CboCpuPriority_MouseHover
        AddHandler cboCpuPriority.SelectedIndexChanged, AddressOf CboCpuPriority_SelectedIndexChanged

        tipIo = New ToolTip()
        tipIo.IsBalloon = True
        tipIo.ToolTipTitle = "Info"
        AddHandler cboIoPriority.MouseHover, AddressOf CboIoPriority_MouseHover
        AddHandler cboIoPriority.SelectedIndexChanged, AddressOf CboIoPriority_SelectedIndexChanged

        tipPage = New ToolTip()
        tipPage.IsBalloon = True
        tipPage.ToolTipTitle = "Info"
        AddHandler cboPagePriority.MouseHover, AddressOf CboPagePriority_MouseHover
        AddHandler cboPagePriority.SelectedIndexChanged, AddressOf CboPagePriority_SelectedIndexChanged

        tipAudit = New ToolTip()
        tipAudit.IsBalloon = True
        tipAudit.ToolTipTitle = "Info"
        AddHandler cboAuditLevel.MouseHover, AddressOf CboAuditLevel_MouseHover
        AddHandler cboAuditLevel.SelectedIndexChanged, AddressOf CboAuditLevel_SelectedIndexChanged

        dlgAbrir = New OpenFileDialog()
        dlgAbrir.Filter = "Executáveis|*.exe"
        dlgAbrir.Title = "Selecionar Jogo"

        AddHandler btnAdicionar.Click, AddressOf BtnAdicionar_Click
        AddHandler btnSalvar.Click, AddressOf BtnSalvar_Click
        AddHandler btnRemover.Click, AddressOf BtnRemover_Click
        AddHandler btnRefresh.Click, AddressOf BtnRefresh_Click
        AddHandler lstJogos.SelectedIndexChanged, AddressOf LstJogos_SelectedIndexChanged
        AddHandler btnDetectar.Click, AddressOf BtnDetectar_Click
        AddHandler btnAbrirTxt.Click, AddressOf BtnAbrirTxt_Click
        AddHandler btnDetectarInstalados.Click, AddressOf BtnDetectarInstalados_Click
        AddHandler btnConfig.Click, AddressOf BtnConfig_Click
        AddHandler lstProcessos.DoubleClick, AddressOf LstProcessos_DoubleClick

        Me.Controls.Add(lblTitulo)
        Me.Controls.Add(lblInfo)
        Me.Controls.Add(lstJogos)
        Me.Controls.Add(panelBotoesJogo)
        Me.Controls.Add(grpExtras)
        Me.Controls.Add(grpProcessos)
    End Sub

    Private Sub LoadGames()
        lstJogos.Items.Clear()
        Dim games = GamePriorityManager.LoadGames()
        For Each g In games
            lstJogos.Items.Add(g)
        Next
        If lstJogos.Items.Count = 0 Then
            lstJogos.Items.Add("(nenhum jogo adicionado)")
            lstJogos.ForeColor = Drawing.Color.Gray
        Else
            lstJogos.ForeColor = Drawing.Color.White
        End If
    End Sub

    Private Function HasSelectedGame() As Boolean
        Return lstJogos.SelectedItem IsNot Nothing AndAlso lstJogos.SelectedItem.ToString() <> "(nenhum jogo adicionado)"
    End Function

    Private Function GetSelectedGameName() As String
        If HasSelectedGame() Then
            Return lstJogos.SelectedItem.ToString()
        End If
        Return Nothing
    End Function

    Private Function GetCurrentPriorityValues() As (Integer, Integer, Integer, Integer)
        Dim cpuVal = CpuIndexToValue(cboCpuPriority.SelectedIndex)
        Dim ioVal = cboIoPriority.SelectedIndex
        Dim pageVal = cboPagePriority.SelectedIndex + 1
        Dim auditVal = cboAuditLevel.SelectedIndex
        Return (cpuVal, ioVal, pageVal, auditVal)
    End Function

    Private Sub ResetToDefaults()
        cboCpuPriority.SelectedIndex = 2
        cboIoPriority.SelectedIndex = 2
        cboPagePriority.SelectedIndex = 4
        cboAuditLevel.SelectedIndex = 0
    End Sub

    Private Sub BtnAdicionar_Click(sender As Object, e As EventArgs)
        If dlgAbrir.ShowDialog() = DialogResult.OK Then
            Try
                Dim values = GetCurrentPriorityValues()
                GamePriorityManager.AddGamePriority(dlgAbrir.FileName, values.Item1, values.Item2, values.Item3, values.Item4)
                MessageBox.Show("Jogo adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadGames()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub BtnSalvar_Click(sender As Object, e As EventArgs)
        Dim gameName = GetSelectedGameName()
        If gameName Is Nothing Then
            MessageBox.Show("Selecione um jogo para modificar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim values = GetCurrentPriorityValues()
            GamePriorityManager.UpdateGamePriority(gameName, values.Item1, values.Item2, values.Item3, values.Item4)
            MessageBox.Show("Configurações salvas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnRemover_Click(sender As Object, e As EventArgs)
        Dim gameName = GetSelectedGameName()
        If gameName Is Nothing Then
            MessageBox.Show("Selecione um jogo para remover.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim result = MessageBox.Show("Remover prioridade de " & gameName & "?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Try
                GamePriorityManager.RemoveGamePriority(gameName)
                LoadGames()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        LoadGames()
    End Sub

    Private Sub LstJogos_SelectedIndexChanged(sender As Object, e As EventArgs)
        If Not HasSelectedGame() Then
            ResetToDefaults()
            Return
        End If

        Dim gameName = GetSelectedGameName()
        Dim values = GamePriorityManager.GetPerfOptions(gameName)
        cboCpuPriority.SelectedIndex = CpuValueToIndex(values.Item1)

        cboIoPriority.SelectedIndex = ClampIndex(values.Item2, cboIoPriority.Items.Count, 2)
        cboPagePriority.SelectedIndex = ClampIndex(values.Item3 - 1, cboPagePriority.Items.Count, 4)
        cboAuditLevel.SelectedIndex = ClampIndex(values.Item4, cboAuditLevel.Items.Count, 0)
    End Sub

    Private Function ClampIndex(idx As Integer, maxItems As Integer, defaultIdx As Integer) As Integer
        If idx >= 0 AndAlso idx < maxItems Then
            Return idx
        End If
        Return defaultIdx
    End Function

    Private Function CpuIndexToValue(idx As Integer) As Integer
        Select Case idx
            Case 0 : Return 1
            Case 1 : Return 2
            Case 2 : Return 3
            Case 3 : Return 5
            Case 4 : Return 6
            Case Else : Return 2
        End Select
    End Function

    Private Function CpuValueToIndex(val As Integer) As Integer
        Select Case val
            Case 1 : Return 0
            Case 2 : Return 1
            Case 3 : Return 2
            Case 5 : Return 3
            Case 6 : Return 4
            Case Else : Return 1
        End Select
    End Function

    Private Function GetCpuDescription(idx As Integer) As String
        Select Case idx
            Case 0 : Return "Idle (1): O jogo só roda quando o PC está livre. Pode causar lag."
            Case 1 : Return "Normal (2): Padrão do Windows. Equilíbrio estável."
            Case 2 : Return "High (3): Jogo ganha mais CPU. Aumenta FPS, apps de fundo ficam lentos."
            Case 3 : Return "Below Normal (5): Menos prioridade. Útil para multitarefa."
            Case 4 : Return "Above Normal (6): Um pouco acima do normal. Bom equilíbrio."
            Case Else : Return "Selecione uma opção."
        End Select
    End Function

    Private Sub CboCpuPriority_MouseHover(sender As Object, e As EventArgs)
        tipCpu.Show(GetCpuDescription(cboCpuPriority.SelectedIndex), cboCpuPriority, cboCpuPriority.Width, 0, 4000)
    End Sub

    Private Sub CboCpuPriority_SelectedIndexChanged(sender As Object, e As EventArgs)
        tipCpu.Show(GetCpuDescription(cboCpuPriority.SelectedIndex), cboCpuPriority, cboCpuPriority.Width, 0, 3000)
    End Sub

    Private Function GetIoDescription(idx As Integer) As String
        Select Case idx
            Case 0 : Return "Very Low (0): Acesso mínimo ao disco. Pode lentidão ao carregar mapas."
            Case 1 : Return "Low (1): Acesso reduzido ao disco."
            Case 2 : Return "Normal (2): Padrão do Windows. Melhor para jogos."
            Case Else : Return "Selecione uma opção."
        End Select
    End Function

    Private Sub CboIoPriority_MouseHover(sender As Object, e As EventArgs)
        tipIo.Show(GetIoDescription(cboIoPriority.SelectedIndex), cboIoPriority, cboIoPriority.Width, 0, 4000)
    End Sub

    Private Sub CboIoPriority_SelectedIndexChanged(sender As Object, e As EventArgs)
        tipIo.Show(GetIoDescription(cboIoPriority.SelectedIndex), cboIoPriority, cboIoPriority.Width, 0, 3000)
    End Sub

    Private Function GetPageDescription(idx As Integer) As String
        Select Case idx
            Case 0 : Return "Very Low (1): Memória liberada rápido. Pode causar stutter."
            Case 1 : Return "Low (2): Prioridade baixa de memória."
            Case 2 : Return "Medium (3): Prioridade média."
            Case 3 : Return "Below Normal (4): Abaixo do normal."
            Case 4 : Return "Normal (5): Mantém dados do jogo na RAM mais tempo. Evita stutter."
            Case Else : Return "Selecione uma opção."
        End Select
    End Function

    Private Sub CboPagePriority_MouseHover(sender As Object, e As EventArgs)
        tipPage.Show(GetPageDescription(cboPagePriority.SelectedIndex), cboPagePriority, cboPagePriority.Width, 0, 4000)
    End Sub

    Private Sub CboPagePriority_SelectedIndexChanged(sender As Object, e As EventArgs)
        tipPage.Show(GetPageDescription(cboPagePriority.SelectedIndex), cboPagePriority, cboPagePriority.Width, 0, 3000)
    End Sub

    Private Function GetAuditDescription(idx As Integer) As String
        Select Case idx
            Case 0 : Return "Desativado (0): Não gera logs. Recomendado para jogos."
            Case 1 : Return "Ativado (1): Gera logs no Event Viewer. Útil para debug."
            Case Else : Return "Selecione uma opção."
        End Select
    End Function

    Private Sub CboAuditLevel_MouseHover(sender As Object, e As EventArgs)
        tipAudit.Show(GetAuditDescription(cboAuditLevel.SelectedIndex), cboAuditLevel, cboAuditLevel.Width, 0, 4000)
    End Sub

    Private Sub CboAuditLevel_SelectedIndexChanged(sender As Object, e As EventArgs)
        tipAudit.Show(GetAuditDescription(cboAuditLevel.SelectedIndex), cboAuditLevel, cboAuditLevel.Width, 0, 3000)
    End Sub

    Private Sub BtnDetectar_Click(sender As Object, e As EventArgs)
        lstProcessos.Items.Clear()
        Dim processos As New List(Of String)()
        Try
            Dim allProcs = Diagnostics.Process.GetProcesses()
            For Each p As Diagnostics.Process In allProcs
                Try
                    If Not String.IsNullOrEmpty(p.ProcessName) Then
                        Dim procName = p.ProcessName & ".exe"
                        If Not processos.Contains(procName) Then
                            processos.Add(procName)
                        End If
                    End If
                Catch
                End Try
            Next
            processos.Sort()
            For Each p In processos
                lstProcessos.Items.Add(p)
            Next
            If lstProcessos.Items.Count = 0 Then
                lstProcessos.Items.Add("(nenhum processo encontrado)")
            End If
        Catch ex As Exception
            MessageBox.Show("Erro ao detectar processos: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LstProcessos_DoubleClick(sender As Object, e As EventArgs)
        If lstProcessos.SelectedItem IsNot Nothing Then
            Dim texto = lstProcessos.SelectedItem.ToString()
            If texto.StartsWith("(") Then Return
            Dim nomeJogo = texto.Split("("c)(0).Trim()
            If nomeJogo.Contains("[") Then
                nomeJogo = nomeJogo.Split("["c)(0).Trim()
            End If
            If Not nomeJogo.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) Then
                nomeJogo &= ".exe"
            End If
            Try
                Dim values = GetCurrentPriorityValues()
                GamePriorityManager.AddGamePriority(nomeJogo, values.Item1, values.Item2, values.Item3, values.Item4)
                MessageBox.Show(nomeJogo & " adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadGames()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub BtnAbrirTxt_Click(sender As Object, e As EventArgs)
        GamePriorityManager.CriarArquivoJogosSeNaoExistir()
        Dim caminho = GamePriorityManager.GetJogosTxtPath()
        Try
            Process.Start("notepad.exe", caminho)
        Catch ex As Exception
            MessageBox.Show("Erro ao abrir arquivo: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnDetectarInstalados_Click(sender As Object, e As EventArgs)
        lstProcessos.Items.Clear()
        lstProcessos.Items.Add("🔍 Procurando jogos...")
        lstProcessos.Items.Add("(aguarde...)")
        lstProcessos.Refresh()

        Try
            Dim jogos = GamePriorityManager.DetectarJogosInstalados()
            lstProcessos.Items.Clear()
            If jogos.Count = 0 Then
                lstProcessos.Items.Add("❌ Nenhum jogo encontrado!")
                lstProcessos.Items.Add("")
                lstProcessos.Items.Add("Verifique:")
                lstProcessos.Items.Add("- As pastas em Config (⚙️)")
                lstProcessos.Items.Add("- Se as pastas existem")
                lstProcessos.Items.Add("- O arquivo jogos.txt")
            Else
                lstProcessos.Items.Add("✅ Encontrados " & jogos.Count & " jogos:")
                For Each j In jogos
                    lstProcessos.Items.Add(j.Item1)
                Next
                lstProcessos.Items.Add("")
                lstProcessos.Items.Add("💡 Duplo clique para adicionar")
            End If
        Catch ex As Exception
            lstProcessos.Items.Clear()
            lstProcessos.Items.Add("❌ Erro: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnConfig_Click(sender As Object, e As EventArgs)
        Dim configForm As New Form()
        configForm.Text = "⚙️ Configurações de Pastas"
        configForm.Size = New Drawing.Size(550, 450)
        configForm.StartPosition = FormStartPosition.CenterParent
        configForm.BackColor = Drawing.Color.FromArgb(25, 25, 28)
        configForm.FormBorderStyle = FormBorderStyle.FixedDialog
        configForm.MaximizeBox = False

        Dim lblTitulo As New Label()
        lblTitulo.Text = "Pastas onde o app busca jogos:"
        lblTitulo.Font = New Drawing.Font("Segoe UI", 11, Drawing.FontStyle.Bold)
        lblTitulo.ForeColor = Drawing.Color.White
        lblTitulo.Location = New Drawing.Point(20, 15)
        lblTitulo.AutoSize = True
        configForm.Controls.Add(lblTitulo)

        Dim pastasPadrao = GamePriorityManager.GetPastasPadrao()
        System.Diagnostics.Debug.WriteLine("Steam: " & pastasPadrao(0).Item2)
        System.Diagnostics.Debug.WriteLine("Epic: " & pastasPadrao(1).Item2)
        System.Diagnostics.Debug.WriteLine("GOG: " & pastasPadrao(2).Item2)
        System.Diagnostics.Debug.WriteLine("Origin: " & pastasPadrao(3).Item2)

        Dim yPos As Integer = 50
        Dim txtSteam As New TextBox()
        Dim txtEpic As New TextBox()
        Dim txtGOG As New TextBox()
        Dim txtOrigin As New TextBox()
        Dim lstPersonalizadas As New ListBox()
        Dim btnAddPasta As New Button()
        Dim btnRemovePasta As New Button()
        Dim btnSalvar As New Button()
        Dim btnCancelar As New Button()

        Dim steamPath = pastasPadrao(0).Item2
        Dim epicPath = pastasPadrao(1).Item2
        Dim gogPath = pastasPadrao(2).Item2
        Dim originPath = pastasPadrao(3).Item2

        If String.IsNullOrWhiteSpace(steamPath) Then steamPath = "C:\Program Files (x86)\Steam\steamapps\common"
        If String.IsNullOrWhiteSpace(epicPath) Then epicPath = "C:\Program Files\Epic Games"
        If String.IsNullOrWhiteSpace(gogPath) Then gogPath = "C:\GOG Games"
        If String.IsNullOrWhiteSpace(originPath) Then originPath = "C:\Program Files\Origin Games"

        Dim services() As Tuple(Of String, String, TextBox) = {
            Tuple.Create("Steam:", steamPath, txtSteam),
            Tuple.Create("Epic Games:", epicPath, txtEpic),
            Tuple.Create("GOG:", gogPath, txtGOG),
            Tuple.Create("Origin:", originPath, txtOrigin)
        }

        For Each svc In services
            Dim lbl As New Label()
            lbl.Text = svc.Item1
            lbl.ForeColor = Drawing.Color.White
            lbl.Location = New Drawing.Point(20, yPos)
            lbl.Size = New Drawing.Size(100, 20)
            configForm.Controls.Add(lbl)

            svc.Item3.Location = New Drawing.Point(130, yPos)
            svc.Item3.Size = New Drawing.Size(280, 22)
            svc.Item3.BackColor = Drawing.Color.FromArgb(40, 40, 45)
            svc.Item3.ForeColor = Drawing.Color.White
            configForm.Controls.Add(svc.Item3)

            Dim btnBrowse As New Button()
            btnBrowse.Text = "📁"
            btnBrowse.Location = New Drawing.Point(420, yPos)
            btnBrowse.Size = New Drawing.Size(35, 22)
            btnBrowse.BackColor = Drawing.Color.FromArgb(60, 60, 70)
            btnBrowse.ForeColor = Drawing.Color.White
            Dim txtRef = svc.Item3
            AddHandler btnBrowse.Click, Sub()
                Using fbd As New FolderBrowserDialog()
                    fbd.Description = "Selecionar pasta do " & svc.Item1
                    If fbd.ShowDialog() = DialogResult.OK Then
                        txtRef.Text = fbd.SelectedPath
                    End If
                End Using
            End Sub
            configForm.Controls.Add(btnBrowse)

            yPos += 30
        Next

        Dim lblPersonalizadas As New Label()
        lblPersonalizadas.Text = "Pastas Personalizadas:"
        lblPersonalizadas.Font = New Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Bold)
        lblPersonalizadas.ForeColor = Drawing.Color.FromArgb(100, 220, 100)
        lblPersonalizadas.Location = New Drawing.Point(20, yPos + 10)
        lblPersonalizadas.AutoSize = True
        configForm.Controls.Add(lblPersonalizadas)

        yPos += 40

        lstPersonalizadas.Location = New Drawing.Point(20, yPos)
        lstPersonalizadas.Size = New Drawing.Size(370, 100)
        lstPersonalizadas.BackColor = Drawing.Color.FromArgb(40, 40, 45)
        lstPersonalizadas.ForeColor = Drawing.Color.White
        For Each p In GamePriorityManager.LerPastasPersonalizadas()
            lstPersonalizadas.Items.Add(p)
        Next
        configForm.Controls.Add(lstPersonalizadas)

        btnAddPasta.Text = "+"
        btnAddPasta.Location = New Drawing.Point(400, yPos)
        btnAddPasta.Size = New Drawing.Size(55, 25)
        btnAddPasta.BackColor = Drawing.Color.FromArgb(0, 140, 80)
        btnAddPasta.ForeColor = Drawing.Color.White
        AddHandler btnAddPasta.Click, Sub()
            Using fbd As New FolderBrowserDialog()
                fbd.Description = "Selecionar pasta personalizada"
                If fbd.ShowDialog() = DialogResult.OK Then
                    lstPersonalizadas.Items.Add(fbd.SelectedPath)
                End If
            End Using
        End Sub
        configForm.Controls.Add(btnAddPasta)

        btnRemovePasta.Text = "-"
        btnRemovePasta.Location = New Drawing.Point(400, yPos + 30)
        btnRemovePasta.Size = New Drawing.Size(55, 25)
        btnRemovePasta.BackColor = Drawing.Color.FromArgb(180, 50, 50)
        btnRemovePasta.ForeColor = Drawing.Color.White
        AddHandler btnRemovePasta.Click, Sub()
            If lstPersonalizadas.SelectedIndex >= 0 Then
                lstPersonalizadas.Items.RemoveAt(lstPersonalizadas.SelectedIndex)
            End If
        End Sub
        configForm.Controls.Add(btnRemovePasta)

        btnSalvar.Text = "💾 Salvar"
        btnSalvar.Location = New Drawing.Point(280, 360)
        btnSalvar.Size = New Drawing.Size(100, 35)
        btnSalvar.BackColor = Drawing.Color.FromArgb(0, 140, 200)
        btnSalvar.ForeColor = Drawing.Color.White
        btnSalvar.FlatStyle = FlatStyle.Flat

        btnCancelar.Text = "Cancelar"
        btnCancelar.Location = New Drawing.Point(390, 360)
        btnCancelar.Size = New Drawing.Size(100, 35)
        btnCancelar.BackColor = Drawing.Color.FromArgb(100, 100, 100)
        btnCancelar.ForeColor = Drawing.Color.White
        btnCancelar.FlatStyle = FlatStyle.Flat

        AddHandler btnSalvar.Click, Sub()
            Dim novasConfig As New Dictionary(Of String, String)
            novasConfig("Steam") = txtSteam.Text
            novasConfig("Epic") = txtEpic.Text
            novasConfig("GOG") = txtGOG.Text
            novasConfig("Origin") = txtOrigin.Text
            GamePriorityManager.SalvarConfiguracaoPastas(novasConfig, lstPersonalizadas.Items.Cast(Of String).ToList())
            configForm.Close()
        End Sub

        AddHandler btnCancelar.Click, Sub()
            configForm.Close()
        End Sub

        configForm.Controls.Add(btnSalvar)
        configForm.Controls.Add(btnCancelar)

        configForm.ShowDialog()
    End Sub
End Class