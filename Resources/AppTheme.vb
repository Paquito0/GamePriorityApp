Namespace Resources

    Public Module AppTheme
        Public ReadOnly BackColor As Drawing.Color = Drawing.Color.FromArgb(25, 25, 28)
        Public ReadOnly PanelBackColor As Drawing.Color = Drawing.Color.FromArgb(30, 30, 35)
        Public ReadOnly ControlBackColor As Drawing.Color = Drawing.Color.FromArgb(35, 35, 40)
        Public ReadOnly InputBackColor As Drawing.Color = Drawing.Color.FromArgb(50, 50, 55)
        Public ReadOnly TextBackColor As Drawing.Color = Drawing.Color.FromArgb(40, 40, 45)

        Public ReadOnly ForeColor As Drawing.Color = Drawing.Color.White
        Public ReadOnly MutedColor As Drawing.Color = Drawing.Color.FromArgb(120, 120, 120)
        Public ReadOnly HintColor As Drawing.Color = Drawing.Color.FromArgb(100, 100, 100)
        Public ReadOnly AccentColor As Drawing.Color = Drawing.Color.FromArgb(0, 200, 255)
        Public ReadOnly WarnColor As Drawing.Color = Drawing.Color.FromArgb(255, 180, 0)
        Public ReadOnly SuccessColor As Drawing.Color = Drawing.Color.FromArgb(100, 220, 100)
        Public ReadOnly ErrorColor As Drawing.Color = Drawing.Color.FromArgb(220, 100, 100)
        Public ReadOnly GrayColor As Drawing.Color = Drawing.Color.Gray

        Public ReadOnly PrimaryButton As Drawing.Color = Drawing.Color.FromArgb(0, 140, 220)
        Public ReadOnly SuccessButton As Drawing.Color = Drawing.Color.FromArgb(0, 170, 90)
        Public ReadOnly DangerButton As Drawing.Color = Drawing.Color.FromArgb(200, 60, 60)
        Public ReadOnly NeutralButton As Drawing.Color = Drawing.Color.FromArgb(80, 80, 90)
        Public ReadOnly DetectButton As Drawing.Color = Drawing.Color.FromArgb(0, 180, 100)
        Public ReadOnly ConfigButton As Drawing.Color = Drawing.Color.FromArgb(100, 100, 130)
        Public ReadOnly BrowseButton As Drawing.Color = Drawing.Color.FromArgb(60, 60, 70)
        Public ReadOnly AddFolderButton As Drawing.Color = Drawing.Color.FromArgb(0, 140, 80)
        Public ReadOnly RemoveFolderButton As Drawing.Color = Drawing.Color.FromArgb(180, 50, 50)
        Public ReadOnly SaveButton As Drawing.Color = Drawing.Color.FromArgb(0, 140, 200)
        Public ReadOnly CancelButton As Drawing.Color = Drawing.Color.FromArgb(100, 100, 100)

        Public ReadOnly TitleFont As New Drawing.Font("Segoe UI", 16, Drawing.FontStyle.Bold)
        Public ReadOnly SubtitleFont As New Drawing.Font("Segoe UI", 9)
        Public ReadOnly GroupFont As New Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Bold)
        Public ReadOnly LabelFont As New Drawing.Font("Segoe UI", 9)
        Public ReadOnly ButtonFont As New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)
        Public ReadOnly DialogTitleFont As New Drawing.Font("Segoe UI", 11, Drawing.FontStyle.Bold)
        Public ReadOnly ListFont As New Drawing.Font("Consolas", 9)
        Public ReadOnly SmallListFont As New Drawing.Font("Consolas", 8)
        Public ReadOnly HintFont As New Drawing.Font("Segoe UI", 8)
    End Module

End Namespace
