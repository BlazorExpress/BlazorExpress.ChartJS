using System.Text.Json;

namespace BlazorExpress.ChartJS.MCP.Tests;

public class ChartJsMcpToolsTests
{
    [Fact]
    public void GenerateChartExample_Returns_Structured_Error_For_Malformed_Labels()
    {
        var json = ChartJsMcpTools.GenerateChartExample("Bar", labelsJson: "[");

        AssertToolError(json, "labelsJson");
    }

    [Fact]
    public void GenerateChartExample_Returns_Structured_Error_For_Malformed_Datasets()
    {
        var json = ChartJsMcpTools.GenerateChartExample("Bar", datasetsJson: """{"label":"Bad"}""");

        AssertToolError(json, "datasetsJson");
    }

    [Fact]
    public void GenerateChartExample_Returns_Structured_Error_For_Unsupported_Chart()
    {
        var json = ChartJsMcpTools.GenerateChartExample("Nope");

        AssertToolError(json, "chartType");
    }

    [Fact]
    public void ApplyProjectIntegration_Returns_Structured_Error_For_Bad_Plan_Json()
    {
        var json = ChartJsMcpTools.ApplyProjectIntegration("[");

        AssertToolError(json);
    }

    [Fact]
    public void PreviewProjectIntegration_Returns_Structured_Error_For_Invalid_Target_Project()
    {
        var json = ChartJsMcpTools.PreviewProjectIntegration(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N")), "Bar");

        AssertToolError(json, "targetProjectPath");
    }

    [Fact]
    public void PreviewProjectIntegration_Returns_Structured_Error_For_Ambiguous_Folder()
    {
        using var workspace = new TemporaryWorkspace();
        var firstProject = workspace.CreateWebAssemblyProject(Path.Combine("src", "FirstApp"), "FirstApp");
        var secondProject = workspace.CreateWebAssemblyProject(Path.Combine("src", "SecondApp"), "SecondApp");

        var json = ChartJsMcpTools.PreviewProjectIntegration(workspace.Root, "Bar");

        AssertToolError(json, "targetProjectPath");
        Assert.Contains(EscapeJsonPath(Path.Combine(firstProject, "FirstApp.csproj")), json);
        Assert.Contains(EscapeJsonPath(Path.Combine(secondProject, "SecondApp.csproj")), json);
    }

    [Fact]
    public void PreviewProjectIntegration_Resolves_Nested_Single_Blazor_Project()
    {
        using var workspace = new TemporaryWorkspace();
        var project = workspace.CreateWebAssemblyProject(Path.Combine("src", "App"), "App");

        var json = ChartJsMcpTools.PreviewProjectIntegration(workspace.Root, "Bar");

        Assert.DoesNotContain("\"success\": false", json);
        Assert.Contains(EscapeJsonPath(Path.Combine(project, "App.csproj")), json);
    }

    [Fact]
    public void PreviewDashboardIntegration_Uses_Same_Project_Resolver()
    {
        using var workspace = new TemporaryWorkspace();
        var project = workspace.CreateWebAssemblyProject(Path.Combine("src", "App"), "App");

        var json = ChartJsMcpTools.PreviewDashboardIntegration(workspace.Root, title: "Charts");

        Assert.DoesNotContain("\"success\": false", json);
        Assert.Contains("ChartDashboardPage.razor", json);
        Assert.Contains(EscapeJsonPath(Path.Combine(project, "App.csproj")), json);
    }

    [Fact]
    public void GenerateChartExample_Accepts_Color_String()
    {
        var json = ChartJsMcpTools.GenerateChartExample(
            "Bar",
            datasetsJson: """[{"label":"Revenue","data":[1,2],"backgroundColor":"#123456","borderColor":"#654321"}]""");

        Assert.DoesNotContain("\"success\": false", json);
        Assert.Contains("#123456", json);
        Assert.Contains("#654321", json);
    }

    [Fact]
    public void GenerateChartExample_Accepts_Color_Array()
    {
        var json = ChartJsMcpTools.GenerateChartExample(
            "Line",
            datasetsJson: """[{"label":"Revenue","data":[1,2],"backgroundColor":["#123456"],"borderColor":["#654321"]}]""");

        Assert.DoesNotContain("\"success\": false", json);
        Assert.Contains("#123456", json);
        Assert.Contains("#654321", json);
    }

    [Fact]
    public void GenerateChartDashboard_Returns_Structured_Error_For_Invalid_Child_Spec()
    {
        var json = ChartJsMcpTools.GenerateChartDashboard(chartsJson: """[{"title":"Missing type"}]""");

        AssertToolError(json, "chartsJson[0]");
    }

    [Fact]
    public void GenerateChartDashboard_Accepts_Custom_Chart_Specs()
    {
        var json = ChartJsMcpTools.GenerateChartDashboard(chartsJson: """
            [
              {
                "chartType": "Scatter",
                "datasets": [
                  {
                    "label": "Samples",
                    "points": [{ "x": 1, "y": 2 }],
                    "backgroundColor": "#123456"
                  }
                ]
              }
            ]
            """);

        Assert.DoesNotContain("\"success\": false", json);
        Assert.Contains("ScatterChart", json);
        Assert.Contains("new(1, 2)", json);
    }

    private static void AssertToolError(string json, string? expectedField = null)
    {
        using var document = JsonDocument.Parse(json);
        Assert.False(document.RootElement.GetProperty("success").GetBoolean());
        Assert.Equal("invalid_input", document.RootElement.GetProperty("error").GetProperty("code").GetString());

        if (expectedField is not null)
            Assert.Equal(expectedField, document.RootElement.GetProperty("error").GetProperty("field").GetString());
    }

    private static string EscapeJsonPath(string path) =>
        path.Replace("\\", "\\\\", StringComparison.Ordinal);

    private sealed class TemporaryWorkspace : IDisposable
    {
        private readonly string root = Path.Combine(Path.GetTempPath(), $"bex-chartjs-mcp-tool-tests-{Guid.NewGuid():N}");

        public string Root => root;

        public string CreateWebAssemblyProject(string relativePath, string projectName)
        {
            var projectRoot = Path.Combine(root, relativePath);
            Directory.CreateDirectory(projectRoot);
            Directory.CreateDirectory(Path.Combine(projectRoot, "wwwroot"));
            Directory.CreateDirectory(Path.Combine(projectRoot, "Pages"));
            Directory.CreateDirectory(Path.Combine(projectRoot, "Shared"));

            File.WriteAllText(Path.Combine(projectRoot, $"{projectName}.csproj"), """
                <Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">
                  <PropertyGroup>
                    <TargetFramework>net10.0</TargetFramework>
                  </PropertyGroup>
                </Project>
                """);
            File.WriteAllText(Path.Combine(projectRoot, "_Imports.razor"), "@using Microsoft.AspNetCore.Components" + Environment.NewLine);
            File.WriteAllText(Path.Combine(projectRoot, "wwwroot", "index.html"), """
                <html>
                <body>
                    <div id="app"></div>
                </body>
                </html>
                """);
            File.WriteAllText(Path.Combine(projectRoot, "Shared", "NavMenu.razor"), """
                <nav>
                    <NavLink class="nav-link" href="">Home</NavLink>
                </nav>
                """);

            return projectRoot;
        }

        public void Dispose()
        {
            if (Directory.Exists(root))
                Directory.Delete(root, recursive: true);
        }
    }
}
