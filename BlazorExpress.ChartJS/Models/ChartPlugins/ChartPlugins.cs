namespace BlazorExpress.ChartJS;

/// <summary>
/// The Chart.js plugin options shared by every chart type, sent as <c>options.plugins</c>.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/developers/plugins.html" /> for more information.
/// </para>
/// </summary>
public class ChartPlugins
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the options of the built-in colors plugin, which gives datasets without colors a default palette.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the options of the built-in colors plugin, which gives datasets without colors a default palette.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartPluginsColors?")]
    public ChartPluginsColors? Colors { get; set; }

    /// <summary>
    /// Gets or sets the options of the built-in filler plugin, which draws area fills for datasets with fill set.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the options of the built-in filler plugin, which draws area fills for datasets with fill set.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartPluginsFiller?")]
    public ChartPluginsFiller? Filler { get; set; }

    /// <summary>
    /// Gets or sets the chart legend, which displays data about the datasets that appear on the chart.
    /// <para>
    /// Default value is a new <see cref="ChartPluginsLegend" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the chart legend, which displays data about the datasets that appear on the chart.")]
    public ChartPluginsLegend Legend { get; set; } = new();

    /// <summary>
    /// Gets or sets the subtitle, drawn below the title.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the subtitle, drawn below the title.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartPluginsSubtitle?")]
    public ChartPluginsSubtitle? Subtitle { get; set; }

    /// <summary>
    /// Gets or sets the chart title, the text drawn at the top of the chart.
    /// <para>
    /// Default value is a new <see cref="ChartPluginsTitle" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the chart title, the text drawn at the top of the chart.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartPluginsTitle?")]
    public ChartPluginsTitle? Title { get; set; } = new();

    /// <summary>
    /// Gets or sets the tooltip options.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the tooltip options.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartPluginsTooltip?")]
    public ChartPluginsTooltip? Tooltip { get; set; }

    #endregion
}

/// <summary>
/// The chart legend displays data about the datasets that are appearing on the chart.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/configuration/legend.html" /> for more information.
/// </para>
/// </summary>
public class ChartPluginsLegend
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the alignment of the legend: "start", "center" or "end".
    /// <para>
    /// Default value is <c>"center"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("center")]
    [Description("Gets or sets the alignment of the legend: \"start\", \"center\" or \"end\".")]
    [ParameterTypeName("string?")]
    public string? Align { get; set; } = "center";

    /// <summary>
    /// Gets or sets a value indicating whether the legend is shown.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the legend is shown.")]
    public bool Display { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the legend takes the full width or height of the canvas, moving other boxes.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the legend takes the full width or height of the canvas, moving other boxes.")]
    public bool FullSize { get; set; } = true;

    /// <summary>
    /// Gets or sets the legend label settings.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the legend label settings.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartPluginsLegendLabels?")]
    public ChartPluginsLegendLabels? Labels { get; set; }

    /// <summary>
    /// Gets or sets the maximum height of the legend, in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the maximum height of the legend, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? MaxHeight { get; set; }

    /// <summary>
    /// Gets or sets the maximum width of the legend, in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the maximum width of the legend, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? MaxWidth { get; set; }

    /// <summary>
    /// Gets or sets the position of the legend: "top", "left", "bottom", "right" or "chartArea".
    /// <para>
    /// Default value is <c>"top"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("top")]
    [Description("Gets or sets the position of the legend: \"top\", \"left\", \"bottom\", \"right\" or \"chartArea\".")]
    public string Position { get; set; } = "top";

    /// <summary>
    /// Gets or sets a value indicating whether the legend shows datasets in reverse order.
    /// <para>
    /// Default value is <see langword="false" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the legend shows datasets in reverse order.")]
    public bool Reverse { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether the legend is rendered from right to left.
    /// <para>
    /// Default value is <see langword="false" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the legend is rendered from right to left.")]
    public bool Rtl { get; set; } = false;

    /// <summary>
    /// Gets or sets the text direction, "rtl" or "ltr", forced on the canvas for the legend regardless of the CSS of the canvas.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the text direction, \"rtl\" or \"ltr\", forced on the canvas for the legend regardless of the CSS of the canvas.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? TextDirection { get; set; }

    /// <summary>
    /// Gets or sets the legend title.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the legend title.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartPluginsLegendTitle?")]
    public ChartPluginsLegendTitle? Title { get; set; }

    #endregion
}

/// <summary>
/// The title of the legend.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/configuration/legend.html#legend-title-configuration" /> for more information.
/// </para>
/// </summary>
public class ChartPluginsLegendTitle
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the color of the legend title.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color of the legend title.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the legend title is shown.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the legend title is shown.")]
    public bool Display { get; set; } = true;

    /// <summary>
    /// Gets or sets the padding around the legend title, in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the padding around the legend title, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? Padding { get; set; }

    /// <summary>
    /// Gets or sets the legend title text.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the legend title text.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Text { get; set; }

    #endregion
}

