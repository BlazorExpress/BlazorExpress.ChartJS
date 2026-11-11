namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// Describes one sample in the demo site: its page, its card in the gallery and its entry in the samples self-check.
/// </summary>
public sealed class SampleInfo
{
    #region Fields and Constants

    /// <summary>The gallery tag that stands for <see cref="IsNewIn200" />. It is not listed in <see cref="Tags" />.</summary>
    public const string NewIn200Tag = "new in 2.0.0";

    #endregion

    #region Methods

    #region Public Methods

    /// <summary>Whether the sample carries the tag, counting <see cref="NewIn200Tag" /> for a sample that is new in 2.0.0.</summary>
    public bool HasTag(string tag) =>
        Tags.Contains(tag, StringComparer.OrdinalIgnoreCase)
        || (IsNewIn200 && string.Equals(tag, NewIn200Tag, StringComparison.OrdinalIgnoreCase));

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>Links to the API tables that document the classes this sample uses.</summary>
    public IReadOnlyList<SampleApiLink> ApiLinks { get; init; } = [];

    public required SampleCategory Category { get; init; }

    /// <summary>The matching Chart.js sample, or <see langword="null" /> when Chart.js has none.</summary>
    public string? ChartJsSampleUrl { get; init; }

    /// <summary>The chart page the sample belongs to, for example <c>line-chart</c>. See <see cref="SampleChartKeys" />.</summary>
    public required string ChartKey { get; init; }

    /// <summary>The first demo component, used for the gallery thumbnail.</summary>
    public Type DemoType => DemoTypes[0];

    /// <summary>The demo components the sample page shows, in page order.</summary>
    public required IReadOnlyList<Type> DemoTypes { get; init; }

    /// <summary>One line shown on the card and in the page's share metadata.</summary>
    public required string Description { get; init; }

    public bool HasPlayground { get; init; }

    public bool IsNewIn200 { get; init; }

    /// <summary>The URL segment of the sample. It is also the anchor the section had on the chart page before 2.0.0.</summary>
    public required string Slug { get; init; }

    public IReadOnlyList<string> Tags { get; init; } = [];

    public required string Title { get; init; }

    public string Url => DemoRouteConstants.GetSampleUrl(ChartKey, Slug);

    /// <summary>Whether a demo references a registered JavaScript function (<c>ChartFunction</c>).</summary>
    public bool UsesJsFunction { get; init; }

    #endregion
}

/// <summary>A link from a sample page to an API table on the chart's docs page.</summary>
public sealed record SampleApiLink(string Text, string Href);
