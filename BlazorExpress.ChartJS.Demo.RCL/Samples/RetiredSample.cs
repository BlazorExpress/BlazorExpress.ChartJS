namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// A sample page the site published and later removed or merged. Its URL keeps working (rules.md DEMO-002):
/// it opens <paramref name="ReplacementSlug" /> on the same chart page, or the chart page itself when there is none.
/// </summary>
/// <param name="ChartKey">The chart page the sample was on. See <see cref="SampleChartKeys" />.</param>
/// <param name="Slug">The retired URL segment.</param>
/// <param name="ReplacementSlug">The sample that replaces it, or <see langword="null" /> to open the chart page.</param>
public sealed record RetiredSample(string ChartKey, string Slug, string? ReplacementSlug = null);
