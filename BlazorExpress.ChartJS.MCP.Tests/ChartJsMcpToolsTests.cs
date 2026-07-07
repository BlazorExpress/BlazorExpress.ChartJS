using System.Text.Json;

namespace BlazorExpress.ChartJS.MCP.Tests;

public class ChartJsMcpToolsTests
{
    [Fact]
    public void GenerateChartExample_Returns_Structured_Error_For_Malformed_Labels()
    {
        var json = ChartJsMcpTools.GenerateChartExample("Bar", labelsJson: "[");

        AssertToolError(json, "labelsJson");
    }

    [Fact]
    public void GenerateChartExample_Returns_Structured_Error_For_Malformed_Datasets()
    {
        var json = ChartJsMcpTools.GenerateChartExample("Bar", datasetsJson: """{"label":"Bad"}""");

        AssertToolError(json, "datasetsJson");
    }

    [Fact]
    public void GenerateChartExample_Returns_Structured_Error_For_Unsupported_Chart()
    {
        var json = ChartJsMcpTools.GenerateChartExample("Nope");

        AssertToolError(json, "chartType");
    }

    [Fact]
    public void ApplyProjectIntegration_Returns_Structured_Error_For_Bad_Plan_Json()
    {
        var json = ChartJsMcpTools.ApplyProjectIntegration("[");

        AssertToolError(json);
    }

    [Fact]
    public void PreviewProjectIntegration_Returns_Structured_Error_For_Invalid_Target_Project()
    {
        var json = ChartJsMcpTools.PreviewProjectIntegration(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N")), "Bar");

        AssertToolError(json, "targetProjectPath");
    }

    [Fact]
    public void GenerateChartExample_Accepts_Color_String()
    {
        var json = ChartJsMcpTools.GenerateChartExample(
            "Bar",
            datasetsJson: """[{"label":"Revenue","data":[1,2],"backgroundColor":"#123456","borderColor":"#654321"}]""");

        Assert.DoesNotContain("\"success\": false", json);
        Assert.Contains("#123456", json);
        Assert.Contains("#654321", json);
    }

    [Fact]
    public void GenerateChartExample_Accepts_Color_Array()
    {
        var json = ChartJsMcpTools.GenerateChartExample(
            "Line",
            datasetsJson: """[{"label":"Revenue","data":[1,2],"backgroundColor":["#123456"],"borderColor":["#654321"]}]""");

        Assert.DoesNotContain("\"success\": false", json);
        Assert.Contains("#123456", json);
        Assert.Contains("#654321", json);
    }

    [Fact]
    public void GenerateChartDashboard_Returns_Structured_Error_For_Invalid_Child_Spec()
    {
        var json = ChartJsMcpTools.GenerateChartDashboard(chartsJson: """[{"title":"Missing type"}]""");

        AssertToolError(json, "chartsJson[0]");
    }

    [Fact]
    public void GenerateChartDashboard_Accepts_Custom_Chart_Specs()
    {
        var json = ChartJsMcpTools.GenerateChartDashboard(chartsJson: """
            [
              {
                "chartType": "Scatter",
                "datasets": [
                  {
                    "label": "Samples",
                    "points": [{ "x": 1, "y": 2 }],
                    "backgroundColor": "#123456"
                  }
                ]
              }
            ]
            """);

        Assert.DoesNotContain("\"success\": false", json);
        Assert.Contains("ScatterChart", json);
        Assert.Contains("new(1, 2)", json);
    }

    private static void AssertToolError(string json, string? expectedField = null)
    {
        using var document = JsonDocument.Parse(json);
        Assert.False(document.RootElement.GetProperty("success").GetBoolean());
        Assert.Equal("invalid_input", document.RootElement.GetProperty("error").GetProperty("code").GetString());

        if (expectedField is not null)
            Assert.Equal(expectedField, document.RootElement.GetProperty("error").GetProperty("field").GetString());
    }
}
