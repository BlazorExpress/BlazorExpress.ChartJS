namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// One sample on a chart page. It renders its content only on that sample's page, so a chart page draws one sample at a time.
/// </summary>
public partial class SampleSection : ComponentBase
{
    #region Properties, Indexers

    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>The sample the page shows, cascaded by the chart page; <see langword="null" /> on the overview.</summary>
    [CascadingParameter] public SampleInfo? CurrentSample { get; set; }

    private bool IsCurrent => CurrentSample is not null && string.Equals(CurrentSample.Slug, Slug, StringComparison.OrdinalIgnoreCase);

    /// <summary>The sample's catalog slug; the section link it had before 1.3.0.</summary>
    [Parameter, EditorRequired] public string Slug { get; set; } = default!;

    #endregion
}
