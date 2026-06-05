# GamePriorityApp

## Compilar e Executar

```powershell
dotnet run
```

## Fatos Importantes

- **Linguagem**: VB.NET (.NET 10 Windows Forms)
- **Target framework**: `net10.0-windows` (requer .NET 10 SDK)
- **Ponto de entrada**: `Program.vb` → cria `Form1`
- **Requer**: Privilégios de administrador (veja `app.manifest` - `requestedExecutionLevel level="requireAdministrator"`)
- **O que faz**: Gerencia prioridades de processos Windows via Registro em `HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\<exe>\PerfOptions`
- **Sem testes ou CI** neste repositório
- **Saída**: Executável Windows (WinExe), não uma biblioteca

## Estrutura de Pastas

```
GamePriorityApp/
├── Program.vb              # Entry point
├── Form1.vb                # UI principal (Forms + bindings)
├── Models/                 # Tipos de dominio
│   ├── PriorityOptions.vb    # Enums Cpu/Io/Page/Audit + PrioritySettings
│   ├── DetectedGame.vb       # Resultado de scan
│   └── PriorityDescriptions.vb  # Tooltips
├── Services/               # Logica de IO / Registry / Config
│   ├── AppLogger.vb          # Log em %LocalAppData%
│   ├── AppConfigService.vb   # config.json (Steam/Epic/GOG/Origin paths)
│   ├── GameRegistryService.vb# Operacoes de Registry (IFEO)
│   └── GameScannerService.vb # Async scan de jogos instalados
├── Forms/                  # Formularios secundarios
│   └── ConfigForm.vb         # Dialog de configuracao de pastas
├── Resources/              # Constantes de UI
│   └── AppTheme.vb           # Cores e fontes
├── app.manifest            # Requer admin
├── GamePriorityApp.vbproj
└── README.md
```

## Onde fica o estado

| Item | Local |
|------|-------|
| Config (paths) | `%LocalAppData%\GamePriorityApp\config.json` |
| Log | `%LocalAppData%\GamePriorityApp\app.log` |
| PerfOptions | `HKLM\...\Image File Execution Options\<exe>\PerfOptions` |

## Build sem SDK .NET 10

Se so tiver o .NET 8 SDK instalado, edite `GamePriorityApp.vbproj` e troque `net10.0-windows` por `net8.0-windows`. O codigo compila identico em ambos.
