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
                Description = "The Radar Chart component visualizes multivariate data with each variable represented on axes starting from the same point, making it ideal for comparing multiple groups across several categories.",
                Category = SampleCategory.OtherCharts,
                Tags = ["radar"],
                DemoTypes = [typeof(RadarChart_Demo_01_Examples)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/radar.html",
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartDatasetData", "radarchart-dataset-data-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "playground",
                Title = "Playground",
                Description = "Change the main options of a radar chart and watch the chart and its C# update together.",
                Category = SampleCategory.Playground,
                Tags = ["radar", "playground", "localization", "right-to-left"],
                DemoTypes = [typeof(RadarChart_Playground)],
                IsNewIn130 = true,
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
                Category = SampleCategory.Advanced,
                Tags = ["radar", "events"],
                DemoTypes = [typeof(RadarChart_Demo_02_Click_Event)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "export-image",
                Title = "Export image",
                Description = "Export the chart as an image with ToBase64ImageAsync, which returns a base64 data URL you can show, store or send, or save it straight to a file in the browser with DownloadImageAsync.",
                Category = SampleCategory.InstanceApi,
                Tags = ["radar", "export"],
                DemoTypes = [typeof(RadarChart_Demo_03_Export_Image)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "show-and-hide-datasets",
                Title = "Show and hide datasets",
                Description = "Show and hide whole datasets or single points from C#, check whether a dataset is visible and count the visible datasets.",
                Category = SampleCategory.InstanceApi,
                Tags = ["radar", "visibility"],
                DemoTypes = [typeof(RadarChart_Demo_04_Show_Hide_Datasets)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "legend-click-event",
                Title = "Legend click event",
                Description = "Set OnLegendClick to react when a legend item is clicked.",
                Category = SampleCategory.Legend,
                Tags = ["radar", "events", "legend"],
                DemoTypes = [typeof(RadarChart_Demo_05_Legend_Click_Event)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/legend/events.html",
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartLegendClickEventArgs", "chart-legend-click-event-args-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-event",
                Title = "Hover event",
                Description = "Set OnHover to react when the pointer moves onto a different point.",
                Category = SampleCategory.Advanced,
                Tags = ["radar", "events", "interaction"],
                DemoTypes = [typeof(RadarChart_Demo_06_Hover_Event)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartHoverEventArgs", "chart-hover-event-args-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "programmatic-highlight",
                Title = "Programmatic highlight",
                Description = "Highlight points and show their tooltip from C# with SetActiveElementsAsync, for example to point at a value from a table next to the chart.",
                Category = SampleCategory.InstanceApi,
                Tags = ["radar", "interaction"],
                DemoTypes = [typeof(RadarChart_Demo_07_Programmatic_Highlight)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/advanced/programmatic-events.html",
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartActiveElement", "chart-active-element-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "update-modes-and-animation-control",
                Title = "Update modes and animation control",
                Description = "Choose how UpdateAsync animates with ChartUpdateMode, and control the running animation with StopAsync, RenderAsync, ResetAsync and ClearAsync.",
                Category = SampleCategory.InstanceApi,
                Tags = ["radar", "animation", "updates"],
                DemoTypes = [typeof(RadarChart_Demo_08_Update_Modes)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartAnimation", "chart-animation-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "radial-scale",
                Title = "Radial scale",
                Description = "The radar chart draws its values on one radial axis, Scales.R, sent to Chart.js as scales.r.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["radar", "axes"],
                DemoTypes = [typeof(RadarChart_Demo_09_Radial_Scale)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadialScales", "radial-scales-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "angle-lines-and-point-labels",
                Title = "Angle lines and point labels",
                Description = "Angle lines run from the center to each label, and point labels name each spoke of the radar.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["radar", "axes"],
                DemoTypes = [typeof(RadarChart_Demo_10_Angle_Lines_And_Point_Labels)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadialScales", "radial-scales-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "start-angle",
                Title = "Start angle",
                Description = "Scales.R.StartAngle rotates the whole radar, in degrees clockwise from the top.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["radar"],
                DemoTypes = [typeof(RadarChart_Demo_11_Start_Angle)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadialScales", "radial-scales-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "title-and-subtitle",
                Title = "Title and subtitle",
                Description = "The title and subtitle plugins draw text around the chart area.",
                Category = SampleCategory.Title,
                Tags = ["radar", "title"],
                DemoTypes = [typeof(RadarChart_Demo_12_Title_And_Subtitle)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/subtitle/basic.html",
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "tooltip-options",
                Title = "Tooltip options",
                Description = "Tooltip options decide which points a tooltip lists, where it opens and how it looks.",
                Category = SampleCategory.Tooltip,
                Tags = ["radar", "tooltip"],
                DemoTypes = [typeof(RadarChart_Demo_13_Tooltip_Options)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/tooltip/position.html",
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "layout-padding",
                Title = "Layout padding",
                Description = "Layout padding adds space between the canvas edge and the chart, legend and title.",
                Category = SampleCategory.Advanced,
                Tags = ["radar", "layout"],
                DemoTypes = [typeof(RadarChart_Demo_14_Layout_Padding)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "aspect-ratio-and-resize-delay",
                Title = "Aspect ratio and resize delay",
                Description = "A responsive chart keeps its width-to-height ratio while MaintainAspectRatio is true; Chart.js uses a ratio of 1 for radar charts.",
                Category = SampleCategory.Advanced,
                Tags = ["radar", "layout"],
                DemoTypes = [typeof(RadarChart_Demo_15_Aspect_Ratio_And_Resize_Delay)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-and-elements",
                Title = "Hover and elements",
                Description = "Elements sets default styles for every point and line the chart draws, and Hover decides which points are highlighted under the pointer, separately from Interaction, which the tooltip keeps using.",
                Category = SampleCategory.Advanced,
                Tags = ["radar", "interaction"],
                DemoTypes = [typeof(RadarChart_Demo_16_Hover_And_Elements)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("ChartHover", "chart-hover-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "colors-plugin",
                Title = "Colors plugin",
                Description = "The Chart.js colors plugin gives datasets that set no colors a color from its built-in palette.",
                Category = SampleCategory.Advanced,
                Tags = ["radar", "colors"],
                DemoTypes = [typeof(RadarChart_Demo_17_Colors_Plugin)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "skip-points",
                Title = "Skip points",
                Description = "A null value skips a point on a radar chart, and SpanGaps decides whether the line bridges the gap.",
                Category = SampleCategory.OtherCharts,
                Tags = ["radar", "gaps", "missing data"],
                DemoTypes = [typeof(RadarChart_Demo_18_Skip_Points)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/radar-skip-points.html",
                ApiLinks = [Api("RadarChartDataset", "radarchart-dataset-properties"), Api("RadarChartOptions", "radarchart-options-properties"), Api("RadarChart parameters", "parameters")],
            },
        ];
    }

    #endregion

    #endregion
}
