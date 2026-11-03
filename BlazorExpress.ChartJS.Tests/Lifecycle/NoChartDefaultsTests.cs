using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Lifecycle;

/// <summary>
/// Checks rule "chart index and no-chart interop behaviour": with no chart (before the first render, after dispose,
/// or after a circuit disconnect) the new methods never throw, actions are skipped and queries return defaults.
/// Other JavaScript failures still surface (CS-015).
/// </summary>
public class NoChartDefaultsTests : BunitContext
{
    #region Fields and Constants

    private static readonly Dictionary<string, Func<ChartComponentBase, Task>> actions = new()
    {
        ["ClearAsync"] = c => c.ClearAsync(),
        ["DownloadImageAsync"] = c => c.DownloadImageAsync(),
        ["HideAsync"] = c => c.HideAsync(0, 1),
        ["RenderAsync"] = c => c.RenderAsync(),
        ["ResetAsync"] = c => c.ResetAsync(),
        ["SetActiveElementsAsync"] = c => c.SetActiveElementsAsync([new ChartActiveElement(0, 1)]),
        ["SetDatasetVisibilityAsync"] = c => c.SetDatasetVisibilityAsync(0, false),
        ["ShowAsync"] = c => c.ShowAsync(0),
        ["StopAsync"] = c => c.StopAsync(),
        ["ToggleDataVisibilityAsync"] = c => c.ToggleDataVisibilityAsync(1)
    };

    #endregion

    #region Methods

    #region Public Methods

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task AfterDispose_ReturnsDefaultsWithoutJsCall(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        await chart.DisposeAsync();
        var invocationsAfterDispose = JSInterop.Invocations.Count;

        await AssertDefaultsAsync(chart);

        foreach (var action in actions.Values)
            await action(chart);

        Assert.Equal(invocationsAfterDispose, JSInterop.Invocations.Count);
    }

    [Fact]
    public async Task BeforeFirstRender_ReturnsDefaultsWithoutJsCall()
    {
        var chart = new BarChart();

        await AssertDefaultsAsync(chart);

        foreach (var action in actions.Values)
            await action(chart);

        Assert.Empty(JSInterop.Invocations);
    }

    [Fact]
    public async Task WhenCallCancelled_ReturnsDefaults()
    {
        var chart = ChartTestData.RenderChart(this, "pie");
        JSInterop.SetupVoid(_ => true).SetCanceled();
        JSInterop.Setup<bool>(_ => true).SetCanceled();
        JSInterop.Setup<int>(_ => true).SetCanceled();
        JSInterop.Setup<IJSStreamReference?>(_ => true).SetCanceled();

        await AssertDefaultsAsync(chart);

        foreach (var action in actions.Values)
            await action(chart);

        AssertEveryNewFunctionWasCalled();
    }

    [Fact]
    public async Task WhenCircuitDisconnected_ReturnsDefaults()
    {
        var chart = ChartTestData.RenderChart(this, "bar");
        var disconnected = new JSDisconnectedException("Circuit disconnected.");
        JSInterop.SetupVoid(_ => true).SetException(disconnected);
        JSInterop.Setup<bool>(_ => true).SetException(disconnected);
        JSInterop.Setup<int>(_ => true).SetException(disconnected);
        JSInterop.Setup<IJSStreamReference?>(_ => true).SetException(disconnected);

        await AssertDefaultsAsync(chart);

        foreach (var action in actions.Values)
            await action(chart);

        AssertEveryNewFunctionWasCalled();
    }

    [Fact]
    public async Task WhenCircuitDisconnectsDuringImageRead_ReturnsNull()
    {
        var chart = ChartTestData.RenderChart(this, "bar");
        JSInterop.Setup<IJSStreamReference?>(ChartInterop.ToBase64Image, _ => true)
            .SetResult(new FakeJSStreamReference("data:image/png;base64,", readException: new JSDisconnectedException("Circuit disconnected.")));

        Assert.Null(await chart.ToBase64ImageAsync());
    }

    [Fact]
    public async Task WhenImageIsLargerThanCap_Throws()
    {
        var chart = ChartTestData.RenderChart(this, "bar");
        JSInterop.Setup<IJSStreamReference?>(ChartInterop.ToBase64Image, _ => true)
            .SetResult(new FakeJSStreamReference("data:image/png;base64,", length: 64L * 1024 * 1024 + 1));

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => chart.ToBase64ImageAsync());
    }

    [Fact]
    public async Task WhenJavaScriptFails_Throws()
    {
        var chart = ChartTestData.RenderChart(this, "bar");
        var failure = new JSException("Chart failed.");
        JSInterop.SetupVoid(_ => true).SetException(failure);
        JSInterop.Setup<bool>(_ => true).SetException(failure);
        JSInterop.Setup<int>(_ => true).SetException(failure);
        JSInterop.Setup<IJSStreamReference?>(_ => true).SetException(failure);

        await Assert.ThrowsAsync<JSException>(() => chart.IsDatasetVisibleAsync(0));
        await Assert.ThrowsAsync<JSException>(() => chart.GetVisibleDatasetCountAsync());
        await Assert.ThrowsAsync<JSException>(() => chart.ToBase64ImageAsync());

        foreach (var action in actions.Values)
            await Assert.ThrowsAsync<JSException>(() => action(chart));
    }

    #endregion

    #region Private Methods

    private static async Task AssertDefaultsAsync(ChartComponentBase chart)
    {
        Assert.Null(await chart.ToBase64ImageAsync());
        Assert.False(await chart.IsDatasetVisibleAsync(0));
        Assert.False(await chart.GetDataVisibilityAsync(0));
        Assert.Equal(0, await chart.GetVisibleDatasetCountAsync());
    }

    // Proves the defaults came from handling the failed call, not from skipping it.
    private void AssertEveryNewFunctionWasCalled()
    {
        string[] identifiers =
        [
            ChartInterop.Clear, ChartInterop.DownloadImage, ChartInterop.GetDataVisibility, ChartInterop.GetVisibleDatasetCount,
            ChartInterop.Hide, ChartInterop.IsDatasetVisible, ChartInterop.Render, ChartInterop.Reset, ChartInterop.SetActiveElements,
            ChartInterop.SetDatasetVisibility, ChartInterop.Show, ChartInterop.Stop, ChartInterop.ToBase64Image, ChartInterop.ToggleDataVisibility
        ];

        foreach (var identifier in identifiers)
            Assert.Contains(JSInterop.Invocations, i => i.Identifier == identifier);
    }

    #endregion

    #endregion
}
