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
    public string? IndexAxis { get; set; }

    /// <summary>
    /// Gets or sets the interaction options for the bar chart.
    /// <para>
    /// See <see href="https://www.chartjs.org/docs/latest/configuration/interactions.html" /> for more details.
    /// </para>
    /// </summary>
    public Interaction Interaction { get; set; } = new();

    /// <summary>
    /// Gets or sets the layout options for the bar chart.
    /// <para>
    /// See <see href="https://www.chartjs.org/docs/latest/configuration/layout.html" /> for more details.
    /// </para>
    /// </summary>
    public ChartLayout Layout { get; set; } = new();

    /// <summary>
    /// Gets or sets the plugins configuration for the bar chart.
    /// <para>
    /// See <see href="https://www.chartjs.org/docs/latest/configuration/plugins.html" /> for more details.
    /// </para>
    /// </summary>
    public BarChartPlugins Plugins { get; set; } = new();

    /// <summary>
    /// Gets or sets the scales configuration for the bar chart.
    /// <para>
    /// See <see href="https://www.chartjs.org/docs/latest/axes/" /> for more details.
    /// </para>
    /// </summary>
    public Scales Scales { get; set; } = new();

    #endregion

    //tooltips -> mode, intersect
}
