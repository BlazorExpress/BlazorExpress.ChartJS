namespace BlazorExpress.ChartJS.Demo.RCL;

public static class PageLinkUtil
{
    public static HashSet<PageLink> GetDemosLinks()
    {
        var index = 1;
        var links = new HashSet<PageLink>();

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.HouseDoorFill, Href = RouteConstants.Docs_GettingStarted, Text = "Getting Started", Categories = new() { PageLinkCategory.All, PageLinkCategory.GettingStarted }, Status = PageLinkStatus.None, IsActive = true, ExcludedFromHomePage = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.BarChartFill, Href = RouteConstants.Demos_BarChart, Text = "Bar Chart", Categories = new() { PageLinkCategory.All, PageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.CircleFill, Href = RouteConstants.Demos_DoughnutChart, Text = "Doughnut Chart", Categories = new() { PageLinkCategory.All, PageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.GraphUp, Href = RouteConstants.Demos_LineChart, Text = "Line Chart", Categories = new() { PageLinkCategory.All, PageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.PieChart, Href = RouteConstants.Demos_PieChart, Text = "Pie Chart", Categories = new() { PageLinkCategory.All, PageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.PieChartFill, Href = RouteConstants.Demos_PolarAreaChart, Text = "PolarArea Chart", Categories = new() { PageLinkCategory.All, PageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.Radar, Href = RouteConstants.Demos_RadarChart, Text = "Radar Chart", Categories = new() { PageLinkCategory.All, PageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.GraphUpArrow, Href = RouteConstants.Demos_ScatterChart, Text = "Scatter Chart", Categories = new() { PageLinkCategory.All, PageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.Palette2, Href = RouteConstants.Demos_ScatterChart, Text = "Color Utils", Categories = new() { PageLinkCategory.All, PageLinkCategory.Utils }, Status = PageLinkStatus.None, IsActive = true });

        return links;
    }
}
