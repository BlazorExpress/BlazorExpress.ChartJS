namespace BlazorExpress.ChartJS;

/// <summary>
/// The plugin options of a bubble chart.
/// </summary>
public class BubbleChartPlugins : ChartPlugins
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the default options of the chartjs-plugin-datalabels plugin, used when it is registered.
    /// <para>
    /// Default value is a new <see cref="BubbleChartDataLabels" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the default options of the chartjs-plugin-datalabels plugin, used when it is registered.")]
    public BubbleChartDataLabels Datalabels { get; set; } = new();

    #endregion
}

/// <summary>
/// The default datalabels options of a bubble chart.
/// <para>
/// See <see href="https://chartjs-plugin-datalabels.netlify.app/guide/options.html" /> for more information.
/// </para>
/// </summary>
public class BubbleChartDataLabels
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the border radius of the labels, in pixels.
    /// <para>
    /// Default value is 4.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(4)]
    [Description("Gets or sets the border radius of the labels, in pixels.")]
    public double BorderRadius { get; set; } = 4;

    /// <summary>
    /// Gets or sets the text color of the labels.
    /// <para>
    /// Default value is <c>"white"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("white")]
    [Description("Gets or sets the text color of the labels.")]
    [ParameterTypeName("string?")]
    public string? Color { get; set; } = "white";

    /// <summary>
    /// Gets or sets the font of the labels.
    /// <para>
    /// Default value is a new <see cref="BubbleChartDataLabelsFont" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the font of the labels.")]
    public BubbleChartDataLabelsFont Font { get; set; } = new();

    /// <summary>
    /// Gets or sets the padding of the labels, in pixels.
    /// <para>
    /// Default value is 6.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(6)]
    [Description("Gets or sets the padding of the labels, in pixels.")]
    public double Padding { get; set; } = 6;

    #endregion
}

/// <summary>
/// The font of the bubble chart datalabels.
/// </summary>
public class BubbleChartDataLabelsFont
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the font weight.
    /// <para>
    /// Default value is <c>"bold"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("bold")]
    [Description("Gets or sets the font weight.")]
    [ParameterTypeName("string?")]
    public string? Weight { get; set; } = "bold";

    #endregion
}
