namespace BlazorExpress.ChartJS;

/// <summary>
/// A bubble at a date on a time or timeseries x-axis, for <see cref="BubbleChartDataset.TimeData" />,
/// for example <c>new BubbleChartTimePoint(new DateTime(2026, 1, 5, 9, 0, 0), 21.5, 6)</c>.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/charts/bubble.html#data-structure" /> for more information.
/// </para>
/// </summary>
/// <param name="X">The date: a <see cref="DateOnly" />, <see cref="DateTime" /> or <see cref="DateTimeOffset" />.</param>
/// <param name="Y">The value.</param>
/// <param name="R">The radius of the bubble in pixels.</param>
public record BubbleChartTimePoint(
    [property: AddedVersion("2.0.0")]
    [property: DefaultValue(null)]
    [property: Description("Gets the date: a DateOnly, DateTime or DateTimeOffset.")]
    [property: ParameterTypeName("ChartTimeValue")]
    ChartTimeValue X,
    [property: AddedVersion("2.0.0")]
    [property: DefaultValue(0)]
    [property: Description("Gets the value.")]
    [property: ParameterTypeName("double")]
    double Y,
    [property: AddedVersion("2.0.0")]
    [property: DefaultValue(0)]
    [property: Description("Gets the radius of the bubble in pixels.")]
    [property: ParameterTypeName("double")]
    double R);
