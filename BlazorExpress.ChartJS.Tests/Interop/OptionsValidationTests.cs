using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Interop;

/// <summary>
/// An additional axis that reuses the ID of a set default axis, or has an empty ID, is a caller mistake:
/// <c>InitializeAsync</c> and <c>UpdateAsync</c> reject it before anything is sent to the browser.
/// </summary>
public class OptionsValidationTests : BunitContext
{
    #region Methods

    #region Public Methods

    [Theory]
    [MemberData(nameof(CasesWithOperations))]
    public async Task DuplicateOrEmptyAxisId_ThrowsBeforeInterop(string chartType, string operation, string id, string message)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var options = CreateOptions(chartType, id);
        var invocationsBefore = JSInterop.Invocations.Count;

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => RunAsync(chart, chartType, operation, options));

        Assert.StartsWith(message, exception.Message, StringComparison.Ordinal);
        Assert.Equal(invocationsBefore, JSInterop.Invocations.Count);
    }

    [Theory]
    [MemberData(nameof(ChartTypes))]
    public async Task DefaultAxisIdWithNullDefaultAxis_IsSent(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);
        var options = CreateOptions(chartType, "y");
        GetScales(options).Y = null;

        await chart.InitializeAsync(ChartTestData.CreateChartData(chartType), options);

        var sent = ChartTestData.Serialize(JSInterop.Invocations.Last(i => i.Identifier.EndsWith(".initialize", StringComparison.Ordinal)).Arguments[3]);
        Assert.Equal("right", sent.GetProperty("scales").GetProperty("y").GetProperty("position").GetString());
    }

    #endregion

    #region Private Methods

    private static IChartOptions CreateOptions(string chartType, string id)
    {
        var options = ChartTestData.CreateOptions(chartType);
        GetScales(options).AdditionalAxes = new() { [id] = new ChartAxes { Position = "right" } };

        return options;
    }

    private static Scales GetScales(IChartOptions options) =>
        options switch
        {
            BarChartOptions o => o.Scales,
            BubbleChartOptions o => o.Scales,
            LineChartOptions o => o.Scales,
            ScatterChartOptions o => o.Scales,
            _ => throw new ArgumentOutOfRangeException(nameof(options))
        };

    private static Task RunAsync(ChartComponentBase chart, string chartType, string operation, IChartOptions options) =>
        operation == "InitializeAsync"
            ? chart.InitializeAsync(ChartTestData.CreateChartData(chartType), options)
            : chart.UpdateAsync(ChartTestData.CreateChartData(chartType), options, ChartUpdateMode.None);

    #endregion

    #endregion

    #region Properties, Indexers

    public static TheoryData<string, string, string, string> CasesWithOperations
    {
        get
        {
            var data = new TheoryData<string, string, string, string>();

            foreach (var chartType in new[] { "base", "bar", "bubble", "line", "scatter" })
                foreach (var operation in new[] { "InitializeAsync", "UpdateAsync" })
                {
                    data.Add(chartType, operation, "x", "Axis ID 'x' is already set by Scales.X.");
                    data.Add(chartType, operation, "y", "Axis ID 'y' is already set by Scales.Y.");
                    data.Add(chartType, operation, "", "An additional axis ID cannot be empty.");
                    data.Add(chartType, operation, " ", "An additional axis ID cannot be empty.");
                }

            return data;
        }
    }

    public static TheoryData<string> ChartTypes => new() { "base", "bar", "bubble", "line", "scatter" };

    #endregion
}
