using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Interop;

/// <summary>
/// Checks the input validation of the new methods (rule "chart index and no-chart interop behaviour"): a negative index,
/// an out-of-range quality or a null element list throws before any JavaScript call, even after dispose.
/// </summary>
public class VisibilityValidationTests : BunitContext
{
    #region Fields and Constants

    private static readonly Dictionary<string, Func<ChartComponentBase, Task>> negativeIndexCalls = new()
    {
        ["GetDataVisibilityAsync"] = c => c.GetDataVisibilityAsync(-1),
        ["HideAsync(dataset)"] = c => c.HideAsync(-1),
        ["HideAsync(dataset, data)"] = c => c.HideAsync(0, -1),
        ["IsDatasetVisibleAsync"] = c => c.IsDatasetVisibleAsync(-1),
        ["SetActiveElementsAsync(dataset)"] = c => c.SetActiveElementsAsync([new ChartActiveElement(-1, 0)]),
        ["SetActiveElementsAsync(data)"] = c => c.SetActiveElementsAsync([new ChartActiveElement(0, -1)]),
        ["SetDatasetVisibilityAsync"] = c => c.SetDatasetVisibilityAsync(-1, true),
        ["ShowAsync(dataset)"] = c => c.ShowAsync(-1),
        ["ShowAsync(dataset, data)"] = c => c.ShowAsync(0, -1),
        ["ToggleDataVisibilityAsync"] = c => c.ToggleDataVisibilityAsync(-1)
    };

    #endregion

    #region Methods

    #region Public Methods

    [Theory]
    [MemberData(nameof(NegativeIndexCallNames))]
    public async Task NegativeIndex_AfterDispose_StillThrows(string call)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, "pie");
        await chart.DisposeAsync();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => negativeIndexCalls[call](chart));
    }

    [Theory]
    [MemberData(nameof(NegativeIndexCallNames))]
    public async Task NegativeIndex_ThrowsWithoutJsCall(string call)
    {
        var chart = ChartTestData.RenderChart(this, "bar");
        var invocationsBefore = JSInterop.Invocations.Count;

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => negativeIndexCalls[call](chart));

        Assert.Equal(invocationsBefore, JSInterop.Invocations.Count);
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(1.1)]
    [InlineData(double.NaN)]
    public async Task Quality_OutsideZeroToOne_ThrowsWithoutJsCall(double quality)
    {
        var chart = ChartTestData.RenderChart(this, "bar");

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => chart.ToBase64ImageAsync(ChartImageType.Jpeg, quality));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => chart.DownloadImageAsync(null, ChartImageType.Jpeg, quality));

        Assert.Empty(JSInterop.Invocations);
    }

    [Fact]
    public async Task SetActiveElementsAsync_WithNullElements_Throws()
    {
        var chart = ChartTestData.RenderChart(this, "bar");

        await Assert.ThrowsAsync<ArgumentNullException>(() => chart.SetActiveElementsAsync(null!));
        await Assert.ThrowsAsync<ArgumentNullException>(() => chart.SetActiveElementsAsync([null!]));
    }

    #endregion

    #endregion

    #region Properties, Indexers

    public static TheoryData<string> NegativeIndexCallNames => new(negativeIndexCalls.Keys);

    #endregion
}
