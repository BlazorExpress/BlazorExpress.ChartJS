using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Interop;

/// <summary>
/// Date add-data (rule "data forms and add-data methods"): line and bar charts add a date label with one value per dataset,
/// and line, bar, scatter and bubble charts append a time point to one dataset's <c>TimeData</c>, in C# and in the browser payload.
/// Every invalid call throws before anything is changed or sent.
/// </summary>
public class TimeAddDataTests : BunitContext
{
    #region Methods

    #region Public Methods

    [Fact]
    public async Task LineChart_DateLabel_AppendsToTimeLabelsAndData()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<LineChart>().Instance;
        var coffee = new LineChartDataset { Label = "Coffee", Data = [12] };
        var chartData = new ChartData { TimeLabels = [new DateOnly(2026, 1, 1)], Datasets = [coffee] };

        await chart.AddTimeDataAsync(chartData, new DateOnly(2026, 1, 2), new LineChartDatasetData("Coffee", 15));

        Assert.Equal([new DateOnly(2026, 1, 1), new DateOnly(2026, 1, 2)], chartData.TimeLabels.Select(Unwrap));
        Assert.Equal([12d, 15d], coffee.Data!.Select(v => v!.Value));
        var call = Assert.Single(JSInterop.Invocations, i => i.Identifier == LineChartInterop.AddDatasetData);
        Assert.Equal("2026-01-02", call.Arguments[1]);
        Assert.Equal(15, ChartTestData.Serialize(call.Arguments[2]).GetProperty("data").GetDouble());
    }

    [Fact]
    public async Task BarChart_DateLabel_AppendsOneValuePerDataset()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<BarChart>().Instance;
        var orders = new BarChartDataset { Label = "Orders", Data = [3] };
        var returns = new BarChartDataset { Label = "Returns", Data = [1] };
        var chartData = new ChartData { TimeLabels = [new DateOnly(2026, 1, 1)], Datasets = [orders, returns] };

        await chart.AddTimeDataAsync(chartData, new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc),
            new List<IChartDatasetData> { new BarChartDatasetData("Orders", 4), new BarChartDatasetData("Returns", 2) });

        Assert.Equal(2, chartData.TimeLabels.Count);
        Assert.Equal([3d, 4d], orders.Data!.Select(v => v!.Value));
        Assert.Equal([1d, 2d], returns.Data!.Select(v => v!.Value));
        var call = Assert.Single(JSInterop.Invocations, i => i.Identifier == BarChartInterop.AddDatasetsData);
        Assert.Equal("2026-01-02T00:00:00.000Z", call.Arguments[1]);
    }

    [Fact]
    public async Task DateLabel_WithoutTimeLabels_ThrowsBeforeAnyChange()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<LineChart>().Instance;
        var coffee = new LineChartDataset { Label = "Coffee", Data = [12] };
        var chartData = new ChartData { Labels = ["Jan"], Datasets = [coffee] };

        await Assert.ThrowsAsync<ArgumentException>(() => chart.AddTimeDataAsync(chartData, new DateOnly(2026, 1, 2), new LineChartDatasetData("Coffee", 15)));

        Assert.Equal([12d], coffee.Data!.Select(v => v!.Value));
        Assert.Empty(JSInterop.Invocations);
    }

    [Fact]
    public async Task DateLabel_ThatExists_ThrowsInTheListOverload()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<LineChart>().Instance;
        var coffee = new LineChartDataset { Label = "Coffee", Data = [12] };
        var chartData = new ChartData { TimeLabels = [new DateOnly(2026, 1, 1)], Datasets = [coffee] };

        await Assert.ThrowsAsync<ArgumentException>(() => chart.AddTimeDataAsync(chartData, new DateOnly(2026, 1, 1), new List<IChartDatasetData> { new LineChartDatasetData("Coffee", 15) }));
        await Assert.ThrowsAsync<ArgumentException>(() => chart.AddTimeDataAsync(chartData, new DateOnly(2026, 1, 3), new List<IChartDatasetData>()));

        Assert.Single(chartData.TimeLabels);
        Assert.Empty(JSInterop.Invocations);
    }

    [Fact]
    public async Task DateLabel_CountMismatch_ThrowsInvalidDataException()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<BarChart>().Instance;
        var chartData = new ChartData
        {
            TimeLabels = [new DateOnly(2026, 1, 1)],
            Datasets = [new BarChartDataset { Label = "Orders", Data = [3] }, new BarChartDataset { Label = "Returns", Data = [1] }]
        };

        await Assert.ThrowsAsync<InvalidDataException>(() => chart.AddTimeDataAsync(chartData, new DateOnly(2026, 1, 2), new List<IChartDatasetData> { new BarChartDatasetData("Orders", 4) }));
        Assert.Empty(JSInterop.Invocations);
    }

    [Fact]
    public async Task OtherCharts_IgnoreDateLabels()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<PieChart>().Instance;
        var chartData = new ChartData { TimeLabels = [], Datasets = [] };

        var result = await chart.AddTimeDataAsync(chartData, new DateOnly(2026, 1, 2), new PieChartDatasetData("A", 1, "red"));

        Assert.Same(chartData, result);
        Assert.Empty(chartData.TimeLabels);
        Assert.Empty(JSInterop.Invocations);
    }

    [Fact]
    public async Task LineChart_TimePoint_AppendsToTimeDataAndSendsThePoint()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<LineChart>().Instance;
        var sensor = new LineChartDataset { Label = "Sensor A", TimeData = [new(new DateTime(2026, 1, 5, 9, 0, 0), 21.5)] };
        var chartData = new ChartData { Datasets = [sensor] };

        await chart.AddDataAsync(chartData, "Sensor A", new LineChartDatasetData("Sensor A", new DateTime(2026, 1, 5, 9, 52, 0), 23.9));

        Assert.Equal(2, sensor.TimeData!.Count);
        Assert.Null(chartData.Labels);
        var call = Assert.Single(JSInterop.Invocations, i => i.Identifier == LineChartInterop.AddDatasetPoint);
        Assert.Equal("Sensor A", call.Arguments[1]);
        Assert.Equal("{\"x\":\"2026-01-05T09:52:00.000\",\"y\":23.9}", ChartTestData.Serialize(call.Arguments[2]).GetRawText());
    }

    [Fact]
    public async Task BarChart_TimePoint_CreatesTimeDataForAnEmptyDataset()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<BarChart>().Instance;
        var orders = new BarChartDataset { Label = "Orders" };
        var chartData = new ChartData { Datasets = [orders] };

        await chart.AddDataAsync(chartData, "Orders", new BarChartDatasetData("Orders", new DateOnly(2026, 3, 1), 30));

        Assert.Equal([new ChartTimePoint(new DateOnly(2026, 3, 1), 30)], orders.TimeData!);
        Assert.Single(JSInterop.Invocations, i => i.Identifier == BarChartInterop.AddDatasetPoint);
    }

    [Fact]
    public async Task ScatterChart_TimePoint_AppendsToTimeData()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<ScatterChart>().Instance;
        var sensor = new ScatterChartDataset { Label = "Sensor B", TimeData = [] };
        var chartData = new ChartData { Datasets = [sensor] };

        await chart.AddDataAsync(chartData, "Sensor B", new ScatterChartDatasetData("Sensor B", new DateTime(2026, 1, 5, 9, 3, 0), 19.8));

        Assert.Single(sensor.TimeData!);
        Assert.Single(JSInterop.Invocations, i => i.Identifier == ScatterChartInterop.AddDatasetPoint);
    }

    [Fact]
    public async Task BubbleChart_TimePoint_AppendsToTimeData()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<BubbleChart>().Instance;
        var readings = new BubbleChartDataset { Label = "Readings" };
        var chartData = new ChartData { Datasets = [readings] };

        await chart.AddDataAsync(chartData, "Readings", new BubbleChartDatasetData("Readings", new DateTime(2026, 1, 5, 9, 0, 0), 21.5, 6));

        Assert.Equal([new BubbleChartTimePoint(new DateTime(2026, 1, 5, 9, 0, 0), 21.5, 6)], readings.TimeData!);
        var call = Assert.Single(JSInterop.Invocations, i => i.Identifier == BubbleChartInterop.AddDatasetPoint);
        Assert.Equal("{\"x\":\"2026-01-05T09:00:00.000\",\"y\":21.5,\"r\":6}", ChartTestData.Serialize(call.Arguments[2]).GetRawText());
    }

    [Fact]
    public async Task TimePoint_WithADifferentDataLabel_ThrowsBeforeAnyChange()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<LineChart>().Instance;
        var sensor = new LineChartDataset { Label = "Sensor A" };
        var chartData = new ChartData { Datasets = [sensor] };

        await Assert.ThrowsAsync<ArgumentException>(() => chart.AddDataAsync(chartData, "09:52", new LineChartDatasetData("Sensor A", new DateTime(2026, 1, 5, 9, 52, 0), 23.9)));

        Assert.Null(sensor.TimeData);
        Assert.Empty(JSInterop.Invocations);
    }

    [Fact]
    public async Task TimePoint_ForAnUnknownDataset_Throws()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<ScatterChart>().Instance;
        var chartData = new ChartData { Datasets = [new ScatterChartDataset { Label = "Sensor A" }] };

        await Assert.ThrowsAsync<ArgumentException>(() => chart.AddDataAsync(chartData, "Sensor B", new ScatterChartDatasetData("Sensor B", new DateOnly(2026, 1, 5), 1)));
        Assert.Empty(JSInterop.Invocations);
    }

    [Fact]
    public async Task TimePoint_ForADatasetThatUsesData_ThrowsBeforeAnyChange()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<BarChart>().Instance;
        var floating = new BarChartDataset { Label = "Plan", FloatingData = [(1, 2)] };
        var line = new LineChartDataset { Label = "Trend", Data = [1] };
        var chartData = new ChartData { Labels = ["A"], Datasets = [floating, line] };

        await Assert.ThrowsAsync<ArgumentException>(() => chart.AddDataAsync(chartData, "Plan", new BarChartDatasetData("Plan", new DateOnly(2026, 1, 5), 1)));
        await Assert.ThrowsAsync<ArgumentException>(() => chart.AddDataAsync(chartData, "Trend", new LineChartDatasetData("Trend", new DateOnly(2026, 1, 5), 1)));

        Assert.Null(floating.TimeData);
        Assert.Null(line.TimeData);
        Assert.Empty(JSInterop.Invocations);
    }

    [Fact]
    public async Task TimePoint_WithNullDataLabel_ThrowsArgumentNullException()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<LineChart>().Instance;
        var chartData = new ChartData { Datasets = [new LineChartDataset { Label = "Sensor A" }] };

        await Assert.ThrowsAsync<ArgumentNullException>(() => chart.AddDataAsync(chartData, (string)null!, new LineChartDatasetData("Sensor A", new DateOnly(2026, 1, 5), 1)));
    }

    #endregion

    #region Private Methods

    private static DateOnly Unwrap(ChartTimeValue value) => DateOnly.Parse(value.ToString(), System.Globalization.CultureInfo.InvariantCulture);

    #endregion

    #endregion
}
