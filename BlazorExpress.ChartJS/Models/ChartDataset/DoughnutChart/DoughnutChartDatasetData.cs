namespace BlazorExpress.ChartJS;

public record DoughnutChartDatasetData : ChartDatasetData
{
    #region Constructors

    public DoughnutChartDatasetData(string? datasetLabel, double data, string? backgroundColor) : base(datasetLabel, data)
    {
        BackgroundColor = backgroundColor;
    }

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets or initializes the background color of the slice that is added, for example <c>"rgb(54, 162, 235)"</c>.
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or initializes the background color of the slice that is added.")]
    [ParameterTypeName("string?")]
    public string? BackgroundColor { get; init; }

    #endregion
}
