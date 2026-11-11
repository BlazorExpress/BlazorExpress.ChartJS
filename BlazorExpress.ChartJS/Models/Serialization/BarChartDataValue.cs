namespace BlazorExpress.ChartJS;

/// <summary>
/// One bar value: a number, drawn from the base, or a <c>[start, end]</c> range, drawn as a floating bar.
/// Assign a number or a tuple directly, for example <c>FloatingData = [5, (2, 8), null]</c>.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/samples/bar/floating.html" /> for more information.
/// </para>
/// </summary>
[JsonConverter(typeof(BarChartDataValueJsonConverter))]
public sealed class BarChartDataValue
{
    #region Constructors and Finalizers

    private BarChartDataValue(double? value, double? start, double? end)
    {
        Value = value;
        Start = start;
        End = end;
    }

    #endregion

    #region Methods

    #region Public Methods

    /// <summary>
    /// Creates a plain bar value, drawn from the base.
    /// </summary>
    /// <param name="value">The value.</param>
    [AddedVersion("2.0.0")]
    [Description("Creates a plain bar value, drawn from the base.")]
    public static implicit operator BarChartDataValue(double value) => new(value, null, null);

    /// <summary>
    /// Creates a floating bar from <c>Start</c> to <c>End</c>.
    /// </summary>
    /// <param name="range">The start and end of the bar.</param>
    [AddedVersion("2.0.0")]
    [Description("Creates a floating bar from Start to End.")]
    public static implicit operator BarChartDataValue((double Start, double End) range) => Range(range.Start, range.End);

    /// <summary>
    /// Creates a floating bar from <paramref name="start" /> to <paramref name="end" />.
    /// </summary>
    /// <param name="start">The value where the bar starts.</param>
    /// <param name="end">The value where the bar ends.</param>
    /// <returns>The floating bar value.</returns>
    [AddedVersion("2.0.0")]
    [Description("Creates a floating bar from start to end.")]
    public static BarChartDataValue Range(double start, double end) => new(null, start, end);

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets the end of a floating bar, or <see langword="null" /> for a plain value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets the end of a floating bar, or null for a plain value.")]
    [ParameterTypeName("double?")]
    public double? End { get; }

    /// <summary>
    /// Gets a value indicating whether this is a floating bar.
    /// <para>
    /// Default value is <see langword="false" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(false)]
    [Description("Gets a value indicating whether this is a floating bar.")]
    public bool IsRange => Start.HasValue;

    /// <summary>
    /// Gets the start of a floating bar, or <see langword="null" /> for a plain value.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets the start of a floating bar, or null for a plain value.")]
    [ParameterTypeName("double?")]
    public double? Start { get; }

    /// <summary>
    /// Gets the plain value, or <see langword="null" /> for a floating bar.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets the plain value, or null for a floating bar.")]
    [ParameterTypeName("double?")]
    public double? Value { get; }

    #endregion
}

/// <summary>
/// Writes a <see cref="BarChartDataValue" /> as a number or a <c>[start, end]</c> array. Reading is not supported.
/// </summary>
internal sealed class BarChartDataValueJsonConverter : JsonConverter<BarChartDataValue>
{
    #region Methods

    #region Public Methods

    public override BarChartDataValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        throw new NotSupportedException($"{nameof(BarChartDataValue)} is only written to JSON.");

    public override void Write(Utf8JsonWriter writer, BarChartDataValue value, JsonSerializerOptions options)
    {
        if (value.Start is double start && value.End is double end)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(start);
            writer.WriteNumberValue(end);
            writer.WriteEndArray();
        }
        else if (value.Value is double number)
            writer.WriteNumberValue(number);
        else
            writer.WriteNullValue();
    }

    #endregion

    #endregion
}
