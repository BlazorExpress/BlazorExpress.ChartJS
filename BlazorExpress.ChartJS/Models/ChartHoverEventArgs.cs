namespace BlazorExpress.ChartJS;

/// <summary>
/// Provides information about the chart data item under the pointer. The values come from the browser.
/// When <see cref="DatasetIndex" /> is <see langword="null" />, the pointer has left all data items.
/// </summary>
[AddedVersion("1.3.0")]
[Description("Provides information about the chart data item under the pointer. A null DatasetIndex means the pointer left all data items.")]
public class ChartHoverEventArgs
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the index of the hovered dataset, or <see langword="null" /> when the pointer has left all data items.
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the index of the hovered dataset, or null when the pointer has left all data items.")]
    [ParameterTypeName("int?")]
    public int? DatasetIndex { get; set; }

    /// <summary>
    /// Gets or sets the label of the hovered dataset.
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the label of the hovered dataset.")]
    public string? DatasetLabel { get; set; }

    /// <summary>
    /// Gets or sets the index of the hovered data item, or <see langword="null" /> when the pointer has left all data items.
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the index of the hovered data item, or null when the pointer has left all data items.")]
    [ParameterTypeName("int?")]
    public int? Index { get; set; }

    /// <summary>
    /// Gets or sets the label of the hovered data item.
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the label of the hovered data item.")]
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the raw value of the hovered data item.
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the raw value of the hovered data item.")]
    public JsonElement? Value { get; set; }

    #endregion
}
