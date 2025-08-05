namespace BlazorExpress.ChartJS;

public class PieChartOptions : ChartOptions
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the collection of plugins associated with the pie chart.
    /// <para>
    /// Default value is a new instance of <see cref="PieChartPlugins"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the collection of plugins associated with the pie chart.")]
    [ParameterTypeName(nameof(PieChartPlugins))]
    public PieChartPlugins Plugins { get; set; } = new();

    #endregion
}
