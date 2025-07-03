using System.ComponentModel;

namespace BlazorExpress.ChartJS.Demo.RCL;

public enum PageLinkCategory
{
    [Description("All")]
    All,

    [Description("Getting Started")]
    GettingStarted,

    [Description("Features")]
    Features,

    [Description("Icons")]
    Icons,

    [Description("Elements")]
    Elements,

    [Description("Form")]
    Form,

    [Description("Components")]
    Components,

    [Description("Layout")]
    Layout
}
