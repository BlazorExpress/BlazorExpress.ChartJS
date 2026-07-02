namespace BlazorExpress.ChartJS.MCP.Tests;

public class ChartCatalogTests
{
    [Fact]
    public void All_Returns_Current_Public_Chart_Types()
    {
        var names = ChartCatalog.All.Select(x => x.Name).ToArray();

        Assert.Equal(["Bar", "Bubble", "Doughnut", "Line", "Pie", "PolarArea", "Radar", "Scatter"], names);
    }

    [Theory]
    [InlineData("Bar", "BarChart", "BarChartOptions", "BarChartDataset")]
    [InlineData("polar-area", "PolarAreaChart", "PolarAreaChartOptions", "PolarAreaChartDataset")]
    public void GetSchema_Returns_Metadata_For_Chart_Type(string chartType, string component, string options, string dataset)
    {
        var schema = ChartCatalog.GetSchema(chartType);

        Assert.Equal(component, schema.Component);
        Assert.Equal(options, schema.OptionsType);
        Assert.Equal(dataset, schema.DatasetType);
        Assert.NotEmpty(schema.CommonInputs);
    }
}
