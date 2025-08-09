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

    #endregion
}
