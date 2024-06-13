namespace BlazorExpress.ChartJS.Demo.RCL;

public partial class PageHeroSection : ComponentBase
{
    /// <summary>
    /// Meta Url.
    /// Example: /alerts, /modal
    /// </summary>
    [Parameter] public string? PageUrl { get; set; }

    /// <summary>
    /// Page Title / Meta Title.
    /// </summary>
    [Parameter] public string? Title { get; set; }

    /// <summary>
    /// Page Heading.
    /// </summary>
    [Parameter] public string? Heading { get; set; }

    /// <summary>
    /// Meta Description.
    /// </summary>
    [Parameter] public string? Description { get; set; }

    /// <summary>
    /// Meta Image Url.
    /// </summary>
    [Parameter] public string? ImageUrl { get; set; }
}
