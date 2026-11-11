namespace BlazorExpress.ChartJS;

/// <summary>
/// A radar chart is a way of showing multiple data points and the variation between them.
/// They are often useful for comparing the points of two or more different data sets.
/// <see href="https://www.chartjs.org/docs/latest/charts/radar.html#dataset-properties" />.
/// </summary>
public class RadarChartDataset : ChartDataset<double?>
{
    #region Properties, Indexers

    /// <summary>
    /// Get or sets the line fill color.
    /// Use <see cref="BackgroundColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("Get or sets the line fill color.")]
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
    [Description("Cap style of the line. Supported values are <b>butt</b>, <b>round</b>, and <b>square</b>.")]
    public string BorderCapStyle { get; set; } = "butt";

    /// <summary>
    /// Get or sets the line color.
    /// Use <see cref="BorderColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("Get or sets the line color.")]
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
    /// Gets or sets the length and spacing of dashes.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the length and spacing of dashes.")]
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
    [Description("Line joint style. There are three possible values for this property: <b>round</b>, <b>bevel</b>, and <b>miter</b>.")]
    public string BorderJoinStyle { get; set; } = "miter";

    /// <summary>
    /// Gets or sets the line width (in pixels).
    /// <para>
    /// Default value is 3.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(3)]
    [Description("Gets or sets the line width (in pixels).")]
    public double BorderWidth { get; set; } = 3;

    /// <summary>
    /// Gets or sets the data labels configuration for the radar chart dataset.
    /// <para>
    /// Use this property to customize the display of data labels, such as their position, format, or
    /// visibility, in the pie chart dataset. If not set, a default configuration is applied.
    /// </para>
    /// <see href="https://chartjs-plugin-datalabels.netlify.app/guide/getting-started.html#configuration" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the data labels configuration for the radar chart dataset. Use this property to customize the display of data labels, such as their position, format, or visibility, in the radar chart dataset. If not set, a default configuration is applied.")]
    [ParameterTypeName(nameof(RadarChartDatasetDataLabels))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RadarChartDatasetDataLabels Datalabels { get; set; } = new();

    /// <summary>
    /// How to fill the area under the line.
    /// <para>
    /// Default value is <see langword="false" />.
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
    /// Default value is <see langword="null" />.
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
    /// Default value is <see langword="null" />.
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
    /// Default value is <see langword="null" />.
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
    /// Gets or sets the length and spacing of dashes when hovered.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the length and spacing of dashes when hovered.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderDash { get; set; }

    /// <summary>
    /// Offset for line dashes when hovered.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Offset for line dashes when hovered.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? HoverBorderDashOffset { get; set; }

    /// <summary>
    /// Line joint style.
    /// There are three possible values for this property: 'round', 'bevel', and 'miter'.
    /// <para>
    /// Default value is 'miter'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Line joint style. There are three possible values for this property: 'round', 'bevel', and 'miter'.")]
    public string HoverBorderJoinStyle { get; set; } = "miter";

    /// <summary>
    /// The bar border width when hovered (in pixels) when hovered.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The bar border width when hovered (in pixels) when hovered.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? HoverBorderWidth { get; set; }

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
    /// Supported values are 'circle', 'cross', 'crossRot', 'dash', 'line', 'rect', 'rectRounded', 'rectRot', 'star', 'triangle' and false.
    /// the point.
    /// <para>
    /// Default value is 'circle'.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/elements.html#point-styles" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("circle")]
    [Description("Style of the point. Supported values are 'circle', 'cross', 'crossRot', 'dash', 'line', 'rect', 'rectRounded', 'rectRot', 'star', 'triangle' and false.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? PointStyle { get; set; }

    /// <summary>
    /// The ID of the radial (<c>r</c>) axis to plot this dataset on.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses the first <c>r</c> axis).
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("The ID of the radial (r) axis to plot this dataset on.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RAxisID { get; set; }

    /// <summary>
    /// If <see langword="true" />, lines will be drawn between points with no or null data.
    /// If <see langword="false" />, points with null data will create a break in the line.
    /// Can also be a number specifying the maximum gap length to span.
    /// The unit of the value depends on the scale used.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("If <b>true</b>, lines will be drawn between points with no or null data. If <b>false</b>, points with null data will create a break in the line.")]
    [ParameterTypeName("bool?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? SpanGaps { get; set; }

    /// <summary>
    /// Bezier curve tension of the line. Set to 0 to draw straight lines.
    /// This option is ignored if monotone cubic interpolation is used.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("Bezier curve tension of the line. Set to 0 to draw straight lines. This option is ignored if monotone cubic interpolation is used.")]
    public double Tension { get; set; }

    #endregion
}
