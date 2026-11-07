namespace BlazorExpress.ChartJS;

/// <summary>
/// The options of the built-in colors plugin, which gives datasets that set no colors a default palette. Datasets that set colors keep them unless ForceOverride is true.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/general/colors.html#default-color-palette" /> for more information.
/// </para>
/// </summary>
public class ChartPluginsColors
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets a value indicating whether the colors plugin is on.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses true).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the colors plugin is on.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? Enabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the palette replaces colors the datasets set.
    /// <para>
    /// Default value is <see langword="null" /> (Chart.js uses false).
    /// </para>
    /// </summary>
    [AddedVersion("1.3.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether the palette replaces colors the datasets set.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? ForceOverride { get; set; }

    #endregion
}
