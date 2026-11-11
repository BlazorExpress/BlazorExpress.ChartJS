using BlazorExpress.ChartJS.Demo.RCL.Pages.Demos.DoughnutChart;

namespace BlazorExpress.ChartJS.Demo.RCL;

public static partial class SampleCatalog
{
    #region Methods

    #region Private Methods

    // One entry per sample section on the Doughnut chart page, in page order.
    private static List<SampleInfo> GetDoughnutChartSamples()
    {
        const string ChartKey = SampleChartKeys.DoughnutChart;
        static SampleApiLink Api(string text, string anchor) => new(text, $"{DemoRouteConstants.Docs_DoughnutChart}#{anchor}");

        return
        [
            new()
            {
                ChartKey = ChartKey,
                Slug = "how-it-works",
                Title = "How it works",
                Description = "The Doughnut Chart component provides a simple way to visualize categorical data as proportional segments of a circle.",
                Category = SampleCategory.OtherCharts,
                Tags = ["doughnut"],
                DemoTypes = [typeof(DoughnutChart_Demo_01_Examples)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/doughnut.html",
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartDatasetData", "doughnutchart-dataset-data-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "playground",
                Title = "Playground",
                Description = "Change the main options of a doughnut chart and watch the chart and its C# update together.",
                Category = SampleCategory.Playground,
                Tags = ["doughnut", "playground", "localization", "right-to-left"],
                DemoTypes = [typeof(DoughnutChart_Playground)],
                IsNewIn200 = true,
                HasPlayground = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "data-labels",
                Title = "Data labels",
                Description = "The Doughnut Chart component supports data labels, allowing you to display values or category names directly on each segment of the chart.",
                Category = SampleCategory.Advanced,
                Tags = ["doughnut", "data labels"],
                DemoTypes = [typeof(DoughnutChart_Demo_02_Datalabels)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartDatasetData", "doughnutchart-dataset-data-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "click-event",
                Title = "Click event",
                Description = "Set the OnClick callback to react when a doughnut segment is selected.",
                Category = SampleCategory.Advanced,
                Tags = ["doughnut", "events"],
                DemoTypes = [typeof(DoughnutChart_Demo_03_Click_Event)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "export-image",
                Title = "Export image",
                Description = "Export the chart as an image with ToBase64ImageAsync, which returns a base64 data URL you can show, store or send, or save it straight to a file in the browser with DownloadImageAsync.",
                Category = SampleCategory.InstanceApi,
                Tags = ["doughnut", "export"],
                DemoTypes = [typeof(DoughnutChart_Demo_04_Export_Image)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "show-and-hide-slices",
                Title = "Show and hide slices",
                Description = "Show and hide single slices from C# and read whether a slice is visible.",
                Category = SampleCategory.InstanceApi,
                Tags = ["doughnut", "visibility"],
                DemoTypes = [typeof(DoughnutChart_Demo_05_Show_Hide_Slices)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "legend-click-event",
                Title = "Legend click event",
                Description = "Set OnLegendClick to react when a legend item is clicked.",
                Category = SampleCategory.Legend,
                Tags = ["doughnut", "events", "legend"],
                DemoTypes = [typeof(DoughnutChart_Demo_06_Legend_Click_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/legend/events.html",
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("ChartLegendClickEventArgs", "chart-legend-click-event-args-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-event",
                Title = "Hover event",
                Description = "Set OnHover to react when the pointer moves onto a different slice.",
                Category = SampleCategory.Advanced,
                Tags = ["doughnut", "events", "interaction"],
                DemoTypes = [typeof(DoughnutChart_Demo_07_Hover_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("ChartHoverEventArgs", "chart-hover-event-args-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "programmatic-highlight",
                Title = "Programmatic highlight",
                Description = "Highlight slices and show their tooltip from C# with SetActiveElementsAsync, for example to point at a value from a table next to the chart.",
                Category = SampleCategory.InstanceApi,
                Tags = ["doughnut", "interaction"],
                DemoTypes = [typeof(DoughnutChart_Demo_08_Programmatic_Highlight)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/advanced/programmatic-events.html",
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("ChartActiveElement", "chart-active-element-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "update-modes-and-animation-control",
                Title = "Update modes and animation control",
                Description = "Choose how UpdateAsync animates with ChartUpdateMode, and control the running animation with StopAsync, RenderAsync, ResetAsync and ClearAsync.",
                Category = SampleCategory.InstanceApi,
                Tags = ["doughnut", "animation", "updates"],
                DemoTypes = [typeof(DoughnutChart_Demo_09_Update_Modes)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("ChartAnimation", "chart-animation-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "cutout-and-radius",
                Title = "Cutout and radius",
                Description = "Cutout sets the size of the hole in the middle and Radius sets the outer radius.",
                Category = SampleCategory.OtherCharts,
                Tags = ["doughnut", "styling"],
                DemoTypes = [typeof(DoughnutChart_Demo_10_Cutout_And_Radius)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "half-gauge",
                Title = "Half gauge",
                Description = "Rotation sets the start angle of the first arc, in degrees from the top, and Circumference sets how far the arcs sweep.",
                Category = SampleCategory.OtherCharts,
                Tags = ["doughnut", "styling"],
                DemoTypes = [typeof(DoughnutChart_Demo_11_Half_Gauge)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "rotate-and-scale-animation",
                Title = "Rotate and scale animation",
                Description = "ChartAnimation.AnimateRotate sweeps the arcs in from the start angle, and AnimateScale grows the chart from the center.",
                Category = SampleCategory.Animations,
                Tags = ["doughnut", "animation"],
                DemoTypes = [typeof(DoughnutChart_Demo_12_Rotate_And_Scale_Animation)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("ChartAnimation", "chart-animation-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "spacing-mode",
                Title = "Spacing mode",
                Description = "The dataset Spacing adds a gap between arcs, in pixels, and SpacingMode sets how the gap is measured: \"angular\" keeps an equal angle (the Chart.js default), \"proportional\" scales with each arc, and \"parallel\" keeps the edges of neighbouring arcs parallel.",
                Category = SampleCategory.OtherCharts,
                Tags = ["doughnut", "styling"],
                DemoTypes = [typeof(DoughnutChart_Demo_13_Spacing_Mode)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "title-and-subtitle",
                Title = "Title and subtitle",
                Description = "Plugins.Title and Plugins.Subtitle draw a title and a smaller line of text, placed by Position and aligned by Align.",
                Category = SampleCategory.Title,
                Tags = ["doughnut", "title"],
                DemoTypes = [typeof(DoughnutChart_Demo_14_Title_And_Subtitle)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/subtitle/basic.html",
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "tooltip-options",
                Title = "Tooltip options",
                Description = "Plugins.Tooltip sets which items the tooltip lists, where it is placed and how it looks.",
                Category = SampleCategory.Tooltip,
                Tags = ["doughnut", "tooltip"],
                DemoTypes = [typeof(DoughnutChart_Demo_15_Tooltip_Options)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/tooltip/position.html",
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("ChartPluginsTooltip", "chart-plugins-tooltip-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "layout-padding",
                Title = "Layout padding",
                Description = "Layout adds empty space inside the canvas, around the legend, title and chart.",
                Category = SampleCategory.Advanced,
                Tags = ["doughnut", "layout"],
                DemoTypes = [typeof(DoughnutChart_Demo_16_Layout_Padding)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("ChartLayout", "chart-layout-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "aspect-ratio-and-resize-delay",
                Title = "Aspect ratio and resize delay",
                Description = "With MaintainAspectRatio = true, the canvas height follows its width by AspectRatio (width / height); Chart.js uses 1 for doughnut charts.",
                Category = SampleCategory.Advanced,
                Tags = ["doughnut", "layout"],
                DemoTypes = [typeof(DoughnutChart_Demo_17_Aspect_Ratio_And_Resize_Delay)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-and-elements",
                Title = "Hover and elements",
                Description = "Elements.Arc sets defaults for every arc, which dataset settings override.",
                Category = SampleCategory.Advanced,
                Tags = ["doughnut", "interaction"],
                DemoTypes = [typeof(DoughnutChart_Demo_18_Hover_And_Elements)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("Interaction", "interaction-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "colors-plugin",
                Title = "Colors plugin",
                Description = "The built-in Chart.js colors plugin gives datasets that set no colors a default palette, one color per slice on a doughnut chart.",
                Category = SampleCategory.Advanced,
                Tags = ["doughnut", "colors"],
                DemoTypes = [typeof(DoughnutChart_Demo_19_Colors_Plugin)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("DoughnutChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "radial-gradient",
                Title = "Radial gradient",
                Description = "A colour function can return a radial canvas gradient, so each slice fades from the centre to the rim.",
                Category = SampleCategory.JsFunctions,
                Tags = ["doughnut", "colors", "gradient", "js-function"],
                DemoTypes = [typeof(DoughnutChart_Demo_20_Radial_Gradient)],
                UsesJsFunction = true,
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/advanced/radial-gradient.html",
                ApiLinks = [Api("DoughnutChartDataset", "doughnutchart-dataset-properties"), Api("DoughnutChartOptions", "doughnutchart-options-properties"), Api("ChartLayout", "chart-layout-properties"), Api("ChartFunction", "chart-function-properties"), Api("DoughnutChart parameters", "parameters")],
            },
        ];
    }

    #endregion

    #endregion
}
