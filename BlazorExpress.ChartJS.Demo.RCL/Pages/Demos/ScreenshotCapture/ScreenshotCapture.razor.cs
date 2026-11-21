using System.IO.Compression;

namespace BlazorExpress.ChartJS.Demo.RCL.Pages.Demos.ScreenshotCapture;

/// <summary>
/// Unlisted page (work item 26112001, AC-04). It captures every chart image the READMEs and the docs pages show, each from
/// its sample on this site, and downloads them as one zip with each image at its repository path. Unzip it over the code
/// repository root to refresh every image in one step. It uses only the demo site and .NET (rules.md TOOL-001).
/// </summary>
public partial class ScreenshotCapture : ComponentBase
{
    #region Fields and Constants

    private const string pageUrl = DemoRouteConstants.Demos_ScreenshotCapture;
    private const string ZipFileName = "chartjs-screenshots.zip";

    // README images are 632 CSS px wide and docs images 600; both are captured at device pixel ratio 2.
    private const int DocsWidth = 600;
    private const int ReadmeWidth = 632;

    private static readonly IReadOnlyList<CaptureEntry> manifest =
    [
        new("screenshots/bar-chart.png", "/demos/bar-chart/stacked-bar-chart-with-data-labels", 0, ReadmeWidth, "Bar chart: website visitors by browser, stacked by operating system, with data labels"),
        new("screenshots/bubble-chart.png", "/demos/bubble-chart/how-it-works", 0, ReadmeWidth, "Bubble chart: cities by household income, population growth and population"),
        new("screenshots/doughnut-chart.png", "/demos/doughnut-chart/data-labels", 0, ReadmeWidth, "Doughnut chart: revenue by sales channel, one ring per year, with data labels"),
        new("screenshots/line-chart.png", "/demos/line-chart/how-it-works", 0, ReadmeWidth, "Line chart: website visitors and sign-ups per month on two axes"),
        new("screenshots/pie-chart.png", "/demos/pie-chart/data-labels", 0, ReadmeWidth, "Pie chart: revenue by sales channel, one ring per year, with data labels"),
        new("screenshots/polar-area-chart.png", "/demos/polar-area-chart/angle-lines-and-point-labels", 1, ReadmeWidth, "Polar area chart: sales by region, each region named in its slice"),
        new("screenshots/radar-chart.png", "/demos/radar-chart/how-it-works", 0, ReadmeWidth, "Radar chart: two laptops rated on six measures"),
        new("screenshots/scatter-chart.png", "/demos/scatter-chart/how-it-works", 0, ReadmeWidth, "Scatter chart: ad spend against revenue for twelve campaigns"),
        new("screenshots/time-series.png", "/demos/line-chart/time-series", 0, ReadmeWidth, "Line chart: values over dates on a time series axis"),
        new("screenshots/tick-and-tooltip-functions.png", "/demos/line-chart/tick-and-tooltip-functions", 0, ReadmeWidth, "Line chart: axis ticks and tooltips formatted by JavaScript functions"),
        new("screenshots/floating-bars.png", "/demos/bar-chart/floating-bars", 0, ReadmeWidth, "Bar chart: floating bars from a start to an end value"),
        new("screenshots/half-gauge.png", "/demos/doughnut-chart/half-gauge", 0, ReadmeWidth, "Doughnut chart: a half gauge"),
        new("screenshots/gradient-fill.png", "/demos/line-chart/gradient-fill", 0, ReadmeWidth, "Line chart: an area filled with a gradient"),
        new("screenshots/multiple-axes.png", "/demos/line-chart/multiple-axes", 0, ReadmeWidth, "Line chart: two datasets on separate y-axes"),
        new("BlazorExpress.ChartJS.Demo.RCL/wwwroot/images/bar-chart.png", "/demos/bar-chart/stacked-bar-chart-with-data-labels", 0, DocsWidth, "Bar chart: website visitors by browser, stacked by operating system, with data labels"),
        new("BlazorExpress.ChartJS.Demo.RCL/wwwroot/images/bubble-chart.png", "/demos/bubble-chart/how-it-works", 0, DocsWidth, "Bubble chart: cities by household income, population growth and population"),
        new("BlazorExpress.ChartJS.Demo.RCL/wwwroot/images/doughnut-chart.png", "/demos/doughnut-chart/data-labels", 0, DocsWidth, "Doughnut chart: revenue by sales channel, one ring per year, with data labels"),
        new("BlazorExpress.ChartJS.Demo.RCL/wwwroot/images/line-chart.png", "/demos/line-chart/how-it-works", 0, DocsWidth, "Line chart: website visitors and sign-ups per month on two axes"),
        new("BlazorExpress.ChartJS.Demo.RCL/wwwroot/images/pie-chart.png", "/demos/pie-chart/data-labels", 0, DocsWidth, "Pie chart: revenue by sales channel, one ring per year, with data labels"),
        new("BlazorExpress.ChartJS.Demo.RCL/wwwroot/images/polar-area-chart.png", "/demos/polar-area-chart/angle-lines-and-point-labels", 1, DocsWidth, "Polar area chart: sales by region, each region named in its slice"),
        new("BlazorExpress.ChartJS.Demo.RCL/wwwroot/images/radar-chart.png", "/demos/radar-chart/how-it-works", 0, DocsWidth, "Radar chart: two laptops rated on six measures"),
        new("BlazorExpress.ChartJS.Demo.RCL/wwwroot/images/scatter-chart.png", "/demos/scatter-chart/how-it-works", 0, DocsWidth, "Scatter chart: ad spend against revenue for twelve campaigns"),
    ];

