namespace BlazorExpress.ChartJS;

public partial class ScatterChart : ChartComponentBase
{
    #region Constructors and Finalizers

    public ScatterChart()
    {
        _chartType = ChartType.Scatter;
    }

    #endregion

    #region Methods

    #region Public Methods

    /// <summary>
    /// Asynchronously appends one point to the dataset named by the data's <see cref="ChartDatasetData.DatasetLabel" />.
    /// A scatter chart has no category labels, so <paramref name="dataLabel" /> is not used and <see cref="ChartData.Labels" />
    /// is never read or changed. A time point (<see cref="ChartTimePoint" />) is appended the same way.
    /// </summary>
    /// <param name="chartData">The chart data to which the point will be added.</param>
    /// <param name="dataLabel">Not used on a scatter chart, except as the dataset label of a time point.</param>
    /// <param name="data">The point to add, with the label of the dataset it belongs to.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated chart data.</returns>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously appends one point to the dataset named by the data's DatasetLabel; labels are not used.")]
    [MethodReturnTypeName($"Task<{nameof(ChartData)}>")]
    public override async Task<ChartData> AddDataAsync(ChartData chartData, string dataLabel, IChartDatasetData data)
    {
        if (chartData is null)
            throw new ArgumentNullException(nameof(chartData));

        if (chartData.Datasets is null)
            throw new ArgumentNullException(nameof(chartData.Datasets));

        if (data is null)
            throw new ArgumentNullException(nameof(data));

        if (TimeDataSupport.IsTimePoint(data))
            return await AddTimePointAsync(chartData, dataLabel, data, dataset => dataset is ScatterChartDataset, ScatterChartInterop.AddDatasetPoint);

        await AppendPointAsync(chartData, data);

        return chartData;
    }

