using BlazorExpress.ChartJS.Demo.RCL.Pages.Demos.BarChart;

namespace BlazorExpress.ChartJS.Demo.RCL;

public static partial class SampleCatalog
{
    #region Methods

    #region Private Methods

    // One entry per sample section on the Bar chart page, in page order.
    private static List<SampleInfo> GetBarChartSamples()
    {
        const string ChartKey = SampleChartKeys.BarChart;
        static SampleApiLink Api(string text, string anchor) => new(text, $"{DemoRouteConstants.Docs_BarChart}#{anchor}");

        return
        [
            new()
            {
                ChartKey = ChartKey,
                Slug = "how-it-works",
                Title = "How it works",
                Description = "The Bar Chart component visualizes data as vertical bars, making it easy to compare values across categories or track changes over time.",
                Category = SampleCategory.Bar,
                Tags = ["bar"],
                DemoTypes = [typeof(BarChart_Demo_01_Examples)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/bar/vertical.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartDatasetData", "barchart-dataset-data-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "playground",
                Title = "Playground",
                Description = "Change the main options of a bar chart and watch the chart and its C# update together.",
                Category = SampleCategory.Playground,
                Tags = ["bar", "playground", "localization", "right-to-left"],
                DemoTypes = [typeof(BarChart_Playground)],
                IsNewIn130 = true,
                HasPlayground = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Scales", "scales-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "click-event",
                Title = "Click event",
                Description = "Set the OnClick callback to react when a chart data item is selected.",
                Category = SampleCategory.Advanced,
                Tags = ["bar", "events"],
                DemoTypes = [typeof(BarChart_Demo_11_Click_Event)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "combo-bar-line",
                Title = "Combo bar/line",
                Description = "The Combo bar/line demo mixes BarChartDataset and LineChartDataset in the same BarChart.",
                Category = SampleCategory.Bar,
                Tags = ["bar", "mixed"],
                DemoTypes = [typeof(BarChart_Demo_09_Combo_Bar_Line)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/combo-bar-line.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Interaction", "interaction-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "horizontal-bar-chart",
                Title = "Horizontal bar chart",
                Description = "The Horizontal Bar Chart displays data values as horizontal bars, making it ideal for comparing categories with long labels or when you want to emphasize comparison between values.",
                Category = SampleCategory.Bar,
                Tags = ["bar", "orientation"],
                DemoTypes = [typeof(BarChart_Demo_02_Horizontal_BarChart)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/bar/horizontal.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Interaction", "interaction-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "stacked-bar-chart",
                Title = "Stacked bar chart",
                Description = "The Stacked Bar Chart allows you to display multiple data series stacked on top of each other, making it easy to compare the total and individual contributions of each series for every category.",
                Category = SampleCategory.Bar,
                Tags = ["bar", "stacked"],
                DemoTypes = [typeof(BarChart_Demo_03_Stacked_BarChart)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/bar/stacked.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Interaction", "interaction-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "stacked-bar-chart-with-groups",
                Title = "Stacked bar chart with groups",
                Description = "The Stacked Bar Chart with Groups displays multiple independent stacks side by side for each category.",
                Category = SampleCategory.Bar,
                Tags = ["bar", "stacked"],
                DemoTypes = [typeof(BarChart_Demo_10_Stacked_BarChart_With_Groups)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/bar/stacked-groups.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Interaction", "interaction-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "stacked-bar-chart-with-data-labels",
                Title = "Stacked bar chart with data labels",
                Description = "The Stacked Bar Chart with Data Labels enhances the standard stacked bar chart by displaying value labels directly on each bar segment.",
                Category = SampleCategory.Bar,
                Tags = ["bar", "stacked", "data labels"],
                DemoTypes = [typeof(BarChart_Demo_04_Stacked_BarChart_with_Datalabels)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Interaction", "interaction-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "locale",
                Title = "Locale",
                Description = "The Locale demo shows how to localize number and date formatting in your bar chart.",
                Category = SampleCategory.Advanced,
                Tags = ["bar", "localization"],
                DemoTypes = [typeof(BarChart_Demo_05_Locale)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Interaction", "interaction-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "border-radius",
                Title = "Border radius",
                Description = "The Border Radius feature allows you to create bar charts with rounded corners, giving your charts a modern and visually appealing look.",
                Category = SampleCategory.Bar,
                Tags = ["bar", "styling"],
                DemoTypes = [typeof(BarChart_Demo_06_Border_Radius)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/bar/border-radius.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Interaction", "interaction-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "animations-delay",
                Title = "Animations - Delay",
                Description = "The Animations - Delay demo illustrates how to add a delay to the animation of your Bar Chart, making the chart elements appear with a timed entrance effect.",
                Category = SampleCategory.Animations,
                Tags = ["bar", "animation"],
                DemoTypes = [typeof(BarChart_Demo_07_Animations_A_Delay)],
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/animations/delay.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Interaction", "interaction-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "animations-dataset-level-delay",
                Title = "Animations - Dataset level delay",
                Description = "The Animations - Dataset level delay demo demonstrates how to apply different animation delays to each dataset in your Bar Chart.",
                Category = SampleCategory.Animations,
                Tags = ["bar", "animation"],
                DemoTypes = [typeof(BarChart_Demo_08_Animations_B_DataSet_Level_Delay)],
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Interaction", "interaction-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "export-image",
                Title = "Export image",
                Description = "Export the chart as an image with ToBase64ImageAsync, which returns a base64 data URL you can show, store or send, or save it straight to a file in the browser with DownloadImageAsync.",
                Category = SampleCategory.InstanceApi,
                Tags = ["bar", "export"],
                DemoTypes = [typeof(BarChart_Demo_12_Export_Image)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "show-and-hide-datasets",
                Title = "Show and hide datasets",
                Description = "Show and hide whole datasets or single bars from C#, check whether a dataset is visible and count the visible datasets.",
                Category = SampleCategory.InstanceApi,
                Tags = ["bar", "visibility"],
                DemoTypes = [typeof(BarChart_Demo_13_Show_Hide_Datasets)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "legend-click-event",
                Title = "Legend click event",
                Description = "Set OnLegendClick to react when a legend item is clicked.",
                Category = SampleCategory.Legend,
                Tags = ["bar", "events", "legend"],
                DemoTypes = [typeof(BarChart_Demo_14_Legend_Click_Event)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/legend/events.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartLegendClickEventArgs", "chart-legend-click-event-args-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-event",
                Title = "Hover event",
                Description = "Set OnHover to react when the pointer moves onto a different bar.",
                Category = SampleCategory.Advanced,
                Tags = ["bar", "events", "interaction"],
                DemoTypes = [typeof(BarChart_Demo_15_Hover_Event)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartHoverEventArgs", "chart-hover-event-args-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "programmatic-highlight",
                Title = "Programmatic highlight",
                Description = "Highlight bars and show their tooltip from C# with SetActiveElementsAsync, for example to point at a value from a table next to the chart.",
                Category = SampleCategory.InstanceApi,
                Tags = ["bar", "interaction"],
                DemoTypes = [typeof(BarChart_Demo_16_Programmatic_Highlight)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/advanced/programmatic-events.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartActiveElement", "chart-active-element-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "update-modes-and-animation-control",
                Title = "Update modes and animation control",
                Description = "Choose how UpdateAsync animates with ChartUpdateMode, and control the running animation with StopAsync, RenderAsync, ResetAsync and ClearAsync.",
                Category = SampleCategory.InstanceApi,
                Tags = ["bar", "animation", "updates"],
                DemoTypes = [typeof(BarChart_Demo_17_Update_Modes)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartAnimation", "chart-animation-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "multiple-axes",
                Title = "Multiple axes",
                Description = "Add more axes with Scales.AdditionalAxes, keyed by axis ID, and point a dataset at one with YAxisID or XAxisID.",
                Category = SampleCategory.Scales,
                Tags = ["bar", "axes"],
                DemoTypes = [typeof(BarChart_Demo_18_Multiple_Axes)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/line/multi-axis.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Interaction", "interaction-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "axis-position-and-reverse",
                Title = "Axis position and reverse",
                Description = "Move an axis to another edge with Position, flip its direction with Reverse, or hide it with Display = false.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["bar", "axes"],
                DemoTypes = [typeof(BarChart_Demo_19_Axis_Position_And_Reverse)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Scales", "scales-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "tick-options",
                Title = "Tick options",
                Description = "ChartAxesTicks controls how many ticks an axis gets and how their labels look.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["bar", "ticks"],
                DemoTypes = [typeof(BarChart_Demo_20_Tick_Options)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/scale-options/ticks.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Scales", "scales-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "axis-title",
                Title = "Axis title",
                Description = "Label an axis with ChartAxesTitle.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["bar", "title", "axes"],
                DemoTypes = [typeof(BarChart_Demo_21_Axis_Title)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/scale-options/titles.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartPadding", "chart-padding-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "grace-and-bounds",
                Title = "Grace and bounds",
                Description = "Grace adds space beyond the data, Bounds decides whether the axis ends at the data (\"data\") or at a whole tick (\"ticks\", the Chart.js default), and SuggestedMin / SuggestedMax extend the range only when the data does not already reach them.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["bar", "axes"],
                DemoTypes = [typeof(BarChart_Demo_22_Grace_And_Bounds)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/scales/linear-min-max-suggested.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Scales", "scales-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "floating-bars",
                Title = "Floating bars",
                Description = "A floating bar spans a (start, end) range instead of growing from the base.",
                Category = SampleCategory.Bar,
                Tags = ["bar", "range"],
                DemoTypes = [typeof(BarChart_Demo_23_Floating_Bars)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/bar/floating.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartDatasetData", "barchart-dataset-data-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "per-corner-border-radius",
                Title = "Per corner border radius",
                Description = "BorderRadiusCorners rounds each bar corner by its own amount, in pixels, and HoverBorderRadiusCorners does the same while the bar is hovered.",
                Category = SampleCategory.Bar,
                Tags = ["bar", "styling"],
                DemoTypes = [typeof(BarChart_Demo_24_Per_Corner_Border_Radius)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartBorderRadius", "chart-border-radius-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "base-and-inflate-amount",
                Title = "Base and inflate amount",
                Description = "Base sets the value bars grow from instead of zero, InflateAmount widens each drawn bar by a number of pixels (Chart.js uses \"auto\" to hide hairline gaps), and PointStyle sets the shape used for the dataset in the legend and tooltip.",
                Category = SampleCategory.Bar,
                Tags = ["bar"],
                DemoTypes = [typeof(BarChart_Demo_25_Base_And_Inflate_Amount)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "title-and-subtitle",
                Title = "Title and subtitle",
                Description = "Plugins.Title and Plugins.Subtitle draw text outside the chart area; Chart.js places the subtitle between the title and the chart.",
                Category = SampleCategory.Title,
                Tags = ["bar", "title"],
                DemoTypes = [typeof(BarChart_Demo_26_Title_And_Subtitle)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/subtitle/basic.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartPluginsTitle", "chart-plugins-title-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "tooltip-options",
                Title = "Tooltip options",
                Description = "Plugins.Tooltip chooses which items the tooltip lists and how it looks.",
                Category = SampleCategory.Tooltip,
                Tags = ["bar", "tooltip"],
                DemoTypes = [typeof(BarChart_Demo_27_Tooltip_Options)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/tooltip/position.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartPluginsTooltip", "chart-plugins-tooltip-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "layout-padding",
                Title = "Layout padding",
                Description = "Layout adds empty space inside the canvas, around the legend, axes and chart.",
                Category = SampleCategory.Advanced,
                Tags = ["bar", "layout"],
                DemoTypes = [typeof(BarChart_Demo_28_Layout_Padding)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartLayout", "chart-layout-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "aspect-ratio-and-resize-delay",
                Title = "Aspect ratio and resize delay",
                Description = "With MaintainAspectRatio = true, the canvas height follows its width by AspectRatio (width / height); Chart.js uses 2 for bar charts.",
                Category = SampleCategory.Advanced,
                Tags = ["bar", "layout"],
                DemoTypes = [typeof(BarChart_Demo_29_Aspect_Ratio_And_Resize_Delay)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "hover-and-elements",
                Title = "Hover and elements",
                Description = "Elements sets defaults for every element of a kind, such as every bar, and a dataset value wins over them.",
                Category = SampleCategory.Advanced,
                Tags = ["bar", "interaction"],
                DemoTypes = [typeof(BarChart_Demo_30_Hover_And_Elements)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartHover", "chart-hover-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "colors-plugin",
                Title = "Colors plugin",
                Description = "The Chart.js colors plugin gives each dataset a color from its built-in palette, but only when no dataset sets a color.",
                Category = SampleCategory.Advanced,
                Tags = ["bar", "colors"],
                DemoTypes = [typeof(BarChart_Demo_31_Colors_Plugin)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartPluginsColors", "chart-plugins-colors-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "currency-ticks",
                Title = "Currency ticks",
                Description = "Ticks.Format formats axis labels with JavaScript's number formatting, so currency, percent and compact labels need no JavaScript of your own.",
                Category = SampleCategory.ScaleOptions,
                Tags = ["bar", "ticks", "formatting"],
                DemoTypes = [typeof(BarChart_Demo_32_Currency_Ticks_Format)],
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Scales", "scales-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "legend-click-function",
                Title = "Legend click function",
                Description = "A legend OnClick function replaces Chart.js's default toggle, as in Chart.js.",
                Category = SampleCategory.Legend,
                Tags = ["bar", "events", "legend", "js-function"],
                DemoTypes = [typeof(BarChart_Demo_33_Legend_Click_Function)],
                UsesJsFunction = true,
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartLegendClickEventArgs", "chart-legend-click-event-args-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "stacked-bar-line",
                Title = "Stacked bar/line",
                Description = "Stacked bars and a line in one stack: the bars add up and the line runs along the total.",
                Category = SampleCategory.Bar,
                Tags = ["bar", "stacked", "mixed", "line"],
                DemoTypes = [typeof(BarChart_Demo_34_Stacked_Bar_Line)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/other-charts/stacked-bar-line.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("Interaction", "interaction-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "legend-position",
                Title = "Legend position",
                Description = "Legend.Position places the legend on any side of a bar chart, and Legend.Align moves it along that side.",
                Category = SampleCategory.Legend,
                Tags = ["bar", "legend", "position"],
                DemoTypes = [typeof(BarChart_Demo_35_Legend_Position)],
                IsNewIn130 = true,
                ChartJsSampleUrl = "https://www.chartjs.org/docs/latest/samples/legend/position.html",
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("ChartPluginsLegend", "chart-plugins-legend-properties"), Api("BarChart parameters", "parameters")],
            },
            new()
            {
                ChartKey = ChartKey,
                Slug = "datalabels-formatter",
                Title = "Datalabels formatter",
                Description = "A datalabels formatter is a registered JavaScript function that turns each value into its label text.",
                Category = SampleCategory.JsFunctions,
                Tags = ["bar", "data labels", "formatting", "js-function"],
                DemoTypes = [typeof(BarChart_Demo_36_Datalabels_Formatter)],
                UsesJsFunction = true,
                IsNewIn130 = true,
                ChartJsSampleUrl = null,
                ApiLinks = [Api("BarChartDataset", "barchart-dataset-properties"), Api("BarChartOptions", "barchart-options-properties"), Api("BarChartDataLabels", "barchart-data-labels-properties"), Api("ChartFunction", "chart-function-properties"), Api("BarChart parameters", "parameters")],
            },
        ];
    }

    #endregion

    #endregion
}
