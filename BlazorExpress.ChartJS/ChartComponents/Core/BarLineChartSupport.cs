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

    internal static bool IsSupportedDataset(IChartDataset dataset) =>
        dataset is BarChartDataset or LineChartDataset;

    internal static void AppendDataPoint(IChartDataset dataset, ChartDatasetData chartDatasetData)
    {
        if (!TryGetDataValue(chartDatasetData, out var value))
            return;

        switch (dataset)
        {
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
}