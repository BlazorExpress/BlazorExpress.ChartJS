using System.Reflection;
using System.Text.RegularExpressions;
using IComponent = Microsoft.AspNetCore.Components.IComponent;

namespace BlazorExpress.ChartJS.Demo.RCL.Pages.Demos.SamplesSelfCheck;

/// <summary>
/// Unlisted page (story 26110206 F-01). It checks the sample catalog against the demo components and their embedded sources,
/// then opens every sample page in a frame and records the charts drawn and the console errors.
/// </summary>
public partial class SamplesSelfCheck : ComponentBase, IDisposable
{
    #region Fields and Constants

    private const string DemosNamespacePrefix = "BlazorExpress.ChartJS.Demo.RCL.Pages.Demos.";
    private const string Fail = "fail";
    private const string pageUrl = DemoRouteConstants.Demos_SamplesSelfCheck;
    private const string Pass = "pass";
    private const string Resized = "resized";

    // The section links the eight chart pages had before the samples moved to their own pages (code repository commit b877743).
    // Each one must stay a sample slug, so old anchors keep resolving.
    private static readonly Dictionary<string, string[]> preMoveSlugs = new()
    {
        [SampleChartKeys.BarChart] = ["how-it-works", "click-event", "combo-bar-line", "horizontal-bar-chart", "stacked-bar-chart", "stacked-bar-chart-with-groups", "stacked-bar-chart-with-data-labels", "locale", "border-radius", "animations-delay", "animations-dataset-level-delay", "export-image", "show-and-hide-datasets", "legend-click-event", "hover-event", "programmatic-highlight", "update-modes-and-animation-control", "multiple-axes", "axis-position-and-reverse", "tick-options", "axis-title", "grace-and-bounds", "floating-bars", "per-corner-border-radius", "base-and-inflate-amount", "title-and-subtitle", "tooltip-options", "layout-padding", "aspect-ratio-and-resize-delay", "hover-and-elements", "colors-plugin", "currency-ticks", "legend-click-function"],
        [SampleChartKeys.BubbleChart] = ["how-it-works", "click-event", "export-image", "show-and-hide-datasets", "legend-click-event", "hover-event", "programmatic-highlight", "update-modes-and-animation-control", "multiple-axes", "axis-position-and-reverse", "tick-options", "axis-title", "grace-and-bounds", "title-and-subtitle", "tooltip-options", "layout-padding", "aspect-ratio-and-resize-delay", "hover-and-elements", "colors-plugin"],
        [SampleChartKeys.DoughnutChart] = ["how-it-works", "data-labels", "click-event", "export-image", "show-and-hide-slices", "legend-click-event", "hover-event", "programmatic-highlight", "update-modes-and-animation-control", "cutout-and-radius", "half-gauge", "rotate-and-scale-animation", "spacing-mode", "title-and-subtitle", "tooltip-options", "layout-padding", "aspect-ratio-and-resize-delay", "hover-and-elements", "colors-plugin"],
        [SampleChartKeys.LineChart] = ["how-it-works", "combo-bar-line", "stacked-line-chart", "data-labels", "tick-configuration", "locale", "interpolation-modes", "click-event", "export-image", "show-and-hide-datasets", "legend-click-event", "hover-event", "programmatic-highlight", "update-modes-and-animation-control", "multiple-axes", "axis-position-and-reverse", "tick-options", "axis-title", "grace-and-bounds", "span-gaps", "clip", "filler-options", "hover-line", "title-and-subtitle", "tooltip-options", "layout-padding", "aspect-ratio-and-resize-delay", "hover-and-elements", "colors-plugin", "time-axis", "time-series", "time-combo-bar-line", "time-sensor-readings", "time-live-streaming", "time-max-span", "time-localized-formats", "tick-and-tooltip-functions", "legend-filter-function", "segment-styling", "gradient-fill"],
        [SampleChartKeys.PieChart] = ["how-it-works", "data-labels", "change-legend-position", "click-event", "show-and-hide-slices", "legend-click-event", "export-image", "hover-event", "programmatic-highlight", "update-modes-and-animation-control", "cutout-and-radius", "half-gauge", "rotate-and-scale-animation", "spacing-mode", "title-and-subtitle", "tooltip-options", "layout-padding", "aspect-ratio-and-resize-delay", "hover-and-elements", "colors-plugin"],
        [SampleChartKeys.PolarAreaChart] = ["how-it-works", "click-event", "export-image", "show-and-hide-slices", "legend-click-event", "hover-event", "programmatic-highlight", "update-modes-and-animation-control", "radial-scale", "angle-lines-and-point-labels", "start-angle", "spacing-mode", "title-and-subtitle", "tooltip-options", "layout-padding", "aspect-ratio-and-resize-delay", "hover-and-elements", "colors-plugin"],
        [SampleChartKeys.RadarChart] = ["how-it-works", "click-event", "export-image", "show-and-hide-datasets", "legend-click-event", "hover-event", "programmatic-highlight", "update-modes-and-animation-control", "radial-scale", "angle-lines-and-point-labels", "start-angle", "title-and-subtitle", "tooltip-options", "layout-padding", "aspect-ratio-and-resize-delay", "hover-and-elements", "colors-plugin"],
        [SampleChartKeys.ScatterChart] = ["how-it-works", "dynamic-data", "click-event", "export-image", "show-and-hide-datasets", "legend-click-event", "hover-event", "programmatic-highlight", "update-modes-and-animation-control", "multiple-axes", "axis-position-and-reverse", "tick-options", "axis-title", "grace-and-bounds", "title-and-subtitle", "tooltip-options", "layout-padding", "aspect-ratio-and-resize-delay", "hover-and-elements", "colors-plugin", "time-sensor-readings"],
    };

