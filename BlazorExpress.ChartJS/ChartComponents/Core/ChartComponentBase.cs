namespace BlazorExpress.ChartJS;

/// <summary>
/// Base class for Chart components.
/// <para>
///     <see href="https://www.chartjs.org/docs/latest/" />
/// </para>
/// </summary>
public abstract class ChartComponentBase : BlazorExpressComponentCore, IDisposable, IAsyncDisposable
{
    #region Fields and Constants

    /// <summary>
    /// The largest exported image, in bytes of data URL text, that is read from the browser.
    /// It bounds server memory on Blazor Server, where the browser is untrusted.
    /// </summary>
    private const long MaxImageBytes = 64L * 1024 * 1024;

    internal ChartType _chartType;

    private DotNetObjectReference<ChartComponentBase>? dotNetObjectReference;

    private bool isDestroyRequested;

    private bool isDisposed;

    private ChartEventOptions? lastEventOptions;

    #endregion

    #region Constructors and Finalizers

    ~ChartComponentBase()
    {
        Dispose(false);
    }

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            isDisposed = true;
            dotNetObjectReference?.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Destroys the Chart.js instance once, then releases the .NET object reference and the base resources,
    /// even when the destroy call fails.
    /// </summary>
    /// <param name="disposing"><see langword="true" /> when called from <see cref="IAsyncDisposable.DisposeAsync" />.</param>
    /// <returns>A task that represents the asynchronous dispose operation.</returns>
    [AddedVersion("1.3.0")]
    [Description("Destroys the Chart.js instance once, then releases the .NET object reference and the base resources, even when the destroy call fails.")]
    protected override async ValueTask DisposeAsyncCore(bool disposing)
    {
        try
        {
            if (disposing && !isDestroyRequested)
            {
                isDestroyRequested = true;
                isDisposed = true;

                if (IsFirstRenderComplete)
                    await SafeInvokeVoidAsync(ChartInterop.Destroy, Id);
            }
        }
        finally
        {
            if (disposing)
                dotNetObjectReference?.Dispose();

            await base.DisposeAsyncCore(disposing);
        }
    }

    /// <summary>
    /// Sends changed event options (<see cref="LegendClickToggles" /> and which event callbacks are bound) to the rendered chart.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [AddedVersion("1.3.0")]
    [Description("Sends changed event options to the rendered chart.")]
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        var eventOptions = EventOptions;
        if (lastEventOptions == eventOptions)
            return;

        var isChange = lastEventOptions is not null;
        lastEventOptions = eventOptions;

        if (isChange && CanInvokeChart)
            await SafeInvokeVoidAsync(ChartInterop.SetEventOptions, Id, eventOptions);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Asynchronously adds a new data entry to the specified chart data.
    /// </summary>
    /// <param name="chartData">The chart data to which the new entry will be added.</param>
    /// <param name="dataLabel">The label associated with the new data entry.</param>
    /// <param name="data">The dataset containing the data to be added.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the updated <see cref="ChartData"/>.</returns>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously adds a new data entry to the specified chart data.")]
    public virtual async Task<ChartData> AddDataAsync(ChartData chartData, string dataLabel, IChartDatasetData data) => await Task.FromResult(chartData);

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
    public virtual async Task<ChartData> AddDataAsync(ChartData chartData, string dataLabel, IReadOnlyCollection<IChartDatasetData> data) => await Task.FromResult(chartData);

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
    public virtual async Task<ChartData> AddDatasetAsync(ChartData chartData, IChartDataset chartDataset, IChartOptions chartOptions) => await Task.FromResult(chartData);

    /// <summary>
    /// Asynchronously clears the chart canvas (Chart.js <c>clear()</c>).
    /// </summary>
    /// <remarks>Does nothing when the chart is not rendered, is disposed, or the circuit is disconnected.</remarks>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously clears the chart canvas.")]
    [MethodReturnTypeName(nameof(Task))]
    public async Task ClearAsync()
    {
        if (CanInvokeChart)
            await SafeInvokeVoidAsync(ChartInterop.Clear, Id);
    }

