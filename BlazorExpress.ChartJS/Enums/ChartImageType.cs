namespace BlazorExpress.ChartJS;

/// <summary>
/// The image format used when exporting a chart.
/// </summary>
[AddedVersion("1.3.0")]
[Description("The image format used when exporting a chart.")]
public enum ChartImageType
{
    /// <summary>
    /// PNG (<c>image/png</c>), which keeps transparency.
    /// </summary>
    [AddedVersion("1.3.0")]
    [Description("PNG (image/png), which keeps transparency.")]
    Png,

    /// <summary>
    /// JPEG (<c>image/jpeg</c>). Transparent areas of the chart canvas become black.
    /// </summary>
    [AddedVersion("1.3.0")]
    [Description("JPEG (image/jpeg). Transparent areas of the chart canvas become black.")]
    Jpeg,

    /// <summary>
    /// WebP (<c>image/webp</c>). Browsers that cannot encode WebP return PNG instead.
    /// </summary>
    [AddedVersion("1.3.0")]
    [Description("WebP (image/webp). Browsers that cannot encode WebP return PNG instead.")]
    Webp
}
