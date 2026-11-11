using System.Diagnostics.Metrics;
using System.Reflection;

namespace BlazorExpress.ChartJS;

/// <summary>
/// A line chart is a way of plotting data points on a line. 
/// Often, it is used to show trend data, or the comparison of two data sets.
/// <para>
/// <see href="https://www.chartjs.org/docs/latest/charts/line.html#dataset-properties" />.
/// </para>
/// </summary>
public class LineChartDataset : ChartDataset<double?>
{
    #region Constructors and Finalizers

    public LineChartDataset()
    {
        Type = "line";
    }

    #endregion

    #region Methods

    /// <summary>
    /// Fill between this dataset and the other dataset, specified by absolute index (zero based) or relative index.
    /// </summary>
    /// <param name="index">The index of the dataset to fill to</param>
    /// <param name="relativeIndex">Whether the specified index is relative or absolute (zero based)</param>
    /// <returns>The dataset, for method chaining</returns>
    /// <exception cref="ArgumentException">If the relative index is zero.</exception>
    public LineChartDataset FillToDataset(int index, bool relativeIndex = false)
    {
        if (relativeIndex && index == 0)
            throw new ArgumentException("The relative index must be non-zero.");

        Fill = relativeIndex ? index.ToString("+0;-0", CultureInfo.InvariantCulture) : index;

        return this;
    }

    /// <summary>
    /// Fill between this dataset and the other dataset, specified by passing a dataset in the same chart.
    /// </summary>
    /// <param name="chartData">The chart data of the chart both datasets live in.</param>
    /// <param name="dataset">The other dataset to fill to.</param>
    /// <param name="relativeIndex">Whether to specify the fill index relative ("+/-n" string) or absolute (as zero-based int)</param>
    /// <returns>The dataset, for method chaining</returns>
    /// <exception cref="ArgumentException">If any of the datasets is not in the chart data, or if both datasets are the same.</exception>
    public LineChartDataset FillToDataset(ChartData chartData, IChartDataset dataset, bool relativeIndex = false)
    {
        var index = chartData?.Datasets?.IndexOf(dataset) ?? -1;

        if (index < 0)
            throw new ArgumentException("The dataset is not in the chart data.");

        if (relativeIndex)
        {
            if (chartData?.Datasets == null)
                throw new ArgumentException("The chart data or its datasets collection is null.");

            var myIndex = chartData.Datasets.IndexOf(this);

            if (myIndex < 0)
                throw new ArgumentException("The dataset is not in the chart data.");

            if (myIndex == index)
                throw new ArgumentException("The dataset is the same as this dataset.");

            Fill = (index - myIndex).ToString("+0;-0", CultureInfo.InvariantCulture);
        }
        else
        {
            Fill = index;
        }

        return this;
    }

    /// <summary>
    /// Fills between the current dataset and the top of the chart (fill: 'end').
    /// </summary>
    /// <returns>The dataset, for method chaining</returns>
    public LineChartDataset FillToEnd()
    {
        Fill = "end";

        return this;
    }

    /// <summary>
    /// Fills between the current dataset and the origin. For legacy reasons, this is the same as fill: true.
    /// </summary>
    /// <returns>The dataset, for method chaining</returns>
    public LineChartDataset FillToOrigin()
    {
        Fill = "origin";

        return this;
    }

    /// <summary>
    /// Fill to the line below the current dataset (fill: 'stack').
    /// </summary>
    /// <returns>The dataset, for method chaining</returns>
    public LineChartDataset FillToStackedValueBelow()
    {
        Fill = "stack";

        return this;
    }

    /// <summary>
    /// Fills between the current dataset and the start (fill: 'start').
    /// </summary>
    /// <returns>The dataset, for method chaining</returns>
    public LineChartDataset FillToStart()
    {
        Fill = "start";

        return this;
    }

