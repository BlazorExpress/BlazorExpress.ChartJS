namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The compiled list of every demo sample. The gallery, the sample pages, the sidebar and the samples self-check all read it.
/// Each chart's samples are in their own partial file, in the order the chart page lists them.
/// </summary>
public static partial class SampleCatalog
{
    #region Fields and Constants

    private const string WaitingForCustomPlugins = "Custom plugin registration";
    private const string WaitingForDecimation = "Data decimation plugin options";
    private const string WaitingForScriptableAnimations = "Scriptable animation options";

    private static readonly Lazy<IReadOnlyList<SampleInfo>> all = new(BuildAll);

    private static readonly IReadOnlyList<SampleChart> charts =
    [
        new(SampleChartKeys.BarChart, "Bar chart", DemoRouteConstants.Demos_BarChart, DemoRouteConstants.Docs_BarChart, DemoImageSrcConstants.BarChart, BootstrapIconName.BarChartFill),
        new(SampleChartKeys.BubbleChart, "Bubble chart", DemoRouteConstants.Demos_BubbleChart, DemoRouteConstants.Docs_BubbleChart, DemoImageSrcConstants.BubbleChart, BootstrapIconName.ThreeDotsVertical),
        new(SampleChartKeys.DoughnutChart, "Doughnut chart", DemoRouteConstants.Demos_DoughnutChart, DemoRouteConstants.Docs_DoughnutChart, DemoImageSrcConstants.DoughnutChart, BootstrapIconName.CircleFill),
        new(SampleChartKeys.LineChart, "Line chart", DemoRouteConstants.Demos_LineChart, DemoRouteConstants.Docs_LineChart, DemoImageSrcConstants.LineChart, BootstrapIconName.GraphUp),
        new(SampleChartKeys.PieChart, "Pie chart", DemoRouteConstants.Demos_PieChart, DemoRouteConstants.Docs_PieChart, DemoImageSrcConstants.PieChart, BootstrapIconName.PieChart),
        new(SampleChartKeys.PolarAreaChart, "PolarArea chart", DemoRouteConstants.Demos_PolarAreaChart, DemoRouteConstants.Docs_PolarAreaChart, DemoImageSrcConstants.PolarAreaChart, BootstrapIconName.PieChartFill),
        new(SampleChartKeys.RadarChart, "Radar chart", DemoRouteConstants.Demos_RadarChart, DemoRouteConstants.Docs_RadarChart, DemoImageSrcConstants.RadarChart, BootstrapIconName.Radar),
        new(SampleChartKeys.ScatterChart, "Scatter chart", DemoRouteConstants.Demos_ScatterChart, DemoRouteConstants.Docs_ScatterChart, DemoImageSrcConstants.ScatterChart, BootstrapIconName.GraphUpArrow),
    ];

    // Story 26110206 R-03: these Chart.js samples need a library capability that is not in this release.
    private static readonly IReadOnlyList<UnavailableSample> unavailable =
    [
        new("Chart area border", "Draws a border around the chart area with a custom plugin.", SampleCategory.Advanced, WaitingForCustomPlugins),
        new("Doughnut empty state", "Draws a placeholder ring when a doughnut has no data, with a custom plugin.", SampleCategory.OtherCharts, WaitingForCustomPlugins),
        new("Quadrants", "Colours the four quadrants of a scatter chart with a custom plugin.", SampleCategory.Advanced, WaitingForCustomPlugins),
        new("HTML legend", "Replaces the canvas legend with an HTML list, with a custom plugin.", SampleCategory.Legend, WaitingForCustomPlugins),
        new("Drop", "Drops each point into place with a scriptable animation start value.", SampleCategory.Animations, WaitingForScriptableAnimations),
        new("Progressive line", "Draws a line point by point with scriptable animation delays.", SampleCategory.Animations, WaitingForScriptableAnimations),
        new("Data decimation", "Reduces a large dataset before drawing with the decimation plugin.", SampleCategory.Advanced, WaitingForDecimation),
    ];

    #endregion

    #region Methods

    #region Public Methods

    public static SampleInfo? Find(string? chartKey, string? slug) =>
        string.IsNullOrWhiteSpace(chartKey) || string.IsNullOrWhiteSpace(slug)
            ? null
            : All.FirstOrDefault(s => s.ChartKey == chartKey && string.Equals(s.Slug, slug, StringComparison.OrdinalIgnoreCase));

    public static List<SampleInfo> GetByChart(string? chartKey) =>
        All.Where(s => s.ChartKey == chartKey).ToList();

    public static SampleChart? GetChart(string? chartKey) =>
        charts.FirstOrDefault(c => c.Key == chartKey);

    /// <summary>The next sample on the same chart page, or <see langword="null" /> after the last one.</summary>
    public static SampleInfo? GetNext(SampleInfo sample) => GetNeighbour(sample, 1);

    /// <summary>The previous sample on the same chart page, or <see langword="null" /> before the first one.</summary>
    public static SampleInfo? GetPrevious(SampleInfo sample) => GetNeighbour(sample, -1);

    #endregion

    #region Private Methods

    private static IReadOnlyList<SampleInfo> BuildAll() =>
    [
        .. GetBarChartSamples(),
        .. GetBubbleChartSamples(),
        .. GetDoughnutChartSamples(),
        .. GetLineChartSamples(),
        .. GetPieChartSamples(),
        .. GetPolarAreaChartSamples(),
        .. GetRadarChartSamples(),
        .. GetScatterChartSamples(),
    ];

    private static SampleInfo? GetNeighbour(SampleInfo sample, int offset)
    {
        var samples = GetByChart(sample.ChartKey);
        var index = samples.IndexOf(sample);
        if (index < 0)
            return null;

        index += offset;

        return index >= 0 && index < samples.Count ? samples[index] : null;
    }

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>Every sample, grouped by chart page in page order.</summary>
    public static IReadOnlyList<SampleInfo> All => all.Value;

    public static IReadOnlyList<SampleChart> Charts => charts;

    /// <summary>The Chart.js samples the library cannot express yet, with the capability each waits for.</summary>
    public static IReadOnlyList<UnavailableSample> Unavailable => unavailable;

    #endregion
}
