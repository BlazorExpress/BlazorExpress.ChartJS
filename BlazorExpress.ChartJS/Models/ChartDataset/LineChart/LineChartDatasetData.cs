namespace BlazorExpress.ChartJS;

public record LineChartDatasetData : ChartDatasetData
{
    #region Constructors

    public LineChartDatasetData(string? datasetLabel, double data) : base(datasetLabel, data) { }

    /// <summary>
    /// Creates a value at a date, for <c>AddDataAsync</c> on a dataset that uses <see cref="LineChartDataset.TimeData" />,
    /// for example <c>new LineChartDatasetData("Sensor A", new DateTime(2026, 1, 5, 9, 52, 0), 23.9)</c>.
    /// </summary>
    /// <param name="datasetLabel">The label of the dataset the value is added to.</param>
    /// <param name="x">The date: a <see cref="DateOnly" />, <see cref="DateTime" /> or <see cref="DateTimeOffset" />.</param>
    /// <param name="y">The value, or <see langword="null" /> for a gap.</param>
    [AddedVersion("1.3.0")]
    [Description("Creates a value at a date, for AddDataAsync on a dataset that uses TimeData.")]
    public LineChartDatasetData(string? datasetLabel, ChartTimeValue x, double? y) : base(datasetLabel, new ChartTimePoint(x, y)) { }

    #endregion
}
