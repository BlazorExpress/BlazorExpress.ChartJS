using System.Text.Json;

namespace BlazorExpress.ChartJS.MCP;

internal static class ChartRequestParser
{
    public static ChartGenerationRequest CreateChartRequest(
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
            Labels = ParseLabels(labelsJson),
            Datasets = ParseDatasets(datasetsJson),
            Width = width,
            Height = height,
            LegendPosition = legendPosition,
            Datalabels = datalabels,
            Stacked = stacked,
            Orientation = orientation,
        };

    public static IReadOnlyList<ChartGenerationRequest>? ParseDashboardCharts(string? chartsJson)
    {
        if (string.IsNullOrWhiteSpace(chartsJson))
            return null;

        try
        {
            using var document = JsonDocument.Parse(chartsJson);
            if (document.RootElement.ValueKind is not JsonValueKind.Array)
                throw new ToolInputException("chartsJson must be a JSON array of chart specifications.", "chartsJson");

            var charts = new List<ChartGenerationRequest>();
            var index = 0;
            foreach (var element in document.RootElement.EnumerateArray())
            {
                if (element.ValueKind is not JsonValueKind.Object)
                    throw new ToolInputException($"chartsJson[{index}] must be an object.", "chartsJson");

                charts.Add(ParseChartObject(element, $"chartsJson[{index}]"));
                index++;
            }

            return charts;
        }
        catch (JsonException exception)
        {
            throw new ToolInputException($"chartsJson is not valid JSON: {exception.Message}", "chartsJson");
        }
    }

    private static IReadOnlyList<string>? ParseLabels(string? labelsJson)
    {
        if (string.IsNullOrWhiteSpace(labelsJson))
            return null;

        try
        {
            var labels = Json.Deserialize<List<string>>(labelsJson);
            return labels;
        }
        catch (JsonException exception)
        {
            throw new ToolInputException($"labelsJson must be a JSON string array: {exception.Message}", "labelsJson");
        }
    }

    private static IReadOnlyList<ChartDatasetRequest>? ParseDatasets(string? datasetsJson)
    {
        if (string.IsNullOrWhiteSpace(datasetsJson))
            return null;

        try
        {
            using var document = JsonDocument.Parse(datasetsJson);
            if (document.RootElement.ValueKind is not JsonValueKind.Array)
                throw new ToolInputException("datasetsJson must be a JSON array.", "datasetsJson");

            return ParseDatasetArray(document.RootElement, "datasetsJson");
        }
        catch (JsonException exception)
        {
            throw new ToolInputException($"datasetsJson is not valid JSON: {exception.Message}", "datasetsJson");
        }
    }

    private static ChartGenerationRequest ParseChartObject(JsonElement element, string field)
    {
        var chartType = GetOptionalString(element, "chartType")
            ?? throw new ToolInputException($"{field}.chartType is required.", field);

        return new ChartGenerationRequest
        {
            ChartType = chartType,
            Title = GetOptionalString(element, "title"),
            Route = GetOptionalString(element, "route"),
            PageName = GetOptionalString(element, "pageName"),
            Labels = element.TryGetProperty("labels", out var labels) ? ParseStringArray(labels, $"{field}.labels") : null,
            Datasets = element.TryGetProperty("datasets", out var datasets) ? ParseDatasetArray(datasets, $"{field}.datasets") : null,
            Width = GetOptionalInt(element, "width", field),
            Height = GetOptionalInt(element, "height", field),
            LegendPosition = GetOptionalString(element, "legendPosition"),
            Datalabels = GetOptionalBool(element, "datalabels", field),
            Stacked = GetOptionalBool(element, "stacked", field),
            Orientation = GetOptionalString(element, "orientation"),
        };
    }

    private static IReadOnlyList<ChartDatasetRequest> ParseDatasetArray(JsonElement element, string field)
    {
        if (element.ValueKind is not JsonValueKind.Array)
            throw new ToolInputException($"{field} must be a JSON array.", field);

        var datasets = new List<ChartDatasetRequest>();
        var index = 0;
        foreach (var datasetElement in element.EnumerateArray())
        {
            if (datasetElement.ValueKind is not JsonValueKind.Object)
                throw new ToolInputException($"{field}[{index}] must be an object.", field);

            datasets.Add(new ChartDatasetRequest
            {
                Label = GetOptionalString(datasetElement, "label"),
                Data = datasetElement.TryGetProperty("data", out var data) ? ParseNullableNumberArray(data, $"{field}[{index}].data") : null,
                Points = datasetElement.TryGetProperty("points", out var points) ? ParsePoints(points, $"{field}[{index}].points") : null,
                BackgroundColor = datasetElement.TryGetProperty("backgroundColor", out var backgroundColor) ? ParseStringOrStringArray(backgroundColor, $"{field}[{index}].backgroundColor") : null,
                BorderColor = datasetElement.TryGetProperty("borderColor", out var borderColor) ? ParseStringOrStringArray(borderColor, $"{field}[{index}].borderColor") : null,
                Stack = GetOptionalString(datasetElement, "stack"),
            });
            index++;
        }

        return datasets;
    }

