namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The top navbar of every demo layout. It matches <c>WebsiteNavbar</c> from BlazorExpress.Bulma.Docx, whose items are fixed,
/// and adds a Releases link to the release list page.
/// </summary>
public partial class DemoNavbar : ComponentBase
{
    #region Fields and Constants

    private const string navbarMenuId = "be-demo-navbar-menu";

    private string githubUrl = default!;
    private bool isNavbarMenuActive;
    private string linkedInUrl = default!;
    private string twitterUrl = default!;
    private string version = default!;

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override void OnInitialized()
    {
        version = $"v{Configuration["version"]}"; // example: v2.0.0
        githubUrl = $"{Configuration["urls:github"]}";
        twitterUrl = $"{Configuration["urls:twitter"]}";
        linkedInUrl = $"{Configuration["urls:linkedin"]}";
    }

    #endregion

    #region Private Methods

    private Task SetAutoTheme() => SetTheme("system");

    private Task SetDarkTheme() => SetTheme("dark");

    private Task SetLightTheme() => SetTheme("light");

    private async Task SetTheme(string themeName) => await JS.TryInvokeVoidAsync("setTheme", themeName);

    private void ToggleNavbarMenu(bool isActive) => isNavbarMenuActive = isActive;

    #endregion

    #endregion

    #region Properties, Indexers

    [Inject] private IConfiguration Configuration { get; set; } = default!;

    [Inject] private IJSRuntime JS { get; set; } = default!;

    /// <summary>The application name shown beside the logo.</summary>
    [Parameter] public string? ApplicationName { get; set; }

    /// <summary>The alternative text of the logo.</summary>
    [Parameter] public string? BrandImgAltText { get; set; }

    /// <summary>The logo image source.</summary>
    [Parameter] public string? BrandImgSrc { get; set; }

    #endregion
}
