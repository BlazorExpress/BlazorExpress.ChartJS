using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Lifecycle;

public class ChartDisposeTests : BunitContext
{
    #region Methods

    #region Public Methods

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task AddDataAsync_AfterDispose_StillValidatesAndReturnsChartData(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        await chart.DisposeAsync();
        var chartData = ChartTestData.CreateChartData(chartType);

        await Assert.ThrowsAsync<ArgumentNullException>(() => chart.AddDataAsync(null!, "C", ChartTestData.CreateDatasetData(chartType)));
        var result = await chart.AddDataAsync(chartData, "C", ChartTestData.CreateDatasetData(chartType));

        Assert.Same(chartData, result);
    }

    [Fact]
    public async Task DisposeAsync_BeforeFirstRender_MakesNoJsCall()
    {
        var chart = new BarChart();

        await chart.DisposeAsync();
        await chart.DisposeAsync();

        Assert.Empty(JSInterop.Invocations);
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypesAndBase), MemberType = typeof(ChartTestData))]
    public async Task DisposeAsync_DestroysChartOnce(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);

        await chart.DisposeAsync();
        await chart.DisposeAsync();
        chart.Dispose();

        var destroy = Assert.Single(JSInterop.Invocations, i => i.Identifier == ChartInterop.Destroy);
        Assert.Equal(chart.Id, destroy.Arguments[0]);
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypesAndOperations), MemberType = typeof(ChartTestData))]
    public async Task Operation_AfterDispose_MakesNoJsCall(string chartType, string operation)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        await chart.DisposeAsync();
        var invocationsAfterDispose = JSInterop.Invocations.Count;

        await ChartTestData.RunOperationAsync(chart, chartType, operation);

        Assert.Equal(invocationsAfterDispose, JSInterop.Invocations.Count);
    }

    #endregion

    #endregion
}
