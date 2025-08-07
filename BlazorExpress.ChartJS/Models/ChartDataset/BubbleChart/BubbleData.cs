namespace BlazorExpress.ChartJS;

/// <summary>
/// <see href="https://www.chartjs.org/docs/latest/charts/bubble.html#data-structure" />
/// </summary>
public class BubbleData
{
    #region Constructors

    public BubbleData(double x, double y, double r)
    {
        X = x;
        Y = y;
        R = r;
    }

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// X Value
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Y Value
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Bubble radius in pixels (not scaled).
    /// </summary>
    public double R { get; set; }

    #endregion
}
