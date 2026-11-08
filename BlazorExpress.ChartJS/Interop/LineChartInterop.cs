namespace BlazorExpress.ChartJS;

public class LineChartInterop
{
    #region Fields and Constants

    private const string Prefix = "window.blazorexpress.chartjs.line.";

    public const string AddDatasetData = Prefix + "addDatasetData";

    /// <summary>
    /// JavaScript function that appends a time point to the dataset with a given label.
    /// </summary>
    [AddedVersion("1.3.0")]
    [Description("JavaScript function that appends a time point to the dataset with a given label.")]
    public const string AddDatasetPoint = Prefix + "addDatasetPoint";

    public const string AddDatasetsData = Prefix + "addDatasetsData";

    public const string AddDataset = Prefix + "addDataset";

    public const string Initialize = Prefix + "initialize";

    public const string Update = Prefix + "update";

    #endregion
}
