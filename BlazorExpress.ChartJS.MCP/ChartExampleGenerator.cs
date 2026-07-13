using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BlazorExpress.ChartJS.MCP;

public sealed class ChartExampleGenerator
{
    private static readonly string[] DefaultColors =
    [
        "rgba(54, 162, 235, 0.7)",
        "rgba(255, 99, 132, 0.7)",
        "rgba(255, 205, 86, 0.7)",
        "rgba(75, 192, 192, 0.7)",
        "rgba(153, 102, 255, 0.7)",
        "rgba(255, 159, 64, 0.7)"
    ];

    public GeneratedChartExample Generate(ChartGenerationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var model = CreateChartModel(request, identifierSuffix: "");
        var code = new StringBuilder();

        code.AppendLine($"@page \"{model.Route}\"");
        code.AppendLine("@using BlazorExpress.ChartJS");
        code.AppendLine();
        code.AppendLine($"<h3>{EscapeHtml(model.Title)}</h3>");
        code.AppendLine();
        AppendChartMarkup(code, model, indent: "");
        code.AppendLine();
        code.AppendLine("@code {");
        AppendChartFields(code, model);
        code.AppendLine();
        code.AppendLine("    protected override void OnInitialized()");
        code.AppendLine("    {");
        AppendChartInitialization(code, model);
        code.AppendLine("    }");
        code.AppendLine();
        AppendAfterRender(code, [model]);
        code.AppendLine("}");

        return new GeneratedChartExample(
            ChartType: model.Definition.Name,
            Route: model.Route,
            PageName: model.PageName,
            Code: code.ToString(),
            RequiredScripts: RequiredScripts([model]));
    }

    public GeneratedChartDashboard GenerateDashboard(ChartDashboardRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var chartRequests = request.Charts is { Count: > 0 }
            ? request.Charts
            : ChartCatalog.All.Select(x => new ChartGenerationRequest
            {
                ChartType = x.Name,
                Title = $"{x.Name} Example",
                Datalabels = false,
            }).ToList();

        var title = string.IsNullOrWhiteSpace(request.Title) ? "Chart Dashboard" : request.Title.Trim();
        var route = NormalizeRoute(request.Route, "/charts/dashboard", "route");
        var pageName = MakeIdentifier(string.IsNullOrWhiteSpace(request.PageName) ? "ChartDashboardPage" : request.PageName);
        var models = chartRequests.Select((chart, index) => CreateChartModel(chart, (index + 1).ToString(CultureInfo.InvariantCulture))).ToList();

        var code = new StringBuilder();
        code.AppendLine($"@page \"{route}\"");
        code.AppendLine("@using BlazorExpress.ChartJS");
        code.AppendLine();
        code.AppendLine($"<h3>{EscapeHtml(title)}</h3>");
        code.AppendLine();
        code.AppendLine("<div class=\"chart-dashboard\">");
        foreach (var model in models)
        {
            code.AppendLine("    <section class=\"chart-dashboard-section\">");
            code.AppendLine($"        <h4>{EscapeHtml(model.Title)}</h4>");
            AppendChartMarkup(code, model, indent: "        ");
            code.AppendLine("    </section>");
        }
        code.AppendLine("</div>");
        code.AppendLine();
        code.AppendLine("@code {");
        foreach (var model in models)
        {
            AppendChartFields(code, model);
            code.AppendLine();
        }
        code.AppendLine("    protected override void OnInitialized()");
        code.AppendLine("    {");
        foreach (var model in models)
            AppendChartInitialization(code, model);
        code.AppendLine("    }");
        code.AppendLine();
        AppendAfterRender(code, models);
        code.AppendLine("}");

        return new GeneratedChartDashboard(
            Route: route,
            PageName: pageName,
            Code: code.ToString(),
            ChartTypes: models.Select(x => x.Definition.Name).ToList(),
            RequiredScripts: RequiredScripts(models));
    }

