namespace BlazorExpress.ChartJS;

/// <summary>
/// The plugin options of a polar area chart.
/// </summary>
public class PolarAreaChartPlugins : ChartPlugins
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the default options of the chartjs-plugin-datalabels plugin, used when it is registered.
    /// <para>
    /// Default value is a new <see cref="PolarAreaChartDataLabels" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.1.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the default options of the chartjs-plugin-datalabels plugin, used when it is registered.")]
    public PolarAreaChartDataLabels Datalabels { get; set; } = new();

    #endregion
}

/// <summary>
/// The default datalabels options of a polar area chart.
/// <para>
/// See <see href="https://chartjs-plugin-datalabels.netlify.app/guide/options.html" /> for more information.
/// </para>
/// </summary>
public class PolarAreaChartDataLabels
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the border color of the labels.
    /// <para>
    /// Default value is <c>"white"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.1.0")]
    [DefaultValue("white")]
    [Description("Gets or sets the border color of the labels.")]
    [ParameterTypeName("string?")]
    public string? BorderColor { get; set; } = "white";

    /// <summary>
    /// Gets or sets the border radius of the labels, in pixels.
    /// <para>
    /// Default value is 25.
    /// </para>
    /// </summary>
    [AddedVersion("1.1.0")]
    [DefaultValue(25)]
    [Description("Gets or sets the border radius of the labels, in pixels.")]
    public double BorderRadius { get; set; } = 25;

    /// <summary>
    /// Gets or sets the border width of the labels, in pixels.
    /// <para>
    /// Default value is 2.
    /// </para>
    /// </summary>
    [AddedVersion("1.1.0")]
    [DefaultValue(2)]
    [Description("Gets or sets the border width of the labels, in pixels.")]
    public double BorderWidth { get; set; } = 2;

    /// <summary>
    /// Gets or sets the text color of the labels.
    /// <para>
    /// Default value is <c>"white"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.1.0")]
    [DefaultValue("white")]
    [Description("Gets or sets the text color of the labels.")]
    [ParameterTypeName("string?")]
    public string? Color { get; set; } = "white";

    /// <summary>
    /// Gets or sets the font of the labels.
    /// <para>
    /// Default value is a new <see cref="PolarAreaChartDataLabelsFont" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.1.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the font of the labels.")]
    public PolarAreaChartDataLabelsFont Font { get; set; } = new();

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the text of each label of the chart, as <c>(value, context) =&gt; string</c>.
    /// Return an array for a label of several lines, or <see langword="null" /> to hide the label. It changes the label text only;
    /// the library's datalabels background color is unaffected.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://chartjs-plugin-datalabels.netlify.app/guide/formatting.html" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the text of each label of the chart, as (value, context) => string. It changes the label text only; the library's datalabels background color is unaffected.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? Formatter { get; set; }

    /// <summary>
    /// Gets or sets the padding of the labels, in pixels.
    /// <para>
    /// Default value is 6.
    /// </para>
    /// </summary>
    [AddedVersion("1.1.0")]
    [DefaultValue(6)]
    [Description("Gets or sets the padding of the labels, in pixels.")]
    public double Padding { get; set; } = 6;

    #endregion
}

/// <summary>
/// The font of the polar area chart datalabels.
/// </summary>
public class PolarAreaChartDataLabelsFont
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the font weight.
    /// <para>
    /// Default value is <c>"bold"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.1.0")]
    [DefaultValue("bold")]
    [Description("Gets or sets the font weight.")]
    [ParameterTypeName("string?")]
    public string? Weight { get; set; } = "bold";

    #endregion
}
