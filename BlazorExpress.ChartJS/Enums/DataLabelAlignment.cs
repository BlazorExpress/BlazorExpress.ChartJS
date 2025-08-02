namespace BlazorExpress.ChartJS;

/// <summary>
/// The align option defines the position of the label relative to the anchor point position and orientation.
/// <para>
/// <see href="https://chartjs-plugin-datalabels.netlify.app/guide/positioning.html#alignment-and-offset" />
/// </para>
/// </summary>
public enum DataLabelAlignment
{
    /// <summary>
    /// 'center' (default): the label is centered on the anchor point
    /// </summary>
    Center,

    /// <summary>
    /// 'start': the label is positioned before the anchor point, following the same direction
    /// </summary>
    Start,

    /// <summary>
    /// 'end': the label is positioned after the anchor point, following the same direction
    /// </summary>
    End,

    /// <summary>
    /// 'left': the label is positioned to the left of the anchor point (180°)
    /// </summary>
    Left,

    /// <summary>
    /// 'top': the label is positioned to the top of the anchor point (270°)
    /// </summary>
    Top,

    /// <summary>
    /// 'right': the label is positioned to the right of the anchor point (0°)
    /// </summary>
    Right,

    /// <summary>
    /// 'bottom': the label is positioned to the bottom of the anchor point (90°)
    /// </summary>
    Bottom
}
