namespace BlazorExpress.ChartJS;

public interface IChartDatasetData { }

public record ChartDatasetData : IChartDatasetData
{
    #region Constructors

    public ChartDatasetData(string? datasetLabel, object? data)
    {
        DatasetLabel = datasetLabel;
        Data = data;
    }

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets the label for the dataset this data belongs to.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets the label for the dataset this data belongs to.")]
    [ParameterTypeName("string?")]
    public string? DatasetLabel { get; init; }

    /// <summary>
    /// Gets the data value or values for the dataset.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets the data value or values for the dataset.")]
    [ParameterTypeName("object?")]
    public object? Data { get; init; }

    #endregion
}
