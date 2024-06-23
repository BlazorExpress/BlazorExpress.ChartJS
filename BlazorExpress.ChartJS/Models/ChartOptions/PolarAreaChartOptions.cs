namespace BlazorExpress.ChartJS;

public class PolarAreaChartOptions : ChartOptions
{
    #region Properties, Indexers

    public PieChartPlugins Plugins { get; set; } = new();

    public Scales? Scales { get; set; }

    #endregion
}
