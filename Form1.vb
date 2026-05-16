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

    Public Sub New()
        InitializeComponent()
        LoadGames()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Game Priority Manager"
        Me.Size = New Drawing.Size(450, 620)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Drawing.Color.FromArgb(30, 30, 30)

        lblTitulo = New Label()
        lblTitulo.Text = "Jogos em Alta Prioridade"
        lblTitulo.Font = New Drawing.Font("Segoe UI", 14, Drawing.FontStyle.Bold)
        lblTitulo.ForeColor = Drawing.Color.Cyan
        lblTitulo.Location = New Drawing.Point(20, 15)
        lblTitulo.AutoSize = True

        lblInfo = New Label()
        lblInfo.Text = "Adicione .exe de jogos para configurar prioridades"
        lblInfo.Font = New Drawing.Font("Segoe UI", 8)
        lblInfo.ForeColor = Drawing.Color.Gray
        lblInfo.Location = New Drawing.Point(20, 45)
        lblInfo.AutoSize = True

        lstJogos = New ListBox()
        lstJogos.Location = New Drawing.Point(20, 75)
        lstJogos.Size = New Drawing.Size(390, 240)
        lstJogos.BackColor = Drawing.Color.FromArgb(40, 40, 40)
        lstJogos.ForeColor = Drawing.Color.White
        lstJogos.BorderStyle = BorderStyle.FixedSingle
        lstJogos.Font = New Drawing.Font("Consolas", 9)

        grpExtras = New GroupBox()
        grpExtras.Text = "Configurar Valores (personalize)"
        grpExtras.ForeColor = Drawing.Color.Orange
        grpExtras.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)
        grpExtras.Location = New Drawing.Point(20, 325)
        grpExtras.Size = New Drawing.Size(390, 150)

        lblCpu = New Label()
        lblCpu.Text = "CpuPriorityClass:"
        lblCpu.ForeColor = Drawing.Color.White
        lblCpu.Font = New Drawing.Font("Segoe UI", 8)
        lblCpu.Location = New Drawing.Point(10, 22)
        lblCpu.Size = New Drawing.Size(110, 20)

        cboCpuPriority = New ComboBox()
        cboCpuPriority.DropDownStyle = ComboBoxStyle.DropDownList
        cboCpuPriority.BackColor = Drawing.Color.FromArgb(50, 50, 50)
        cboCpuPriority.ForeColor = Drawing.Color.White
        cboCpuPriority.Font = New Drawing.Font("Segoe UI", 8)
        cboCpuPriority.Location = New Drawing.Point(130, 20)
        cboCpuPriority.Size = New Drawing.Size(240, 22)
        cboCpuPriority.Items.Add("1 - Idle")
        cboCpuPriority.Items.Add("2 - Normal (padrão)")
        cboCpuPriority.Items.Add("3 - High")
        cboCpuPriority.Items.Add("5 - Below Normal")
        cboCpuPriority.Items.Add("6 - Above Normal")
        cboCpuPriority.SelectedIndex = 2

        lblIo = New Label()
        lblIo.Text = "IoPriority:"
        lblIo.ForeColor = Drawing.Color.White
        lblIo.Font = New Drawing.Font("Segoe UI", 8)
        lblIo.Location = New Drawing.Point(10, 50)
        lblIo.Size = New Drawing.Size(110, 20)

        cboIoPriority = New ComboBox()
        cboIoPriority.DropDownStyle = ComboBoxStyle.DropDownList
        cboIoPriority.BackColor = Drawing.Color.FromArgb(50, 50, 50)
        cboIoPriority.ForeColor = Drawing.Color.White
        cboIoPriority.Font = New Drawing.Font("Segoe UI", 8)
        cboIoPriority.Location = New Drawing.Point(130, 48)
        cboIoPriority.Size = New Drawing.Size(240, 22)
        cboIoPriority.Items.Add("0 - Very Low")
        cboIoPriority.Items.Add("1 - Low")
        cboIoPriority.Items.Add("2 - Normal (padrão)")
        cboIoPriority.SelectedIndex = 2

        lblPage = New Label()
        lblPage.Text = "PagePriority:"
        lblPage.ForeColor = Drawing.Color.White
        lblPage.Font = New Drawing.Font("Segoe UI", 8)
        lblPage.Location = New Drawing.Point(10, 78)
        lblPage.Size = New Drawing.Size(110, 20)

        cboPagePriority = New ComboBox()
        cboPagePriority.DropDownStyle = ComboBoxStyle.DropDownList
        cboPagePriority.BackColor = Drawing.Color.FromArgb(50, 50, 50)
        cboPagePriority.ForeColor = Drawing.Color.White
        cboPagePriority.Font = New Drawing.Font("Segoe UI", 8)
        cboPagePriority.Location = New Drawing.Point(130, 76)
        cboPagePriority.Size = New Drawing.Size(240, 22)
        cboPagePriority.Items.Add("1 - Very Low")
        cboPagePriority.Items.Add("2 - Low")
        cboPagePriority.Items.Add("3 - Medium")
        cboPagePriority.Items.Add("4 - Below Normal")
        cboPagePriority.Items.Add("5 - Normal (padrão)")
        cboPagePriority.SelectedIndex = 4

        lblAudit = New Label()
        lblAudit.Text = "AuditLevel:"
        lblAudit.ForeColor = Drawing.Color.White
        lblAudit.Font = New Drawing.Font("Segoe UI", 8)
        lblAudit.Location = New Drawing.Point(10, 106)
        lblAudit.Size = New Drawing.Size(110, 20)

        cboAuditLevel = New ComboBox()
        cboAuditLevel.DropDownStyle = ComboBoxStyle.DropDownList
        cboAuditLevel.BackColor = Drawing.Color.FromArgb(50, 50, 50)
        cboAuditLevel.ForeColor = Drawing.Color.White
        cboAuditLevel.Font = New Drawing.Font("Segoe UI", 8)
        cboAuditLevel.Location = New Drawing.Point(130, 104)
        cboAuditLevel.Size = New Drawing.Size(240, 22)
        cboAuditLevel.Items.Add("0 - Desativado")
        cboAuditLevel.Items.Add("1 - Ativado")
        cboAuditLevel.SelectedIndex = 0

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

        grpExtras.Controls.Add(lblCpu)
        grpExtras.Controls.Add(cboCpuPriority)
        grpExtras.Controls.Add(lblIo)
        grpExtras.Controls.Add(cboIoPriority)
        grpExtras.Controls.Add(lblPage)
        grpExtras.Controls.Add(cboPagePriority)
        grpExtras.Controls.Add(lblAudit)
        grpExtras.Controls.Add(cboAuditLevel)

        btnAdicionar = New Button()
        btnAdicionar.Text = "Adicionar Jogo"
        btnAdicionar.Location = New Drawing.Point(20, 490)
        btnAdicionar.Size = New Drawing.Size(100, 35)
        btnAdicionar.BackColor = Drawing.Color.FromArgb(0, 120, 215)
        btnAdicionar.ForeColor = Drawing.Color.White
        btnAdicionar.FlatStyle = FlatStyle.Flat
        btnAdicionar.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)

        btnSalvar = New Button()
        btnSalvar.Text = "Salvar Alterações"
        btnSalvar.Location = New Drawing.Point(130, 490)
        btnSalvar.Size = New Drawing.Size(120, 35)
        btnSalvar.BackColor = Drawing.Color.FromArgb(0, 150, 80)
        btnSalvar.ForeColor = Drawing.Color.White
        btnSalvar.FlatStyle = FlatStyle.Flat
        btnSalvar.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)

        btnRemover = New Button()
        btnRemover.Text = "Remover Selecionado"
        btnRemover.Location = New Drawing.Point(260, 490)
        btnRemover.Size = New Drawing.Size(120, 35)
        btnRemover.BackColor = Drawing.Color.FromArgb(180, 40, 40)
        btnRemover.ForeColor = Drawing.Color.White
        btnRemover.FlatStyle = FlatStyle.Flat
        btnRemover.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)

        btnRefresh = New Button()
        btnRefresh.Text = "Atualizar"
        btnRefresh.Location = New Drawing.Point(20, 535)
        btnRefresh.Size = New Drawing.Size(360, 35)
        btnRefresh.BackColor = Drawing.Color.FromArgb(60, 60, 60)
        btnRefresh.ForeColor = Drawing.Color.White
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)

        dlgAbrir = New OpenFileDialog()
        dlgAbrir.Filter = "Executáveis|*.exe"
        dlgAbrir.Title = "Selecionar Jogo"

        AddHandler btnAdicionar.Click, AddressOf BtnAdicionar_Click
        AddHandler btnSalvar.Click, AddressOf BtnSalvar_Click
        AddHandler btnRemover.Click, AddressOf BtnRemover_Click
        AddHandler btnRefresh.Click, AddressOf BtnRefresh_Click
        AddHandler lstJogos.SelectedIndexChanged, AddressOf LstJogos_SelectedIndexChanged

        Me.Controls.Add(lblTitulo)
        Me.Controls.Add(lblInfo)
        Me.Controls.Add(lstJogos)
        Me.Controls.Add(grpExtras)
        Me.Controls.Add(btnAdicionar)
        Me.Controls.Add(btnSalvar)
        Me.Controls.Add(btnRemover)
        Me.Controls.Add(btnRefresh)
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
End Class