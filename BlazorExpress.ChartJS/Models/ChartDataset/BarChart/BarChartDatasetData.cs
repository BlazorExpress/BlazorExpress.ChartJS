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
    [AddedVersion("1.3.0")]
    [Description("Creates a floating bar value from start to end, for AddDataAsync.")]
    public BarChartDatasetData(string? datasetLabel, double start, double end) : base(datasetLabel, new[] { start, end }) { }

    #endregion
}
