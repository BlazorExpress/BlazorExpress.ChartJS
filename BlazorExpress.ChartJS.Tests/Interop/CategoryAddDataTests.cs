using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Interop;

/// <summary>
/// The single-value <c>AddDataAsync</c> on doughnut, pie, polar area and radar charts follows the bar and line charts and
/// Chart.js (work item 26112101, AC-02): the label is a new category, and the value (and slice colour) goes to the dataset
/// named by the data, in C# and in the payload sent to the browser.
/// </summary>
public class CategoryAddDataTests : BunitContext
{
    #region Methods

    #region Public Methods

    public static TheoryData<string> CategoryCharts => new() { "doughnut", "pie", "polarArea", "radar" };

    [Theory]
    [MemberData(nameof(CategoryCharts))]
    public async Task AddDataAsync_AddsTheLabelAndAppendsToTheNamedDataset(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var chartData = ChartTestData.CreateChartData(chartType);
        var other = ChartTestData.CreateDataset(chartType);
        SetLabel(other, "Series 2");
        chartData.Datasets!.Add(other);

        var result = await chart.AddDataAsync(chartData, "C", ChartTestData.CreateDatasetData(chartType));

        Assert.Same(chartData, result);
        Assert.Equal(new[] { "A", "B", "C" }, chartData.Labels);
        Assert.Equal(new double?[] { 1, 2, 3 }, GetData(chartData.Datasets[0]));
        Assert.Equal(new double?[] { 1, 2 }, GetData(chartData.Datasets[1]));

        var invocation = Assert.Single(JSInterop.Invocations, i => i.Identifier.EndsWith("addDatasetData", StringComparison.Ordinal));
        Assert.Equal("C", invocation.Arguments[1]);
    }

    [Theory]
    [InlineData("doughnut")]
    [InlineData("pie")]
    [InlineData("polarArea")]
    public async Task AddDataAsync_AppendsTheSliceColour(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var chartData = ChartTestData.CreateChartData(chartType);
        var colors = new List<string> { "green", "blue" };
        SetColors(chartData.Datasets![0], colors);

        await chart.AddDataAsync(chartData, "C", ChartTestData.CreateDatasetData(chartType));

        Assert.Equal(new[] { "green", "blue", "red" }, colors);
    }

    [Theory]
    [MemberData(nameof(CategoryCharts))]
    public async Task AddDataAsync_WithAnExistingLabel_DoesNotAddItTwice(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var chartData = ChartTestData.CreateChartData(chartType);

        await chart.AddDataAsync(chartData, "B", ChartTestData.CreateDatasetData(chartType));

        Assert.Equal(new[] { "A", "B" }, chartData.Labels);
        Assert.Equal(new double?[] { 1, 2, 3 }, GetData(chartData.Datasets![0]));
    }

    [Theory]
    [MemberData(nameof(CategoryCharts))]
    public async Task AddDataAsync_ForAnUnknownDataset_AddsOnlyTheLabel(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var chartData = ChartTestData.CreateChartData(chartType);
        var data = WithDatasetLabel(ChartTestData.CreateDatasetData(chartType), "No such dataset");

        await chart.AddDataAsync(chartData, "C", data);

        Assert.Equal(new[] { "A", "B", "C" }, chartData.Labels);
        Assert.Equal(new double?[] { 1, 2 }, GetData(chartData.Datasets![0]));
    }

    [Theory]
    [MemberData(nameof(CategoryCharts))]
    public async Task AddDataAsync_WithoutLabels_ThrowsBeforeChangingAnything(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var chartData = ChartTestData.CreateChartData(chartType);
        chartData.Labels = null;

        await Assert.ThrowsAsync<ArgumentNullException>(() => chart.AddDataAsync(chartData, "C", ChartTestData.CreateDatasetData(chartType)));

        Assert.Equal(new double?[] { 1, 2 }, GetData(chartData.Datasets![0]));
        Assert.DoesNotContain(JSInterop.Invocations, i => i.Identifier.EndsWith("addDatasetData", StringComparison.Ordinal));
    }

    #endregion

    #region Private Methods

    private static List<double?>? GetData(IChartDataset dataset) =>
        dataset switch
        {
            DoughnutChartDataset d => d.Data,
            PieChartDataset p => p.Data,
            PolarAreaChartDataset p => p.Data,
            RadarChartDataset r => r.Data,
            _ => throw new ArgumentOutOfRangeException(nameof(dataset))
        };

    private static void SetColors(IChartDataset dataset, List<string> colors)
    {
        switch (dataset)
        {
            case DoughnutChartDataset d: d.BackgroundColor = colors; break;
            case PieChartDataset p: p.BackgroundColor = colors; break;
            case PolarAreaChartDataset p: p.BackgroundColor = colors; break;
        }
    }

    private static void SetLabel(IChartDataset dataset, string label)
    {
        switch (dataset)
        {
            case DoughnutChartDataset d: d.Label = label; break;
            case PieChartDataset p: p.Label = label; break;
            case PolarAreaChartDataset p: p.Label = label; break;
            case RadarChartDataset r: r.Label = label; break;
        }
    }

    private static IChartDatasetData WithDatasetLabel(IChartDatasetData data, string datasetLabel) =>
        data switch
        {
            DoughnutChartDatasetData d => new DoughnutChartDatasetData(datasetLabel, 3, d.BackgroundColor),
            PieChartDatasetData p => new PieChartDatasetData(datasetLabel, 3, p.BackgroundColor),
            PolarAreaChartDatasetData p => new PolarAreaChartDatasetData(datasetLabel, 3, p.BackgroundColor, null),
            RadarChartDatasetData => new RadarChartDatasetData(datasetLabel, 3),
            _ => throw new ArgumentOutOfRangeException(nameof(data))
        };

    #endregion

    #endregion
}
