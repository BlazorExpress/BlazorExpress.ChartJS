namespace BlazorExpress.ChartJS;

public record BarChartDatasetData : ChartDatasetData
{
    #region Constructors and Finalizers

    public BarChartDatasetData(string? datasetLabel, double data) : base(datasetLabel, data) { }

    /// <summary>
    /// Creates a floating bar value from <paramref name="start" /> to <paramref name="end" />, for <c>AddDataAsync</c>.
    /// </summary>
    /// <param name="datasetLabel">The label of the dataset the value is added to.</param>
    /// <param name="start">The value where the bar starts.</param>
    /// <param name="end">The value where the bar ends.</param>
    [AddedVersion("2.0.0")]
    [Description("Creates a floating bar value from start to end, for AddDataAsync.")]
    public BarChartDatasetData(string? datasetLabel, double start, double end) : base(datasetLabel, new[] { start, end }) { }

    /// <summary>
    /// Creates a bar value at a date, for <c>AddDataAsync</c> on a dataset that uses <see cref="BarChartDataset.TimeData" />,
    /// for example <c>new BarChartDatasetData("Orders", new DateOnly(2026, 3, 1), 30)</c>.
    /// </summary>
    /// <param name="datasetLabel">The label of the dataset the value is added to.</param>
    /// <param name="x">The date: a <see cref="DateOnly" />, <see cref="DateTime" /> or <see cref="DateTimeOffset" />.</param>
    /// <param name="y">The value, or <see langword="null" /> for a gap.</param>
    [AddedVersion("2.0.0")]
    [Description("Creates a bar value at a date, for AddDataAsync on a dataset that uses TimeData.")]
    public BarChartDatasetData(string? datasetLabel, ChartTimeValue x, double? y) : base(datasetLabel, new ChartTimePoint(x, y)) { }

    #endregion
}
