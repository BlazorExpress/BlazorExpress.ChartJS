namespace BlazorExpress.ChartJS;

/// <summary>
/// Highly customizable Chart.js plugin that displays labels on data for any type of charts.
/// <see href="https://chartjs-plugin-datalabels.netlify.app/guide/options.html#options" />.
/// </summary>
public class ChartDatasetDataLabels
{
    #region Fields and Constants

    private Alignment alignment;

    private Anchor anchor;

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets or sets the data labels alignment. 
    /// <para>
    /// Default value is <see cref="Alignment.Center"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(Alignment.Center)]
    [Description("Gets or sets the data labels alignment.")]
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
    /// <para>
    /// Default value is <see cref="Anchor.None"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(Anchor.None)]
    [Description("Gets or sets the data labels anchor.")]
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

    public double BorderWidth { get; set; } = 2;

    /// <summary>
    /// Gets or sets the data labels alignment.
    /// Possible values: start, center, and end.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the data labels alignment. Possible values: start, center, and end.")]
    [ParameterTypeName("string?")]
    [JsonPropertyName("align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DataLabelsAlignment { get; private set; }

    /// <summary>
    /// Gets or sets the data labels anchor.
    /// Possible values: start, center, and end.
    /// <para>
    /// Default value is <see langword="null"/>.
    /// </para>
    /// </summary>
    [AddedVersion("1.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the data labels anchor. Possible values: start, center, and end.")]
    [ParameterTypeName("string?")]
    [JsonPropertyName("anchor")]
    public string? DataLabelsAnchor { get; private set; }

    #endregion
}
