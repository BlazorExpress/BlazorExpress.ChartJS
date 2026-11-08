namespace BlazorExpress.ChartJS;

public record ScatterChartDatasetData : ChartDatasetData
{
    #region Constructors

    public ScatterChartDatasetData(string? datasetLabel, ScatterChartDataPoint? data) : base(datasetLabel, data) { }

    /// <summary>
    /// Creates a point at a date, for <c>AddDataAsync</c> on a dataset that uses <see cref="ScatterChartDataset.TimeData" />,
    /// for example <c>new ScatterChartDatasetData("Sensor A", new DateTime(2026, 1, 5, 9, 52, 0), 23.9)</c>.
    /// </summary>
    /// <param name="datasetLabel">The label of the dataset the point is added to.</param>
    /// <param name="x">The date: a <see cref="DateOnly" />, <see cref="DateTime" /> or <see cref="DateTimeOffset" />.</param>
    /// <param name="y">The value, or <see langword="null" /> for a gap.</param>
    [AddedVersion("1.3.0")]
    [Description("Creates a point at a date, for AddDataAsync on a dataset that uses TimeData.")]
    public ScatterChartDatasetData(string? datasetLabel, ChartTimeValue x, double? y) : base(datasetLabel, new ChartTimePoint(x, y)) { }

    #endregion
}
