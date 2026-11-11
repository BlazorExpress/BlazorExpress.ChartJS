namespace BlazorExpress.ChartJS;

/// <summary>
/// Provides configuration options specific to polar area charts.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/charts/polar.html" /> for more information.
/// </para>
/// </summary>
public class PolarAreaChartOptions : ChartOptions
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the interaction options of the polar area chart, which also apply to hover and tooltips unless they set their own.
    /// <para>
    /// Default value is a new <see cref="Interaction" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the interaction options of the polar area chart, which also apply to hover and tooltips unless they set their own.")]
    public Interaction Interaction { get; set; } = new();

    /// <summary>
    /// Gets or sets the layout options of the polar area chart.
    /// <para>
    /// Default value is a new <see cref="ChartLayout" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the layout options of the polar area chart.")]
    [ParameterTypeName("ChartLayout")]
    public ChartLayout Layout { get; set; } = new();

    /// <summary>
    /// Gets or sets the plugin options of the polar area chart.
    /// <para>
    /// Default value is a new <see cref="PolarAreaChartPlugins" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the plugin options of the polar area chart.")]
    [ParameterTypeName("PolarAreaChartPlugins")]
    public PolarAreaChartPlugins Plugins { get; set; } = new();

    /// <summary>
    /// Gets or sets the radial scale of the polar area chart, sent as scales.r.
    /// <para>
    /// Its <c>R</c> axis starts at zero, as in Chart.js.
    /// </para>
    /// <para>
    /// Default value is a new <see cref="RadialScales" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the radial scale of the polar area chart, sent as scales.r.")]
    [ParameterTypeName("RadialScales")]
    public RadialScales Scales { get; set; } = new();

    #endregion
}