/// <summary>
/// The legend label settings.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/configuration/legend.html#legend-label-configuration" /> for more information.
/// </para>
/// </summary>
public class ChartPluginsLegendLabels
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the border radius of the colored box, used when UseBorderRadius is true.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the border radius of the colored box, used when UseBorderRadius is true.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? BorderRadius { get; set; }

    /// <summary>
    /// Gets or sets the height of the colored box.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the height of the colored box.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? BoxHeight { get; set; }

    /// <summary>
    /// Gets or sets the width of the colored box.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the width of the colored box.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? BoxWidth { get; set; }

    /// <summary>
    /// Gets or sets the color of the label and the strikethrough.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color of the label and the strikethrough.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the font of the labels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the font of the labels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartFont?")]
    public ChartFont? Font { get; set; }

    /// <summary>
    /// Gets or sets the padding between rows of colored boxes.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the padding between rows of colored boxes.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? Padding { get; set; }

    /// <summary>
    /// Gets or sets the point style used for the legend. Only used if UsePointStyle is true.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the point style used for the legend. Only used if UsePointStyle is true.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? PointStyle { get; set; }

    /// <summary>
    /// Gets or sets the width of the point style used for the legend, when UsePointStyle is true.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the width of the point style used for the legend, when UsePointStyle is true.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? PointStyleWidth { get; set; }

    /// <summary>
    /// Gets or sets the horizontal alignment of the label text: "left", "right" or "center".
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "center").
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the horizontal alignment of the label text: \"left\", \"right\" or \"center\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? TextAlign { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the colored box uses BorderRadius.
    /// <para>
    /// Default value is <see langword="false" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the colored box uses BorderRadius.")]
    public bool UseBorderRadius { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether the label style matches the corresponding point style.
    /// <para>
    /// Default value is <see langword="false" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the label style matches the corresponding point style.")]
    public bool UsePointStyle { get; set; } = false;

    #endregion
}

/// <summary>
/// The chart title defines text to draw at the top of the chart.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/configuration/title.html" /> for more information.
/// </para>
/// </summary>
public class ChartPluginsTitle
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the alignment of the title: "start", "center" or "end".
    /// <para>
    /// Default value is <c>"center"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("center")]
    [Description("Gets or sets the alignment of the title: \"start\", \"center\" or \"end\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Align { get; set; } = "center";

    /// <summary>
    /// Gets or sets the color of the text.
    /// <para>
    /// Default value is <c>"black"</c> (this differs from Chart.js, which uses <c>Chart.defaults.color</c> (<c>"#666"</c>)).
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("black")]
    [Description("Gets or sets the color of the text.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Color { get; set; } = "black";

    /// <summary>
    /// Gets or sets a value indicating whether the title is shown.
    /// <para>
    /// Default value is <see langword="false" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the title is shown.")]
    public bool Display { get; set; }

    /// <summary>
    /// Gets or sets the font of the title. A new ChartFont sends the library defaults (size 12, bold).
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the font of the title. A new ChartFont sends the library defaults (size 12, bold).")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartFont?")]
    public ChartFont? Font { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the title takes the full width of the canvas, moving other boxes.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses true).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the title takes the full width of the canvas, moving other boxes.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? FullSize { get; set; }

    /// <summary>
    /// Gets or sets the padding around the title: a number for top and bottom, or a ChartPadding with Top and Bottom.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 10).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the padding around the title: a number for top and bottom, or a ChartPadding with Top and Bottom.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartPadding?")]
    public ChartPadding? Padding { get; set; }

    /// <summary>
    /// Gets or sets the position of the title: "top", "left", "bottom" or "right".
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "top").
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the position of the title: \"top\", \"left\", \"bottom\" or \"right\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Position { get; set; }

    /// <summary>
    /// Gets or sets the title text.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the title text.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Text { get; set; }

    #endregion
}

