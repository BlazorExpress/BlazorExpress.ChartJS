namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// Writes a page's title and head metadata: description, canonical URL, Open Graph and Twitter tags, and optionally robots.
/// The title becomes "<c>Title</c> | BlazorExpress.ChartJS". Once the tags are on the page, the site-wide defaults in
/// <c>wwwroot/index.html</c> (marked <c>data-site-default</c>) are removed, so each page has one set of tags.
/// </summary>
public partial class SiteMetaTags : ComponentBase
{
    #region Fields and Constants

    private const string DefaultImageUrl = DemoImageSrcConstants.LineChart;

    private string absoluteImageUrl = default!;
    private string absoluteUrl = default!;
    private string description = default!;
    private string fullTitle = default!;

    #endregion

    #region Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await JS.InvokeVoidAsync("removeSiteDefaultHeadTags");

        await base.OnAfterRenderAsync(firstRender);
    }

    protected override void OnParametersSet()
    {
        fullTitle = string.IsNullOrWhiteSpace(Title) ? DemoStringConstants.SiteDefaultTitle : Title + DemoStringConstants.PageTitle_Suffix;
        description = string.IsNullOrWhiteSpace(Description) ? DemoStringConstants.SiteDefaultDescription : Description;
        absoluteUrl = ToAbsoluteUrl(PageUrl);
        absoluteImageUrl = ToAbsoluteUrl(string.IsNullOrWhiteSpace(ImageUrl) ? DefaultImageUrl : ImageUrl);
    }

    private static string ToAbsoluteUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return DemoStringConstants.SiteUrl + "/";

        if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            return url;

        return DemoStringConstants.SiteUrl + "/" + url.TrimStart('/');
    }

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the page description, used for the description, Open Graph and Twitter tags.
    /// Default is <see cref="DemoStringConstants.SiteDefaultDescription" />.
    /// </summary>
    [Parameter] public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the preview image, as an absolute URL or a path on this site. Default is the line chart image.
    /// </summary>
    [Parameter] public string? ImageUrl { get; set; }

    [Inject] private IJSRuntime JS { get; set; } = default!;

    /// <summary>
    /// Gets or sets the page path, for example <c>/docs/bar-chart</c>. The canonical and Open Graph URLs are built on <see cref="DemoStringConstants.SiteUrl" />.
    /// </summary>
    [Parameter] public string? PageUrl { get; set; }

    /// <summary>
    /// Gets or sets the robots directive, for example <c>noindex, nofollow</c>. Default is none (indexable).
    /// </summary>
    [Parameter] public string? Robots { get; set; }

    /// <summary>
    /// Gets or sets the page topic, without the site name, for example <c>Blazor Bar Chart API Reference</c>.
    /// Keep it under about 40 characters so the full title fits in search results.
    /// </summary>
    [Parameter] public string? Title { get; set; }

    #endregion
}
