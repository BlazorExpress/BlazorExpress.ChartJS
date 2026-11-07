namespace BlazorExpress.ChartJS;

/// <summary>
/// A border radius per corner, in pixels. Corners left <see langword="null" /> use the Chart.js default.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/charts/bar.html#borderradius" /> for more information.
/// </para>
/// </summary>
public sealed class ChartBorderRadius
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the bottom-left radius in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the bottom-left radius in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BottomLeft { get; set; }

    /// <summary>
    /// Gets or sets the bottom-right radius in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the bottom-right radius in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BottomRight { get; set; }

    /// <summary>
    /// Gets or sets the top-left radius in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the top-left radius in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? TopLeft { get; set; }

    /// <summary>
    /// Gets or sets the top-right radius in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the top-right radius in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? TopRight { get; set; }

    #endregion
}
