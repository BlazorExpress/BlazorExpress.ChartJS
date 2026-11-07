using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Interop;

/// <summary>
/// Floating bars: <see cref="BarChartDataset.FloatingData" /> sends numbers, <c>[start, end]</c> ranges and nulls, and the add-data
/// methods accept a range through <see cref="BarChartDatasetData" />, in C# and in the browser payload.
/// </summary>
public class FloatingBarTests : BunitContext
{
    #region Methods

    #region Public Methods

    [Fact]
    public async Task AddDataAsync_AppendsARangeInCSharpAndInThePayload()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<BarChart>().Instance;
        var dataset = new BarChartDataset { Label = "Plan", Data = [1, null] };
        var chartData = new ChartData { Labels = ["Wk1", "Wk2"], Datasets = [dataset] };

        await chart.AddDataAsync(chartData, "Wk3", new BarChartDatasetData("Plan", start: 3, end: 9));

        Assert.Null(dataset.Data);
        Assert.Equal("[1,null,[3,9]]", ChartTestData.Serialize(dataset).GetProperty("data").GetRawText());
        var payload = ChartTestData.Serialize(JSInterop.Invocations.Last(i => i.Identifier.EndsWith(".addDatasetData", StringComparison.Ordinal)).Arguments[2]);
        Assert.Equal("[3,9]", payload.GetProperty("data").GetRawText());
    }

    [Fact]
    public async Task AddDataAsync_AppendsANumberToFloatingData()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<BarChart>().Instance;
        var dataset = new BarChartDataset { Label = "Plan", FloatingData = [(1, 4)] };
        var chartData = new ChartData { Labels = ["Wk1"], Datasets = [dataset] };

        await chart.AddDataAsync(chartData, "Wk2", new List<IChartDatasetData> { new BarChartDatasetData("Plan", 6) });

        Assert.Equal("[[1,4],6]", ChartTestData.Serialize(dataset).GetProperty("data").GetRawText());
    }

    [Fact]
    public async Task AddDataAsync_IgnoresARangeForALineDataset()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<BarChart>().Instance;
        var line = new LineChartDataset { Label = "Plan", Data = [1] };
        var chartData = new ChartData { Labels = ["Wk1"], Datasets = [line] };

        await chart.AddDataAsync(chartData, "Wk2", new BarChartDatasetData("Plan", 3, 9));

        Assert.Equal([1d], line.Data!.Select(v => v!.Value));
        Assert.Equal(["Wk1"], chartData.Labels);
        Assert.DoesNotContain(JSInterop.Invocations, i => i.Identifier.EndsWith(".addDatasetData", StringComparison.Ordinal));
    }

    [Fact]
    public async Task AddDataAsync_IgnoresARangeForALineDatasetInTheListOverload()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<LineChart>().Instance;
        var line = new LineChartDataset { Label = "Plan", Data = [1] };
        var chartData = new ChartData { Labels = ["Wk1"], Datasets = [line] };

        await chart.AddDataAsync(chartData, "Wk2", new List<IChartDatasetData> { new BarChartDatasetData("Plan", 3, 9) });

        Assert.Equal([1d], line.Data!.Select(v => v!.Value));

        Assert.Equal(["Wk1"], chartData.Labels);
        Assert.DoesNotContain(JSInterop.Invocations, i => i.Identifier.EndsWith(".addDatasetsData", StringComparison.Ordinal));
    }

    [Fact]
    public void FloatingData_IsSentInsteadOfData()
    {
        var dataset = new BarChartDataset { Data = [1, 2], FloatingData = [5, (2, 8), null] };

        Assert.Equal("[5,[2,8],null]", ChartTestData.Serialize(dataset).GetProperty("data").GetRawText());
    }

    [Fact]
    public async Task UpdateAsync_SendsRanges()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<BarChart>().Instance;
        var chartData = new ChartData { Labels = ["A", "B"], Datasets = [new BarChartDataset { Label = "Plan", FloatingData = [(1, 3), (2, 6)] }] };

        await chart.InitializeAsync(chartData, new BarChartOptions());
        await chart.UpdateAsync(chartData, new BarChartOptions());

        var sent = ChartTestData.Serialize(JSInterop.Invocations.Last(i => i.Identifier.EndsWith(".update", StringComparison.Ordinal)).Arguments[2]);
        Assert.Equal("[[1,3],[2,6]]", sent.GetProperty("datasets")[0].GetProperty("data").GetRawText());
    }

    #endregion

    #endregion
}
