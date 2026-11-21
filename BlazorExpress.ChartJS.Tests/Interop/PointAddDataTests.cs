using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Interop;

/// <summary>
/// On scatter and bubble charts both <c>AddDataAsync</c> overloads append each point to the dataset named by its data and
/// never read or change the labels (work item 26112101, AC-04), in C# and in the call sent to the browser.
/// </summary>
public class PointAddDataTests : BunitContext
{
    #region Methods

    #region Public Methods

    public static TheoryData<string> PointCharts => new() { "scatter", "bubble" };

    [Theory]
    [MemberData(nameof(PointCharts))]
    public async Task AddDataAsync_AppendsThePointWithoutLabels(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var chartData = ChartTestData.CreateChartData(chartType);
        chartData.Labels = null;

        var result = await chart.AddDataAsync(chartData, "ignored", ChartTestData.CreateDatasetData(chartType));

        Assert.Same(chartData, result);
        Assert.Null(chartData.Labels);
        Assert.Equal(3, PointCount(chartData.Datasets![0]));

        var invocation = Assert.Single(JSInterop.Invocations, i => i.Identifier.EndsWith("addDatasetPoint", StringComparison.Ordinal));
        Assert.Equal("Series 1", invocation.Arguments[1]);
    }

    [Theory]
    [MemberData(nameof(PointCharts))]
    public async Task AddDataAsync_WithLabels_LeavesThemUnchanged(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var chartData = ChartTestData.CreateChartData(chartType);

        await chart.AddDataAsync(chartData, "C", ChartTestData.CreateDatasetData(chartType));

        Assert.Equal(new[] { "A", "B" }, chartData.Labels);
    }

    [Theory]
    [MemberData(nameof(PointCharts))]
    public async Task AddDataAsync_Collection_AppendsToEachNamedDatasetWithoutLabels(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var chartData = ChartTestData.CreateChartData(chartType);
        chartData.Labels = null;
        var second = ChartTestData.CreateDataset(chartType);
        SetLabel(second, "Series 2");
        chartData.Datasets!.Add(second);

        // One item only: the items no longer have to cover every dataset.
        await chart.AddDataAsync(chartData, "ignored", new List<IChartDatasetData> { WithDatasetLabel(ChartTestData.CreateDatasetData(chartType), "Series 2") });

        Assert.Null(chartData.Labels);
        Assert.Equal(2, PointCount(chartData.Datasets[0]));
        Assert.Equal(3, PointCount(chartData.Datasets[1]));
        Assert.Single(JSInterop.Invocations, i => i.Identifier.EndsWith("addDatasetPoint", StringComparison.Ordinal));
    }

    [Theory]
    [MemberData(nameof(PointCharts))]
    public async Task AddDataAsync_ForAnUnknownDataset_ChangesNothingInCSharp(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var chartData = ChartTestData.CreateChartData(chartType);

        await chart.AddDataAsync(chartData, "C", WithDatasetLabel(ChartTestData.CreateDatasetData(chartType), "No such dataset"));

        Assert.Equal(2, PointCount(chartData.Datasets![0]));
    }

    [Theory]
    [MemberData(nameof(PointCharts))]
    public async Task AddDataAsync_WithNullData_ThrowsBeforeChangingAnything(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var chartData = ChartTestData.CreateChartData(chartType);

        await Assert.ThrowsAsync<ArgumentNullException>(() => chart.AddDataAsync(chartData, "C", (IChartDatasetData)null!));
        await Assert.ThrowsAsync<ArgumentException>(() => chart.AddDataAsync(chartData, "C", new List<IChartDatasetData>()));

        Assert.Equal(2, PointCount(chartData.Datasets![0]));
        Assert.DoesNotContain(JSInterop.Invocations, i => i.Identifier.EndsWith("addDatasetPoint", StringComparison.Ordinal));
    }

    #endregion

    #region Private Methods

    private static int PointCount(IChartDataset dataset) =>
        dataset switch
        {
            ScatterChartDataset s => s.Data!.Count,
            BubbleChartDataset b => b.Data!.Count,
            _ => throw new ArgumentOutOfRangeException(nameof(dataset))
        };

    private static void SetLabel(IChartDataset dataset, string label)
    {
        switch (dataset)
        {
            case ScatterChartDataset s: s.Label = label; break;
            case BubbleChartDataset b: b.Label = label; break;
        }
    }

    private static IChartDatasetData WithDatasetLabel(IChartDatasetData data, string datasetLabel) =>
        data switch
        {
            ScatterChartDatasetData => new ScatterChartDatasetData(datasetLabel, new ScatterChartDataPoint(5, 6)),
            BubbleChartDatasetData => new BubbleChartDatasetData(datasetLabel, new BubbleChartDataPoint(7, 8, 9)),
            _ => throw new ArgumentOutOfRangeException(nameof(data))
        };

    #endregion

    #endregion
}
