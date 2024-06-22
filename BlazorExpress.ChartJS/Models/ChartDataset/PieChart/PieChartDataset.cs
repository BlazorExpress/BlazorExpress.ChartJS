namespace BlazorExpress.ChartJS;

/// <summary>
/// The doughnut/pie chart allows a number of properties to be specified for each dataset. 
/// These are used to set display properties for a specific dataset.
/// <see href="https://www.chartjs.org/docs/latest/charts/doughnut.html#dataset-properties" />.
/// </summary>
public class PieChartDataset : ChartDataset
{
    #region Properties, Indexers

    /// <summary>
    /// Get or sets the background color.
    /// </summary>
    /// <remarks>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? BackgroundColor { get; set; }

    /// <summary>
    /// Get or sets the border color.
    /// </summary>
    /// <remarks>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the border width (in pixels).
    /// </summary>
    /// <remarks>
    /// Default value is 0.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? BorderWidth { get; set; }



    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public PieChartDatasetDataLabels Datalabels { get; set; } = new();

    /// <summary>
    /// The bar/arc background color when hovered.
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? HoverBackgroundColor { get; set; }

    /// <summary>
    /// The bar/arc border color when hovered.
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? HoverBorderColor { get; set; }

    /// <summary>
    /// The bar border width when hovered (in pixels).
    /// </summary>
    /// <remarks>
    /// Default value is 1.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderWidth { get; set; }

    #endregion
}

public class PieChartDatasetDataLabels
{
    #region Fields and Constants

    private Anchor anchor;

    #endregion

    #region Properties, Indexers
    
    /// <summary>
    /// Gets or sets the data labels anchor.
    /// </summary>
    /// <remarks>
    /// Default value is <see cref="Anchor.None"/>.
    /// </remarks>
    [JsonIgnore]
    public Anchor Anchor
    {
        get => anchor;
        set
        {
            anchor = value;
            DataLabelsAnchor = value.ToAnchorString();
        }
    }

    //public string? BackgroundColor { get; set; }

    public double? BorderWidth { get; set; } = 2;

    /// <summary>
    /// Gets or sets the data labels anchor.
    /// Possible values: start, center, and end.
    /// </summary>
    [JsonPropertyName("anchor")]
    public string? DataLabelsAnchor { get; private set; }

    #endregion
}
