using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Serialization;

public class BaseChartDataTests : BunitContext
{
    #region Methods

    #region Public Methods

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypes), MemberType = typeof(ChartTestData))]
    public async Task InitializeAsync_BasePath_SendsEveryDatasetType(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<BaseOnlyChart>().Instance;

        await chart.InitializeAsync(ChartTestData.CreateChartData(chartType), ChartTestData.CreateOptions(chartType));

        var invocation = JSInterop.Invocations.Single(i => i.Identifier == ChartInterop.Initialize);
        var datasets = ChartTestData.Serialize(invocation.Arguments[2]).GetProperty("datasets");
        Assert.Equal(1, datasets.GetArrayLength());
        Assert.Equal("Series 1", datasets[0].GetProperty("label").GetString());
    }

    #endregion

    #endregion
}
