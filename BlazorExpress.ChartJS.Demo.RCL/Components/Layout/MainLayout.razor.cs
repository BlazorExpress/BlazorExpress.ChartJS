namespace BlazorExpress.ChartJS.Demo.RCL;

public partial class MainLayout : MainLayoutBase
{
    internal override IEnumerable<NavItem> GetNavItems()
    {
        navItems ??= new List<NavItem>
        {
            new (){ Id = "1", Text = "Getting Started", Href = "/getting-started", IconName = IconName.HouseDoorFill },

            new (){ Id = "2", Text = "Bar Chart", IconName = IconName.BarChartFill },
            new (){ Id = "200", Text = "API", Href = "/charts/bar-chart", IconName = IconName.BarChartFill, ParentId = "2", Match = NavLinkMatch.All },

            new (){ Id = "3", Text = "Doughnut Chart", IconName = IconName.CircleFill, },
            new (){ Id = "300", Text = "API", Href = "/charts/doughnut-chart", IconName = IconName.CircleFill, ParentId = "3", Match = NavLinkMatch.All },

            new (){ Id = "4", Text = "Line Chart", IconName = IconName.GraphUp },
            new (){ Id = "400", Text = "API", Href = "/charts/line-chart", IconName = IconName.GraphUp, ParentId = "4", Match = NavLinkMatch.All },

            new (){ Id = "5", Text = "Pie Chart", IconName = IconName.PieChartFill },
            new (){ Id = "500", Text = "API", Href = "/charts/pie-chart", IconName = IconName.PieChartFill, ParentId = "5", Match = NavLinkMatch.All },

        };

        return navItems;
    }
}