    private static IReadOnlyList<string> ParseStringOrStringArray(JsonElement element, string field) =>
        element.ValueKind switch
        {
            JsonValueKind.String => [element.GetString() ?? ""],
            JsonValueKind.Array => ParseStringArray(element, field),
            _ => throw new ToolInputException($"{field} must be a string or string array.", field),
        };

    private static IReadOnlyList<string> ParseStringArray(JsonElement element, string field)
    {
        if (element.ValueKind is not JsonValueKind.Array)
            throw new ToolInputException($"{field} must be a string array.", field);

        var values = new List<string>();
        foreach (var item in element.EnumerateArray())
        {
            if (item.ValueKind is not JsonValueKind.String)
                throw new ToolInputException($"{field} must contain only strings.", field);

            values.Add(item.GetString() ?? "");
        }

        return values;
    }

    private static IReadOnlyList<double?> ParseNullableNumberArray(JsonElement element, string field)
    {
        if (element.ValueKind is not JsonValueKind.Array)
            throw new ToolInputException($"{field} must be a number array.", field);

        var values = new List<double?>();
        foreach (var item in element.EnumerateArray())
        {
            if (item.ValueKind is JsonValueKind.Null)
            {
                values.Add(null);
                continue;
            }

            if (item.ValueKind is not JsonValueKind.Number || !item.TryGetDouble(out var value))
                throw new ToolInputException($"{field} must contain only numbers or null.", field);

            values.Add(value);
        }

        return values;
    }

    private static IReadOnlyList<ChartPointRequest> ParsePoints(JsonElement element, string field)
    {
        if (element.ValueKind is not JsonValueKind.Array)
            throw new ToolInputException($"{field} must be an array of point objects.", field);

        var points = new List<ChartPointRequest>();
        var index = 0;
        foreach (var point in element.EnumerateArray())
        {
            if (point.ValueKind is not JsonValueKind.Object)
                throw new ToolInputException($"{field}[{index}] must be an object.", field);

            points.Add(new ChartPointRequest
            {
                X = GetRequiredDouble(point, "x", $"{field}[{index}]"),
                Y = GetRequiredDouble(point, "y", $"{field}[{index}]"),
                R = GetOptionalDouble(point, "r", $"{field}[{index}]"),
            });
            index++;
        }

        return points;
    }

    private static string? GetOptionalString(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var value) || value.ValueKind is JsonValueKind.Null)
            return null;

        if (value.ValueKind is not JsonValueKind.String)
            throw new ToolInputException($"{propertyName} must be a string.", propertyName);

        return value.GetString();
    }

    private static int? GetOptionalInt(JsonElement element, string propertyName, string field)
    {
        if (!element.TryGetProperty(propertyName, out var value) || value.ValueKind is JsonValueKind.Null)
            return null;

        if (value.ValueKind is not JsonValueKind.Number || !value.TryGetInt32(out var result))
            throw new ToolInputException($"{field}.{propertyName} must be an integer.", $"{field}.{propertyName}");

        return result;
    }

    private static bool? GetOptionalBool(JsonElement element, string propertyName, string field)
    {
        if (!element.TryGetProperty(propertyName, out var value) || value.ValueKind is JsonValueKind.Null)
            return null;

        return value.ValueKind switch
        {
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            _ => throw new ToolInputException($"{field}.{propertyName} must be a boolean.", $"{field}.{propertyName}"),
        };
    }

    private static double GetRequiredDouble(JsonElement element, string propertyName, string field)
    {
        if (!element.TryGetProperty(propertyName, out var value))
            throw new ToolInputException($"{field}.{propertyName} is required.", $"{field}.{propertyName}");

        if (value.ValueKind is not JsonValueKind.Number || !value.TryGetDouble(out var result))
            throw new ToolInputException($"{field}.{propertyName} must be a number.", $"{field}.{propertyName}");

        return result;
    }

    private static double? GetOptionalDouble(JsonElement element, string propertyName, string field)
    {
        if (!element.TryGetProperty(propertyName, out var value) || value.ValueKind is JsonValueKind.Null)
            return null;

        if (value.ValueKind is not JsonValueKind.Number || !value.TryGetDouble(out var result))
            throw new ToolInputException($"{field}.{propertyName} must be a number.", $"{field}.{propertyName}");

        return result;
    }
}
