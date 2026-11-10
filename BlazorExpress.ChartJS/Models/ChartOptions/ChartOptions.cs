namespace BlazorExpress.ChartJS;

public interface IChartOptions { }

/// <summary>
/// Represents the base configuration options for all chart types.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/general/options.html" /> for more information.
/// </para>
/// </summary>
public class ChartOptions : IChartOptions
{
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
    [ParameterTypeName("ChartAnimation?")]
    public ChartAnimation? Animation { get; set; }

    /// <summary>
    /// Gets or sets the canvas aspect ratio (width / height) used when MaintainAspectRatio is on.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 2, and 1 for doughnut, pie, polar area and radar charts).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the canvas aspect ratio (width / height) used when MaintainAspectRatio is on.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? AspectRatio { get; set; }

    /// <summary>
    /// Gets or sets the pixel ratio the canvas is drawn at, for example 2 for sharper exported images.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses the window's devicePixelRatio).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the pixel ratio the canvas is drawn at, for example 2 for sharper exported images.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? DevicePixelRatio { get; set; }

    /// <summary>
    /// Gets or sets the default styles of the points, lines, bars and arcs the chart draws.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the default styles of the points, lines, bars and arcs the chart draws.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartElements?")]
    public ChartElements? Elements { get; set; }

    /// <summary>
    /// Gets or sets the browser events the chart listens to, such as "mousemove", "mouseout", "click", "touchstart" and "touchmove".
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js listens to all five).
    /// </para>
    /// <para>
    /// Chart.js reacts only to the listed events. When <c>"click"</c> is left out, <c>OnClick</c> and the legend click (<c>OnLegendClick</c>) don't fire;
    /// when <c>"mousemove"</c> is left out, <c>OnHover</c>, hover styles and tooltips don't react to the pointer;
    /// when <c>"mouseout"</c> is left out, <c>OnHover</c> is not called when the pointer leaves the chart.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the browser events the chart listens to, such as \"mousemove\", \"mouseout\", \"click\", \"touchstart\" and \"touchmove\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("List<string>?")]
    public List<string>? Events { get; set; }

    /// <summary>
    /// Gets or sets the hover options. Values left null use the interaction options.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the hover options. Values left null use the interaction options.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartHover?")]
    public ChartHover? Hover { get; set; }

    /// <summary>
    /// Gets or sets the locale for the chart.
    /// <para>
    /// By default, the chart is using the default locale of the platform which is running on.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/locale.html" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("By default, the chart is using the default locale of the platform which is running on.")]
    [Description("Gets or sets the locale for the chart.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Locale { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to maintain the original canvas aspect ratio (width / height) when resizing.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// <para>
    /// See <see href="https://www.chartjs.org/docs/latest/configuration/responsive.html#configuration-options" /> for more information.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to maintain the original canvas aspect ratio (width / height) when resizing.")]
    public bool MaintainAspectRatio { get; set; } = true;

    /// <summary>
    /// Gets or sets the registered JavaScript function Chart.js calls when the chart is clicked, as <c>(event, elements, chart) =&gt; void</c>.
    /// It runs alongside the component's .NET <see cref="ChartComponentBase.OnClick" /> event, which is still raised.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/interactions.html#events" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function Chart.js calls when the chart is clicked, as (event, elements, chart) => void. It runs alongside the component's .NET OnClick event, which is still raised.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? OnClick { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function Chart.js calls when the pointer moves over the chart, as <c>(event, elements, chart) =&gt; void</c>.
    /// It runs alongside the component's .NET <see cref="ChartComponentBase.OnHover" /> event, which is still raised.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/interactions.html#events" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function Chart.js calls when the pointer moves over the chart, as (event, elements, chart) => void. It runs alongside the component's .NET OnHover event, which is still raised.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? OnHover { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function Chart.js calls after the chart resizes, as <c>(chart, size) =&gt; void</c>,
    /// where <c>size</c> is <c>{ width, height }</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/responsive.html#configuration-options" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function Chart.js calls after the chart resizes, as (chart, size) => void, where size is { width, height }.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? OnResize { get; set; }

    /// <summary>
    /// Gets or sets the delay in milliseconds before the chart redraws after a resize, so rapid resizes cause one redraw.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 0).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the delay in milliseconds before the chart redraws after a resize, so rapid resizes cause one redraw.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? ResizeDelay { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the chart canvas should resize when its container does.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// <para>
    /// See <see href="https://www.chartjs.org/docs/latest/configuration/responsive.html#configuration-options" /> for more information.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the chart canvas should resize when its container does.")]
    public bool Responsive { get; set; } = true;

    #endregion
}

/// <summary>
/// Namespace: options.layout, the global options for the chart layout is defined in Chart.defaults.layout.
/// <see href="https://www.chartjs.org/docs/latest/configuration/layout.html#layout" /> for more information.
/// </summary>
public class ChartLayout
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets a value indicating whether to apply automatic padding so visible elements are completely drawn.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to apply automatic padding so visible elements are completely drawn.")]
    public bool AutoPadding { get; set; } = true;

    /// <summary>
    /// Gets or sets the padding to add inside the chart, in pixels, on every side. Use <see cref="PaddingSides" /> for a value per side.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("Gets or sets the padding to add inside the chart.")]
    [JsonIgnore]
    public int Padding { get; set; } = 0;

    /// <summary>
    /// Gets or sets the padding per side, in pixels. When set, it is sent instead of <see cref="Padding" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the padding per side, in pixels. When set, it is sent instead of Padding.")]
    [JsonIgnore]
    [ParameterTypeName("ChartPadding?")]
    public ChartPadding? PaddingSides { get; set; }

    /// <summary>
    /// Gets the value sent as <c>padding</c>: <see cref="PaddingSides" /> when set, otherwise <see cref="Padding" />.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("padding")]
    internal object PaddingValue => PaddingSides is null ? Padding : PaddingSides;

    #endregion
}

