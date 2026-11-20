using BlazorExpress.ChartJS.Demo.RCL.Pages.Demos.PieChart;

namespace BlazorExpress.ChartJS.Demo.RCL;

public static partial class SampleCatalog
{
    #region Methods

    #region Private Methods

    // One entry per sample section on the Pie chart page, in page order.
    private static List<SampleInfo> GetPieChartSamples()
    {
        const string ChartKey = SampleChartKeys.PieChart;
        static SampleApiLink Api(string text, string anchor) => new(text, $"{DemoRouteConstants.Docs_PieChart}#{anchor}");

        return
        [
            new()
            {
                ChartKey = ChartKey,
                Slug = "how-it-works",
                Title = "How it works",
                Description = "A pie chart of a website's visits by traffic source, each slice sized by its share of all visits.",
                UseWhen = "you're adding your first pie chart to a page.",
                Category = SampleCategory.OtherCharts,
                Tags = ["pie"],
                DemoTypes = [typeof(PieChart_Demo_01_How_It_Works)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/pie.html",
                ApiLinks = [Api("ChartData", "chart-data-properties"), Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "playground",
                Title = "Playground",
                Description = "Change the main options of a pie chart and watch the chart and its C# update together.",
                UseWhen = "you want to try the main options and copy the C# that produces the result.",
                Category = SampleCategory.Playground,
                Tags = ["pie", "playground", "localization", "right-to-left"],
                DemoTypes = [typeof(PieChart_Playground)],
                IsNewIn200 = true,
                HasPlayground = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartLayout", "chart-layout-properties"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "data-labels",
                Title = "Data labels",
                Description = "The Pie Chart component supports data labels, allowing you to display values or category names directly on each slice of the chart.",
                UseWhen = "readers need each slice's exact value without hovering, for example in a printed report.",
                Category = SampleCategory.Advanced,
                Tags = ["pie", "data labels"],
                DemoTypes = [typeof(PieChart_Demo_02_Datalabels)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("ChartDatasetDataLabels", "chart-dataset-data-labels-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("PieChart methods", "methods"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "change-legend-position",
                Title = "Change legend position",
                Description = "The Pie Chart component allows you to easily change the position of the chart legend to improve readability or match your application's layout.",
                UseWhen = "the legend crowds the chart or should sit beside it to fit your page layout.",
                Category = SampleCategory.Legend,
                Tags = ["pie", "legend"],
                DemoTypes = [typeof(PieChart_Demo_03_Change_Legend_Position)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/legend/position.html",
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartPluginsLegend", "chart-plugins-legend-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("PieChart methods", "methods"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "click-event",
                Title = "Click event",
                Description = "Set the OnClick callback to react when a pie slice is selected.",
                UseWhen = "a click on a slice should show its value or open more detail.",
                Category = SampleCategory.Advanced,
                Tags = ["pie", "events"],
                DemoTypes = [typeof(PieChart_Demo_04_Click_Event)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartClickEventArgs", "chart-click-event-args-properties"), Api("Common events", "common-events"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "show-and-hide-slices",
                Title = "Show and hide slices",
                Description = "Show and hide single slices from C# and read whether a slice is visible.",
                UseWhen = "users or your code should hide and show series or single slices without rebuilding the chart.",
                Category = SampleCategory.InstanceApi,
                Tags = ["pie", "visibility"],
                DemoTypes = [typeof(PieChart_Demo_05_Show_Hide_Slices)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("Common methods", "common-methods"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "legend-click-event",
                Title = "Legend click event",
                Description = "Set OnLegendClick to react when a legend item is clicked.",
                UseWhen = "your page should react when users click a legend item, or the click should do something other than hide the series.",
                Category = SampleCategory.Legend,
                Tags = ["pie", "events", "legend"],
                DemoTypes = [typeof(PieChart_Demo_06_Legend_Click_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/legend/events.html",
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartLegendClickEventArgs", "chart-legend-click-event-args-properties"), Api("Common parameters", "common-parameters"), Api("Common events", "common-events"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "export-image",
                Title = "Export image",
                Description = "Export the chart as an image with ToBase64ImageAsync, which returns a base64 data URL you can show, store or send, or save it straight to a file in the browser with DownloadImageAsync.",
                UseWhen = "users need to download the chart, or you need it as an image for a report or an email.",
                Category = SampleCategory.InstanceApi,
                Tags = ["pie", "export"],
                DemoTypes = [typeof(PieChart_Demo_07_Export_Image)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("Common methods", "common-methods"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-event",
                Title = "Hover event",
                Description = "Set OnHover to react when the pointer moves onto a different slice.",
                UseWhen = "the page should show details elsewhere as the pointer moves over the slices.",
                Category = SampleCategory.Advanced,
                Tags = ["pie", "events", "interaction"],
                DemoTypes = [typeof(PieChart_Demo_08_Hover_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartHoverEventArgs", "chart-hover-event-args-properties"), Api("Common events", "common-events"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "programmatic-highlight",
                Title = "Programmatic highlight",
                Description = "Highlight slices and show their tooltip from C# with SetActiveElementsAsync, for example to point at a value from a table next to the chart.",
                UseWhen = "a table, list or other control next to the chart should point at a value on it.",
                Category = SampleCategory.InstanceApi,
                Tags = ["pie", "interaction"],
                DemoTypes = [typeof(PieChart_Demo_09_Programmatic_Highlight)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/advanced/programmatic-events.html",
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartActiveElement", "chart-active-element-properties"), Api("Common methods", "common-methods"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "update-modes-and-animation-control",
                Title = "Update modes and animation control",
                Description = "Choose how UpdateAsync animates with ChartUpdateMode, and control the running animation with StopAsync, RenderAsync, ResetAsync and ClearAsync.",
                UseWhen = "data changes often and you need to control or skip the animation, for example for live data.",
                Category = SampleCategory.InstanceApi,
                Tags = ["pie", "animation", "updates"],
                DemoTypes = [typeof(PieChart_Demo_10_Update_Modes)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartAnimation", "chart-animation-properties"), Api("Common methods", "common-methods"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "cutout-and-radius",
                Title = "Cutout and radius",
                Description = "Cutout sets the size of the hole in the middle and Radius sets the outer radius.",
                UseWhen = "the pie should leave room around it, or become a ring with space for a total in the middle.",
                Category = SampleCategory.OtherCharts,
                Tags = ["pie", "styling"],
                DemoTypes = [typeof(PieChart_Demo_11_Cutout_And_Radius)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "half-gauge",
                Title = "Half gauge",
                Description = "Rotation sets the start angle of the first arc, in degrees from the top, and Circumference sets how far the arcs sweep.",
                UseWhen = "a single figure, such as progress towards a target, should read as a gauge rather than a full pie.",
                Category = SampleCategory.OtherCharts,
                Tags = ["pie", "styling"],
                DemoTypes = [typeof(PieChart_Demo_12_Half_Gauge)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartPluginsLegend", "chart-plugins-legend-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "rotate-and-scale-animation",
                Title = "Rotate and scale animation",
                Description = "ChartAnimation.AnimateRotate sweeps the arcs in from the start angle, and AnimateScale grows the chart from the center.",
                UseWhen = "the chart should sweep in, grow from the centre, or both when it first appears.",
                Category = SampleCategory.Animations,
                Tags = ["pie", "animation"],
                DemoTypes = [typeof(PieChart_Demo_13_Rotate_And_Scale_Animation)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartAnimation", "chart-animation-properties"), Api("Common methods", "common-methods"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "spacing-mode",
                Title = "Spacing mode",
                Description = "The dataset Spacing adds a gap between arcs, in pixels, and SpacingMode sets how the gap is measured: \"angular\" keeps an equal angle (the Chart.js default), \"proportional\" scales with each arc, and \"parallel\" keeps the edges of neighbouring arcs parallel.",
                UseWhen = "slices should stand apart with a visible gap, for example so small neighbouring slices stay distinct.",
                Category = SampleCategory.OtherCharts,
                Tags = ["pie", "styling"],
                DemoTypes = [typeof(PieChart_Demo_14_Spacing_Mode)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "title-and-subtitle",
                Title = "Title and subtitle",
                Description = "Plugins.Title and Plugins.Subtitle draw a title and a smaller line of text, placed by Position and aligned by Align.",
                UseWhen = "the chart needs a heading and a short note, such as the period or the units, drawn with it.",
                Category = SampleCategory.Title,
                Tags = ["pie", "title"],
                DemoTypes = [typeof(PieChart_Demo_15_Title_And_Subtitle)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/subtitle/basic.html",
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("ChartPluginsSubtitle", "chart-plugins-subtitle-properties"), Api("ChartFont", "chart-font-properties"), Api("ChartPadding", "chart-padding-properties"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "tooltip-options",
                Title = "Tooltip options",
                Description = "Plugins.Tooltip sets which items the tooltip lists, where it is placed and how it looks.",
                UseWhen = "the default tooltip shows too much, too little, or in the wrong place.",
                Category = SampleCategory.Tooltip,
                Tags = ["pie", "tooltip"],
                DemoTypes = [typeof(PieChart_Demo_16_Tooltip_Options)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/tooltip/position.html",
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartPluginsTooltip", "chart-plugins-tooltip-properties"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "layout-padding",
                Title = "Layout padding",
                Description = "Layout adds empty space inside the canvas, around the legend, title and chart.",
                UseWhen = "the legend, labels or chart touch the edge of the canvas or the surrounding layout.",
                Category = SampleCategory.Advanced,
                Tags = ["pie", "layout"],
                DemoTypes = [typeof(PieChart_Demo_17_Layout_Padding)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartLayout", "chart-layout-properties"), Api("ChartPadding", "chart-padding-properties"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "aspect-ratio-and-resize-delay",
                Title = "Aspect ratio and resize delay",
                Description = "With MaintainAspectRatio = true, the canvas height follows its width by AspectRatio (width / height); Chart.js uses 1 for pie charts.",
                UseWhen = "the chart is too tall or too short for its space, or redraws too often while the window resizes.",
                Category = SampleCategory.Advanced,
                Tags = ["pie", "layout"],
                DemoTypes = [typeof(PieChart_Demo_18_Aspect_Ratio_And_Resize_Delay)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-and-elements",
                Title = "Hover and elements",
                Description = "Elements.Arc sets defaults for every arc, which dataset settings override.",
                UseWhen = "every slice should share one style, or hovering should change how slices look.",
                Category = SampleCategory.Advanced,
                Tags = ["pie", "interaction"],
                DemoTypes = [typeof(PieChart_Demo_19_Hover_And_Elements)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartElements", "chart-elements-properties"), Api("ChartElementsArc", "chart-elements-arc-properties"), Api("ChartHover", "chart-hover-properties"), Api("Interaction", "interaction-properties"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "colors-plugin",
                Title = "Colors plugin",
                Description = "The built-in Chart.js colors plugin gives datasets that set no colors a default palette, one color per slice on a pie chart.",
                UseWhen = "you want readable default colors without choosing a color for every dataset.",
                Category = SampleCategory.Advanced,
                Tags = ["pie", "colors"],
                DemoTypes = [typeof(PieChart_Demo_20_Colors_Plugin)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartPluginsColors", "chart-plugins-colors-properties"), Api("PieChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "multi-series-pie",
                Title = "Multi-series pie",
                Description = "A pie chart with more than one dataset draws each dataset as its own ring.",
                UseWhen = "the same categories should be compared across two periods, such as sales by region this year and last year.",
                Category = SampleCategory.OtherCharts,
                Tags = ["pie", "rings", "comparison"],
                DemoTypes = [typeof(PieChart_Demo_21_Multi_Series_Pie)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/multi-series-pie.html",
                ApiLinks = [Api("PieChartDataset", "piechart-dataset-properties"), Api("PieChartOptions", "piechart-options-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("PieChart parameters", "parameters")],
            },
        ];
    }

    #endregion

    #endregion
}
