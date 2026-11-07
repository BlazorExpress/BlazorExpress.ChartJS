namespace BlazorExpress.ChartJS;

/// <summary>
/// Hover options, sent as <c>options.hover</c>. Every value left <see langword="null" /> falls back to the chart's <c>interaction</c> options,
/// so set only what should differ from them.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/configuration/interactions.html" /> for more information.
/// </para>
/// </summary>
public class ChartHover
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the directions used to calculate distances: <c>"x"</c>, <c>"y"</c>, <c>"xy"</c> or <c>"r"</c>.
    /// <para>
    /// Default value is <see langword="null" /> (the interaction value).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the directions used to calculate distances: \"x\", \"y\", \"xy\" or \"r\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Axis { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether points outside the chart area are also considered.
    /// <para>
    /// Default value is <see langword="null" /> (the interaction value).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether points outside the chart area are also considered.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? IncludeInvisible { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether hover applies only when the pointer intersects an item.
    /// <para>
    /// Default value is <see langword="null" /> (the interaction value).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether hover applies only when the pointer intersects an item.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Intersect { get; set; }

    /// <summary>
    /// Gets or sets which elements are hovered. See <see cref="InteractionMode" />.
    /// <para>
    /// Default value is <see langword="null" /> (the interaction value).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets which elements are hovered.")]
    [JsonConverter(typeof(CamelCaseEnumConverter<InteractionMode>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("InteractionMode?")]
    public InteractionMode? Mode { get; set; }

    #endregion
}
