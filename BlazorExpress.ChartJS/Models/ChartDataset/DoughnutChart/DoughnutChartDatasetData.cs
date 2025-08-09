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
    /// Gets or initializes the background color as a string.
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("")]
    [ParameterTypeName("string?")]
    public string? BackgroundColor { get; init; }

    #endregion
}
