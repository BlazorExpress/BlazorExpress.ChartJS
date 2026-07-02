using System.ComponentModel;
using System.Reflection;

namespace BlazorExpress.ChartJS.MCP;

public static class ChartCatalog
{
    private static readonly IReadOnlyDictionary<string, ChartDefinition> DefinitionsByKey = new Dictionary<string, ChartDefinition>(StringComparer.OrdinalIgnoreCase)
    {
        ["bar"] = new("Bar", "BarChart", "BarChartOptions", "BarChartDataset", typeof(BarChart), typeof(BarChartOptions), typeof(BarChartDataset), true, true, true),
        ["bubble"] = new("Bubble", "BubbleChart", "BubbleChartOptions", "BubbleChartDataset", typeof(BubbleChart), typeof(BubbleChartOptions), typeof(BubbleChartDataset), true, false, false),
        ["doughnut"] = new("Doughnut", "DoughnutChart", "DoughnutChartOptions", "DoughnutChartDataset", typeof(DoughnutChart), typeof(DoughnutChartOptions), typeof(DoughnutChartDataset), true, false, false),
        ["line"] = new("Line", "LineChart", "LineChartOptions", "LineChartDataset", typeof(LineChart), typeof(LineChartOptions), typeof(LineChartDataset), true, true, false),
        ["pie"] = new("Pie", "PieChart", "PieChartOptions", "PieChartDataset", typeof(PieChart), typeof(PieChartOptions), typeof(PieChartDataset), true, false, false),
        ["polararea"] = new("PolarArea", "PolarAreaChart", "PolarAreaChartOptions", "PolarAreaChartDataset", typeof(PolarAreaChart), typeof(PolarAreaChartOptions), typeof(PolarAreaChartDataset), true, false, false),
        ["polar-area"] = new("PolarArea", "PolarAreaChart", "PolarAreaChartOptions", "PolarAreaChartDataset", typeof(PolarAreaChart), typeof(PolarAreaChartOptions), typeof(PolarAreaChartDataset), true, false, false),
        ["radar"] = new("Radar", "RadarChart", "RadarChartOptions", "RadarChartDataset", typeof(RadarChart), typeof(RadarChartOptions), typeof(RadarChartDataset), true, false, false),
        ["scatter"] = new("Scatter", "ScatterChart", "ScatterChartOptions", "ScatterChartDataset", typeof(ScatterChart), typeof(ScatterChartOptions), typeof(ScatterChartDataset), true, false, false),
    };

    public static IReadOnlyList<ChartDefinition> All { get; } = DefinitionsByKey.Values
        .GroupBy(x => x.Name, StringComparer.OrdinalIgnoreCase)
        .Select(x => x.First())
        .OrderBy(x => x.Name)
        .ToList();

    public static ChartDefinition Get(string chartType)
    {
        var key = NormalizeKey(chartType);

        if (DefinitionsByKey.TryGetValue(key, out var definition))
            return definition;

        throw new ArgumentException($"Unsupported chart type '{chartType}'. Supported values are: {string.Join(", ", All.Select(x => x.Name))}.", nameof(chartType));
    }

    public static ChartGenerationSchema GetSchema(string chartType)
    {
        var definition = Get(chartType);

        return new ChartGenerationSchema(
            ChartType: definition.Name,
            Component: definition.ComponentName,
            OptionsType: definition.OptionsTypeName,
            DatasetType: definition.DatasetTypeName,
            SupportsDatalabels: definition.SupportsDatalabels,
            SupportsStacking: definition.SupportsStacking,
            SupportsOrientation: definition.SupportsOrientation,
            CommonInputs:
            [
                "title",
                "route",
                "pageName",
                "labelsJson",
                "datasetsJson",
                "width",
                "height",
                "legendPosition",
                "datalabels"
            ],
            ChartSpecificInputs: GetChartSpecificInputs(definition),
            Metadata: GetTypeMetadata(definition));
    }

    private static IReadOnlyList<string> GetChartSpecificInputs(ChartDefinition definition)
    {
        var inputs = new List<string>();

        if (definition.SupportsStacking)
            inputs.Add("stacked");

        if (definition.SupportsOrientation)
            inputs.Add("orientation: 'vertical' or 'horizontal'");

        if (definition.Name is "Bubble")
            inputs.Add("datasetsJson data points may include x, y, r");
        else if (definition.Name is "Scatter")
            inputs.Add("datasetsJson data points may include x, y");
        else
            inputs.Add("datasetsJson data values are numeric");

        return inputs;
    }

    private static IReadOnlyDictionary<string, object?> GetTypeMetadata(ChartDefinition definition) =>
        new Dictionary<string, object?>
        {
            ["componentDescription"] = GetDescription(definition.ComponentType),
            ["optionsProperties"] = GetPublicPropertyMetadata(definition.OptionsType),
            ["datasetProperties"] = GetPublicPropertyMetadata(definition.DatasetType),
        };

    private static string? GetDescription(MemberInfo member) =>
        member.GetCustomAttribute<DescriptionAttribute>()?.Description;

    private static IReadOnlyList<PropertyMetadata> GetPublicPropertyMetadata(Type type) =>
        type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.GetMethod is not null)
            .Select(x => new PropertyMetadata(
                Name: x.Name,
                Type: x.PropertyType.Name,
                Description: x.GetCustomAttribute<DescriptionAttribute>()?.Description,
                DefaultValue: x.GetCustomAttribute<DefaultValueAttribute>()?.Value?.ToString()))
            .OrderBy(x => x.Name)
            .ToList();

    private static string NormalizeKey(string value) =>
        value.Replace(" ", "", StringComparison.Ordinal)
            .Replace("_", "", StringComparison.Ordinal)
            .ToLowerInvariant();
}
