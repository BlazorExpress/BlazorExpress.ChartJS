namespace BlazorExpress.ChartJS;

internal static class BarLineChartSupport
{
    internal static List<object> GetSupportedDatasets(ChartData chartData)
    {
        var datasets = new List<object>();

        if (chartData?.Datasets?.Any() ?? false)
            foreach (var dataset in chartData.Datasets)
                if (dataset is BarChartDataset barChartDataset)
                    datasets.Add(barChartDataset);
                else if (dataset is LineChartDataset lineChartDataset)
                    datasets.Add(lineChartDataset);

        return datasets;
    }

    internal static List<ChartDatasetData> GetSupportedDatasetData(IEnumerable<IChartDatasetData> data) =>
        data.Select(GetSupportedDatasetData)
            .Where(x => x is not null)
            .Cast<ChartDatasetData>()
            .ToList();

    internal static ChartDatasetData? GetSupportedDatasetData(IChartDatasetData data) =>
        data switch
        {
            BarChartDatasetData barChartDatasetData => barChartDatasetData,
            LineChartDatasetData lineChartDatasetData => lineChartDatasetData,
            _ => null,
        };

    /// <summary>
    /// Returns <see langword="false" /> for a <c>[start, end]</c> range whose label has no bar dataset, so it is sent neither to the chart data nor to the browser.
    /// </summary>
    internal static bool CanAppend(IEnumerable<IChartDataset> datasets, ChartDatasetData chartDatasetData) =>
        !TryGetRange(chartDatasetData, out _)
        || datasets.Any(dataset => dataset is BarChartDataset bar && bar.Label == chartDatasetData.DatasetLabel);

    internal static bool IsSupportedDataset(IChartDataset dataset) =>
        dataset is BarChartDataset or LineChartDataset;

    internal static void AppendDataPoint(IChartDataset dataset, ChartDatasetData chartDatasetData)
    {
        if (dataset is BarChartDataset bar && bar.Label == chartDatasetData.DatasetLabel && TryGetRange(chartDatasetData, out var range))
        {
            UseFloatingData(bar).Add(range);
            return;
        }

        if (!TryGetDataValue(chartDatasetData, out var value))
            return;

        switch (dataset)
        {
            case BarChartDataset { FloatingData: not null } barChartDataset when barChartDataset.Label == chartDatasetData.DatasetLabel:
                barChartDataset.FloatingData.Add(value is double number ? number : null);
                break;
            case BarChartDataset barChartDataset when barChartDataset.Label == chartDatasetData.DatasetLabel:
                barChartDataset.Data ??= new List<double?>();
                barChartDataset.Data.Add(value);
                break;
            case LineChartDataset lineChartDataset when lineChartDataset.Label == chartDatasetData.DatasetLabel:
                lineChartDataset.Data ??= new List<double?>();
                lineChartDataset.Data.Add(value);
                break;
        }
    }

    private static bool TryGetDataValue(ChartDatasetData chartDatasetData, out double? value)
    {
        switch (chartDatasetData.Data)
        {
            case double number:
                value = number;
                return true;
            default:
                value = null;
                return false;
        }
    }

    private static bool TryGetRange(ChartDatasetData chartDatasetData, out BarChartDataValue range)
    {
        if (chartDatasetData.Data is double[] { Length: 2 } values)
        {
            range = BarChartDataValue.Range(values[0], values[1]);
            return true;
        }

        range = null!;
        return false;
    }

    /// <summary>
    /// Returns the dataset's <see cref="BarChartDataset.FloatingData" />, creating it from <see cref="ChartDataset{TData}.Data" /> the first time,
    /// so the C# values stay equal to the browser's.
    /// </summary>
    private static List<BarChartDataValue?> UseFloatingData(BarChartDataset dataset)
    {
        if (dataset.FloatingData is null)
        {
            dataset.FloatingData = dataset.Data?.Select(v => v is double number ? (BarChartDataValue?)number : null).ToList() ?? new List<BarChartDataValue?>();
            dataset.Data = null;
        }

        return dataset.FloatingData;
    }
}
