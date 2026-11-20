namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// Draws one chart type of the home page hero with fixed sample data in the .NET violet and cyan palette (story 26111801 AC-03, AC-09).
/// It sets no tick, grid or legend colours, so the demo theme script keeps the chart readable in light and dark (AC-10).
/// The parent re-creates this component for every chart type, so the old chart is disposed and a new one is drawn.
/// </summary>
public partial class HomeHeroChartView : ComponentBase
{
    #region Fields and Constants

    private const string Amber = "#F59E0B";
    private const int ChartHeight = 260;
    private const string Cyan = "#06B6D4";
    private const string CyanFill = "rgba(6, 182, 212, 0.15)";
    private const string CyanSolid = "rgba(6, 182, 212, 0.85)";
    private const string Slate = "#94A3B8";
    private const string Violet = "#512BD4";
    private const string VioletFill = "rgba(81, 43, 212, 0.15)";
    private const string VioletLight = "#8B5CF6";
    private const string VioletSolid = "rgba(81, 43, 212, 0.85)";

    private ChartComponentBase? chart;
    private static readonly List<string> months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun"];
    private static readonly List<string> sliceColors = [Violet, Cyan, VioletLight, Amber, Slate];
    private static readonly List<string> slices = ["Chrome", "Edge", "Firefox", "Safari", "Other"];

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && chart is not null)
            await chart.InitializeAsync(CreateData(), CreateOptions());

        await base.OnAfterRenderAsync(firstRender);
    }

    #endregion

    #region Private Methods

    private ChartData CreateData() => ChartKey switch
    {
        SampleChartKeys.BubbleChart => new ChartData
        {
            Datasets =
            [
                new BubbleChartDataset { Label = "Visitors", Data = [new(10, 22, 8), new(24, 41, 12), new(38, 30, 6), new(52, 58, 14), new(66, 44, 9)], BackgroundColor = ["rgba(81, 43, 212, 0.6)"], BorderColor = [Violet], BorderWidth = 1 },
                new BubbleChartDataset { Label = "Sign-ups", Data = [new(16, 12, 6), new(30, 26, 9), new(44, 18, 12), new(58, 36, 7), new(72, 28, 10)], BackgroundColor = ["rgba(6, 182, 212, 0.6)"], BorderColor = [Cyan], BorderWidth = 1 }
            ]
        },
        SampleChartKeys.DoughnutChart => new ChartData { Labels = slices, Datasets = [new DoughnutChartDataset { Label = "Share", Data = [48, 22, 14, 11, 5], BackgroundColor = sliceColors }] },
        SampleChartKeys.LineChart => new ChartData
        {
            Labels = months,
            Datasets =
            [
                new LineChartDataset { Label = "Visitors", Data = [42, 55, 49, 68, 74, 81], BorderColor = Violet, BackgroundColor = VioletFill, Fill = true, Tension = 0.35, PointRadius = [3] },
                new LineChartDataset { Label = "Sign-ups", Data = [18, 24, 31, 29, 40, 47], BorderColor = Cyan, BackgroundColor = CyanFill, Fill = true, Tension = 0.35, PointRadius = [3] }
            ]
        },
        SampleChartKeys.PieChart => new ChartData { Labels = slices, Datasets = [new PieChartDataset { Label = "Share", Data = [48, 22, 14, 11, 5], BackgroundColor = sliceColors }] },
        SampleChartKeys.PolarAreaChart => new ChartData { Labels = slices, Datasets = [new PolarAreaChartDataset { Label = "Share", Data = [48, 22, 14, 11, 5], BackgroundColor = ["rgba(81, 43, 212, 0.7)", "rgba(6, 182, 212, 0.7)", "rgba(139, 92, 246, 0.7)", "rgba(245, 158, 11, 0.7)", "rgba(148, 163, 184, 0.7)"] }] },
        SampleChartKeys.RadarChart => new ChartData
        {
            Labels = ["Speed", "Docs", "Types", "Events", "Themes", "Samples"],
            Datasets =
            [
                new RadarChartDataset { Label = "2.0", Data = [82, 90, 95, 88, 76, 92], BorderColor = Violet, BackgroundColor = VioletFill, Fill = true, PointRadius = [2] },
                new RadarChartDataset { Label = "1.x", Data = [70, 62, 80, 40, 60, 55], BorderColor = Cyan, BackgroundColor = CyanFill, Fill = true, PointRadius = [2] }
            ]
        },
        SampleChartKeys.ScatterChart => new ChartData
        {
            Datasets =
            [
                new ScatterChartDataset { Label = "Visitors", Data = [new(5, 22), new(12, 35), new(20, 28), new(28, 47), new(36, 41), new(44, 60), new(52, 55)], BackgroundColor = Violet, BorderColor = Violet, PointRadius = [5] },
                new ScatterChartDataset { Label = "Sign-ups", Data = [new(8, 12), new(16, 18), new(24, 26), new(32, 22), new(40, 34), new(48, 30), new(56, 42)], BackgroundColor = Cyan, BorderColor = Cyan, PointRadius = [5] }
            ]
        },
        _ => new ChartData
        {
            Labels = months,
            Datasets =
            [
                new BarChartDataset { Label = "Visitors", Data = [42, 55, 49, 68, 74, 81], BackgroundColor = [VioletSolid], BorderRadius = [4] },
                new BarChartDataset { Label = "Sign-ups", Data = [18, 24, 31, 29, 40, 47], BackgroundColor = [CyanSolid], BorderRadius = [4] }
            ]
        }
    };

    private IChartOptions CreateOptions()
    {
        // Reduced motion (AC-14): Chart.js draws the final frame at once when the duration is 0.
        var animation = ReducedMotion ? new ChartAnimation { Duration = 0 } : null;

        // Charts with two series keep their legend, so the series are not told apart by colour alone (WCAG 1.4.1, rules.md A11Y-004).
        switch (ChartKey)
        {
            case SampleChartKeys.BubbleChart:
                var bubble = new BubbleChartOptions { Animation = animation, MaintainAspectRatio = false };
                return bubble;
            case SampleChartKeys.DoughnutChart:
                var doughnut = new DoughnutChartOptions { Animation = animation, MaintainAspectRatio = false };
                doughnut.Plugins.Legend.Position = "right";
                return doughnut;
            case SampleChartKeys.LineChart:
                var line = new LineChartOptions { Animation = animation, MaintainAspectRatio = false };
                return line;
            case SampleChartKeys.PieChart:
                var pie = new PieChartOptions { Animation = animation, MaintainAspectRatio = false };
                pie.Plugins.Legend.Position = "right";
                return pie;
            case SampleChartKeys.PolarAreaChart:
                var polarArea = new PolarAreaChartOptions { Animation = animation, MaintainAspectRatio = false };
                polarArea.Plugins.Legend.Position = "right";
                return polarArea;
            case SampleChartKeys.RadarChart:
                var radar = new RadarChartOptions { Animation = animation, MaintainAspectRatio = false };
                return radar;
            case SampleChartKeys.ScatterChart:
                var scatter = new ScatterChartOptions { Animation = animation, MaintainAspectRatio = false };
                return scatter;
            default:
                var bar = new BarChartOptions { Animation = animation, MaintainAspectRatio = false };
                return bar;
        }
    }

    #endregion

    #endregion

    #region Properties, Indexers

    private string AriaLabel => $"Sample {HomeHeroChart.GetLabel(ChartKey).ToLowerInvariant()} chart";

    /// <summary>The chart type to draw, one of <see cref="SampleChartKeys" />.</summary>
    [Parameter, EditorRequired] public string ChartKey { get; set; } = SampleChartKeys.BarChart;

    /// <summary>When true, the chart draws without animation.</summary>
    [Parameter] public bool ReducedMotion { get; set; }

    #endregion
}
