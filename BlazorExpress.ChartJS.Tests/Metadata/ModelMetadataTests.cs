using System.ComponentModel;
using System.Reflection;

namespace BlazorExpress.ChartJS.Tests.Metadata;

/// <summary>
/// Every public property of the model types in the files 26110203 touched carries the metadata the docs tables read
/// (rule "metadata backfill in touched model files"), and types new in 1.3.0 carry <c>[AddedVersion("1.3.0")]</c>.
/// </summary>
public class ModelMetadataTests
{
    #region Fields and Constants

    private static readonly string[] releasedVersions = ["1.0.0", "1.1.0", "1.2.0", "1.2.1", "1.2.2", "1.2.3", "1.2.4", "1.3.0"];

    private static readonly Type[] newTypes =
    [
        typeof(BarChartDataValue), typeof(ChartFunction), typeof(BubbleChartDataLabels), typeof(BubbleChartDataLabelsFont), typeof(BubbleChartPlugins),
        typeof(ChartAxesTime), typeof(ChartAxesTimeDisplayFormats), typeof(ChartTimeValue), typeof(ChartTimePoint), typeof(BubbleChartTimePoint),
        typeof(ChartAxesAngleLines), typeof(ChartAxesPointLabels), typeof(ChartBorderRadius), typeof(ChartClip), typeof(ChartDimension),
        typeof(ChartElements), typeof(ChartElementsArc), typeof(ChartElementsBar), typeof(ChartElementsLine), typeof(ChartElementsPoint),
        typeof(ChartHover), typeof(ChartPadding), typeof(ChartPluginsColors), typeof(ChartPluginsFiller), typeof(ChartPluginsTooltipLine),
        typeof(ChartRadialAxes), typeof(RadarChartDataLabels), typeof(RadarChartDataLabelsFont), typeof(RadarChartPlugins), typeof(RadialScales),
        // New in 26110205
        typeof(ChartLineSegment), typeof(ChartNumberFormat), typeof(ChartPluginsTooltipCallbacks)
    ];

    private static readonly Type[] touchedTypes =
    [
        // Models/ChartOptions
        typeof(ChartOptions), typeof(ChartLayout), typeof(Interaction), typeof(Scales), typeof(ChartAxes), typeof(ChartAxesBorder),
        typeof(ChartAxesGrid), typeof(ChartAxesTicks), typeof(ChartAxesTicksMajor), typeof(ChartAxesTitle), typeof(ChartFont),
        typeof(ChartAnimation), typeof(BubbleChartOptions), typeof(DoughnutChartOptions), typeof(PieChartOptions),
        typeof(PolarAreaChartOptions), typeof(RadarChartOptions),
        // Models/ChartPlugins
        typeof(ChartPlugins), typeof(ChartPluginsLegend), typeof(ChartPluginsLegendTitle), typeof(ChartPluginsLegendLabels),
        typeof(ChartPluginsTitle), typeof(ChartPluginsSubtitle), typeof(ChartPluginsTooltip), typeof(ChartPluginsTooltipFont),
        typeof(BarChartPlugins), typeof(BarChartDataLabels), typeof(BarChartDataLabelsFont),
        typeof(DoughnutChartPlugins), typeof(DoughnutChartDataLabels), typeof(DoughnutChartDataLabelsFont),
        typeof(LineChartPlugins), typeof(LineChartDataLabels), typeof(LineChartDataLabelsFont),
        typeof(PieChartPlugins), typeof(PieChartDataLabels), typeof(PieChartDataLabelsFont),
        typeof(PolarAreaChartPlugins), typeof(PolarAreaChartDataLabels), typeof(PolarAreaChartDataLabelsFont),
        typeof(ScatterChartPlugins), typeof(ScatterChartDataLabels), typeof(ScatterChartDataLabelsFont),
        // Models/ChartDataset
        typeof(ChartDataset<>), typeof(BarChartDataset), typeof(BarChartDatasetData), typeof(DoughnutChartDataset), typeof(LineChartDataset),
        typeof(PieChartDataset), typeof(PolarAreaChartDataset), typeof(RadarChartDataset), typeof(ScatterChartDataset),
        // Touched by 26110204
        typeof(ChartData), typeof(BubbleChartDataset), typeof(BubbleChartDatasetData), typeof(LineChartDatasetData), typeof(ScatterChartDatasetData),
        // Touched by 26110205
        typeof(ChartDatasetDataLabels),
        .. newTypes
    ];

    #endregion

    #region Methods

    #region Public Methods

    [Theory]
    [MemberData(nameof(NewTypeNames))]
    public void NewTypes_AreAddedIn130(string typeName)
    {
        var wrong = PublicProperties(Find(typeName)).Where(p => p.GetCustomAttribute<AddedVersionAttribute>()?.Version != "1.3.0").Select(p => p.Name);

        Assert.Empty(wrong);
    }

    [Theory]
    [MemberData(nameof(TouchedTypeNames))]
    public void TouchedTypes_HaveFullPropertyMetadata(string typeName)
    {
        var missing = PublicProperties(Find(typeName))
            .Where(p => p.GetCustomAttribute<AddedVersionAttribute>() is not { } added || !releasedVersions.Contains(added.Version)
                || p.GetCustomAttribute<DefaultValueAttribute>() is null
                || string.IsNullOrWhiteSpace(p.GetCustomAttribute<DescriptionAttribute>()?.Description))
            .Select(p => p.Name);

        Assert.Empty(missing);
    }

    #endregion

    #region Private Methods

    private static Type Find(string typeName) => touchedTypes.Single(t => t.Name == typeName);

    private static IEnumerable<PropertyInfo> PublicProperties(Type type) =>
        type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

    #endregion

    #endregion

    #region Properties, Indexers

    public static TheoryData<string> NewTypeNames => new(newTypes.Select(t => t.Name));

    public static TheoryData<string> TouchedTypeNames => new(touchedTypes.Select(t => t.Name).Distinct());

    #endregion
}
