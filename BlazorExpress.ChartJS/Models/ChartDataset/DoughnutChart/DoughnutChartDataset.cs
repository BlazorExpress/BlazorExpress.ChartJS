namespace BlazorExpress.ChartJS;

/// <summary>
/// The doughnut/pie chart allows a number of properties to be specified for each dataset. 
/// These are used to set display properties for a specific dataset.
/// <see href="https://www.chartjs.org/docs/latest/charts/doughnut.html#dataset-properties" />.
/// </summary>
public class DoughnutChartDataset : ChartDataset<double?>
{
    #region Properties, Indexers

    /// <summary>
    /// Arc background color.
    /// <para>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("Arc background color.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? BackgroundColor { get; set; }

    /// <summary>
    /// Supported values are 'center' and 'inner'.
    /// When 'center' is set, the borders of arcs next to each other will overlap. 
    /// When 'inner' is set, it is guaranteed that all borders will not overlap.
    /// <para>
    /// Default value is 'center'.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/doughnut.html#border-alignment" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("center")]
    [Description("Supported values are 'center' and 'inner'. When 'center' is set, the borders of arcs next to each other will overlap. When 'inner' is set, it is guaranteed that all borders will not overlap.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? BorderAlign { get; set; } // TODO: change this to enum

    /// <summary>
    /// Arc border color.
    /// <para>
    /// Default value is '#fff'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("#fff")]
    [Description("Arc border color.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? BorderColor { get; set; }

    /// <summary>
    /// Arc border length and spacing of dashes.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border length and spacing of dashes.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? BorderDash { get; set; }

    /// <summary>
    /// Arc border offset for line dashes.
    /// <para>
    /// Default value is 0.0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("0.0")]
    [Description("Arc border offset for line dashes.")]
    //[ParameterTypeName("")]
    public double BorderDashOffset { get; set; }

    /// <summary>
    /// Arc border join style.
    /// Supported values are 'round', 'bevel', 'miter'.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border join style. Supported values are 'round', 'bevel', 'miter'.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? BorderJoinStyle { get; set; } // TODO: change this to enum

    /// <summary>
    /// It is applied to all corners of the arc (outerStart, outerEnd, innerStart, innerRight).
    /// <para>
    /// Default value is 0.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/doughnut.html#border-radius" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("It is applied to all corners of the arc (outerStart, outerEnd, innerStart, innerRight).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? BorderRadius { get; set; }

    /// <summary>
    /// Arc border width (in pixels).
    /// <para>
    /// Default value is 2.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(2)]
    [Description("Arc border width (in pixels).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? BorderWidth { get; set; }

    /// <summary>
    /// Per-dataset override for the sweep that the arcs cover.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Per-dataset override for the sweep that the arcs cover.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Circumference { get; set; }

    //Clip
    //https://www.chartjs.org/docs/latest/charts/doughnut.html#general

    /// <summary>
    /// Gets or sets the data labels configuration for the doughnut chart dataset.
    /// <para>
    /// Use this property to customize the display of data labels, such as their font, color,
    /// alignment, and visibility, for the doughnut chart dataset.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the data labels configuration for the doughnut chart dataset. Use this property to customize the display of data labels, such as their font, color, alignment, and visibility, for the doughnut chart dataset.")]
    [ParameterTypeName("DoughnutChartDatasetDataLabels")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public DoughnutChartDatasetDataLabels Datalabels { get; set; } = new();

    /// <summary>
    /// Arc background color when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc background color when hovered.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? HoverBackgroundColor { get; set; }

    /// <summary>
    /// Arc border color when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border color when hovered.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? HoverBorderColor { get; set; }

    /// <summary>
    /// Arc border length and spacing of dashes when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border length and spacing of dashes when hovered.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderDash { get; set; }

    /// <summary>
    /// Arc border offset for line dashes when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border offset for line dashes when hovered.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? HoverBorderDashOffset { get; set; }

    /// <summary>
    /// Arc border join style when hovered.
    /// Supported values are 'round', 'bevel', 'miter'.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border join style when hovered. Supported values are 'round', 'bevel', 'miter'.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? HoverBorderJoinStyle { get; set; } // TODO: change this to enum

    /// <summary>
    /// Arc border width when hovered (in pixels).
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Arc border width when hovered (in pixels).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderWidth { get; set; }

    /// <summary>
    /// Arc offset when hovered (in pixels).
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("Arc offset when hovered (in pixels).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverOffset { get; set; }

    /// <summary>
    /// Arc offset (in pixels).
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("Arc offset (in pixels).")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? Offset { get; set; }

    /// <summary>
    /// Per-dataset override for the starting angle to draw arcs from.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Per-dataset override for the starting angle to draw arcs from.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Rotation { get; set; }

    /// <summary>
    /// Fixed arc offset (in pixels). Similar to <see cref="Offset" /> but applies to all arcs.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("Fixed arc offset (in pixels). Similar to <code>Offset</code> but applies to all arcs.")]
    public double Spacing { get; set; }

    /// <summary>
    /// The relative thickness of the dataset. 
    /// Providing a value for weight will cause the pie or doughnut dataset to be drawn 
    /// with a thickness relative to the sum of all the dataset weight values.
    /// <para>
    /// Default value is 1.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(1)]
    [Description("The relative thickness of the dataset. Providing a value for weight will cause the pie or doughnut dataset to be drawn with a thickness relative to the sum of all the dataset weight values.")]
    public double Weight { get; set; } = 1;

    #endregion
}
