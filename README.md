# Game Priority Manager

Aplicativo desktop Windows para gerenciar prioridades de processos de jogos.

## Requisitos

- Windows 10/11
- .NET 10 Runtime (ou superior)
- Privilégios de administrador

## Instalação

### Download (Recomendado)

Baixe o executável em: https://github.com/Paquito0/GamePriorityApp/releases

Escolha entre:
- **GamePriorityApp.exe** - Execute diretamente (requer .NET 10 Runtime)
- **GamePriorityApp-vX.X.X.zip** - Self-contained (não precisa de runtime)

### Build manual

```powershell
git clone https://github.com/Paquito0/GamePriorityApp.git
cd GamePriorityApp
dotnet build
dotnet run
```

**Nota**: Execute como administrador (o aplicativo requer privilégios elevados para modificar o Registro do Windows).

## Uso

1. Execute o aplicativo como administrador
2. Clique em **Adicionar Jogo** para selecionar um executável (.exe)
3. Configure as prioridades:
   - **CpuPriorityClass**: Prioridade de CPU (Idle, Normal, High, Below Normal, Above Normal)
   - **IoPriority**: Prioridade de I/O (Very Low, Low, Normal)
   - **PagePriority**: Prioridade de memória (Very Low até Normal)
   - **AuditLevel**: Logging de eventos (0 = desativado, 1 = ativado)
4. Clique em **Salvar Alterações** para aplicar
5. Quando o jogo iniciar, o Windows aplicará automaticamente as prioridades configuradas

## Como funciona

O aplicativo modifica o Registro do Windows em:
```
HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\<jogo>.exe\PerfOptions
```

Isso permite que o Windows aplique prioridades personalizadas automaticamente quando um jogo é iniciado.

## Estrutura do Projeto

```
GamePriorityApp/
├── Program.vb                       # Entry point
├── Form1.vb                         # UI principal
├── Models/
│   ├── PriorityOptions.vb           # Enums + PrioritySettings
│   ├── DetectedGame.vb              # Resultado de scan
│   └── PriorityDescriptions.vb     # Tooltips
├── Services/
│   ├── AppLogger.vb                 # Logger em arquivo
│   ├── AppConfigService.vb          # config.json
│   ├── GameRegistryService.vb       # Operacoes de Registry
│   └── GameScannerService.vb        # Async scan de jogos
├── Forms/
│   └── ConfigForm.vb                # Dialog de configuracao
├── Resources/
│   └── AppTheme.vb                  # Cores e fontes
├── app.manifest
└── GamePriorityApp.vbproj
```

## Localizacao dos arquivos

- **Config**: `%LocalAppData%\GamePriorityApp\config.json`
- **Log**: `%LocalAppData%\GamePriorityApp\app.log`

## Tecnologia

- VB.NET (.NET 10)
- Windows Forms
- Registry API (Microsoft.Win32)

## Licença

MIT
