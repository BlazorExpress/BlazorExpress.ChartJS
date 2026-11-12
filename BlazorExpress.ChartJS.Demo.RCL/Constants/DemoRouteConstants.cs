namespace BlazorExpress.ChartJS.Demo.RCL;

public static class DemoRouteConstants
{
    public const string Home_Prefix = "/";

    // The page the router renders for an unknown URL (Router NotFoundPage, .NET 11 template style).
    public const string Not_Found = "/not-found";

    public const string Blog_Prefix = "/blog";
    public const string Demos_Prefix = "/demos";
    public const string Docs_Prefix = "/docs";

    #region Blog

    public const string Blog_Home = Blog_Prefix + "/home";

    // Release notes, one page per release. The wording follows CHANGELOG.md. Versions use dashes, not dots:
    // a last segment such as "2.0.0" is read as a file name, so a direct load or refresh would not reach the app.
    public const string Blog_Releases_Prefix = Blog_Prefix + "/releases";
    public const string Blog_Release_2_0_0 = Blog_Releases_Prefix + "/2-0-0";

    #endregion

    #region Demos

    // Charts
    public const string Demos_BarChart = Demos_Prefix + "/bar-chart";
    public const string Demos_BubbleChart = Demos_Prefix + "/bubble-chart";
    public const string Demos_DoughnutChart = Demos_Prefix + "/doughnut-chart";
    public const string Demos_LineChart = Demos_Prefix + "/line-chart";
    public const string Demos_PieChart = Demos_Prefix + "/pie-chart";
    public const string Demos_PolarAreaChart = Demos_Prefix + "/polar-area-chart";
    public const string Demos_RadarChart = Demos_Prefix + "/radar-chart";
    public const string Demos_ScatterChart = Demos_Prefix + "/scatter-chart";

    // Samples gallery. Query: q (search), chart, category, tag.
    public const string Demos_Samples = Demos_Prefix + "/samples";

    // Unlisted: checks the sample catalog and opens every sample page in turn.
    public const string Demos_SamplesSelfCheck = Demos_Prefix + "/samples-self-check";

    // Unlisted: checks JavaScript function references (ChartFunction) on live charts.
    public const string Demos_JsFunctionsSelfCheck = Demos_Prefix + "/js-functions-self-check";

    // Unlisted: runs the built-in date adapter's checks in the browser's current time zone.
    public const string Demos_TimeAxisSelfCheck = Demos_Prefix + "/time-axis-self-check";

    // Unlisted: checks that charts always end at their container's size, including a resize Chart.js drops at the end of an animation.
    public const string Demos_ResizeSelfCheck = Demos_Prefix + "/resize-self-check";

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
    public const string Docs_BubbleChart = Docs_Prefix + "/bubble-chart";
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

    #region Methods

    #region Public Methods

    /// <summary>The gallery URL, filtered to one category.</summary>
    public static string GetSamplesCategoryUrl(SampleCategory category) => $"{Demos_Samples}?category={category.ToSlug()}";

    /// <summary>The gallery URL, filtered to one tag.</summary>
    public static string GetSamplesTagUrl(string tag) => $"{Demos_Samples}?tag={Uri.EscapeDataString(tag)}";

    /// <summary>The sample page URL, for example <c>/demos/line-chart/gradient-fill</c>.</summary>
    public static string GetSampleUrl(string chartKey, string slug) => $"{Demos_Prefix}/{chartKey}/{slug}";

    #endregion

    #endregion
}
