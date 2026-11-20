namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The page title block: an h1, a rule, and an optional subtitle shown as lead text.
/// </summary>
public partial class DemoPageHero : ComponentBase
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the subtitle text. Ignored when <see cref="SubTitleTemplate" /> is set.
    /// </summary>
    [Parameter] public string? SubTitle { get; set; }

    /// <summary>
    /// Gets or sets the subtitle content.
    /// </summary>
    [Parameter] public RenderFragment? SubTitleTemplate { get; set; }

    /// <summary>
    /// Gets or sets the title text.
    /// </summary>
    [Parameter] public string? Title { get; set; }

    #endregion
}
