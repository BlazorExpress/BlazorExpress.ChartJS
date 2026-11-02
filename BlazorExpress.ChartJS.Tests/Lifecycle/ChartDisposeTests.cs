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
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = new BarChart();
        AttachJSRuntime(chart);

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
    [MemberData(nameof(ChartTestData.ChartTypesAndBase), MemberType = typeof(ChartTestData))]
    public async Task DisposeAsync_WhenDestroyFails_SurfacesErrorAndReleasesReference(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        await chart.InitializeAsync(ChartTestData.CreateChartData(chartType), ChartTestData.CreateOptions(chartType));
        var reference = (IDisposable)JSInterop.Invocations.Last(i => i.Identifier.EndsWith(".initialize", StringComparison.Ordinal)).Arguments[^1]!;
        JSInterop.SetupVoid(ChartInterop.Destroy, _ => true).SetException(new JSException("Chart.js is not loaded."));

        await Assert.ThrowsAsync<JSException>(() => chart.DisposeAsync().AsTask());

        var value = reference.GetType().GetProperty("Value")!;
        var error = Assert.Throws<System.Reflection.TargetInvocationException>(() => value.GetValue(reference));
        Assert.IsType<ObjectDisposedException>(error.InnerException);
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

    #region Private Methods

    /// <summary>
    /// Gives an unrendered chart the test JS runtime, so a JS call made before the first render would be recorded.
    /// </summary>
    private void AttachJSRuntime(ChartComponentBase chart) =>
        typeof(BlazorExpressComponentCore)
            .GetProperty("JSRuntime", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)!
            .SetValue(chart, JSInterop.JSRuntime);

    #endregion

    #endregion
}
