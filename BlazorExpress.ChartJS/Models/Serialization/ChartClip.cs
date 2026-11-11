namespace BlazorExpress.ChartJS;

/// <summary>
/// How far a dataset may draw outside the chart area, per side, in pixels. A positive value allows overflow, a negative value clips
/// that many pixels inside the chart area, and 0 clips at the chart area. Every side is sent, because Chart.js needs all four; an unset side is 0.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/general/data-structures.html#dataset-configuration" /> for more information.
/// </para>
/// </summary>
public sealed class ChartClip
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the bottom clip in pixels.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(0)]
    [Description("Gets or sets the bottom clip in pixels.")]
    public double Bottom { get; set; }

    /// <summary>
    /// Gets or sets the left clip in pixels.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(0)]
    [Description("Gets or sets the left clip in pixels.")]
    public double Left { get; set; }

    /// <summary>
    /// Gets or sets the right clip in pixels.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(0)]
    [Description("Gets or sets the right clip in pixels.")]
    public double Right { get; set; }

    /// <summary>
    /// Gets or sets the top clip in pixels.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(0)]
    [Description("Gets or sets the top clip in pixels.")]
    public double Top { get; set; }

    #endregion
}
