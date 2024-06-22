﻿namespace BlazorExpress.ChartJS;

public class PieChartDataset : ChartDataset
{
    #region Properties, Indexers

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public PieChartDatasetDataLabels Datalabels { get; set; } = new();

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
