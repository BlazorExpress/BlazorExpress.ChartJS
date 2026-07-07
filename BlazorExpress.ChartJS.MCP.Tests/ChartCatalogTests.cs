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
        Assert.NotEmpty(schema.Examples);
    }

    [Fact]
    public void Radar_Is_Not_Datalabel_Safe()
    {
        var schema = ChartCatalog.GetSchema("Radar");

        Assert.False(schema.SupportsDatalabels);
    }

    [Theory]
    [InlineData("Bubble")]
    [InlineData("Radar")]
    public void Charts_Without_Plugins_Report_No_Title_Or_Legend_Support(string chartType)
    {
        var schema = ChartCatalog.GetSchema(chartType);

        Assert.False(schema.SupportsPluginOptions);
        Assert.False(schema.SupportsTitleOptions);
        Assert.False(schema.SupportsLegendOptions);
    }

    [Theory]
    [InlineData("Bar")]
    [InlineData("Scatter")]
    public void Charts_With_Plugins_Report_Title_And_Legend_Support(string chartType)
    {
        var schema = ChartCatalog.GetSchema(chartType);

        Assert.True(schema.SupportsPluginOptions);
        Assert.True(schema.SupportsTitleOptions);
        Assert.True(schema.SupportsLegendOptions);
    }

    [Theory]
    [InlineData("Bar", "numericDatasetsJson")]
    [InlineData("Scatter", "pointDatasetsJson")]
    [InlineData("Bubble", "pointDatasetsJson")]
    public void GetSchema_Includes_Dataset_Examples(string chartType, string exampleKey)
    {
        var schema = ChartCatalog.GetSchema(chartType);

        Assert.True(schema.Examples.ContainsKey(exampleKey));
    }
}
