namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The top of a chart page. Without a sample it shows the page's overview content and the chart's sample cards;
/// with a known sample it shows that sample's title, description, tags and share metadata; with an unknown one, "sample not found".
/// </summary>
public partial class SamplePageHeader : ComponentBase
{
    #region Fields and Constants

    private SampleChart? chart;
    private string metaTitle = default!;
    private SampleInfo? sample;

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override void OnParametersSet()
    {
        RedirectOldAnchor();
        chart = SampleCatalog.GetChart(ChartKey);
        sample = SampleCatalog.Find(ChartKey, Sample);
        metaTitle = sample is null || chart is null ? string.Empty : $"{sample.Title} - Blazor {chart.Name} sample";
    }

    #endregion

    #region Private Methods

    // Before 1.3.0 every sample was a section of the chart page, reached by an anchor (/demos/line-chart#gradient-fill).
    // An anchor that names a sample opens that sample's page and replaces the history entry; any other anchor is left alone.
    private void RedirectOldAnchor()
    {
        if (!string.IsNullOrWhiteSpace(Sample))
            return;

        var fragment = new Uri(NavigationManager.Uri).Fragment.TrimStart('#');
        var target = SampleCatalog.Find(ChartKey, Uri.UnescapeDataString(fragment));
        if (target is not null)
            NavigationManager.NavigateTo(target.Url, replace: true);
    }

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>The chart page key. See <see cref="SampleChartKeys" />.</summary>
    [Parameter, EditorRequired] public string ChartKey { get; set; } = default!;

    /// <summary>The overview content: the page's meta tags, hero and links.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    /// <summary>The sample slug from the page URL, or <see langword="null" /> on the overview.</summary>
    [Parameter] public string? Sample { get; set; }

    #endregion
}