    /// <summary>
    /// Fill to the line of the given constant value.
    /// </summary>
    /// <param name="value">The value to fill to</param>
    /// <returns>The dataset, for method chaining</returns>
    public LineChartDataset FillToValue(double value)
    {
        Fill = new { value };

        return this;
    }

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// The line fill color.
    /// Use <see cref="BackgroundColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("The line fill color.")]
    [JsonIgnore]
    public string BackgroundColor { get; set; } = "rgba(0, 0, 0, 0.1)";

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the background color, as the scriptable option <c>(context) =&gt; color</c>;
    /// it may also return a <c>CanvasGradient</c> or <c>CanvasPattern</c>. <c>context</c> is the Chart.js option context.
    /// Chart.js calls the function for the line itself with the dataset context, which has <c>context.chart</c> (for example
    /// <c>context.chart.chartArea</c>) and <c>context.datasetIndex</c>, but no <c>context.dataIndex</c> or <c>context.raw</c>.
    /// It calls the same function for each point, with the data context, only when the matching point color is not set.
    /// When set, it is sent instead of <see cref="BackgroundColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the background color, as the scriptable option (context) => color. When set, it is sent instead of BackgroundColor.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? BackgroundColorFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>backgroundColor</c>: <see cref="BackgroundColorFunction" /> when set, otherwise <see cref="BackgroundColor" />.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("backgroundColor")]
    internal object? BackgroundColorValue => (object?)BackgroundColorFunction ?? BackgroundColor;

    /// <summary>
    /// Cap style of the line.
    /// Supported values are 'butt', 'round', and 'square'.
    /// <para>
    /// Default value is 'butt'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("butt")]
    [Description("Cap style of the line. Supported values are 'butt', 'round', and 'square'.")]
    public string BorderCapStyle { get; set; } = "butt";

    /// <summary>
    /// The line color.
    /// Use <see cref="BorderColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("The line color.")]
    [JsonIgnore]
    public string BorderColor { get; set; } = "rgba(0, 0, 0, 0.1)";

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the border color, as the scriptable option <c>(context) =&gt; color</c>;
    /// it may also return a <c>CanvasGradient</c> or <c>CanvasPattern</c>. <c>context</c> is the Chart.js option context.
    /// Chart.js calls the function for the line itself with the dataset context, which has <c>context.chart</c> (for example
    /// <c>context.chart.chartArea</c>) and <c>context.datasetIndex</c>, but no <c>context.dataIndex</c> or <c>context.raw</c>.
    /// It calls the same function for each point, with the data context, only when the matching point color is not set.
    /// When set, it is sent instead of <see cref="BorderColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the border color, as the scriptable option (context) => color. When set, it is sent instead of BorderColor.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? BorderColorFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>borderColor</c>: <see cref="BorderColorFunction" /> when set, otherwise <see cref="BorderColor" />.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("borderColor")]
    internal object? BorderColorValue => (object?)BorderColorFunction ?? BorderColor;

    /// <summary>
    /// Length and spacing of dashes.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Length and spacing of dashes.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? BorderDash { get; set; }

    /// <summary>
    /// Offset for line dashes.
    /// <para>
    /// Default value is 0.0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0.0)]
    [Description("Offset for line dashes.")]
    public double BorderDashOffset { get; set; }

    /// <summary>
    /// Line joint style. 
    /// There are three possible values for this property: 'round', 'bevel', and 'miter'.
    /// <para>
    /// Default value is 'miter'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("miter")]
    [Description("Line joint style. There are three possible values for this property: 'round', 'bevel', and 'miter'.")]
    public string BorderJoinStyle { get; set; } = "miter";

    /// <summary>
    /// The line width (in pixels).
    /// <para>
    /// Default value is 3.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(3)]
    [Description("The line width (in pixels).")]
    public double BorderWidth { get; set; } = 3;

    //clip
    //https://www.chartjs.org/docs/latest/charts/line.html#general

