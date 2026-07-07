using System.ComponentModel;
using System.Reflection;

namespace BlazorExpress.ChartJS.MCP;

public static class ChartCatalog
{
    private static readonly IReadOnlyDictionary<string, ChartDefinition> DefinitionsByKey = new Dictionary<string, ChartDefinition>(StringComparer.OrdinalIgnoreCase)
    {
        ["bar"] = Create("Bar", "BarChart", "BarChartOptions", "BarChartDataset", typeof(BarChart), typeof(BarChartOptions), typeof(BarChartDataset), true, true, true),
        ["bubble"] = Create("Bubble", "BubbleChart", "BubbleChartOptions", "BubbleChartDataset", typeof(BubbleChart), typeof(BubbleChartOptions), typeof(BubbleChartDataset), true, false, false),
        ["doughnut"] = Create("Doughnut", "DoughnutChart", "DoughnutChartOptions", "DoughnutChartDataset", typeof(DoughnutChart), typeof(DoughnutChartOptions), typeof(DoughnutChartDataset), true, false, false),
        ["line"] = Create("Line", "LineChart", "LineChartOptions", "LineChartDataset", typeof(LineChart), typeof(LineChartOptions), typeof(LineChartDataset), true, true, false),
        ["pie"] = Create("Pie", "PieChart", "PieChartOptions", "PieChartDataset", typeof(PieChart), typeof(PieChartOptions), typeof(PieChartDataset), true, false, false),
        ["polararea"] = Create("PolarArea", "PolarAreaChart", "PolarAreaChartOptions", "PolarAreaChartDataset", typeof(PolarAreaChart), typeof(PolarAreaChartOptions), typeof(PolarAreaChartDataset), true, false, false),
        ["polar-area"] = Create("PolarArea", "PolarAreaChart", "PolarAreaChartOptions", "PolarAreaChartDataset", typeof(PolarAreaChart), typeof(PolarAreaChartOptions), typeof(PolarAreaChartDataset), true, false, false),
        ["radar"] = Create("Radar", "RadarChart", "RadarChartOptions", "RadarChartDataset", typeof(RadarChart), typeof(RadarChartOptions), typeof(RadarChartDataset), false, false, false),
        ["scatter"] = Create("Scatter", "ScatterChart", "ScatterChartOptions", "ScatterChartDataset", typeof(ScatterChart), typeof(ScatterChartOptions), typeof(ScatterChartDataset), true, false, false),
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
            SupportsPluginOptions: definition.SupportsPluginOptions,
            SupportsTitleOptions: definition.SupportsTitleOptions,
            SupportsLegendOptions: definition.SupportsLegendOptions,
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
            Examples: GetExamples(definition),
            Metadata: GetTypeMetadata(definition));
    }

    private static ChartDefinition Create(
        string name,
        string componentName,
        string optionsTypeName,
        string datasetTypeName,
        Type componentType,
        Type optionsType,
        Type datasetType,
        bool supportsDatalabels,
        bool supportsStacking,
        bool supportsOrientation)
    {
        var pluginsProperty = optionsType.GetProperty("Plugins", BindingFlags.Public | BindingFlags.Instance);
        var supportsPluginOptions = pluginsProperty is not null;
        var pluginType = pluginsProperty?.PropertyType;
        var supportsTitleOptions = pluginType?.GetProperty("Title", BindingFlags.Public | BindingFlags.Instance) is not null;
        var supportsLegendOptions = pluginType?.GetProperty("Legend", BindingFlags.Public | BindingFlags.Instance) is not null;
        var supportsScales = optionsType.GetProperty("Scales", BindingFlags.Public | BindingFlags.Instance) is not null;

        return new ChartDefinition(
            name,
            componentName,
            optionsTypeName,
            datasetTypeName,
            componentType,
            optionsType,
            datasetType,
            supportsDatalabels,
            supportsStacking,
            supportsOrientation,
            supportsPluginOptions,
            supportsTitleOptions,
            supportsLegendOptions,
            supportsScales);
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

    private static IReadOnlyDictionary<string, object?> GetExamples(ChartDefinition definition)
    {
        var common = new Dictionary<string, object?>
        {
            ["labelsJson"] = """["Jan","Feb","Mar"]""",
            ["numericDatasetsJson"] = """[{"label":"Revenue","data":[12,19,7],"backgroundColor":"rgba(54, 162, 235, 0.7)","borderColor":["rgba(54, 162, 235, 1)"]}]""",
        };

        if (definition.Name is "Scatter" or "Bubble")
            common["pointDatasetsJson"] = """[{"label":"Samples","points":[{"x":1,"y":12,"r":6},{"x":2,"y":19,"r":8}],"backgroundColor":"rgba(255, 99, 132, 0.7)"}]""";

        return common;
    }

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

    private static string NormalizeKey(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("A chart type is required.", nameof(value));

        return
        value.Replace(" ", "", StringComparison.Ordinal)
            .Replace("_", "", StringComparison.Ordinal)
            .ToLowerInvariant();
    }
}