/// <summary>
/// Namespace: options.interaction, the global interaction configuration is at Chart.defaults.interaction.
/// <see href="https://www.chartjs.org/docs/latest/configuration/interactions.html#interactions" />.
/// </summary>
public class Interaction
{
    #region Fields and Constants

    private InteractionMode mode;

    #endregion

    #region Constructors and Finalizers

    public Interaction()
    {
        Mode = InteractionMode.Nearest;
    }

    #endregion

    #region Methods

    #region Private Methods

    private void SetMode(InteractionMode interactionMode) =>
        ChartInteractionMode = interactionMode switch
        {
            InteractionMode.Dataset => "dataset",
            InteractionMode.Index => "index",
            InteractionMode.Nearest => "nearest",
            InteractionMode.Point => "point",
            InteractionMode.X => "x",
            InteractionMode.Y => "y",
            _ => ""
        };

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the directions used to calculate distances: "x", "y", "xy" or "r".
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses "x" for the index mode and "xy" otherwise).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the directions used to calculate distances: \"x\", \"y\", \"xy\" or \"r\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Axis { get; set; }

    /// <summary>
    /// Gets the Chart.js name of <see cref="Mode" />, which is what is sent to Chart.js.
    /// <para>
    /// Default value is <c>"nearest"</c>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("nearest")]
    [Description("Gets the Chart.js name of Mode, which is what is sent to Chart.js.")]
    [JsonPropertyName("mode")]
    public string ChartInteractionMode { get; private set; } = string.Empty;


    /// <summary>
    /// Gets or sets a value indicating whether points outside the chart area are also considered.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses false).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether points outside the chart area are also considered.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? IncludeInvisible { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the interaction mode only applies when the mouse position intersects an item on the chart.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the interaction mode only applies when the mouse position intersects an item on the chart.")]
    public bool Intersect { get; set; } = true;

    /// <summary>
    /// Gets or sets which elements appear in the interaction. See <see cref="InteractionMode" />.
    /// <para>
    /// Default value is <see cref="InteractionMode.Nearest" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(InteractionMode.Nearest)]
    [Description("Gets or sets which elements appear in the interaction.")]
    [JsonIgnore]
    public InteractionMode Mode
    {
        get => mode;
        set
        {
            mode = value;
            SetMode(value);
        }
    }

    #endregion
}

/// <summary>
/// The cartesian scales (axes) of a chart: the default <c>x</c> and <c>y</c> axes, plus any additional named axes.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/axes/" /> for more information.
/// </para>
/// </summary>
[JsonConverter(typeof(ScalesJsonConverter))]
public class Scales
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets additional axes by ID, for example a second y-axis <c>"y1"</c> on the right.
    /// A dataset uses one through its <c>XAxisID</c> or <c>YAxisID</c>.
    /// <para>
    /// An ID that does not start with <c>x</c> or <c>y</c> must set <see cref="ChartAxes.Axis" />.
    /// The IDs <c>x</c> and <c>y</c> can only be used here when <see cref="X" /> or <see cref="Y" /> is <see langword="null" />;
    /// otherwise <c>InitializeAsync</c> and <c>UpdateAsync</c> throw <see cref="ArgumentException" />.
    /// </para>
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/samples/line/multi-axis.html" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets additional axes by ID, for example a second y-axis \"y1\" on the right.")]
    [ParameterTypeName("Dictionary<string, ChartAxes>?")]
    public Dictionary<string, ChartAxes>? AdditionalAxes { get; set; }

    /// <summary>
    /// Gets or sets the default x-axis, sent as <c>x</c>.
    /// <para>
    /// Default value is a new <see cref="ChartAxes" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the default x-axis, sent as x.")]
    [ParameterTypeName("ChartAxes?")]
    public ChartAxes? X { get; set; } = new();

    /// <summary>
    /// Gets or sets the default y-axis, sent as <c>y</c>.
    /// <para>
    /// Default value is a new <see cref="ChartAxes" /> instance.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the default y-axis, sent as y.")]
    [ParameterTypeName("ChartAxes?")]
    public ChartAxes? Y { get; set; } = new();

    #endregion
}

/// <summary>
/// Writes <see cref="Scales" /> as the Chart.js <c>scales</c> object: <c>x</c> and <c>y</c> when set, then each additional axis under its own ID.
/// Reading is not supported.
/// </summary>
internal sealed class ScalesJsonConverter : JsonConverter<Scales>
{
    #region Methods

    #region Public Methods

    public override Scales Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        throw new NotSupportedException($"{nameof(Scales)} is only written to JSON.");

    public override void Write(Utf8JsonWriter writer, Scales value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        WriteAxis(writer, "x", value.X, options);
        WriteAxis(writer, "y", value.Y, options);

        if (value.AdditionalAxes is not null)
            foreach (var (id, axis) in value.AdditionalAxes)
                WriteAxis(writer, id, axis, options);

        writer.WriteEndObject();
    }

    #endregion

    #region Private Methods

    private static void WriteAxis(Utf8JsonWriter writer, string id, ChartAxes? axis, JsonSerializerOptions options)
    {
        if (axis is null)
            return;

        writer.WritePropertyName(id);
        JsonSerializer.Serialize(writer, axis, axis.GetType(), options);
    }

    #endregion

    #endregion
}

