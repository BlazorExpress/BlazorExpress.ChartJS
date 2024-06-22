﻿namespace BlazorExpress.ChartJS;

/// <summary>
/// The bar chart allows a number of properties to be specified for each dataset. 
/// These are used to set display properties for a specific dataset.
/// <see href="https://www.chartjs.org/docs/latest/charts/bar.html#dataset-properties" />
/// </summary>
public class BarChartDataset : ChartDataset
{
    #region Properties, Indexers

    /// <summary>
    /// Percent (0-1) of the available width each bar should be within the category width.
    /// 1.0 will take the whole category width and put the bars right next to each other.
    /// </summary>
    /// <remarks>
    /// Default value is 0.9.
    /// </remarks>
    public double BarPercentage { get; set; } = 0.9;

    /// <summary>
    /// Border thickness
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? BarThickness { get; set; }

    /// <summary>
    /// Border radius
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    public List<double>? BorderRadius { get; set; }

    //BorderSkipped
    //https://www.chartjs.org/docs/latest/api/interfaces/BarControllerDatasetOptions.html#borderskipped

    /// <summary>
    /// Percent (0-1) of the available width each category should be within the sample width.
    /// </summary>
    /// <remarks>
    /// Default value is 0.8.
    /// </remarks>
    public double CategoryPercentage { get; set; } = 0.8;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public BarChartDatasetDataLabels Datalabels { get; set; } = new(); // TODO: add the reference link

    /// <summary>
    /// Should the bars be grouped on index axis. 
    /// When true, all the datasets at same index value will be placed next to each other centering on that index value. 
    /// When false, each bar is placed on its actual index-axis value.
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="true"/>.
    /// </remarks>
    public bool Grouped { get; set; } = true;

    /// <summary>
    /// The bar border radius when hovered (in pixels).
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderRadius { get; set; }

    /// <summary>
    /// The base axis of the chart. 'x' for vertical charts and 'y' for horizontal charts.
    /// </summary>
    /// <remarks>
    /// Default value is 'x'.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string IndexAxis { get; set; } = "x";

    //InflateAmount
    //https://www.chartjs.org/docs/latest/charts/bar.html#inflateamount

    /// <summary>
    /// Set this to ensure that bars are not sized thicker than this.
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MaxBarThickness { get; set; }

    /// <summary>
    /// Set this to ensure that bars have a minimum length in pixels.
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MinBarLength { get; set; }

    //PointStyle
    //https://www.chartjs.org/docs/latest/configuration/elements.html#point-styles

    /// <summary>
    /// If true, null or undefined values will not be used for spacing calculations when determining bar size.
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="false"/>.
    /// </remarks>
    public bool SkipNull { get; set; }

    //Stack
    //https://www.chartjs.org/docs/latest/charts/bar.html#general

    /// <summary>
    /// The ID of the x axis to plot this dataset on.
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? XAxisID { get; set; }

    /// <summary>
    /// The ID of the y axis to plot this dataset on.
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? YAxisID { get; set; }

    #endregion
}

public class BarChartDatasetDataLabels
{
    #region Fields and Constants

    private Alignment alignment;

    private Anchor anchor;

    #endregion

    #region Properties, Indexers
    
    /// <summary>
    /// Gets or sets the data labels alignment. 
    /// </summary>
    /// <remarks>
    /// Default value is <see cref="Alignment.Center"/>.
    /// </remarks>
    [JsonIgnore]
    public Alignment Alignment
    {
        get => alignment;
        set
        {
            alignment = value;
            DataLabelsAlignment = value.ToAlignmentString();
        }
    }

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

    /// <summary>
    /// Gets or sets the data labels alignment.
    /// Possible values: start, center, and end.
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    [JsonPropertyName("align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DataLabelsAlignment { get; private set; }

    /// <summary>
    /// Gets or sets the data labels anchor.
    /// Possible values: start, center, and end.
    /// </summary>
    /// <remarks>
    /// Default value is <see langword="null"/>.
    /// </remarks>
    [JsonPropertyName("anchor")]
    public string? DataLabelsAnchor { get; private set; }

    #endregion
}
