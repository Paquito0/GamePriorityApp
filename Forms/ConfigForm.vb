Imports System.IO
Imports System.Windows.Forms
Imports GamePriorityApp.Models
Imports GamePriorityApp.Resources
Imports GamePriorityApp.Services

Namespace Forms

    Public Class ConfigForm
        Inherits Form

        Private _services As List(Of ServiceFolder)
        Private _customFolders As List(Of String)

        Public Sub New()
            _services = AppConfigService.GetServiceFolders() _
                .Select(Function(s) New ServiceFolder(s.Key, s.Path, s.DefaultPath)) _
                .ToList()
            _customFolders = AppConfigService.GetCustomFolders().ToList()
            InitializeComponent()
            BuildLayout()
        End Sub

        Private Sub InitializeComponent()
            Me.Text = "⚙️ Configurações de Pastas"
            Me.Size = New Drawing.Size(620, 450)
            Me.StartPosition = FormStartPosition.CenterParent
            Me.BackColor = AppTheme.BackColor
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.ShowInTaskbar = False
        End Sub

        Private Sub BuildLayout()
            Me.Controls.Clear()

            Dim lblTitulo As New Label()
            lblTitulo.Text = "Pastas onde o app busca jogos:"
            lblTitulo.Font = AppTheme.DialogTitleFont
            lblTitulo.ForeColor = AppTheme.ForeColor
            lblTitulo.Location = New Drawing.Point(20, 15)
            lblTitulo.AutoSize = True
            Me.Controls.Add(lblTitulo)

            Dim yPos As Integer = 50
            For Each svc In _services
                AddServiceRow(svc, yPos)
                yPos += 30
            Next

            Dim lblPersonalizadas As New Label()
            lblPersonalizadas.Text = "Pastas Personalizadas:"
            lblPersonalizadas.Font = AppTheme.GroupFont
            lblPersonalizadas.ForeColor = AppTheme.SuccessColor
            lblPersonalizadas.Location = New Drawing.Point(20, yPos + 10)
            lblPersonalizadas.AutoSize = True
            Me.Controls.Add(lblPersonalizadas)

            yPos += 40

            Dim lstPersonalizadas As New ListBox()
            lstPersonalizadas.Name = "lstPersonalizadas"
            lstPersonalizadas.Location = New Drawing.Point(20, yPos)
            lstPersonalizadas.Size = New Drawing.Size(370, 100)
            lstPersonalizadas.BackColor = AppTheme.TextBackColor
            lstPersonalizadas.ForeColor = AppTheme.ForeColor
            lstPersonalizadas.BorderStyle = BorderStyle.FixedSingle
            For Each p In _customFolders
                lstPersonalizadas.Items.Add(p)
            Next
            Me.Controls.Add(lstPersonalizadas)

            Dim btnAddPasta As New Button()
            btnAddPasta.Text = "+"
            btnAddPasta.Location = New Drawing.Point(400, yPos)
            btnAddPasta.Size = New Drawing.Size(55, 25)
            btnAddPasta.BackColor = AppTheme.AddFolderButton
            btnAddPasta.ForeColor = AppTheme.ForeColor
            AddHandler btnAddPasta.Click, Sub()
                Using fbd As New FolderBrowserDialog()
                    fbd.Description = "Selecionar pasta personalizada"
                    If fbd.ShowDialog() = DialogResult.OK Then
                        lstPersonalizadas.Items.Add(fbd.SelectedPath)
                    End If
                End Using
            End Sub
            Me.Controls.Add(btnAddPasta)

            Dim btnRemovePasta As New Button()
            btnRemovePasta.Text = "-"
            btnRemovePasta.Location = New Drawing.Point(400, yPos + 30)
            btnRemovePasta.Size = New Drawing.Size(55, 25)
            btnRemovePasta.BackColor = AppTheme.RemoveFolderButton
            btnRemovePasta.ForeColor = AppTheme.ForeColor
            AddHandler btnRemovePasta.Click, Sub()
                If lstPersonalizadas.SelectedIndex >= 0 Then
                    lstPersonalizadas.Items.RemoveAt(lstPersonalizadas.SelectedIndex)
                End If
            End Sub
            Me.Controls.Add(btnRemovePasta)

            Dim btnSalvar As New Button()
            btnSalvar.Text = "💾 Salvar"
            btnSalvar.Location = New Drawing.Point(290, 360)
            btnSalvar.Size = New Drawing.Size(100, 35)
            btnSalvar.BackColor = AppTheme.SaveButton
            btnSalvar.ForeColor = AppTheme.ForeColor
            btnSalvar.FlatStyle = FlatStyle.Flat
            btnSalvar.Font = AppTheme.ButtonFont
            AddHandler btnSalvar.Click, Sub() HandleSave(lstPersonalizadas)
            Me.Controls.Add(btnSalvar)

            Dim btnCancelar As New Button()
            btnCancelar.Text = "Cancelar"
            btnCancelar.Location = New Drawing.Point(400, 360)
            btnCancelar.Size = New Drawing.Size(100, 35)
            btnCancelar.BackColor = AppTheme.CancelButton
            btnCancelar.ForeColor = AppTheme.ForeColor
            btnCancelar.FlatStyle = FlatStyle.Flat
            btnCancelar.Font = AppTheme.ButtonFont
            AddHandler btnCancelar.Click, Sub() Me.DialogResult = DialogResult.Cancel
            Me.Controls.Add(btnCancelar)

            Me.AcceptButton = btnSalvar
            Me.CancelButton = btnCancelar
        End Sub

        Private Sub AddServiceRow(svc As ServiceFolder, yPos As Integer)
            Dim lbl As New Label()
            lbl.Text = $"{svc.Key}:"
            lbl.ForeColor = AppTheme.ForeColor
            lbl.Location = New Drawing.Point(20, yPos)
            lbl.Size = New Drawing.Size(100, 20)
            Me.Controls.Add(lbl)

            Dim txt As New TextBox()
            txt.Name = $"txt{svc.Key}"
            txt.Text = svc.Path
            txt.Location = New Drawing.Point(130, yPos)
            txt.Size = New Drawing.Size(280, 22)
            txt.BackColor = AppTheme.TextBackColor
            txt.ForeColor = AppTheme.ForeColor
            Me.Controls.Add(txt)

            Dim btnBrowse As New Button()
            btnBrowse.Text = "..."
            btnBrowse.Location = New Drawing.Point(420, yPos)
            btnBrowse.Size = New Drawing.Size(35, 22)
            btnBrowse.BackColor = AppTheme.BrowseButton
            btnBrowse.ForeColor = AppTheme.ForeColor
            Dim txtRef = txt
            Dim svcKey = svc.Key
            AddHandler btnBrowse.Click, Sub()
                Using fbd As New FolderBrowserDialog()
                    fbd.Description = $"Selecionar pasta do {svcKey}"
                    If fbd.ShowDialog() = DialogResult.OK Then
                        txtRef.Text = fbd.SelectedPath
                    End If
                End Using
            End Sub
            Me.Controls.Add(btnBrowse)

            Dim lblStatus As New Label()
            If Directory.Exists(svc.Path) Then
                lblStatus.Text = "OK"
                lblStatus.ForeColor = AppTheme.SuccessColor
            Else
                lblStatus.Text = "Nao encontrado"
                lblStatus.ForeColor = AppTheme.ErrorColor
            End If
            lblStatus.Location = New Drawing.Point(460, yPos + 3)
            lblStatus.AutoSize = True
            Me.Controls.Add(lblStatus)
        End Sub

        Private Sub HandleSave(lstPersonalizadas As ListBox)
            Try
                For Each ctrl In Me.Controls.Find("txtSteam", True)
                    If TypeOf ctrl Is TextBox Then
                        _services(0).Path = CType(ctrl, TextBox).Text
                    End If
                Next
                For Each ctrl In Me.Controls.Find("txtEpic", True)
                    If TypeOf ctrl Is TextBox Then
                        _services(1).Path = CType(ctrl, TextBox).Text
                    End If
                Next
                For Each ctrl In Me.Controls.Find("txtGOG", True)
                    If TypeOf ctrl Is TextBox Then
                        _services(2).Path = CType(ctrl, TextBox).Text
                    End If
                Next
                For Each ctrl In Me.Controls.Find("txtOrigin", True)
                    If TypeOf ctrl Is TextBox Then
                        _services(3).Path = CType(ctrl, TextBox).Text
                    End If
                Next

                Dim customs = lstPersonalizadas.Items.Cast(Of String).ToList()
                AppConfigService.UpdateFolders(_services, customs)
                AppLogger.Info("Configuracao de pastas salva")
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
    End Class

End Namespace
