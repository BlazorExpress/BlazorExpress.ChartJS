namespace BlazorExpress.ChartJS;

/// <summary>
/// Provides information about a selected chart data item.
/// </summary>
public class ChartClickEventArgs
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the index of the selected dataset.
    /// </summary>
    [AddedVersion("1.2.4")]
    [Description("Gets or sets the index of the selected dataset.")]
    public int DatasetIndex { get; set; }

    /// <summary>
    /// Gets or sets the label of the selected dataset.
    /// </summary>
    [AddedVersion("1.2.4")]
    [DefaultValue(null)]
    [Description("Gets or sets the label of the selected dataset.")]
    public string? DatasetLabel { get; set; }

    /// <summary>
    /// Gets or sets the index of the selected data item.
    /// </summary>
    [AddedVersion("1.2.4")]
    [Description("Gets or sets the index of the selected data item.")]
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets the label of the selected data item.
    /// </summary>
    [AddedVersion("1.2.4")]
    [DefaultValue(null)]
    [Description("Gets or sets the label of the selected data item.")]
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the raw value of the selected data item.
    /// </summary>
    [AddedVersion("1.2.4")]
    [DefaultValue(null)]
    [Description("Gets or sets the raw value of the selected data item.")]
    public JsonElement? Value { get; set; }

    #endregion
}
