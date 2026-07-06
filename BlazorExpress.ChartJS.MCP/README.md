# BlazorExpress.ChartJS.MCP

Model Context Protocol server for generating and integrating BlazorExpress.ChartJS charts.

Requires .NET 10 SDK/runtime.

## Install

```powershell
dotnet tool install --global BlazorExpress.ChartJS.MCP
```

## MCP command

```powershell
blazorexpress-chartjs-mcp
```

The default command starts a Streamable HTTP MCP server at `http://localhost:5000/mcp`.

The server exposes tools for listing supported chart types, generating complete Razor examples, previewing project integration edits, and applying approved integration plans.

HTTP mode does not require authentication when `ASPNETCORE_ENVIRONMENT` is `Development`. In other environments, set `CHARTJS_MCP_TOKEN` and send it as a bearer token.

Use explicit stdio mode for editor integrations that launch the MCP server as a child process:

```powershell
blazorexpress-chartjs-mcp --stdio
```

You can also select the transport with:

```powershell
blazorexpress-chartjs-mcp --transport http
blazorexpress-chartjs-mcp --transport stdio
```

## How to test in local

From the repository root, run the solution tests:

```powershell
dotnet test .\BlazorExpress.ChartJS.sln
```

Create the local .NET tool package:

```powershell
dotnet pack .\BlazorExpress.ChartJS.MCP\BlazorExpress.ChartJS.MCP.csproj -c Release
```

Install the generated package locally:

```powershell
dotnet tool install --global BlazorExpress.ChartJS.MCP --add-source .\BlazorExpress.ChartJS.MCP\bin\Release
```

If the tool is already installed, update it instead:

```powershell
dotnet tool update --global BlazorExpress.ChartJS.MCP --add-source .\BlazorExpress.ChartJS.MCP\bin\Release
```

Run the MCP server:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
blazorexpress-chartjs-mcp
```

The command starts a Streamable HTTP MCP server on `http://localhost:5000/mcp`.

Check health:

```powershell
Invoke-RestMethod http://localhost:5000/health
```

Run the stdio MCP server for local editor integration:

```powershell
blazorexpress-chartjs-mcp --stdio
```

## How to run as a Kestrel service

For local development, no bearer token is required:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
blazorexpress-chartjs-mcp
```

For production, configure the URL and bearer token:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Production"
$env:ASPNETCORE_URLS = "http://localhost:5000"
$env:CHARTJS_MCP_TOKEN = "<your-strong-token>"
blazorexpress-chartjs-mcp
```

Production MCP clients must send:

```text
Authorization: Bearer <your-strong-token>
```

The Streamable HTTP endpoint is:

```text
http://localhost:5000/mcp
```

The health endpoint is:

```text
http://localhost:5000/health
```

## How to integrate with VS Code

Install or update the tool locally first:

```powershell
dotnet pack .\BlazorExpress.ChartJS.MCP\BlazorExpress.ChartJS.MCP.csproj -c Release
dotnet tool install --global BlazorExpress.ChartJS.MCP --add-source .\BlazorExpress.ChartJS.MCP\bin\Release
```

If the tool is already installed:

```powershell
dotnet tool update --global BlazorExpress.ChartJS.MCP --add-source .\BlazorExpress.ChartJS.MCP\bin\Release
```

Create or update `.vscode/mcp.json` in your workspace:

```json
{
  "servers": {
    "blazorexpress-chartjs": {
      "type": "stdio",
      "command": "blazorexpress-chartjs-mcp",
      "args": ["--stdio"]
    }
  }
}
```

For an HTTP MCP client, use this URL:

```text
http://localhost:5000/mcp
```

In VS Code:

1. Open Command Palette.
2. Run `MCP: List Servers`.
3. Start `blazorexpress-chartjs` if it is not already running.
4. Open Copilot Chat in Agent mode.
5. Use the tools exposed by the server, such as `list_chart_types` or `generate_chart_example`.

You can also add the server through Command Palette using `MCP: Add Server`, choose a command/stdio server, and use `blazorexpress-chartjs-mcp` as the command.

## How to integrate with Visual Studio

Prerequisite: Visual Studio 2022 version 17.14 or later, or Visual Studio 2026, with GitHub Copilot Agent mode enabled.

Option 1: use Visual Studio chat.

1. Open the Copilot chat pane.
2. Switch to Agent mode.
3. Select Tools.
4. Select the plus (`+`) button.
5. Select `Add custom MCP server`.
6. Enter:
   - Name: `blazorexpress-chartjs`
   - Transport: `stdio`
   - Command: `blazorexpress-chartjs-mcp`
   - Arguments: `--stdio`
7. Save the server.
8. Enable the MCP tools from the Tools picker.

Option 2: use a config file.

Create one of these files:

- `<SOLUTIONDIR>\.mcp.json` for a solution-level config that can be checked in.
- `%USERPROFILE%\.mcp.json` for a user-level config.
- `<SOLUTIONDIR>\.vscode\mcp.json` if you want VS Code and Visual Studio to share the same workspace config.

Use this configuration:

```json
{
  "servers": {
    "blazorexpress-chartjs": {
      "type": "stdio",
      "command": "blazorexpress-chartjs-mcp",
      "args": ["--stdio"]
    }
  }
}
```

After saving the file, open Copilot Chat in Agent mode, select Tools, and enable the BlazorExpress.ChartJS MCP tools. Visual Studio may ask for permission before running a tool.

References:

- VS Code MCP servers: https://code.visualstudio.com/docs/agent-customization/mcp-servers
- Visual Studio MCP servers: https://learn.microsoft.com/en-us/visualstudio/ide/mcp-servers
