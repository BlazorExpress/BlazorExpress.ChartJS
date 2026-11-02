namespace BlazorExpress.ChartJS.Tests;

public class SmokeTests : BunitContext
{
    #region Methods

    #region Public Methods

    [Fact]
    public void BarChart_Renders_Canvas()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = Render<BarChart>();

        Assert.NotNull(cut.Find("canvas"));
    }

    #endregion

    #endregion
}
