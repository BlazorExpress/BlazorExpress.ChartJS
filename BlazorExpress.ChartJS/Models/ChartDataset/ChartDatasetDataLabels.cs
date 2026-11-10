namespace BlazorExpress.ChartJS;

/// <summary>
/// Highly customizable Chart.js plugin that displays labels on data for any type of charts.
/// <see href="https://chartjs-plugin-datalabels.netlify.app/guide/options.html#options" />.
/// </summary>
public class ChartDatasetDataLabels
{
    #region Fields and Constants

    private DataLabelAlignment alignment;

    private DataLabelAnchor anchor;

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the data labels alignment. 
    /// <para>
    /// Default value is <see cref="DataLabelAlignment.Center"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(DataLabelAlignment.Center)]
    [Description("Gets or sets the data labels alignment.")]
    [JsonIgnore]
    public DataLabelAlignment Alignment
    {
        get => alignment;
        set
        {
            alignment = value;
            DataLabelsAlignmentAsString = value.ToChartDatasetDataLabelAlignmentString();
        }
    }

    /// <summary>
    /// Gets or sets the data labels anchor.
    /// <para>
    /// Default value is <see cref="DataLabelAnchor.None"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(DataLabelAnchor.Center)]
    [Description("Gets or sets the data labels anchor.")]
    [JsonIgnore]
    public DataLabelAnchor Anchor
    {
        get => anchor;
        set
        {
            anchor = value;
            DataLabelsAnchorAsString = value.ToChartDatasetDataLabelAnchorString();
        }
    }

    /// <summary>
    /// Gets or sets the data label background color.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the data label background color.")]
    public string? BackgroundColor { get; set; }


    /// <summary>
    /// Gets or sets the data label border color.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the data label border color.")]
    public string? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the width of the border.
    /// <para>
    /// Default value is 2.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(2)]
    [Description("Gets or sets the width of the border.")]
    public double BorderWidth { get; set; } = 2;

    /// <summary>
    /// Gets the data labels alignment as a string.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets the data labels alignment as a string.")]
    [ParameterTypeName("string?")]
    [JsonPropertyName("align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DataLabelsAlignmentAsString { get; private set; }

    /// <summary>
    /// Gets the data labels anchor as a string.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets the data labels anchor as a string.")]
    [ParameterTypeName("string?")]
    [JsonPropertyName("anchor")]
    public string? DataLabelsAnchorAsString { get; private set; }

    /// <summary>
    /// Gets or sets the registered JavaScript function that returns the text of each label of this dataset, as <c>(value, context) =&gt; string</c>.
    /// Return an array for a label of several lines, or <see langword="null" /> to hide the label. It changes the label text only;
    /// the library's datalabels background color is unaffected.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://chartjs-plugin-datalabels.netlify.app/guide/formatting.html" />
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the registered JavaScript function that returns the text of each label of this dataset, as (value, context) => string. It changes the label text only; the library's datalabels background color is unaffected.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("ChartFunction?")]
    public ChartFunction? Formatter { get; set; }

    #endregion
}
