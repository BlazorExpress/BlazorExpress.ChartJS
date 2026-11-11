namespace BlazorExpress.ChartJS;

/// <summary>
/// A Chart.js value that is either a number or a string, such as <c>"50%"</c> or <c>"auto"</c>.
/// Assign a number or a string directly, for example <c>Cutout = "60%"</c> or <c>Cutout = 80</c>.
/// <para>
/// Used by options such as <c>cutout</c>, <c>radius</c>, <c>grace</c> and <c>inflateAmount</c>.
/// </para>
/// </summary>
[JsonConverter(typeof(ChartDimensionJsonConverter))]
public sealed class ChartDimension
{
    #region Constructors and Finalizers

    private ChartDimension(double? number, string? text)
    {
        Number = number;
        Text = text;
    }

    #endregion

    #region Methods

    #region Public Methods

    /// <summary>
    /// Creates a value from a number.
    /// </summary>
    /// <param name="number">The number, for example pixels or data units.</param>
    [AddedVersion("2.0.0")]
    [Description("Creates a value from a number.")]
    public static implicit operator ChartDimension(double number) => new(number, null);

    /// <summary>
    /// Creates a value from a string, such as <c>"50%"</c> or <c>"auto"</c>. A <see langword="null" /> string gives <see langword="null" />.
    /// </summary>
    /// <param name="text">The string value.</param>
    [AddedVersion("2.0.0")]
    [Description("Creates a value from a string, such as \"50%\" or \"auto\".")]
    [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(text))]
    public static implicit operator ChartDimension?(string? text) => text is null ? null : new(null, text);

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets the number, when the value was created from a number.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets the number, when the value was created from a number.")]
    [ParameterTypeName("double?")]
    public double? Number { get; }

    /// <summary>
    /// Gets the string, when the value was created from a string.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets the string, when the value was created from a string.")]
    [ParameterTypeName("string?")]
    public string? Text { get; }

    #endregion
}

/// <summary>
/// Writes a <see cref="ChartDimension" /> as a JSON number or string. Reading is not supported.
/// </summary>
internal sealed class ChartDimensionJsonConverter : JsonConverter<ChartDimension>
{
    #region Methods

    #region Public Methods

    public override ChartDimension Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        throw new NotSupportedException($"{nameof(ChartDimension)} is only written to JSON.");

    public override void Write(Utf8JsonWriter writer, ChartDimension value, JsonSerializerOptions options)
    {
        if (value.Number is double number)
            writer.WriteNumberValue(number);
        else
            writer.WriteStringValue(value.Text);
    }

    #endregion

    #endregion
}