    private readonly List<CaptureResult> results = new();
    private ElementReference frame;
    private bool running;

    #endregion

    #region Methods

    #region Private Methods

    private async Task CaptureAllAsync()
    {
        if (running)
            return;

        running = true;
        results.Clear();
        var files = new List<(string Path, byte[] Bytes)>();

        foreach (var entry in manifest)
        {
            try
            {
                var image = await JS.InvokeAsync<CapturedImage>("screenshotCapture.capture", frame, entry.SampleUrl, entry.CanvasIndex, entry.CssWidth);
                var bytes = Convert.FromBase64String(image.Base64);
                files.Add((entry.Path, bytes));
                results.Add(new CaptureResult(entry, true, $"{image.Width} x {image.Height}, {bytes.Length / 1024} KB"));
            }
            catch (JSException exception)
            {
                // A sample that does not draw is reported and left out of the zip, so a partial zip never replaces a good image silently.
                results.Add(new CaptureResult(entry, false, exception.Message));
            }

            StateHasChanged();
        }

        if (files.Count > 0)
        {
            using var stream = new MemoryStream();
            using (var zip = new ZipArchive(stream, ZipArchiveMode.Create, leaveOpen: true))
            {
                foreach (var (path, bytes) in files)
                {
                    var zipEntry = zip.CreateEntry(path, CompressionLevel.Optimal);
                    await using var entryStream = zipEntry.Open();
                    await entryStream.WriteAsync(bytes);
                }
            }

            stream.Position = 0;
            using var streamReference = new DotNetStreamReference(stream, leaveOpen: true);
            await JS.InvokeVoidAsync("screenshotCapture.download", ZipFileName, streamReference);
        }

        running = false;
    }

    #endregion

    #endregion

    #region Properties, Indexers

    [Inject] private IJSRuntime JS { get; set; } = default!;

    #endregion

    #region Nested Types

    /// <summary>One image: where it lives in the code repository, the sample it shows, which chart on that page, its CSS width and its alt text.</summary>
    private sealed record CaptureEntry(string Path, string SampleUrl, int CanvasIndex, int CssWidth, string AltText);

    private sealed record CaptureResult(CaptureEntry Entry, bool Captured, string Detail);

    private sealed class CapturedImage
    {
        public string Base64 { get; set; } = string.Empty;

        public int Height { get; set; }

        public int Width { get; set; }
    }

    #endregion
}
