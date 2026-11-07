namespace BlazorExpress.ChartJS;

/// <summary>
/// The dashed vertical line the library draws through the hovered point of a line chart, sent as <c>options.plugins.tooltipLine</c>. Chart.js has no such line; the library adds it as an inline plugin to <c>type: 'line'</c> charts. Leave <c>LineChartPlugins.TooltipLine</c> unset to keep the default line.
/// </summary>
public class ChartPluginsTooltipLine
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the color of the line.
    /// <para>
    /// Default value is <c>"grey"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("grey")]
    [Description("Gets or sets the color of the line.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Color { get; set; } = "grey";

    /// <summary>
    /// Gets or sets the length and spacing of the dashes; an empty list draws a solid line.
    /// <para>
    /// Default value is <c>[5, 5]</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("[5, 5]")]
    [Description("Gets or sets the length and spacing of the dashes; an empty list draws a solid line.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("List<double>?")]
    public List<double>? Dash { get; set; } = [5, 5];

    /// <summary>
    /// Gets or sets a value indicating whether the line is drawn.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the line is drawn.")]
    public bool Display { get; set; } = true;

    /// <summary>
    /// Gets or sets the width of the line, in pixels.
    /// <para>
    /// Default value is 2.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(2)]
    [Description("Gets or sets the width of the line, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Width { get; set; } = 2;

    #endregion
}
