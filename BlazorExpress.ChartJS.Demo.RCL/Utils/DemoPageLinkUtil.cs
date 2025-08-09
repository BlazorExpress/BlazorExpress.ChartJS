namespace BlazorExpress.ChartJS.Demo.RCL;

public static class DemoPageLinkUtil
{
    public static HashSet<PageLink> GetDemosLinks()
    {
        var index = 1;
        var links = new HashSet<PageLink>();

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.HouseDoorFill, Href = DemoRouteConstants.Docs_Getting_Started_Prefix, Text = "Getting Started", Categories = new() { DemoPageLinkCategory.All, DemoPageLinkCategory.GettingStarted }, Status = PageLinkStatus.None, IsActive = true, ExcludedFromHomePage = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.BarChartFill, Href = DemoRouteConstants.Demos_BarChart, Text = "Bar Chart", Categories = new() { DemoPageLinkCategory.All, DemoPageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.ThreeDotsVertical, Href = DemoRouteConstants.Demos_BubbleChart, Text = "Bubble Chart", Categories = new() { DemoPageLinkCategory.All, DemoPageLinkCategory.Charts }, Status = PageLinkStatus.New, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.CircleFill, Href = DemoRouteConstants.Demos_DoughnutChart, Text = "Doughnut Chart", Categories = new() { DemoPageLinkCategory.All, DemoPageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.GraphUp, Href = DemoRouteConstants.Demos_LineChart, Text = "Line Chart", Categories = new() { DemoPageLinkCategory.All, DemoPageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.PieChart, Href = DemoRouteConstants.Demos_PieChart, Text = "Pie Chart", Categories = new() { DemoPageLinkCategory.All, DemoPageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.PieChartFill, Href = DemoRouteConstants.Demos_PolarAreaChart, Text = "PolarArea Chart", Categories = new() { DemoPageLinkCategory.All, DemoPageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.Radar, Href = DemoRouteConstants.Demos_RadarChart, Text = "Radar Chart", Categories = new() { DemoPageLinkCategory.All, DemoPageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.GraphUpArrow, Href = DemoRouteConstants.Demos_ScatterChart, Text = "Scatter Chart", Categories = new() { DemoPageLinkCategory.All, DemoPageLinkCategory.Charts }, Status = PageLinkStatus.None, IsActive = true });

        index += 1;
        links.Add(new PageLink { Id = index, IconName = BootstrapIconName.Palette2, Href = DemoRouteConstants.Demos_ColorUtils, Text = "Color Utils", Categories = new() { DemoPageLinkCategory.All, DemoPageLinkCategory.Utils }, Status = PageLinkStatus.None, IsActive = true });

        return links;
    }
}
