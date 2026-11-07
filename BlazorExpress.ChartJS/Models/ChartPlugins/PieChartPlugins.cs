namespace BlazorExpress.ChartJS;

/// <summary>
/// The plugin options of a pie chart.
/// </summary>
public class PieChartPlugins : ChartPlugins
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the default options of the chartjs-plugin-datalabels plugin, used when it is registered.
    /// <para>
    /// Default value is a new <see cref="PieChartDataLabels" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the default options of the chartjs-plugin-datalabels plugin, used when it is registered.")]
    public PieChartDataLabels Datalabels { get; set; } = new();

    #endregion
}

/// <summary>
/// The default datalabels options of a pie chart.
/// <para>
/// See <see href="https://chartjs-plugin-datalabels.netlify.app/guide/options.html" /> for more information.
/// </para>
/// </summary>
public class PieChartDataLabels
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the border color of the labels.
    /// <para>
    /// Default value is <c>"white"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
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
    [AddedVersion("1.0.0")]
    [DefaultValue(25)]
    [Description("Gets or sets the border radius of the labels, in pixels.")]
    public double BorderRadius { get; set; } = 25;

    /// <summary>
    /// Gets or sets the border width of the labels, in pixels.
    /// <para>
    /// Default value is 2.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(2)]
    [Description("Gets or sets the border width of the labels, in pixels.")]
    public double BorderWidth { get; set; } = 2;

    /// <summary>
    /// Gets or sets the text color of the labels.
    /// <para>
    /// Default value is <c>"white"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("white")]
    [Description("Gets or sets the text color of the labels.")]
    [ParameterTypeName("string?")]
    public string? Color { get; set; } = "white";

    /// <summary>
    /// Gets or sets the font of the labels.
    /// <para>
    /// Default value is a new <see cref="PieChartDataLabelsFont" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the font of the labels.")]
    public PieChartDataLabelsFont Font { get; set; } = new();

    /// <summary>
    /// Gets or sets the padding of the labels, in pixels.
    /// <para>
    /// Default value is 6.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(6)]
    [Description("Gets or sets the padding of the labels, in pixels.")]
    public double Padding { get; set; } = 6;

    #endregion
}

/// <summary>
/// The font of the pie chart datalabels.
/// </summary>
public class PieChartDataLabelsFont
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the font weight.
    /// <para>
    /// Default value is <c>"bold"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("bold")]
    [Description("Gets or sets the font weight.")]
    [ParameterTypeName("string?")]
    public string? Weight { get; set; } = "bold";

    #endregion
}
