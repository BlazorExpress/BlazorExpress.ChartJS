namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>A chart page that holds samples.</summary>
/// <param name="Key">The chart page's URL segment. See <see cref="SampleChartKeys" />.</param>
/// <param name="Name">The display name, for example "Line chart".</param>
/// <param name="Route">The chart page route.</param>
/// <param name="DocsRoute">The chart's docs page route.</param>
/// <param name="ImageUrl">The chart's share image.</param>
/// <param name="IconName">The icon a card shows until its thumbnail is drawn.</param>
public sealed record SampleChart(string Key, string Name, string Route, string DocsRoute, string ImageUrl, BootstrapIconName IconName);

/// <summary>The chart page keys. Each one is the chart page's URL segment under <c>/demos</c>.</summary>
public static class SampleChartKeys
{
    #region Fields and Constants

    public const string BarChart = "bar-chart";
    public const string BubbleChart = "bubble-chart";
    public const string DoughnutChart = "doughnut-chart";
    public const string LineChart = "line-chart";
    public const string PieChart = "pie-chart";
    public const string PolarAreaChart = "polar-area-chart";
    public const string RadarChart = "radar-chart";
    public const string ScatterChart = "scatter-chart";

    #endregion
}