/// <summary>
/// The Chart.js axis (scale) types. Assign one to <see cref="ChartAxes.Type" /> or <see cref="ChartRadialAxes.Type" />.
/// </summary>
public class ChartAxesType
{
    #region Fields and Constants

    /// <summary>
    /// A category axis, for labels such as months.
    /// </summary>
    public static readonly string Category = "category";

    /// <summary>
    /// A numeric linear axis.
    /// </summary>
    public static readonly string Linear = "linear";

    /// <summary>
    /// A numeric logarithmic axis.
    /// </summary>
    public static readonly string Logarithmic = "logarithmic";

    /// <summary>
    /// The radial linear axis used by radar and polar area charts (<c>"radialLinear"</c>).
    /// </summary>
    public static readonly string RadialLinear = "radialLinear";

    /// <summary>
    /// A time axis: data points are spaced by the real time between them. Set its options with <see cref="ChartAxes.Time" />.
    /// The library's own date adapter draws it, so no extra script is needed.
    /// </summary>
    public static readonly string Time = "time";

    /// <summary>
    /// A time series axis: like <see cref="Time" />, but data points are spaced equally. Set its options with <see cref="ChartAxes.Time" />.
    /// The library's own date adapter draws it, so no extra script is needed.
    /// </summary>
    public static readonly string Timeseries = "timeseries";

    #endregion
}

