namespace BlazorExpress.ChartJS.MCP.Tests;

public class ProjectIntegrationServiceTests
{
    [Fact]
    public void Preview_For_WebAssembly_Project_Creates_Plan_Without_Writing_Files()
    {
        using var workspace = new TemporaryWorkspace();
        var projectDirectory = workspace.CreateWebAssemblyProject();
        var service = new ProjectIntegrationService(new ChartExampleGenerator());

        var plan = service.Preview(new PreviewIntegrationRequest
        {
            TargetProjectPath = projectDirectory,
            Chart = new ChartGenerationRequest { ChartType = "Line", Title = "Sales Trend", Route = "/charts/sales-trend" },
        });

        Assert.Equal("BlazorWebAssembly", plan.DetectedHostModel);
        Assert.NotEmpty(plan.PlanHash);
        Assert.Contains(plan.Edits, x => x.Path.EndsWith("LineChartPage.razor", StringComparison.Ordinal));
        Assert.Contains(plan.Edits, x => x.Path.EndsWith("Sample.csproj", StringComparison.Ordinal) && x.NewContent.Contains("BlazorExpress.ChartJS", StringComparison.Ordinal));
        Assert.Contains(plan.Edits, x => x.Path.EndsWith("_Imports.razor", StringComparison.Ordinal) && x.NewContent.Contains("@using BlazorExpress.ChartJS", StringComparison.Ordinal));
        Assert.Contains(plan.Edits, x => x.Path.EndsWith("index.html", StringComparison.Ordinal) && x.NewContent.Contains("chart.umd.js", StringComparison.Ordinal));
        Assert.Contains(plan.Edits, x => x.Path.EndsWith("NavMenu.razor", StringComparison.Ordinal) && x.NewContent.Contains("charts/sales-trend", StringComparison.Ordinal));
        Assert.False(File.Exists(Path.Combine(projectDirectory, "Pages", "LineChartPage.razor")));
    }

    [Fact]
    public void Apply_Writes_Files_For_Matching_Preview()
    {
        using var workspace = new TemporaryWorkspace();
        var projectDirectory = workspace.CreateWebAssemblyProject();
        var service = new ProjectIntegrationService(new ChartExampleGenerator());
        var plan = service.Preview(new PreviewIntegrationRequest
        {
            TargetProjectPath = projectDirectory,
            Chart = new ChartGenerationRequest { ChartType = "Pie", Title = "Sales Mix" },
        });

        var result = service.Apply(plan);

        Assert.NotEmpty(result.WrittenFiles);
        Assert.True(File.Exists(Path.Combine(projectDirectory, "Pages", "PieChartPage.razor")));
    }

    [Fact]
    public void Apply_Rejects_Stale_Preview()
    {
        using var workspace = new TemporaryWorkspace();
        var projectDirectory = workspace.CreateWebAssemblyProject();
        var service = new ProjectIntegrationService(new ChartExampleGenerator());
        var plan = service.Preview(new PreviewIntegrationRequest
        {
            TargetProjectPath = projectDirectory,
            Chart = new ChartGenerationRequest { ChartType = "Bar" },
        });

        File.AppendAllText(Path.Combine(projectDirectory, "_Imports.razor"), "@using Changed");

        Assert.Throws<InvalidOperationException>(() => service.Apply(plan));
    }

    [Fact]
    public void PreviewDashboard_For_WebAssembly_Project_Creates_Plan_Without_Writing_Files()
    {
        using var workspace = new TemporaryWorkspace();
        var projectDirectory = workspace.CreateWebAssemblyProject();
        var service = new ProjectIntegrationService(new ChartExampleGenerator());

        var plan = service.PreviewDashboard(new PreviewDashboardIntegrationRequest
        {
            TargetProjectPath = projectDirectory,
            Dashboard = new ChartDashboardRequest { Title = "Charts", Route = "/charts/dashboard" },
        });

        Assert.Equal("BlazorWebAssembly", plan.DetectedHostModel);
        Assert.NotEmpty(plan.PlanHash);
        Assert.Contains(plan.Edits, x => x.Path.EndsWith("ChartDashboardPage.razor", StringComparison.Ordinal));
        Assert.Contains(plan.Edits, x => x.Path.EndsWith("NavMenu.razor", StringComparison.Ordinal) && x.NewContent.Contains("charts/dashboard", StringComparison.Ordinal));
        Assert.False(File.Exists(Path.Combine(projectDirectory, "Pages", "ChartDashboardPage.razor")));
    }

    [Fact]
    public void ApplyDashboard_Writes_Files_For_Matching_Preview()
    {
        using var workspace = new TemporaryWorkspace();
        var projectDirectory = workspace.CreateWebAssemblyProject();
        var service = new ProjectIntegrationService(new ChartExampleGenerator());
        var plan = service.PreviewDashboard(new PreviewDashboardIntegrationRequest
        {
            TargetProjectPath = projectDirectory,
            Dashboard = new ChartDashboardRequest { Title = "Charts" },
        });

        var result = service.Apply(plan);

        Assert.NotEmpty(result.WrittenFiles);
        Assert.True(File.Exists(Path.Combine(projectDirectory, "Pages", "ChartDashboardPage.razor")));
    }

    private sealed class TemporaryWorkspace : IDisposable
    {
        private readonly string root = Path.Combine(Path.GetTempPath(), $"bex-chartjs-mcp-tests-{Guid.NewGuid():N}");

        public string CreateWebAssemblyProject()
        {
            Directory.CreateDirectory(root);
            Directory.CreateDirectory(Path.Combine(root, "wwwroot"));
            Directory.CreateDirectory(Path.Combine(root, "Pages"));
            Directory.CreateDirectory(Path.Combine(root, "Shared"));

            File.WriteAllText(Path.Combine(root, "Sample.csproj"), """
                <Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">
                  <PropertyGroup>
                    <TargetFramework>net10.0</TargetFramework>
                  </PropertyGroup>
                </Project>
                """);
            File.WriteAllText(Path.Combine(root, "_Imports.razor"), "@using Microsoft.AspNetCore.Components" + Environment.NewLine);
            File.WriteAllText(Path.Combine(root, "wwwroot", "index.html"), """
                <html>
                <body>
                    <div id="app"></div>
                </body>
                </html>
                """);
            File.WriteAllText(Path.Combine(root, "Shared", "NavMenu.razor"), """
                <nav>
                    <NavLink class="nav-link" href="">Home</NavLink>
                </nav>
                """);

            return root;
        }

        public void Dispose()
        {
            if (Directory.Exists(root))
                Directory.Delete(root, recursive: true);
        }
    }
}
