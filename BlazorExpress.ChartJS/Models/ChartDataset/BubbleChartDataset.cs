namespace BlazorExpress.ChartJS;

public class BubbleChartDataset : ChartDataset
{
    #region Properties, Indexers

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public new string? BackgroundColor { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public new string? BorderColor { get; set; }

    public new double BorderWidth { get; set; }

    public new List<BubbleData>? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public new string? HoverBackgroundColor { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public new string? HoverBorderColor { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public new string? HoverBorderWidth { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public string? Label { get; set; }

    public int Radius { get; set; } = 3;

    #endregion
}
