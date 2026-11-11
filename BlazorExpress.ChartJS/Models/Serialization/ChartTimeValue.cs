using System.Globalization;

namespace BlazorExpress.ChartJS;

/// <summary>
/// A date or time for a time axis: assign a <see cref="DateOnly" />, <see cref="DateTime" /> or <see cref="DateTimeOffset" /> directly,
/// for example <c>TimeLabels = [new DateOnly(2026, 1, 5)]</c> or <c>MinTime = DateTime.UtcNow</c>.
/// <para>
/// <see cref="DateOnly" /> and a <see cref="DateTime" /> whose <see cref="DateTime.Kind" /> is <see cref="DateTimeKind.Unspecified" /> are wall-clock values:
/// they show the same date and clock time in every browser time zone (for example 09:00 is 09:00 in Tokyo and in New York).
/// A <see cref="DateTimeKind.Utc" /> or <see cref="DateTimeKind.Local" /> <see cref="DateTime" /> and every <see cref="DateTimeOffset" /> are exact instants,
/// shown in the browser's local time (for example 09:00 UTC shows 04:00 in New York). A <see cref="DateTimeKind.Local" /> value is converted
/// with the time zone of the .NET process, which in Blazor Server is the server's.
/// </para>
/// </summary>
[JsonConverter(typeof(ChartTimeValueJsonConverter))]
public sealed class ChartTimeValue : IEquatable<ChartTimeValue>
{
    #region Fields and Constants

    private const string DateFormat = "yyyy'-'MM'-'dd";

    private const string InstantFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";

    private const string WallClockFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff";

    private readonly string value;

    #endregion

    #region Constructors and Finalizers

    private ChartTimeValue(string value) => this.value = value;

    #endregion

    #region Methods

    #region Public Methods

    /// <summary>
    /// Determines whether two values are the same date or time.
    /// </summary>
    /// <param name="left">The first value.</param>
    /// <param name="right">The second value.</param>
    [AddedVersion("2.0.0")]
    [Description("Determines whether two values are the same date or time.")]
    public static bool operator ==(ChartTimeValue? left, ChartTimeValue? right) => Equals(left, right);

    /// <summary>
    /// Creates a wall-clock date, shown as the same calendar date in every browser time zone.
    /// </summary>
    /// <param name="date">The date.</param>
    [AddedVersion("2.0.0")]
    [Description("Creates a wall-clock date, shown as the same calendar date in every browser time zone.")]
    public static implicit operator ChartTimeValue(DateOnly date) => new(date.ToString(DateFormat, CultureInfo.InvariantCulture));

    /// <summary>
    /// Creates a wall-clock time from an <see cref="DateTimeKind.Unspecified" /> value, or an exact instant from a
    /// <see cref="DateTimeKind.Utc" /> or <see cref="DateTimeKind.Local" /> value.
    /// </summary>
    /// <param name="dateTime">The date and time.</param>
    [AddedVersion("2.0.0")]
    [Description("Creates a wall-clock time from an Unspecified value, or an exact instant from a Utc or Local value.")]
    public static implicit operator ChartTimeValue(DateTime dateTime) =>
        dateTime.Kind == DateTimeKind.Unspecified
            ? new(dateTime.ToString(WallClockFormat, CultureInfo.InvariantCulture))
            : new(dateTime.ToUniversalTime().ToString(InstantFormat, CultureInfo.InvariantCulture));

    /// <summary>
    /// Creates an exact instant, shown in the browser's local time.
    /// </summary>
    /// <param name="dateTimeOffset">The date, time and offset.</param>
    [AddedVersion("2.0.0")]
    [Description("Creates an exact instant, shown in the browser's local time.")]
    public static implicit operator ChartTimeValue(DateTimeOffset dateTimeOffset) =>
        new(dateTimeOffset.UtcDateTime.ToString(InstantFormat, CultureInfo.InvariantCulture));

    /// <summary>
    /// Determines whether two values are different dates or times.
    /// </summary>
    /// <param name="left">The first value.</param>
    /// <param name="right">The second value.</param>
    [AddedVersion("2.0.0")]
    [Description("Determines whether two values are different dates or times.")]
    public static bool operator !=(ChartTimeValue? left, ChartTimeValue? right) => !Equals(left, right);

    /// <inheritdoc />
    public bool Equals(ChartTimeValue? other) => other is not null && string.Equals(value, other.value, StringComparison.Ordinal);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is ChartTimeValue other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(value);

    /// <summary>
    /// Returns the value as it is sent to the chart: an ISO 8601 string, without an offset for a wall-clock value
    /// (<c>2026-01-05</c>, <c>2026-01-05T09:00:00.000</c>) and in UTC with <c>Z</c> for an instant (<c>2026-01-05T09:00:00.000Z</c>).
    /// </summary>
    /// <returns>The ISO 8601 string.</returns>
    public override string ToString() => value;

    #endregion

    #endregion
}

/// <summary>
/// Writes a <see cref="ChartTimeValue" /> as its ISO 8601 string. Reading is not supported.
/// </summary>
internal sealed class ChartTimeValueJsonConverter : JsonConverter<ChartTimeValue>
{
    #region Methods

    #region Public Methods

    public override ChartTimeValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        throw new NotSupportedException($"{nameof(ChartTimeValue)} is only written to JSON.");

    public override void Write(Utf8JsonWriter writer, ChartTimeValue value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());

    #endregion

    #endregion
}