    /// <summary>
    /// Asynchronously downloads the chart as an image file in the browser. The image is not sent to .NET.
    /// If the chart is animating, the download waits for the animation to finish.
    /// </summary>
    /// <remarks>Does nothing when the chart is not rendered, is disposed, or the circuit is disconnected.</remarks>
    /// <param name="fileName">The file name. Default is <c>chart.</c> followed by the extension of <paramref name="imageType" />.</param>
    /// <param name="imageType">The image format. Default is <see cref="ChartImageType.Png" />.</param>
    /// <param name="quality">The image quality from 0 to 1 for <see cref="ChartImageType.Jpeg" /> and <see cref="ChartImageType.Webp" />. Default is the browser's default.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="quality" /> is outside 0 to 1.</exception>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously downloads the chart as an image file in the browser.")]
    [MethodReturnTypeName(nameof(Task))]
    public async Task DownloadImageAsync(string? fileName = null, ChartImageType imageType = ChartImageType.Png, double? quality = null)
    {
        ValidateQuality(quality);

        if (!CanInvokeChart)
            return;

        var downloadFileName = string.IsNullOrWhiteSpace(fileName) ? $"chart.{imageType.ToFileExtension()}" : fileName;
        await SafeInvokeVoidAsync(ChartInterop.DownloadImage, Id, downloadFileName, imageType.ToMimeType(), quality);
    }

    /// <summary>
    /// Asynchronously gets whether the data item at <paramref name="index" /> is visible (Chart.js <c>getDataVisibility</c>).
    /// Only doughnut, pie and polar area charts draw this flag.
    /// </summary>
    /// <param name="index">The data index.</param>
    /// <returns>
    /// <see langword="true" /> when the data item is visible; <see langword="false" /> when it is hidden, the index is above the chart's range,
    /// or there is no chart (not rendered, disposed, or the circuit is disconnected).
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is negative.</exception>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously gets whether the data item at the index is visible.")]
    [MethodReturnTypeName("Task<bool>")]
    public async Task<bool> GetDataVisibilityAsync(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);

