Namespace Models

    Public Module PriorityDescriptions
        Public Function CpuDescription(idx As Integer) As String
            Select Case idx
                Case 0 : Return "Idle (1): O jogo so roda quando o PC esta livre. Pode causar lag."
                Case 1 : Return "Normal (2): Padrao do Windows. Equilibrio estavel."
                Case 2 : Return "High (3): Jogo ganha mais CPU. Aumenta FPS, apps de fundo ficam lentos."
                Case 3 : Return "Below Normal (5): Menos prioridade. Util para multitarefa."
                Case 4 : Return "Above Normal (6): Um pouco acima do normal. Bom equilibrio."
                Case Else : Return "Selecione uma opcao."
            End Select
        End Function

        Public Function IoDescription(idx As Integer) As String
            Select Case idx
                Case 0 : Return "Very Low (0): Acesso minimo ao disco. Pode causar lentidao ao carregar mapas."
                Case 1 : Return "Low (1): Acesso reduzido ao disco."
                Case 2 : Return "Normal (2): Padrao do Windows. Melhor para jogos."
                Case Else : Return "Selecione uma opcao."
            End Select
        End Function

        Public Function PageDescription(idx As Integer) As String
            Select Case idx
                Case 0 : Return "Very Low (1): Memoria liberada rapido. Pode causar stutter."
                Case 1 : Return "Low (2): Prioridade baixa de memoria."
                Case 2 : Return "Medium (3): Prioridade media."
                Case 3 : Return "Below Normal (4): Abaixo do normal."
                Case 4 : Return "Normal (5): Mantem dados do jogo na RAM mais tempo. Evita stutter."
                Case Else : Return "Selecione uma opcao."
            End Select
        End Function

        Public Function AuditDescription(idx As Integer) As String
            Select Case idx
                Case 0 : Return "Desativado (0): Nao gera logs. Recomendado para jogos."
                Case 1 : Return "Ativado (1): Gera logs no Event Viewer. Util para debug."
                Case Else : Return "Selecione uma opcao."
            End Select
        End Function
    End Module

End Namespace
