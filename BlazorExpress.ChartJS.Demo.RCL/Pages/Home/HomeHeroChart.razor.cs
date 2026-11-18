namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The home page hero chart: one button per chart type, a live chart of the selected type, its component tag,
/// and a link to that type's samples (story 26111801 AC-03, AC-04, AC-14).
/// </summary>
public partial class HomeHeroChart : ComponentBase
{
    #region Fields and Constants

    private static readonly Dictionary<string, (string Label, string Component)> chartTypes = new()
    {
        [SampleChartKeys.BarChart] = ("Bar", "BarChart"),
        [SampleChartKeys.BubbleChart] = ("Bubble", "BubbleChart"),
        [SampleChartKeys.DoughnutChart] = ("Doughnut", "DoughnutChart"),
        [SampleChartKeys.LineChart] = ("Line", "LineChart"),
        [SampleChartKeys.PieChart] = ("Pie", "PieChart"),
        [SampleChartKeys.PolarAreaChart] = ("Polar area", "PolarAreaChart"),
        [SampleChartKeys.RadarChart] = ("Radar", "RadarChart"),
        [SampleChartKeys.ScatterChart] = ("Scatter", "ScatterChart"),
    };

    private bool isReady;
    private bool reducedMotion;
    private string selectedKey = SampleChartKeys.BarChart;

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // The chart is drawn only once the motion preference is known, so it never animates against the visitor's setting.
            reducedMotion = await GetReducedMotionAsync();
            isReady = true;
            StateHasChanged();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    #endregion

    #region Internal Methods

    /// <summary>The short button label of a chart type, for example "Polar area".</summary>
    internal static string GetLabel(string chartKey) =>
        chartTypes.TryGetValue(chartKey, out var type) ? type.Label : chartKey;

    #endregion

    #region Private Methods

    private async Task<bool> GetReducedMotionAsync() =>
        ReducedMotion ?? await JS.TryInvokeAsync("demoPrefersReducedMotion", false);

    private async Task SelectAsync(string chartKey)
    {
        // The new key re-creates the chart view at once, so quick clicks always end on the last type chosen (AC-04).
        selectedKey = chartKey;

        // A motion setting changed during the visit applies from the next chart drawn (finalize A-04).
        reducedMotion = await GetReducedMotionAsync();
    }

    #endregion

    #endregion

    #region Properties, Indexers

    private string Excerpt
    {
        get
        {
            var component = chartTypes[selectedKey].Component;
            return $"<{component} @ref=\"{char.ToLowerInvariant(component[0])}{component[1..]}\" />";
        }
    }

    [Inject] private IJSRuntime JS { get; set; } = default!;

    /// <summary>Overrides the visitor's reduced-motion setting; used by the home self-check page. Null reads the browser setting.</summary>
    [Parameter] public bool? ReducedMotion { get; set; }

    private string SamplesLinkText =>
        $"Open {SampleCatalog.GetByChart(selectedKey).Count} {GetLabel(selectedKey).ToLowerInvariant()} chart samples";

    private SampleChart SelectedChart => SampleCatalog.GetChart(selectedKey)!;

    #endregion
}
