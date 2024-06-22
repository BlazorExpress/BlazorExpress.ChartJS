namespace BlazorExpress.ChartJS;

public static class EnumExtensions
{
    #region Methods

    public static string? ToAlignmentString(this Alignment alignment) =>
        alignment switch
        {
            Alignment.Center => "center", // default
            Alignment.Start => "start",
            Alignment.End => "end",
            _ => null
        };

    public static string? ToAnchorString(this Anchor anchor) =>
        anchor switch
        {
            Anchor.Center => "center", // default
            Anchor.Start => "start",
            Anchor.End => "end",
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
