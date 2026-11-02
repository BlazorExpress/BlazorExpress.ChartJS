namespace BlazorExpress.ChartJS.Tests.Infrastructure;

/// <summary>
/// Builds representative chart inputs and serializes interop arguments the way Blazor's JS interop does.
/// The chart type <c>base</c> is a chart that runs <see cref="ChartComponentBase" />'s own paths, fed with bar data.
/// </summary>
public static class ChartTestData
{
    #region Fields and Constants

    public const string InteropPrefix = "window.blazorexpress.chartjs.";

    /// <summary>
    /// Matches the options <c>Microsoft.JSInterop.JSRuntime</c> uses for interop arguments.
    /// </summary>
    public static readonly JsonSerializerOptions InteropJsonOptions = new()
    {
        MaxDepth = 32,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    #endregion

    #region Methods

    #region Public Methods

    public static ChartData CreateChartData(string chartType) =>
        new() { Labels = new List<string> { "A", "B" }, Datasets = new List<IChartDataset> { CreateDataset(chartType) } };

    public static IChartDataset CreateDataset(string chartType) =>
        chartType switch
        {
            "bar" or "base" => new BarChartDataset { Label = "Series 1", Data = new List<double?> { 1, 2 } },
            "bubble" => new BubbleChartDataset { Label = "Series 1", Data = new List<BubbleChartDataPoint> { new(1, 2, 3), new(4, 5, 6) } },
            "doughnut" => new DoughnutChartDataset { Label = "Series 1", Data = new List<double?> { 1, 2 } },
            "line" => new LineChartDataset { Label = "Series 1", Data = new List<double?> { 1, 2 } },
            "pie" => new PieChartDataset { Label = "Series 1", Data = new List<double?> { 1, 2 } },
            "polarArea" => new PolarAreaChartDataset { Label = "Series 1", Data = new List<double?> { 1, 2 } },
            "radar" => new RadarChartDataset { Label = "Series 1", Data = new List<double?> { 1, 2 } },
            "scatter" => new ScatterChartDataset { Label = "Series 1", Data = new List<ScatterChartDataPoint> { new(1, 2), new(3, 4) } },
            _ => throw new ArgumentOutOfRangeException(nameof(chartType), chartType, null)
        };

    public static IChartDatasetData CreateDatasetData(string chartType) =>
        chartType switch
        {
            "bar" or "base" => new BarChartDatasetData("Series 1", 3),
            "bubble" => new BubbleChartDatasetData("Series 1", new BubbleChartDataPoint(7, 8, 9)),
            "doughnut" => new DoughnutChartDatasetData("Series 1", 3, "red"),
            "line" => new LineChartDatasetData("Series 1", 3),
            "pie" => new PieChartDatasetData("Series 1", 3, "red"),
            "polarArea" => new PolarAreaChartDatasetData("Series 1", 3, "red", "blue"),
            "radar" => new RadarChartDatasetData("Series 1", 3),
            "scatter" => new ScatterChartDatasetData("Series 1", new ScatterChartDataPoint(5, 6)),
            _ => throw new ArgumentOutOfRangeException(nameof(chartType), chartType, null)
        };

    public static IChartOptions CreateOptions(string chartType) =>
        chartType switch
        {
            "bar" or "base" => new BarChartOptions(),
            "bubble" => new BubbleChartOptions(),
            "doughnut" => new DoughnutChartOptions(),
            "line" => new LineChartOptions(),
            "pie" => new PieChartOptions(),
            "polarArea" => new PolarAreaChartOptions(),
            "radar" => new RadarChartOptions(),
            "scatter" => new ScatterChartOptions(),
            _ => throw new ArgumentOutOfRangeException(nameof(chartType), chartType, null)
        };

    public static ChartComponentBase RenderChart(BunitContext context, string chartType) =>
        chartType switch
        {
            "base" => context.Render<BaseOnlyChart>().Instance,
            "bar" => context.Render<BarChart>().Instance,
            "bubble" => context.Render<BubbleChart>().Instance,
            "doughnut" => context.Render<DoughnutChart>().Instance,
            "line" => context.Render<LineChart>().Instance,
            "pie" => context.Render<PieChart>().Instance,
            "polarArea" => context.Render<PolarAreaChart>().Instance,
            "radar" => context.Render<RadarChart>().Instance,
            "scatter" => context.Render<ScatterChart>().Instance,
            _ => throw new ArgumentOutOfRangeException(nameof(chartType), chartType, null)
        };

    /// <summary>
    /// Runs one chart operation with valid inputs for <paramref name="chartType" />.
    /// </summary>
    public static Task RunOperationAsync(ChartComponentBase chart, string chartType, string operation)
    {
        var chartData = CreateChartData(chartType);
        var options = CreateOptions(chartType);

        return operation switch
        {
            "AddDataAsync" => chart.AddDataAsync(chartData, "C", CreateDatasetData(chartType)),
            "AddDataAsync(collection)" => chart.AddDataAsync(chartData, "C", new List<IChartDatasetData> { CreateDatasetData(chartType) }),
            "AddDatasetAsync" => chart.AddDatasetAsync(chartData, CreateDataset(chartType), options),
            "InitializeAsync" => chart.InitializeAsync(chartData, options),
            "ResizeAsync" => chart.ResizeAsync(300, 200),
            "UpdateAsync" => chart.UpdateAsync(chartData, options),
            _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
        };
    }

    public static JsonElement Serialize(object? argument) =>
        JsonSerializer.SerializeToElement(argument, argument?.GetType() ?? typeof(object), InteropJsonOptions);

    #endregion

    #endregion

    #region Properties, Indexers

    public static TheoryData<string> ChartTypes => new() { "bar", "bubble", "doughnut", "line", "pie", "polarArea", "radar", "scatter" };

    public static TheoryData<string> ChartTypesAndBase => new() { "base", "bar", "bubble", "doughnut", "line", "pie", "polarArea", "radar", "scatter" };

    public static TheoryData<string, string> ChartTypesAndOperations
    {
        get
        {
            var data = new TheoryData<string, string>();

            foreach (var chartType in new[] { "base", "bar", "bubble", "doughnut", "line", "pie", "polarArea", "radar", "scatter" })
                foreach (var operation in Operations)
                    data.Add(chartType, operation);

            return data;
        }
    }

    /// <summary>
    /// Like <see cref="ChartTypesAndOperations" />, without the <c>base</c> data operations, which return the data without calling JavaScript.
    /// </summary>
    public static TheoryData<string, string> JsCallingChartTypesAndOperations
    {
        get
        {
            var data = new TheoryData<string, string>();

            foreach (var chartType in new[] { "base", "bar", "bubble", "doughnut", "line", "pie", "polarArea", "radar", "scatter" })
                foreach (var operation in Operations.Where(o => chartType != "base" || !o.StartsWith("AddData", StringComparison.Ordinal)))
                    data.Add(chartType, operation);

            return data;
        }
    }

    public static string[] Operations =>["AddDataAsync", "AddDataAsync(collection)", "AddDatasetAsync", "InitializeAsync", "ResizeAsync", "UpdateAsync"];

    #endregion
}
