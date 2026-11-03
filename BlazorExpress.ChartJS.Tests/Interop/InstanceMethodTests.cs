using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Interop;

/// <summary>
/// Checks that every chart type exposes the instance, visibility, highlight and export methods, and that each one
/// calls its shared JavaScript function with the chart id and the expected arguments.
/// </summary>
public class InstanceMethodTests : BunitContext
{
    #region Fields and Constants

    private static readonly Dictionary<string, (Func<ChartComponentBase, Task> Run, string Identifier, object?[] Arguments)> actions = new()
    {
        ["ClearAsync"] = (c => c.ClearAsync(), ChartInterop.Clear, []),
        ["DownloadImageAsync()"] = (c => c.DownloadImageAsync(), ChartInterop.DownloadImage, ["chart.png", "image/png", null]),
        ["DownloadImageAsync(name, jpeg, 0.8)"] = (c => c.DownloadImageAsync("sales.jpg", ChartImageType.Jpeg, 0.8), ChartInterop.DownloadImage, ["sales.jpg", "image/jpeg", 0.8]),
        ["DownloadImageAsync(webp)"] = (c => c.DownloadImageAsync(imageType: ChartImageType.Webp), ChartInterop.DownloadImage, ["chart.webp", "image/webp", null]),
        ["HideAsync(dataset)"] = (c => c.HideAsync(1), ChartInterop.Hide, [1, null]),
        ["HideAsync(dataset, data)"] = (c => c.HideAsync(1, 2), ChartInterop.Hide, [1, 2]),
        ["RenderAsync"] = (c => c.RenderAsync(), ChartInterop.Render, []),
        ["ResetAsync"] = (c => c.ResetAsync(), ChartInterop.Reset, []),
        ["SetDatasetVisibilityAsync"] = (c => c.SetDatasetVisibilityAsync(1, false), ChartInterop.SetDatasetVisibility, [1, false]),
        ["ShowAsync(dataset)"] = (c => c.ShowAsync(1), ChartInterop.Show, [1, null]),
        ["ShowAsync(dataset, data)"] = (c => c.ShowAsync(1, 2), ChartInterop.Show, [1, 2]),
        ["StopAsync"] = (c => c.StopAsync(), ChartInterop.Stop, []),
        ["ToggleDataVisibilityAsync"] = (c => c.ToggleDataVisibilityAsync(2), ChartInterop.ToggleDataVisibility, [2])
    };

    #endregion

    #region Methods

    #region Public Methods

    [Theory]
    [MemberData(nameof(ChartTypesAndActions))]
    public async Task Action_InvokesSharedFunctionWithArguments(string chartType, string action)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var (run, identifier, arguments) = actions[action];

        await run(chart);

