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
    public void Preview_Accepts_Exact_Project_File_Path()
    {
        using var workspace = new TemporaryWorkspace();
        var projectDirectory = workspace.CreateWebAssemblyProject();
        var projectFilePath = Path.Combine(projectDirectory, "Sample.csproj");
        var service = new ProjectIntegrationService(new ChartExampleGenerator());

        var plan = service.Preview(new PreviewIntegrationRequest
        {
            TargetProjectPath = projectFilePath,
            Chart = new ChartGenerationRequest { ChartType = "Line" },
        });

        Assert.Equal(projectFilePath, plan.ProjectFilePath);
    }

    [Fact]
    public void Preview_Resolves_Repo_Folder_With_One_Nested_Blazor_App()
    {
        using var workspace = new TemporaryWorkspace();
        var projectDirectory = workspace.CreateWebAssemblyProject(Path.Combine("src", "App"), "App");
        var service = new ProjectIntegrationService(new ChartExampleGenerator());

        var plan = service.Preview(new PreviewIntegrationRequest
        {
            TargetProjectPath = workspace.Root,
            Chart = new ChartGenerationRequest { ChartType = "Line" },
        });

        Assert.Equal(Path.Combine(projectDirectory, "App.csproj"), plan.ProjectFilePath);
    }

    [Fact]
    public void Preview_Rejects_Repo_Folder_With_Multiple_Blazor_App_Candidates()
    {
        using var workspace = new TemporaryWorkspace();
        var firstProjectDirectory = workspace.CreateWebAssemblyProject(Path.Combine("src", "FirstApp"), "FirstApp");
        var secondProjectDirectory = workspace.CreateWebAssemblyProject(Path.Combine("src", "SecondApp"), "SecondApp");
        var service = new ProjectIntegrationService(new ChartExampleGenerator());

        var exception = Assert.Throws<ToolInputException>(() => service.Preview(new PreviewIntegrationRequest
        {
            TargetProjectPath = workspace.Root,
            Chart = new ChartGenerationRequest { ChartType = "Line" },
        }));

        Assert.Equal("targetProjectPath", exception.Field);
        Assert.Contains(Path.Combine(firstProjectDirectory, "FirstApp.csproj"), exception.Details);
        Assert.Contains(Path.Combine(secondProjectDirectory, "SecondApp.csproj"), exception.Details);
    }

    [Fact]
    public void Preview_Rejects_Folder_With_Only_NonBlazor_Projects()
    {
        using var workspace = new TemporaryWorkspace();
        var projectDirectory = workspace.CreateLibraryProject(Path.Combine("src", "Library"), "Library");
        var service = new ProjectIntegrationService(new ChartExampleGenerator());

        var exception = Assert.Throws<ToolInputException>(() => service.Preview(new PreviewIntegrationRequest
        {
            TargetProjectPath = workspace.Root,
            Chart = new ChartGenerationRequest { ChartType = "Line" },
        }));

        Assert.Equal("targetProjectPath", exception.Field);
        Assert.Contains(Path.Combine(projectDirectory, "Library.csproj"), exception.Details);
    }

    [Fact]
    public void Preview_Resolves_Solution_With_One_Blazor_App()
    {
        using var workspace = new TemporaryWorkspace();
        var projectDirectory = workspace.CreateWebAssemblyProject(Path.Combine("src", "App"), "App");
        var libraryDirectory = workspace.CreateLibraryProject(Path.Combine("src", "Library"), "Library");
        var solutionPath = workspace.CreateSolution("Sample.sln", Path.Combine(projectDirectory, "App.csproj"), Path.Combine(libraryDirectory, "Library.csproj"));
        var service = new ProjectIntegrationService(new ChartExampleGenerator());

        var plan = service.Preview(new PreviewIntegrationRequest
        {
            TargetProjectPath = solutionPath,
            Chart = new ChartGenerationRequest { ChartType = "Line" },
        });

        Assert.Equal(Path.Combine(projectDirectory, "App.csproj"), plan.ProjectFilePath);
    }

    [Fact]
    public void Preview_Rejects_Solution_With_Multiple_Blazor_App_Candidates()
    {
        using var workspace = new TemporaryWorkspace();
        var firstProjectDirectory = workspace.CreateWebAssemblyProject(Path.Combine("src", "FirstApp"), "FirstApp");
        var secondProjectDirectory = workspace.CreateWebAssemblyProject(Path.Combine("src", "SecondApp"), "SecondApp");
        var solutionPath = workspace.CreateSolution("Sample.sln", Path.Combine(firstProjectDirectory, "FirstApp.csproj"), Path.Combine(secondProjectDirectory, "SecondApp.csproj"));
        var service = new ProjectIntegrationService(new ChartExampleGenerator());

        var exception = Assert.Throws<ToolInputException>(() => service.Preview(new PreviewIntegrationRequest
        {
            TargetProjectPath = solutionPath,
            Chart = new ChartGenerationRequest { ChartType = "Line" },
        }));

        Assert.Equal("targetProjectPath", exception.Field);
        Assert.Contains(Path.Combine(firstProjectDirectory, "FirstApp.csproj"), exception.Details);
        Assert.Contains(Path.Combine(secondProjectDirectory, "SecondApp.csproj"), exception.Details);
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

        public string Root => root;

        public string CreateWebAssemblyProject(string relativePath = "", string projectName = "Sample")
        {
            var projectRoot = string.IsNullOrWhiteSpace(relativePath) ? root : Path.Combine(root, relativePath);
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

        public string CreateLibraryProject(string relativePath, string projectName)
        {
            var projectRoot = Path.Combine(root, relativePath);
            Directory.CreateDirectory(projectRoot);
            File.WriteAllText(Path.Combine(projectRoot, $"{projectName}.csproj"), """
                <Project Sdk="Microsoft.NET.Sdk">
                  <PropertyGroup>
                    <TargetFramework>net10.0</TargetFramework>
                  </PropertyGroup>
                </Project>
                """);

            return projectRoot;
        }

        public string CreateSolution(string fileName, params string[] projectPaths)
        {
            Directory.CreateDirectory(root);
            var solutionPath = Path.Combine(root, fileName);
            var lines = new List<string>
            {
                "Microsoft Visual Studio Solution File, Format Version 12.00",
                "# Visual Studio Version 17",
            };

            foreach (var projectPath in projectPaths)
            {
                var relativePath = Path.GetRelativePath(root, projectPath);
                var projectName = Path.GetFileNameWithoutExtension(projectPath);
                lines.Add($"Project(\"{{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}}\") = \"{projectName}\", \"{relativePath}\", \"{{{Guid.NewGuid():D}}}\"");
                lines.Add("EndProject");
            }

            lines.Add("Global");
            lines.Add("EndGlobal");
            File.WriteAllText(solutionPath, string.Join(Environment.NewLine, lines));

            return solutionPath;
        }

        public void Dispose()
        {
            if (Directory.Exists(root))
                Directory.Delete(root, recursive: true);
        }
    }
}
