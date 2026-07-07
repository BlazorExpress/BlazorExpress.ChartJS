using System.Text.Json;

namespace BlazorExpress.ChartJS.MCP;

internal static class McpToolExecution
{
    public static string Run<T>(Func<T> action)
    {
        try
        {
            return Json.Serialize(action());
        }
        catch (ToolInputException exception)
        {
            return InvalidInput(exception.Message, exception.Field, exception.Details);
        }
        catch (ArgumentException exception)
        {
            return InvalidInput(exception.Message, exception.ParamName);
        }
        catch (JsonException exception)
        {
            return InvalidInput(exception.Message, null);
        }
        catch (DirectoryNotFoundException exception)
        {
            return InvalidInput(exception.Message, "targetProjectPath");
        }
        catch (FileNotFoundException exception)
        {
            return InvalidInput(exception.Message, "targetProjectPath");
        }
        catch (InvalidOperationException exception)
        {
            return InvalidInput(exception.Message, null);
        }
    }

    private static string InvalidInput(string message, string? field, IReadOnlyList<string>? details = null) =>
        Json.Serialize(new ToolFailure(
            Success: false,
            Error: new ToolError(
                Code: "invalid_input",
                Message: message,
                Field: field,
                Details: details ?? [])));
}
