namespace BlazorExpress.ChartJS;

/// <summary>
/// Chart.js padding: one number for every side, or a value per side.
/// Assign a number for the same padding on every side (<c>Padding = 10</c>), or set the sides (<c>Padding = new() { Top = 10, Bottom = 4 }</c>).
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/general/padding.html" /> for more information.
/// </para>
/// </summary>
[JsonConverter(typeof(ChartPaddingJsonConverter))]
public sealed class ChartPadding
{
    #region Methods

    #region Public Methods

    /// <summary>
    /// Creates padding with the same value on every side.
    /// </summary>
    /// <param name="value">The padding in pixels.</param>
    [AddedVersion("1.3.0")]
    [Description("Creates padding with the same value on every side.")]
    public static implicit operator ChartPadding(double value) => new() { Bottom = value, Left = value, Right = value, Top = value };

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the bottom padding in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the bottom padding in pixels.")]
    [ParameterTypeName("double?")]
    public double? Bottom { get; set; }

    /// <summary>
    /// Gets or sets the left padding in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the left padding in pixels.")]
    [ParameterTypeName("double?")]
    public double? Left { get; set; }

    /// <summary>
    /// Gets or sets the right padding in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the right padding in pixels.")]
    [ParameterTypeName("double?")]
    public double? Right { get; set; }

    /// <summary>
    /// Gets or sets the top padding in pixels.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the top padding in pixels.")]
    [ParameterTypeName("double?")]
    public double? Top { get; set; }

    #endregion
}

/// <summary>
/// Writes a <see cref="ChartPadding" /> as one number when every side has the same value, otherwise as an object of the sides that are set.
/// Reading is not supported.
/// </summary>
internal sealed class ChartPaddingJsonConverter : JsonConverter<ChartPadding>
{
    #region Methods

    #region Public Methods

    public override ChartPadding Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        throw new NotSupportedException($"{nameof(ChartPadding)} is only written to JSON.");

    public override void Write(Utf8JsonWriter writer, ChartPadding value, JsonSerializerOptions options)
    {
        if (value.Top is double top && value.Right == top && value.Bottom == top && value.Left == top)
        {
            writer.WriteNumberValue(top);
            return;
        }

        writer.WriteStartObject();
        WriteSide(writer, "top", value.Top);
        WriteSide(writer, "right", value.Right);
        WriteSide(writer, "bottom", value.Bottom);
        WriteSide(writer, "left", value.Left);
        writer.WriteEndObject();
    }

    #endregion

    #region Private Methods

    private static void WriteSide(Utf8JsonWriter writer, string name, double? side)
    {
        if (side is double value)
            writer.WriteNumber(name, value);
    }

    #endregion

    #endregion
}
