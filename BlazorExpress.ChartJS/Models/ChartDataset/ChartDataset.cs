namespace BlazorExpress.ChartJS;

public interface IChartDataset { }

/// <summary>
/// Represents a dataset configuration for Chart.js charts.
/// See <see href="https://www.chartjs.org/docs/latest/general/data-structures.html#dataset-configuration" /> for more information.
/// </summary>
public class ChartDataset<TData> : IChartDataset
{
    #region Constructors and Finalizers

    public ChartDataset()
    {
        Oid = Guid.NewGuid();
    }

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Chart.js animates charts out of the box. 
    /// A number of options are provided to configure how the animation looks and how long it takes.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/animations.html" />
    /// </summary>
    [AddedVersion("1.2.0")]
    [DefaultValue(null)]
    [Description("Chart.js animates charts out of the box. A number of options are provided to configure how the animation looks and how long it takes.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ChartAnimation? Animation { get; set; }

    /// <summary>
    /// How to clip relative to chartArea. Positive value allows overflow, negative value clips that
    /// many pixels inside chartArea. 0 = clip at chartArea. Use <see cref="ClipSides" /> to clip each side differently;
    /// when set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("How to clip relative to chartArea. Positive value allows overflow, negative value clips that many pixels inside chartArea. 0 = clip at chartArea. Use ClipSides to clip each side differently.")]
    [JsonIgnore]
    [ParameterTypeName("string?")]
    public string? Clip { get; set; }

    /// <summary>
    /// Gets or sets the clip per side, in pixels. When set, it is sent instead of <see cref="Clip" />.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the clip per side, in pixels. When set, it is sent instead of Clip.")]
    [JsonIgnore]
    [ParameterTypeName("ChartClip?")]
    public ChartClip? ClipSides { get; set; }

    /// <summary>
    /// Gets the value sent as <c>clip</c>: <see cref="ClipSides" /> when set, otherwise <see cref="Clip" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("clip")]
    internal object? ClipValue => (object?)ClipSides ?? Clip;

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
    [JsonIgnore]
    public List<TData>? Data { get; set; }

    /// <summary>
    /// Gets the value sent as <c>data</c>. It is <see cref="Data" />, unless a dataset type sends another shape (for example floating bars).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("data")]
    internal virtual object? DataValue => Data;

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
    /// Gets or sets a stable identity key for the dataset. It is not drawn on the chart.
    /// <para>
    /// On <c>UpdateAsync</c>, a dataset is matched to a dataset already on the chart by this key when set,
    /// otherwise by <see cref="Label"/>; datasets that share a label, or have none, pair in order of occurrence. A matched dataset animates from its
    /// current values to the new ones and keeps the visibility changed at runtime. Set a key when a series keeps its
    /// identity while its label changes, for example on relabelling or localization. An empty key counts as not set.
    /// </para>
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a stable identity key for the dataset. It is not drawn on the chart. On <code>UpdateAsync</code>, datasets are matched by this key when set, otherwise by <code>Label</code>, pairing datasets that share a label, or have none, in order. A matched dataset animates from its current values and keeps its runtime visibility.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Key { get; set; }

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

    /// <summary>
    /// The ID of the group to which this dataset belongs. On a stacked axis, datasets sharing the same stack are stacked together;
    /// datasets with different stacks are drawn side by side (bar) or independently (line). Doughnut, pie and polar area charts ignore it.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/general/data-structures.html#dataset-configuration" />
    /// </summary>
    [AddedVersion("1.2.3")]
    [DefaultValue(null)]
    [Description("The ID of the group to which this dataset belongs. Datasets sharing the same stack are stacked together; datasets with different stacks are drawn side by side (bar) or independently (line).")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Stack { get; set; }

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
