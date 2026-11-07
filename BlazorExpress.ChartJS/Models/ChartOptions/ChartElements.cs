namespace BlazorExpress.ChartJS;

/// <summary>
/// Default styles for the elements every dataset of the chart draws, sent as <c>options.elements</c>. A dataset's own value overrides them. One model serves every chart type; Chart.js ignores the element types a chart does not draw.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/configuration/elements.html" /> for more information.
/// </para>
/// <para>
/// Some datasets always send their own value for a few of these styles (for example line <c>Tension</c> and polar area <c>Circular</c>), so the matching default has no effect on those charts; each such property says which charts.
/// </para>
/// </summary>
public class ChartElements
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the defaults for arcs, used by doughnut, pie and polar area charts.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the defaults for arcs, used by doughnut, pie and polar area charts.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartElementsArc?")]
    public ChartElementsArc? Arc { get; set; }

    /// <summary>
    /// Gets or sets the defaults for bars.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the defaults for bars.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartElementsBar?")]
    public ChartElementsBar? Bar { get; set; }

    /// <summary>
    /// Gets or sets the defaults for lines, used by line and radar charts.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the defaults for lines, used by line and radar charts.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartElementsLine?")]
    public ChartElementsLine? Line { get; set; }

    /// <summary>
    /// Gets or sets the defaults for points, used by line, radar, scatter and bubble charts.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the defaults for points, used by line, radar, scatter and bubble charts.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartElementsPoint?")]
    public ChartElementsPoint? Point { get; set; }

    #endregion
}

/// <summary>
/// Default styles for arcs (<c>options.elements.arc</c>).
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/configuration/elements.html#arc-configuration" /> for more information.
/// </para>
/// <para>
/// Doughnut, pie and polar area datasets always send <c>BorderDashOffset</c>, and polar area datasets <c>Circular</c>, so those two defaults have no effect on those charts.
/// </para>
/// </summary>
public class ChartElementsArc
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the default angle of each arc on polar area charts, in degrees.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default angle of each arc on polar area charts, in degrees.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Angle { get; set; }

    /// <summary>
    /// Gets or sets the default fill color of arcs.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default fill color of arcs.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets where the arc border is drawn: "center" or "inner".
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "center").
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets where the arc border is drawn: \"center\" or \"inner\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BorderAlign { get; set; }

    /// <summary>
    /// Gets or sets the default border color of arcs.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "#fff").
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default border color of arcs.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the default length and spacing of dashes on arc borders.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default length and spacing of dashes on arc borders.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("List<double>?")]
    public List<double>? BorderDash { get; set; }

    /// <summary>
    /// Gets or sets the default offset for arc border dashes.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <para>
    /// Doughnut, pie and polar area datasets always send their own value, so this default has no effect on those charts; set <c>BorderDashOffset</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default offset for arc border dashes.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BorderDashOffset { get; set; }

    /// <summary>
    /// Gets or sets the default arc border join style: "bevel", "round" or "miter".
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default arc border join style: \"bevel\", \"round\" or \"miter\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BorderJoinStyle { get; set; }

    /// <summary>
    /// Gets or sets the default arc border radius, in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default arc border radius, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BorderRadius { get; set; }

    /// <summary>
    /// Gets or sets the default arc border width, in pixels.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 2).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default arc border width, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BorderWidth { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether arcs are curved (true) or flat (false).
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses true).
    /// </para>
    /// <para>
    /// Polar area datasets always send their own value, so this default has no effect on those charts; set <c>Circular</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether arcs are curved (true) or flat (false).")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Circular { get; set; }

    #endregion
}

/// <summary>
/// Default styles for bars (<c>options.elements.bar</c>).
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/configuration/elements.html#bar-configuration" /> for more information.
/// </para>
/// <para>
/// Bar datasets always send <c>BorderSkipped</c>, so that default has no effect.
/// </para>
/// </summary>
public class ChartElementsBar
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the default fill color of bars.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default fill color of bars.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the default border color of bars.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default border color of bars.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the default border radius of bars, in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default border radius of bars, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BorderRadius { get; set; }

    /// <summary>
    /// Gets or sets the default edge to skip when drawing the border: "start", "end", "middle", "bottom", "left", "top" or "right".
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "start").
    /// </para>
    /// <para>
    /// Bar datasets always send their own value, so this default has no effect on those charts; set <c>BorderSkipped</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default edge to skip when drawing the border: \"start\", \"end\", \"middle\", \"bottom\", \"left\", \"top\" or \"right\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BorderSkipped { get; set; }

    /// <summary>
    /// Gets or sets the default border width of bars, in pixels.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 0).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default border width of bars, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BorderWidth { get; set; }

    /// <summary>
    /// Gets or sets how many pixels bars are inflated by when drawn, to avoid gaps; a number or "auto".
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "auto").
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets how many pixels bars are inflated by when drawn, to avoid gaps; a number or \"auto\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartDimension?")]
    public ChartDimension? InflateAmount { get; set; }

    /// <summary>
    /// Gets or sets the point style used for bars in the legend when the legend uses point styles.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the point style used for bars in the legend when the legend uses point styles.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? PointStyle { get; set; }

    #endregion
}

