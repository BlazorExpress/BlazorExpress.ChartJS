namespace BlazorExpress.ChartJS;

/// <summary>
/// Identifies one chart data item to highlight with <see cref="ChartComponentBase.SetActiveElementsAsync" />.
/// </summary>
[AddedVersion("2.0.0")]
[Description("Identifies one chart data item to highlight.")]
public class ChartActiveElement
{
    #region Constructors and Finalizers

    /// <summary>
    /// Initializes a new instance of the <see cref="ChartActiveElement" /> class.
    /// </summary>
    public ChartActiveElement() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChartActiveElement" /> class for a dataset and data index.
    /// </summary>
    /// <param name="datasetIndex">The dataset index.</param>
    /// <param name="index">The data index within the dataset.</param>
    public ChartActiveElement(int datasetIndex, int index)
    {
        DatasetIndex = datasetIndex;
        Index = index;
    }

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the dataset index.
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(0)]
    [Description("Gets or sets the dataset index.")]
    public int DatasetIndex { get; set; }

    /// <summary>
    /// Gets or sets the data index within the dataset.
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(0)]
    [Description("Gets or sets the data index within the dataset.")]
    public int Index { get; set; }

    #endregion
}
