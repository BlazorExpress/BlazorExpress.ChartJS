namespace BlazorExpress.ChartJS.MCP.Tests;

public class ChartExampleGeneratorTests
{
    [Theory]
    [InlineData("Bar", "BarChart", "BarChartOptions", "BarChartDataset")]
    [InlineData("Bubble", "BubbleChart", "BubbleChartOptions", "BubbleChartDataset")]
    [InlineData("Doughnut", "DoughnutChart", "DoughnutChartOptions", "DoughnutChartDataset")]
    [InlineData("Line", "LineChart", "LineChartOptions", "LineChartDataset")]
    [InlineData("Pie", "PieChart", "PieChartOptions", "PieChartDataset")]
    [InlineData("PolarArea", "PolarAreaChart", "PolarAreaChartOptions", "PolarAreaChartDataset")]
    [InlineData("Radar", "RadarChart", "RadarChartOptions", "RadarChartDataset")]
    [InlineData("Scatter", "ScatterChart", "ScatterChartOptions", "ScatterChartDataset")]
    public void Generate_Includes_Expected_Razor_Types_For_All_Charts(string chartType, string component, string options, string dataset)
    {
        var generator = new ChartExampleGenerator();

        var generated = generator.Generate(new ChartGenerationRequest { ChartType = chartType, Title = $"{chartType} Sample" });

        Assert.Contains($"<{component} @ref=", generated.Code);
        Assert.Contains($"private {options}", generated.Code);
        Assert.Contains($"new {dataset}", generated.Code);
        Assert.Contains("@using BlazorExpress.ChartJS", generated.Code);
    }

    [Fact]
    public void Generate_Bar_Datalabels_Uses_Plugin()
    {
        var generator = new ChartExampleGenerator();

        var generated = generator.Generate(new ChartGenerationRequest { ChartType = "Bar", Datalabels = true });

        Assert.Contains("ChartDataLabels", generated.Code);
        Assert.Contains("chartjs-plugin-datalabels", string.Join(" ", generated.RequiredScripts));
    }

    [Fact]
    public void Generate_Radar_Does_Not_Use_Datalabels()
    {
        var generator = new ChartExampleGenerator();

        var generated = generator.Generate(new ChartGenerationRequest { ChartType = "Radar", Datalabels = true });

        Assert.DoesNotContain("ChartDataLabels", generated.Code);
        Assert.DoesNotContain("chartjs-plugin-datalabels", string.Join(" ", generated.RequiredScripts));
    }

    [Theory]
    [InlineData("Bubble")]
    [InlineData("Radar")]
    public void Generate_Charts_Without_Plugin_Options_Do_Not_Emit_Plugin_Title(string chartType)
    {
        var generator = new ChartExampleGenerator();

        var generated = generator.Generate(new ChartGenerationRequest { ChartType = chartType });

        Assert.DoesNotContain(".Plugins.Title", generated.Code);
        Assert.DoesNotContain(".Plugins.Legend", generated.Code);
    }

    [Fact]
    public void Generate_Scatter_Uses_Point_Data()
    {
        var generator = new ChartExampleGenerator();

        var generated = generator.Generate(new ChartGenerationRequest
        {
            ChartType = "Scatter",
            Datasets =
            [
                new()
                {
                    Label = "Samples",
                    Points =
                    [
                        new ChartPointRequest { X = 1, Y = 2 },
                        new ChartPointRequest { X = 3, Y = 4 },
                    ],
                },
            ],
        });

        Assert.Contains("new List<ScatterChartDataPoint> { new(1, 2), new(3, 4)", generated.Code);
    }

    [Theory]
    [InlineData(null, "new(1, 2, 8)")]
    [InlineData(9.0, "new(1, 2, 9)")]
    public void Generate_Bubble_Uses_Point_Data_With_Defaultable_Radius(double? radius, string expected)
    {
        var generator = new ChartExampleGenerator();

        var generated = generator.Generate(new ChartGenerationRequest
        {
            ChartType = "Bubble",
            Datasets =
            [
                new()
                {
                    Label = "Samples",
                    Points = [new ChartPointRequest { X = 1, Y = 2, R = radius }],
                },
            ],
        });

        Assert.Contains(expected, generated.Code);
    }