    private static ChartRenderModel CreateChartModel(ChartGenerationRequest request, string identifierSuffix)
    {
        var definition = ChartCatalog.Get(request.ChartType);
        var title = string.IsNullOrWhiteSpace(request.Title) ? $"{definition.Name} Chart" : request.Title.Trim();
        var pageName = MakeIdentifier(string.IsNullOrWhiteSpace(request.PageName) ? $"{definition.Name}ChartPage" : request.PageName);
        var route = NormalizeRoute(request.Route, $"/charts/{ToKebabCase(definition.Name)}", "route");
        var labels = NormalizeLabels(request.Labels, definition);
        var datasets = NormalizeDatasets(request.Datasets, labels, definition);
        var width = NormalizeDimension(request.Width, 700, "width");
        var height = NormalizeDimension(request.Height, 400, "height");
        var datalabels = request.Datalabels == true && definition.SupportsDatalabels;
        var legendPosition = NormalizeLegendPosition(request.LegendPosition);
        var stacked = request.Stacked == true && definition.SupportsStacking;
        var horizontal = definition.SupportsOrientation && string.Equals(request.Orientation, "horizontal", StringComparison.OrdinalIgnoreCase);
        var suffix = string.IsNullOrWhiteSpace(identifierSuffix) ? "" : identifierSuffix;

        return new ChartRenderModel(
            Definition: definition,
            Title: title,
            Route: route,
            PageName: pageName,
            Labels: labels,
            Datasets: datasets,
            Width: width,
            Height: height,
            IncludeDatalabels: datalabels,
            LegendPosition: legendPosition,
            Stacked: stacked,
            Horizontal: horizontal,
            ChartField: $"{LowerFirst(definition.ComponentName)}{suffix}",
            OptionsField: $"{LowerFirst(definition.OptionsTypeName)}{suffix}",
            DataField: $"chartData{suffix}");
    }

    private static void AppendChartMarkup(StringBuilder code, ChartRenderModel model, string indent) =>
        code.AppendLine($"{indent}<{model.Definition.ComponentName} @ref=\"{model.ChartField}\" Width=\"{model.Width}\" Height=\"{model.Height}\" />");

    private static void AppendChartFields(StringBuilder code, ChartRenderModel model)
    {
        code.AppendLine($"    private {model.Definition.ComponentName} {model.ChartField} = default!;");
        code.AppendLine($"    private {model.Definition.OptionsTypeName} {model.OptionsField} = default!;");
        code.AppendLine($"    private ChartData {model.DataField} = default!;");
    }

    private static void AppendChartInitialization(StringBuilder code, ChartRenderModel model)
    {
        code.AppendLine($"        {model.DataField} = new ChartData");
        code.AppendLine("        {");
        code.AppendLine($"            Labels = new List<string> {{ {string.Join(", ", model.Labels.Select(ToCSharpString))} }},");
        code.AppendLine("            Datasets = new List<IChartDataset>");
        code.AppendLine("            {");
        foreach (var dataset in model.Datasets)
            AppendDataset(code, model.Definition, dataset, model.Labels.Count, model.Stacked);
        code.AppendLine("            },");
        code.AppendLine("        };");
        code.AppendLine();
        code.AppendLine($"        {model.OptionsField} = new {model.Definition.OptionsTypeName}");
        code.AppendLine("        {");
        code.AppendLine("            Responsive = true,");
        code.AppendLine("            MaintainAspectRatio = false,");
        if (model.Definition.Name is "Bar" && model.Horizontal)
            code.AppendLine("            IndexAxis = \"y\",");
        code.AppendLine("        };");
        code.AppendLine();
        AppendOptionsCustomization(code, model);
    }

    private static void AppendAfterRender(StringBuilder code, IReadOnlyList<ChartRenderModel> models)
    {
        code.AppendLine("    protected override async Task OnAfterRenderAsync(bool firstRender)");
        code.AppendLine("    {");
        code.AppendLine("        if (firstRender)");
        code.AppendLine("        {");
        foreach (var model in models)
        {
            if (model.IncludeDatalabels)
                code.AppendLine($"            await {model.ChartField}.InitializeAsync({model.DataField}, {model.OptionsField}, new string[] {{ \"ChartDataLabels\" }});");
            else
                code.AppendLine($"            await {model.ChartField}.InitializeAsync({model.DataField}, {model.OptionsField});");
        }
        code.AppendLine("        }");
        code.AppendLine();
        code.AppendLine("        await base.OnAfterRenderAsync(firstRender);");
        code.AppendLine("    }");
    }

