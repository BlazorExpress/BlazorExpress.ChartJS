using BlazorExpress.ChartJS.Demo.RCL.Pages.Demos.BubbleChart;

namespace BlazorExpress.ChartJS.Demo.RCL;

public static partial class SampleCatalog
{
    #region Methods

    #region Private Methods

    // One entry per sample section on the Bubble chart page, in page order.
    private static List<SampleInfo> GetBubbleChartSamples()
    {
        const string ChartKey = SampleChartKeys.BubbleChart;
        static SampleApiLink Api(string text, string anchor) => new(text, $"{DemoRouteConstants.Docs_BubbleChart}#{anchor}");

        return
        [
            new()
            {
                ChartKey = ChartKey,
                Slug = "how-it-works",
                Title = "How it works",
                Description = "The Bubble Chart component visualizes three dimensions of data by displaying each data point as a bubble, where the X and Y coordinates determine the position and the bubble size represents a third value.",
                Category = SampleCategory.OtherCharts,
                Tags = ["bubble"],
                DemoTypes = [typeof(BubbleChart_Demo_01_Examples)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/bubble.html",
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartDatasetData", "bubblechart-dataset-data-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "playground",
                Title = "Playground",
                Description = "Change the main options of a bubble chart and watch the chart and its C# update together.",
                Category = SampleCategory.Playground,
                Tags = ["bubble", "playground", "localization", "right-to-left"],
                DemoTypes = [typeof(BubbleChart_Playground)],
                IsNewIn200 = true,
                HasPlayground = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("Scales", "scales-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "click-event",
                Title = "Click event",
                Description = "Set the OnClick callback to react when a bubble is selected.",
                Category = SampleCategory.Advanced,
                Tags = ["bubble", "events"],
                DemoTypes = [typeof(BubbleChart_Demo_02_Click_Event)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "export-image",
                Title = "Export image",
                Description = "Export the chart as an image with ToBase64ImageAsync, which returns a base64 data URL you can show, store or send, or save it straight to a file in the browser with DownloadImageAsync.",
                Category = SampleCategory.InstanceApi,
                Tags = ["bubble", "export"],
                DemoTypes = [typeof(BubbleChart_Demo_03_Export_Image)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "show-and-hide-datasets",
                Title = "Show and hide datasets",
                Description = "Show and hide whole datasets or single bubbles from C#, check whether a dataset is visible and count the visible datasets.",
                Category = SampleCategory.InstanceApi,
                Tags = ["bubble", "visibility"],
                DemoTypes = [typeof(BubbleChart_Demo_04_Show_Hide_Datasets)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "legend-click-event",
                Title = "Legend click event",
                Description = "Set OnLegendClick to react when a legend item is clicked.",
                Category = SampleCategory.Legend,
                Tags = ["bubble", "events", "legend"],
                DemoTypes = [typeof(BubbleChart_Demo_05_Legend_Click_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/legend/events.html",
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("ChartLegendClickEventArgs", "chart-legend-click-event-args-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-event",
                Title = "Hover event",
                Description = "Set OnHover to react when the pointer moves onto a different bubble.",
                Category = SampleCategory.Advanced,
                Tags = ["bubble", "events", "interaction"],
                DemoTypes = [typeof(BubbleChart_Demo_06_Hover_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("ChartHoverEventArgs", "chart-hover-event-args-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "programmatic-highlight",
                Title = "Programmatic highlight",
                Description = "Highlight bubbles and show their tooltip from C# with SetActiveElementsAsync, for example to point at a value from a table next to the chart.",
                Category = SampleCategory.InstanceApi,
                Tags = ["bubble", "interaction"],
                DemoTypes = [typeof(BubbleChart_Demo_07_Programmatic_Highlight)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/advanced/programmatic-events.html",
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("ChartActiveElement", "chart-active-element-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "update-modes-and-animation-control",
                Title = "Update modes and animation control",
                Description = "Choose how UpdateAsync animates with ChartUpdateMode, and control the running animation with StopAsync, RenderAsync, ResetAsync and ClearAsync.",
                Category = SampleCategory.InstanceApi,
                Tags = ["bubble", "animation", "updates"],
                DemoTypes = [typeof(BubbleChart_Demo_08_Update_Modes)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("ChartAnimation", "chart-animation-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "multiple-axes",
                Title = "Multiple axes",
                Description = "Add a second y-axis or x-axis with Scales.AdditionalAxes and point a dataset at it with YAxisID or XAxisID, so two series with different units share one chart.",
                Category = SampleCategory.Scales,
                Tags = ["bubble", "axes"],
                DemoTypes = [typeof(BubbleChart_Demo_09_Multiple_Axes)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("Scales", "scales-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "axis-position-and-reverse",
                Title = "Axis position and reverse",
                Description = "Move an axis to another edge with Position, flip its direction with Reverse, or hide it with Display = false.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["bubble", "axes"],
                DemoTypes = [typeof(BubbleChart_Demo_10_Axis_Position_And_Reverse)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("Scales", "scales-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "tick-options",
                Title = "Tick options",
                Description = "Control how many tick labels a linear axis shows, how they are spaced and how they look with ChartAxesTicks.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["bubble", "ticks"],
                DemoTypes = [typeof(BubbleChart_Demo_11_Tick_Options)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/scale-options/ticks.html",
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("Scales", "scales-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "axis-title",
                Title = "Axis title",
                Description = "Label each axis with ChartAxesTitle.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["bubble", "title", "axes"],
                DemoTypes = [typeof(BubbleChart_Demo_12_Axis_Title)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/scale-options/titles.html",
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("Scales", "scales-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "grace-and-bounds",
                Title = "Grace and bounds",
                Description = "Add room beyond the data with Grace, so large bubbles at the edge are not cut off, choose whether the axis ends at the data or at a whole tick with Bounds, and stack two axes on one edge with Stack.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["bubble", "axes"],
                DemoTypes = [typeof(BubbleChart_Demo_13_Grace_And_Bounds)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/scales/linear-min-max-suggested.html",
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("Scales", "scales-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "title-and-subtitle",
                Title = "Title and subtitle",
                Description = "Show a chart title with Plugins.Title and a second line with Plugins.Subtitle.",
                Category = SampleCategory.Title,
                Tags = ["bubble", "title"],
                DemoTypes = [typeof(BubbleChart_Demo_14_Title_And_Subtitle)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/subtitle/basic.html",
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "tooltip-options",
                Title = "Tooltip options",
                Description = "Choose which bubbles a tooltip lists and how it looks with Plugins.Tooltip.",
                Category = SampleCategory.Tooltip,
                Tags = ["bubble", "tooltip"],
                DemoTypes = [typeof(BubbleChart_Demo_15_Tooltip_Options)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/tooltip/position.html",
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("ChartPluginsTooltip", "chart-plugins-tooltip-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "layout-padding",
                Title = "Layout padding",
                Description = "Add empty space between the canvas edge and the chart with Layout.",
                Category = SampleCategory.Advanced,
                Tags = ["bubble", "layout"],
                DemoTypes = [typeof(BubbleChart_Demo_16_Layout_Padding)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "aspect-ratio-and-resize-delay",
                Title = "Aspect ratio and resize delay",
                Description = "Set the width-to-height ratio of a responsive chart with AspectRatio; Chart.js keeps it while MaintainAspectRatio = true (the default) and uses 2 for bubble charts.",
                Category = SampleCategory.Advanced,
                Tags = ["bubble", "layout"],
                DemoTypes = [typeof(BubbleChart_Demo_17_Aspect_Ratio_And_Resize_Delay)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-and-elements",
                Title = "Hover and elements",
                Description = "Set default bubble styling for every dataset with Elements.Point, and change what the pointer highlights with Hover.",
                Category = SampleCategory.Advanced,
                Tags = ["bubble", "interaction"],
                DemoTypes = [typeof(BubbleChart_Demo_18_Hover_And_Elements)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("Interaction", "interaction-properties"), Api("BubbleChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "colors-plugin",
                Title = "Colors plugin",
                Description = "The Chart.js colors plugin gives datasets without colors a color from its built-in palette.",
                Category = SampleCategory.Advanced,
                Tags = ["bubble", "colors"],
                DemoTypes = [typeof(BubbleChart_Demo_19_Colors_Plugin)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BubbleChartDataset", "bubblechart-dataset-properties"), Api("BubbleChartOptions", "bubblechart-options-properties"), Api("ChartPluginsColors", "chart-plugins-colors-properties"), Api("BubbleChart parameters", "parameters")],
            },
        ];
    }

    #endregion

    #endregion
}
