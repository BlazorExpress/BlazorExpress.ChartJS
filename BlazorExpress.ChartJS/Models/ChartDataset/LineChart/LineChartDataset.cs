using System.Diagnostics.Metrics;
using System.Reflection;

namespace BlazorExpress.ChartJS;

/// <summary>
/// A line chart is a way of plotting data points on a line. 
/// Often, it is used to show trend data, or the comparison of two data sets.
/// <see href="https://www.chartjs.org/docs/latest/charts/line.html#dataset-properties" />.
/// </summary>
public class LineChartDataset : ChartDataset<double?>
{
    #region Methods

    /// <summary>
    /// Fill between this dataset and the other dataset, specified by absolute index (zero based) or relative index.
    /// </summary>
    /// <param name="index">The index of the dataset to fill to</param>
    /// <param name="relativeIndex">Whether the specified index is relative or absolute (zero based)</param>
    /// <returns>The dataset, for method chaining</returns>
    /// <exception cref="ArgumentException">If the relative index is zero.</exception>
    public LineChartDataset FillToDataset(int index, bool relativeIndex = false)
    {
        if (relativeIndex && index == 0)
            throw new ArgumentException("The relative index must be non-zero.");

        Fill = relativeIndex ? index.ToString("+0;-0", CultureInfo.InvariantCulture) : index;

        return this;
    }

    /// <summary>
    /// Fill between this dataset and the other dataset, specified by passing a dataset in the same chart.
    /// </summary>
    /// <param name="chartData">The chart data of the chart both datasets live in.</param>
    /// <param name="dataset">The other dataset to fill to.</param>
    /// <param name="relativeIndex">Whether to specify the fill index relative ("+/-n" string) or absolute (as zero-based int)</param>
    /// <returns>The dataset, for method chaining</returns>
    /// <exception cref="ArgumentException">If any of the datasets is not in the chart data, or if both datasets are the same.</exception>
    public LineChartDataset FillToDataset(ChartData chartData, IChartDataset dataset, bool relativeIndex = false)
    {
        var index = chartData?.Datasets?.IndexOf(dataset) ?? -1;

        if (index < 0)
            throw new ArgumentException("The dataset is not in the chart data.");

        if (relativeIndex)
        {
            var myIndex = relativeIndex ? chartData.Datasets.IndexOf(this) : 0;

            if (myIndex < 0)
                throw new ArgumentException("The dataset is not in the chart data.");

            if (myIndex == index)
                throw new ArgumentException("The dataset is the same as this dataset.");

            Fill = (index - myIndex).ToString("+0;-0", CultureInfo.InvariantCulture);
        }
        else
        {
            Fill = index;
        }

        return this;
    }

    /// <summary>
    /// Fills between the current dataset and the top of the chart (fill: 'end').
    /// </summary>
    /// <returns>The dataset, for method chaining</returns>
    public LineChartDataset FillToEnd()
    {
        Fill = "end";

        return this;
    }

    /// <summary>
    /// Fills between the current dataset and the origin. For legacy reasons, this is the same as fill: true.
    /// </summary>
    /// <returns>The dataset, for method chaining</returns>
    public LineChartDataset FillToOrigin()
    {
        Fill = "origin";

        return this;
    }

    /// <summary>
    /// Fill to the line below the current dataset (fill: 'stack').
    /// </summary>
    /// <returns>The dataset, for method chaining</returns>
    public LineChartDataset FillToStackedValueBelow()
    {
        Fill = "stack";

        return this;
    }

    /// <summary>
    /// Fills between the current dataset and the start (fill: 'start').
    /// </summary>
    /// <returns>The dataset, for method chaining</returns>
    public LineChartDataset FillToStart()
    {
        Fill = "start";

        return this;
    }

    /// <summary>
    /// Fill to the line of the given constant value.
    /// </summary>
    /// <param name="value">The value to fill to</param>
    /// <returns>The dataset, for method chaining</returns>
    public LineChartDataset FillToValue(double value)
    {
        Fill = new { value };

        return this;
    }

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// The line fill color.
    /// <para>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("The line fill color.")]
    public string BackgroundColor { get; set; } = "rgba(0, 0, 0, 0.1)";

    /// <summary>
    /// Cap style of the line.
    /// Supported values are 'butt', 'round', and 'square'.
    /// <para>
    /// Default value is 'butt'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("butt")]
    [Description("Cap style of the line. Supported values are 'butt', 'round', and 'square'.")]
    public string BorderCapStyle { get; set; } = "butt";

    /// <summary>
    /// The line color.
    /// <para>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("The line color.")]
    public string BorderColor { get; set; } = "rgba(0, 0, 0, 0.1)";