/// <summary>
/// Default styles for lines (<c>options.elements.line</c>).
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/configuration/elements.html#line-configuration" /> for more information.
/// </para>
/// <para>
/// Line, radar and scatter datasets always send their colours, <c>BorderCapStyle</c>, <c>BorderDashOffset</c>, <c>BorderJoinStyle</c>, <c>BorderWidth</c>, <c>Fill</c> and <c>Tension</c> (line and scatter also <c>CubicInterpolationMode</c> and <c>Stepped</c>), so those defaults have no effect on those charts; set them on the dataset. <c>BorderDash</c> and <c>CapBezierPoints</c> do apply.
/// </para>
/// </summary>
public class ChartElementsLine
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the default fill color of lines.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <para>
    /// Line, radar and scatter datasets always send their own value, so this default has no effect on those charts; set <c>BackgroundColor</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default fill color of lines.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the default line cap style: "butt", "round" or "square".
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <para>
    /// Line, radar and scatter datasets always send their own value, so this default has no effect on those charts; set <c>BorderCapStyle</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default line cap style: \"butt\", \"round\" or \"square\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BorderCapStyle { get; set; }

    /// <summary>
    /// Gets or sets the default color of lines.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <para>
    /// Line, radar and scatter datasets always send their own value, so this default has no effect on those charts; set <c>BorderColor</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default color of lines.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the default length and spacing of dashes on lines.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default length and spacing of dashes on lines.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("List<double>?")]
    public List<double>? BorderDash { get; set; }

    /// <summary>
    /// Gets or sets the default offset for line dashes.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <para>
    /// Line, radar and scatter datasets always send their own value, so this default has no effect on those charts; set <c>BorderDashOffset</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default offset for line dashes.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BorderDashOffset { get; set; }

    /// <summary>
    /// Gets or sets the default line join style: "bevel", "round" or "miter".
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <para>
    /// Line, radar and scatter datasets always send their own value, so this default has no effect on those charts; set <c>BorderJoinStyle</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default line join style: \"bevel\", \"round\" or \"miter\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BorderJoinStyle { get; set; }

    /// <summary>
    /// Gets or sets the default width of lines, in pixels.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 3).
    /// </para>
    /// <para>
    /// Line, radar and scatter datasets always send their own value, so this default has no effect on those charts; set <c>BorderWidth</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default width of lines, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BorderWidth { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether control points are kept inside the chart area.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses true).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether control points are kept inside the chart area.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? CapBezierPoints { get; set; }

    /// <summary>
    /// Gets or sets the default interpolation mode: "default" or "monotone".
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <para>
    /// Line and scatter datasets always send their own value, so this default has no effect on those charts; set <c>CubicInterpolationMode</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default interpolation mode: \"default\" or \"monotone\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? CubicInterpolationMode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether lines are filled by default.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses false).
    /// </para>
    /// <para>
    /// Line, radar and scatter datasets always send their own value, so this default has no effect on those charts; set <c>Fill</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether lines are filled by default.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Fill { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether lines are drawn as stepped lines by default.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <para>
    /// Line and scatter datasets always send their own value, so this default has no effect on those charts; set <c>Stepped</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether lines are drawn as stepped lines by default.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Stepped { get; set; }

    /// <summary>
    /// Gets or sets the default Bezier curve tension; 0 draws straight lines.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 0).
    /// </para>
    /// <para>
    /// Line, radar and scatter datasets always send their own value, so this default has no effect on those charts; set <c>Tension</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default Bezier curve tension; 0 draws straight lines.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Tension { get; set; }

    #endregion
}

/// <summary>
/// Default styles for points (<c>options.elements.point</c>).
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/configuration/elements.html#point-configuration" /> for more information.
/// </para>
/// <para>
/// Line, radar and scatter datasets always send <c>backgroundColor</c>, <c>borderColor</c> and <c>borderWidth</c>, and bubble datasets <c>borderWidth</c>, which Chart.js also uses for their points, so those defaults have no effect on those charts.
/// </para>
/// </summary>
public class ChartElementsPoint
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the default fill color of points.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <para>
    /// Line, radar and scatter datasets always send <c>backgroundColor</c>, which Chart.js also uses for their points, so this default has no effect on those charts; set <c>PointBackgroundColor</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default fill color of points.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the default border color of points.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <para>
    /// Line, radar and scatter datasets always send <c>borderColor</c>, which Chart.js also uses for their points, so this default has no effect on those charts; set <c>PointBorderColor</c> on the dataset instead.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default border color of points.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the default border width of points, in pixels.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 1).
    /// </para>
    /// <para>
    /// Line, radar, scatter and bubble datasets always send <c>borderWidth</c>, which Chart.js also uses for their points, so this default has no effect on those charts; set <c>PointBorderWidth</c> on the dataset instead (on a bubble dataset, <c>BorderWidth</c>).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default border width of points, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BorderWidth { get; set; }

    /// <summary>
    /// Gets or sets the extra radius, in pixels, added to points for hit detection.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 1).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the extra radius, in pixels, added to points for hit detection.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? HitRadius { get; set; }

    /// <summary>
    /// Gets or sets the default border width of points when hovered, in pixels.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 1).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default border width of points when hovered, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? HoverBorderWidth { get; set; }

    /// <summary>
    /// Gets or sets the default radius of points when hovered, in pixels.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 4).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default radius of points when hovered, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? HoverRadius { get; set; }

    /// <summary>
    /// Gets or sets the default point style: "circle", "cross", "crossRot", "dash", "line", "rect", "rectRounded", "rectRot", "star" or "triangle".
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "circle").
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default point style: \"circle\", \"cross\", \"crossRot\", \"dash\", \"line\", \"rect\", \"rectRounded\", \"rectRot\", \"star\" or \"triangle\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? PointStyle { get; set; }

    /// <summary>
    /// Gets or sets the default radius of points, in pixels; 0 hides them.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 3).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default radius of points, in pixels; 0 hides them.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Radius { get; set; }

    /// <summary>
    /// Gets or sets the default rotation of points, in degrees.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default rotation of points, in degrees.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Rotation { get; set; }

    #endregion
}