    private readonly List<SelfCheckCase> cases = new();
    private DotNetObjectReference<SamplesSelfCheck>? dotNetRef;
    private ElementReference frame;
    private bool walkDone;
    private readonly List<SampleWalkResult> walkResults = new();
    private int walkTotal;

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;

        cases.AddRange(RunCatalogCases());
        StateHasChanged();

        var charts = SampleCatalog.Charts
            .Where(c => string.IsNullOrWhiteSpace(Chart) || c.Key == Chart)
            .Select(c => new
            {
                key = c.Key,
                route = c.Route,
                count = SampleCatalog.GetByChart(c.Key).Count,
                // A section link the chart page had before the move, so the case follows a real old anchor.
                anchor = preMoveSlugs[c.Key][^1],
            })
            .ToList();
        var samples = SampleCatalog.All
            .Where(s => string.IsNullOrWhiteSpace(Chart) || s.ChartKey == Chart)
            .Select(s => new { url = s.Url })
            .ToList();
        walkTotal = samples.Count;

        if (!string.IsNullOrWhiteSpace(Chart) && samples.Count == 0)
            cases.Add(Check($"Samples found for ?chart={Chart}", "1 or more", "0"));

        try
        {
            // Routing, old anchors, the Colors plugin and the injection cases run in the frame first.
            // The Bar sample: bar datasets send no colour of their own, so the palette shows without ForceOverride.
            var colorsUrl = SampleCatalog.Find(SampleChartKeys.BarChart, "colors-plugin")?.Url;
            var frameCases = await JS.InvokeAsync<List<SelfCheckCase>>("samplesSelfCheck.runFrameCases", frame, charts, DemoRouteConstants.Demos_Samples, colorsUrl);
            cases.AddRange(frameCases);
            StateHasChanged();

            dotNetRef = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("samplesSelfCheck.walk", frame, samples, dotNetRef);
        }
        catch (JSException exception)
        {
            // The frame did not start, or the script stopped: that is a failure, not an empty result.
            cases.Add(new SelfCheckCase("The frame cases and the sample walk ran to the end", "no script error", exception.Message, Fail));
            StateHasChanged();
        }
    }

    #endregion

    #region Public Methods

    public void Dispose() => dotNetRef?.Dispose();

    [JSInvokable]
    public void OnSampleChecked(SampleWalkResult result)
    {
        walkResults.Add(result);
        StateHasChanged();
    }

    [JSInvokable]
    public void OnWalkDone()
    {
        walkDone = true;
        StateHasChanged();
    }

    #endregion

    #region Private Methods

    private static SelfCheckCase Check(string name, string expected, string actual) =>
        new(name, expected, actual, expected == actual ? Pass : Fail);

    private static string Describe(IEnumerable<string> items)
    {
        var list = items.ToList();

        return list.Count == 0 ? "none" : $"{list.Count}: {string.Join(", ", list.Take(10))}{(list.Count > 10 ? ", …" : string.Empty)}";
    }

    // Reads the demo sources embedded in the assembly and reports each one that contains a component tag
    // from the demo site's own UI packages.
    private static IEnumerable<string> FindSiteComponentTags(Assembly assembly)
    {
        var componentNames = new[] { typeof(Button).Assembly, typeof(global::BlazorExpress.Bulma.Docx.Demo).Assembly }
            .SelectMany(a => a.GetExportedTypes())
            .Where(t => typeof(IComponent).IsAssignableFrom(t) && !t.IsAbstract)
            .Select(t => t.Name.Split('`')[0])
            .Distinct()
            .ToList();
        var tag = new Regex($@"<(?<name>{string.Join("|", componentNames.Select(Regex.Escape))})[\s/>]", RegexOptions.CultureInvariant);

        foreach (var resourceName in assembly.GetManifestResourceNames().Order())
        {
            if (!resourceName.StartsWith(DemosNamespacePrefix, StringComparison.Ordinal)
                || !resourceName.Contains("_Demo_", StringComparison.Ordinal)
                || !resourceName.EndsWith(".razor", StringComparison.Ordinal))
                continue;

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is null)
                continue;

            using var reader = new StreamReader(stream);
            var names = tag.Matches(reader.ReadToEnd()).Select(m => m.Groups["name"].Value).Distinct().ToList();
            if (names.Count > 0)
                yield return $"{resourceName[DemosNamespacePrefix.Length..]} ({string.Join(", ", names)})";
        }
    }

    private static List<SelfCheckCase> RunCatalogCases()
    {
        var assembly = typeof(App).Assembly;
        var all = SampleCatalog.All;

        // Demo components: every *_Demo_* and *_Playground component in a chart folder under Pages/Demos.
        var demoTypes = assembly.GetTypes()
            .Where(t => typeof(IComponent).IsAssignableFrom(t)
                        && (t.Namespace ?? string.Empty).StartsWith(DemosNamespacePrefix, StringComparison.Ordinal)
                        && t.Namespace!.EndsWith("Chart", StringComparison.Ordinal)
                        && (t.Name.Contains("_Demo_", StringComparison.Ordinal) || t.Name.EndsWith("_Playground", StringComparison.Ordinal)))
            .ToList();
        var uses = all.SelectMany(s => s.DemoTypes).GroupBy(t => t).ToDictionary(g => g.Key, g => g.Count());

        // The source of a demo is embedded under its type name. A demo with no embedded source would not be scanned below,
        // and its page would have nothing to show on the source tab.
        var resources = assembly.GetManifestResourceNames().ToHashSet(StringComparer.Ordinal);
        var demosWithoutSource = demoTypes
            .Where(t => t.Name.Contains("_Demo_", StringComparison.Ordinal) && !resources.Contains($"{t.FullName}.razor"))
            .Select(t => t.Name);

        var result = new List<SelfCheckCase>
        {
            Check("Demo components with no catalog entry", "none", Describe(demoTypes.Where(t => !uses.ContainsKey(t)).Select(t => t.Name))),
            Check("Demo components with more than one catalog entry", "none", Describe(uses.Where(u => u.Value > 1).Select(u => u.Key.Name))),
            Check("Catalog demo types that are not demo components", "none", Describe(uses.Keys.Where(t => !demoTypes.Contains(t)).Select(t => t.Name))),
            Check("Slugs used twice on one chart page", "none", Describe(all.GroupBy(s => $"{s.ChartKey}/{s.Slug}").Where(g => g.Count() > 1).Select(g => g.Key))),
            Check("Pre-move section links that are no longer a slug", "none", Describe(preMoveSlugs.SelectMany(c => c.Value.Where(slug => SampleCatalog.Find(c.Key, slug) is null).Select(slug => $"{c.Key}/{slug}")))),
            Check("Samples on an unknown chart page", "none", Describe(all.Where(s => SampleCatalog.GetChart(s.ChartKey) is null).Select(s => s.Url))),
            Check("Samples with no description or no tag", "none", Describe(all.Where(s => string.IsNullOrWhiteSpace(s.Description) || s.Tags.Count == 0).Select(s => s.Url))),
            Check("Not-yet-available samples", "7", SampleCatalog.Unavailable.Count.ToString()),
            Check("Demo components with no embedded source", "none", Describe(demosWithoutSource)),
            Check("Demo sources that use a site UI component (rules.md: framework-neutral demo sources)", "none", Describe(FindSiteComponentTags(assembly))),
        };

        return result;
    }

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>Optional: walk one chart page only, for example <c>?chart=line-chart</c>.</summary>
    [SupplyParameterFromQuery(Name = "chart")] public string? Chart { get; set; }

    [Inject] private IJSRuntime JS { get; set; } = default!;

    #endregion

    #region Nested Types

    public sealed record SampleWalkResult(string Url, int Charts, int ZeroSize, int Resized, List<string> Notes, List<string> Errors, string Status);

    public sealed record SelfCheckCase(string Name, string Expected, string Actual, string Status);

    #endregion
}
