namespace BlazorExpress.ChartJS;

/// <summary>
/// Provides configuration options specific to radar charts.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/charts/radar.html" /> for more information.
/// </para>
/// </summary>
public class RadarChartOptions : ChartOptions
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the interaction options of the radar chart, which also apply to hover and tooltips unless they set their own.
    /// <para>
    /// Default value is a new <see cref="Interaction" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the interaction options of the radar chart, which also apply to hover and tooltips unless they set their own.")]
    public Interaction Interaction { get; set; } = new();

    /// <summary>
    /// Gets or sets the layout options of the radar chart.
    /// <para>
    /// Default value is a new <see cref="ChartLayout" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the layout options of the radar chart.")]
    [ParameterTypeName("ChartLayout")]
    public ChartLayout Layout { get; set; } = new();

    /// <summary>
    /// Gets or sets the plugin options of the radar chart.
    /// <para>
    /// Default value is a new <see cref="RadarChartPlugins" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the plugin options of the radar chart.")]
    [ParameterTypeName("RadarChartPlugins")]
    public RadarChartPlugins Plugins { get; set; } = new();

    /// <summary>
    /// Gets or sets the radial scale of the radar chart, sent as scales.r.
    /// <para>
    /// Its <c>R</c> axis starts at zero by default (<see cref="ChartRadialAxes.BeginAtZero" />), unlike Chart.js radar charts; set <c>Scales.R.BeginAtZero = false</c> to start from the data minimum.
    /// </para>
    /// <para>
    /// Default value is a new <see cref="RadialScales" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the radial scale of the radar chart, sent as scales.r.")]
    [ParameterTypeName("RadialScales")]
    public RadialScales Scales { get; set; } = new();

    #endregion
}
