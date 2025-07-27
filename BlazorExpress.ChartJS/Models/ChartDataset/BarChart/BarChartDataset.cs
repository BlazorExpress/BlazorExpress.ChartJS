namespace BlazorExpress.ChartJS;

/// <summary>
/// The bar chart allows a number of properties to be specified for each dataset. 
/// These are used to set display properties for a specific dataset.
/// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#dataset-properties" />
/// <seealso href="https://www.chartjs.org/docs/latest/charts/bar.html#general" />
/// </summary>
public class BarChartDataset : ChartDataset<double?>
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the bar background color.
    /// <para>
    /// Default value is <c>rgba(0, 0, 0, 0.1)</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("Gets or sets the bar background color.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the percent (0-1) of the available width each bar should be within the category width.
    /// 1.0 will take the whole category width and put the bars right next to each other.
    /// <para>
    /// Default value is 0.9.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0.9)]
    [Description("Gets or sets the percent (0-1) of the available width each bar should be within the category width.")]
    public double BarPercentage { get; set; } = 0.9;

    /// <summary>
    /// Gets or sets the width of each bar, in pixels. When this is enforced, barPercentage and categoryPercentage are ignored.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the width of each bar, in pixels. When this is enforced, barPercentage and categoryPercentage are ignored.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? BarThickness { get; set; }

    /// <summary>
    /// Gets or sets the bar border color.
    /// <para>
    /// Default value is <c>rgba(0, 0, 0, 0.1)</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("Gets or sets the bar border color.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the border radius.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("Gets or sets the border radius.")]
    [ParameterTypeName("List<double>?")]
    public List<double>? BorderRadius { get; set; }

    /// <summary>
    /// Gets or sets the border width (in pixels).
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("Gets or sets the border width (in pixels).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? BorderWidth { get; set; }

    //BorderSkipped
    //https://www.chartjs.org/docs/latest/api/interfaces/BarControllerDatasetOptions.html#borderskipped

    /// <summary>
    /// Gets or sets the percent (0-1) of the available width each category should be within the sample width.
    /// <para>
    /// Default value is 0.8.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0.8)]
    [Description("Gets or sets the percent (0-1) of the available width each category should be within the sample width.")]
    public double CategoryPercentage { get; set; } = 0.8;

    /// <summary>
    /// Gets or sets the data labels configuration for the bar chart dataset.
    /// <para>
    /// Default value is a new <see cref="BarChartDatasetDataLabels" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the data labels configuration for the bar chart dataset.")]
    [ParameterTypeName("BarChartDatasetDataLabels")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BarChartDatasetDataLabels Datalabels { get; set; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether the bars should be grouped on the index axis.
    /// When <see langword="true" />, all the datasets at the same index value will be placed next to each other centering on that index value.
    /// When <see langword="false" />, each bar is placed on its actual index-axis value.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the bars should be grouped on the index axis.")]
    public bool Grouped { get; set; } = true;

    /// <summary>
    /// Gets or sets the bar background color when hovered.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the bar background color when hovered.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? HoverBackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the bar border color when hovered.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the bar border color when hovered.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? HoverBorderColor { get; set; }

    /// <summary>
    /// Gets or sets the bar border radius when hovered (in pixels).
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("Gets or sets the bar border radius when hovered (in pixels).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderRadius { get; set; }

    /// <summary>
    /// Gets or sets the bar border width when hovered (in pixels).
    /// <para>
    /// Default value is 1.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(1)]
    [Description("Gets or sets the bar border width when hovered (in pixels).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderWidth { get; set; }

    /// <summary>
    /// Gets or sets the base axis of the chart. 'x' for vertical charts and 'y' for horizontal charts.
    /// Supported values are 'x' and 'y'.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the base axis of the chart. 'x' for vertical charts and 'y' for horizontal charts. Supported values are 'x' and 'y'.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IndexAxis { get; set; }

    //InflateAmount
    //https://www.chartjs.org/docs/latest/charts/bar.html#inflateamount

    /// <summary>
    /// Gets or sets the maximum bar thickness to ensure that bars are not sized thicker than this.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the maximum bar thickness to ensure that bars are not sized thicker than this.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MaxBarThickness { get; set; }

    /// <summary>
    /// Gets or sets the minimum bar length in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the minimum bar length in pixels.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MinBarLength { get; set; }

    //PointStyle
    //https://www.chartjs.org/docs/latest/configuration/elements.html#point-styles

    /// <summary>
    /// Gets or sets a value indicating whether null or undefined values will not be used for spacing calculations when determining bar size.
    /// <para>
    /// Default value is <see langword="false" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether null or undefined values will not be used for spacing calculations when determining bar size.")]
    public bool SkipNull { get; set; }

    //Stack
    //https://www.chartjs.org/docs/latest/charts/bar.html#general

    /// <summary>
    /// Gets or sets the ID of the x axis to plot this dataset on.
    /// <para>
    /// Default value is first x axis.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the ID of the x axis to plot this dataset on.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? XAxisID { get; set; }

    /// <summary>
    /// Gets or sets the ID of the y axis to plot this dataset on.
    /// <para>
    /// Default value is first y axis.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the ID of the y axis to plot this dataset on.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? YAxisID { get; set; }

    #endregion
}