        return await SafeInvokeAsync(ChartInterop.GetDataVisibility, false, Id, index);
    }

    /// <summary>
    /// Asynchronously gets the number of visible datasets (Chart.js <c>getVisibleDatasetCount</c>).
    /// </summary>
    /// <returns>The number of visible datasets, or 0 when there is no chart (not rendered, disposed, or the circuit is disconnected).</returns>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously gets the number of visible datasets.")]
    [MethodReturnTypeName("Task<int>")]
    public async Task<int> GetVisibleDatasetCountAsync() => await SafeInvokeAsync(ChartInterop.GetVisibleDatasetCount, 0, Id);

    /// <summary>
    /// Dispatches a chart-item click to the configured callback.
    /// </summary>
    /// <param name="eventArgs">The selected chart data-item metadata.</param>
    /// <returns>A task that represents the asynchronous callback dispatch operation.</returns>
    [JSInvokable]
    [AddedVersion("1.2.4")]
    [Description("Dispatches a chart-item click to the configured callback.")]
    public async Task HandleClickAsync(ChartClickEventArgs eventArgs)
    {
        if (OnClick.HasDelegate)
            await OnClick.InvokeAsync(eventArgs);
    }

    /// <summary>
    /// Dispatches a change of the hovered chart item to the configured callback.
    /// </summary>
    /// <param name="eventArgs">The hovered data-item metadata, sent by the browser.</param>
    /// <returns>A task that represents the asynchronous callback dispatch operation.</returns>
    [JSInvokable]
    [AddedVersion("1.3.0")]
    [Description("Dispatches a change of the hovered chart item to the configured callback.")]
    public async Task HandleHoverAsync(ChartHoverEventArgs eventArgs)
    {
        if (OnHover.HasDelegate)
            await OnHover.InvokeAsync(eventArgs);
    }

    /// <summary>
    /// Dispatches a legend-item click to the configured callback.
    /// </summary>
    /// <param name="eventArgs">The clicked legend-item metadata, sent by the browser.</param>
    /// <returns>A task that represents the asynchronous callback dispatch operation.</returns>
    [JSInvokable]
    [AddedVersion("1.3.0")]
    [Description("Dispatches a legend-item click to the configured callback.")]
    public async Task HandleLegendClickAsync(ChartLegendClickEventArgs eventArgs)
    {
        if (OnLegendClick.HasDelegate)
            await OnLegendClick.InvokeAsync(eventArgs);
    }

    /// <summary>
    /// Asynchronously hides a dataset (Chart.js <c>hide(datasetIndex)</c>). The dataset stays hidden across <see cref="UpdateAsync(ChartData, IChartOptions)" />.
    /// </summary>
    /// <remarks>An index above the chart's range does nothing, as does a call with no chart.</remarks>
    /// <param name="datasetIndex">The dataset index.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="datasetIndex" /> is negative.</exception>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously hides a dataset.")]
    [MethodReturnTypeName(nameof(Task))]
    public async Task HideAsync(int datasetIndex) => await SetVisibilityAsync(ChartInterop.Hide, datasetIndex, null);

    /// <summary>
    /// Asynchronously hides one data item. On doughnut, pie and polar area charts this hides the slice;
    /// on other charts it hides the bar or point (Chart.js <c>hide(datasetIndex, dataIndex)</c>).
    /// The item stays hidden across <see cref="UpdateAsync(ChartData, IChartOptions)" />.
    /// </summary>
    /// <remarks>An index above the chart's range does nothing, as does a call with no chart.</remarks>
    /// <param name="datasetIndex">The dataset index.</param>
    /// <param name="dataIndex">The data index within the dataset.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="datasetIndex" /> or <paramref name="dataIndex" /> is negative.</exception>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously hides one data item (a slice, bar or point).")]
    [MethodReturnTypeName(nameof(Task))]
    public async Task HideAsync(int datasetIndex, int dataIndex) => await SetVisibilityAsync(ChartInterop.Hide, datasetIndex, dataIndex);

    /// <summary>
    /// Asynchronously initializes the chart with the specified data and options.
    /// </summary>
    /// <remarks>This method prepares the chart for rendering by invoking the necessary JavaScript functions
    /// to set up the chart with the provided data and options. Ensure that <paramref name="chartData"/> contains valid
    /// datasets before calling this method. Calling it again on a chart that is already initialized has no effect;
    /// use <see cref="UpdateAsync(ChartData, IChartOptions)"/> to change the data or options of a rendered chart.</remarks>
    /// <param name="chartData">The data to be used for the chart. Must contain at least one dataset.</param>
    /// <param name="chartOptions">The options to configure the chart's appearance and behavior.</param>
    /// <param name="plugins">An optional array of plugin identifiers to enhance the chart's functionality.</param>
    /// <returns>A task that represents the asynchronous initialization operation.</returns>
    /// <exception cref="ArgumentException">An additional axis in <see cref="Scales.AdditionalAxes" /> has an empty ID, or uses the ID <c>x</c> or <c>y</c> while <see cref="Scales.X" /> or <see cref="Scales.Y" /> is set. Thrown before anything is sent to the browser.</exception>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously initializes the chart with the specified data and options.")]
    public virtual async Task InitializeAsync(ChartData chartData, IChartOptions chartOptions, string[]? plugins = null)
    {
        ChartOptionsValidator.ThrowIfInvalid(chartOptions);

        if (chartData is not null && chartData.Datasets is not null && chartData.Datasets.Any())
        {
            var _data = GetChartDataObject(chartData);
            await SafeInvokeVoidAsync(ChartInterop.Initialize, Id, GetChartType(), _data, chartOptions, plugins, DotNetObjectReference, EventOptions);
        }
    }

    /// <summary>
    /// Asynchronously gets whether a dataset is visible (Chart.js <c>isDatasetVisible</c>).
    /// </summary>
    /// <param name="datasetIndex">The dataset index.</param>
    /// <returns>
    /// <see langword="true" /> when the dataset is visible; <see langword="false" /> when it is hidden, the index is above the chart's range,
    /// or there is no chart (not rendered, disposed, or the circuit is disconnected).
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="datasetIndex" /> is negative.</exception>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously gets whether a dataset is visible.")]
    [MethodReturnTypeName("Task<bool>")]
    public async Task<bool> IsDatasetVisibleAsync(int datasetIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(datasetIndex);

        return await SafeInvokeAsync(ChartInterop.IsDatasetVisible, false, Id, datasetIndex);
    }

    /// <summary>
    /// Asynchronously redraws all chart elements (Chart.js <c>render()</c>). It does not apply new data; use <see cref="UpdateAsync(ChartData, IChartOptions)" /> for that.
    /// </summary>
    /// <remarks>Does nothing when the chart is not rendered, is disposed, or the circuit is disconnected.</remarks>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously redraws all chart elements.")]
    [MethodReturnTypeName(nameof(Task))]
    public async Task RenderAsync()
    {
        if (CanInvokeChart)
            await SafeInvokeVoidAsync(ChartInterop.Render, Id);
    }

    /// <summary>
    /// Asynchronously resets the chart to its state before the initial animation (Chart.js <c>reset()</c>).
    /// A later animated update then animates from that state, so <c>ResetAsync</c> followed by
    /// <see cref="UpdateAsync(ChartData, IChartOptions)" /> regrows the chart as on first render.
    /// </summary>
    /// <remarks>Without a reset, an animated update animates matched datasets from their current values.
    /// Does nothing when the chart is not rendered, is disposed, or the circuit is disconnected.</remarks>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously resets the chart to its state before the initial animation.")]
    [MethodReturnTypeName(nameof(Task))]
    public async Task ResetAsync()
    {
        if (CanInvokeChart)
            await SafeInvokeVoidAsync(ChartInterop.Reset, Id);
    }

    /// <summary>
    /// Asynchronously resizes the chart to the specified dimensions.
    /// </summary>
    /// <remarks>This method updates the chart's dimensions by invoking a JavaScript function. Ensure that the
    /// chart is properly initialized before calling this method. The operation is performed asynchronously and will not
    /// block the calling thread.</remarks>
    /// <param name="width">The new width of the chart. Must be a non-negative integer.</param>
    /// <param name="height">The new height of the chart. Must be a non-negative integer.</param>
    /// <param name="widthUnit">The unit of measurement for the width. Defaults to pixels.</param>
    /// <param name="heightUnit">The unit of measurement for the height. Defaults to pixels.</param>
    /// <returns>A task that represents the asynchronous resize operation.</returns>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously resizes the chart to the specified dimensions.")]
    public async Task ResizeAsync(int width, int height, Unit widthUnit = Unit.Px, Unit heightUnit = Unit.Px)
    {
        var widthWithUnit = $"width:{width.ToString(CultureInfo.InvariantCulture)}{widthUnit.ToCssString()}";
        var heightWithUnit = $"height:{height.ToString(CultureInfo.InvariantCulture)}{heightUnit.ToCssString()}";
        await SafeInvokeVoidAsync(ChartInterop.Resize, Id, widthWithUnit, heightWithUnit);
    }

    /// <summary>
    /// Asynchronously highlights the given data items and shows their tooltip (Chart.js <c>setActiveElements</c>).
    /// An empty collection clears the highlight and the tooltip.
    /// </summary>
    /// <remarks>Items above the chart's range are ignored; when every item is out of range, nothing changes. Does nothing when there is no chart.</remarks>
    /// <param name="elements">The data items to highlight.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="elements" /> or one of its items is <see langword="null" />.</exception>
    /// <exception cref="ArgumentOutOfRangeException">An item has a negative index.</exception>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously highlights the given data items and shows their tooltip. An empty collection clears both.")]
    [MethodReturnTypeName(nameof(Task))]
    public async Task SetActiveElementsAsync(IEnumerable<ChartActiveElement> elements)
    {
        ArgumentNullException.ThrowIfNull(elements);

        var activeElements = elements.ToList();
        foreach (var element in activeElements)
        {
            ArgumentNullException.ThrowIfNull(element, nameof(elements));
            ArgumentOutOfRangeException.ThrowIfNegative(element.DatasetIndex, nameof(elements));
            ArgumentOutOfRangeException.ThrowIfNegative(element.Index, nameof(elements));
        }

        if (CanInvokeChart)
            await SafeInvokeVoidAsync(ChartInterop.SetActiveElements, Id, activeElements);
    }

    /// <summary>
    /// Asynchronously shows or hides a dataset (Chart.js <c>setDatasetVisibility</c> then <c>update()</c>, which animates the remaining datasets).
    /// The visibility is kept across <see cref="UpdateAsync(ChartData, IChartOptions)" />.
    /// </summary>
    /// <remarks>An index above the chart's range does nothing, as does a call with no chart.</remarks>
    /// <param name="datasetIndex">The dataset index.</param>
    /// <param name="visible"><see langword="true" /> to show the dataset; <see langword="false" /> to hide it.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="datasetIndex" /> is negative.</exception>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously shows or hides a dataset.")]
    [MethodReturnTypeName(nameof(Task))]
    public async Task SetDatasetVisibilityAsync(int datasetIndex, bool visible)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(datasetIndex);

        if (CanInvokeChart)
            await SafeInvokeVoidAsync(ChartInterop.SetDatasetVisibility, Id, datasetIndex, visible);
    }

    /// <summary>
    /// Asynchronously shows a dataset (Chart.js <c>show(datasetIndex)</c>). The dataset stays visible across <see cref="UpdateAsync(ChartData, IChartOptions)" />.
    /// </summary>
    /// <remarks>An index above the chart's range does nothing, as does a call with no chart.</remarks>
    /// <param name="datasetIndex">The dataset index.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="datasetIndex" /> is negative.</exception>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously shows a dataset.")]
    [MethodReturnTypeName(nameof(Task))]
    public async Task ShowAsync(int datasetIndex) => await SetVisibilityAsync(ChartInterop.Show, datasetIndex, null);

    /// <summary>
    /// Asynchronously shows one data item. On doughnut, pie and polar area charts this shows the slice;
    /// on other charts it shows the bar or point (Chart.js <c>show(datasetIndex, dataIndex)</c>).
    /// The item stays visible across <see cref="UpdateAsync(ChartData, IChartOptions)" />.
    /// </summary>
    /// <remarks>An index above the chart's range does nothing, as does a call with no chart.</remarks>
    /// <param name="datasetIndex">The dataset index.</param>
    /// <param name="dataIndex">The data index within the dataset.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="datasetIndex" /> or <paramref name="dataIndex" /> is negative.</exception>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously shows one data item (a slice, bar or point).")]
    [MethodReturnTypeName(nameof(Task))]
    public async Task ShowAsync(int datasetIndex, int dataIndex) => await SetVisibilityAsync(ChartInterop.Show, datasetIndex, dataIndex);

    /// <summary>
    /// Asynchronously stops the current animation (Chart.js <c>stop()</c>). The chart stays as it was drawn at that moment; Chart.js 4 ends the animation, so <see cref="RenderAsync" /> only redraws that frame. Call <see cref="UpdateAsync(ChartData, IChartOptions)" /> to animate again.
    /// </summary>
    /// <remarks>Does nothing when the chart is not rendered, is disposed, or the circuit is disconnected.</remarks>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously stops the current animation.")]
    [MethodReturnTypeName(nameof(Task))]
    public async Task StopAsync()
    {
        if (CanInvokeChart)
            await SafeInvokeVoidAsync(ChartInterop.Stop, Id);
    }

    /// <summary>
    /// Asynchronously exports the chart as a base64 data URL (Chart.js <c>toBase64Image</c>), for example <c>data:image/png;base64,...</c>.
    /// If the chart is animating, the export waits for the animation to finish (at most about 5 seconds).
    /// The image is streamed from the browser, so Blazor Server needs no larger SignalR message size.
    /// </summary>
    /// <param name="imageType">The image format. Default is <see cref="ChartImageType.Png" />.</param>
    /// <param name="quality">The image quality from 0 to 1 for <see cref="ChartImageType.Jpeg" /> and <see cref="ChartImageType.Webp" />. Default is the browser's default.</param>
    /// <returns>The data URL, or <see langword="null" /> when there is no chart (not rendered, disposed, or the circuit is disconnected).</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="quality" /> is outside 0 to 1, or the image is larger than 64 MiB.</exception>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously exports the chart as a base64 data URL.")]
    [MethodReturnTypeName("Task<string?>")]
    public async Task<string?> ToBase64ImageAsync(ChartImageType imageType = ChartImageType.Png, double? quality = null)
    {
        ValidateQuality(quality);

        var streamReference = await SafeInvokeAsync<IJSStreamReference?>(ChartInterop.ToBase64Image, null, Id, imageType.ToMimeType(), quality);
        if (streamReference is null)
            return null;

        string? dataUrl;

        try
        {
            await using var stream = await streamReference.OpenReadStreamAsync(MaxImageBytes);
            using var reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            dataUrl = await reader.ReadToEndAsync();
        }
        catch (JSDisconnectedException)
        {
            // The circuit is gone while the image was streaming.
            return null;
        }
        catch (TaskCanceledException)
        {
            // The read was cancelled while the component was being torn down.
            return null;
        }
        finally
        {
            await DisposeStreamReferenceAsync(streamReference);
        }

        return string.IsNullOrEmpty(dataUrl) ? null : dataUrl;
    }

    /// <summary>
    /// Asynchronously toggles the visibility of the data item at <paramref name="index" /> (Chart.js <c>toggleDataVisibility</c>).
    /// Only doughnut, pie and polar area charts draw this flag; on those charts the change is kept across <see cref="UpdateAsync(ChartData, IChartOptions)" />.
    /// </summary>
    /// <remarks>An index above the chart's range does nothing, as does a call with no chart.</remarks>
    /// <param name="index">The data index.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is negative.</exception>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously toggles the visibility of a data item (drawn on doughnut, pie and polar area charts).")]
    [MethodReturnTypeName(nameof(Task))]
    public async Task ToggleDataVisibilityAsync(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);

        if (CanInvokeChart)
            await SafeInvokeVoidAsync(ChartInterop.ToggleDataVisibility, Id, index);
    }

    /// <summary>
    /// Asynchronously updates the chart with the specified data and options.
    /// </summary>
    /// <remarks>This method updates the chart by invoking a JavaScript function to render the new data and
    /// options. Ensure that <paramref name="chartData"/> contains valid datasets to avoid no operation.
    /// With animation on, a dataset that matches one already on the chart animates from its current values to the new ones, and any other dataset grows in from its base. Datasets are matched by <see cref="ChartDataset{TData}.Key" /> when set, otherwise by label; datasets that share a label, or have none, pair in order of occurrence. A matched dataset keeps the visibility changed at runtime, and slices, bars and points keep theirs while their label is still present. To replay the initial animation instead, call <see cref="ResetAsync" /> and then update.</remarks>
    /// <param name="chartData">The data to be displayed on the chart. Must not be null and must contain at least one dataset.</param>
    /// <param name="chartOptions">The options to configure the chart's appearance and behavior.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [AddedVersion("1.0.0")]
    [Description("Asynchronously updates the chart with the specified data and options.")]
    public virtual async Task UpdateAsync(ChartData chartData, IChartOptions chartOptions) =>
        await UpdateAsync(chartData, chartOptions, ChartUpdateMode.Default);

    /// <summary>
    /// Asynchronously updates the chart with the specified data and options, using a Chart.js update mode.
    /// For example, <see cref="ChartUpdateMode.None" /> applies the changes without animation.
    /// </summary>
    /// <remarks>
    /// With animation on, a dataset that matches one already on the chart animates from its current values to the new ones, and any other dataset grows in from its base. Datasets are matched by <see cref="ChartDataset{TData}.Key" /> when set, otherwise by label; datasets that share a label, or have none, pair in order of occurrence. A matched dataset keeps the visibility changed at runtime, and slices, bars and points keep theirs while their label is still present. To replay the initial animation instead, call <see cref="ResetAsync" /> and then update.
    /// <para>
    /// The 2-argument overload calls this one with <see cref="ChartUpdateMode.Default" />. A derived chart that overrides this
    /// overload must call <c>base.UpdateAsync(chartData, chartOptions, mode)</c>, not the 2-argument overload, which would call back into the override.
    /// </para>
    /// </remarks>
    /// <param name="chartData">The data to be displayed on the chart. Must not be null and must contain at least one dataset.</param>
    /// <param name="chartOptions">The options to configure the chart's appearance and behavior.</param>
    /// <param name="mode">The Chart.js update mode.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentException">An additional axis in <see cref="Scales.AdditionalAxes" /> has an empty ID, or uses the ID <c>x</c> or <c>y</c> while <see cref="Scales.X" /> or <see cref="Scales.Y" /> is set. Thrown before anything is sent to the browser.</exception>
    [AddedVersion("1.3.0")]
    [Description("Asynchronously updates the chart with the specified data and options, using a Chart.js update mode.")]
    [MethodReturnTypeName(nameof(Task))]
    public virtual async Task UpdateAsync(ChartData chartData, IChartOptions chartOptions, ChartUpdateMode mode)
    {
        ChartOptionsValidator.ThrowIfInvalid(chartOptions);

        if (chartData is null || chartData.Datasets is null || !chartData.Datasets.Any())
            return;

        var _data = GetChartDataObject(chartData);
        await SafeInvokeVoidAsync(ChartInterop.Update, [Id, GetChartType(), _data, chartOptions, .. GetUpdateModeArguments(mode)]);
    }

    #endregion

    #region Internal Methods

    /// <summary>
    /// Gets the trailing JavaScript <c>update</c> argument for <paramref name="mode" />: none for <see cref="ChartUpdateMode.Default" />,
    /// so a default update sends the same call as before update modes existed.
    /// </summary>
    internal static object?[] GetUpdateModeArguments(ChartUpdateMode mode) =>
        mode.ToChartJsMode() is { } chartJsMode ? [chartJsMode] : [];

    #endregion

    #region Protected Methods

    protected string GetChartType() =>
        _chartType switch
        {
            ChartType.Bar => "bar",
            ChartType.Bubble => "bubble",
            ChartType.Doughnut => "doughnut",
            ChartType.Line => "line",
            ChartType.Pie => "pie",
            ChartType.PolarArea => "polarArea",
            ChartType.Radar => "radar",
            ChartType.Scatter => "scatter",
            _ => "line" // default
        };

    /// <summary>
    /// Invokes a JavaScript function that returns a value for this chart. Returns <paramref name="defaultValue" /> when there is no chart
    /// to ask (before the first render or after dispose), when the circuit is disconnected, or when the call is cancelled during teardown.
    /// </summary>
    /// <typeparam name="TValue">The JSON-serializable return type.</typeparam>
    /// <param name="identifier">The JavaScript function to invoke.</param>
    /// <param name="defaultValue">The value returned when the chart cannot answer.</param>
    /// <param name="args">The JSON-serializable arguments.</param>
    /// <returns>A task that represents the asynchronous invocation, with the function's result or <paramref name="defaultValue" />.</returns>
    [AddedVersion("1.3.0")]
    [Description("Invokes a JavaScript function that returns a value for this chart, returning a default value when there is no chart to ask.")]
    protected async Task<TValue> SafeInvokeAsync<TValue>(string identifier, TValue defaultValue, params object?[] args)
    {
        if (!CanInvokeChart)
            return defaultValue;

        try
        {
            return await JSRuntime.InvokeAsync<TValue>(identifier, args);
        }
        catch (JSDisconnectedException)
        {
            // The circuit is gone, so there is no browser-side chart to ask.
            return defaultValue;
        }
        catch (TaskCanceledException)
        {
            // The call was cancelled while the component was being torn down.
            return defaultValue;
        }
    }

    /// <summary>
    /// Invokes a JavaScript function for this chart, treating a disconnected circuit or a cancelled call during
    /// teardown as benign. After the component is disposed, only the chart destroy call is sent.
    /// </summary>
    /// <param name="identifier">The JavaScript function to invoke.</param>
    /// <param name="args">The JSON-serializable arguments.</param>
    /// <returns>A task that represents the asynchronous invocation.</returns>
    [AddedVersion("1.3.0")]
    [Description("Invokes a JavaScript function for this chart, treating a disconnected circuit or a cancelled call during teardown as benign.")]
    protected async Task SafeInvokeVoidAsync(string identifier, params object?[] args)
    {
        if (isDisposed && identifier != ChartInterop.Destroy)
            return;

        try
        {
            await JSRuntime.InvokeVoidAsync(identifier, args);
        }
        catch (JSDisconnectedException)
        {
            // The circuit is gone, so there is no browser-side chart left to update.
        }
        catch (TaskCanceledException)
        {
            // The call was cancelled while the component was being torn down.
        }
    }

    #endregion

    #region Private Methods

    // The data URL is already read, or the read failed; a teardown during disposal must not change that result.
    private static async Task DisposeStreamReferenceAsync(IJSStreamReference streamReference)
    {
        try
        {
            await streamReference.DisposeAsync();
        }
        catch (JSDisconnectedException)
        {
            // The circuit is gone, so the browser released the stream already.
        }
        catch (TaskCanceledException)
        {
            // The component is being torn down.
        }
    }

    private object GetChartDataObject(ChartData chartData)
    {
        var datasets = new List<object>();

        if (chartData?.Datasets?.Any() ?? false)
            foreach (var dataset in chartData.Datasets)
                if (dataset is BarChartDataset)
                    datasets.Add((BarChartDataset)dataset);
                else if (dataset is BubbleChartDataset)
                    datasets.Add((BubbleChartDataset)dataset);
                else if (dataset is DoughnutChartDataset)
                    datasets.Add((DoughnutChartDataset)dataset);
                else if (dataset is LineChartDataset)
                    datasets.Add((LineChartDataset)dataset);
                else if (dataset is PieChartDataset)
                    datasets.Add((PieChartDataset)dataset);
                else if (dataset is PolarAreaChartDataset)
                    datasets.Add((PolarAreaChartDataset)dataset);
                else if (dataset is RadarChartDataset)
                    datasets.Add((RadarChartDataset)dataset);
                else if (dataset is ScatterChartDataset)
                    datasets.Add((ScatterChartDataset)dataset);

        var data = new { chartData?.Labels, Datasets = datasets };

        return data;
    }

    private async Task SetVisibilityAsync(string identifier, int datasetIndex, int? dataIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(datasetIndex);

        if (dataIndex.HasValue)
            ArgumentOutOfRangeException.ThrowIfNegative(dataIndex.Value, "dataIndex");

        if (CanInvokeChart)
            await SafeInvokeVoidAsync(identifier, Id, datasetIndex, dataIndex);
    }

    private static void ValidateQuality(double? quality)
    {
        if (quality is < 0 or > 1 || (quality.HasValue && double.IsNaN(quality.Value)))
            throw new ArgumentOutOfRangeException(nameof(quality), quality, "The image quality must be between 0 and 1.");
    }

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets a value indicating whether a browser-side chart can exist: the component has rendered and is not disposed.
    /// </summary>
    private bool CanInvokeChart => IsFirstRenderComplete && !isDisposed;

    /// <summary>
    /// Gets or sets the CSS class name for the container element.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the CSS class name for the container element.")]
    [Parameter]
    public string? ContainerClass { get; set; }

    protected string ContainerClassNames => ContainerClass!;

    /// <summary>
    /// Gets or sets the CSS style string applied to the container element.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the CSS style string applied to the container element.")]
    [Parameter]
    public string? ContainerStyle { get; set; }

    protected string ContainerStyleNames =>
        BuildStyleNames(
            ContainerStyle,
            (Width.HasValue && Width.Value > 0 ? $"width:{Width.Value.ToString(CultureInfo.InvariantCulture)}{WidthUnit.ToCssString()};" : null, Width.HasValue && Width.Value > 0),
            (Height.HasValue && Height.Value > 0 ? $"height:{Height.Value.ToString(CultureInfo.InvariantCulture)}{HeightUnit.ToCssString()};" : null, Height.HasValue && Height.Value > 0)
        );

    protected DotNetObjectReference<ChartComponentBase> DotNetObjectReference =>
        dotNetObjectReference ??= Microsoft.JSInterop.DotNetObjectReference.Create(this);

    /// <summary>
    /// Gets the event options the browser-side chart needs: whether legend clicks toggle visibility and which event callbacks are bound.
    /// </summary>
    internal ChartEventOptions EventOptions => new(OnHover.HasDelegate, OnLegendClick.HasDelegate, LegendClickToggles);

    /// <summary>
    /// Gets or sets chart container height.
    /// The default unit of measure is <see cref="Unit.Px" />.
    /// To change the unit of measure see <see cref="HeightUnit" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets chart container height.")]
    [ParameterTypeName("int?")]
    [Parameter]
    public int? Height { get; set; }

    /// <summary>
    /// Gets or sets chart container height unit of measure.
    /// <para>
    /// Default value is <see cref="Unit.Px" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(Unit.Px)]
    [Description("Gets or sets chart container height unit of measure.")]
    [Parameter]
    public Unit HeightUnit { get; set; } = Unit.Px;

    /// <summary>
    /// Gets or sets a value indicating whether the container is fluid.
    /// <para>
    /// Default value is <see langword="false" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the container is fluid.")]
    [Parameter]
    public bool IsContainerFluid { get; set; }

    protected bool IsRenderComplete { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether clicking a legend item toggles the visibility of its dataset or slice, as Chart.js does by default.
    /// Set it to <see langword="false" /> to handle legend clicks yourself in <see cref="OnLegendClick" />.
    /// <para>
    /// Default value is <see langword="true" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether clicking a legend item toggles the visibility of its dataset or slice.")]
    [Parameter]
    public bool LegendClickToggles { get; set; } = true;

    /// <summary>
    /// Gets or sets the callback that is invoked when a chart data item is selected.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.2.4")]
    [DefaultValue(null)]
    [Description("Gets or sets the callback that is invoked when a chart data item is selected.")]
    [Parameter]
    public EventCallback<ChartClickEventArgs> OnClick { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked when the hovered chart data item changes, and once with no item when the pointer leaves all items.
    /// The event data comes from the browser.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the callback that is invoked when the hovered chart data item changes, and once with no item when the pointer leaves all items.")]
    [Parameter]
    public EventCallback<ChartHoverEventArgs> OnHover { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked when a legend item is clicked, after any visibility toggle (see <see cref="LegendClickToggles" />).
    /// The event data comes from the browser.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the callback that is invoked when a legend item is clicked, after any visibility toggle.")]
    [Parameter]
    public EventCallback<ChartLegendClickEventArgs> OnLegendClick { get; set; }

    /// <summary>
    /// Gets or sets chart container width.
    /// The default unit of measure is <see cref="Unit.Px" />.
    /// To change the unit of measure see <see cref="WidthUnit" />.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets chart container width.")]
    [ParameterTypeName("int?")]
    [Parameter]
    public int? Width { get; set; }

    /// <summary>
    /// Gets or sets chart container width unit of measure.
    /// <para>
    /// Default value is <see cref="Unit.Px" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(Unit.Px)]
    [Description("Gets or sets chart container width unit of measure.")]
    [Parameter]
    public Unit WidthUnit { get; set; } = Unit.Px;

    #endregion

    #region Nested Types

    /// <summary>
    /// The event options sent to the browser-side chart. Serialized as <c>hasHover</c>, <c>hasLegendClick</c> and <c>legendClickToggles</c>.
    /// </summary>
    internal readonly record struct ChartEventOptions(bool HasHover, bool HasLegendClick, bool LegendClickToggles);

    #endregion
}
