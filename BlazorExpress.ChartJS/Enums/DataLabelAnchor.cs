namespace BlazorExpress.ChartJS;

/// <summary>
/// An anchor point is defined by an orientation vector and a position on the data element. 
/// The orientation depends on the scale type (vertical, horizontal or radial). 
/// The position is calculated based on the anchor option and the orientation vector.
/// <para>
/// <see href="https://chartjs-plugin-datalabels.netlify.app/guide/positioning.html#anchoring" />
/// </para>
/// </summary>
public enum DataLabelAnchor
{
    /// <summary>
    /// 'center' (default): element center
    /// </summary>
    Center,

    /// <summary>
    /// 'start': lowest element boundary
    /// </summary>
    Start,

    /// <summary>
    /// 'end': highest element boundary
    /// </summary>
    End
}
