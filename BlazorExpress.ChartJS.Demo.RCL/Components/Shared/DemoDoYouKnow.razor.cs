namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The "Do you know?" panel under each page. It renders the same markup as the BlazorExpress.Bulma.Docx <c>DoYouKnow</c>
/// component, whose class list is the caller's classes followed by <c>mb-4</c>.
/// </summary>
public partial class DemoDoYouKnow : ComponentBase
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets extra CSS classes for the panel.
    /// </summary>
    [Parameter] public string? Class { get; set; }

    private string ClassNames => string.IsNullOrWhiteSpace(Class) ? "mb-4" : $"{Class.Trim()} mb-4";

    #endregion
}
