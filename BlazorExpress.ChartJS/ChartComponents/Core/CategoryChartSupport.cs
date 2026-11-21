namespace BlazorExpress.ChartJS;

/// <summary>
/// Shared steps of the single-value <c>AddDataAsync</c> on doughnut, pie, polar area and radar charts. They follow the
/// bar and line charts and Chart.js's update pattern: <c>dataLabel</c> is a new category label, and the value goes to the
/// dataset named by the data's <see cref="ChartDatasetData.DatasetLabel" /> (work item 26112101).
/// </summary>
internal static class CategoryChartSupport
{
    /// <summary>
    /// Validates the arguments before anything changes (CS-005), with the same checks as <c>BarChart.AddDataAsync</c>.
    /// </summary>
    internal static void Validate(ChartData chartData, string dataLabel, IChartDatasetData data)
    {
        if (chartData is null)
            throw new ArgumentNullException(nameof(chartData));

        if (chartData.Datasets is null)
            throw new ArgumentNullException(nameof(chartData.Datasets));

        if (chartData.Labels is null)
            throw new ArgumentNullException(nameof(chartData.Labels));

        if (dataLabel is null)
            throw new ArgumentNullException(nameof(dataLabel));

        if (string.IsNullOrWhiteSpace(dataLabel))
            throw new Exception($"{nameof(dataLabel)} cannot be empty.");

        if (data is null)
            throw new ArgumentNullException(nameof(data));
    }

    /// <summary>Adds the category label once, as the shared JavaScript <c>addDatasetData</c> does.</summary>
    internal static void AddLabel(ChartData chartData, string dataLabel)
    {
        if (!chartData.Labels!.Contains(dataLabel))
            chartData.Labels.Add(dataLabel);
    }

    /// <summary>Appends a slice colour when the data has one and the dataset keeps one colour per slice.</summary>
    internal static void AppendColor(List<string>? colors, string? color)
    {
        if (colors is not null && !string.IsNullOrWhiteSpace(color))
            colors.Add(color);
    }
}
