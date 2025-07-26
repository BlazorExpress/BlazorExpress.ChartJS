namespace BlazorExpress.ChartJS;

public partial class BarChart : ChartComponentBase
{
    #region Constructors

    public BarChart()
    {
        _chartType = ChartType.Bar;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Asynchronously adds a new data entry to the specified chart data.
    /// </summary>
    /// <param name="chartData">The chart data to which the new entry will be added.</param>
    /// <param name="dataLabel">The label associated with the new data entry.</param>
    /// <param name="data">The dataset containing the data to be added.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the updated <see cref="ChartData"/>.</returns>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously adds a new data entry to the specified chart data.")]
    public override async Task<ChartData> AddDataAsync(ChartData chartData, string dataLabel, IChartDatasetData data)
    {
        if (chartData is null)
            throw new ArgumentNullException(nameof(chartData));

        if (chartData.Datasets is null)
            throw new ArgumentNullException(nameof(chartData.Datasets));

        if (data is null)
            throw new ArgumentNullException(nameof(data));

        foreach (var dataset in chartData.Datasets)
            if (dataset is BarChartDataset barChartDataset && barChartDataset.Label == dataLabel)
                if (data is BarChartDatasetData barChartDatasetData)
                    barChartDataset.Data?.Add(barChartDatasetData.Data as double?);

        await JSRuntime.InvokeVoidAsync(BarChartInterop.AddDatasetData, Id, dataLabel, data);

        return chartData;
    }

    /// <summary>
    /// Asynchronously adds a new dataset to the specified chart data.
    /// </summary>
    /// <param name="chartData">The chart data to which the dataset will be added.</param>
    /// <param name="dataLabel">The label for the new dataset.</param>
    /// <param name="data">A read-only collection of data points to be included in the new dataset.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated chart data with the new
    /// dataset added.</returns>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously adds a new dataset to the specified chart data.")]
    public override async Task<ChartData> AddDataAsync(ChartData chartData, string dataLabel, IReadOnlyCollection<IChartDatasetData> data)
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

        if (!data.Any())
            throw new Exception($"{nameof(data)} cannot be empty.");

        if (chartData.Datasets.Count != data.Count)
            throw new InvalidDataException("The chart dataset count and the new data points count do not match.");

        if (chartData.Labels.Contains(dataLabel))
            throw new Exception($"{dataLabel} already exists.");

        chartData.Labels.Add(dataLabel);

        foreach (var dataset in chartData.Datasets)
            if (dataset is BarChartDataset barChartDataset)
            {
                var chartDatasetData = data.FirstOrDefault(x => x is BarChartDatasetData barChartDatasetData && barChartDatasetData.DatasetLabel == barChartDataset.Label);

                if (chartDatasetData is BarChartDatasetData barChartDatasetData)
                    barChartDataset.Data?.Add(barChartDatasetData.Data as double?);
            }

        await JSRuntime.InvokeVoidAsync(BarChartInterop.AddDatasetsData, Id, dataLabel, data?.Select(x => (BarChartDatasetData)x));

        return chartData;
    }

    /// <summary>
    /// Asynchronously adds a dataset to the specified chart data.
    /// </summary>
    /// <param name="chartData">The chart data to which the dataset will be added.</param>
    /// <param name="chartDataset">The dataset to add to the chart data.</param>
    /// <param name="chartOptions">The options that configure the chart's appearance and behavior.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated chart data with the new
    /// dataset added.</returns>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously adds a dataset to the specified chart data.")]
    public override async Task<ChartData> AddDatasetAsync(ChartData chartData, IChartDataset chartDataset, IChartOptions chartOptions)
    {
        if (chartData is null)
            throw new ArgumentNullException(nameof(chartData));

        if (chartData.Datasets is null)
            throw new ArgumentNullException(nameof(chartData.Datasets));

        if (chartDataset is null)
            throw new ArgumentNullException(nameof(chartDataset));

        if (chartDataset is BarChartDataset)
        {
            chartData.Datasets.Add(chartDataset);
            await JSRuntime.InvokeVoidAsync(BarChartInterop.AddDataset, Id, (BarChartDataset)chartDataset);
        }

        return chartData;
    }

    /// <summary>
    /// Asynchronously initializes the chart with the specified data and options.
    /// </summary>
    /// <remarks>This method prepares the chart for rendering by invoking the necessary JavaScript functions
    /// to set up the chart with the provided data and options. Ensure that <paramref name="chartData"/> contains valid
    /// datasets before calling this method.</remarks>
    /// <param name="chartData">The data to be used for the chart. Must contain at least one dataset.</param>
    /// <param name="chartOptions">The options to configure the chart's appearance and behavior.</param>
    /// <param name="plugins">An optional array of plugin identifiers to enhance the chart's functionality.</param>
    /// <returns>A task that represents the asynchronous initialization operation.</returns>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously initializes the chart with the specified data and options.")]
    public override async Task InitializeAsync(ChartData chartData, IChartOptions chartOptions, string[]? plugins = null)
    {
        if (chartData is not null && chartData.Datasets is not null)
        {
            var datasets = chartData.Datasets.OfType<BarChartDataset>();
            var data = new { chartData.Labels, Datasets = datasets };
            await JSRuntime.InvokeVoidAsync(BarChartInterop.Initialize, Id, GetChartType(), data, (BarChartOptions)chartOptions, plugins);
        }
    }

    /// <summary>
    /// Asynchronously updates the chart with the specified data and options.
    /// </summary>
    /// <remarks>This method updates the chart by invoking a JavaScript function to render the new data and
    /// options. Ensure that <paramref name="chartData"/> contains valid datasets to avoid no operation.</remarks>
    /// <param name="chartData">The data to be displayed on the chart. Must not be null and must contain at least one dataset.</param>
    /// <param name="chartOptions">The options to configure the chart's appearance and behavior.</param>
    /// <returns></returns>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously updates the chart with the specified data and options.")]
    public override async Task UpdateAsync(ChartData chartData, IChartOptions chartOptions)
    {
        if (chartData is not null && chartData.Datasets is not null)
        {
            var datasets = chartData.Datasets.OfType<BarChartDataset>();
            var data = new { chartData.Labels, Datasets = datasets };
            await JSRuntime.InvokeVoidAsync(BarChartInterop.Update, Id, GetChartType(), data, (BarChartOptions)chartOptions);
        }
    }

    #endregion
}
