using System.Text.Json.Serialization;

namespace BlazorExpress.ChartJS.MCP;

public sealed record ChartDefinition(
    string Name,
    string ComponentName,
    string OptionsTypeName,
    string DatasetTypeName,
    Type ComponentType,
    Type OptionsType,
    Type DatasetType,
    bool SupportsDatalabels,
    bool SupportsStacking,
    bool SupportsOrientation,
    bool SupportsPluginOptions,
    bool SupportsTitleOptions,
    bool SupportsLegendOptions,
    bool SupportsScales);

public sealed record ChartGenerationSchema(
    string ChartType,
    string Component,
    string OptionsType,
    string DatasetType,
    bool SupportsDatalabels,
    bool SupportsStacking,
    bool SupportsOrientation,
    bool SupportsPluginOptions,
    bool SupportsTitleOptions,
    bool SupportsLegendOptions,
    IReadOnlyList<string> CommonInputs,
    IReadOnlyList<string> ChartSpecificInputs,
    IReadOnlyDictionary<string, object?> Examples,
    IReadOnlyDictionary<string, object?> Metadata);

public sealed record PropertyMetadata(
    string Name,
    string Type,
    string? Description,
    string? DefaultValue);

public sealed record ChartGenerationRequest
{
    public string ChartType { get; init; } = "Bar";
    public string? Title { get; init; }
    public string? Route { get; init; }
    public string? PageName { get; init; }
    public IReadOnlyList<string>? Labels { get; init; }
    public IReadOnlyList<ChartDatasetRequest>? Datasets { get; init; }
    public int? Width { get; init; }
    public int? Height { get; init; }
    public string? LegendPosition { get; init; }
    public bool? Datalabels { get; init; }
    public bool? Stacked { get; init; }
    public string? Orientation { get; init; }
}

public sealed record ChartDatasetRequest
{
    public string? Label { get; init; }
    public IReadOnlyList<double?>? Data { get; init; }
    public IReadOnlyList<ChartPointRequest>? Points { get; init; }
    public IReadOnlyList<string>? BackgroundColor { get; init; }
    public IReadOnlyList<string>? BorderColor { get; init; }
    public string? Stack { get; init; }
}

public sealed record ChartPointRequest
{
    [JsonPropertyName("x")]
    public double X { get; init; }

    [JsonPropertyName("y")]
    public double Y { get; init; }

    [JsonPropertyName("r")]
    public double? R { get; init; }
}

public sealed record GeneratedChartExample(
    string ChartType,
    string Route,
    string PageName,
    string Code,
    IReadOnlyList<string> RequiredScripts);

public sealed record ChartDashboardRequest
{
    public string? Title { get; init; }
    public string? Route { get; init; }
    public string? PageName { get; init; }
    public IReadOnlyList<ChartGenerationRequest>? Charts { get; init; }
}

public sealed record GeneratedChartDashboard(
    string Route,
    string PageName,
    string Code,
    IReadOnlyList<string> ChartTypes,
    IReadOnlyList<string> RequiredScripts);

public sealed record PreviewIntegrationRequest
{
    public string TargetProjectPath { get; init; } = "";
    public ChartGenerationRequest Chart { get; init; } = new();
}

public sealed record PreviewDashboardIntegrationRequest
{
    public string TargetProjectPath { get; init; } = "";
    public ChartDashboardRequest Dashboard { get; init; } = new();
}

public sealed record IntegrationPlan
{
    public string PlanHash { get; init; } = "";
    public string TargetProjectRoot { get; init; } = "";
    public string ProjectFilePath { get; init; } = "";
    public string DetectedHostModel { get; init; } = "";
    public IReadOnlyList<FileEdit> Edits { get; init; } = [];
    public IReadOnlyList<string> ManualSteps { get; init; } = [];
}

public sealed record FileEdit
{
    public string Path { get; init; } = "";
    public string Operation { get; init; } = "";
    public string? OriginalHash { get; init; }
    public string NewContent { get; init; } = "";
    public string Description { get; init; } = "";
}

public sealed record ApplyIntegrationResult(
    string PlanHash,
    IReadOnlyList<string> WrittenFiles,
    IReadOnlyList<string> ManualSteps);

public sealed record ToolFailure(bool Success, ToolError Error);

public sealed record ToolError(
    string Code,
    string Message,
    string? Field = null,
    IReadOnlyList<string>? Details = null);

public sealed class ToolInputException : Exception
{
    public ToolInputException(string message, string? field = null, IReadOnlyList<string>? details = null)
        : base(message)
    {
        Field = field;
        Details = details ?? [];
    }

    public string? Field { get; }

    public IReadOnlyList<string> Details { get; }
}
