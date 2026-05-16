# GamePriorityApp

## Compilar e Executar

```powershell
dotnet run
```

## Fatos Importantes

- **Linguagem**: VB.NET (.NET 10 Windows Forms)
- **Ponto de entrada**: `Program.vb` → cria `Form1`
- **Requer**: Privilégios de administrador (veja `app.manifest` - `requestedExecutionLevel level="requireAdministrator"`)
- **O que faz**: Gerencia prioridades de processos Windows via Registro em `HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\<exe>\PerfOptions`
- **Sem testes ou CI** neste repositório
- **Saída**: Executável Windows (WinExe), não uma biblioteca