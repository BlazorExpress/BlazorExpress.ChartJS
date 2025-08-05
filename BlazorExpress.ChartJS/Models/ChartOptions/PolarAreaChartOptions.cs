namespace BlazorExpress.ChartJS;

public class PolarAreaChartOptions : ChartOptions
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the collection of plugins associated with the polar area chart.
    /// <para>
    /// Default value is a new instance of <see cref="PieChartPlugins"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the collection of plugins associated with the polar area chart.")]
    [ParameterTypeName(nameof(PolarAreaChartPlugins))]
    public PolarAreaChartPlugins Plugins { get; set; } = new();

    #endregion
}