/// <summary>
/// The options of one cartesian axis.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/axes/cartesian/" /> for more information.
/// </para>
/// </summary>
public class ChartAxes
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets which direction the axis runs: <c>"x"</c> or <c>"y"</c>.
    /// Needed only for an additional axis whose ID does not start with <c>x</c> or <c>y</c>; otherwise Chart.js infers it from the ID.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets which direction the axis runs: \"x\" or \"y\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Axis { get; set; }

    /// <summary>
    /// Gets or sets the background color of the axis area.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the background color of the axis area.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the scale starts at zero, if the data does not already include it.
    /// <para>
    /// Default value is <see langword="true" />. This differs from Chart.js, whose linear axes default to <see langword="false" />
    /// except the bar chart value axis.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the scale starts at zero, if the data does not already include it.")]
    public bool BeginAtZero { get; set; } = true;

    /// <summary>
    /// Gets or sets the border that runs perpendicular to the axis.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the border that runs perpendicular to the axis.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartAxesBorder?")]
    public ChartAxesBorder? Border { get; set; }

    /// <summary>
    /// Gets or sets how the axis range is fitted: <c>"data"</c> makes sure the data is fully visible, <c>"ticks"</c> makes sure the ticks are fully visible.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses <c>"ticks"</c>).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets how the axis range is fitted: \"data\" or \"ticks\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Bounds { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the axis is displayed.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js shows the axis).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the axis is displayed.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Display { get; set; }

    /// <summary>
    /// Gets or sets extra space added above the maximum and below the minimum: a number in data units, or a percentage of half the data range such as <c>"5%"</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/axes/cartesian/linear.html#grace" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets extra space added above the maximum and below the minimum: a number in data units, or a percentage of half the data range such as \"5%\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartDimension?")]
    public ChartDimension? Grace { get; set; }

    /// <summary>
    /// Gets or sets the grid lines.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the grid lines.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartAxesGrid?")]
    public ChartAxesGrid? Grid { get; set; }

    /// <summary>
    /// Gets or sets the user-defined maximum, which overrides the maximum from the data.
    /// On a time axis, use <see cref="MaxTime" />; when it is set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the user-defined maximum, which overrides the maximum from the data. On a time axis, use MaxTime.")]
    [JsonIgnore]
    [ParameterTypeName("double?")]
    public double? Max { get; set; }

    /// <summary>
    /// Gets or sets the user-defined maximum date of a time or timeseries axis, which overrides the maximum from the data.
    /// When set, it is sent instead of <see cref="Max" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/samples/scales/time-max-span.html" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the user-defined maximum date of a time or timeseries axis. When set, it is sent instead of Max.")]
    [JsonIgnore]
    [ParameterTypeName("ChartTimeValue?")]
    public ChartTimeValue? MaxTime { get; set; }

    /// <summary>
    /// Gets the value sent as <c>max</c>: <see cref="MaxTime" /> when set, otherwise <see cref="Max" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("max")]
    internal object? MaxValue => (object?)MaxTime ?? Max;

    /// <summary>
    /// Gets or sets the user-defined minimum, which overrides the minimum from the data.
    /// On a time axis, use <see cref="MinTime" />; when it is set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the user-defined minimum, which overrides the minimum from the data. On a time axis, use MinTime.")]
    [JsonIgnore]
    [ParameterTypeName("double?")]
    public double? Min { get; set; }

    /// <summary>
    /// Gets or sets the user-defined minimum date of a time or timeseries axis, which overrides the minimum from the data.
    /// When set, it is sent instead of <see cref="Min" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/samples/scales/time-max-span.html" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the user-defined minimum date of a time or timeseries axis. When set, it is sent instead of Min.")]
    [JsonIgnore]
    [ParameterTypeName("ChartTimeValue?")]
    public ChartTimeValue? MinTime { get; set; }

    /// <summary>
    /// Gets the value sent as <c>min</c>: <see cref="MinTime" /> when set, otherwise <see cref="Min" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("min")]
    internal object? MinValue => (object?)MinTime ?? Min;

    /// <summary>
    /// Gets or sets a value indicating whether extra space is added to both edges and the axis is scaled to fit the chart area,
    /// as bar charts do by default.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether extra space is added to both edges and the axis is scaled to fit the chart area.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Offset { get; set; }

    /// <summary>
    /// Gets or sets where the axis is drawn: <c>"top"</c>, <c>"left"</c>, <c>"bottom"</c>, <c>"right"</c> or <c>"center"</c>.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js puts x at the bottom and y on the left).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets where the axis is drawn: \"top\", \"left\", \"bottom\", \"right\" or \"center\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Position { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the axis runs in reverse.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the axis runs in reverse.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Reverse { get; set; }

    /// <summary>
    /// Gets or sets the stack group of the axis. Axes in the same group on the same position are stacked on top of each other.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/samples/scales/stacked.html" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the stack group of the axis. Axes in the same group on the same position are stacked on top of each other.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Stack { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the data is stacked.
    /// If the value axis (the y-axis of a vertical chart) is stacked, positive and negative values are stacked separately.
    /// <para>
    /// Default value is <see langword="false" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the data is stacked.")]
    public bool Stacked { get; set; }

    /// <summary>
    /// Gets or sets the relative size of a stacked axis in its stack group.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 1).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the relative size of a stacked axis in its stack group.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? StackWeight { get; set; }

    /// <summary>
    /// Gets or sets a value used to calculate the maximum, unless the data is larger.
    /// On a time axis, use <see cref="SuggestedMaxTime" />; when it is set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value used to calculate the maximum, unless the data is larger. On a time axis, use SuggestedMaxTime.")]
    [JsonIgnore]
    [ParameterTypeName("double?")]
    public double? SuggestedMax { get; set; }

    /// <summary>
    /// Gets or sets a date the range of a time or timeseries axis is widened to include, unless the data ends later.
    /// When set, it is sent instead of <see cref="SuggestedMax" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a date the range of a time or timeseries axis is widened to include, unless the data ends later. When set, it is sent instead of SuggestedMax.")]
    [JsonIgnore]
    [ParameterTypeName("ChartTimeValue?")]
    public ChartTimeValue? SuggestedMaxTime { get; set; }

    /// <summary>
    /// Gets the value sent as <c>suggestedMax</c>: <see cref="SuggestedMaxTime" /> when set, otherwise <see cref="SuggestedMax" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("suggestedMax")]
    internal object? SuggestedMaxValue => (object?)SuggestedMaxTime ?? SuggestedMax;

    /// <summary>
    /// Gets or sets a value used to calculate the minimum, unless the data is smaller.
    /// On a time axis, use <see cref="SuggestedMinTime" />; when it is set, it is sent instead of this value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value used to calculate the minimum, unless the data is smaller. On a time axis, use SuggestedMinTime.")]
    [JsonIgnore]
    [ParameterTypeName("double?")]
    public double? SuggestedMin { get; set; }

    /// <summary>
    /// Gets or sets a date the range of a time or timeseries axis is widened to include, unless the data starts earlier.
    /// When set, it is sent instead of <see cref="SuggestedMin" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a date the range of a time or timeseries axis is widened to include, unless the data starts earlier. When set, it is sent instead of SuggestedMin.")]
    [JsonIgnore]
    [ParameterTypeName("ChartTimeValue?")]
    public ChartTimeValue? SuggestedMinTime { get; set; }

    /// <summary>
    /// Gets the value sent as <c>suggestedMin</c>: <see cref="SuggestedMinTime" /> when set, otherwise <see cref="SuggestedMin" />.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonInclude]
    [JsonPropertyName("suggestedMin")]
    internal object? SuggestedMinValue => (object?)SuggestedMinTime ?? SuggestedMin;

    /// <summary>
    /// Gets or sets the tick configuration.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the tick configuration.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartAxesTicks?")]
    public ChartAxesTicks? Ticks { get; set; }

    /// <summary>
    /// Gets or sets the options of a <see cref="ChartAxesType.Time" /> or <see cref="ChartAxesType.Timeseries" /> axis:
    /// units, rounding, first day of the week, and the .NET date formats of the tick labels and tooltips.
    /// The library draws time axes with its own date adapter, so no extra script is needed.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/axes/cartesian/time.html" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the options of a time or timeseries axis: units, rounding, first day of the week, and the .NET date formats of the tick labels and tooltips.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartAxesTime?")]
    public ChartAxesTime? Time { get; set; }

    /// <summary>
    /// Gets or sets the axis title.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the axis title.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartAxesTitle?")]
    public ChartAxesTitle? Title { get; set; }

    /// <summary>
    /// Gets or sets the axis type. See <see cref="ChartAxesType" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.1.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the axis type. See ChartAxesType.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the weight used to sort axes on the same position; a larger weight is drawn further from the chart area.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 0).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the weight used to sort axes on the same position; a larger weight is drawn further from the chart area.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? Weight { get; set; }

    #endregion
}

/// <summary>
/// Define options for the border that run perpendicular to the axis.
/// <see href="https://www.chartjs.org/docs/latest/axes/styling.html" />
/// </summary>
public class ChartAxesBorder
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the color of the border line.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color of the border line.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the length and spacing of dashes on the border line.
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/setLineDash" />
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the length and spacing of dashes on the border line.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("List<int>?")]
    public List<int>? Dash { get; set; }

    /// <summary>
    /// Gets or sets the offset for line dashes.
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineDashOffset" />
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the offset for line dashes.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? DashOffset { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to draw a border at the edge between the axis and the chart area.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to draw a border at the edge between the axis and the chart area.")]
    public bool Display { get; set; } = true;

    /// <summary>
    /// Gets or sets the width of the border line.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the width of the border line.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? Width { get; set; }

    /// <summary>
    /// Gets or sets the z-index of the border layer. Values &lt;= 0 are drawn under datasets, &gt; 0 on top.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the z-index of the border layer. Values <= 0 are drawn under datasets, > 0 on top.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? Z { get; set; }

    #endregion
}

