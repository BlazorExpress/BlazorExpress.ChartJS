using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Interop;

/// <summary>
/// A literal <c>null</c> label passed to <c>AddDataAsync</c> compiles, as it did in 1.2.4, and resolves to the <see cref="string" /> label
/// overloads: date labels have their own <c>AddTimeDataAsync</c> methods, so no overload competes for <c>null</c>.
/// Each call behaves exactly like the same call with <c>(string)null!</c>.
/// </summary>
public class NullLabelAddDataTests : BunitContext
{
    #region Methods

    #region Public Methods

    [Fact]
    public async Task BarAndLineCharts_NullLiteralLabel_CompilesOnTheConcreteChartTypes()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var line = Render<LineChart>().Instance;
        var bar = Render<BarChart>().Instance;

        // Bar and line charts are the ones that also have date-label methods; these calls must keep compiling.
        // `null!` is still the untyped null literal for overload resolution; the `!` only silences the nullable warning.
        await Assert.ThrowsAnyAsync<ArgumentException>(() => line.AddDataAsync(ChartTestData.CreateChartData("line"), null!, ChartTestData.CreateDatasetData("line")));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => bar.AddDataAsync(ChartTestData.CreateChartData("bar"), null!, new List<IChartDatasetData> { ChartTestData.CreateDatasetData("bar") }));
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task NullLiteralLabel_BehavesLikeAStringNull(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);

        var literal = await OutcomeAsync(() => chart.AddDataAsync(ChartTestData.CreateChartData(chartType), null!, ChartTestData.CreateDatasetData(chartType)));
        var typed = await OutcomeAsync(() => chart.AddDataAsync(ChartTestData.CreateChartData(chartType), (string)null!, ChartTestData.CreateDatasetData(chartType)));
        var literalCollection = await OutcomeAsync(() => chart.AddDataAsync(ChartTestData.CreateChartData(chartType), null!, new List<IChartDatasetData> { ChartTestData.CreateDatasetData(chartType) }));
        var typedCollection = await OutcomeAsync(() => chart.AddDataAsync(ChartTestData.CreateChartData(chartType), (string)null!, new List<IChartDatasetData> { ChartTestData.CreateDatasetData(chartType) }));

        Assert.Equal(typed, literal);
        Assert.Equal(typedCollection, literalCollection);
    }

    #endregion

    #region Private Methods

    private static async Task<string> OutcomeAsync(Func<Task> call)
    {
        try
        {
            await call();
            return "completed";
        }
        catch (Exception ex)
        {
            return ex.GetType().Name;
        }
    }

    #endregion

    #endregion
}
