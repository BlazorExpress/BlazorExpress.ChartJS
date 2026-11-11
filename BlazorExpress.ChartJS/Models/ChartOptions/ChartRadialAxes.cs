namespace BlazorExpress.ChartJS;

/// <summary>
/// The options of the radial linear axis used by radar and polar area charts.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/axes/radial/linear.html" /> for more information.
/// </para>
/// </summary>
public class ChartRadialAxes
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the lines that run from the center to each point label.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the lines that run from the center to each point label.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartAxesAngleLines?")]
    public ChartAxesAngleLines? AngleLines { get; set; }

    /// <summary>
    /// Gets or sets the background color of the chart area inside the axis.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the background color of the chart area inside the axis.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the scale starts at zero, if the data does not already include it.
    /// <para>
    /// Default value is <see langword="true" />, like <see cref="ChartAxes.BeginAtZero" />. This differs from Chart.js on radar charts,
    /// which start from the data minimum; polar area charts also start at zero in Chart.js.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the scale starts at zero, if the data does not already include it.")]
    public bool BeginAtZero { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the axis is displayed.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js shows the axis).
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the axis is displayed.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Display { get; set; }

    /// <summary>
    /// Gets or sets the grid lines, the rings drawn at each tick. Set <see cref="ChartAxesGrid.Circular" /> for round rings on radar charts.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the grid lines, the rings drawn at each tick.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartAxesGrid?")]
    public ChartAxesGrid? Grid { get; set; }

    /// <summary>
    /// Gets or sets the user-defined maximum, which overrides the maximum from the data.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the user-defined maximum, which overrides the maximum from the data.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Max { get; set; }

    /// <summary>
    /// Gets or sets the user-defined minimum, which overrides the minimum from the data.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the user-defined minimum, which overrides the minimum from the data.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Min { get; set; }

    /// <summary>
    /// Gets or sets the labels drawn at the end of each angle line, such as the radar chart's labels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the labels drawn at the end of each angle line, such as the radar chart's labels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartAxesPointLabels?")]
    public ChartAxesPointLabels? PointLabels { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the axis runs in reverse, with the maximum at the center.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the axis runs in reverse, with the maximum at the center.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Reverse { get; set; }

    /// <summary>
    /// Gets or sets the starting angle of the axis, in degrees clockwise from the top.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 0).
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the starting angle of the axis, in degrees clockwise from the top.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? StartAngle { get; set; }

    /// <summary>
    /// Gets or sets a value used to calculate the maximum, unless the data is larger.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value used to calculate the maximum, unless the data is larger.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? SuggestedMax { get; set; }

    /// <summary>
    /// Gets or sets a value used to calculate the minimum, unless the data is smaller.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value used to calculate the minimum, unless the data is smaller.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? SuggestedMin { get; set; }

    /// <summary>
    /// Gets or sets the tick configuration. Radial ticks also support <see cref="ChartAxesTicks.ShowLabelBackdrop" />,
    /// <see cref="ChartAxesTicks.BackdropColor" /> and <see cref="ChartAxesTicks.BackdropPadding" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the tick configuration.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartAxesTicks?")]
    public ChartAxesTicks? Ticks { get; set; }

    /// <summary>
    /// Gets or sets the axis type, for example <see cref="ChartAxesType.RadialLinear" />.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses <c>"radialLinear"</c> on radar and polar area charts).
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the axis type, for example ChartAxesType.RadialLinear.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Type { get; set; }

    #endregion
}

/// <summary>
/// The angle lines of a radial axis, which run from the center to each point label.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/axes/radial/#angle-line-options" /> for more information.
/// </para>
/// </summary>
public class ChartAxesAngleLines
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the length and spacing of dashes on the angle lines.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the length and spacing of dashes on the angle lines.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("List<double>?")]
    public List<double>? BorderDash { get; set; }

    /// <summary>
    /// Gets or sets the offset for the line dashes.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the offset for the line dashes.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BorderDashOffset { get; set; }

    /// <summary>
    /// Gets or sets the color of the angle lines.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color of the angle lines.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the angle lines are shown.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js shows them on radar charts and hides them on polar area charts).
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the angle lines are shown.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Display { get; set; }

    /// <summary>
    /// Gets or sets the width of the angle lines in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the width of the angle lines in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? LineWidth { get; set; }

    #endregion
}

/// <summary>
/// The point labels of a radial axis, drawn at the end of each angle line.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/axes/radial/#point-label-options" /> for more information.
/// </para>
/// </summary>
public class ChartAxesPointLabels
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the background color of the point labels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the background color of the point labels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BackdropColor { get; set; }

    /// <summary>
    /// Gets or sets the padding of the point label background.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 2).
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the padding of the point label background.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartPadding?")]
    public ChartPadding? BackdropPadding { get; set; }

    /// <summary>
    /// Gets or sets the border radius of the point label background, in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the border radius of the point label background, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? BorderRadius { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether point labels are centered on their slice, as on polar area charts.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether point labels are centered on their slice, as on polar area charts.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? CenterPointLabels { get; set; }

    /// <summary>
    /// Gets or sets the color of the point labels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color of the point labels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the point labels are shown.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js shows them on radar charts and hides them on polar area charts).
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the point labels are shown.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Display { get; set; }

    /// <summary>
    /// Gets or sets the font of the point labels. A new <see cref="ChartFont" /> sends the library defaults (size 12, bold).
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the font of the point labels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartFont?")]
    public ChartFont? Font { get; set; }

    /// <summary>
    /// Gets or sets the distance between the chart area and the point labels, in pixels.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 5).
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the distance between the chart area and the point labels, in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Padding { get; set; }

    #endregion
}
