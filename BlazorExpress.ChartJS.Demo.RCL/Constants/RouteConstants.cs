namespace BlazorExpress.ChartJS.Demo.RCL;

public static class RouteConstants
{
    public const string Blog_Prefix = "/blog";
    public const string Docs_Prefix = "/docs";
    public const string Demos_Prefix = "";

    #region Demos

    // Getting Started
    public const string Demos_GettingStarted_Documentation = Demos_Prefix + "/getting-started";

    public const string Demos_Charts_Prefix = Demos_Prefix + "/charts";
    public const string Demos_BarChart_Documentation = Demos_Charts_Prefix + "/bar-chart";
    public const string Demos_DoughnutChart_Documentation = Demos_Charts_Prefix + "/doughnut-chart";
    public const string Demos_LineChart_Documentation = Demos_Charts_Prefix + "/line-chart";
    public const string Demos_PieChart_Documentation = Demos_Charts_Prefix + "/pie-chart";
    public const string Demos_PolarAreaChart_Documentation = Demos_Charts_Prefix + "/polar-area-chart";
    public const string Demos_RadarChart_Documentation = Demos_Charts_Prefix + "/radar-chart";
    public const string Demos_ScatterChart_Documentation = Demos_Charts_Prefix + "/scatter-chart";

    // Utilities
    public const string Demos_Utils_Prefix = Demos_Prefix + "/utils";
    public const string Demos_ColorUtils_Documentation = Demos_Utils_Prefix + "/color-utility";

    #endregion
}
