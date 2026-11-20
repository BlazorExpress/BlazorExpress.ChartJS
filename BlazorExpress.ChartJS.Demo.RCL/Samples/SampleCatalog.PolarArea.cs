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
                Description = "A polar area chart of the support tickets received last month on each channel, with one equal-angle slice per channel sized by its count.",
                UseWhen = "you're adding your first polar area chart to a page.",
                Category = SampleCategory.OtherCharts,
                Tags = ["polar area"],
                DemoTypes = [typeof(PolarAreaChart_Demo_01_How_It_Works)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/polar-area.html",
                ApiLinks = [Api("ChartData", "chart-data-properties"), Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "playground",
                Title = "Playground",
                Description = "Change the main options of a polar area chart and watch the chart and its C# update together.",
                UseWhen = "you want to try the main options and copy the C# that produces the result.",
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
                UseWhen = "a click on a slice should show its value or open more detail.",
                Category = SampleCategory.Advanced,
                Tags = ["polar area", "events"],
                DemoTypes = [typeof(PolarAreaChart_Demo_02_Click_Event)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartClickEventArgs", "chart-click-event-args-properties"), Api("Common events", "common-events"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "export-image",
                Title = "Export image",
                Description = "Export the chart as an image with ToBase64ImageAsync, which returns a base64 data URL you can show, store or send, or save it straight to a file in the browser with DownloadImageAsync.",
                UseWhen = "users need to download the chart, or you need it as an image for a report or an email.",
                Category = SampleCategory.InstanceApi,
                Tags = ["polar area", "export"],
                DemoTypes = [typeof(PolarAreaChart_Demo_03_Export_Image)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("Common methods", "common-methods"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "show-and-hide-slices",
                Title = "Show and hide slices",
                Description = "Show and hide single slices from C# and read whether a slice is visible.",
                UseWhen = "users or your code should hide and show series or single slices without rebuilding the chart.",
                Category = SampleCategory.InstanceApi,
                Tags = ["polar area", "visibility"],
                DemoTypes = [typeof(PolarAreaChart_Demo_04_Show_Hide_Slices)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("Common methods", "common-methods"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "legend-click-event",
                Title = "Legend click event",
                Description = "Set OnLegendClick to react when a legend item is clicked.",
                UseWhen = "your page should react when users click a legend item, or the click should do something other than hide the series.",
                Category = SampleCategory.Legend,
                Tags = ["polar area", "events", "legend"],
                DemoTypes = [typeof(PolarAreaChart_Demo_05_Legend_Click_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/legend/events.html",
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartLegendClickEventArgs", "chart-legend-click-event-args-properties"), Api("Common parameters", "common-parameters"), Api("Common events", "common-events"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-event",
                Title = "Hover event",
                Description = "Set OnHover to react when the pointer moves onto a different slice.",
                UseWhen = "the page should show details elsewhere as the pointer moves over the slices.",
                Category = SampleCategory.Advanced,
                Tags = ["polar area", "events", "interaction"],
                DemoTypes = [typeof(PolarAreaChart_Demo_06_Hover_Event)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartHoverEventArgs", "chart-hover-event-args-properties"), Api("Common events", "common-events"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "programmatic-highlight",
                Title = "Programmatic highlight",
                Description = "Highlight slices and show their tooltip from C# with SetActiveElementsAsync, for example to point at a value from a table next to the chart.",
                UseWhen = "a table, list or other control next to the chart should point at a value on it.",
                Category = SampleCategory.InstanceApi,
                Tags = ["polar area", "interaction"],
                DemoTypes = [typeof(PolarAreaChart_Demo_07_Programmatic_Highlight)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/advanced/programmatic-events.html",
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartActiveElement", "chart-active-element-properties"), Api("Common methods", "common-methods"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "update-modes-and-animation-control",
                Title = "Update modes and animation control",
                Description = "Choose how UpdateAsync animates with ChartUpdateMode, and control the running animation with StopAsync, RenderAsync, ResetAsync and ClearAsync.",
                UseWhen = "data changes often and you need to control or skip the animation, for example for live data.",
                Category = SampleCategory.InstanceApi,
                Tags = ["polar area", "animation", "updates"],
                DemoTypes = [typeof(PolarAreaChart_Demo_08_Update_Modes)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartAnimation", "chart-animation-properties"), Api("Common methods", "common-methods"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "radial-scale",
                Title = "Radial scale",
                Description = "The polar area chart sizes each slice on one radial axis, Scales.R, sent to Chart.js as scales.r.",
                UseWhen = "values sit close together and the slices should be measured on a fixed or reversed range, for example regional sales that all fall between 40 and 80.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["polar area", "axes"],
                DemoTypes = [typeof(PolarAreaChart_Demo_09_Radial_Scale)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("RadialScales", "radial-scales-properties"), Api("ChartRadialAxes", "chart-radial-axes-properties"), Api("ChartAxesTicks", "chart-axes-ticks-properties"), Api("ChartAxesGrid", "chart-axes-grid-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "angle-lines-and-point-labels",
                Title = "Angle lines and point labels",
                Description = "Angle lines run from the center outwards between the slices, and point labels name each slice at the edge of the chart.",
                UseWhen = "readers should see each region's name on the chart itself instead of matching slice colors to the legend.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["polar area", "axes"],
                DemoTypes = [typeof(PolarAreaChart_Demo_10_Angle_Lines_And_Point_Labels)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/polar-area-center-labels.html",
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("RadialScales", "radial-scales-properties"), Api("ChartRadialAxes", "chart-radial-axes-properties"), Api("ChartAxesAngleLines", "chart-axes-angle-lines-properties"), Api("ChartAxesPointLabels", "chart-axes-point-labels-properties"), Api("ChartFont", "chart-font-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "start-angle",
                Title = "Start angle",
                Description = "Scales.R.StartAngle rotates the slices, in degrees clockwise from the top.",
                UseWhen = "the first slice should start somewhere other than the top, for example to line the slices up with a map or a compass layout.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["polar area"],
                DemoTypes = [typeof(PolarAreaChart_Demo_11_Start_Angle)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("RadialScales", "radial-scales-properties"), Api("ChartRadialAxes", "chart-radial-axes-properties"), Api("ChartAxesPointLabels", "chart-axes-point-labels-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "spacing-mode",
                Title = "Spacing mode",
                Description = "SpacingMode sets how Chart.js measures the gap between slices: \"angular\" keeps an equal angle, \"proportional\" (the Chart.js default for polar area) scales the gap with each slice, and \"parallel\" keeps the edges of neighbouring slices parallel.",
                UseWhen = "the slices should be separated by a visible gap and you want to choose how that gap is shaped.",
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
                UseWhen = "the chart needs a heading and a short note, such as the period or the units, drawn with it.",
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
                UseWhen = "the default tooltip shows too much, too little, or in the wrong place.",
                Category = SampleCategory.Tooltip,
                Tags = ["polar area", "tooltip"],
                DemoTypes = [typeof(PolarAreaChart_Demo_14_Tooltip_Options)],
                IsNewIn200 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/tooltip/position.html",
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartPluginsTooltip", "chart-plugins-tooltip-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "layout-padding",
                Title = "Layout padding",
                Description = "Layout padding adds space between the canvas edge and the chart, legend and title.",
                UseWhen = "the legend, labels or chart touch the edge of the canvas or the surrounding layout.",
                Category = SampleCategory.Advanced,
                Tags = ["polar area", "layout"],
                DemoTypes = [typeof(PolarAreaChart_Demo_15_Layout_Padding)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartLayout", "chart-layout-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "aspect-ratio-and-resize-delay",
                Title = "Aspect ratio and resize delay",
                Description = "A responsive chart keeps its width-to-height ratio while MaintainAspectRatio is true; Chart.js uses a ratio of 1 for polar area charts.",
                UseWhen = "the chart is too tall or too short for its space, or redraws too often while the window resizes.",
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
                UseWhen = "every slice should share one style, or hovering should change how slices look.",
                Category = SampleCategory.Advanced,
                Tags = ["polar area", "interaction"],
                DemoTypes = [typeof(PolarAreaChart_Demo_17_Hover_And_Elements)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartHover", "chart-hover-properties"), Api("ChartElementsArc", "chart-elements-arc-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "colors-plugin",
                Title = "Colors plugin",
                Description = "The Chart.js colors plugin gives datasets that set no colors a color from its built-in palette; on polar area charts it colors each slice.",
                UseWhen = "you want readable default colors without choosing a color for every dataset.",
                Category = SampleCategory.Advanced,
                Tags = ["polar area", "colors"],
                DemoTypes = [typeof(PolarAreaChart_Demo_18_Colors_Plugin)],
                IsNewIn200 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("PolarAreaChartDataset", "polarareachart-dataset-properties"), Api("PolarAreaChartOptions", "polarareachart-options-properties"), Api("ChartPluginsColors", "chart-plugins-colors-properties"), Api("PolarAreaChart parameters", "parameters")],
            },
        ];
    }

    #endregion

    #endregion
}
