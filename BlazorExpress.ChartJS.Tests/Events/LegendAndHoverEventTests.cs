using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Events;

/// <summary>
/// Checks the legend-click and hover callbacks and how their options reach the browser.
/// </summary>
public class LegendAndHoverEventTests : BunitContext
{
    #region Methods

    #region Public Methods

    [Fact]
    public async Task HandleHoverAsync_InvokesOnHover()
    {
        var received = new List<ChartHoverEventArgs>();
        var chart = Render<BarChart>(parameters => parameters.Add(p => p.OnHover, (ChartHoverEventArgs e) => received.Add(e))).Instance;

        await chart.HandleHoverAsync(new ChartHoverEventArgs { DatasetIndex = 1, Index = 2, Label = "Mar" });
        await chart.HandleHoverAsync(new ChartHoverEventArgs());

        Assert.Equal(2, received.Count);
        Assert.Equal(1, received[0].DatasetIndex);
        Assert.Null(received[1].DatasetIndex);
    }

    [Fact]
    public async Task HandleLegendClickAsync_InvokesOnLegendClick()
    {
        ChartLegendClickEventArgs? received = null;
        var chart = Render<PieChart>(parameters => parameters.Add(p => p.OnLegendClick, (ChartLegendClickEventArgs e) => received = e)).Instance;

        await chart.HandleLegendClickAsync(new ChartLegendClickEventArgs { Index = 2, Text = "East", Hidden = true });

        Assert.NotNull(received);
        Assert.Equal(2, received.Index);
        Assert.Equal("East", received.Text);
        Assert.True(received.Hidden);
    }

    [Fact]
    public async Task Handlers_WithoutCallbacks_DoNotThrow()
    {
        var chart = Render<BarChart>().Instance;

        await chart.HandleHoverAsync(new ChartHoverEventArgs());
        await chart.HandleLegendClickAsync(new ChartLegendClickEventArgs());
    }

    [Fact]
    public async Task InitializeAsync_SendsBoundCallbacksAndToggleSetting()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = Render<BarChart>(parameters => parameters
            .Add(p => p.LegendClickToggles, false)
            .Add(p => p.OnLegendClick, (ChartLegendClickEventArgs _) => { })
            .Add(p => p.OnHover, (ChartHoverEventArgs _) => { })).Instance;

        await chart.InitializeAsync(ChartTestData.CreateChartData("bar"), ChartTestData.CreateOptions("bar"));

        var eventOptions = ChartTestData.Serialize(JSInterop.Invocations.Last(i => i.Identifier == BarChartInterop.Initialize).Arguments[^1]);
        Assert.True(eventOptions.GetProperty("hasHover").GetBoolean());
        Assert.True(eventOptions.GetProperty("hasLegendClick").GetBoolean());
        Assert.False(eventOptions.GetProperty("legendClickToggles").GetBoolean());
    }

    [Theory]
    [MemberData(nameof(ChartTestData.ChartTypesAndBase), MemberType = typeof(ChartTestData))]
    public async Task InitializeAsync_SendsEventOptionsLast(string chartType)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var chart = ChartTestData.RenderChart(this, chartType);

        await chart.InitializeAsync(ChartTestData.CreateChartData(chartType), ChartTestData.CreateOptions(chartType));

        var invocation = JSInterop.Invocations.Last(i => i.Identifier.EndsWith(".initialize", StringComparison.Ordinal));
        var eventOptions = ChartTestData.Serialize(invocation.Arguments[^1]);
        Assert.False(eventOptions.GetProperty("hasHover").GetBoolean());
        Assert.False(eventOptions.GetProperty("hasLegendClick").GetBoolean());
        Assert.True(eventOptions.GetProperty("legendClickToggles").GetBoolean());
    }

    [Fact]
    public void ParameterChange_AfterFirstRender_SendsEventOptionsOnce()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<BarChart>();
        Assert.DoesNotContain(JSInterop.Invocations, i => i.Identifier == ChartInterop.SetEventOptions);

        cut.Render(parameters => parameters.Add(p => p.LegendClickToggles, false));
        cut.Render(parameters => parameters.Add(p => p.LegendClickToggles, false));

        var invocation = Assert.Single(JSInterop.Invocations, i => i.Identifier == ChartInterop.SetEventOptions);
        Assert.Equal(cut.Instance.Id, invocation.Arguments[0]);
        Assert.False(ChartTestData.Serialize(invocation.Arguments[1]).GetProperty("legendClickToggles").GetBoolean());
    }

    #endregion

    #endregion
}
