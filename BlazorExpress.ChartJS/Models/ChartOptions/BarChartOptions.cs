namespace BlazorExpress.ChartJS;

/// <summary>
/// Provides configuration options specific to bar charts.
/// <para>
///     <see href="https://www.chartjs.org/docs/latest/charts/bar.html" />
/// </para>
/// </summary>
public class BarChartOptions : ChartOptions
{
    #region Properties, Indexers

    //hover -> mode, intersect
    //maintainAspectRatio
    //plugins -> title -> display, text

    /// <summary>
    /// Gets or sets the base axis of the chart. Use 'x' for vertical charts and 'y' for horizontal charts.
    /// Supported values are 'x' and 'y'.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the base axis of the chart. Use 'x' for vertical charts and 'y' for horizontal charts. Supported values are 'x' and 'y'.")]
    //[ParameterTypeName("string?")]
    [Parameter]
    public string? IndexAxis { get; set; }

    /// <summary>
    /// Gets or sets the interaction options for the bar chart.
    /// <para>
    /// Default value is a new <see cref="Interaction" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the interaction options for the bar chart.")]
    //[ParameterTypeName("Interaction")]
    [Parameter]
    public Interaction Interaction { get; set; } = new();

    /// <summary>
    /// Gets or sets the layout options for the bar chart.
    /// <para>
    /// Default value is a new <see cref="ChartLayout" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the layout options for the bar chart.")]
    [ParameterTypeName("ChartLayout")]
    [Parameter]
    public ChartLayout Layout { get; set; } = new();

    /// <summary>
    /// Gets or sets the plugins configuration for the bar chart.
    /// <para>
    /// Default value is a new <see cref="BarChartPlugins" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the plugins configuration for the bar chart.")]
    [ParameterTypeName("BarChartPlugins")]
    [Parameter]
    public BarChartPlugins Plugins { get; set; } = new();

    /// <summary>
    /// Gets or sets the scales configuration for the bar chart.
    /// <para>
    /// Default value is a new <see cref="Scales" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the scales configuration for the bar chart.")]
    //[ParameterTypeName("Scales")]
    [Parameter]
    public Scales Scales { get; set; } = new();

    #endregion

    //tooltips -> mode, intersect
}
