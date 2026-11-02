using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Interop;

public class SafeInvokeTests : BunitContext
{
    #region Methods

    #region Public Methods

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypesAndOperations), MemberType = typeof(ChartTestData))]
    public async Task Operation_WhenCallCancelled_DoesNotThrow(string chartType, string operation)
    {
        var chart = ChartTestData.RenderChart(this, chartType);
        JSInterop.SetupVoid(_ => true).SetCanceled();

        await ChartTestData.RunOperationAsync(chart, chartType, operation);
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypesAndOperations), MemberType = typeof(ChartTestData))]
    public async Task Operation_WhenCircuitDisconnected_DoesNotThrow(string chartType, string operation)
    {
        var chart = ChartTestData.RenderChart(this, chartType);
        JSInterop.SetupVoid(_ => true).SetException(new JSDisconnectedException("Circuit disconnected."));

        await ChartTestData.RunOperationAsync(chart, chartType, operation);
    }

    [Theory]
    [MemberData(nameof(ChartTestData.JsCallingChartTypesAndOperations), MemberType = typeof(ChartTestData))]
    public async Task Operation_WhenJavaScriptFails_Throws(string chartType, string operation)
    {
        var chart = ChartTestData.RenderChart(this, chartType);
        JSInterop.SetupVoid(_ => true).SetException(new JSException("Chart failed."));

        await Assert.ThrowsAsync<JSException>(() => ChartTestData.RunOperationAsync(chart, chartType, operation));
    }

    #endregion

    #endregion
}
