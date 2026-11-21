using BlazorExpress.ChartJS.Demo.RCL.Pages.Demos.RadarChart;

namespace BlazorExpress.ChartJS.Demo.RCL;

public static partial class SampleCatalog
{
    #region Methods

    #region Private Methods

    // One entry per sample section on the Radar chart page, in page order.
    private static List<SampleInfo> GetRadarChartSamples()
    {
        const string ChartKey = SampleChartKeys.RadarChart;
        static SampleApiLink Api(string text, string anchor) => new(text, $"{DemoRouteConstants.Docs_RadarChart}#{anchor}");

        return
        [
            new()
            {
                ChartKey = ChartKey,
                Slug = "how-it-works",
                Title = "How it works",
                Description = "A radar chart that compares two laptops rated from 1 to 10 on battery, performance, display, weight, price and build quality.",
                UseWhen = "you're adding your first radar chart to a page.",
                Category = SampleCategory.OtherCharts,
                Tags = ["radar"],
                DemoTypes = [typeof(RadarChart_Demo_01_How_It_Works)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/radar.html",
                ApiLinks = [Api("ChartData", "chart-data-properties"), Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "playground",
                Title = "Playground",
                Description = "Change the main options of a radar chart and watch the chart and its C# update together.",
                UseWhen = "you want to try the main options and copy the C# that produces the result.",
                Category = SampleCategory.Playground,
                Tags = ["radar", "playground", "localization", "right-to-left"],
                DemoTypes = [typeof(RadarChart_Playground)],
                IsNewIn200 = true,
                HasPlayground = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "click-event",
                Title = "Click event",
                Description = "Set the OnClick callback to react when a radar data point is selected.",
                UseWhen = "a click on a point should show its value or open more detail.",
                Category = SampleCategory.Advanced,
                Tags = ["radar", "events"],
                DemoTypes = [typeof(RadarChart_Demo_02_Click_Event)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartClickEventArgs", "chart-click-event-args-properties"), Api("Common events", "common-events"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "export-image",
                Title = "Export image",
                Description = "Export the chart as an image with ToBase64ImageAsync, which returns a base64 data URL you can show, store or send, or save it straight to a file in the browser with DownloadImageAsync.",
                UseWhen = "users need to download the chart, or you need it as an image for a report or an email.",
                Category = SampleCategory.InstanceApi,
                Tags = ["radar", "export"],
                DemoTypes = [typeof(RadarChart_Demo_03_Export_Image)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("Common methods", "common-methods"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "show-and-hide-datasets",
                Title = "Show and hide datasets",
                Description = "Show and hide whole datasets or single points from C#, check whether a dataset is visible and count the visible datasets.",
                UseWhen = "users or your code should hide and show series or single points without rebuilding the chart.",
                Category = SampleCategory.InstanceApi,
                Tags = ["radar", "visibility"],
                DemoTypes = [typeof(RadarChart_Demo_04_Show_Hide_Datasets)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("Common methods", "common-methods"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "legend-click-event",
                Title = "Legend click event",
                Description = "Set OnLegendClick to react when a legend item is clicked.",
                UseWhen = "your page should react when users click a legend item, or the click should do something other than hide the series.",
                Category = SampleCategory.Legend,
                Tags = ["radar", "events", "legend"],
                DemoTypes = [typeof(RadarChart_Demo_05_Legend_Click_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/legend/events.html",
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartLegendClickEventArgs", "chart-legend-click-event-args-properties"), Api("Common parameters", "common-parameters"), Api("Common events", "common-events"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-event",
                Title = "Hover event",
                Description = "Set OnHover to react when the pointer moves onto a different point.",
                UseWhen = "the page should show details elsewhere as the pointer moves over the points.",
                Category = SampleCategory.Advanced,
                Tags = ["radar", "events", "interaction"],
                DemoTypes = [typeof(RadarChart_Demo_06_Hover_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartHoverEventArgs", "chart-hover-event-args-properties"), Api("Common events", "common-events"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "programmatic-highlight",
                Title = "Programmatic highlight",
                Description = "Highlight points and show their tooltip from C# with SetActiveElementsAsync, for example to point at a value from a table next to the chart.",
                UseWhen = "a table, list or other control next to the chart should point at a value on it.",
                Category = SampleCategory.InstanceApi,
                Tags = ["radar", "interaction"],
                DemoTypes = [typeof(RadarChart_Demo_07_Programmatic_Highlight)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/advanced/programmatic-events.html",
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartActiveElement", "chart-active-element-properties"), Api("Common methods", "common-methods"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "update-modes-and-animation-control",
                Title = "Update modes and animation control",
                Description = "Choose how UpdateAsync animates with ChartUpdateMode, and control the running animation with StopAsync, RenderAsync, ResetAsync and ClearAsync.",
                UseWhen = "data changes often and you need to control or skip the animation, for example for live data.",
                Category = SampleCategory.InstanceApi,
                Tags = ["radar", "animation", "updates"],
                DemoTypes = [typeof(RadarChart_Demo_08_Update_Modes)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartAnimation", "chart-animation-properties"), Api("Common methods", "common-methods"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "radial-scale",
                Title = "Radial scale",
                Description = "The radar chart draws its values on one radial axis, Scales.R, sent to Chart.js as scales.r.",
                UseWhen = "scores sit close together and the axis should start at a fixed value, run to a fixed maximum or run in reverse, for example survey scores that all fall between 60 and 100.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["radar", "axes"],
                DemoTypes = [typeof(RadarChart_Demo_09_Radial_Scale)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadialScales", "radial-scales-properties"), Api("ChartRadialAxes", "chart-radial-axes-properties"), Api("ChartAxesTicks", "chart-axes-ticks-properties"), Api("ChartAxesGrid", "chart-axes-grid-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "angle-lines-and-point-labels",
                Title = "Angle lines and point labels",
                Description = "Angle lines run from the center to each label, and point labels name each spoke of the radar.",
                UseWhen = "the spokes and their labels should stand out, blend in, or be hidden to match your page design.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["radar", "axes"],
                DemoTypes = [typeof(RadarChart_Demo_10_Angle_Lines_And_Point_Labels)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadialScales", "radial-scales-properties"), Api("ChartRadialAxes", "chart-radial-axes-properties"), Api("ChartAxesAngleLines", "chart-axes-angle-lines-properties"), Api("ChartAxesPointLabels", "chart-axes-point-labels-properties"), Api("ChartFont", "chart-font-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "start-angle",
                Title = "Start angle",
                Description = "Scales.R.StartAngle rotates the whole radar, in degrees clockwise from the top.",
                UseWhen = "a different topic should sit at the top, or the shape should be turned so its labels fit the space around it.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["radar"],
                DemoTypes = [typeof(RadarChart_Demo_11_Start_Angle)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadialScales", "radial-scales-properties"), Api("ChartRadialAxes", "chart-radial-axes-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "title-and-subtitle",
                Title = "Title and subtitle",
                Description = "The title and subtitle plugins draw text around the chart area.",
                UseWhen = "the chart needs a heading and a short note, such as the period or the units, drawn with it.",
                Category = SampleCategory.Title,
                Tags = ["radar", "title"],
                DemoTypes = [typeof(RadarChart_Demo_12_Title_And_Subtitle)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/subtitle/basic.html",
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("ChartPluginsSubtitle", "chart-plugins-subtitle-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "tooltip-options",
                Title = "Tooltip options",
                Description = "Tooltip options decide which points a tooltip lists, where it opens and how it looks.",
                UseWhen = "the default tooltip shows too much, too little, or in the wrong place.",
                Category = SampleCategory.Tooltip,
                Tags = ["radar", "tooltip"],
                DemoTypes = [typeof(RadarChart_Demo_13_Tooltip_Options)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/tooltip/position.html",
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartPluginsTooltip", "chart-plugins-tooltip-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "layout-padding",
                Title = "Layout padding",
                Description = "Layout padding adds space between the canvas edge and the chart, legend and title.",
                UseWhen = "the legend, labels or chart touch the edge of the canvas or the surrounding layout.",
                Category = SampleCategory.Advanced,
                Tags = ["radar", "layout"],
                DemoTypes = [typeof(RadarChart_Demo_14_Layout_Padding)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartLayout", "chart-layout-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "aspect-ratio-and-resize-delay",
                Title = "Aspect ratio and resize delay",
                Description = "A responsive chart keeps its width-to-height ratio while MaintainAspectRatio is true; Chart.js uses a ratio of 1 for radar charts.",
                UseWhen = "the chart is too tall or too short for its space, or redraws too often while the window resizes.",
                Category = SampleCategory.Advanced,
                Tags = ["radar", "layout"],
                DemoTypes = [typeof(RadarChart_Demo_15_Aspect_Ratio_And_Resize_Delay)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-and-elements",
                Title = "Hover and elements",
                Description = "Elements sets default styles for every point and line the chart draws, and Hover decides which points are highlighted under the pointer, separately from Interaction, which the tooltip keeps using.",
                UseWhen = "every point should share one style, or hovering should change how points look.",
                Category = SampleCategory.Advanced,
                Tags = ["radar", "interaction"],
                DemoTypes = [typeof(RadarChart_Demo_16_Hover_And_Elements)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartHover", "chart-hover-properties"), Api("ChartElements", "chart-elements-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "colors-plugin",
                Title = "Colors plugin",
                Description = "The Chart.js colors plugin gives datasets that set no colors a color from its built-in palette.",
                UseWhen = "you want readable default colors without choosing a color for every dataset.",
                Category = SampleCategory.Advanced,
                Tags = ["radar", "colors"],
                DemoTypes = [typeof(RadarChart_Demo_17_Colors_Plugin)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartPluginsColors", "chart-plugins-colors-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "skip-points",
                Title = "Skip points",
                Description = "A null value skips a point on a radar chart, and SpanGaps decides whether the line bridges the gap.",
                UseWhen = "some scores are missing, for example a test that was not run, and the chart should show the gap or join the line across it.",
                Category = SampleCategory.OtherCharts,
                Tags = ["radar", "gaps", "missing data"],
                DemoTypes = [typeof(RadarChart_Demo_18_Skip_Points)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/radar-skip-points.html",
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
        ];
    }

    #endregion

    #endregion
}
