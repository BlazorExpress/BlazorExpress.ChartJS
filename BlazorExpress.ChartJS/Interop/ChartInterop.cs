namespace BlazorExpress.ChartJS;

public class ChartInterop
{
    #region Fields and Constants

    public const string AddDataset = Prefix + "addDataset";

    public const string AddDatasetData = Prefix + "addDatasetData";

    /// <summary>
    /// JavaScript function that appends a time point to the dataset with a given label.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that appends a time point to the dataset with a given label.")]
    public const string AddDatasetPoint = Prefix + "addDatasetPoint";

    public const string AddDatasetsData = Prefix + "addDatasetsData";

    /// <summary>
    /// JavaScript function that clears the chart canvas.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that clears the chart canvas.")]
    public const string Clear = Prefix + "clear";

    /// <summary>
    /// JavaScript function that destroys the Chart.js instance bound to a canvas.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that destroys the Chart.js instance bound to a canvas.")]
    public const string Destroy = Prefix + "destroy";

    /// <summary>
    /// JavaScript function that downloads the chart image as a file in the browser.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that downloads the chart image as a file in the browser.")]
    public const string DownloadImage = Prefix + "downloadImage";

    /// <summary>
    /// JavaScript function that reads the visibility of a data item.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that reads the visibility of a data item.")]
    public const string GetDataVisibility = Prefix + "getDataVisibility";

    /// <summary>
    /// JavaScript function that counts the visible datasets.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that counts the visible datasets.")]
    public const string GetVisibleDatasetCount = Prefix + "getVisibleDatasetCount";

    /// <summary>
    /// JavaScript function that hides a dataset or one data item.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that hides a dataset or one data item.")]
    public const string Hide = Prefix + "hide";

    public const string Initialize = Prefix + "initialize";

    /// <summary>
    /// JavaScript function that reads whether a dataset is visible.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that reads whether a dataset is visible.")]
    public const string IsDatasetVisible = Prefix + "isDatasetVisible";

    private const string Prefix = "window.blazorexpress.chartjs.";

    /// <summary>
    /// JavaScript function that redraws the chart elements.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that redraws the chart elements.")]
    public const string Render = Prefix + "render";

    /// <summary>
    /// JavaScript function that resets the chart to its state before the initial animation.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that resets the chart to its state before the initial animation.")]
    public const string Reset = Prefix + "reset";

    public const string Resize = Prefix + "resize";

    /// <summary>
    /// JavaScript function that highlights data items and shows their tooltip.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that highlights data items and shows their tooltip.")]
    public const string SetActiveElements = Prefix + "setActiveElements";

    /// <summary>
    /// JavaScript function that shows or hides a dataset.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that shows or hides a dataset.")]
    public const string SetDatasetVisibility = Prefix + "setDatasetVisibility";

    /// <summary>
    /// JavaScript function that updates the chart's event options.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that updates the chart's event options.")]
    public const string SetEventOptions = Prefix + "setEventOptions";

    /// <summary>
    /// JavaScript function that shows a dataset or one data item.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that shows a dataset or one data item.")]
    public const string Show = Prefix + "show";

    /// <summary>
    /// JavaScript function that stops the current animation.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that stops the current animation.")]
    public const string Stop = Prefix + "stop";

    /// <summary>
    /// JavaScript function that exports the chart image as a data URL stream.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that exports the chart image as a data URL stream.")]
    public const string ToBase64Image = Prefix + "toBase64Image";

    /// <summary>
    /// JavaScript function that toggles the visibility of a data item.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("JavaScript function that toggles the visibility of a data item.")]
    public const string ToggleDataVisibility = Prefix + "toggleDataVisibility";

    public const string Update = Prefix + "update";

    #endregion
}
