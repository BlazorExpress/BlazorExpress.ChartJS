using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Interop;

/// <summary>
/// Checks that <c>UpdateAsync</c> with a mode sends the Chart.js mode as the last <c>update</c> argument, and that the default
/// mode and the 2-argument overload send the same call as before update modes existed.
/// </summary>
public class UpdateModeTests : BunitContext
{
    #region Methods

    #region Public Methods

    [Theory]
    [MemberData(nameof(ChartTypesAndModes))]
    public async Task UpdateAsync_WithMode_SendsChartJsMode(string chartType, ChartUpdateMode mode)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);

        await chart.UpdateAsync(ChartTestData.CreateChartData(chartType), ChartTestData.CreateOptions(chartType), mode);

        var arguments = UpdateInvocation().Arguments;
        if (mode == ChartUpdateMode.Default)
        {
            Assert.Equal(4, arguments.Count);
        }
        else
        {
            Assert.Equal(5, arguments.Count);
            Assert.Equal(mode.ToChartJsMode(), arguments[^1]);
        }
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypesAndBase), MemberType = typeof(ChartTestData))]
    public async Task UpdateAsync_WithoutMode_SendsFourArguments(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);

        await chart.UpdateAsync(ChartTestData.CreateChartData(chartType), ChartTestData.CreateOptions(chartType));

        var arguments = UpdateInvocation().Arguments;
        Assert.Equal(4, arguments.Count);
        Assert.Equal(chart.Id, arguments[0]);
    }

    #endregion

    #region Private Methods

    private JSRuntimeInvocation UpdateInvocation() =>
        Assert.Single(JSInterop.Invocations, i => i.Identifier.EndsWith(".update", StringComparison.Ordinal));

    #endregion

    #endregion

    #region Properties, Indexers

    public static TheoryData<string, ChartUpdateMode> ChartTypesAndModes
    {
        get
        {
            var data = new TheoryData<string, ChartUpdateMode>();

            foreach (var chartType in new[] { "base", "bar", "bubble", "doughnut", "line", "pie", "polarArea", "radar", "scatter" })
                foreach (var mode in Enum.GetValues<ChartUpdateMode>())
                    data.Add(chartType, mode);

            return data;
        }
    }

    #endregion
}