    /// <summary>
    /// Length and spacing of dashes.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Length and spacing of dashes.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? BorderDash { get; set; }

    /// <summary>
    /// Offset for line dashes.
    /// <para>
    /// Default value is 0.0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0.0)]
    [Description("Offset for line dashes.")]
    public double BorderDashOffset { get; set; }

    /// <summary>
    /// Line joint style. 
    /// There are three possible values for this property: 'round', 'bevel', and 'miter'.
    /// <para>
    /// Default value is 'miter'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("miter")]
    [Description("Line joint style. There are three possible values for this property: 'round', 'bevel', and 'miter'.")]
    public string BorderJoinStyle { get; set; } = "miter";

    /// <summary>
    /// The line width (in pixels).
    /// <para>
    /// Default value is 3.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(3)]
    [Description("The line width (in pixels).")]
    public double BorderWidth { get; set; } = 3;

    //clip
    //https://www.chartjs.org/docs/latest/charts/line.html#general

    /// <summary>
    /// Supported values are 'default', and 'monotone'.
    /// <para>
    /// Default value is 'default'.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#cubicinterpolationmode" />.
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("default")]
    [Description("Supported values are 'default', and 'monotone'.")]
    public string CubicInterpolationMode { get; set; } = "default";

    /// <summary>
    /// Gets or sets the data labels configuration for the line chart dataset.
    /// <para>
    /// Use this property to customize the display of data labels, such as their position,
    /// formatting, and visibility, for the dataset in the line chart.
    /// </para>
    /// <see href="https://chartjs-plugin-datalabels.netlify.app/guide/getting-started.html#configuration" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("new()")]
    [Description("Gets or sets the data labels configuration for the line chart dataset. Use this property to customize the display of data labels, such as their position, formatting, and visibility, for the dataset in the line chart.")]
    [ParameterTypeName("LineChartDatasetDataLabels")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public LineChartDatasetDataLabels Datalabels { get; set; } = new();

    /// <summary>
    /// Draw the active points of a dataset over the other points of the dataset.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Draw the active points of a dataset over the other points of the dataset.")]
    [ParameterTypeName("List<bool>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<bool>? DrawActiveElementsOnTop { get; set; }

    /// <summary>
    /// How to fill the area under the line.
    /// <para>
    /// Default value is <see langword="false"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("How to fill the area under the line.")]
    public object Fill { get; set; } = false;

    /// <summary>
    /// The line fill color when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The line fill color when hovered.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HoverBackgroundColor { get; set; }

    /// <summary>
    /// Cap style of the line when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Cap style of the line when hovered.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HoverBorderCapStyle { get; set; }

    /// <summary>
    /// The line color when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The line color when hovered.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HoverBorderColor { get; set; }

    /// <summary>
    /// Length and spacing of dashes when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Length and spacing of dashes when hovered.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? HoverBorderDash { get; set; }

    /// <summary>
    /// Offset for line dashes when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Offset for line dashes when hovered.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? HoverBorderDashOffset { get; set; }

    /// <summary>
    /// Line joint style when hovered.
    /// There are three possible values for this property: 'round', 'bevel', and 'miter'.
    /// <para>
    /// Default value is 'miter'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("miter")]
    [Description("Line joint style. There are three possible values for this property: 'round', 'bevel', and 'miter'.")]
    public string HoverBorderJoinStyle { get; set; } = "miter";

    /// <summary>
    /// The line width (in pixels) when hovered.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("The line width (in pixels) when hovered.")]
    [ParameterTypeName("double?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? HoverBorderWidth { get; set; }

    /// <summary>
    /// The base axis of the dataset. 'x' for horizontal lines and 'y' for vertical lines.
    /// <para>
    /// Default value is 'x'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("x")]
    [Description("The base axis of the dataset. 'x' for horizontal lines and 'y' for vertical lines.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IndexAxis { get; set; } = "x";

    /// <summary>
    /// The fill color for points.
    /// <para>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("The fill color for points.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? PointBackgroundColor { get; set; }

    /// <summary>
    /// The border color for points.
    /// <para>
    /// Default value is 'rgba(0, 0, 0, 0.1)'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("rgba(0, 0, 0, 0.1)")]
    [Description("The border color for points.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? PointBorderColor { get; set; }

    /// <summary>
    /// The width of the point border in pixels.
    /// <para>
    /// Default value is 1.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(1)]
    [Description("The width of the point border in pixels.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? PointBorderWidth { get; set; }

    /// <summary>
    /// The pixel size of the non-displayed point that reacts to mouse events.
    /// <para>
    /// Default value is 1.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(1)]
    [Description("The pixel size of the non-displayed point that reacts to mouse events.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? PointHitRadius { get; set; }

    /// <summary>
    /// Point background color when hovered.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Point background color when hovered.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? PointHoverBackgroundColor { get; set; }

    /// <summary>
    /// Point border color when hovered.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Point border color when hovered.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? PointHoverBorderColor { get; set; }

    /// <summary>
    /// Border width of point when hovered.
    /// <para>
    /// Default value is 1.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(1)]
    [Description("Border width of point when hovered.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? PointHoverBorderWidth { get; set; }

    /// <summary>
    /// The radius of the point when hovered.
    /// <para>
    /// Default value is 4.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(4)]
    [Description("The radius of the point when hovered.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? PointHoverRadius { get; set; }

    /// <summary>
    /// The radius of the point shape. If set to 0, the point is not rendered.
    /// <para>
    /// Default value is 3.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(3)]
    [Description("The radius of the point shape. If set to 0, the point is not rendered.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? PointRadius { get; set; }

    /// <summary>
    /// The rotation of the point in degrees.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("The rotation of the point in degrees.")]
    [ParameterTypeName("List<double>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<double>? PointRotation { get; set; }

    /// <summary>
    /// Style of the point.
    /// Supported values are 'circle', 'cross', 'crossRot', 'dash', 'line', 'rect', 'rectRounded', 'rectRot', 'star', and 'triangle'.
    /// <para>
    /// Default value is 'circle'.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/configuration/elements.html#point-styles" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("circle")]
    [Description("Style of the point. Supported values are 'circle', 'cross', 'crossRot', 'dash', 'line', 'rect', 'rectRounded', 'rectRot', 'star', and 'triangle'.")]
    [ParameterTypeName("List<string>?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? PointStyle { get; set; }

    //segment
    //https://www.chartjs.org/docs/latest/charts/line.html#segment

    /// <summary>
    /// If <see langword="false" />, the line is not drawn for this dataset.
    /// <para>
    /// Default value is <see langword="true"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(true)]
    [Description("If false, the line is not drawn for this dataset.")]
    public bool ShowLine { get; set; } = true;

    /// <summary>
    /// If <see langword="true" />, lines will be drawn between points with no or null data.
    /// If <see langword="false" />, points with null data will create a break in the line.
    /// Can also be a number specifying the maximum gap length to span.
    /// The unit of the value depends on the scale used.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("If true, lines will be drawn between points with no or null data. If false, points with null data will create a break in the line. Can also be a number specifying the maximum gap length to span. The unit of the value depends on the scale used.")]
    [ParameterTypeName("bool?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? SpanGaps { get; set; }

    //stack
    //https://www.chartjs.org/docs/latest/charts/line.html#general

    /// <summary>
    /// If the stepped value is set to anything other than <see langword="false"/>, tension will be ignored.
    /// The following values are supported for stepped.
    /// - false: No Step Interpolation(default)
    /// - true: Step-before Interpolation(eq. 'before')
    /// - 'before': Step-before Interpolation
    /// - 'after': Step-after Interpolation
    /// - 'middle': Step-middle Interpolation
    /// <para>
    /// Default value is <see langword="false"/>.
    /// </para>
    /// <see href="https://www.chartjs.org/docs/latest/charts/line.html#stepped" />
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(false)]
    [Description("If the stepped value is set to anything other than false, tension will be ignored. <br /> <div>The following values are supported for stepped. <ol class=\"pl-4\"><li><b>false</b>: No Step Interpolation(default)</li><li><b>true</b>: Step-before Interpolation(eq. 'before')</li><li><b>'before'</b>: Step-before Interpolation</li><li><b>'after'</b>: Step-after Interpolation</li><li><b>'middle'</b>: Step-middle Interpolation</ol></div>")]
    public object Stepped { get; set; } = false;

    /// <summary>
    /// Bezier curve tension of the line. Set to 0 to draw straight lines.
    /// This option is ignored if monotone cubic interpolation is used.
    /// <para>
    /// Default value is 0.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(0)]
    [Description("Bezier curve tension of the line. Set to 0 to draw straightlines. This option is ignored if monotone cubic interpolation is used.")]
    public double Tension { get; set; }

    /// <summary>
    /// The ID of the x axis to plot this dataset on.
    /// <para>
    /// Default value is 'first x axis'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("Default value is 'first x axis'.")]
    [Description("The ID of the x-axis to plot this dataset on. Default value is 'first x axis'.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? XAxisID { get; set; }

    /// <summary>
    /// The ID of the y axis to plot this dataset on.
    /// <para>
    /// Default value is 'first y axis'.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue("Default value is 'first y axis'.")]
    [Description("The ID of the y-axis to plot this dataset on. Default value is 'first y axis'.")]
    [ParameterTypeName("string?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? YAxisID { get; set; }

    #endregion
}
