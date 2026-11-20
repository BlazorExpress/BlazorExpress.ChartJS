using Microsoft.JSInterop;

namespace BlazorExpress.ChartJS.Demo.RCL.Pages.Demos.AccessibilitySelfCheck;

/// <summary>
/// Unlisted accessibility self-check (story 26111901, AC-08). The browser walks every routed page in a frame and records
/// pass or fail per page, width, theme and case; this component only supplies the page list and shows the results.
/// </summary>
public partial class AccessibilitySelfCheck : ComponentBase
{
    #region Fields and Constants

    private const string frameHostId = "accessibility-self-check-frames";
    private const string pageUrl = DemoRouteConstants.Demos_AccessibilitySelfCheck;
    private const string progressId = "accessibility-self-check-progress";

    /// <summary>
    /// The routed page components, by area. Chart pages and samples are added from <see cref="SampleCatalog" />.
    /// The two self-checks that walk other pages are loaded with a filter that matches nothing, so the walk does not
    /// start a second walk inside its frame; the page markup checked is the same.
    /// </summary>
    private static readonly (string Area, string Path)[] pages =
    [
        ("home", DemoRouteConstants.Home_Prefix),
        ("other", DemoRouteConstants.Not_Found),
        ("blog", DemoRouteConstants.Blog_Home),
        ("blog", DemoRouteConstants.Blog_Releases),
        ("blog", DemoRouteConstants.Blog_Release_2_0_0),
        ("docs", DemoRouteConstants.Docs_Getting_Started_Introduction),
        ("docs", DemoRouteConstants.Docs_Getting_Started_Blazor_WebAssembly_NET8),
        ("docs", DemoRouteConstants.Docs_Getting_Started_Blazor_WebApp_NET_8_Interactive_Render_Mode_Server_Global_Location),
        ("docs", DemoRouteConstants.Docs_Accessibility),
        ("docs", DemoRouteConstants.Docs_BarChart),
        ("docs", DemoRouteConstants.Docs_BubbleChart),
        ("docs", DemoRouteConstants.Docs_DoughnutChart),
        ("docs", DemoRouteConstants.Docs_LineChart),
        ("docs", DemoRouteConstants.Docs_PieChart),
        ("docs", DemoRouteConstants.Docs_PolarAreaChart),
        ("docs", DemoRouteConstants.Docs_RadarChart),
        ("docs", DemoRouteConstants.Docs_ScatterChart),
        ("demos", DemoRouteConstants.Demos_ColorUtils),
        ("demos", DemoRouteConstants.Demos_Samples),
        ("demos", DemoRouteConstants.Demos_HomeSelfCheck),
        ("demos", DemoRouteConstants.Demos_JsFunctionsSelfCheck),
        ("demos", DemoRouteConstants.Demos_ResizeSelfCheck),
        ("demos", DemoRouteConstants.Demos_SamplesSelfCheck + "?chart=none"),
        ("demos", DemoRouteConstants.Demos_TimeAxisSelfCheck),
        ("demos", pageUrl + "?area=none"),
    ];

    private List<SelfCheckCase> shownCases = [];
    private SelfCheckSummary? summary;

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var expected = new
            {
                frameHostId,
                progressId,
                pages = GetPages(Area, Chart).Select(page => new { area = page.Area, path = page.Path }).ToList(),
                // Opt-in W3C mode (AC-07, Q-06): "1" adds the W3C cases, "only" runs just those. Any other value sends nothing.
                w3c = W3c is "1" or "only" ? W3c : null,
                // Seconds between W3C requests (the script keeps it between 3 and 120).
                w3cPace = Pace,
            };

            try
            {
                var run = await JS.InvokeAsync<SelfCheckRun>("accessibilitySelfCheck.run", expected);
                shownCases = run.Cases;
                summary = new SelfCheckSummary(run.Pages, run.Cases.Count(c => c.Status == "pass"), run.Cases.Count(c => c.Status == "fail"), run.Cases.Count(c => c.Status == "not run"), run.Frames, run.Stalls);
            }
            catch (JSException exception)
            {
                shownCases = [new("-", "The self-check ran to the end", "no script error", exception.Message, "fail")];
                summary = new SelfCheckSummary(0, 0, 1, 0, 0, 0);
            }

            StateHasChanged();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Gets the pages to walk: every routed page, one area (home, docs, demos, samples, blog, other), or one chart's pages.
    /// An unknown area or chart walks no page.
    /// </summary>
    private static IEnumerable<(string Area, string Path)> GetPages(string? area, string? chart)
    {
        var chartPages = SampleCatalog.Charts.Select(c => (Area: "demos", Path: c.Route));
        var samples = SampleCatalog.All.Select(s => (Area: "samples", Path: s.Url));
        var all = pages.Concat(chartPages).Concat(samples).ToList();

        if (!string.IsNullOrWhiteSpace(chart))
            return all.Where(page => page.Path.StartsWith($"{DemoRouteConstants.Demos_Prefix}/{chart}", StringComparison.OrdinalIgnoreCase)
                || page.Path.StartsWith($"{DemoRouteConstants.Docs_Prefix}/{chart}", StringComparison.OrdinalIgnoreCase));

        return string.IsNullOrWhiteSpace(area) ? all : all.Where(page => page.Area.Equals(area, StringComparison.OrdinalIgnoreCase));
    }

    #endregion

    #endregion

    #region Properties, Indexers

    [SupplyParameterFromQuery(Name = "area")] public string? Area { get; set; }

    [SupplyParameterFromQuery(Name = "chart")] public string? Chart { get; set; }

    [Inject] private IJSRuntime JS { get; set; } = default!;

    [SupplyParameterFromQuery(Name = "pace")] public int? Pace { get; set; }

    [SupplyParameterFromQuery(Name = "w3c")] public string? W3c { get; set; }

    #endregion

    #region Nested Types

    private sealed record SelfCheckCase(string Page, string Name, string Expected, string Actual, string Status);

    private sealed record SelfCheckRun(int Pages, int Frames, int Stalls, List<SelfCheckCase> Cases);

    private sealed record SelfCheckSummary(int Pages, int Passed, int Failed, int NotRun, int Frames, int Stalls);

    #endregion
}
