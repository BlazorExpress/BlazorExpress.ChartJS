namespace BlazorExpress.ChartJS;

public static class EnumExtensions
{
    #region Methods

    public static string? ToChartDatasetDataLabelAlignmentString(this DataLabelAlignment alignment) =>
        alignment switch
        {
            DataLabelAlignment.Start => "start",
            DataLabelAlignment.Center => "center", // default
            DataLabelAlignment.End => "end",
            _ => null
        };

    public static string? ToChartDatasetDataLabelAnchorString(this DataLabelAnchor anchor) =>
        anchor switch
        {
            DataLabelAnchor.Start => "start",
            DataLabelAnchor.Center => "center", // default
            DataLabelAnchor.End => "end",
            _ => null
        };

    /// <summary>
    /// Converts the update mode to the Chart.js update mode string.
    /// </summary>
    /// <returns>The Chart.js mode, or <see langword="null" /> for <see cref="ChartUpdateMode.Default" />.</returns>
    [AddedVersion("1.3.0")]
    [Description("Converts the update mode to the Chart.js update mode string.")]
    public static string? ToChartJsMode(this ChartUpdateMode mode) =>
        mode switch
        {
            ChartUpdateMode.Active => "active",
            ChartUpdateMode.Hide => "hide",
            ChartUpdateMode.None => "none",
            ChartUpdateMode.Reset => "reset",
            ChartUpdateMode.Resize => "resize",
            ChartUpdateMode.Show => "show",
            _ => null // default: chart.update() without a mode
        };

    public static string ToCssString(this Unit unit) =>
        unit switch
        {
            Unit.Em => "em",
            Unit.Percentage => "%",
            Unit.Pt => "pt",
            Unit.Px => "px",
            Unit.Rem => "rem",
            Unit.Vh => "vh",
            Unit.VMax => "vmax",
            Unit.VMin => "vmin",
            Unit.Vw => "vw",
            _ => string.Empty
        };

    /// <summary>
    /// Gets the file extension, without a dot, for the image type.
    /// </summary>
    /// <returns>The file extension, for example <c>png</c>.</returns>
    [AddedVersion("1.3.0")]
    [Description("Gets the file extension, without a dot, for the image type.")]
    public static string ToFileExtension(this ChartImageType imageType) =>
        imageType switch
        {
            ChartImageType.Jpeg => "jpg",
            ChartImageType.Webp => "webp",
            _ => "png"
        };

    public static string ToInteractionModeString(this InteractionMode interactionMode) =>
        interactionMode switch
        {
            InteractionMode.Dataset => "dataset",
            InteractionMode.Index => "index",
            InteractionMode.Nearest => "nearest",
            InteractionMode.Point => "point",
            InteractionMode.X => "x",
            InteractionMode.Y => "y",
            _ => string.Empty
        };

    /// <summary>
    /// Gets the MIME type for the image type.
    /// </summary>
    /// <returns>The MIME type, for example <c>image/png</c>.</returns>
    [AddedVersion("1.3.0")]
    [Description("Gets the MIME type for the image type.")]
    public static string ToMimeType(this ChartImageType imageType) =>
        imageType switch
        {
            ChartImageType.Jpeg => "image/jpeg",
            ChartImageType.Webp => "image/webp",
            _ => "image/png"
        };

    #endregion
}
