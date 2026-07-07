using System.ComponentModel;
using ModelContextProtocol.Server;

namespace BlazorExpress.ChartJS.MCP;

[McpServerToolType]
public static class ChartJsMcpTools
{
    [McpServerTool(Name = "list_chart_types")]
    [Description("Lists chart types supported by BlazorExpress.ChartJS code generation.")]
    public static string ListChartTypes() =>
        McpToolExecution.Run(() => ChartCatalog.All.Select(x => new
        {
            x.Name,
            x.ComponentName,
            x.OptionsTypeName,
            x.DatasetTypeName,
            x.SupportsDatalabels,
            x.SupportsStacking,
            x.SupportsOrientation,
            x.SupportsPluginOptions,
            x.SupportsTitleOptions,
            x.SupportsLegendOptions,
        }));

    [McpServerTool(Name = "get_chart_generation_schema")]
    [Description("Returns the code-generation input schema and local API metadata for a BlazorExpress.ChartJS chart type.")]
    public static string GetChartGenerationSchema(
        [Description("Chart type, for example Bar, Line, Pie, Doughnut, Bubble, Scatter, Radar, or PolarArea.")]
        string chartType) =>
        McpToolExecution.Run(() => ChartCatalog.GetSchema(chartType));

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
        => McpToolExecution.Run(() =>
        {
            var request = ChartRequestParser.CreateChartRequest(chartType, title, route, pageName, labelsJson, datasetsJson, width, height, legendPosition, datalabels, stacked, orientation);
            var generator = new ChartExampleGenerator();
            return generator.Generate(request);
        });

    [McpServerTool(Name = "generate_chart_dashboard")]
    [Description("Generates a complete ready-to-paste Razor dashboard page. chartsJson is an optional JSON array of chart specs using chartType, title, labels, datasets, width, height, legendPosition, datalabels, stacked, and orientation.")]
    public static string GenerateChartDashboard(
        string? title = null,
        string? route = null,
        string? pageName = null,
        string? chartsJson = null)
        => McpToolExecution.Run(() =>
        {
            var request = new ChartDashboardRequest
            {
                Title = title,
                Route = route,
                PageName = pageName,
                Charts = ChartRequestParser.ParseDashboardCharts(chartsJson),
            };

            var generator = new ChartExampleGenerator();
            return generator.GenerateDashboard(request);
        });

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
        => McpToolExecution.Run(() =>
        {
            var request = new PreviewIntegrationRequest
            {
                TargetProjectPath = targetProjectPath,
                Chart = ChartRequestParser.CreateChartRequest(chartType, title, route, pageName, labelsJson, datasetsJson, width, height, legendPosition, datalabels, stacked, orientation),
            };

            var service = new ProjectIntegrationService(new ChartExampleGenerator());
            return service.Preview(request);
        });

    [McpServerTool(Name = "preview_dashboard_integration")]
    [Description("Creates a non-mutating preview plan for adding a generated chart dashboard page and required BlazorExpress.ChartJS setup to a target Blazor project.")]
    public static string PreviewDashboardIntegration(
        string targetProjectPath,
        string? title = null,
        string? route = null,
        string? pageName = null,
        string? chartsJson = null)
        => McpToolExecution.Run(() =>
        {
            var request = new PreviewDashboardIntegrationRequest
            {
                TargetProjectPath = targetProjectPath,
                Dashboard = new ChartDashboardRequest
                {
                    Title = title,
                    Route = route,
                    PageName = pageName,
                    Charts = ChartRequestParser.ParseDashboardCharts(chartsJson),
                },
            };

            var service = new ProjectIntegrationService(new ChartExampleGenerator());
            return service.PreviewDashboard(request);
        });

    [McpServerTool(Name = "apply_project_integration")]
    [Description("Applies a preview_project_integration plan after validating its plan hash and current file hashes.")]
    public static string ApplyProjectIntegration(
        [Description("The full JSON plan returned by preview_project_integration.")]
        string integrationPlanJson)
        => McpToolExecution.Run(() =>
        {
            var plan = Json.Deserialize<IntegrationPlan>(integrationPlanJson)
                ?? throw new ToolInputException("integrationPlanJson must contain a serialized integration plan.", "integrationPlanJson");

            var service = new ProjectIntegrationService(new ChartExampleGenerator());
            return service.Apply(plan);
        });
}
