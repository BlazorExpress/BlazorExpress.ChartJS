using Microsoft.JSInterop;

namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// A documentation section with an anchored heading, as the BlazorExpress.Bulma.Docx <c>Section</c> renders it, except that
/// the heading's level and its look are separate: <see cref="Level" /> sets the element (h2 to h6) and <see cref="Size" />
/// sets the Bulma title size. As in the package, the # link scrolls to the heading through <c>navigateToHeading</c>.
/// </summary>
public partial class DemoSection : ComponentBase
{
    #region Methods

    #region Private Methods

    private async Task OnClick()
    {
        try
        {
            await JSRuntime.InvokeVoidAsync("navigateToHeading");
        }
        catch (JSException)
        {
            // Without the script the link still moves to its fragment as a plain link does.
        }
    }

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets or sets attributes for the section element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Gets or sets the section's content.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets extra CSS classes for the section element.
    /// </summary>
    [Parameter] public string? Class { get; set; }

    // The package Section's default classes, its mobile modifier included.
    private string ClassNames => string.Join(' ', new[] { Class?.Trim(), "section", "is-mobile" }.Where(c => !string.IsNullOrWhiteSpace(c)));

    private string headerClassNames => $"{Size.ToTitleSizeClass()} mt-4 mb-3";

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    /// <summary>
    /// Gets or sets the heading level, from 2 to 6, that the page outline needs. Default value is 2.
    /// </summary>
    [Parameter] public int Level { get; set; } = 2;

    private string link => $"{PageUrl}#{Link}".Trim().ToLower();

    /// <summary>
    /// Gets or sets the heading's anchor, which is also its id.
    /// </summary>
    [Parameter] public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the heading text.
    /// </summary>
    [Parameter] public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the page address the anchor link starts with.
    /// </summary>
    [Parameter] public string? PageUrl { get; set; }

    /// <summary>
    /// Gets or sets the heading's look (a Bulma title size), independent of its level. Default value is <see cref="HeadingSize.H4" />.
    /// </summary>
    [Parameter] public HeadingSize Size { get; set; } = HeadingSize.H4;

    #endregion
}
