namespace BlazorExpress.ChartJS;

public class LineChartOptions : ChartOptions
{
    #region Properties, Indexers

    //hover -> mode, intersect
    //maintainAspectRatio
    //plugins -> title -> display, text

    /// <summary>
    /// The base axis of the dataset. 'x' for horizontal lines and 'y' for vertical lines.
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The base axis of the dataset. 'x' for horizontal lines and 'y' for vertical lines.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IndexAxis { get; set; }

    /// <summary>
    /// Gets or sets the interaction options for the chart.
    /// By default, these options apply to both the hover and tooltip interactions.
    /// <para>
    /// Default value is a new instance of <see cref="Interaction"/>.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/interactions.html#interactions" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the interaction options for the chart.")]
    public Interaction Interaction { get; set; } = new();

    /// <summary>
    /// Gets or sets the layout configuration for the chart.
    /// <para>
    /// Default value is a new instance of <see cref="ChartLayout"/>.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/layout.html#layout" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the layout configuration for the chart.")]
    [ParameterTypeName("ChartLayout")]
    public ChartLayout Layout { get; set; } = new();

    /// <summary>
    /// Gets or sets the collection of plugins associated with the line chart.
    /// <para>
    /// Default value is a new instance of <see cref="LineChartPlugins"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the collection of plugins associated with the line chart.")]
    [ParameterTypeName("LineChartPlugins")]
    public LineChartPlugins Plugins { get; set; } = new();

    /// <summary>
    /// Gets or sets the collection of scales used for measurement or calibration.
    /// <para>
    /// Default value is a new instance of <see cref="Scales"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the collection of scales used for measurement or calibration.")]
    public Scales Scales { get; set; } = new();

    #endregion

    //tooltips -> mode, intersect
}
