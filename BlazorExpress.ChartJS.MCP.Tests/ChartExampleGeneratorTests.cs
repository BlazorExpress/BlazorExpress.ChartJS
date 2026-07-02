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
}
