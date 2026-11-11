using BlazorExpress.ChartJS.Demo.RCL.Pages.Demos.PolarAreaChart;

namespace BlazorExpress.ChartJS.Demo.RCL;

public static partial class SampleCatalog
{
    #region Methods

    #region Private Methods

    // One entry per sample section on the PolarArea chart page, in page order.
    private static List<SampleInfo> GetPolarAreaChartSamples()
    {
        const string ChartKey = SampleChartKeys.PolarAreaChart;
        static SampleApiLink Api(string text, string anchor) => new(text, $"{DemoRouteConstants.Docs_PolarAreaChart}#{anchor}");

        return
        [
            new()
            {
                ChartKey = ChartKey,
                Slug = "how-it-works",
                Title = "How it works",
                Description = "The Polar Area Chart component visualizes each data value as a segment with equal angles but varying radius, making it ideal for comparing values across categories in a circular layout.",
                Category = SampleCategory.OtherCharts,
                Tags = ["polar area"],
                DemoTypes = [typeof(PolarAreaChart_Demo_01_Examples)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/polar-area.html",
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartDatasetData", "polarareachart-dataset-data-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "playground",
                Title = "Playground",
                Description = "Change the main options of a polar area chart and watch the chart and its C# update together.",
                Category = SampleCategory.Playground,
                Tags = ["polar area", "playground", "localization", "right-to-left"],
                DemoTypes = [typeof(PolarAreaChart_Playground)],
                IsNewIn200 = true,
                HasPlayground = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartAxesTicks", "chart-axes-ticks-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "click-event",
                Title = "Click event",
                Description = "Set the OnClick callback to react when a polar-area segment is selected.",
                Category = SampleCategory.Advanced,
                Tags = ["polar area", "events"],
                DemoTypes = [typeof(PolarAreaChart_Demo_02_Click_Event)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "export-image",
                Title = "Export image",
                Description = "Export the chart as an image with ToBase64ImageAsync, which returns a base64 data URL you can show, store or send, or save it straight to a file in the browser with DownloadImageAsync.",
                Category = SampleCategory.InstanceApi,
                Tags = ["polar area", "export"],
                DemoTypes = [typeof(PolarAreaChart_Demo_03_Export_Image)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "show-and-hide-slices",
                Title = "Show and hide slices",
                Description = "Show and hide single slices from C# and read whether a slice is visible.",
                Category = SampleCategory.InstanceApi,
                Tags = ["polar area", "visibility"],
                DemoTypes = [typeof(PolarAreaChart_Demo_04_Show_Hide_Slices)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "legend-click-event",
                Title = "Legend click event",
                Description = "Set OnLegendClick to react when a legend item is clicked.",
                Category = SampleCategory.Legend,
                Tags = ["polar area", "events", "legend"],
                DemoTypes = [typeof(PolarAreaChart_Demo_05_Legend_Click_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/legend/events.html",
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartLegendClickEventArgs", "chart-legend-click-event-args-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-event",
                Title = "Hover event",
                Description = "Set OnHover to react when the pointer moves onto a different slice.",
                Category = SampleCategory.Advanced,
                Tags = ["polar area", "events", "interaction"],
                DemoTypes = [typeof(PolarAreaChart_Demo_06_Hover_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartHoverEventArgs", "chart-hover-event-args-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "programmatic-highlight",
                Title = "Programmatic highlight",
                Description = "Highlight slices and show their tooltip from C# with SetActiveElementsAsync, for example to point at a value from a table next to the chart.",
                Category = SampleCategory.InstanceApi,
                Tags = ["polar area", "interaction"],
                DemoTypes = [typeof(PolarAreaChart_Demo_07_Programmatic_Highlight)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/advanced/programmatic-events.html",
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartActiveElement", "chart-active-element-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "update-modes-and-animation-control",
                Title = "Update modes and animation control",
                Description = "Choose how UpdateAsync animates with ChartUpdateMode, and control the running animation with StopAsync, RenderAsync, ResetAsync and ClearAsync.",
                Category = SampleCategory.InstanceApi,
                Tags = ["polar area", "animation", "updates"],
                DemoTypes = [typeof(PolarAreaChart_Demo_08_Update_Modes)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartAnimation", "chart-animation-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "radial-scale",
                Title = "Radial scale",
                Description = "The polar area chart sizes each slice on one radial axis, Scales.R, sent to Chart.js as scales.r.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["polar area", "axes"],
                DemoTypes = [typeof(PolarAreaChart_Demo_09_Radial_Scale)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("RadialScales", "radial-scales-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "angle-lines-and-point-labels",
                Title = "Angle lines and point labels",
                Description = "Angle lines run from the center outwards between the slices, and point labels name each slice at the edge of the chart.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["polar area", "axes"],
                DemoTypes = [typeof(PolarAreaChart_Demo_10_Angle_Lines_And_Point_Labels)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("RadialScales", "radial-scales-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "start-angle",
                Title = "Start angle",
                Description = "Scales.R.StartAngle rotates the slices, in degrees clockwise from the top.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["polar area"],
                DemoTypes = [typeof(PolarAreaChart_Demo_11_Start_Angle)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("RadialScales", "radial-scales-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "spacing-mode",
                Title = "Spacing mode",
                Description = "SpacingMode sets how Chart.js measures the gap between slices: \"angular\" keeps an equal angle, \"proportional\" (the Chart.js default for polar area) scales the gap with each slice, and \"parallel\" keeps the edges of neighbouring slices parallel.",
                Category = SampleCategory.OtherCharts,
                Tags = ["polar area", "styling"],
                DemoTypes = [typeof(PolarAreaChart_Demo_12_Spacing_Mode)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "title-and-subtitle",
                Title = "Title and subtitle",
                Description = "The title and subtitle plugins draw text around the chart area.",
                Category = SampleCategory.Title,
                Tags = ["polar area", "title"],
                DemoTypes = [typeof(PolarAreaChart_Demo_13_Title_And_Subtitle)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/subtitle/basic.html",
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "tooltip-options",
                Title = "Tooltip options",
                Description = "Tooltip options decide which slices a tooltip lists, where it opens and how it looks.",
                Category = SampleCategory.Tooltip,
                Tags = ["polar area", "tooltip"],
                DemoTypes = [typeof(PolarAreaChart_Demo_14_Tooltip_Options)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/tooltip/position.html",
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "layout-padding",
                Title = "Layout padding",
                Description = "Layout padding adds space between the canvas edge and the chart, legend and title.",
                Category = SampleCategory.Advanced,
                Tags = ["polar area", "layout"],
                DemoTypes = [typeof(PolarAreaChart_Demo_15_Layout_Padding)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "aspect-ratio-and-resize-delay",
                Title = "Aspect ratio and resize delay",
                Description = "A responsive chart keeps its width-to-height ratio while MaintainAspectRatio is true; Chart.js uses a ratio of 1 for polar area charts.",
                Category = SampleCategory.Advanced,
                Tags = ["polar area", "layout"],
                DemoTypes = [typeof(PolarAreaChart_Demo_16_Aspect_Ratio_And_Resize_Delay)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-and-elements",
                Title = "Hover and elements",
                Description = "Elements sets default styles for every arc the chart draws, and Hover decides which slices are highlighted under the pointer, separately from Interaction, which the tooltip keeps using.",
                Category = SampleCategory.Advanced,
                Tags = ["polar area", "interaction"],
                DemoTypes = [typeof(PolarAreaChart_Demo_17_Hover_And_Elements)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartHover", "chart-hover-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "colors-plugin",
                Title = "Colors plugin",
                Description = "The Chart.js colors plugin gives datasets that set no colors a color from its built-in palette; on polar area charts it colors each slice.",
                Category = SampleCategory.Advanced,
                Tags = ["polar area", "colors"],
                DemoTypes = [typeof(PolarAreaChart_Demo_18_Colors_Plugin)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
        ];
    }

    #endregion

    #endregion
}
