namespace BlazorExpress.ChartJS;

/// <summary>
/// Checks chart options for caller mistakes before they are sent to Chart.js.
/// </summary>
internal static class ChartOptionsValidator
{
    #region Methods

    #region Internal Methods

    /// <summary>
    /// Throws when an additional axis has an empty ID, or uses the ID of a dedicated axis property that is set
    /// (<c>x</c> with <see cref="Scales.X" />, <c>y</c> with <see cref="Scales.Y" />). Chart.js keys axes by ID, so the browser would
    /// silently keep only one of them.
    /// </summary>
    /// <exception cref="ArgumentException">An additional axis ID is empty or already used.</exception>
    internal static void ThrowIfInvalid(IChartOptions? chartOptions)
    {
        var scales = chartOptions switch
        {
            BarChartOptions options => options.Scales,
            BubbleChartOptions options => options.Scales,
            LineChartOptions options => options.Scales,
            ScatterChartOptions options => options.Scales,
            _ => null
        };

        if (scales?.AdditionalAxes is null)
            return;

        foreach (var id in scales.AdditionalAxes.Keys)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("An additional axis ID cannot be empty.", nameof(chartOptions));

            if (id == "x" && scales.X is not null)
                throw new ArgumentException("Axis ID 'x' is already set by Scales.X.", nameof(chartOptions));

            if (id == "y" && scales.Y is not null)
                throw new ArgumentException("Axis ID 'y' is already set by Scales.Y.", nameof(chartOptions));
        }
    }

    #endregion

    #endregion
}
