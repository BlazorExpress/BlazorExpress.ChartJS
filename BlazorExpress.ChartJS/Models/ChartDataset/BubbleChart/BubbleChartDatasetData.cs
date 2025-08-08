namespace BlazorExpress.ChartJS;

public record BubbleChartDatasetData : ChartDatasetData
{
    #region Constructors

    public BubbleChartDatasetData(string? datasetLabel, double data) : base(datasetLabel, data) { }

    #endregion
}
