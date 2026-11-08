namespace BlazorExpress.ChartJS;

/// <summary>
/// A value at a date on a time or timeseries x-axis, for the <c>TimeData</c> of a line, bar or scatter dataset,
/// for example <c>new ChartTimePoint(new DateTime(2026, 1, 5, 9, 0, 0), 21.5)</c>.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/general/data-structures.html#object" /> for more information.
/// </para>
/// </summary>
/// <param name="X">The date: a <see cref="DateOnly" />, <see cref="DateTime" /> or <see cref="DateTimeOffset" />.</param>
/// <param name="Y">The value, or <see langword="null" /> for a gap.</param>
public record ChartTimePoint(
    [property: AddedVersion("1.3.0")]
    [property: DefaultValue(null)]
    [property: Description("Gets the date: a DateOnly, DateTime or DateTimeOffset.")]
    [property: ParameterTypeName("ChartTimeValue")]
    ChartTimeValue X,
    [property: AddedVersion("1.3.0")]
    [property: DefaultValue(null)]
    [property: Description("Gets the value, or null for a gap.")]
    [property: ParameterTypeName("double?")]
    double? Y);
