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
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the base axis of the chart. Use 'x' for vertical charts and 'y' for horizontal charts. Supported values are 'x' and 'y'.")]
    public string? IndexAxis { get; set; }

    /// <summary>
    /// Gets or sets the interaction options for the bar chart.
    /// <para>
    /// See <see href="https://www.chartjs.org/docs/latest/configuration/interactions.html" /> for more details.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [Description("Gets or sets the interaction options for the bar chart.")]
    public Interaction Interaction { get; set; } = new();

    /// <summary>
    /// Gets or sets the layout options for the bar chart.
    /// <para>
    /// See <see href="https://www.chartjs.org/docs/latest/configuration/layout.html" /> for more details.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [Description("Gets or sets the layout options for the bar chart.")]
    public ChartLayout Layout { get; set; } = new();

    /// <summary>
    /// Gets or sets the plugins configuration for the bar chart.
    /// <para>
    /// See <see href="https://www.chartjs.org/docs/latest/configuration/plugins.html" /> for more details.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [Description("Gets or sets the plugins configuration for the bar chart.")]
    public BarChartPlugins Plugins { get; set; } = new();

    /// <summary>
    /// Gets or sets the scales configuration for the bar chart.
    /// <para>
    /// See <see href="https://www.chartjs.org/docs/latest/axes/" /> for more details.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [Description("Gets or sets the scales configuration for the bar chart.")]
    public Scales Scales { get; set; } = new();

    /// <summary>
    /// Gets or sets additional attributes that will be applied to the component.
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets additional attributes that will be applied to the component.")]
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    #endregion

    //tooltips -> mode, intersect
}
