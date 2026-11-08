namespace BlazorExpress.ChartJS;

/// <summary>
/// A unit of a time or timeseries axis. See <see cref="ChartAxesTime" />.
/// <see href="https://www.chartjs.org/docs/latest/axes/cartesian/time.html#time-units" />
/// </summary>
public enum ChartTimeUnit
{
    /// <summary>
    /// Milliseconds.
    /// </summary>
    Millisecond,

    /// <summary>
    /// Seconds.
    /// </summary>
    Second,

    /// <summary>
    /// Minutes.
    /// </summary>
    Minute,

    /// <summary>
    /// Hours.
    /// </summary>
    Hour,

    /// <summary>
    /// Days.
    /// </summary>
    Day,

    /// <summary>
    /// Weeks. <see cref="ChartAxesTime.IsoWeekday" /> sets the first day of the week.
    /// </summary>
    Week,

    /// <summary>
    /// Months.
    /// </summary>
    Month,

    /// <summary>
    /// Quarters of a year.
    /// </summary>
    Quarter,

    /// <summary>
    /// Years.
    /// </summary>
    Year
}