    /// <summary>
    /// Supported values are 'default', and 'monotone'.
    /// <para>
    /// Default value is 'default'.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#cubicinterpolationmode" />.
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("default")]
    [Description("Supported values are 'default', and 'monotone'.")]
    public string CubicInterpolationMode { get; set; } = "default";

    /// <summary>
    /// Gets or sets the data labels configuration for the line chart dataset.
    /// <para>
    /// Use this property to customize the display of data labels, such as their position,
    /// formatting, and visibility, for the dataset in the line chart.
    /// </para>
    /// <see href="https://chartjs-plugin-datalabels.netlify.app/guide/getting-started.html#configuration" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the data labels configuration for the line chart dataset. Use this property to customize the display of data labels, such as their position, formatting, and visibility, for the dataset in the line chart.")]
    [ParameterTypeName("LineChartDatasetDataLabels")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public LineChartDatasetDataLabels Datalabels { get; set; } = new();

    /// <summary>
    /// Draw the active points of a dataset over the other points of the dataset.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Draw the active points of a dataset over the other points of the dataset.")]
    [ParameterTypeName("List<bool>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<bool>? DrawActiveElementsOnTop { get; set; }

    /// <summary>
    /// How to fill the area under the line.
    /// <para>
    /// Default value is <see langword="false"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("How to fill the area under the line.")]
    public object Fill { get; set; } = false;

    /// <summary>
    /// The line fill color when hovered.
    /// Use <see cref="HoverBackgroundColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The line fill color when hovered.")]
    [ParameterTypeName("string?")]
    [JsonIgnore]
    public string? HoverBackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the background color when hovered, as the scriptable option <c>(context) =&gt; color</c>;
    /// it may also return a <c>CanvasGradient</c> or <c>CanvasPattern</c>. <c>context</c> is the Chart.js option context.
    /// Chart.js calls the function for the line itself with the dataset context, which has <c>context.chart</c> (for example
    /// <c>context.chart.chartArea</c>) and <c>context.datasetIndex</c>, but no <c>context.dataIndex</c> or <c>context.raw</c>.
    /// It calls the same function for each point, with the data context, only when the matching point color is not set.
    /// When set, it is sent instead of <see cref="HoverBackgroundColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the background color when hovered, as the scriptable option (context) => color. When set, it is sent instead of HoverBackgroundColor.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? HoverBackgroundColorFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>hoverBackgroundColor</c>: <see cref="HoverBackgroundColorFunction" /> when set, otherwise <see cref="HoverBackgroundColor" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("hoverBackgroundColor")]
    internal object? HoverBackgroundColorValue => (object?)HoverBackgroundColorFunction ?? HoverBackgroundColor;

    /// <summary>
    /// Cap style of the line when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Cap style of the line when hovered.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HoverBorderCapStyle { get; set; }

    /// <summary>
    /// The line color when hovered.
    /// Use <see cref="HoverBorderColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The line color when hovered.")]
    [ParameterTypeName("string?")]
    [JsonIgnore]
    public string? HoverBorderColor { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the border color when hovered, as the scriptable option <c>(context) =&gt; color</c>;
    /// it may also return a <c>CanvasGradient</c> or <c>CanvasPattern</c>. <c>context</c> is the Chart.js option context.
    /// Chart.js calls the function for the line itself with the dataset context, which has <c>context.chart</c> (for example
    /// <c>context.chart.chartArea</c>) and <c>context.datasetIndex</c>, but no <c>context.dataIndex</c> or <c>context.raw</c>.
    /// It calls the same function for each point, with the data context, only when the matching point color is not set.
    /// When set, it is sent instead of <see cref="HoverBorderColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the border color when hovered, as the scriptable option (context) => color. When set, it is sent instead of HoverBorderColor.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? HoverBorderColorFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>hoverBorderColor</c>: <see cref="HoverBorderColorFunction" /> when set, otherwise <see cref="HoverBorderColor" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("hoverBorderColor")]
    internal object? HoverBorderColorValue => (object?)HoverBorderColorFunction ?? HoverBorderColor;

    /// <summary>
    /// Length and spacing of dashes when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Length and spacing of dashes when hovered.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderDash { get; set; }

    /// <summary>
    /// Offset for line dashes when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Offset for line dashes when hovered.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? HoverBorderDashOffset { get; set; }

    /// <summary>
    /// Line joint style when hovered.
    /// There are three possible values for this property: 'round', 'bevel', and 'miter'.
    /// <para>
    /// Default value is 'miter'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("miter")]
    [Description("Line joint style. There are three possible values for this property: 'round', 'bevel', and 'miter'.")]
    public string HoverBorderJoinStyle { get; set; } = "miter";

    /// <summary>
    /// The line width (in pixels) when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The line width (in pixels) when hovered.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? HoverBorderWidth { get; set; }

    /// <summary>
    /// The base axis of the dataset. 'x' for horizontal lines and 'y' for vertical lines.
    /// <para>
    /// Default value is 'x'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("x")]
    [Description("The base axis of the dataset. 'x' for horizontal lines and 'y' for vertical lines.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IndexAxis { get; set; } = "x";

    /// <summary>
    /// The longest gap to span, in units of the index axis: lines are drawn across gaps up to this length and break at longer ones.
    /// When set, it is sent as <c>spanGaps</c> instead of <see cref="SpanGaps" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#line-styling" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("The longest gap to span, in units of the index axis: lines are drawn across gaps up to this length and break at longer ones.")]
    [JsonIgnore]
    [ParameterTypeName("double?")]
    public double? MaxSpanGap { get; set; }

    /// <summary>
    /// The fill color for points.
    /// Use <see cref="PointBackgroundColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("The fill color for points.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore]
    public List<string>? PointBackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the point fill color, as the scriptable option <c>(context) =&gt; color</c>;
    /// it may also return a <c>CanvasGradient</c> or <c>CanvasPattern</c>. <c>context</c> is the Chart.js option context, for example
    /// <c>context.dataIndex</c>, <c>context.raw</c> and <c>context.chart</c>.
    /// When set, it is sent instead of <see cref="PointBackgroundColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the point fill color, as the scriptable option (context) => color. When set, it is sent instead of PointBackgroundColor.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? PointBackgroundColorFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>pointBackgroundColor</c>: <see cref="PointBackgroundColorFunction" /> when set, otherwise <see cref="PointBackgroundColor" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("pointBackgroundColor")]
    internal object? PointBackgroundColorValue => (object?)PointBackgroundColorFunction ?? PointBackgroundColor;

    /// <summary>
    /// The border color for points.
    /// Use <see cref="PointBorderColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("The border color for points.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore]
    public List<string>? PointBorderColor { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the point border color, as the scriptable option <c>(context) =&gt; color</c>;
    /// it may also return a <c>CanvasGradient</c> or <c>CanvasPattern</c>. <c>context</c> is the Chart.js option context, for example
    /// <c>context.dataIndex</c>, <c>context.raw</c> and <c>context.chart</c>.
    /// When set, it is sent instead of <see cref="PointBorderColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the point border color, as the scriptable option (context) => color. When set, it is sent instead of PointBorderColor.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? PointBorderColorFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>pointBorderColor</c>: <see cref="PointBorderColorFunction" /> when set, otherwise <see cref="PointBorderColor" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("pointBorderColor")]
    internal object? PointBorderColorValue => (object?)PointBorderColorFunction ?? PointBorderColor;

    /// <summary>
    /// The width of the point border in pixels.
    /// <para>
    /// Default value is 1.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(1)]
    [Description("The width of the point border in pixels.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? PointBorderWidth { get; set; }

    /// <summary>
    /// The pixel size of the non-displayed point that reacts to mouse events.
    /// <para>
    /// Default value is 1.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(1)]
    [Description("The pixel size of the non-displayed point that reacts to mouse events.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? PointHitRadius { get; set; }

    /// <summary>
    /// Point background color when hovered.
    /// Use <see cref="PointHoverBackgroundColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Point background color when hovered.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore]
    public List<string>? PointHoverBackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the point fill color when hovered, as the scriptable option <c>(context) =&gt; color</c>;
    /// it may also return a <c>CanvasGradient</c> or <c>CanvasPattern</c>. <c>context</c> is the Chart.js option context, for example
    /// <c>context.dataIndex</c>, <c>context.raw</c> and <c>context.chart</c>.
    /// When set, it is sent instead of <see cref="PointHoverBackgroundColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the point fill color when hovered, as the scriptable option (context) => color. When set, it is sent instead of PointHoverBackgroundColor.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? PointHoverBackgroundColorFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>pointHoverBackgroundColor</c>: <see cref="PointHoverBackgroundColorFunction" /> when set, otherwise <see cref="PointHoverBackgroundColor" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("pointHoverBackgroundColor")]
    internal object? PointHoverBackgroundColorValue => (object?)PointHoverBackgroundColorFunction ?? PointHoverBackgroundColor;

    /// <summary>
    /// Point border color when hovered.
    /// Use <see cref="PointHoverBorderColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Point border color when hovered.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore]
    public List<string>? PointHoverBorderColor { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the point border color when hovered, as the scriptable option <c>(context) =&gt; color</c>;
    /// it may also return a <c>CanvasGradient</c> or <c>CanvasPattern</c>. <c>context</c> is the Chart.js option context, for example
    /// <c>context.dataIndex</c>, <c>context.raw</c> and <c>context.chart</c>.
    /// When set, it is sent instead of <see cref="PointHoverBorderColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the point border color when hovered, as the scriptable option (context) => color. When set, it is sent instead of PointHoverBorderColor.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? PointHoverBorderColorFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>pointHoverBorderColor</c>: <see cref="PointHoverBorderColorFunction" /> when set, otherwise <see cref="PointHoverBorderColor" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("pointHoverBorderColor")]
    internal object? PointHoverBorderColorValue => (object?)PointHoverBorderColorFunction ?? PointHoverBorderColor;

    /// <summary>
    /// Border width of point when hovered.
    /// <para>
    /// Default value is 1.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(1)]
    [Description("Border width of point when hovered.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? PointHoverBorderWidth { get; set; }

    /// <summary>
    /// The radius of the point when hovered.
    /// <para>
    /// Default value is 4.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(4)]
    [Description("The radius of the point when hovered.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? PointHoverRadius { get; set; }

    /// <summary>
    /// The radius of the point shape. If set to 0, the point is not rendered.
    /// <para>
    /// Default value is 3.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(3)]
    [Description("The radius of the point shape. If set to 0, the point is not rendered.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? PointRadius { get; set; }

    /// <summary>
    /// The rotation of the point in degrees.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("The rotation of the point in degrees.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? PointRotation { get; set; }

    /// <summary>
    /// Style of the point.
    /// Supported values are 'circle', 'cross', 'crossRot', 'dash', 'line', 'rect', 'rectRounded', 'rectRot', 'star', and 'triangle'.
    /// <para>
    /// Default value is 'circle'.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/elements.html#point-styles" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("circle")]
    [Description("Style of the point. Supported values are 'circle', 'cross', 'crossRot', 'dash', 'line', 'rect', 'rectRounded', 'rectRot', 'star', and 'triangle'.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? PointStyle { get; set; }

    /// <summary>
    /// Line segment styles can be overridden by scriptable options in the segment object. Currently, all of the Border* and <see cref="BackgroundColor" /> options are supported. 
    /// The segment styles are resolved for each section of the line between each point.
    /// Use <see cref="SegmentStyle" /> for typed options and registered JavaScript functions; when set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("1.2.0")]
    [DefaultValue(null)]
    [Description("Line segment styles can be overridden by scriptable options in the segment object. Currently, all of the Border* and <code>BackgroundColor</code> options are supported. The segment styles are resolved for each section of the line between each point.")]
    [ParameterTypeName("object?")]
    [JsonIgnore]
    public object? Segment { get; set; }

    /// <summary>
    /// Gets or sets the style of each line segment, as values or registered JavaScript functions, for example
    /// <c>SegmentStyle = new() { BorderDashFunction = new ChartFunction("dashGaps") }</c>. When set, it is sent as <c>segment</c> instead of <see cref="Segment" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the style of each line segment, as values or registered JavaScript functions. When set, it is sent as segment instead of Segment.")]
    [JsonIgnore]
    [ParameterTypeName("ChartLineSegment?")]
    public ChartLineSegment? SegmentStyle { get; set; }

    /// <summary>
    /// Gets the value sent as <c>segment</c>: <see cref="SegmentStyle" /> when set, otherwise <see cref="Segment" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("segment")]
    internal object? SegmentValue => (object?)SegmentStyle ?? Segment;

    /// <summary>
    /// If <see langword="false" />, the line is not drawn for this dataset.
    /// <para>
    /// Default value is <see langword="true"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("If false, the line is not drawn for this dataset.")]
    public bool ShowLine { get; set; } = true;

    /// <summary>
    /// If <see langword="true" />, lines will be drawn between points with no or null data.
    /// If <see langword="false" />, points with null data will create a break in the line.
    /// Use <see cref="MaxSpanGap" /> to span only gaps up to a given length; when set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("If true, lines will be drawn between points with no or null data. If false, points with null data will create a break in the line. Use MaxSpanGap to span only gaps up to a given length.")]
    [JsonIgnore]
    [ParameterTypeName("bool?")]
    public bool? SpanGaps { get; set; }

    /// <summary>
    /// Gets the value sent as <c>spanGaps</c>: <see cref="MaxSpanGap" /> when set, otherwise <see cref="SpanGaps" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("spanGaps")]
    internal object? SpanGapsValue => MaxSpanGap is double maxSpanGap ? maxSpanGap : SpanGaps;

    /// <summary>
    /// If the stepped value is set to anything other than <see langword="false"/>, tension will be ignored.
    /// The following values are supported for stepped.
    /// - false: No Step Interpolation(default)
    /// - true: Step-before Interpolation(eq. 'before')
    /// - 'before': Step-before Interpolation
    /// - 'after': Step-after Interpolation
    /// - 'middle': Step-middle Interpolation
    /// <para>
    /// Default value is <see langword="false"/>.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#stepped" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("If the stepped value is set to anything other than false, tension will be ignored. <br /> <div>The following values are supported for stepped. <ol class=\"pl-4\"><li><b>false</b>: No Step Interpolation(default)</li><li><b>true</b>: Step-before Interpolation(eq. 'before')</li><li><b>'before'</b>: Step-before Interpolation</li><li><b>'after'</b>: Step-after Interpolation</li><li><b>'middle'</b>: Step-middle Interpolation</ol></div>")]
    public object Stepped { get; set; } = false;

    /// <summary>
    /// Bezier curve tension of the line. Set to 0 to draw straight lines.
    /// This option is ignored if monotone cubic interpolation is used.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("Bezier curve tension of the line. Set to 0 to draw straightlines. This option is ignored if monotone cubic interpolation is used.")]
    public double Tension { get; set; }

    /// <summary>
    /// The values of a time or timeseries x-axis, each at its own date, for example
    /// <c>TimeData = [new(new DateTime(2026, 1, 5, 9, 0, 0), 21.5), new(new DateTime(2026, 1, 5, 9, 7, 0), 22.1)]</c>.
    /// A <see langword="null" /> <c>Y</c> leaves a gap. When set, it is sent instead of <c>Data</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/axes/cartesian/time.html" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("The values of a time or timeseries x-axis, each at its own date. A null Y leaves a gap. When set, it is sent instead of Data.")]
    [JsonIgnore]
    [ParameterTypeName("List<ChartTimePoint>?")]
    public List<ChartTimePoint>? TimeData { get; set; }

    /// <summary>
    /// Gets the value sent as <c>data</c>: <see cref="TimeData" /> when set, otherwise <c>Data</c>.
    /// </summary>
    internal override object? DataValue => (object?)TimeData ?? Data;

    /// <summary>
    /// The ID of the x axis to plot this dataset on.
    /// <para>
    /// Default value is 'first x axis'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("Default value is 'first x axis'.")]
    [Description("The ID of the x-axis to plot this dataset on. Default value is 'first x axis'.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? XAxisID { get; set; }

    /// <summary>
    /// The ID of the y axis to plot this dataset on.
    /// <para>
    /// Default value is 'first y axis'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("Default value is 'first y axis'.")]
    [Description("The ID of the y-axis to plot this dataset on. Default value is 'first y axis'.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? YAxisID { get; set; }

    #endregion
}
