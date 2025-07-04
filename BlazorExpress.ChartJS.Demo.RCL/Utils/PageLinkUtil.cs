namespace BlazorExpress.ChartJS.Demo.RCL;

public static class PageLinkUtil
{
    public static HashSet<PageLink> GetDemosLinks()
    {
        var index = 1;
        var links = new HashSet<PageLink>();

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.None, Href = RouteConstants.Demos_GettingStarted_Route, Text = "Getting Started", Categories = new() { PageLinkCategory.All, PageLinkCategory.Form }, Status = PageLinkStatus.None, IsActive = false });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.Box, Href = RouteConstants.Demos_BarChart_Route, Text = "Bar Chart", Categories = new() { PageLinkCategory.All, PageLinkCategory.Elements }, Status = PageLinkStatus.New, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.PersonSquare, Href = RouteConstants.Demos_DoughnutChart_Route, Text = "Doughnut Chart", Categories = new() { PageLinkCategory.All, PageLinkCategory.Icons }, Status = PageLinkStatus.Updated, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.Box, Href = RouteConstants.Demos_LineChart_Route, Text = "Line Chart", Categories = new() { PageLinkCategory.All, PageLinkCategory.Elements }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.SegmentedNav, Href = RouteConstants.Demos_PieChart_Route, Text = "Pie Chart", Categories = new() { PageLinkCategory.All }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.ToggleOn, Href = RouteConstants.Demos_PolarAreaChart_Route, Text = "PolarArea Chart", Categories = new() { PageLinkCategory.All, PageLinkCategory.Elements }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.None, Href = RouteConstants.Demos_RadarChart_Route, Text = "Radar Chart", Categories = new() { PageLinkCategory.All }, Status = PageLinkStatus.None, IsActive = false });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.None, Href = RouteConstants.Demos_ScatterChart_Route, Text = "Scatter Chart", Categories = new() { PageLinkCategory.All, PageLinkCategory.Form }, Status = PageLinkStatus.None, IsActive = false });

        return links;
    }
}
