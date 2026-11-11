namespace BlazorExpress.ChartJS;

/// <summary>
/// The style of each line segment, the part of the line between two points, sent as the Chart.js line <c>segment</c> option.
/// Set a value to style every segment the same, or a registered JavaScript function to style each segment, for example to dash the
/// segments that span a gap: <c>SegmentStyle = new() { BorderDashFunction = new ChartFunction("dashGaps") }</c>.
/// Chart.js calls each function with the segment context <c>{ type: 'segment', p0, p1, p0DataIndex, p1DataIndex, datasetIndex }</c>.
/// Only the options that are set are sent.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" /> for more information.
/// </para>
/// </summary>
public sealed class ChartLineSegment
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the fill color under the segment, the same for every segment.
    /// When <see cref="BackgroundColorFunction" /> is set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the fill color under the segment, the same for every segment. When BackgroundColorFunction is set, it is sent instead.")]
    [JsonIgnore]
    [ParameterTypeName("string?")]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the fill color under the segment for each segment, as
    /// <c>(context) =&gt; color</c>, where <c>context</c> is <c>{ type: 'segment', p0, p1, p0DataIndex, p1DataIndex, datasetIndex }</c>.
    /// Return <see langword="undefined" /> to keep the dataset value. When set, it is sent instead of <see cref="BackgroundColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the fill color under the segment for each segment, as (context) => color. When set, it is sent instead of BackgroundColor.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? BackgroundColorFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>backgroundColor</c>: <see cref="BackgroundColorFunction" /> when set, otherwise <see cref="BackgroundColor" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("backgroundColor")]
    internal object? BackgroundColorValue => (object?)BackgroundColorFunction ?? BackgroundColor;

    /// <summary>
    /// Gets or sets the cap style of the segment: <c>"butt"</c>, <c>"round"</c> or <c>"square"</c>, the same for every segment.
    /// When <see cref="BorderCapStyleFunction" /> is set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the cap style of the segment: \"butt\", \"round\" or \"square\", the same for every segment. When BorderCapStyleFunction is set, it is sent instead.")]
    [JsonIgnore]
    [ParameterTypeName("string?")]
    public string? BorderCapStyle { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the cap style of the segment: <c>"butt"</c>, <c>"round"</c> or <c>"square"</c> for each segment, as
    /// <c>(context) =&gt; string</c>, where <c>context</c> is <c>{ type: 'segment', p0, p1, p0DataIndex, p1DataIndex, datasetIndex }</c>.
    /// Return <see langword="undefined" /> to keep the dataset value. When set, it is sent instead of <see cref="BorderCapStyle" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the cap style of the segment: \"butt\", \"round\" or \"square\" for each segment, as (context) => string. When set, it is sent instead of BorderCapStyle.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? BorderCapStyleFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>borderCapStyle</c>: <see cref="BorderCapStyleFunction" /> when set, otherwise <see cref="BorderCapStyle" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("borderCapStyle")]
    internal object? BorderCapStyleValue => (object?)BorderCapStyleFunction ?? BorderCapStyle;

    /// <summary>
    /// Gets or sets the color of the segment, the same for every segment.
    /// When <see cref="BorderColorFunction" /> is set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color of the segment, the same for every segment. When BorderColorFunction is set, it is sent instead.")]
    [JsonIgnore]
    [ParameterTypeName("string?")]
    public string? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the color of the segment for each segment, as
    /// <c>(context) =&gt; color</c>, where <c>context</c> is <c>{ type: 'segment', p0, p1, p0DataIndex, p1DataIndex, datasetIndex }</c>.
    /// Return <see langword="undefined" /> to keep the dataset value. When set, it is sent instead of <see cref="BorderColor" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the color of the segment for each segment, as (context) => color. When set, it is sent instead of BorderColor.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? BorderColorFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>borderColor</c>: <see cref="BorderColorFunction" /> when set, otherwise <see cref="BorderColor" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("borderColor")]
    internal object? BorderColorValue => (object?)BorderColorFunction ?? BorderColor;

    /// <summary>
    /// Gets or sets the dash pattern of the segment, as lengths of alternating lines and gaps in pixels, the same for every segment.
    /// When <see cref="BorderDashFunction" /> is set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the dash pattern of the segment, as lengths of alternating lines and gaps in pixels, the same for every segment. When BorderDashFunction is set, it is sent instead.")]
    [JsonIgnore]
    [ParameterTypeName("List<double>?")]
    public List<double>? BorderDash { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the dash pattern of the segment, as lengths of alternating lines and gaps in pixels for each segment, as
    /// <c>(context) =&gt; number[]</c>, where <c>context</c> is <c>{ type: 'segment', p0, p1, p0DataIndex, p1DataIndex, datasetIndex }</c>.
    /// Return <see langword="undefined" /> to keep the dataset value. When set, it is sent instead of <see cref="BorderDash" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the dash pattern of the segment, as lengths of alternating lines and gaps in pixels for each segment, as (context) => number[]. When set, it is sent instead of BorderDash.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? BorderDashFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>borderDash</c>: <see cref="BorderDashFunction" /> when set, otherwise <see cref="BorderDash" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("borderDash")]
    internal object? BorderDashValue => (object?)BorderDashFunction ?? BorderDash;

    /// <summary>
    /// Gets or sets the offset of the dash pattern of the segment, in pixels, the same for every segment.
    /// When <see cref="BorderDashOffsetFunction" /> is set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the offset of the dash pattern of the segment, in pixels, the same for every segment. When BorderDashOffsetFunction is set, it is sent instead.")]
    [JsonIgnore]
    [ParameterTypeName("double?")]
    public double? BorderDashOffset { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the offset of the dash pattern of the segment, in pixels for each segment, as
    /// <c>(context) =&gt; number</c>, where <c>context</c> is <c>{ type: 'segment', p0, p1, p0DataIndex, p1DataIndex, datasetIndex }</c>.
    /// Return <see langword="undefined" /> to keep the dataset value. When set, it is sent instead of <see cref="BorderDashOffset" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the offset of the dash pattern of the segment, in pixels for each segment, as (context) => number. When set, it is sent instead of BorderDashOffset.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? BorderDashOffsetFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>borderDashOffset</c>: <see cref="BorderDashOffsetFunction" /> when set, otherwise <see cref="BorderDashOffset" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("borderDashOffset")]
    internal object? BorderDashOffsetValue => (object?)BorderDashOffsetFunction ?? BorderDashOffset;

    /// <summary>
    /// Gets or sets the join style of the segment: <c>"bevel"</c>, <c>"round"</c> or <c>"miter"</c>, the same for every segment.
    /// When <see cref="BorderJoinStyleFunction" /> is set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the join style of the segment: \"bevel\", \"round\" or \"miter\", the same for every segment. When BorderJoinStyleFunction is set, it is sent instead.")]
    [JsonIgnore]
    [ParameterTypeName("string?")]
    public string? BorderJoinStyle { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the join style of the segment: <c>"bevel"</c>, <c>"round"</c> or <c>"miter"</c> for each segment, as
    /// <c>(context) =&gt; string</c>, where <c>context</c> is <c>{ type: 'segment', p0, p1, p0DataIndex, p1DataIndex, datasetIndex }</c>.
    /// Return <see langword="undefined" /> to keep the dataset value. When set, it is sent instead of <see cref="BorderJoinStyle" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the join style of the segment: \"bevel\", \"round\" or \"miter\" for each segment, as (context) => string. When set, it is sent instead of BorderJoinStyle.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? BorderJoinStyleFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>borderJoinStyle</c>: <see cref="BorderJoinStyleFunction" /> when set, otherwise <see cref="BorderJoinStyle" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("borderJoinStyle")]
    internal object? BorderJoinStyleValue => (object?)BorderJoinStyleFunction ?? BorderJoinStyle;

    /// <summary>
    /// Gets or sets the width of the segment, in pixels, the same for every segment.
    /// When <see cref="BorderWidthFunction" /> is set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the width of the segment, in pixels, the same for every segment. When BorderWidthFunction is set, it is sent instead.")]
    [JsonIgnore]
    [ParameterTypeName("double?")]
    public double? BorderWidth { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the width of the segment, in pixels for each segment, as
    /// <c>(context) =&gt; number</c>, where <c>context</c> is <c>{ type: 'segment', p0, p1, p0DataIndex, p1DataIndex, datasetIndex }</c>.
    /// Return <see langword="undefined" /> to keep the dataset value. When set, it is sent instead of <see cref="BorderWidth" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#segment" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the width of the segment, in pixels for each segment, as (context) => number. When set, it is sent instead of BorderWidth.")]
    [JsonIgnore]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? BorderWidthFunction { get; set; }

    /// <summary>
    /// Gets the value sent as <c>borderWidth</c>: <see cref="BorderWidthFunction" /> when set, otherwise <see cref="BorderWidth" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("borderWidth")]
    internal object? BorderWidthValue => (object?)BorderWidthFunction ?? BorderWidth;

    #endregion
}
