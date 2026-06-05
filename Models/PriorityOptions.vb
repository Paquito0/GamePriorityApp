Namespace Models

    Public Enum CpuPriority As Integer
        Idle = 1
        Normal = 2
        High = 3
        BelowNormal = 5
        AboveNormal = 6
    End Enum

    Public Enum IoPriority As Integer
        VeryLow = 0
        Low = 1
        Normal = 2
    End Enum

    Public Enum PagePriority As Integer
        VeryLow = 1
        Low = 2
        Medium = 3
        BelowNormal = 4
        [Normal] = 5
    End Enum

    Public Enum AuditLevel As Integer
        Disabled = 0
        Enabled = 1
    End Enum

    Public Class PrioritySettings
        Public Property Cpu As CpuPriority = CpuPriority.High
        Public Property Io As IoPriority = IoPriority.Normal
        Public Property Page As PagePriority = PagePriority.Normal
        Public Property Audit As AuditLevel = AuditLevel.Disabled

        Public Sub New()
        End Sub

        Public Sub New(cpu As CpuPriority, io As IoPriority, page As PagePriority, audit As AuditLevel)
            Me.Cpu = cpu
            Me.Io = io
            Me.Page = page
            Me.Audit = audit
        End Sub

        Public Function CpuIndex() As Integer
            Select Case Cpu
                Case CpuPriority.Idle : Return 0
                Case CpuPriority.Normal : Return 1
                Case CpuPriority.High : Return 2
                Case CpuPriority.BelowNormal : Return 3
                Case CpuPriority.AboveNormal : Return 4
                Case Else : Return 1
            End Select
        End Function

        Public Shared Function CpuFromIndex(idx As Integer) As CpuPriority
            Select Case idx
                Case 0 : Return CpuPriority.Idle
                Case 1 : Return CpuPriority.Normal
                Case 2 : Return CpuPriority.High
                Case 3 : Return CpuPriority.BelowNormal
                Case 4 : Return CpuPriority.AboveNormal
                Case Else : Return CpuPriority.Normal
            End Select
        End Function

        Public Function IoIndex() As Integer
            Select Case Io
                Case IoPriority.VeryLow : Return 0
                Case IoPriority.Low : Return 1
                Case IoPriority.Normal : Return 2
                Case Else : Return 2
            End Select
        End Function

        Public Shared Function IoFromIndex(idx As Integer) As IoPriority
            Select Case idx
                Case 0 : Return IoPriority.VeryLow
                Case 1 : Return IoPriority.Low
                Case 2 : Return IoPriority.Normal
                Case Else : Return IoPriority.Normal
            End Select
        End Function

        Public Function PageIndex() As Integer
            Select Case Page
                Case PagePriority.VeryLow : Return 0
                Case PagePriority.Low : Return 1
                Case PagePriority.Medium : Return 2
                Case PagePriority.BelowNormal : Return 3
                Case PagePriority.Normal : Return 4
                Case Else : Return 4
            End Select
        End Function

        Public Shared Function PageFromIndex(idx As Integer) As PagePriority
            Select Case idx
                Case 0 : Return PagePriority.VeryLow
                Case 1 : Return PagePriority.Low
                Case 2 : Return PagePriority.Medium
                Case 3 : Return PagePriority.BelowNormal
                Case 4 : Return PagePriority.Normal
                Case Else : Return PagePriority.Normal
            End Select
        End Function

        Public Function AuditIndex() As Integer
            Select Case Audit
                Case AuditLevel.Disabled : Return 0
                Case AuditLevel.Enabled : Return 1
                Case Else : Return 0
            End Select
        End Function

        Public Shared Function AuditFromIndex(idx As Integer) As AuditLevel
            Select Case idx
                Case 0 : Return AuditLevel.Disabled
                Case 1 : Return AuditLevel.Enabled
                Case Else : Return AuditLevel.Disabled
            End Select
        End Function
    End Class

End Namespace
