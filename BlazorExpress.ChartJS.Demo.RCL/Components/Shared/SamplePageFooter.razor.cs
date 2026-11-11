namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The bottom of a sample page: links to the API tables and the matching Chart.js sample, and the previous and next samples on the same chart page.
/// Renders nothing on a chart overview or for an unknown sample.
/// </summary>
public partial class SamplePageFooter : ComponentBase
{
    #region Fields and Constants

    private SampleChart? chart;
    private SampleInfo? next;
    private SampleInfo? previous;
    private SampleInfo? sample;

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override void OnParametersSet()
    {
        chart = SampleCatalog.GetChart(ChartKey);
        sample = SampleCatalog.Find(ChartKey, Sample);
        previous = sample is null ? null : SampleCatalog.GetPrevious(sample);
        next = sample is null ? null : SampleCatalog.GetNext(sample);
    }

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>The chart page key. See <see cref="SampleChartKeys" />.</summary>
    [Parameter, EditorRequired] public string ChartKey { get; set; } = default!;

    /// <summary>The sample slug from the page URL, or <see langword="null" /> on the overview.</summary>
    [Parameter] public string? Sample { get; set; }

    #endregion
}
