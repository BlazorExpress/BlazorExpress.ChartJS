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

    internal ChartType _chartType;

    private bool isAsyncDisposed;

    private bool isDisposed;

    #endregion

    #region Methods

    //public async Task Stop() { }

    //public async Task ToBase64Image() { }

    //public async Task ToBase64Image(string type, double quality) { }

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

    //public async Task Clear() { }

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
    public virtual async Task InitializeAsync(ChartData chartData, IChartOptions chartOptions, string[]? plugins = null)
    {
        if (chartData is not null && chartData.Datasets is not null && chartData.Datasets.Any())
        {
            var _data = GetChartDataObject(chartData);
            await JSRuntime.InvokeVoidAsync(ChartInterop.Initialize, Id, GetChartType(), _data, chartOptions, plugins);
        }
    }

    //public async Task Render() { }

    //public async Task Reset() { }

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
        await JSRuntime.InvokeVoidAsync(ChartInterop.Resize, Id, widthWithUnit, heightWithUnit);
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
    public virtual async Task UpdateAsync(ChartData chartData, IChartOptions chartOptions)
    {
        if (chartData is null || chartData.Datasets is null || !chartData.Datasets.Any())
            return;

        var _data = GetChartDataObject(chartData);
        await JSRuntime.InvokeVoidAsync(ChartInterop.Update, Id, GetChartType(), _data, chartOptions);
    }

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

    protected string ContainerClassNames => ContainerClass!;

    protected string ContainerStyleNames =>
        BuildStyleNames(
            ContainerStyle,
            (Width.HasValue && Width.Value > 0 ? $"width:{Width.Value.ToString(CultureInfo.InvariantCulture)}{WidthUnit.ToCssString()};" : null, Width.HasValue && Width.Value > 0),
            (Height.HasValue && Height.Value > 0 ? $"height:{Height.Value.ToString(CultureInfo.InvariantCulture)}{HeightUnit.ToCssString()};" : null, Height.HasValue && Height.Value > 0)
        );

    private object GetChartDataObject(ChartData chartData)
    {
        var datasets = new List<object>();

        if (chartData?.Datasets?.Any() ?? false)
            foreach (var dataset in chartData.Datasets)
                if (dataset is BarChartDataset)
                    datasets.Add((BarChartDataset)dataset);
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

    #endregion

    #region Properties, Indexers

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

    #region Other

    ~ChartComponentBase()
    {
        Dispose(false);
    }

    #endregion
}
