namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// A gallery card. Set <see cref="Sample" /> for a sample with a page, or <see cref="Unavailable" /> for a disabled card
/// that names the capability the sample waits for.
/// </summary>
public partial class SampleCard : ComponentBase
{
    #region Fields and Constants

    private SampleChart? chart;

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override void OnParametersSet() => chart = SampleCatalog.GetChart(Sample?.ChartKey);

    #endregion

    #endregion

    #region Properties, Indexers

    [Parameter] public SampleInfo? Sample { get; set; }

    [Parameter] public UnavailableSample? Unavailable { get; set; }

    #endregion
}
