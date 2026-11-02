namespace BlazorExpress.ChartJS;

public class ChartInterop
{
    #region Fields and Constants

    public const string AddDataset = Prefix + "addDataset";

    public const string AddDatasetData = Prefix + "addDatasetData";

    public const string AddDatasetsData = Prefix + "addDatasetsData";

    public const string Destroy = Prefix + "destroy";

    public const string Initialize = Prefix + "initialize";

    private const string Prefix = "window.blazorexpress.chartjs.";

    public const string Resize = Prefix + "resize";

    public const string Update = Prefix + "update";

    #endregion
}