    private static void AppendDataset(StringBuilder code, ChartDefinition definition, ChartDatasetRequest dataset, int labelCount, bool stacked)
    {
        var colors = NormalizeColors(dataset.BackgroundColor, labelCount);
        var borderColors = NormalizeColors(dataset.BorderColor, labelCount);

        code.AppendLine($"                new {definition.DatasetTypeName}");
        code.AppendLine("                {");
        code.AppendLine($"                    Label = {ToCSharpString(dataset.Label ?? "Dataset")},");

        if (definition.Name is "Bubble")
        {
            var points = NormalizePoints(dataset, labelCount, includeRadius: true);
            code.AppendLine($"                    Data = new List<BubbleChartDataPoint> {{ {string.Join(", ", points.Select(x => $"new({FormatNumber(x.X)}, {FormatNumber(x.Y)}, {FormatNumber(x.R ?? 8)})"))} }},");
        }
        else if (definition.Name is "Scatter")
        {
            var points = NormalizePoints(dataset, labelCount, includeRadius: false);
            code.AppendLine($"                    Data = new List<ScatterChartDataPoint> {{ {string.Join(", ", points.Select(x => $"new({FormatNumber(x.X)}, {FormatNumber(x.Y)})"))} }},");
        }
        else
        {
            var values = NormalizeData(dataset.Data, labelCount);
            code.AppendLine($"                    Data = new List<double?> {{ {string.Join(", ", values.Select(FormatNullableNumber))} }},");
        }

        AppendColorProperty(code, definition, "BackgroundColor", colors);
        AppendColorProperty(code, definition, "BorderColor", borderColors);

        if (definition.Name is "Bar")
            code.AppendLine("                    BorderWidth = new List<double> { 1 },");

        if (stacked && !string.IsNullOrWhiteSpace(dataset.Stack) && definition.Name is "Bar")
            code.AppendLine($"                    Stack = {ToCSharpString(dataset.Stack)},");

        code.AppendLine("                },");
    }

    private static void AppendOptionsCustomization(StringBuilder code, ChartRenderModel model)
    {
        var definition = model.Definition;

        if (definition.SupportsTitleOptions)
        {
            code.AppendLine($"        {model.OptionsField}.Plugins.Title!.Text = {ToCSharpString(model.Title)};");
            code.AppendLine($"        {model.OptionsField}.Plugins.Title.Display = true;");
        }

        if (definition.SupportsLegendOptions)
            code.AppendLine($"        {model.OptionsField}.Plugins.Legend.Position = {ToCSharpString(model.LegendPosition)};");

        if (model.Stacked && definition.SupportsScales)
        {
            code.AppendLine($"        {model.OptionsField}.Scales.X!.Stacked = true;");
            code.AppendLine($"        {model.OptionsField}.Scales.Y!.Stacked = true;");
        }
    }

    private static void AppendColorProperty(StringBuilder code, ChartDefinition definition, string propertyName, IReadOnlyList<string> colors)
    {
        var propertyType = definition.DatasetType.GetProperty(propertyName)?.PropertyType;
        if (propertyType == typeof(string))
        {
            code.AppendLine($"                    {propertyName} = {ToCSharpString(colors.FirstOrDefault() ?? DefaultColors[0])},");
            return;
        }

        code.AppendLine($"                    {propertyName} = new List<string> {{ {string.Join(", ", colors.Select(ToCSharpString))} }},");
    }

    private static IReadOnlyList<string> RequiredScripts(IReadOnlyList<ChartRenderModel> models)
    {
        var scripts = new List<string>
        {
            "https://cdnjs.cloudflare.com/ajax/libs/Chart.js/4.4.1/chart.umd.js",
        };

        if (models.Any(x => x.IncludeDatalabels))
            scripts.Add("https://cdnjs.cloudflare.com/ajax/libs/chartjs-plugin-datalabels/2.2.0/chartjs-plugin-datalabels.min.js");

        scripts.Add("_content/BlazorExpress.ChartJS/blazorexpress.chartjs.js");
        return scripts.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
    }

    private static IReadOnlyList<string> NormalizeLabels(IReadOnlyList<string>? labels, ChartDefinition definition)
    {
        if (labels is { Count: > 0 })
            return labels.Select((x, index) => string.IsNullOrWhiteSpace(x) ? $"Label {index + 1}" : x.Trim()).ToList();

        return definition.Name is "Bubble" or "Scatter"
            ? ["Point 1", "Point 2", "Point 3", "Point 4"]
            : ["Jan", "Feb", "Mar", "Apr", "May", "Jun"];
    }

    private static IReadOnlyList<ChartDatasetRequest> NormalizeDatasets(IReadOnlyList<ChartDatasetRequest>? datasets, IReadOnlyList<string> labels, ChartDefinition definition)
    {
        if (datasets is { Count: > 0 })
            return datasets;

        if (definition.Name is "Pie" or "Doughnut" or "PolarArea")
        {
            return
            [
                new()
                {
                    Label = "Revenue",
                    Data = labels.Select((_, index) => (double?)(20 + (index + 1) * 8)).ToList(),
                    BackgroundColor = DefaultColors.Take(labels.Count).ToList(),
                    BorderColor = DefaultColors.Take(labels.Count).ToList(),
                }
            ];
        }

        return
        [
            new()
            {
                Label = "Current",
                Data = labels.Select((_, index) => (double?)(12 + (index + 1) * 6)).ToList(),
                BackgroundColor = [DefaultColors[0]],
                BorderColor = [DefaultColors[0]],
                Points = labels.Select((_, index) => new ChartPointRequest { X = index + 1, Y = 12 + (index + 1) * 6, R = 6 + index }).ToList(),
            },
            new()
            {
                Label = "Previous",
                Data = labels.Select((_, index) => (double?)(10 + (index + 1) * 4)).ToList(),
                BackgroundColor = [DefaultColors[1]],
                BorderColor = [DefaultColors[1]],
                Points = labels.Select((_, index) => new ChartPointRequest { X = index + 1, Y = 10 + (index + 1) * 4, R = 5 + index }).ToList(),
            }
        ];
    }

