namespace BlazorExpress.ChartJS.Demo.RCL;

public partial class MainLayout : MainLayoutBase
{
    internal override IEnumerable<NavItem> GetNavItems()
    {
        navItems ??= new List<NavItem>
        {
            new (){ Id = "1", Text = "Overview", Href = "/charts/overview", IconName = IconName.HouseDoorFill },
            
            new (){ Id = "2", Text = "Getting Started", IconName = IconName.Palette2 },
            new (){ Id = "201", Text = "Blazor WebAssembly (.NET 8)", Href = "/charts/getting-started/blazor-webassembly-net-8", IconName = IconName.Palette2, ParentId = "2", Match = NavLinkMatch.All },
            new (){ Id = "202", Text = "Blazor WebApp Server (.NET 8)", Href = "/charts/getting-started/blazor-webapp-server-global-net-8", IconName = IconName.Palette2, ParentId = "2", Match = NavLinkMatch.All },
            new (){ Id = "202", Text = "Blazor WebApp Auto (.NET 8)", Href = "/charts/getting-started/blazor-webapp-auto-global-net-8", IconName = IconName.Palette2, ParentId = "2", Match = NavLinkMatch.All },

            new (){ Id = "3", Text = "Bar Chart", IconName = IconName.BarChartLineFill },
            new (){ Id = "301", Text = "Bar", Href = "/charts/bar-chart", IconName = IconName.BarChartLine, ParentId = "3", Match = NavLinkMatch.All },
            new (){ Id = "302", Text = "Horizontal", Href = "/charts/bar-chart/horizontal", IconName = IconName.BarChartLine, ParentId = "3", },
            new (){ Id = "303", Text = "Stacked", Href = "/charts/bar-chart/stacked", IconName = IconName.BarChartLine, ParentId = "3", },
            new (){ Id = "304", Text = "Data labels", Href = "/charts/bar-chart/data-labels", IconName = IconName.BarChartLine, ParentId = "3", },
            new (){ Id = "305", Text = "Locale", Href = "/charts/bar-chart/locale", IconName = IconName.BarChartLine, ParentId = "3", },
            new (){ Id = "305", Text = "API Documentation", Href = "/charts/bar-chart/api-documentation", IconName = IconName.FileTextFill, ParentId = "3", },

            new (){ Id = "4", Text = "Doughnut Chart", IconName = IconName.CircleFill, },
            new (){ Id = "401", Text = "Doughnut", Href = "/charts/doughnut-chart", IconName = IconName.Circle, ParentId = "4", Match = NavLinkMatch.All },
            new (){ Id = "402", Text = "Data labels", Href = "/charts/doughnut-chart/doughnut-labels", IconName = IconName.Circle, ParentId = "4", Match = NavLinkMatch.All },

            new (){ Id = "5", Text = "Line Chart", IconName = IconName.GraphUpArrow },
            new (){ Id = "500", Text = "Line", Href = "/charts/line-chart", IconName = IconName.GraphUp, ParentId = "5", Match = NavLinkMatch.All },
            new (){ Id = "500", Text = "Data labels", Href = "/charts/line-chart/data-labels", IconName = IconName.GraphUp, ParentId = "5", Match = NavLinkMatch.All },
            new (){ Id = "500", Text = "Tick configuration", Href = "/charts/line-chart/tick-configuration", IconName = IconName.GraphUp, ParentId = "5", Match = NavLinkMatch.All },
            new (){ Id = "500", Text = "Locale", Href = "/charts/line-chart/locale", IconName = IconName.GraphUp, ParentId = "5", Match = NavLinkMatch.All },

            new (){ Id = "6", Text = "Pie Chart", IconName = IconName.PieChartFill },
            new (){ Id = "600", Text = "Pie", Href = "/charts/pie-chart", IconName = IconName.PieChart, ParentId = "6", Match = NavLinkMatch.All },
            new (){ Id = "600", Text = "Data labels", Href = "/charts/pie-chart/data-labels", IconName = IconName.PieChart, ParentId = "6", Match = NavLinkMatch.All },
            new (){ Id = "600", Text = "Legend position", Href = "/charts/pie-chart/legend", IconName = IconName.PieChart, ParentId = "6", Match = NavLinkMatch.All },
        };

        return navItems;
    }
}