/// <summary>
/// The tooltip shown when hovering over chart items.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/configuration/tooltip.html" /> for more information.
/// </para>
/// </summary>
public class ChartPluginsTooltip
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the background color of the tooltip.
    /// <para>
    /// Default value is <c>"rgba(0, 0, 0, 0.8)"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.8)")]
    [Description("Gets or sets the background color of the tooltip.")]
    public string BackgroundColor { get; set; } = "rgba(0, 0, 0, 0.8)";

    /// <summary>
    /// Gets or sets the horizontal alignment of the body text lines: "left", "right" or "center".
    /// <para>
    /// Default value is <c>"left"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("left")]
    [Description("Gets or sets the horizontal alignment of the body text lines: \"left\", \"right\" or \"center\".")]
    public string BodyAlign { get; set; } = "left";

    /// <summary>
    /// Gets or sets the color of the body text.
    /// <para>
    /// Default value is <c>"#fff"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("#fff")]
    [Description("Gets or sets the color of the body text.")]
    public string BodyColor { get; set; } = "#fff";

    /// <summary>
    /// Gets or sets the font of the body text.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the font of the body text.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartPluginsTooltipFont?")]
    public ChartPluginsTooltipFont? BodyFont { get; set; }

    /// <summary>
    /// Gets or sets the spacing added to the top and bottom of each tooltip item.
    /// <para>
    /// Default value is 2.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(2)]
    [Description("Gets or sets the spacing added to the top and bottom of each tooltip item.")]
    public int BodySpacing { get; set; } = 2;

    /// <summary>
    /// Gets or sets the color of the tooltip border.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "rgba(0, 0, 0, 0)").
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color of the tooltip border.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the width of the tooltip border, in pixels.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 0).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the width of the tooltip border, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BorderWidth { get; set; }

    /// <summary>
    /// Gets or sets the height of the color box when DisplayColors is true.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses the body font size).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the height of the color box when DisplayColors is true.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BoxHeight { get; set; }

    /// <summary>
    /// Gets or sets the padding between the color box and the text.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 1).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the padding between the color box and the text.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BoxPadding { get; set; }

    /// <summary>
    /// Gets or sets the width of the color box when DisplayColors is true.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses the body font size).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the width of the color box when DisplayColors is true.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BoxWidth { get; set; }

    /// <summary>
    /// Gets or sets the extra distance that moves the end of the tooltip arrow away from the tooltip point.
    /// <para>
    /// Default value is 2.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(2)]
    [Description("Gets or sets the extra distance that moves the end of the tooltip arrow away from the tooltip point.")]
    public int CaretPadding { get; set; } = 2;

    /// <summary>
    /// Gets or sets the size of the tooltip arrow, in pixels.
    /// <para>
    /// Default value is 5.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(5)]
    [Description("Gets or sets the size of the tooltip arrow, in pixels.")]
    public int CaretSize { get; set; } = 5;

    /// <summary>
    /// Gets or sets the radius of the tooltip corners, in pixels.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 6).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the radius of the tooltip corners, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? CornerRadius { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether color boxes are shown in the tooltip.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether color boxes are shown in the tooltip.")]
    public bool DisplayColors { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether on-canvas tooltips are enabled.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether on-canvas tooltips are enabled.")]
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the horizontal alignment of the footer text lines: "left", "right" or "center".
    /// <para>
    /// Default value is <c>"left"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("left")]
    [Description("Gets or sets the horizontal alignment of the footer text lines: \"left\", \"right\" or \"center\".")]
    public string FooterAlign { get; set; } = "left";

    /// <summary>
    /// Gets or sets the color of the footer text.
    /// <para>
    /// Default value is <c>"#fff"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("#fff")]
    [Description("Gets or sets the color of the footer text.")]
    public string FooterColor { get; set; } = "#fff";

    /// <summary>
    /// Gets or sets the font of the footer text.
    /// <para>
    /// Default value is a new <see cref="ChartPluginsTooltipFont" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the font of the footer text.")]
    public ChartPluginsTooltipFont FooterFont { get; set; } = new();

    /// <summary>
    /// Gets or sets the margin added before drawing the footer.
    /// <para>
    /// Default value is 6.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(6)]
    [Description("Gets or sets the margin added before drawing the footer.")]
    public int FooterMarginTop { get; set; } = 6;

    /// <summary>
    /// Gets or sets the spacing added to the top and bottom of each footer line.
    /// <para>
    /// Default value is 2.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(2)]
    [Description("Gets or sets the spacing added to the top and bottom of each footer line.")]
    public int FooterSpacing { get; set; } = 2;

    /// <summary>
    /// Gets or sets a value indicating whether the tooltip applies only when the pointer intersects an item.
    /// <para>
    /// Default value is <see langword="null" /> (the interaction value).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the tooltip applies only when the pointer intersects an item.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Intersect { get; set; }

    /// <summary>
    /// Gets or sets which elements appear in the tooltip. See InteractionMode.
    /// <para>
    /// Default value is <see langword="null" /> (the interaction value).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets which elements appear in the tooltip. See InteractionMode.")]
    [JsonConverter(typeof(CamelCaseEnumConverter<InteractionMode>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("InteractionMode?")]
    public InteractionMode? Mode { get; set; }

    /// <summary>
    /// Gets or sets the color drawn behind the color boxes when several items are in the tooltip.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "#fff").
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color drawn behind the color boxes when several items are in the tooltip.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? MultiKeyBackground { get; set; }

    /// <summary>
    /// Gets or sets the padding inside the tooltip.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 6).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the padding inside the tooltip.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartPadding?")]
    public ChartPadding? Padding { get; set; }

    /// <summary>
    /// Gets or sets where the tooltip is placed: "average" or "nearest".
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "average").
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets where the tooltip is placed: \"average\" or \"nearest\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Position { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the tooltip is rendered from right to left.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the tooltip is rendered from right to left.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Rtl { get; set; }

    /// <summary>
    /// Gets or sets the text direction, "rtl" or "ltr", forced on the canvas for the tooltip regardless of the CSS of the canvas.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the text direction, \"rtl\" or \"ltr\", forced on the canvas for the tooltip regardless of the CSS of the canvas.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? TextDirection { get; set; }

    /// <summary>
    /// Gets or sets the horizontal alignment of the title text lines: "left", "right" or "center".
    /// <para>
    /// Default value is <c>"left"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("left")]
    [Description("Gets or sets the horizontal alignment of the title text lines: \"left\", \"right\" or \"center\".")]
    public string TitleAlign { get; set; } = "left";

    /// <summary>
    /// Gets or sets the color of the title text.
    /// <para>
    /// Default value is <c>"#fff"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("#fff")]
    [Description("Gets or sets the color of the title text.")]
    public string TitleColor { get; set; } = "#fff";

    /// <summary>
    /// Gets or sets the font of the title text.
    /// <para>
    /// Default value is a new <see cref="ChartPluginsTooltipFont" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the font of the title text.")]
    public ChartPluginsTooltipFont TitleFont { get; set; } = new();

    /// <summary>
    /// Gets or sets the margin added below the title section.
    /// <para>
    /// Default value is 6.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(6)]
    [Description("Gets or sets the margin added below the title section.")]
    public int TitleMarginBottom { get; set; } = 6;

    /// <summary>
    /// Gets or sets the spacing added to the top and bottom of each title line.
    /// <para>
    /// Default value is 2.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(2)]
    [Description("Gets or sets the spacing added to the top and bottom of each title line.")]
    public int TitleSpacing { get; set; } = 2;

    /// <summary>
    /// Gets or sets a value indicating whether the color boxes use the dataset point style instead of a square.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the color boxes use the dataset point style instead of a square.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? UsePointStyle { get; set; }

    /// <summary>
    /// Gets or sets the position of the tooltip caret in the X direction: "left", "center" or "right".
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the position of the tooltip caret in the X direction: \"left\", \"center\" or \"right\".")]
    [ParameterTypeName("string?")]
    public string? XAlign { get; set; }

    /// <summary>
    /// Gets or sets the position of the tooltip caret in the Y direction: "top", "center" or "bottom".
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the position of the tooltip caret in the Y direction: \"top\", \"center\" or \"bottom\".")]
    [ParameterTypeName("string?")]
    public string? YAlign { get; set; }

    #endregion
}

/// <summary>
/// A tooltip font.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/general/fonts.html" /> for more information.
/// </para>
/// </summary>
public class ChartPluginsTooltipFont
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the font family, following CSS font-family.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the font family, following CSS font-family.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Family { get; set; }

    /// <summary>
    /// Gets or sets the height of a line of text.
    /// <para>
    /// Default value is 1.2.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(1.2)]
    [Description("Gets or sets the height of a line of text.")]
    public double LineHeight { get; set; } = 1.2;

    /// <summary>
    /// Gets or sets the font size in pixels.
    /// <para>
    /// Default value is 12.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(12)]
    [Description("Gets or sets the font size in pixels.")]
    public int Size { get; set; } = 12;

    /// <summary>
    /// Gets or sets the font style, following CSS font-style.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the font style, following CSS font-style.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Style { get; set; }

    /// <summary>
    /// Gets or sets the font weight (boldness).
    /// <para>
    /// Default value is <c>"bold"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("bold")]
    [Description("Gets or sets the font weight (boldness).")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Weight { get; set; } = "bold";

    #endregion
}
