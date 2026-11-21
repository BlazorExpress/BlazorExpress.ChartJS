namespace BlazorExpress.ChartJS.Demo.RCL;

public partial class BlogMainLayout : MainLayoutBase
{
    #region Fields and Constants
    
    private bool isSidebarVisible = false;
    private HashSet<LinkGroup> linkGroups = new();
    private Menu menuRef = default!;

    #endregion

    #region Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await JS.TryInvokeVoidAsync("initializeTheme");

        await base.OnAfterRenderAsync(firstRender);
    }

    protected override void OnInitialized()
    {
        if (!linkGroups.Any())
            linkGroups = GetLinkGroups();

        base.OnInitialized();
    }

    private HashSet<LinkGroup> GetLinkGroups()
    {
        var groups = new HashSet<LinkGroup>();

        // RELEASES: the only group; getting started, charts and utils are in the Docs and Demos menus.
        // One link per release from ReleaseCatalog, newest first. Every link matches its whole address, so 1.0.0 is not
        // also highlighted on 1.0.0-preview1, and 2.0.0 not on its breaking-changes post.
        var links = new List<Link>
        {
            new() { Href = DemoRouteConstants.Blog_Releases, Text = "All releases", Match = MenuItemMatch.All }
        };

        foreach (var release in ReleaseCatalog.Releases)
        {
            links.Add(new Link { Href = release.PageUrl, Text = $"{release.Version} release notes", Match = MenuItemMatch.All });

            if (release.Version == "2.0.0")
                links.Add(new Link { Href = DemoRouteConstants.Blog_Release_2_0_0_Breaking_Changes, Text = "Upgrading to 2.0.0", Match = MenuItemMatch.All });
        }

        groups.Add(new LinkGroup
        {
            Name = "Releases",
            CssClass = "is-size-7 has-text-weight-bold has-text-success",
            Links = [.. links]
        });

        return groups;
    }

    private Task SetAutoTheme() => SetTheme("system");

    private Task SetDarkTheme() => SetTheme("dark");

    private Task SetLightTheme() => SetTheme("light");

    private async Task SetTheme(string themeName) => await JS.TryInvokeVoidAsync("setTheme", themeName);

    private void ToggleSidebarSection()
    {
        @menuRef.Toggle();
    }

    #endregion
}