/// <summary>
/// Defines options for the grid lines that run perpendicular to the axis.
/// <see href="https://www.chartjs.org/docs/latest/samples/scale-options/grid.html" />
/// </summary>
public class ChartAxesGrid
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets a value indicating whether grid lines are circular (on radar and polar area charts only).
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether grid lines are circular (on radar and polar area charts only).")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Circular { get; set; }

    /// <summary>
    /// Gets or sets the color of the grid lines.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color of the grid lines.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether grid lines are displayed for this axis.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether grid lines are displayed for this axis.")]
    public bool Display { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to draw lines on the chart area inside the axis lines.
    /// This is useful when there are multiple axes and you need to control which grid lines are drawn.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to draw lines on the chart area inside the axis lines.")]
    public bool DrawOnChartArea { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to draw lines beside the ticks in the axis area beside the chart.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to draw lines beside the ticks in the axis area beside the chart.")]
    public bool DrawTicks { get; set; } = true;

    /// <summary>
    /// Gets or sets the stroke width of grid lines.
    /// <para>
    /// Default value is 1.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(1)]
    [Description("Gets or sets the stroke width of grid lines.")]
    public int LineWidth { get; set; } = 1;

    /// <summary>
    /// Gets or sets a value indicating whether grid lines are shifted to be between labels.
    /// <para>
    /// Default value is <see langword="false" />, sent whenever a grid is set. This differs from Chart.js, which uses <see langword="true" />
    /// on the bar chart category axis.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether grid lines are shifted to be between labels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Offset { get; set; } = false;

    /// <summary>
    /// Gets or sets the length and spacing of the tick mark line.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the length and spacing of the tick mark line.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("List<int>?")]
    public List<int>? TickBorderDash { get; set; }

    /// <summary>
    /// Gets or sets the offset for the line dash of the tick mark.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the offset for the line dash of the tick mark.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? TickBorderDashOffset { get; set; }

    /// <summary>
    /// Gets or sets the color of the tick line. If unset, defaults to the grid line color.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color of the tick line. If unset, defaults to the grid line color.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? TickColor { get; set; }

    /// <summary>
    /// Gets or sets the length in pixels that the grid lines draw into the axis area.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the length in pixels that the grid lines draw into the axis area.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? TickLength { get; set; }

    /// <summary>
    /// Gets or sets the width of the tick mark in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the width of the tick mark in pixels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? TickWidth { get; set; }

    /// <summary>
    /// Gets or sets the z-index of the grid line layer. Values &lt;= 0 are drawn under datasets, &gt; 0 on top.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the z-index of the grid line layer. Values <= 0 are drawn under datasets, > 0 on top.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? Z { get; set; }

    #endregion
}

public enum TicksAlignment
{
    Center, // default
    Start,
    End
}

public enum TitleAlignment
{
    Center, // default
    Start,
    End
}

/// <summary>
/// Chart axes tick styling.
/// <see href="https://www.chartjs.org/docs/latest/axes/styling.html#tick-configuration" />
/// </summary>
public class ChartAxesTicks
{
    #region Fields and Constants

    private TicksAlignment ticksAlignment;

    #endregion

    #region Methods

    #region Private Methods

    private void SetTicksAlignment(TicksAlignment interactionMode) =>
        Alignment = interactionMode switch
        {
            TicksAlignment.Center => "center",
            TicksAlignment.Start => "start",
            TicksAlignment.End => "end",
            _ => null
        };

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets the Chart.js name of <see cref="TicksAlignment" />, which is what is sent to Chart.js as <c>align</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.1.0")]
    [DefaultValue(null)]
    [Description("Gets the Chart.js name of TicksAlignment, which is what is sent to Chart.js as align.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("align")]
    [ParameterTypeName("string?")]
    public string? Alignment { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether Chart.js skips ticks automatically so labels don't overlap.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses <see langword="true" />).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether Chart.js skips ticks automatically so labels don't overlap.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? AutoSkip { get; set; }

    /// <summary>
    /// Gets or sets the padding between ticks on the horizontal axis when <see cref="AutoSkip" /> is on.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 3).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the padding between ticks on the horizontal axis when AutoSkip is on.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? AutoSkipPadding { get; set; }

    /// <summary>
    /// Gets or sets the color of label backdrops.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color of label backdrops.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? BackdropColor { get; set; }

    /// <summary>
    /// Gets or sets the padding of label backdrops.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the padding of label backdrops.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? BackdropPadding { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the text of each tick label, as
    /// <c>(value, index, ticks) =&gt; string | string[]</c>. Return an array for a label of several lines, or <see langword="null" />
    /// to hide the tick and its grid line. On a category axis <c>value</c> is the label index; use <c>this.getLabelForValue(value)</c> for the label.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/axes/labelling.html#creating-custom-tick-formats" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the text of each tick label, as (value, index, ticks) => string | string[].")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? Callback { get; set; }

    /// <summary>
    /// Gets or sets the color of the ticks.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color of the ticks.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the number of ticks to generate on a linear axis. When set, <see cref="StepSize" /> is ignored.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the number of ticks to generate on a linear axis. When set, StepSize is ignored.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? Count { get; set; }

    /// <summary>
    /// Gets or sets how labels align across the axis: <c>"near"</c>, <c>"center"</c> or <c>"far"</c>. Applies only when the labels are not rotated.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses <c>"near"</c>).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets how labels align across the axis: \"near\", \"center\" or \"far\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? CrossAlign { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether tick labels are shown.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether tick labels are shown.")]
    public bool Display { get; set; } = true;

    /// <summary>
    /// Gets or sets the font of the tick labels. A new <see cref="ChartFont" /> sends the library defaults (size 12, bold).
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the font of the tick labels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartFont?")]
    public ChartFont? Font { get; set; }

    /// <summary>
    /// Gets or sets the <c>Intl.NumberFormat</c> options the default tick label formatter of linear, logarithmic and radial linear axes uses,
    /// with <see cref="ChartOptions.Locale" />, for example <c>Format = new() { Style = "currency", Currency = "USD" }</c>.
    /// The default tooltip value uses the same formatter. Other axis types ignore it; time axes use <see cref="ChartAxesTime.DisplayFormats" />.
    /// A <see cref="Callback" /> replaces the default formatter.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/axes/labelling.html" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the Intl.NumberFormat options the default tick label formatter of linear, logarithmic and radial linear axes uses, with Locale. The default tooltip value uses the same formatter. Other axis types ignore it; time axes use Time.DisplayFormats.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartNumberFormat?")]
    public ChartNumberFormat? Format { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the minimum and maximum are shown as ticks even when they are not on a step.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses <see langword="true" />).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the minimum and maximum are shown as ticks even when they are not on a step.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? IncludeBounds { get; set; }

    /// <summary>
    /// Gets or sets the distance in pixels to offset each label from the center of its tick, along the axis.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the distance in pixels to offset each label from the center of its tick, along the axis.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? LabelOffset { get; set; }

    /// <summary>
    /// Gets or sets options for the major tick marks that are generated by the axis.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets options for the major tick marks that are generated by the axis.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartAxesTicksMajor?")]
    public ChartAxesTicksMajor? Major { get; set; }

    /// <summary>
    /// Gets or sets the maximum rotation of labels on the horizontal axis, in degrees.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 50).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the maximum rotation of labels on the horizontal axis, in degrees.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? MaxRotation { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of ticks and grid lines to show.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 11).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the maximum number of ticks and grid lines to show.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? MaxTicksLimit { get; set; }

    /// <summary>
    /// Gets or sets the minimum rotation of labels on the horizontal axis, in degrees.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 0).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the minimum rotation of labels on the horizontal axis, in degrees.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? MinRotation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether tick labels are drawn inside the chart area instead of outside it. Applies to vertical axes.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether tick labels are drawn inside the chart area instead of outside it.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Mirror { get; set; }

    /// <summary>
    /// Gets or sets the offset of the tick labels from the axis.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the offset of the tick labels from the axis.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? Padding { get; set; }

    /// <summary>
    /// Gets or sets the number of decimal places the tick step is rounded to on a linear axis.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the number of decimal places the tick step is rounded to on a linear axis.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? Precision { get; set; }

    /// <summary>
    /// Gets or sets the number of ticks Chart.js samples to measure label sizes, which speeds up large axes.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js measures every tick).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the number of ticks Chart.js samples to measure label sizes, which speeds up large axes.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? SampleSize { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to draw a background behind the tick labels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether to draw a background behind the tick labels.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? ShowLabelBackdrop { get; set; }

    /// <summary>
    /// Gets or sets where the ticks of a time or timeseries axis come from: <c>"auto"</c> picks well-spaced dates,
    /// <c>"data"</c> puts a tick at every data point, and <c>"labels"</c> puts a tick at every label.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses <c>"auto"</c>).
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/axes/cartesian/time.html#ticks-source" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets where the ticks of a time or timeseries axis come from: \"auto\", \"data\" or \"labels\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets a fixed step between ticks on a linear axis.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a fixed step between ticks on a linear axis.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("double?")]
    public double? StepSize { get; set; }

    /// <summary>
    /// Gets or sets the color of the stroke around the text.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the color of the stroke around the text.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? TextStrokeColor { get; set; }

    /// <summary>
    /// Gets or sets the stroke width around the text.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the stroke width around the text.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? TextStrokeWidth { get; set; }

    /// <summary>
    /// Gets or sets the alignment of the tick labels along the axis.
    /// <para>
    /// Default value is <see cref="TicksAlignment.Center" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(TicksAlignment.Center)]
    [Description("Gets or sets the alignment of the tick labels along the axis.")]
    [JsonIgnore]
    public TicksAlignment TicksAlignment
    {
        get => ticksAlignment;
        set
        {
            ticksAlignment = value;
            SetTicksAlignment(value);
        }
    }

    /// <summary>
    /// Gets or sets the z-index of the tick layer. Values &lt;= 0 are drawn under datasets, &gt; 0 on top.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the z-index of the tick layer. Values <= 0 are drawn under datasets, > 0 on top.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? Z { get; set; }

    #endregion
}

/// <summary>
/// Defines options for the major tick marks that are generated by the axis.
/// </summary>
public class ChartAxesTicksMajor
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets a value indicating whether major ticks are generated. A major tick affects auto skipping.
    /// <para>
    /// Default value is <see langword="false" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether major ticks are generated. A major tick affects auto skipping.")]
    public bool Enabled { get; set; } = false;

    #endregion
}

/// <summary>
/// The title of an axis.
/// <see href="https://www.chartjs.org/docs/latest/axes/labelling.html#scale-title-configuration" />
/// </summary>
public class ChartAxesTitle
{
    #region Fields and Constants

    private TitleAlignment titleAlignment;

    #endregion

    #region Methods

    #region Private Methods

    private void SetTitleAlignment(TitleAlignment interactionMode) =>
        Alignment = interactionMode switch
        {
            TitleAlignment.Center => "center", // default
            TitleAlignment.Start => "start",
            TitleAlignment.End => "end",
            _ => null
        };

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets the Chart.js name of <see cref="TitleAlignment" />, which is what is sent to Chart.js as <c>align</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.1.0")]
    [DefaultValue(null)]
    [Description("Gets the Chart.js name of TitleAlignment, which is what is sent to Chart.js as align.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("align")]
    [ParameterTypeName("string?")]
    public string? Alignment { get; private set; }

    /// <summary>
    /// Gets or sets the color of the text.
    /// <para>
    /// Default value is <c>"black"</c>. This differs from Chart.js, which uses <c>Chart.defaults.color</c> (<c>"#666"</c>).
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
    /// Gets or sets the font of the title.
    /// <para>
    /// Default value is a new <see cref="ChartFont" /> instance (size 12, bold).
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the font of the title.")]
    [ParameterTypeName("ChartFont?")]
    public ChartFont? Font { get; set; } = new();

    /// <summary>
    /// Gets or sets the padding around the title: a number for top and bottom, or a <see cref="ChartPadding" /> with <c>Top</c> and <c>Bottom</c>.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses 4 on top and bottom).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the padding around the title.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartPadding?")]
    public ChartPadding? Padding { get; set; }

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

    /// <summary>
    /// Gets or sets the alignment of the title.
    /// <para>
    /// Default value is <see cref="TitleAlignment.Center" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(TitleAlignment.Center)]
    [Description("Gets or sets the alignment of the title.")]
    [JsonIgnore]
    public TitleAlignment TitleAlignment
    {
        get => titleAlignment;
        set
        {
            titleAlignment = value;
            SetTitleAlignment(value);
        }
    }

    #endregion
}

/// <summary>
/// The options of a <see cref="ChartAxesType.Time" /> or <see cref="ChartAxesType.Timeseries" /> axis.
/// Formats are .NET date format strings, such as <c>"dd MMM yyyy"</c> or <c>"t"</c>; month and weekday names follow
/// <see cref="ChartOptions.Locale" />, or the browser's language when it is not set.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/axes/cartesian/time.html#time-axis-specific-options" /> for more information.
/// </para>
/// </summary>
public class ChartAxesTime
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the .NET date format of the tick labels for each time unit, for example <c>Day = "dd MMM"</c>.
    /// A unit left unset uses the library default for that unit.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the .NET date format of the tick labels for each time unit. A unit left unset uses the library default.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartAxesTimeDisplayFormats?")]
    public ChartAxesTimeDisplayFormats? DisplayFormats { get; set; }

    /// <summary>
    /// Gets or sets the first day of the week when the unit is <see cref="ChartTimeUnit.Week" />, for example <see cref="DayOfWeek.Monday" />.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js starts weeks on Sunday).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the first day of the week when the unit is Week, for example DayOfWeek.Monday.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("DayOfWeek?")]
    public DayOfWeek? IsoWeekday { get; set; }

    /// <summary>
    /// Gets or sets the smallest unit Chart.js may pick when it chooses the unit itself.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses <see cref="ChartTimeUnit.Millisecond" />).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the smallest unit Chart.js may pick when it chooses the unit itself.")]
    [JsonConverter(typeof(CamelCaseEnumConverter<ChartTimeUnit>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartTimeUnit?")]
    public ChartTimeUnit? MinUnit { get; set; }

    /// <summary>
    /// Gets or sets the unit every date is rounded down to, for example <see cref="ChartTimeUnit.Hour" /> puts 09:42 at 09:00.
    /// <para>
    /// Default value is <see langword="null" /> (no rounding).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the unit every date is rounded down to, for example Hour puts 09:42 at 09:00.")]
    [JsonConverter(typeof(CamelCaseEnumConverter<ChartTimeUnit>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartTimeUnit?")]
    public ChartTimeUnit? Round { get; set; }

    /// <summary>
    /// Gets or sets the .NET date format of the tooltip title, for example <c>"ddd, dd MMM yyyy HH:mm"</c>.
    /// <para>
    /// Default value is <see langword="null" /> (the library uses <c>"G"</c>, the browser's short date and long time).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the .NET date format of the tooltip title, for example \"ddd, dd MMM yyyy HH:mm\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? TooltipFormat { get; set; }

    /// <summary>
    /// Gets or sets the unit of the ticks, for example <see cref="ChartTimeUnit.Day" /> for one tick a day.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js picks the unit that fits the data).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the unit of the ticks, for example Day for one tick a day.")]
    [JsonConverter(typeof(CamelCaseEnumConverter<ChartTimeUnit>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartTimeUnit?")]
    public ChartTimeUnit? Unit { get; set; }

    #endregion
}

/// <summary>
/// The .NET date format of the tick labels for each time unit, for example <c>Day = "dd MMM"</c> or <c>Hour = "HH:mm"</c>.
/// Both custom formats (<c>"ddd, dd MMM yyyy"</c>) and standard formats (<c>"d"</c>, <c>"t"</c>) are supported.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/axes/cartesian/time.html#display-formats" /> for more information.
/// </para>
/// </summary>
public class ChartAxesTimeDisplayFormats
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the format of day ticks.
    /// <para>
    /// Default value is <see langword="null" /> (the library uses <c>"d MMM"</c>).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the format of day ticks.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Day { get; set; }

    /// <summary>
    /// Gets or sets the format of hour ticks.
    /// <para>
    /// Default value is <see langword="null" /> (the library uses <c>"t"</c>, the browser's short time).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the format of hour ticks.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Hour { get; set; }

    /// <summary>
    /// Gets or sets the format of millisecond ticks.
    /// <para>
    /// Default value is <see langword="null" /> (the library uses <c>"HH:mm:ss.fff"</c>).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the format of millisecond ticks.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Millisecond { get; set; }

    /// <summary>
    /// Gets or sets the format of minute ticks.
    /// <para>
    /// Default value is <see langword="null" /> (the library uses <c>"t"</c>, the browser's short time).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the format of minute ticks.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Minute { get; set; }

    /// <summary>
    /// Gets or sets the format of month ticks.
    /// <para>
    /// Default value is <see langword="null" /> (the library uses <c>"MMM yyyy"</c>).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the format of month ticks.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Month { get; set; }

    /// <summary>
    /// Gets or sets the format of quarter ticks.
    /// <para>
    /// Default value is <see langword="null" /> (the library uses <c>"MMM yyyy"</c>).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the format of quarter ticks.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Quarter { get; set; }

    /// <summary>
    /// Gets or sets the format of second ticks.
    /// <para>
    /// Default value is <see langword="null" /> (the library uses <c>"T"</c>, the browser's long time).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the format of second ticks.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Second { get; set; }

    /// <summary>
    /// Gets or sets the format of week ticks.
    /// <para>
    /// Default value is <see langword="null" /> (the library uses <c>"d MMM"</c>).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the format of week ticks.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Week { get; set; }

    /// <summary>
    /// Gets or sets the format of year ticks.
    /// <para>
    /// Default value is <see langword="null" /> (the library uses <c>"yyyy"</c>).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the format of year ticks.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Year { get; set; }

    #endregion
}

/// <summary>
/// A font.
/// <see href="https://www.chartjs.org/docs/latest/general/fonts.html" />
/// </summary>
public class ChartFont
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the font family, following CSS <c>font-family</c>.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses <c>'Helvetica Neue', 'Helvetica', 'Arial', sans-serif</c>).
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
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/CSS/line-height" />
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
    /// Gets or sets the font style, following CSS <c>font-style</c> (normal, italic, oblique, initial, inherit).
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
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight" />
    /// <para>
    /// Default value is <c>"bold"</c>. This differs from Chart.js, which uses a normal weight except for the chart title.
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

/// <summary>
/// Chart.js animates charts out of the box.
/// A number of options are provided to configure how the animation looks and how long it takes.
/// <see href="https://www.chartjs.org/docs/latest/configuration/animations.html#animations" />
/// </summary>
public class ChartAnimation
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets a value indicating whether arcs animate their rotation on doughnut, pie and polar area charts.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses true).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether arcs animate their rotation on doughnut, pie and polar area charts.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? AnimateRotate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether doughnut, pie and polar area charts grow from the center.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses false for doughnut and pie, true for polar area).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether doughnut, pie and polar area charts grow from the center.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? AnimateScale { get; set; }

    /// <summary>
    /// Gets or sets the delay before starting the animations, in milliseconds.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.2.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the delay before starting the animations, in milliseconds.")]
    [ParameterTypeName("double?")]
    public double? Delay { get; set; } = null;

    /// <summary>
    /// Gets or sets the number of milliseconds an animation takes.
    /// <para>
    /// Default value is 1000.
    /// </para>
    /// </summary>
    [AddedVersion("1.2.0")]
    [DefaultValue(1000)]
    [Description("Gets or sets the number of milliseconds an animation takes.")]
    public double Duration { get; set; } = 1000;

    /// <summary>
    /// Gets or sets the easing function to use.
    /// <para>
    /// Default value is "easeOutQuart".
    /// </para>
    /// <para>
    /// Supported values are: 'linear', 'easeInQuad', 'easeOutQuad', 'easeInOutQuad', 'easeInCubic', 'easeOutCubic', 'easeInOutCubic', 'easeInQuart', 'easeOutQuart', 'easeInOutQuart', 'easeInQuint', 'easeOutQuint', 'easeInOutQuint', 'easeInSine', 'easeOutSine', 'easeInOutSine', 'easeInExpo', 'easeOutExpo', 'easeInOutExpo', 'easeInCirc', 'easeOutCirc', 'easeInOutCirc', 'easeInElastic', 'easeOutElastic', 'easeInOutElastic', 'easeInBack', 'easeOutBack', 'easeInOutBack', 'easeInBounce', 'easeOutBounce', and 'easeInOutBounce'.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/animations.html#easing" />
    /// </summary>
    [AddedVersion("1.2.0")]
    [DefaultValue("easeOutQuart")]
    [Description("Gets or sets the easing function to use.")]
    public string Easing { get; set; } = "easeOutQuart";

    /// <summary>
    /// Gets or sets a value indicating whether the animations loop endlessly.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.2.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the animations loop endlessly.")]
    [ParameterTypeName("bool?")]
    public bool? Loop { get; set; } = null;

    /// <summary>
    /// Gets or sets the registered JavaScript function Chart.js calls when an animation completes, as <c>(animation) =&gt; void</c>,
    /// where <c>animation</c> is <c>{ chart, currentStep, initial, numSteps }</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/animations.html#animation-callbacks" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function Chart.js calls when an animation completes, as (animation) => void.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? OnComplete { get; set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function Chart.js calls on each step of an animation, as <c>(animation) =&gt; void</c>,
    /// where <c>animation</c> is <c>{ chart, currentStep, initial, numSteps }</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/animations.html#animation-callbacks" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function Chart.js calls on each step of an animation, as (animation) => void.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? OnProgress { get; set; }

    #endregion
}
