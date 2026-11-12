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
        groups.Add(new LinkGroup
        {
            Name = "Releases",
            CssClass = "is-size-7 has-text-weight-bold has-text-success",
            Links = [
                new Link { Href = DemoRouteConstants.Blog_Releases, Text = "All releases", Match = MenuItemMatch.All },
                new Link { Href = DemoRouteConstants.Blog_Release_2_0_0, Text = "2.0.0 release notes" }
            ]
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
