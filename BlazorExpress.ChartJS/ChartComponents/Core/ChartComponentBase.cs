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

    public virtual async Task<ChartData> AddDataAsync(ChartData chartData, string dataLabel, IChartDatasetData data) => await Task.FromResult(chartData);

    public virtual async Task<ChartData> AddDataAsync(ChartData chartData, string dataLabel, IReadOnlyCollection<IChartDatasetData> data) => await Task.FromResult(chartData);

    public virtual async Task<ChartData> AddDatasetAsync(ChartData chartData, IChartDataset chartDataset, IChartOptions chartOptions) => await Task.FromResult(chartData);

    //public async Task Clear() { }

    /// <summary>
    /// Initialize the chart.
    /// </summary>
    /// <param name="chartData"></param>
    /// <param name="chartOptions"></param>
    /// <param name="plugins"></param>
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
    /// Resize the chart.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="widthUnit"></param>
    /// <param name="heightUnit"></param>
    public async Task ResizeAsync(int width, int height, Unit widthUnit = Unit.Px, Unit heightUnit = Unit.Px)
    {
        var widthWithUnit = $"width:{width.ToString(CultureInfo.InvariantCulture)}{widthUnit.ToCssString()}";
        var heightWithUnit = $"height:{height.ToString(CultureInfo.InvariantCulture)}{heightUnit.ToCssString()}";
        await JSRuntime.InvokeVoidAsync(ChartInterop.Resize, Id, widthWithUnit, heightWithUnit);
    }

    /// <summary>
    /// Update the chart.
    /// </summary>
    /// <param name="chartData"></param>
    /// <param name="chartOptions"></param>
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
