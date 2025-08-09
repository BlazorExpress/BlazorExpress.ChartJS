namespace BlazorExpress.ChartJS.Demo.RCL;

public partial class DemosMainLayout : MainLayoutBase
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
            await JS.InvokeVoidAsync("initializeTheme");

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

        // CHARTS
        groups.Add(new LinkGroup
        {
            Name = "Charts",
            CssClass = "is-size-7 has-text-weight-bold has-text-warning",
            Links = [
                new Link { Href = DemoRouteConstants.Demos_BarChart, Text = "Bar chart" },
                new Link { Href = DemoRouteConstants.Demos_BubbleChart, Text = "Bubble chart" },
                new Link { Href = DemoRouteConstants.Demos_DoughnutChart, Text = "Doughnut chart" },
                new Link { Href = DemoRouteConstants.Demos_LineChart, Text = "Line chart" },
                new Link { Href = DemoRouteConstants.Demos_PieChart, Text = "Pie chart" },
                new Link { Href = DemoRouteConstants.Demos_PolarAreaChart, Text = "PolarArea chart" },
                new Link { Href = DemoRouteConstants.Demos_RadarChart, Text = "Radar chart" },
                new Link { Href = DemoRouteConstants.Demos_ScatterChart, Text = "Scatter chart" }
            ]
        });

        // UTILS
        groups.Add(new LinkGroup
        {
            Name = "UTILS",
            CssClass = "is-size-7 has-text-weight-bold has-text-info",
            Links = [
                new Link { Href = DemoRouteConstants.Demos_ColorUtils, Text = "Color Utils" },
            ]
        });

        return groups;
    }

    private Task SetAutoTheme() => SetTheme("system");

    private Task SetDarkTheme() => SetTheme("dark");

    private Task SetLightTheme() => SetTheme("light");

    private async Task SetTheme(string themeName) => await JS.InvokeVoidAsync("setTheme", themeName);

    private void ToggleSidebarSection()
    {
        @menuRef.Toggle();
    }

    #endregion
}
