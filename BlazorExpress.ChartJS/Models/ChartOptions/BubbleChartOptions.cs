namespace BlazorExpress.ChartJS;

/// <summary>
/// Provides configuration options specific to bubble charts.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/charts/bubble.html" /> for more information.
/// </para>
/// </summary>
public class BubbleChartOptions : ChartOptions
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the interaction options of the bubble chart, which also apply to hover and tooltips unless they set their own.
    /// <para>
    /// Default value is a new <see cref="Interaction" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the interaction options of the bubble chart, which also apply to hover and tooltips unless they set their own.")]
    public Interaction Interaction { get; set; } = new();

    /// <summary>
    /// Gets or sets the layout options of the bubble chart.
    /// <para>
    /// Default value is a new <see cref="ChartLayout" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the layout options of the bubble chart.")]
    [ParameterTypeName("ChartLayout")]
    public ChartLayout Layout { get; set; } = new();

    /// <summary>
    /// Gets or sets the plugin options of the bubble chart.
    /// <para>
    /// Default value is a new <see cref="BubbleChartPlugins" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the plugin options of the bubble chart.")]
    [ParameterTypeName("BubbleChartPlugins")]
    public BubbleChartPlugins Plugins { get; set; } = new();

    /// <summary>
    /// Gets or sets the x and y scales of the bubble chart, and any additional axes.
    /// <para>
    /// Both axes start at zero by default (<see cref="ChartAxes.BeginAtZero" />), unlike Chart.js; set <c>BeginAtZero = false</c> on an axis to start from the data minimum.
    /// </para>
    /// <para>
    /// Default value is a new <see cref="Scales" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the x and y scales of the bubble chart, and any additional axes.")]
    [ParameterTypeName("Scales")]
    public Scales Scales { get; set; } = new();

    #endregion
}
