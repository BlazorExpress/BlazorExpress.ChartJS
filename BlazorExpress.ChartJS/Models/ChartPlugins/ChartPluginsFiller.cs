namespace BlazorExpress.ChartJS;

/// <summary>
/// The options of the built-in filler plugin, which draws the area fills of datasets that set fill.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/charts/area.html#configuration" /> for more information.
/// </para>
/// </summary>
public class ChartPluginsFiller
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets when the fill is drawn: "beforeDraw", "beforeDatasetDraw" or "beforeDatasetsDraw".
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "beforeDatasetDraw").
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets when the fill is drawn: \"beforeDraw\", \"beforeDatasetDraw\" or \"beforeDatasetsDraw\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? DrawTime { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a fill whose target dataset is hidden propagates to the next visible target.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses true).
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether a fill whose target dataset is hidden propagates to the next visible target.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Propagate { get; set; }

    #endregion
}