        var invocation = Assert.Single(JSInterop.Invocations, i => i.Identifier == identifier);
        Assert.Equal([chart.Id, .. arguments], invocation.Arguments);
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task GetDataVisibilityAsync_ReturnsBrowserValue(string chartType)
    {
        var chart = ChartTestData.RenderChart(this, chartType);
        JSInterop.Setup<bool>(ChartInterop.GetDataVisibility, i => i.Arguments.SequenceEqual(new object?[] { chart.Id, 2 })).SetResult(true);

        Assert.True(await chart.GetDataVisibilityAsync(2));
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task GetVisibleDatasetCountAsync_ReturnsBrowserValue(string chartType)
    {
        var chart = ChartTestData.RenderChart(this, chartType);
        JSInterop.Setup<int>(ChartInterop.GetVisibleDatasetCount, i => i.Arguments.SequenceEqual(new object?[] { chart.Id })).SetResult(2);

        Assert.Equal(2, await chart.GetVisibleDatasetCountAsync());
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task IsDatasetVisibleAsync_ReturnsBrowserValue(string chartType)
    {
        var chart = ChartTestData.RenderChart(this, chartType);
        JSInterop.Setup<bool>(ChartInterop.IsDatasetVisible, i => i.Arguments.SequenceEqual(new object?[] { chart.Id, 1 })).SetResult(true);

        Assert.True(await chart.IsDatasetVisibleAsync(1));
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task SetActiveElementsAsync_SendsElements(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);

        await chart.SetActiveElementsAsync([new ChartActiveElement(0, 1), new ChartActiveElement(1, 0)]);

        var invocation = Assert.Single(JSInterop.Invocations, i => i.Identifier == ChartInterop.SetActiveElements);
        Assert.Equal(chart.Id, invocation.Arguments[0]);
        var elements = ChartTestData.Serialize(invocation.Arguments[1]);
        Assert.Equal(2, elements.GetArrayLength());
        Assert.Equal(0, elements[0].GetProperty("datasetIndex").GetInt32());
        Assert.Equal(1, elements[0].GetProperty("index").GetInt32());
    }

    [Fact]
    public async Task SetActiveElementsAsync_WithEmptyList_SendsEmptyListToClear()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, "bar");

        await chart.SetActiveElementsAsync([]);

        var invocation = Assert.Single(JSInterop.Invocations, i => i.Identifier == ChartInterop.SetActiveElements);
        Assert.Equal(0, ChartTestData.Serialize(invocation.Arguments[1]).GetArrayLength());
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task ToBase64ImageAsync_ReadsDataUrlFromStream(string chartType)
    {
        var chart = ChartTestData.RenderChart(this, chartType);
        var stream = new FakeJSStreamReference("data:image/png;base64,iVBORw0KGgo=");
        JSInterop.Setup<IJSStreamReference?>(ChartInterop.ToBase64Image, i => i.Arguments.SequenceEqual(new object?[] { chart.Id, "image/png", null })).SetResult(stream);

        var dataUrl = await chart.ToBase64ImageAsync();

        Assert.Equal("data:image/png;base64,iVBORw0KGgo=", dataUrl);
        Assert.Equal(64L * 1024 * 1024, stream.MaxAllowedSize);
        Assert.True(stream.IsDisposed);
    }

    [Fact]
    public async Task ToBase64ImageAsync_SendsTypeAndQuality()
    {
        var chart = ChartTestData.RenderChart(this, "bar");
        JSInterop.Setup<IJSStreamReference?>(ChartInterop.ToBase64Image, i => i.Arguments.SequenceEqual(new object?[] { chart.Id, "image/jpeg", 0.8 }))
            .SetResult(new FakeJSStreamReference("data:image/jpeg;base64,/9j/"));

        Assert.Equal("data:image/jpeg;base64,/9j/", await chart.ToBase64ImageAsync(ChartImageType.Jpeg, 0.8));
    }

    [Fact]
    public async Task ToBase64ImageAsync_WhenBrowserHasNoChart_ReturnsNull()
    {
        var chart = ChartTestData.RenderChart(this, "bar");
        JSInterop.Setup<IJSStreamReference?>(ChartInterop.ToBase64Image, _ => true).SetResult(new FakeJSStreamReference(string.Empty));

        Assert.Null(await chart.ToBase64ImageAsync());
    }

    [Fact]
    public async Task ToBase64ImageAsync_WhenDisposeFailsAfterRead_ReturnsDataUrl()
    {
        var chart = ChartTestData.RenderChart(this, "bar");
        var stream = new FakeJSStreamReference("data:image/png;base64,iVBORw0KGgo=", disposeException: new JSDisconnectedException("Circuit disconnected."));
        JSInterop.Setup<IJSStreamReference?>(ChartInterop.ToBase64Image, _ => true).SetResult(stream);

        Assert.Equal("data:image/png;base64,iVBORw0KGgo=", await chart.ToBase64ImageAsync());
        Assert.True(stream.IsDisposed);
    }

    #endregion

    #endregion

    #region Properties, Indexers

    public static TheoryData<string, string> ChartTypesAndActions
    {
        get
        {
            var data = new TheoryData<string, string>();

            foreach (var chartType in new[] { "bar", "bubble", "doughnut", "line", "pie", "polarArea", "radar", "scatter" })
                foreach (var action in actions.Keys)
                    data.Add(chartType, action);

            return data;
        }
    }

    #endregion
}