    private static IReadOnlyList<double?> NormalizeData(IReadOnlyList<double?>? values, int count)
    {
        if (values is { Count: > 0 })
            return Pad(values, count, 0);

        return Enumerable.Range(1, count).Select(x => (double?)(x * 10)).ToList();
    }

    private static IReadOnlyList<ChartPointRequest> NormalizePoints(ChartDatasetRequest dataset, int count, bool includeRadius)
    {
        if (dataset.Points is { Count: > 0 })
            return Pad(dataset.Points, count, new ChartPointRequest());

        var data = NormalizeData(dataset.Data, count);
        return data.Select((y, index) => new ChartPointRequest { X = index + 1, Y = y ?? 0, R = includeRadius ? 6 + index : null }).ToList();
    }

    private static IReadOnlyList<string> NormalizeColors(IReadOnlyList<string>? colors, int count)
    {
        if (colors is { Count: > 0 })
            return colors.Select(x => string.IsNullOrWhiteSpace(x) ? DefaultColors[0] : x).ToList();

        return DefaultColors.Take(Math.Max(1, Math.Min(count, DefaultColors.Length))).ToList();
    }

    private static int NormalizeDimension(int? value, int fallback, string field)
    {
        if (value is null)
            return fallback;

        if (value <= 0)
            throw new ToolInputException($"{field} must be greater than zero.", field);

        return value.Value;
    }

    private static IReadOnlyList<T> Pad<T>(IReadOnlyList<T> values, int count, T fallback)
    {
        var result = values.Take(count).ToList();

        while (result.Count < count)
            result.Add(fallback);

        return result;
    }

    private static string NormalizeRoute(string? route, string fallback, string field)
    {
        if (string.IsNullOrWhiteSpace(route))
            return fallback;

        var normalized = route.Trim();
        if (normalized.Any(char.IsWhiteSpace) || normalized.Contains('"', StringComparison.Ordinal))
            throw new ToolInputException($"{field} must be a route path without whitespace or quotes.", field);

        return normalized.StartsWith("/", StringComparison.Ordinal) ? normalized : $"/{normalized}";
    }

    private static string NormalizeLegendPosition(string? legendPosition)
    {
        var value = legendPosition?.Trim().ToLowerInvariant();
        return value is "left" or "right" or "bottom" or "top" ? value : "top";
    }

    private static string MakeIdentifier(string? value)
    {
        var cleaned = Regex.Replace(value ?? "GeneratedChartPage", "[^a-zA-Z0-9_]", "");
        if (string.IsNullOrWhiteSpace(cleaned))
            cleaned = "GeneratedChartPage";
        if (char.IsDigit(cleaned[0]))
            cleaned = $"Chart{cleaned}";
        return cleaned;
    }

    internal static string ToKebabCase(string value) =>
        Regex.Replace(value, "([a-z0-9])([A-Z])", "$1-$2").ToLowerInvariant();

    private static string LowerFirst(string value) =>
        string.IsNullOrWhiteSpace(value) ? value : char.ToLowerInvariant(value[0]) + value[1..];

    private static string EscapeHtml(string value) =>
        value.Replace("&", "&amp;", StringComparison.Ordinal)
            .Replace("<", "&lt;", StringComparison.Ordinal)
            .Replace(">", "&gt;", StringComparison.Ordinal);

    private static string ToCSharpString(string value) =>
        $"\"{value.Replace("\\", "\\\\", StringComparison.Ordinal).Replace("\"", "\\\"", StringComparison.Ordinal)}\"";

    private static string FormatNullableNumber(double? value) =>
        value.HasValue ? FormatNumber(value.Value) : "null";

    private static string FormatNumber(double value) =>
        value.ToString("0.########", CultureInfo.InvariantCulture);

    private sealed record ChartRenderModel(
        ChartDefinition Definition,
        string Title,
        string Route,
        string PageName,
        IReadOnlyList<string> Labels,
        IReadOnlyList<ChartDatasetRequest> Datasets,
        int Width,
        int Height,
        bool IncludeDatalabels,
        string LegendPosition,
        bool Stacked,
        bool Horizontal,
        string ChartField,
        string OptionsField,
        string DataField);
}
