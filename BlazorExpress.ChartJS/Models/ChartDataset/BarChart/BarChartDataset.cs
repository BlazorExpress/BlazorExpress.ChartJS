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
    #region Constructors and Finalizers

    public BarChartDataset()
    {
        Type = "bar";
    }

    #endregion

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
    /// The value the bars start from, in data units, instead of the axis zero line.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#general" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("The value the bars start from, in data units, instead of the axis zero line.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Base { get; set; }

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
    [JsonIgnore]
    [ParameterTypeName("List<double>?")]
    public List<double>? BorderRadius { get; set; }

    /// <summary>
    /// The bar border radius per corner, in pixels, applied to every bar. When set, it is sent instead of <see cref="BorderRadius" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#borderradius" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("The bar border radius per corner, in pixels, applied to every bar. When set, it is sent instead of BorderRadius.")]
    [JsonIgnore]
    [ParameterTypeName("ChartBorderRadius?")]
    public ChartBorderRadius? BorderRadiusCorners { get; set; }

    /// <summary>
    /// Gets the value sent as <c>borderRadius</c>: <see cref="BorderRadiusCorners" /> when set, otherwise <see cref="BorderRadius" />.
    /// Nothing is sent when neither is set, so <c>options.elements.bar.borderRadius</c> applies.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("borderRadius")]
    internal object? BorderRadiusValue => (object?)BorderRadiusCorners ?? BorderRadius;

    /// <summary>
    /// This setting is used to avoid drawing the bar stroke at the base of the fill, or disable the border radius. 
    /// In general, this does not need to be changed except when creating chart types that derive from a bar chart.
    /// Supported values are 'start', 'end', 'middle', 'bottom', 'left', 'top', 'right', 'false', and 'true'.
    /// <para>
    /// Default value is 'start'.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#styling" />
    /// </summary>
    [AddedVersion("1.2.0")]
    [DefaultValue("start")]
    [Description("This setting is used to avoid drawing the bar stroke at the base of the fill, or disable the border radius. In general, this does not need to be changed except when creating chart types that derive from a bar chart. <br />Supported values are <b>start</b>, <b>end</b>, <b>middle</b>, <b>bottom</b>, <b>left</b>, <b>top</b>, <b>right</b>, <b>false</b>, and <b>true</b>.")]
    public object BorderSkipped { get; set; } = "start";

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
    /// <see href="https://chartjs-plugin-datalabels.netlify.app/guide/getting-started.html#configuration" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the data labels configuration for the bar chart dataset. This property allows customization of data labels, such as their appearance, positioning, and formatting. If not set, the default configuration will be used.")]
    [ParameterTypeName("BarChartDatasetDataLabels")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BarChartDatasetDataLabels Datalabels { get; set; } = new();

    /// <summary>
    /// Gets the value sent as <c>data</c>: <see cref="TimeData" /> when set, then <see cref="FloatingData" />, otherwise <c>Data</c>.
    /// </summary>
    internal override object? DataValue => (object?)TimeData ?? (object?)FloatingData ?? Data;

    /// <summary>
    /// The bar values when some or all bars are floating: each item is a number, drawn from the base, a <c>(start, end)</c> range,
    /// drawn as a floating bar, or <see langword="null" />, for example <c>FloatingData = [5, (2, 8), null]</c>.
    /// When set, it is sent instead of <c>Data</c>. Adding a range with <c>AddDataAsync</c> moves the values of <c>Data</c> here and sets <c>Data</c> to <see langword="null" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/samples/bar/floating.html" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("The bar values when some or all bars are floating: each item is a number, a (start, end) range drawn as a floating bar, or null. When set, it is sent instead of Data.")]
    [JsonIgnore]
    [ParameterTypeName("List<BarChartDataValue?>?")]
    public List<BarChartDataValue?>? FloatingData { get; set; }

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
    [JsonIgnore]
    public List<double>? HoverBorderRadius { get; set; }

    /// <summary>
    /// The bar border radius per corner when hovered, in pixels. When set, it is sent instead of <see cref="HoverBorderRadius" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("The bar border radius per corner when hovered, in pixels. When set, it is sent instead of HoverBorderRadius.")]
    [JsonIgnore]
    [ParameterTypeName("ChartBorderRadius?")]
    public ChartBorderRadius? HoverBorderRadiusCorners { get; set; }

    /// <summary>
    /// Gets the value sent as <c>hoverBorderRadius</c>: <see cref="HoverBorderRadiusCorners" /> when set, otherwise <see cref="HoverBorderRadius" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("hoverBorderRadius")]
    internal object? HoverBorderRadiusValue => (object?)HoverBorderRadiusCorners ?? HoverBorderRadius;

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

    /// <summary>
    /// How many pixels each bar is inflated by when drawn, to avoid visible gaps between bars: a number, or <c>"auto"</c>.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses <c>"auto"</c>).
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#inflateamount" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("How many pixels each bar is inflated by when drawn, to avoid visible gaps between bars: a number, or \"auto\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartDimension?")]
    public ChartDimension? InflateAmount { get; set; }

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

    /// <summary>
    /// The point style drawn for this dataset in the legend when the legend uses point styles (<c>UsePointStyle</c>), for example <c>"rectRounded"</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/elements.html#point-styles" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("The point style drawn for this dataset in the legend when the legend uses point styles, for example \"rectRounded\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? PointStyle { get; set; }

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


    /// <summary>
    /// The bar values of a time or timeseries x-axis, each at its own date, for example
    /// <c>TimeData = [new(new DateOnly(2026, 1, 5), 12), new(new DateOnly(2026, 1, 9), 15)]</c>.
    /// A <see langword="null" /> <c>Y</c> leaves a gap. When set, it is sent instead of <see cref="FloatingData" /> and <c>Data</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/axes/cartesian/time.html" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("The bar values of a time or timeseries x-axis, each at its own date. A null Y leaves a gap. When set, it is sent instead of FloatingData and Data.")]
    [JsonIgnore]
    [ParameterTypeName("List<ChartTimePoint>?")]
    public List<ChartTimePoint>? TimeData { get; set; }

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
