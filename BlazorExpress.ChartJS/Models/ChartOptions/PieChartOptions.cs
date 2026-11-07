namespace BlazorExpress.ChartJS;

/// <summary>
/// Provides configuration options specific to pie charts.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/charts/doughnut.html" /> for more information.
/// </para>
/// </summary>
public class PieChartOptions : ChartOptions
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the sweep of the arcs, in degrees; 180 draws a half circle.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 360).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the sweep of the arcs, in degrees; 180 draws a half circle.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Circumference { get; set; }

    /// <summary>
    /// Gets or sets the size of the hole in the middle: pixels, or a percentage of the radius such as "50%".
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 0).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the size of the hole in the middle: pixels, or a percentage of the radius such as \"50%\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartDimension?")]
    public ChartDimension? Cutout { get; set; }

    /// <summary>
    /// Gets or sets the interaction options of the pie chart, which also apply to hover and tooltips unless they set their own.
    /// <para>
    /// Default value is a new <see cref="Interaction" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the interaction options of the pie chart, which also apply to hover and tooltips unless they set their own.")]
    public Interaction Interaction { get; set; } = new();

    /// <summary>
    /// Gets or sets the layout options of the pie chart.
    /// <para>
    /// Default value is a new <see cref="ChartLayout" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the layout options of the pie chart.")]
    [ParameterTypeName("ChartLayout")]
    public ChartLayout Layout { get; set; } = new();

    /// <summary>
    /// Gets or sets the plugin options of the pie chart.
    /// <para>
    /// Default value is a new <see cref="PieChartPlugins" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the plugin options of the pie chart.")]
    [ParameterTypeName("PieChartPlugins")]
    public PieChartPlugins Plugins { get; set; } = new();

    /// <summary>
    /// Gets or sets the outer radius: pixels, or a percentage of the available space such as "90%".
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "100%").
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the outer radius: pixels, or a percentage of the available space such as \"90%\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartDimension?")]
    public ChartDimension? Radius { get; set; }

    /// <summary>
    /// Gets or sets the starting angle of the arcs, in degrees; -90 starts a half circle on the left.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 0, the top).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the starting angle of the arcs, in degrees; -90 starts a half circle on the left.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Rotation { get; set; }

    #endregion
}
