namespace BlazorExpress.ChartJS;

/// <summary>
/// The scales of a radar or polar area chart: one radial axis, sent as <c>r</c>. These charts never send an <c>x</c> or <c>y</c> axis.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/axes/radial/" /> for more information.
/// </para>
/// </summary>
public class RadialScales
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the radial axis, sent as <c>r</c>.
    /// <para>
    /// Default value is a new <see cref="ChartRadialAxes" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the radial axis, sent as r.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartRadialAxes?")]
    public ChartRadialAxes? R { get; set; } = new();

    #endregion
}
