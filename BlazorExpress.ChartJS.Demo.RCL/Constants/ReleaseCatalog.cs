namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// One published release: its version, the date it was published on GitHub (UTC), its release notes page and a one-line summary.
/// </summary>
public sealed record ReleaseInfo(string Version, DateOnly Date, string PageUrl, string Summary)
{
    /// <summary>Gets the GitHub tag, such as <c>v1.2.4</c>.</summary>
    public string Tag => $"v{Version}";

    /// <summary>Gets the date as the release pages show it, such as <c>14 July 2026</c>.</summary>
    public string DisplayDate => Date.ToString("d MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

    /// <summary>Gets the date in ISO form for the <c>time</c> element, such as <c>2026-07-14</c>.</summary>
    public string IsoDate => Date.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
}

/// <summary>
/// Every release, newest first. The release list page and the Blog menu read it. Dates are the GitHub release dates;
/// the wording follows CHANGELOG.md. To add a release, add it here, add its page, and add it to CHANGELOG.md,
/// sitemap.xml and llms.txt.
/// </summary>
public static class ReleaseCatalog
{
    public const string GitHubReleasesUrl = "https://github.com/BlazorExpress/BlazorExpress.ChartJS/releases";
    public const string GitHubRepositoryUrl = "https://github.com/BlazorExpress/BlazorExpress.ChartJS";
    public const string NuGetPackageUrl = "https://www.nuget.org/packages/BlazorExpress.ChartJS";

    public static readonly IReadOnlyList<ReleaseInfo> Releases =
    [
        new("2.0.0", new DateOnly(2026, 11, 21), DemoRouteConstants.Blog_Release_2_0_0,
            "Time axes, JavaScript functions, chart methods and events, and wider Chart.js 4.6 options. Some charts look or behave differently without any code change, so read the breaking changes before you upgrade from 1.x."),
        new("1.2.4", new DateOnly(2026, 7, 14), DemoRouteConstants.Blog_Release_1_2_4,
            "Chart item click events: an OnClick callback on every chart type."),
        new("1.2.3", new DateOnly(2026, 5, 22), DemoRouteConstants.Blog_Release_1_2_3,
            "Grouped stacked bars with the dataset Stack property, a stacked line chart demo and a Chart.js version support table."),
        new("1.2.2", new DateOnly(2026, 4, 18), DemoRouteConstants.Blog_Release_1_2_2,
            "Combo bar and line charts: bar and line datasets in one chart."),
        new("1.2.1", new DateOnly(2026, 2, 25), DemoRouteConstants.Blog_Release_1_2_1,
            ".NET 9 and .NET 10 targets, and a fix for the dashed hover line on line charts."),
        new("1.2.0", new DateOnly(2025, 8, 13), DemoRouteConstants.Blog_Release_1_2_0,
            "Bar border radius and animation delays, line interpolation modes and the line Segment property, and clearer getting-started docs."),
        new("1.1.0", new DateOnly(2025, 8, 9), DemoRouteConstants.Blog_Release_1_1_0,
            "Bubble, polar area, radar and scatter charts."),
        new("1.0.0", new DateOnly(2024, 6, 16), DemoRouteConstants.Blog_Release_1_0_0,
            "The first stable release: bar, doughnut, line and pie charts."),
        new("1.0.0-preview1", new DateOnly(2024, 6, 14), DemoRouteConstants.Blog_Release_1_0_0_Preview1,
            "The first preview: bar, doughnut, line and pie charts."),
    ];

    /// <summary>Gets the release with the given version.</summary>
    public static ReleaseInfo Get(string version) => Releases.First(r => r.Version == version);

    /// <summary>Gets the release published before the given one, or null for the first release.</summary>
    public static ReleaseInfo? Previous(string version)
    {
        var index = Releases.ToList().FindIndex(r => r.Version == version);
        return index >= 0 && index + 1 < Releases.Count ? Releases[index + 1] : null;
    }
}
