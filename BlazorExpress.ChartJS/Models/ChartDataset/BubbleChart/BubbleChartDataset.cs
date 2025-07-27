namespace BlazorExpress.ChartJS;

public class BubbleChartDataset : ChartDataset<double?>
{
    #region Properties, Indexers

    public new List<BubbleData>? Data { get; set; }

    public int Radius { get; set; } = 3;

    #endregion
}
