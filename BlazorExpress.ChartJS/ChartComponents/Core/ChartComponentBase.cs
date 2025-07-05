namespace BlazorExpress.ChartJS;

public abstract class ChartComponentBase : ComponentBase, IDisposable, IAsyncDisposable
{
    #region Fields and Constants

    internal ChartType _chartType;

    private bool isAsyncDisposed;

    private bool isDisposed;

    #endregion

    #region Methods

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        IsRenderComplete = true;

        await base.OnAfterRenderAsync(firstRender);
    }

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        Id ??= IdUtility.GetNextId();

        base.OnInitialized();
    }

    public static string BuildClassNames(params (string? cssClass, bool when)[] cssClassList)
    {
        var list = new HashSet<string>();

        if (cssClassList is not null && cssClassList.Any())
            foreach (var (cssClass, when) in cssClassList)
                if (!string.IsNullOrWhiteSpace(cssClass) && when)
                    list.Add(cssClass);

        if (list.Any())
            return string.Join(" ", list);

        return string.Empty;
    }

    public static string BuildClassNames(string? userDefinedCssClass, params (string? cssClass, bool when)[] cssClassList)
    {
        var list = new HashSet<string>();

        if (cssClassList is not null && cssClassList.Any())
            foreach (var (cssClass, when) in cssClassList)
                if (!string.IsNullOrWhiteSpace(cssClass) && when)
                    list.Add(cssClass);

        if (!string.IsNullOrWhiteSpace(userDefinedCssClass))
            list.Add(userDefinedCssClass.Trim());

        if (list.Any())
            return string.Join(" ", list);

        return string.Empty;
    }

    public static string BuildStyleNames(string? userDefinedCssStyle, params (string? cssStyle, bool when)[] cssStyleList)
    {
        var list = new HashSet<string>();

        if (cssStyleList is not null && cssStyleList.Any())
            foreach (var (cssStyle, when) in cssStyleList)
                if (!string.IsNullOrWhiteSpace(cssStyle) && when)
                    list.Add(cssStyle);

        if (!string.IsNullOrWhiteSpace(userDefinedCssStyle))
            list.Add(userDefinedCssStyle.Trim());

        if (list.Any())
            return string.Join(';', list);

        return string.Empty;
    }

    //public async Task Stop() { }

    //public async Task ToBase64Image() { }

    //public async Task ToBase64Image(string type, double quality) { }

    public virtual async Task<ChartData> AddDataAsync(ChartData chartData, string dataLabel, IChartDatasetData data) => await Task.FromResult(chartData);

    public virtual async Task<ChartData> AddDataAsync(ChartData chartData, string dataLabel, IReadOnlyCollection<IChartDatasetData> data) => await Task.FromResult(chartData);

    public virtual async Task<ChartData> AddDatasetAsync(ChartData chartData, IChartDataset chartDataset, IChartOptions chartOptions) => await Task.FromResult(chartData);

    /// <inheritdoc />
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/api/system.idisposable?view=net-6.0" />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    /// <seealso
    ///     href="https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-disposeasync#implement-both-dispose-and-async-dispose-patterns" />
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore(true).ConfigureAwait(false);

        Dispose(false);
        GC.SuppressFinalize(this);
    }

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

            if (_chartType == ChartType.Bar)
                await JSRuntime.InvokeVoidAsync("window.blazorChart.bar.initialize", Id, GetChartType(), _data, (BarChartOptions)chartOptions, plugins);
            else if (_chartType == ChartType.Line)
                await JSRuntime.InvokeVoidAsync("window.blazorChart.line.initialize", Id, GetChartType(), _data, (LineChartOptions)chartOptions, plugins);
            else
                await JSRuntime.InvokeVoidAsync("window.blazorChart.initialize", Id, GetChartType(), _data, chartOptions, plugins);
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
        await JSRuntime.InvokeVoidAsync("window.blazorChart.resize", Id, widthWithUnit, heightWithUnit);
    }

    /// <summary>
    /// Update the chart.
    /// </summary>
    /// <param name="chartData"></param>
    /// <param name="chartOptions"></param>
    public virtual async Task UpdateAsync(ChartData chartData, IChartOptions chartOptions)
    {
        if (chartData is not null && chartData.Datasets is not null && chartData.Datasets.Any())
        {
            var _data = GetChartDataObject(chartData);

            if (_chartType == ChartType.Bar)
                await JSRuntime.InvokeVoidAsync("window.blazorChart.bar.update", Id, GetChartType(), _data, (BarChartOptions)chartOptions);
            else if (_chartType == ChartType.Line)
                await JSRuntime.InvokeVoidAsync("window.blazorChart.line.update", Id, GetChartType(), _data, (LineChartOptions)chartOptions);
            else
                await JSRuntime.InvokeVoidAsync("window.blazorChart.update", Id, GetChartType(), _data, chartOptions);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
                // cleanup
            }

            isDisposed = true;
        }
    }

    protected virtual ValueTask DisposeAsyncCore(bool disposing)
    {
        if (!isAsyncDisposed)
        {
            if (disposing)
            {
                // cleanup
            }

            isAsyncDisposed = true;
        }

        return ValueTask.CompletedTask;
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

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = default!;

    [Parameter] public string? Class { get; set; }

    protected virtual string? ClassNames => Class;

    [Parameter]
    public string? ContainerClass { get; set; }

    [Parameter]
    public string? ContainerStyle { get; set; }

    public ElementReference Element { get; set; }

    /// <summary>
    /// Gets or sets chart container height.
    /// The default unit of measure is <see cref="Unit.Px" />.
    /// To change the unit of measure see <see cref="HeightUnit" />.
    /// </summary>
    /// <remarks>
    /// Default value is null.
    /// </remarks>
    [Parameter]
    public int? Height { get; set; }

    /// <summary>
    /// Gets or sets chart container height unit of measure.
    /// </summary>
    /// <remarks>
    /// Default value is <see cref="Unit.Px" />.
    /// </remarks>
    [Parameter]
    public Unit HeightUnit { get; set; } = Unit.Px;

    [Parameter]
    public string? Id { get; set; }

    [Parameter]
    public bool IsContainerFluid { get; set; }

    protected bool IsRenderComplete { get; private set; }

    [Inject]
    protected IJSRuntime JSRuntime { get; set; } = default!;

    [Parameter]
    public string? Style { get; set; }

    protected virtual string? StyleNames => Style;

    /// <summary>
    /// Get or sets chart container width.
    /// The default unit of measure is <see cref="Unit.Px" />.
    /// To change the unit of measure see <see cref="WidthUnit" />.
    /// </summary>
    /// <remarks>
    /// Default value is null.
    /// </remarks>
    [Parameter]
    public int? Width { get; set; }

    /// <summary>
    /// Gets or sets chart container width unit of measure.
    /// </summary>
    /// <remarks>
    /// Default value is <see cref="Unit.Px" />.
    /// </remarks>
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
