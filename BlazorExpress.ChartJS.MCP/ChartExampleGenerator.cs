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

        var definition = ChartCatalog.Get(request.ChartType);
        var title = string.IsNullOrWhiteSpace(request.Title) ? $"{definition.Name} Chart" : request.Title.Trim();
        var pageName = MakeIdentifier(string.IsNullOrWhiteSpace(request.PageName) ? $"{definition.Name}ChartPage" : request.PageName);
        var route = NormalizeRoute(request.Route, definition);
        var labels = NormalizeLabels(request.Labels, definition);
        var datasets = NormalizeDatasets(request.Datasets, labels, definition);
        var width = request.Width is > 0 ? request.Width.Value : 700;
        var height = request.Height is > 0 ? request.Height.Value : 400;
        var datalabels = request.Datalabels == true && definition.SupportsDatalabels;
        var legendPosition = NormalizeLegendPosition(request.LegendPosition);
        var stacked = request.Stacked == true && definition.SupportsStacking;
        var horizontal = definition.SupportsOrientation && string.Equals(request.Orientation, "horizontal", StringComparison.OrdinalIgnoreCase);

        var chartField = LowerFirst(definition.ComponentName);
        var optionsField = LowerFirst(definition.OptionsTypeName);
        var code = new StringBuilder();

        code.AppendLine($"@page \"{route}\"");
        code.AppendLine("@using BlazorExpress.ChartJS");
        code.AppendLine();
        code.AppendLine($"<h3>{EscapeHtml(title)}</h3>");
        code.AppendLine();
        code.AppendLine($"<{definition.ComponentName} @ref=\"{chartField}\" Width=\"{width}\" Height=\"{height}\" />");
        code.AppendLine();
        code.AppendLine("@code {");
        code.AppendLine($"    private {definition.ComponentName} {chartField} = default!;");
        code.AppendLine($"    private {definition.OptionsTypeName} {optionsField} = default!;");
        code.AppendLine("    private ChartData chartData = default!;");
        code.AppendLine();
        code.AppendLine("    protected override void OnInitialized()");
        code.AppendLine("    {");
        code.AppendLine("        chartData = new ChartData");
        code.AppendLine("        {");
        code.AppendLine($"            Labels = new List<string> {{ {string.Join(", ", labels.Select(ToCSharpString))} }},");
        code.AppendLine("            Datasets = new List<IChartDataset>");
        code.AppendLine("            {");
        foreach (var dataset in datasets)
            AppendDataset(code, definition, dataset, labels.Count, stacked);
        code.AppendLine("            },");
        code.AppendLine("        };");
        code.AppendLine();
        code.AppendLine($"        {optionsField} = new {definition.OptionsTypeName}");
        code.AppendLine("        {");
        code.AppendLine("            Responsive = true,");
        code.AppendLine("            MaintainAspectRatio = false,");
        if (definition.Name is "Bar" && horizontal)
            code.AppendLine("            IndexAxis = \"y\",");
        code.AppendLine("        };");
        code.AppendLine();
        AppendOptionsCustomization(code, definition, optionsField, title, legendPosition, stacked);
        code.AppendLine("    }");
        code.AppendLine();
        code.AppendLine("    protected override async Task OnAfterRenderAsync(bool firstRender)");
        code.AppendLine("    {");
        code.AppendLine("        if (firstRender)");
        if (datalabels)
            code.AppendLine($"            await {chartField}.InitializeAsync(chartData: chartData, chartOptions: {optionsField}, plugins: new string[] {{ \"ChartDataLabels\" }});");
        else
            code.AppendLine($"            await {chartField}.InitializeAsync(chartData, {optionsField});");
        code.AppendLine();
        code.AppendLine("        await base.OnAfterRenderAsync(firstRender);");
        code.AppendLine("    }");
        code.AppendLine("}");

        return new GeneratedChartExample(
            ChartType: definition.Name,
            Route: route,
            PageName: pageName,
            Code: code.ToString(),
            RequiredScripts: RequiredScripts(datalabels));
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

        code.AppendLine($"                    BackgroundColor = new List<string> {{ {string.Join(", ", colors.Select(ToCSharpString))} }},");
        code.AppendLine($"                    BorderColor = new List<string> {{ {string.Join(", ", borderColors.Select(ToCSharpString))} }},");

        if (definition.Name is "Bar")
            code.AppendLine("                    BorderWidth = new List<double> { 1 },");

        if (stacked && !string.IsNullOrWhiteSpace(dataset.Stack) && (definition.Name is "Bar"))
            code.AppendLine($"                    Stack = {ToCSharpString(dataset.Stack)},");

        code.AppendLine("                },");
    }

    private static void AppendOptionsCustomization(StringBuilder code, ChartDefinition definition, string optionsField, string title, string legendPosition, bool stacked)
    {
        if (definition.Name is "Bubble")
        {
            code.AppendLine($"        // BubbleChartOptions currently inherits the common ChartOptions surface.");
            return;
        }

        code.AppendLine($"        {optionsField}.Plugins.Title!.Text = {ToCSharpString(title)};");
        code.AppendLine($"        {optionsField}.Plugins.Title.Display = true;");
        code.AppendLine($"        {optionsField}.Plugins.Legend.Position = {ToCSharpString(legendPosition)};");

        if (stacked && definition.Name is "Bar" or "Line")
        {
            code.AppendLine($"        {optionsField}.Scales.X!.Stacked = true;");
            code.AppendLine($"        {optionsField}.Scales.Y!.Stacked = true;");
        }
    }

    private static IReadOnlyList<string> RequiredScripts(bool includeDatalabels)
    {
        var scripts = new List<string>
        {
            "https://cdnjs.cloudflare.com/ajax/libs/Chart.js/4.4.1/chart.umd.js",
            "_content/BlazorExpress.ChartJS/blazorexpress.chartjs.js"
        };

        if (includeDatalabels)
            scripts.Insert(1, "https://cdnjs.cloudflare.com/ajax/libs/chartjs-plugin-datalabels/2.2.0/chartjs-plugin-datalabels.min.js");

        return scripts;
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
            return colors;

        return DefaultColors.Take(Math.Max(1, Math.Min(count, DefaultColors.Length))).ToList();
    }

    private static IReadOnlyList<T> Pad<T>(IReadOnlyList<T> values, int count, T fallback)
    {
        var result = values.Take(count).ToList();

        while (result.Count < count)
            result.Add(fallback);

        return result;
    }

    private static string NormalizeRoute(string? route, ChartDefinition definition)
    {
        if (string.IsNullOrWhiteSpace(route))
            return $"/charts/{ToKebabCase(definition.Name)}";

        var normalized = route.Trim();
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
}
