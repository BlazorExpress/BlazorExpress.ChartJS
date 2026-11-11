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
    [EditorRequired]
    [ParameterTypeName("List<IChartDataset>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<IChartDataset>? Datasets { get; set; }

    /// <summary>
    /// Gets or sets the labels for the chart, typically used for the x-axis or categories.
    /// For dates on a time or timeseries axis, use <see cref="TimeLabels" />; when it is set, it is sent instead of these labels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the labels for the chart, typically used for the x-axis or categories. For dates on a time axis, use TimeLabels.")]
    [EditorRequired]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore]
    public List<string>? Labels { get; set; }

    /// <summary>
    /// Gets the value sent as <c>labels</c>: <see cref="TimeLabels" /> when set, otherwise <see cref="Labels" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("labels")]
    internal object? LabelsValue => (object?)TimeLabels ?? Labels;

    /// <summary>
    /// Gets or sets the dates of a time or timeseries x-axis, shared by every dataset, for example
    /// <c>TimeLabels = [new DateOnly(2026, 1, 1), new DateOnly(2026, 1, 2), new DateOnly(2026, 1, 5)]</c> with one value per date in each dataset's <c>Data</c>.
    /// When set, it is sent instead of <see cref="Labels" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/axes/cartesian/time.html" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the dates of a time or timeseries x-axis, shared by every dataset. When set, it is sent instead of Labels.")]
    [JsonIgnore]
    [ParameterTypeName("List<ChartTimeValue>?")]
    public List<ChartTimeValue>? TimeLabels { get; set; }

    #endregion
}
