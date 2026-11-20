using BlazorExpress.ChartJS.Demo.RCL.Pages.Demos.ScatterChart;

namespace BlazorExpress.ChartJS.Demo.RCL;

public static partial class SampleCatalog
{
    #region Methods

    #region Private Methods

    // One entry per sample section on the Scatter chart page, in page order.
    private static List<SampleInfo> GetScatterChartSamples()
    {
        const string ChartKey = SampleChartKeys.ScatterChart;
        static SampleApiLink Api(string text, string anchor) => new(text, $"{DemoRouteConstants.Docs_ScatterChart}#{anchor}");

        return
        [
            new()
            {
                ChartKey = ChartKey,
                Slug = "how-it-works",
                Title = "How it works",
                Description = "Plot revenue against ad spend for 12 marketing campaigns, one point per campaign, with each axis titled with its unit.",
                UseWhen = "you're adding your first scatter chart to a page.",
                Category = SampleCategory.OtherCharts,
                Tags = ["scatter"],
                DemoTypes = [typeof(ScatterChart_Demo_01_How_It_Works)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/scatter.html",
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("ScatterChartDataPoint", "scatterchart-data-point-properties"), Api("Scales", "scales-properties"), Api("ChartAxesTitle", "chart-axes-title-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "playground",
                Title = "Playground",
                Description = "Change the main options of a scatter chart and watch the chart and its C# update together.",
                UseWhen = "you want to try the main options and copy the C# that produces the result.",
                Category = SampleCategory.Playground,
                Tags = ["scatter", "playground", "localization", "right-to-left"],
                DemoTypes = [typeof(ScatterChart_Playground)],
                IsNewIn200 = true,
                HasPlayground = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("Scales", "scales-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "dynamic-data",
                Title = "Dynamic data",
                Description = "The Scatter Chart component supports dynamic data updates, allowing you to add, remove, or modify data points and datasets at runtime.",
                Category = SampleCategory.InstanceApi,
                Tags = ["scatter", "updates"],
                DemoTypes = [typeof(ScatterChart_Demo_02_Dynamic_Data)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartDatasetData", "scatterchart-dataset-data-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "click-event",
                Title = "Click event",
                Description = "Set the OnClick callback to react when a scatter point is selected.",
                UseWhen = "a click on a point should show its value or open more detail.",
                Category = SampleCategory.Advanced,
                Tags = ["scatter", "events"],
                DemoTypes = [typeof(ScatterChart_Demo_03_Click_Event)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("ChartClickEventArgs", "chart-click-event-args-properties"), Api("Common events", "common-events"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "export-image",
                Title = "Export image",
                Description = "Export the chart as an image with ToBase64ImageAsync, which returns a base64 data URL you can show, store or send, or save it straight to a file in the browser with DownloadImageAsync.",
                UseWhen = "users need to download the chart, or you need it as an image for a report or an email.",
                Category = SampleCategory.InstanceApi,
                Tags = ["scatter", "export"],
                DemoTypes = [typeof(ScatterChart_Demo_04_Export_Image)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("Common methods", "common-methods"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "show-and-hide-datasets",
                Title = "Show and hide datasets",
                Description = "Show and hide whole datasets or single points from C#, check whether a dataset is visible and count the visible datasets.",
                UseWhen = "users or your code should hide and show series or single points without rebuilding the chart.",
                Category = SampleCategory.InstanceApi,
                Tags = ["scatter", "visibility"],
                DemoTypes = [typeof(ScatterChart_Demo_05_Show_Hide_Datasets)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("Common methods", "common-methods"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "legend-click-event",
                Title = "Legend click event",
                Description = "Set OnLegendClick to react when a legend item is clicked.",
                UseWhen = "your page should react when users click a legend item, or the click should do something other than hide the series.",
                Category = SampleCategory.Legend,
                Tags = ["scatter", "events", "legend"],
                DemoTypes = [typeof(ScatterChart_Demo_06_Legend_Click_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/legend/events.html",
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("ChartLegendClickEventArgs", "chart-legend-click-event-args-properties"), Api("Common parameters", "common-parameters"), Api("Common events", "common-events"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-event",
                Title = "Hover event",
                Description = "Set OnHover to react when the pointer moves onto a different point.",
                UseWhen = "the page should show details elsewhere as the pointer moves over the points.",
                Category = SampleCategory.Advanced,
                Tags = ["scatter", "events", "interaction"],
                DemoTypes = [typeof(ScatterChart_Demo_07_Hover_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("ChartHoverEventArgs", "chart-hover-event-args-properties"), Api("Common events", "common-events"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "programmatic-highlight",
                Title = "Programmatic highlight",
                Description = "Highlight points and show their tooltip from C# with SetActiveElementsAsync, for example to point at a value from a table next to the chart.",
                UseWhen = "a table, list or other control next to the chart should point at a value on it.",
                Category = SampleCategory.InstanceApi,
                Tags = ["scatter", "interaction"],
                DemoTypes = [typeof(ScatterChart_Demo_08_Programmatic_Highlight)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/advanced/programmatic-events.html",
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("ChartActiveElement", "chart-active-element-properties"), Api("Common methods", "common-methods"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "update-modes-and-animation-control",
                Title = "Update modes and animation control",
                Description = "Choose how UpdateAsync animates with ChartUpdateMode, and control the running animation with StopAsync, RenderAsync, ResetAsync and ClearAsync.",
                UseWhen = "data changes often and you need to control or skip the animation, for example for live data.",
                Category = SampleCategory.InstanceApi,
                Tags = ["scatter", "animation", "updates"],
                DemoTypes = [typeof(ScatterChart_Demo_09_Update_Modes)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("ChartAnimation", "chart-animation-properties"), Api("Common methods", "common-methods"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "multiple-axes",
                Title = "Multiple axes",
                Description = "Add a second y-axis or x-axis with Scales.AdditionalAxes and point a dataset at it with YAxisID or XAxisID, so two series with different units share one chart.",
                Category = SampleCategory.Scales,
                Tags = ["scatter", "axes"],
                DemoTypes = [typeof(ScatterChart_Demo_10_Multiple_Axes)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("Scales", "scales-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "axis-position-and-reverse",
                Title = "Axis position and reverse",
                Description = "Move an axis to another edge with Position, flip its direction with Reverse, or hide it with Display = false.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["scatter", "axes"],
                DemoTypes = [typeof(ScatterChart_Demo_11_Axis_Position_And_Reverse)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("Scales", "scales-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "tick-options",
                Title = "Tick options",
                Description = "Control how many tick labels a linear axis shows, how they are spaced and how they look with ChartAxesTicks.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["scatter", "ticks"],
                DemoTypes = [typeof(ScatterChart_Demo_12_Tick_Options)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/scale-options/ticks.html",
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("Scales", "scales-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "axis-title",
                Title = "Axis title",
                Description = "Label each axis with ChartAxesTitle.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["scatter", "title", "axes"],
                DemoTypes = [typeof(ScatterChart_Demo_13_Axis_Title)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/scale-options/titles.html",
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("Scales", "scales-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "grace-and-bounds",
                Title = "Grace and bounds",
                Description = "Add room beyond the data with Grace, choose whether the axis ends at the data or at a whole tick with Bounds, and stack two axes on one edge with Stack.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["scatter", "axes"],
                DemoTypes = [typeof(ScatterChart_Demo_14_Grace_And_Bounds)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/scales/linear-min-max-suggested.html",
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("Scales", "scales-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "title-and-subtitle",
                Title = "Title and subtitle",
                Description = "Show a chart title with Plugins.Title and a second line with Plugins.Subtitle.",
                UseWhen = "the chart needs a heading and a short note, such as the period or the units, drawn with it.",
                Category = SampleCategory.Title,
                Tags = ["scatter", "title"],
                DemoTypes = [typeof(ScatterChart_Demo_15_Title_And_Subtitle)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/subtitle/basic.html",
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("ChartPluginsSubtitle", "chart-plugins-subtitle-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "tooltip-options",
                Title = "Tooltip options",
                Description = "Choose which points a tooltip lists and how it looks with Plugins.Tooltip.",
                UseWhen = "the default tooltip shows too much, too little, or in the wrong place.",
                Category = SampleCategory.Tooltip,
                Tags = ["scatter", "tooltip"],
                DemoTypes = [typeof(ScatterChart_Demo_16_Tooltip_Options)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/tooltip/position.html",
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("ChartPluginsTooltip", "chart-plugins-tooltip-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "layout-padding",
                Title = "Layout padding",
                Description = "Add empty space between the canvas edge and the chart with Layout.",
                UseWhen = "the legend, labels or chart touch the edge of the canvas or the surrounding layout.",
                Category = SampleCategory.Advanced,
                Tags = ["scatter", "layout"],
                DemoTypes = [typeof(ScatterChart_Demo_17_Layout_Padding)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("ChartLayout", "chart-layout-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "aspect-ratio-and-resize-delay",
                Title = "Aspect ratio and resize delay",
                Description = "Set the width-to-height ratio of a responsive chart with AspectRatio; Chart.js keeps it while MaintainAspectRatio = true (the default) and uses 2 for scatter charts.",
                UseWhen = "the chart is too tall or too short for its space, or redraws too often while the window resizes.",
                Category = SampleCategory.Advanced,
                Tags = ["scatter", "layout"],
                DemoTypes = [typeof(ScatterChart_Demo_18_Aspect_Ratio_And_Resize_Delay)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-and-elements",
                Title = "Hover and elements",
                Description = "Set default point styling for every dataset with Elements.Point, and change what the pointer highlights with Hover.",
                UseWhen = "every point should share one style, or hovering should change how points look.",
                Category = SampleCategory.Advanced,
                Tags = ["scatter", "interaction"],
                DemoTypes = [typeof(ScatterChart_Demo_19_Hover_And_Elements)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("Interaction", "interaction-properties"), Api("ChartHover", "chart-hover-properties"), Api("ChartElementsPoint", "chart-elements-point-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "colors-plugin",
                Title = "Colors plugin",
                Description = "The Chart.js colors plugin gives datasets without colors a color from its built-in palette.",
                UseWhen = "you want readable default colors without choosing a color for every dataset.",
                Category = SampleCategory.Advanced,
                Tags = ["scatter", "colors"],
                DemoTypes = [typeof(ScatterChart_Demo_20_Colors_Plugin)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("ChartPluginsColors", "chart-plugins-colors-properties"), Api("ScatterChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "time-sensor-readings",
                Title = "Sensor readings over time",
                Description = "A scatter chart can plot points against a time axis, each point at its own date and time, without any extra script.",
                Category = SampleCategory.TimeAxis,
                Tags = ["scatter", "time"],
                DemoTypes = [typeof(ScatterChart_Demo_21_Time_Sensor_Readings)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("ScatterChartDataset", "scatterchart-dataset-properties"), Api("ScatterChartOptions", "scatterchart-options-properties"), Api("Scales", "scales-properties"), Api("ScatterChart parameters", "parameters")],
            },
        ];
    }

    #endregion

    #endregion
}
