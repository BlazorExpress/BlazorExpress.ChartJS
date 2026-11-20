using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// A heading element whose level is a parameter, so a page sets the level its outline needs (story 26111901 AC-02)
/// while the look comes from its classes.
/// </summary>
public sealed class DemoHeading : ComponentBase
{
    #region Methods

    #region Protected Override Methods

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, $"h{Math.Clamp(Level, 1, 6)}");
        builder.AddAttribute(1, "id", string.IsNullOrWhiteSpace(Id) ? null : Id);
        builder.AddAttribute(2, "class", Class);
        builder.AddContent(3, ChildContent);
        builder.CloseElement();
    }

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the heading's content.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the heading's CSS classes.
    /// </summary>
    [Parameter] public string? Class { get; set; }

    /// <summary>
    /// Gets or sets the heading's id.
    /// </summary>
    [Parameter] public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the heading level, from 1 to 6.
    /// </summary>
    [Parameter] public int Level { get; set; } = 2;

    #endregion
}
