namespace BlazorExpress.ChartJS;

/// <summary>
/// Writes <see cref="ChartAnimation.Disabled" /> as <c>false</c>, which turns Chart.js animation off, and every other
/// <see cref="ChartAnimation" /> as its usual object. It is applied to the <c>Animation</c> properties, not to the type,
/// so writing the usual object does not call this converter again.
/// </summary>
internal sealed class ChartAnimationJsonConverter : JsonConverter<ChartAnimation>
{
    #region Methods

    #region Public Methods

    public override ChartAnimation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        throw new NotSupportedException($"{nameof(ChartAnimation)} is only written to JSON.");

    public override void Write(Utf8JsonWriter writer, ChartAnimation value, JsonSerializerOptions options)
    {
        if (value.IsDisabled)
            writer.WriteBooleanValue(false);
        else
            JsonSerializer.Serialize(writer, value, options);
    }

    #endregion

    #endregion
}
