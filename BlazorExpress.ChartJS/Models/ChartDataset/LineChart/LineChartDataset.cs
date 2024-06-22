﻿namespace BlazorExpress.ChartJS;

public class LineChartDataset : ChartDataset
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
    /// Line dash.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? BorderDash { get; set; }

    /// <summary>
    /// Line dash offset.
    /// </summary>
    public double BorderDashOffset { get; set; }

    /// <summary>
    /// Gets or sets the border width (in pixels).
    /// </summary>
    /// <remarks>
    /// Default value is 0.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? BorderWidth { get; set; }



    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public LineChartDatasetDataLabels Datalabels { get; set; } = new();

    /// <summary>
    /// Both line and radar charts support a fill option on the dataset object
    /// which can be used to create area between two datasets or a dataset and
    /// a boundary, i.e. the scale origin, start or end.
    /// </summary>
    public bool Fill { get; set; }

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
    /// Hover line dash.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? HoverBorderDash { get; set; }

    /// <summary>
    /// The bar border width when hovered (in pixels).
    /// </summary>
    /// <remarks>
    /// Default value is 1.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderWidth { get; set; }

    /// <summary>
    /// The fill color for points.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string> PointBackgroundColor { get; set; } = new() { "rgba(0, 0, 0, 0.1)" };

    /// <summary>
    /// The border color for points.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string> PointBorderColor { get; set; } = new() { "rgba(0, 0, 0, 0.1)" };

    /// <summary>
    /// The width of the point border in pixels.
    /// </summary>
    public List<double> PointBorderWidth { get; set; } = new() { 1 };

    /// <summary>
    /// The pixel size of the non-displayed point that reacts to mouse events.
    /// </summary>
    public List<double> PointHitRadius { get; set; } = new() { 1 };

    /// <summary>
    /// Point background color when hovered.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? PointHoverBackgroundColor { get; set; }

    /// <summary>
    /// Point border color when hovered.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? PointHoverBorderColor { get; set; }

    /// <summary>
    /// Border width of point when hovered.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double> PointHoverBorderWidth { get; set; } = new() { 1 };

    /// <summary>
    /// The radius of the point when hovered.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int> PointHoverRadius { get; set; } = new() { 1 }; // Default: 4

    /// <summary>
    /// The radius of the point shape. If set to 0, the point is not rendered.
    /// Default: 3
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int> PointRadius { get; set; } = new() { 1 }; // Default: 3

    /// <summary>
    /// The rotation of the point in degrees.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int> PointRotation { get; set; } = new() { 0 };

    /// <summary>
    /// Style of the point.
    /// Use 'circle', 'cross', 'crossRot', 'dash', 'line', 'rect', 'rectRounded', 'rectRot', 'star', and 'triangle' to style
    /// the point.
    /// </summary>
    public List<string> PointStyle { get; set; } = new() { "circle" };

    // Segment
    // https://www.chartjs.org/docs/latest/api/interfaces/LineControllerDatasetOptions.html#segment

    /// <summary>
    /// If <see langword="false" />, the lines between points are not drawn.
    /// </summary>
    public bool ShowLine { get; set; } = true;

    /// <summary>
    /// If <see langword="true" />, lines will be drawn between points with no or null data.
    /// If <see langword="false" />, points with null data will create a break in the line.
    /// Can also be a number specifying the maximum gap length to span.
    /// The unit of the value depends on the scale used.
    /// </summary>
    public bool SpanGaps { get; set; }

    /// <summary>
    /// true to show the line as a stepped line (tension will be ignored).
    /// </summary>
    public bool Stepped { get; set; }

    /// <summary>
    /// Bezier curve tension of the line. Set to 0 to draw straight lines.
    /// This option is ignored if monotone cubic interpolation is used.
    /// </summary>
    public double Tension { get; set; } = 0.2;

    /// <summary>
    /// The ID of the x axis to plot this dataset on.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? XAxisID { get; set; }

    /// <summary>
    /// The ID of the y axis to plot this dataset on.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? YAxisID { get; set; }

    #endregion
}

public class LineChartDatasetDataLabels
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
    [JsonPropertyName("align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DataLabelsAlignment { get; private set; }

    /// <summary>
    /// Gets or sets the data labels anchor.
    /// Possible values: start, center, and end.
    /// </summary>
    [JsonPropertyName("anchor")]
    public string? DataLabelsAnchor { get; private set; }

    #endregion
}