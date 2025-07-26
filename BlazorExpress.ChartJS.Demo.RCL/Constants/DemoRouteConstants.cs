namespace BlazorExpress.ChartJS.Demo.RCL;

public static class DemoRouteConstants
{
    public const string Home_Prefix = "/";

    public const string Blog_Prefix = "/blog";
    public const string Demos_Prefix = "/demos";
    public const string Docs_Prefix = "/docs";

    #region Blog

    public const string Blog_Home = Blog_Prefix + "/home";

    #endregion

    #region Demos

    // Charts
    public const string Demos_BarChart = Demos_Prefix + "/bar-chart";
    public const string Demos_DoughnutChart = Demos_Prefix + "/doughnut-chart";
    public const string Demos_LineChart = Demos_Prefix + "/line-chart";
    public const string Demos_PieChart = Demos_Prefix + "/pie-chart";
    public const string Demos_PolarAreaChart = Demos_Prefix + "/polar-area-chart";
    public const string Demos_RadarChart = Demos_Prefix + "/radar-chart";
    public const string Demos_ScatterChart = Demos_Prefix + "/scatter-chart";

    // Utils
    public const string Demos_Utils_Prefix = Demos_Prefix + "/utils";
    public const string Demos_ColorUtils = Demos_Utils_Prefix + "/color-utility";

    #endregion

    #region Docs

    // Getting Started
    public const string Docs_Getting_Started_Prefix = Docs_Prefix + "/getting-started";
    public const string Docs_Getting_Started_Introduction = Docs_Getting_Started_Prefix + "/introduction";
    public const string Docs_Getting_Started_Blazor_WebAssembly_NET8 = Docs_Getting_Started_Prefix + "/blazor-webassembly-net-8";
    public const string Docs_Getting_Started_Blazor_WebApp_NET_8_Interactive_Render_Mode_Server_Global_Location = Docs_Getting_Started_Prefix + "/blazor-webapp-server-global-net-8";
    public const string Docs_Getting_Started_Blazor_WebApp_NET_8_Interactive_Render_Mode_Auto_Global_Location = Docs_Getting_Started_Prefix + "/blazor-webapp-auto-global-net-8";
    public const string Docs_Getting_Started_MAUI_NET_8 = Docs_Getting_Started_Prefix + "/maui-blazor-net-8";

    // Charts
    public const string Docs_BarChart = Docs_Prefix + "/bar-chart";
    public const string Docs_DoughnutChart = Docs_Prefix + "/doughnut-chart";
    public const string Docs_LineChart = Docs_Prefix + "/line-chart";
    public const string Docs_PieChart = Docs_Prefix + "/pie-chart";
    public const string Docs_PolarAreaChart = Docs_Prefix + "/polar-area-chart";
    public const string Docs_RadarChart = Docs_Prefix + "/radar-chart";
    public const string Docs_ScatterChart = Docs_Prefix + "/scatter-chart";

    // Utils
    public const string Docs_Utils_Prefix = Docs_Prefix + "/utils";
    public const string Docs_ColorUtils = Docs_Utils_Prefix + "/color-utility";

    #endregion
}
