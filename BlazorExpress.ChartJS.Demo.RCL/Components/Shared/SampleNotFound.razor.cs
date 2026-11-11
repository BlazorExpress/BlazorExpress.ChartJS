namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>Shown for a chart page URL whose sample slug is not in the catalog, with that chart's sample cards.</summary>
public partial class SampleNotFound : ComponentBase
{
    #region Fields and Constants

    private SampleChart? chart;

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override void OnParametersSet() => chart = SampleCatalog.GetChart(ChartKey);

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>The chart page key. See <see cref="SampleChartKeys" />.</summary>
    [Parameter, EditorRequired] public string ChartKey { get; set; } = default!;

    /// <summary>The slug from the URL that matched no sample.</summary>
    [Parameter] public string? Slug { get; set; }

    #endregion
}
