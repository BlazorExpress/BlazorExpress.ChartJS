namespace BlazorExpress.ChartJS;

public class PolarAreaChartDataset : ChartDataset<double?>
{
    #region Properties, Indexers

    /// <summary>
    /// Arc background color.
    /// Use <see cref="BackgroundColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("Arc background color.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore]
    public List<string>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the background color, as the scriptable option <c>(context) =&gt; color</c>;
    /// it may also return a <c>CanvasGradient</c> or <c>CanvasPattern</c>. <c>context</c> is the Chart.js option context, for example
    /// <c>context.dataIndex</c>, <c>context.raw</c> and <c>context.chart</c>.
    /// When set, it is sent instead of <see cref="BackgroundColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the background color, as the scriptable option (context) => color. When set, it is sent instead of BackgroundColor.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? BackgroundColorFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>backgroundColor</c>: <see cref="BackgroundColorFunction" /> when set, otherwise <see cref="BackgroundColor" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("backgroundColor")]
    internal object? BackgroundColorValue => (object?)BackgroundColorFunction ?? BackgroundColor;

    /// <summary>
    /// Supported values are 'center' and 'inner'.
    /// When 'center' is set, the borders of arcs next to each other will overlap. 
    /// When 'inner' is set, it is guaranteed that all borders will not overlap.
    /// <para>
    /// Default value is 'center'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("center")]
    [Description("Supported values are <b>center</b> and <b>inner</b>. When <b>center</b> is set, the borders of arcs next to each other will overlap. When <b>inner</b> is set, it is guaranteed that all borders will not overlap.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? BorderAlign { get; set; } // TODO: change this to enum

    /// <summary>
    /// Arc border color.
    /// Use <see cref="BorderColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is '#fff'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("#fff")]
    [Description("Arc border color.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore]
    public List<string>? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the border color, as the scriptable option <c>(context) =&gt; color</c>;
    /// it may also return a <c>CanvasGradient</c> or <c>CanvasPattern</c>. <c>context</c> is the Chart.js option context, for example
    /// <c>context.dataIndex</c>, <c>context.raw</c> and <c>context.chart</c>.
    /// When set, it is sent instead of <see cref="BorderColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the border color, as the scriptable option (context) => color. When set, it is sent instead of BorderColor.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? BorderColorFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>borderColor</c>: <see cref="BorderColorFunction" /> when set, otherwise <see cref="BorderColor" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("borderColor")]
    internal object? BorderColorValue => (object?)BorderColorFunction ?? BorderColor;

    /// <summary>
    /// Arc border length and spacing of dashes.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border length and spacing of dashes.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? BorderDash { get; set; }

    /// <summary>
    /// Arc border offset for line dashes.
    /// <para>
    /// Default value is 0.0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0.0)]
    [Description("Arc border offset for line dashes.")]
    public double BorderDashOffset { get; set; }

    /// <summary>
    /// Arc border join style.
    /// Supported values are 'round', 'bevel', and 'miter'.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border join style. Supported values are <b>round</b>, <b>bevel</b>, and <b>miter</b>.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? BorderJoinStyle { get; set; } // TODO: change this to enum

    /// <summary>
    /// Arc border width (in pixels).
    /// <para>
    /// Default value is 2.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(2)]
    [Description("Arc border width (in pixels).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? BorderWidth { get; set; }

    /// <summary>
    /// By default the Arc is curved. If <see langword="false"/>, the Arc will be flat.
    /// <para>
    /// Default value is <see langword="true"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("By default the Arc is curved. If <b>false</b>, the Arc will be flat.")]
    public bool Circular { get; set; } = true;

    /// <summary>
    /// Gets or sets the data labels configuration for the polar area chart dataset.
    /// <para>
    /// Use this property to customize the display of data labels, such as their position, format, or
    /// visibility, in the pie chart dataset. If not set, a default configuration is applied.
    /// </para>
    /// <see href="https://chartjs-plugin-datalabels.netlify.app/guide/getting-started.html#configuration" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the data labels configuration for the polar area chart dataset. Use this property to customize the display of data labels, such as their position, format, or visibility, in the polar area chart dataset. If not set, a default configuration is applied.")]
    [ParameterTypeName(nameof(PolarAreaChartDatasetDataLabels))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PolarAreaChartDatasetDataLabels Datalabels { get; set; } = new();

    /// <summary>
    /// Arc background color when hovered.
    /// Use <see cref="HoverBackgroundColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc background color when hovered.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore]
    public List<string>? HoverBackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the background color when hovered, as the scriptable option <c>(context) =&gt; color</c>;
    /// it may also return a <c>CanvasGradient</c> or <c>CanvasPattern</c>. <c>context</c> is the Chart.js option context, for example
    /// <c>context.dataIndex</c>, <c>context.raw</c> and <c>context.chart</c>.
    /// When set, it is sent instead of <see cref="HoverBackgroundColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" />
    /// </summary>
    [AddedVersion("1.3.0")]
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
    /// Arc border color when hovered.
    /// Use <see cref="HoverBorderColorFunction" /> for a registered JavaScript function; when set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border color when hovered.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore]
    public List<string>? HoverBorderColor { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the border color when hovered, as the scriptable option <c>(context) =&gt; color</c>;
    /// it may also return a <c>CanvasGradient</c> or <c>CanvasPattern</c>. <c>context</c> is the Chart.js option context, for example
    /// <c>context.dataIndex</c>, <c>context.raw</c> and <c>context.chart</c>.
    /// When set, it is sent instead of <see cref="HoverBorderColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" />
    /// </summary>
    [AddedVersion("1.3.0")]
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
    /// Arc border length and spacing of dashes when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border length and spacing of dashes when hovered.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderDash { get; set; }

    /// <summary>
    /// Arc border offset for line dashes when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border offset for line dashes when hovered.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? HoverBorderDashOffset { get; set; }

    /// <summary>
    /// Arc border join style when hovered.
    /// Supported values are 'round', 'bevel', and 'miter'.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border join style when hovered. Supported values are <b>round</b>, <b>bevel</b>, and <b>miter</b>.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? HoverBorderJoinStyle { get; set; } // TODO: change this to enum

    /// <summary>
    /// Arc border width when hovered (in pixels).
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border width when hovered (in pixels).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderWidth { get; set; }

    /// <summary>
    /// The gap between arcs, in pixels. <see cref="SpacingMode" /> sets how the gap is measured.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 0).
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/polar.html" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("The gap between arcs, in pixels. SpacingMode sets how the gap is measured.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Spacing { get; set; }

    /// <summary>
    /// How the spacing between arcs is measured: <c>"angular"</c> keeps an equal angle, <c>"proportional"</c> scales with each arc,
    /// and <c>"parallel"</c> keeps the edges of neighbouring arcs parallel.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses <c>"proportional"</c>).
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/polar.html" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("How the spacing between arcs is measured: \"angular\", \"proportional\" or \"parallel\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? SpacingMode { get; set; }

    #endregion
}