    /// <summary>
    /// Asynchronously appends one point to each dataset the data names, through each item's <see cref="ChartDatasetData.DatasetLabel" />.
    /// A scatter chart has no category labels, so <paramref name="dataLabel" /> is not used and <see cref="ChartData.Labels" />
    /// is never read or changed; the items do not need to cover every dataset.
    /// </summary>
    /// <param name="chartData">The chart data to which the points will be added.</param>
    /// <param name="dataLabel">Not used on a scatter chart.</param>
    /// <param name="data">The points to add, each with the label of the dataset it belongs to.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated chart data.</returns>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously appends one point to each dataset the data names; labels are not used.")]
    [MethodReturnTypeName($"Task<{nameof(ChartData)}>")]
    public override async Task<ChartData> AddDataAsync(ChartData chartData, string dataLabel, IReadOnlyCollection<IChartDatasetData> data)
    {
        if (chartData is null)
            throw new ArgumentNullException(nameof(chartData));

        if (chartData.Datasets is null)
            throw new ArgumentNullException(nameof(chartData.Datasets));

        if (data is null)
            throw new ArgumentNullException(nameof(data));

        if (!data.Any())
            throw new ArgumentException($"{nameof(data)} cannot be empty.", nameof(data));

        foreach (var item in data)
            await AppendPointAsync(chartData, item);

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
    [MethodReturnTypeName($"Task<{nameof(ChartData)}>")]
    public override async Task<ChartData> AddDatasetAsync(ChartData chartData, IChartDataset chartDataset, IChartOptions chartOptions)
    {
        if (chartData is null)
            throw new ArgumentNullException(nameof(chartData));

        if (chartData.Datasets is null)
            throw new ArgumentException("chartData.Datasets must not be null", nameof(chartData));

        if (chartDataset is null)
            throw new ArgumentNullException(nameof(chartDataset));

        if (chartDataset is ScatterChartDataset)
        {
            chartData.Datasets.Add(chartDataset);
            await SafeInvokeVoidAsync(ScatterChartInterop.AddDataset, Id, (ScatterChartDataset)chartDataset);
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
    /// <exception cref="ArgumentException">An additional axis in <see cref="Scales.AdditionalAxes" /> has an empty ID, or uses the ID <c>x</c> or <c>y</c> while <see cref="Scales.X" /> or <see cref="Scales.Y" /> is set. Thrown before anything is sent to the browser.</exception>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously initializes the chart with the specified data and options.")]
    [MethodReturnTypeName(nameof(Task))]
    public override async Task InitializeAsync(ChartData chartData, IChartOptions chartOptions, string[]? plugins = null)
    {
        ChartOptionsValidator.ThrowIfInvalid(chartOptions);

        if (chartData is null)
            throw new ArgumentNullException(nameof(chartData));

        if (chartData.Datasets is null)
            throw new ArgumentException("chartData.Datasets must not be null", nameof(chartData));

        if (chartOptions is null)
            throw new ArgumentNullException(nameof(chartOptions));

        var datasets = chartData.Datasets.OfType<ScatterChartDataset>();
        var data = new { Labels = chartData.LabelsValue, Datasets = datasets };
        await SafeInvokeVoidAsync(ScatterChartInterop.Initialize, Id, GetChartType(), data, (ScatterChartOptions)chartOptions, plugins, DotNetObjectReference, EventOptions);
    }

    /// <summary>
    /// Asynchronously updates the chart with the specified data and options.
    /// </summary>
    /// <remarks>This method updates the chart by invoking a JavaScript function to render the new data and
    /// options. Ensure that <paramref name="chartData"/> contains valid datasets to avoid no operation.</remarks>
    /// <param name="chartData">The data to be displayed on the chart. Must not be null and must contain at least one dataset.</param>
    /// <param name="chartOptions">The options to configure the chart's appearance and behavior.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously updates the chart with the specified data and options.")]
    [MethodReturnTypeName(nameof(Task))]
    public override async Task UpdateAsync(ChartData chartData, IChartOptions chartOptions) =>
        await UpdateAsync(chartData, chartOptions, ChartUpdateMode.Default);

    /// <summary>
    /// Asynchronously updates the chart with the specified data and options, using a Chart.js update mode.
    /// For example, <see cref="ChartUpdateMode.None" /> applies the changes without animation.
    /// </summary>
    /// <remarks>With animation on, a dataset that matches one already on the chart animates from its current values to the new ones, and any other dataset grows in from its base. Datasets are matched by <see cref="ChartDataset{TData}.Key" /> when set, otherwise by label; datasets that share a label, or have none, pair in order of occurrence. A matched dataset keeps the visibility changed at runtime, and slices, bars and points keep theirs while their label is still present. To replay the initial animation instead, call <see cref="ResetAsync" /> and then update.</remarks>
    /// <param name="chartData">The data to be displayed on the chart. Must not be null and must contain at least one dataset.</param>
    /// <param name="chartOptions">The options to configure the chart's appearance and behavior.</param>
    /// <param name="mode">The Chart.js update mode.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentException">An additional axis in <see cref="Scales.AdditionalAxes" /> has an empty ID, or uses the ID <c>x</c> or <c>y</c> while <see cref="Scales.X" /> or <see cref="Scales.Y" /> is set. Thrown before anything is sent to the browser.</exception>
    [AddedVersion("2.0.0")]
    [Description("Asynchronously updates the chart with the specified data and options, using a Chart.js update mode.")]
    [MethodReturnTypeName(nameof(Task))]
    public override async Task UpdateAsync(ChartData chartData, IChartOptions chartOptions, ChartUpdateMode mode)
    {
        ChartOptionsValidator.ThrowIfInvalid(chartOptions);

        if (chartData is null)
            throw new ArgumentNullException(nameof(chartData));

        if (chartData.Datasets is null)
            throw new ArgumentException("chartData.Datasets must not be null", nameof(chartData));

        if (chartOptions is null)
            throw new ArgumentNullException(nameof(chartOptions));

        var datasets = chartData.Datasets.OfType<ScatterChartDataset>();
        var data = new { Labels = chartData.LabelsValue, Datasets = datasets };
        await SafeInvokeVoidAsync(ScatterChartInterop.Update, [Id, GetChartType(), data, (ScatterChartOptions)chartOptions, .. GetUpdateModeArguments(mode)]);
    }

    #endregion

    #region Private Methods
    // Appends a point to the named dataset in C# and in the browser; the browser function never touches the labels (work item 26112101).
    private async Task AppendPointAsync(ChartData chartData, IChartDatasetData data)
    {
        if (data is not ScatterChartDatasetData { Data: ScatterChartDataPoint point } scatterChartDatasetData)
            return;

        foreach (var dataset in chartData.Datasets!.OfType<ScatterChartDataset>().Where(dataset => dataset.Label == scatterChartDatasetData.DatasetLabel))
            (dataset.Data ??= new List<ScatterChartDataPoint>()).Add(point);

        await SafeInvokeVoidAsync(ScatterChartInterop.AddDatasetPoint, Id, scatterChartDatasetData.DatasetLabel, point);
    }

    #endregion

    #endregion
}
