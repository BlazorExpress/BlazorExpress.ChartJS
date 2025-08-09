namespace BlazorExpress.ChartJS;

public interface IChartDataset { }

/// <summary>
/// Represents a dataset configuration for Chart.js charts.
/// See <see href="https://www.chartjs.org/docs/latest/general/data-structures.html#dataset-configuration" /> for more information.
/// </summary>
public class ChartDataset<TData> : IChartDataset
{
    #region Constructors

    public ChartDataset()
    {
        Oid = Guid.NewGuid();
    }

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// How to clip relative to chartArea. Positive value allows overflow, negative value clips that 
    /// many pixels inside chartArea. 0 = clip at chartArea.  Clipping can also be configured 
    /// per side: clip: {left: 5, top: false, right: -2, bottom: 0}
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("How to clip relative to chartArea. Positive value allows overflow, negative value clips that many pixels inside chartArea. 0 = clip at chartArea. Clipping can also be configured per side: clip: {left: 5, top: false, right: -2, bottom: 0}")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Clip { get; set; }

    /// <summary>
    /// Get or sets the Data.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Get or sets the Data.")]
    [EditorRequired]
    [ParameterTypeName("List<TData>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<TData>? Data { get; set; }

    /// <summary>
    /// Configure the visibility of the dataset. Setting Hidden to true will prevent the dataset from being rendered in the Chart.
    /// <para>
    /// Default value is <see langword="false"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("Configure the visibility of the dataset. Setting <code>Hidden</code> to <b>true</b> will prevent the dataset from being rendered in the Chart.")]
    public bool Hidden { get; set; }

    /// <summary>
    /// The label for the dataset which appears in the legend and tooltips.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The label for the dataset which appears in the legend and tooltips.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Label { get; set; }

    /// <summary>
    /// Gets the unique object identifier for this dataset instance.
    /// <para>
    /// Default value is an auto-generated <see cref="Guid"/> assigned at construction.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("Guid.NewGuid()")]
    [Description("Gets the unique object identifier for this dataset instance. This value is auto-generated when the dataset is created.")]
    public Guid Oid { get; private set; }

    /// <summary>
    /// The drawing order of dataset. Also affects order for stacking, tooltip and legend.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("The drawing order of dataset. Also affects order for stacking, tooltip and legend.")]
    public int Order { get; set; }

    //Parsing
    //https://www.chartjs.org/docs/latest/general/data-structures.html#dataset-configuration

    //Stack
    //https://www.chartjs.org/docs/latest/general/data-structures.html#dataset-configuration

    /// <summary>
    /// Gets or sets the chart type.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// This value is auto-set based on the chart type.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the chart type. This value is auto-set based on the chart type.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; protected set; }

    #endregion
}
