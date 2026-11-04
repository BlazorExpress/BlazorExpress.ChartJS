using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Serialization;

/// <summary>
/// Locks the JSON each chart sends to <c>initialize</c>, measured on 1.2.4 before the 26110201 refactor.
/// </summary>
public class ChartInteropJsonTests : BunitContext
{
    #region Fields and Constants

    private static readonly Dictionary<string, string[]> datasetProperties = new()
    {
        ["bar"] = ["barPercentage", "borderRadius", "borderSkipped", "categoryPercentage", "data", "datalabels", "grouped", "hidden", "label", "oid", "order", "skipNull", "type"],
        ["bubble"] = ["borderWidth", "data", "datalabels", "hidden", "label", "oid", "order"],
        ["doughnut"] = ["borderDashOffset", "data", "datalabels", "hidden", "label", "oid", "order", "spacing", "weight"],
        ["line"] = ["backgroundColor", "borderCapStyle", "borderColor", "borderDashOffset", "borderJoinStyle", "borderWidth", "cubicInterpolationMode", "data", "datalabels", "fill", "hidden", "hoverBorderJoinStyle", "indexAxis", "label", "oid", "order", "showLine", "stepped", "tension", "type"],
        ["pie"] = ["borderDashOffset", "data", "datalabels", "hidden", "label", "oid", "order", "spacing", "weight"],
        ["polarArea"] = ["borderDashOffset", "circular", "data", "datalabels", "hidden", "label", "oid", "order"],
        ["radar"] = ["backgroundColor", "borderCapStyle", "borderColor", "borderDashOffset", "borderJoinStyle", "borderWidth", "data", "datalabels", "fill", "hidden", "hoverBorderJoinStyle", "label", "oid", "order", "tension"],
        ["scatter"] = ["backgroundColor", "borderCapStyle", "borderColor", "borderDashOffset", "borderJoinStyle", "borderWidth", "cubicInterpolationMode", "data", "datalabels", "fill", "hidden", "hoverBorderJoinStyle", "label", "oid", "order", "showLine", "stepped", "tension"]
    };

    private static readonly Dictionary<string, string[]> optionProperties = new()
    {
        ["bar"] = ["interaction", "layout", "maintainAspectRatio", "plugins", "responsive", "scales"],
        ["bubble"] = ["maintainAspectRatio", "responsive"],
        ["doughnut"] = ["maintainAspectRatio", "plugins", "responsive"],
        ["line"] = ["interaction", "layout", "maintainAspectRatio", "plugins", "responsive", "scales"],
        ["pie"] = ["maintainAspectRatio", "plugins", "responsive"],
        ["polarArea"] = ["maintainAspectRatio", "plugins", "responsive"],
        ["radar"] = ["maintainAspectRatio", "responsive"],
        ["scatter"] = ["interaction", "layout", "maintainAspectRatio", "plugins", "responsive", "scales"]
    };

    #endregion

    #region Methods

    #region Public Methods

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task InitializeAsync_SendsDatasetKeyWhenSet(string chartType)
    {
        var chartData = ChartTestData.CreateChartData(chartType);
        SetKey(chartData.Datasets![0], "sales");

        var dataset = (await InitializeAsync(chartType, chartData)).Data.GetProperty("datasets")[0];

        Assert.Equal("sales", dataset.GetProperty("key").GetString());
        Assert.Equal(datasetProperties[chartType].Append("key").Order(), dataset.EnumerateObject().Select(p => p.Name).Order());
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task InitializeAsync_SendsDatasetProperties(string chartType)
    {
        var dataset = (await InitializeAsync(chartType)).Data.GetProperty("datasets")[0];

        Assert.Equal(datasetProperties[chartType], dataset.EnumerateObject().Select(p => p.Name).Order());
        Assert.Equal("Series 1", dataset.GetProperty("label").GetString());
        Assert.Equal(2, dataset.GetProperty("datalabels").GetProperty("borderWidth").GetInt32());
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task InitializeAsync_SendsDatasetTypeOnlyForBarAndLine(string chartType)
    {
        var dataset = (await InitializeAsync(chartType)).Data.GetProperty("datasets")[0];

        if (chartType is "bar" or "line")
            Assert.Equal(chartType, dataset.GetProperty("type").GetString());
        else
            Assert.False(dataset.TryGetProperty("type", out _));
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task InitializeAsync_SendsInteropTarget(string chartType)
    {
        var sent = await InitializeAsync(chartType);

        Assert.Equal($"{ChartTestData.InteropPrefix}{chartType}.initialize", sent.Identifier);
        Assert.Equal(chartType, sent.Type);
        Assert.Equal(["A", "B"], sent.Data.GetProperty("labels").EnumerateArray().Select(l => l.GetString()));
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task InitializeAsync_SendsOptionProperties(string chartType)
    {
        var options = (await InitializeAsync(chartType)).Options;

        Assert.Equal(optionProperties[chartType], options.EnumerateObject().Select(p => p.Name).Order());
        Assert.True(options.GetProperty("responsive").GetBoolean());
        Assert.True(options.GetProperty("maintainAspectRatio").GetBoolean());

        if (options.TryGetProperty("plugins", out var plugins))
            Assert.Equal(["datalabels", "legend", "title"], plugins.EnumerateObject().Select(p => p.Name).Order());

        if (options.TryGetProperty("scales", out var scales))
            Assert.Equal(["x", "y"], scales.EnumerateObject().Select(p => p.Name).Order());
    }

    [Fact]
    public async Task InitializeAsync_SendsPointDataShapes()
    {
        var bubble = (await InitializeAsync("bubble")).Data.GetProperty("datasets")[0].GetProperty("data")[0];
        var scatter = (await InitializeAsync("scatter")).Data.GetProperty("datasets")[0].GetProperty("data")[0];
        var bar = (await InitializeAsync("bar")).Data.GetProperty("datasets")[0].GetProperty("data");

        Assert.Equal(["x", "y", "r"], bubble.EnumerateObject().Select(p => p.Name));
        Assert.Equal(["x", "y"], scatter.EnumerateObject().Select(p => p.Name));
        Assert.Equal([1d, 2d], bar.EnumerateArray().Select(v => v.GetDouble()));
    }

    #endregion

    #region Private Methods

    // Key is declared on the generic ChartDataset<TData>, so it is set through reflection for every dataset type.
    private static void SetKey(IChartDataset dataset, string key) =>
        dataset.GetType().GetProperty(nameof(ChartDataset<double?>.Key))!.SetValue(dataset, key);

    private async Task<SentChart> InitializeAsync(string chartType, ChartData? chartData = null)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;

        var chart = ChartTestData.RenderChart(this, chartType);
        await chart.InitializeAsync(chartData ?? ChartTestData.CreateChartData(chartType), ChartTestData.CreateOptions(chartType));

        var invocation = JSInterop.Invocations.Last(i => i.Identifier.EndsWith(".initialize", StringComparison.Ordinal));

        return new SentChart(invocation.Identifier, (string)invocation.Arguments[1]!, ChartTestData.Serialize(invocation.Arguments[2]), ChartTestData.Serialize(invocation.Arguments[3]));
    }

    #endregion

    #endregion

    #region Nested Types

    private sealed record SentChart(string Identifier, string Type, JsonElement Data, JsonElement Options);

    #endregion
}
