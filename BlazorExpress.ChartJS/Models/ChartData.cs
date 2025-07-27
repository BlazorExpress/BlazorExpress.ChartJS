namespace BlazorExpress.ChartJS;

/// <summary>
/// Represents the data structure for a Chart.js chart, including datasets and labels.
/// </summary>
public class ChartData
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the collection of datasets to be displayed in the chart.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the collection of datasets to be displayed in the chart.")]
    [ParameterTypeName("List<IChartDataset>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<IChartDataset>? Datasets { get; set; }

    /// <summary>
    /// Gets or sets the labels for the chart, typically used for the x-axis or categories.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the labels for the chart, typically used for the x-axis or categories.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Labels { get; set; }

    #endregion
}
