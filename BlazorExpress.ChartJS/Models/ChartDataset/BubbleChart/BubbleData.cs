namespace BlazorExpress.ChartJS;

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

    public double R { get; set; }

    public double X { get; set; }

    public double Y { get; set; }

    #endregion
}
