using BlazorExpress.ChartJS.Enums;

namespace BlazorExpress.ChartJS;

public class DoughnutChartDataset : ChartDataset
{
    #region Properties, Indexers

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public DoughnutChartDatasetDataLabels Datalabels { get; set; } = new();

    /// <summary>
    /// The label for the dataset which appears in the legend and tooltips.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Label { get; set; }

    #endregion
}

public class DoughnutChartDatasetDataLabels
{
    #region Fields and Constants

    private Anchor anchor;

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the data labels anchor.
    /// </summary>
    /// <remarks>
    /// Default value is <see cref="Anchor.None"/>.
    /// </remarks>
    [JsonIgnore]
    public Anchor Anchor
    {
        get => anchor;
        set
        {
            anchor = value;
            DataLabelsAnchor = value.ToAnchorString();
        }
    }

    //public string? BackgroundColor { get; set; }

    public double? BorderWidth { get; set; } = 2;

    /// <summary>
    /// Gets or sets the data labels anchor.
    /// Possible values: start, center, and end.
    /// </summary>
    [JsonPropertyName("anchor")]
    public string? DataLabelsAnchor { get; private set; }

    #endregion
}
