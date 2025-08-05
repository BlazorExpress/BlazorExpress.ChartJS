namespace BlazorExpress.ChartJS;

/// <summary>
/// The bar chart allows a number of properties to be specified for each dataset. 
/// These are used to set display properties for a specific dataset.
/// <para>
/// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#general" />
/// </para>
/// <para>
/// <seealso href="https://www.chartjs.org/docs/latest/charts/bar.html#dataset-properties" />
/// </para>
/// </summary>
public class BarChartDataset : ChartDataset<double?>
{
    #region Properties, Indexers

    /// <summary>
    /// The bar background color.
    /// <para>
    /// Default value is rgba(0, 0, 0, 0.1).
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#styling" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("Gets or sets the bar background color.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? BackgroundColor { get; set; }

    /// <summary>
    /// Percent (0-1) of the available width each bar should be within the category width. 
    /// 1.0 will take the whole category width and put the bars right next to each other.
    /// <para>
    /// Default value is 0.9.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#barpercentage" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0.9)]
    [Description("Percent (0-1) of the available width each bar should be within the category width. 1.0 will take the whole category width and put the bars right next to each other.")]
    public double BarPercentage { get; set; } = 0.9;

    /// <summary>
    /// If this value is a number, it is applied to the width of each bar, in pixels. 
    /// When this is enforced, <see cref="BarPercentage" /> and <see cref="CategoryPercentage" /> are ignored.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#barthickness" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the width of each bar, in pixels. When this is enforced, <code>BarPercentage</code> and <code>CategoryPercentage</code> are ignored.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? BarThickness { get; set; }

    /// <summary>
    /// The bar border color.
    /// <para>
    /// Default value is rgba(0, 0, 0, 0.1).
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#styling" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("The bar border color.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? BorderColor { get; set; }

    /// <summary>
    /// The bar border radius (in pixels).
    /// <para>
    /// Default value is 0.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#styling" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("The bar border radius (in pixels).")]
    [ParameterTypeName("List<double>?")]
    public List<double>? BorderRadius { get; set; }

    //BorderSkipped
    //https://www.chartjs.org/docs/latest/charts/bar.html#styling
    //https://www.chartjs.org/docs/latest/api/interfaces/BarControllerDatasetOptions.html#borderskipped

    /// <summary>
    /// The bar border width (in pixels).
    /// <para>
    /// Default value is 0.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#styling" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("The bar border width (in pixels).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? BorderWidth { get; set; }

    /// <summary>
    /// Percent (0-1) of the available width each category should be within the sample width.
    /// <para>
    /// Default value is 0.8.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#categorypercentage" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0.8)]
    [Description("Percent (0-1) of the available width each category should be within the sample width.")]
    public double CategoryPercentage { get; set; } = 0.8;

    /// <summary>
    /// Gets or sets the data labels configuration for the bar chart dataset.
    /// <para>
    /// This property allows customization of data labels, such as their appearance, positioning, and
    /// formatting. If not set, the default configuration will be used.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the data labels configuration for the bar chart dataset. This property allows customization of data labels, such as their appearance, positioning, and formatting. If not set, the default configuration will be used.")]
    [ParameterTypeName("BarChartDatasetDataLabels")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BarChartDatasetDataLabels Datalabels { get; set; } = new();

    /// <summary>
    /// Should the bars be grouped on index axis. 
    /// When true, all the datasets at same index value will be placed next to each other centering on that index value. 
    /// When false, each bar is placed on its actual index-axis value.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Should the bars be grouped on index axis. When <b>true</b>, all the datasets at same index value will be placed next to each other centering on that index value. When <b>false</b>, each bar is placed on its actual index-axis value.")]
    public bool Grouped { get; set; } = true;

    /// <summary>
    /// The bar background color when hovered.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#interactions" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The bar background color when hovered.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? HoverBackgroundColor { get; set; }

    /// <summary>
    /// The bar border color when hovered.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#interactions" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The bar border color when hovered.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? HoverBorderColor { get; set; }

    /// <summary>
    /// The bar border radius when hovered (in pixels).
    /// <para>
    /// Default value is 0.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#interactions" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("The bar border radius when hovered (in pixels).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderRadius { get; set; }

    /// <summary>
    /// The bar border width when hovered (in pixels).
    /// <para>
    /// Default value is 1.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#interactions" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(1)]
    [Description("The bar border width when hovered (in pixels).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderWidth { get; set; }

    /// <summary>
    /// The base axis of the dataset. 'x' for vertical bars and 'y' for horizontal bars.
    /// <para>
    /// Default value is 'x'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("x")]
    [Description("The base axis of the dataset. 'x' for vertical bars and 'y' for horizontal bars.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IndexAxis { get; set; }

    //InflateAmount
    //https://www.chartjs.org/docs/latest/charts/bar.html#inflateamount

    /// <summary>
    /// Set this to ensure that bars are not sized thicker than this.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#maxbarthickness" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Set this to ensure that bars are not sized thicker than this.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MaxBarThickness { get; set; }

    /// <summary>
    /// Set this to ensure that bars have a minimum length in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#styling" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Set this to ensure that bars have a minimum length in pixels.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MinBarLength { get; set; }

    //PointStyle
    //https://www.chartjs.org/docs/latest/charts/bar.html#styling
    //https://www.chartjs.org/docs/latest/configuration/elements.html#point-styles

    /// <summary>
    /// If true, null or undefined values will not be used for spacing calculations when determining bar size.
    /// <para>
    /// Default value is <see langword="false" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("If <b>true</b>, null or undefined values will not be used for spacing calculations when determining bar size.")]
    public bool SkipNull { get; set; }

    //Stack
    //https://www.chartjs.org/docs/latest/charts/bar.html#general

    /// <summary>
    /// The ID of the x-axis to plot this dataset on.
    /// <para>
    /// Default value is first x axis.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The ID of the x-axis to plot this dataset on.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? XAxisID { get; set; }

    /// <summary>
    /// The ID of the y-axis to plot this dataset on.
    /// <para>
    /// Default value is first y axis.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The ID of the y-axis to plot this dataset on.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? YAxisID { get; set; }

    #endregion
}
