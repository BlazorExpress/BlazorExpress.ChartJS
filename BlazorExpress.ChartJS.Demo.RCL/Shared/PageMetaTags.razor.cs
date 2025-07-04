namespace BlazorExpress.ChartJS.Demo.RCL;

public partial class PageMetaTags : BulmaComponentBase
{
    private string homeUrl = default!;

    #region Members

    private string siteName => "BlazorExpress Bulma";

    private string title => $"{Title} | BlazorExpress Bulma: An enterprise-grade open-source component library from the Blazor Express team.";

    private string url => $"{homeUrl}{PageUrl}";

    #endregion

    #region Methods

    protected override void OnInitialized()
    {
        homeUrl = $"{Configuration["urls:homeUrl"]}";
    }

    #endregion

    #region Properties

    [Inject] public IConfiguration Configuration { get; set; } = default!;

    /// <summary>
    /// Meta Url.
    /// Example: /alerts, /modal
    /// </summary>
    [Parameter] public string PageUrl { get; set; } = null!;

    /// <summary>
    /// Page Title / Meta Title.
    /// </summary>
    [Parameter] public string Title { get; set; } = null!;

    /// <summary>
    /// Meta Description.
    /// </summary>
    [Parameter] public string Description { get; set; } = null!;

    /// <summary>
    /// Meta Image Url.
    /// </summary>
    [Parameter] public string ImageUrl { get; set; } = null!;

    #endregion
}
