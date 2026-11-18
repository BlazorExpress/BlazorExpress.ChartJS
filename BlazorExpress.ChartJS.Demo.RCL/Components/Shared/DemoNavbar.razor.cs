using Microsoft.AspNetCore.Components.Routing;

namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The top navbar of every demo layout. It matches <c>WebsiteNavbar</c> from BlazorExpress.Bulma.Docx, whose items are fixed,
/// and adds a Releases link to the release list page.
/// It marks the section of the current address: Docs for /docs, Demos for /demos and Releases for /blog (story 26111801 AC-22).
/// </summary>
public partial class DemoNavbar : ComponentBase, IDisposable
{
    #region Fields and Constants

    private const string currentSectionClass = "is-current";
    private const string navbarMenuId = "be-demo-navbar-menu";

    private string currentPath = string.Empty;
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

        currentPath = GetPath(Navigation.Uri);
        Navigation.LocationChanged += OnLocationChanged;
    }

    #endregion

    #region Public Methods

    public void Dispose() => Navigation.LocationChanged -= OnLocationChanged;

    #endregion

    #region Private Methods

    // "page" on the item's own address, "true" anywhere else in its section, and no attribute outside it.
    private string? GetAriaCurrent(string sectionPrefix, string itemHref)
    {
        if (!IsInSection(sectionPrefix))
            return null;

        return currentPath == itemHref.Trim('/') ? "page" : "true";
    }

    // The address as a path without the base, query or fragment, for example "docs/getting-started/introduction".
    private string GetPath(string uri)
    {
        var relative = Navigation.ToBaseRelativePath(uri);
        var end = relative.IndexOfAny(['?', '#']);
        return (end >= 0 ? relative[..end] : relative).Trim('/');
    }

    private string GetSectionClass(string sectionPrefix) =>
        IsInSection(sectionPrefix) ? $"mr-1 {currentSectionClass}" : "mr-1";

    private bool IsInSection(string sectionPrefix)
    {
        var section = sectionPrefix.Trim('/');
        return currentPath == section || currentPath.StartsWith($"{section}/", StringComparison.Ordinal);
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        currentPath = GetPath(e.Location);
        StateHasChanged();
    }

    private Task SetAutoTheme() => SetTheme("system");

    private Task SetDarkTheme() => SetTheme("dark");

    private Task SetLightTheme() => SetTheme("light");

    private async Task SetTheme(string themeName) => await JS.TryInvokeVoidAsync("setTheme", themeName);

    private void ToggleNavbarMenu(bool isActive) => isNavbarMenuActive = isActive;

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>The application name shown beside the logo.</summary>
    [Parameter] public string? ApplicationName { get; set; }

    /// <summary>The alternative text of the logo.</summary>
    [Parameter] public string? BrandImgAltText { get; set; }

    /// <summary>The logo image source.</summary>
    [Parameter] public string? BrandImgSrc { get; set; }

    [Inject] private IConfiguration Configuration { get; set; } = default!;

    [Inject] private IJSRuntime JS { get; set; } = default!;

    [Inject] private NavigationManager Navigation { get; set; } = default!;

    #endregion
}
