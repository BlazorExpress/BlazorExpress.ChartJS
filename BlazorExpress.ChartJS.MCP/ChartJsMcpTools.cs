using System.ComponentModel;
using ModelContextProtocol.Server;

namespace BlazorExpress.ChartJS.MCP;

[McpServerToolType]
public static class ChartJsMcpTools
{
    [McpServerTool(Name = "list_chart_types")]
    [Description("Lists chart types supported by BlazorExpress.ChartJS code generation.")]
    public static string ListChartTypes() =>
        Json.Serialize(ChartCatalog.All.Select(x => new
        {
            x.Name,
            x.ComponentName,
            x.OptionsTypeName,
            x.DatasetTypeName,
            x.SupportsDatalabels,
            x.SupportsStacking,
            x.SupportsOrientation,
        }));

    [McpServerTool(Name = "get_chart_generation_schema")]
    [Description("Returns the code-generation input schema and local API metadata for a BlazorExpress.ChartJS chart type.")]
    public static string GetChartGenerationSchema(
        [Description("Chart type, for example Bar, Line, Pie, Doughnut, Bubble, Scatter, Radar, or PolarArea.")]
        string chartType) =>
        Json.Serialize(ChartCatalog.GetSchema(chartType));

    [McpServerTool(Name = "generate_chart_example")]
    [Description("Generates a complete ready-to-paste Razor example for a BlazorExpress.ChartJS chart. labelsJson is a JSON string array. datasetsJson is a JSON array of { label, data, points, backgroundColor, borderColor, stack }.")]
    public static string GenerateChartExample(
        string chartType,
        string? title = null,
        string? route = null,
        string? pageName = null,
        string? labelsJson = null,
        string? datasetsJson = null,
        int? width = null,
        int? height = null,
        string? legendPosition = null,
        bool? datalabels = null,
        bool? stacked = null,
        string? orientation = null)
    {
        var request = CreateChartRequest(chartType, title, route, pageName, labelsJson, datasetsJson, width, height, legendPosition, datalabels, stacked, orientation);
        var generator = new ChartExampleGenerator();
        return Json.Serialize(generator.Generate(request));
    }

    [McpServerTool(Name = "preview_project_integration")]
    [Description("Creates a non-mutating preview plan for adding a generated chart page and required BlazorExpress.ChartJS setup to a target Blazor project.")]
    public static string PreviewProjectIntegration(
        string targetProjectPath,
        string chartType,
        string? title = null,
        string? route = null,
        string? pageName = null,
        string? labelsJson = null,
        string? datasetsJson = null,
        int? width = null,
        int? height = null,
        string? legendPosition = null,
        bool? datalabels = null,
        bool? stacked = null,
        string? orientation = null)
    {
        var request = new PreviewIntegrationRequest
        {
            TargetProjectPath = targetProjectPath,
            Chart = CreateChartRequest(chartType, title, route, pageName, labelsJson, datasetsJson, width, height, legendPosition, datalabels, stacked, orientation),
        };

        var service = new ProjectIntegrationService(new ChartExampleGenerator());
        return Json.Serialize(service.Preview(request));
    }

    [McpServerTool(Name = "apply_project_integration")]
    [Description("Applies a preview_project_integration plan after validating its plan hash and current file hashes.")]
    public static string ApplyProjectIntegration(
        [Description("The full JSON plan returned by preview_project_integration.")]
        string integrationPlanJson)
    {
        var plan = Json.Deserialize<IntegrationPlan>(integrationPlanJson)
            ?? throw new ArgumentException("integrationPlanJson must contain a serialized integration plan.", nameof(integrationPlanJson));

        var service = new ProjectIntegrationService(new ChartExampleGenerator());
        return Json.Serialize(service.Apply(plan));
    }

    private static ChartGenerationRequest CreateChartRequest(
        string chartType,
        string? title,
        string? route,
        string? pageName,
        string? labelsJson,
        string? datasetsJson,
        int? width,
        int? height,
        string? legendPosition,
        bool? datalabels,
        bool? stacked,
        string? orientation) =>
        new()
        {
            ChartType = chartType,
            Title = title,
            Route = route,
            PageName = pageName,
            Labels = Json.Deserialize<List<string>>(labelsJson),
            Datasets = Json.Deserialize<List<ChartDatasetRequest>>(datasetsJson),
            Width = width,
            Height = height,
            LegendPosition = legendPosition,
            Datalabels = datalabels,
            Stacked = stacked,
            Orientation = orientation,
        };
}
