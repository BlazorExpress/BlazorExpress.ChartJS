namespace BlazorExpress.ChartJS;

/// <summary>
/// Provides information about a clicked legend item. The values come from the browser.
/// </summary>
[AddedVersion("1.3.0")]
[Description("Provides information about a clicked legend item. The values come from the browser.")]
public class ChartLegendClickEventArgs
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the index of the dataset the legend item represents.
    /// Set for bar, bubble, line, radar and scatter charts; <see langword="null" /> for doughnut, pie and polar area charts.
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the index of the dataset the legend item represents. Null for doughnut, pie and polar area charts.")]
    [ParameterTypeName("int?")]
    public int? DatasetIndex { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item is hidden after the click.
    /// When <see cref="ChartComponentBase.LegendClickToggles" /> is <see langword="false" />, this is the unchanged state.
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the item is hidden after the click.")]
    public bool Hidden { get; set; }

    /// <summary>
    /// Gets or sets the index of the data item (slice) the legend item represents.
    /// Set for doughnut, pie and polar area charts; <see langword="null" /> for other charts.
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the index of the data item (slice) the legend item represents. Null for non-arc charts.")]
    [ParameterTypeName("int?")]
    public int? Index { get; set; }

    /// <summary>
    /// Gets or sets the legend item text.
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the legend item text.")]
    public string? Text { get; set; }

    #endregion
}
