namespace BlazorExpress.ChartJS;

public record BubbleChartDatasetData : ChartDatasetData
{
    #region Constructors

    public BubbleChartDatasetData(string? datasetLabel, BubbleChartDataPoint data) : base(datasetLabel, data) { }

    /// <summary>
    /// Creates a bubble at a date, for <c>AddDataAsync</c> on a dataset that uses <see cref="BubbleChartDataset.TimeData" />,
    /// for example <c>new BubbleChartDatasetData("Sensor A", new DateTime(2026, 1, 5, 9, 52, 0), 23.9, 6)</c>.
    /// </summary>
    /// <param name="datasetLabel">The label of the dataset the bubble is added to.</param>
    /// <param name="x">The date: a <see cref="DateOnly" />, <see cref="DateTime" /> or <see cref="DateTimeOffset" />.</param>
    /// <param name="y">The value.</param>
    /// <param name="r">The radius of the bubble in pixels.</param>
    [AddedVersion("2.0.0")]
    [Description("Creates a bubble at a date, for AddDataAsync on a dataset that uses TimeData.")]
    public BubbleChartDatasetData(string? datasetLabel, ChartTimeValue x, double y, double r) : base(datasetLabel, new BubbleChartTimePoint(x, y, r)) { }

    #endregion
}