    [Fact]
    public void GenerateDashboard_Default_Includes_All_Chart_Components()
    {
        var generator = new ChartExampleGenerator();

        var generated = generator.GenerateDashboard(new ChartDashboardRequest());

        foreach (var definition in ChartCatalog.All)
            Assert.Contains($"<{definition.ComponentName} @ref=", generated.Code);

        Assert.Equal(8, generated.ChartTypes.Count);
    }

    [Fact]
    public void GenerateDashboard_Custom_Uses_Unique_Field_Names_And_Deduplicated_Scripts()
    {
        var generator = new ChartExampleGenerator();

        var generated = generator.GenerateDashboard(new ChartDashboardRequest
        {
            Charts =
            [
                new ChartGenerationRequest { ChartType = "Bar", Datalabels = true },
                new ChartGenerationRequest { ChartType = "Bar", Datalabels = true },
            ],
        });

        Assert.Contains("barChart1", generated.Code);
        Assert.Contains("barChart2", generated.Code);
        Assert.Equal(generated.RequiredScripts.Count, generated.RequiredScripts.Distinct().Count());
    }

    [Fact]
    public void Generated_Single_And_Dashboard_Pages_Compile()
    {
        using var workspace = new RazorCompileWorkspace();
        var generator = new ChartExampleGenerator();
        var single = generator.Generate(new ChartGenerationRequest { ChartType = "Bubble", Title = "Bubble Compile" });
        var dashboard = generator.GenerateDashboard(new ChartDashboardRequest());

        workspace.AddPage(single.PageName, single.Code);
        workspace.AddPage(dashboard.PageName, dashboard.Code);

        workspace.Build();
    }

    private sealed class RazorCompileWorkspace : IDisposable
    {
        private readonly string root = Path.Combine(Path.GetTempPath(), $"bex-chartjs-compile-{Guid.NewGuid():N}");

        public RazorCompileWorkspace()
        {
            Directory.CreateDirectory(root);
            Directory.CreateDirectory(Path.Combine(root, "Pages"));

            File.WriteAllText(Path.Combine(root, "CompileHost.csproj"), $$"""
                <Project Sdk="Microsoft.NET.Sdk.Razor">
                  <PropertyGroup>
                    <TargetFramework>net10.0</TargetFramework>
                    <Nullable>enable</Nullable>
                    <ImplicitUsings>enable</ImplicitUsings>
                  </PropertyGroup>
                  <ItemGroup>
                    <FrameworkReference Include="Microsoft.AspNetCore.App" />
                    <ProjectReference Include="{{Path.Combine(FindRepoRoot(), "BlazorExpress.ChartJS", "BlazorExpress.ChartJS.csproj")}}" />
                  </ItemGroup>
                </Project>
                """);

            File.WriteAllText(Path.Combine(root, "_Imports.razor"), "@using BlazorExpress.ChartJS" + Environment.NewLine);
        }

        public void AddPage(string pageName, string code) =>
            File.WriteAllText(Path.Combine(root, "Pages", $"{pageName}.razor"), code);

        public void Build()
        {
            using var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "dotnet";
            process.StartInfo.ArgumentList.Add("build");
            process.StartInfo.ArgumentList.Add(Path.Combine(root, "CompileHost.csproj"));
            process.StartInfo.ArgumentList.Add("--nologo");
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            process.WaitForExit(120_000);

            Assert.True(process.ExitCode == 0, output + Environment.NewLine + error);
        }

        public void Dispose()
        {
            if (Directory.Exists(root))
                Directory.Delete(root, recursive: true);
        }

        private static string FindRepoRoot()
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);
            while (directory is not null)
            {
                if (File.Exists(Path.Combine(directory.FullName, "BlazorExpress.ChartJS.sln")))
                    return directory.FullName;

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException("Could not locate repository root.");
        }
    }
}
