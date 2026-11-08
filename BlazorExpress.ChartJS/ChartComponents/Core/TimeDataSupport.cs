namespace BlazorExpress.ChartJS;

/// <summary>
/// Adds date-typed data to <see cref="ChartData" /> for the add-data methods (rule "data forms and add-data methods"):
/// a date label with one value per dataset (line and bar), or a time point appended to one dataset's <c>TimeData</c>
/// (line, bar, scatter and bubble). Every check runs before anything is changed.
/// </summary>
internal static class TimeDataSupport
{
    #region Methods

    #region Internal Methods

    /// <summary>
    /// Returns <see langword="true" /> when <paramref name="data" /> carries a time point rather than a plain value.
    /// </summary>
    internal static bool IsTimePoint(IChartDatasetData? data) =>
        data is ChartDatasetData { Data: ChartTimePoint or BubbleChartTimePoint };

    /// <summary>
    /// Appends the time point in <paramref name="data" /> to the <c>TimeData</c> of the dataset named by its label.
    /// </summary>
    /// <param name="chartData">The chart data.</param>
    /// <param name="dataLabel">The label of the dataset; it must equal the label in <paramref name="data" />.</param>
    /// <param name="data">The time point.</param>
    /// <param name="isSupportedDataset">Whether a dataset belongs to the calling chart type.</param>
    /// <returns>The label of the dataset and the point, for the browser.</returns>
    /// <exception cref="ArgumentException">The labels differ, no dataset of the chart has that label, or the dataset already holds plain values.</exception>
    internal static (string DatasetLabel, object Point) AppendTimePoint(ChartData chartData, string dataLabel, IChartDatasetData data, Func<IChartDataset, bool> isSupportedDataset)
    {
        var chartDatasetData = (ChartDatasetData)data;

        if (!string.Equals(dataLabel, chartDatasetData.DatasetLabel, StringComparison.Ordinal))
            throw new ArgumentException($"A time point is added to the dataset named by dataLabel, which must equal its DatasetLabel ('{dataLabel}' and '{chartDatasetData.DatasetLabel}' differ).", nameof(dataLabel));

        var dataset = chartData.Datasets?.Where(isSupportedDataset).FirstOrDefault(d => GetLabel(d) == dataLabel)
            ?? throw new ArgumentException($"The chart has no dataset labelled '{dataLabel}' for this time point.", nameof(data));

        switch (dataset, chartDatasetData.Data)
        {
            case (LineChartDataset line, ChartTimePoint point):
                EnsureNoPlainValues(dataLabel, line.Data?.Count > 0);
                (line.TimeData ??= new List<ChartTimePoint>()).Add(point);
                return (dataLabel, point);
            case (BarChartDataset bar, ChartTimePoint point):
                EnsureNoPlainValues(dataLabel, bar.Data?.Count > 0 || bar.FloatingData?.Count > 0);
                (bar.TimeData ??= new List<ChartTimePoint>()).Add(point);
                return (dataLabel, point);
            case (ScatterChartDataset scatter, ChartTimePoint point):
                EnsureNoPlainValues(dataLabel, scatter.Data?.Count > 0);
                (scatter.TimeData ??= new List<ChartTimePoint>()).Add(point);
                return (dataLabel, point);
            case (BubbleChartDataset bubble, BubbleChartTimePoint point):
                EnsureNoPlainValues(dataLabel, bubble.Data?.Count > 0);
                (bubble.TimeData ??= new List<BubbleChartTimePoint>()).Add(point);
                return (dataLabel, point);
            default:
                throw new ArgumentException($"The dataset labelled '{dataLabel}' does not take this kind of time point.", nameof(data));
        }
    }

    /// <summary>
    /// Validates a date label added with one value per dataset, before anything is changed.
    /// </summary>
    /// <exception cref="ArgumentNullException"><paramref name="chartData" />, its datasets, <paramref name="dataLabel" /> or <paramref name="data" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException"><see cref="ChartData.TimeLabels" /> is <see langword="null" />.</exception>
    internal static void ValidateTimeLabel(ChartData chartData, ChartTimeValue dataLabel, object data)
    {
        if (chartData is null)
            throw new ArgumentNullException(nameof(chartData));

        if (chartData.Datasets is null)
            throw new ArgumentNullException(nameof(chartData.Datasets));

        if (dataLabel is null)
            throw new ArgumentNullException(nameof(dataLabel));

        if (data is null)
            throw new ArgumentNullException(nameof(data));

        if (chartData.TimeLabels is null)
            throw new ArgumentException("chartData.TimeLabels must not be null when a date label is added.", nameof(chartData));
    }

    #endregion

    #region Private Methods

    private static void EnsureNoPlainValues(string dataLabel, bool hasPlainValues)
    {
        if (hasPlainValues)
            throw new ArgumentException($"The dataset '{dataLabel}' uses Data; time points need TimeData.", nameof(dataLabel));
    }

    private static string? GetLabel(IChartDataset dataset) =>
        dataset switch
        {
            BarChartDataset bar => bar.Label,
            BubbleChartDataset bubble => bubble.Label,
            LineChartDataset line => line.Label,
            ScatterChartDataset scatter => scatter.Label,
            _ => null,
        };

    #endregion

    #endregion
}
