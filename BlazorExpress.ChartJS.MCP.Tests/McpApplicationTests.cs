using Microsoft.AspNetCore.TestHost;

namespace BlazorExpress.ChartJS.MCP.Tests;

public class McpApplicationTests
{
    [Fact]
    public void ParseOptions_NoArgs_Selects_Http()
    {
        var options = McpApplication.ParseOptions([]);

        Assert.Equal(McpTransportMode.Http, options.Transport);
        Assert.Empty(options.RemainingArgs);
    }

    [Theory]
    [InlineData("--stdio")]
    [InlineData("--transport", "stdio")]
    public void ParseOptions_StdioArgs_Select_Stdio(params string[] args)
    {
        var options = McpApplication.ParseOptions(args);

        Assert.Equal(McpTransportMode.Stdio, options.Transport);
        Assert.Empty(options.RemainingArgs);
    }

    [Fact]
    public void ParseOptions_HttpArgs_Select_Http()
    {
        var options = McpApplication.ParseOptions(["--http"]);

        Assert.Equal(McpTransportMode.Http, options.Transport);
        Assert.Empty(options.RemainingArgs);
    }

    [Fact]
    public void CreateHttpApp_Defaults_To_Localhost_5000()
    {
        using var app = McpApplication.CreateHttpApp([], "Development");

        Assert.Contains(McpApplication.DefaultHttpUrl, app.Urls);
    }

    [Fact]
    public async Task Health_Returns_Success()
    {
        await using var app = CreateTestHttpApp("Development");
        await app.StartAsync();

        var response = await app.GetTestClient().GetAsync(McpApplication.HealthPath);

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Development_Mcp_Allows_Missing_Bearer_Token()
    {
        await using var app = CreateTestHttpApp("Development");
        await app.StartAsync();

        var response = await app.GetTestClient().GetAsync(McpApplication.McpPath);

        Assert.NotEqual(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public void Production_Http_Requires_Token_At_Startup()
    {
        var exception = Assert.Throws<InvalidOperationException>(() => CreateTestHttpApp("Production"));

        Assert.Contains(McpApplication.TokenConfigurationKey, exception.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("wrong-token")]
    public async Task Production_Mcp_Rejects_Missing_Or_Wrong_Bearer_Token(string? token)
    {
        await using var app = CreateTestHttpApp("Production", "correct-token");
        await app.StartAsync();
        using var request = new HttpRequestMessage(HttpMethod.Get, McpApplication.McpPath);
        if (token is not null)
            request.Headers.Authorization = new("Bearer", token);

        var response = await app.GetTestClient().SendAsync(request);

        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Production_Mcp_Allows_Correct_Bearer_Token()
    {
        await using var app = CreateTestHttpApp("Production", "correct-token");
        await app.StartAsync();
        using var request = new HttpRequestMessage(HttpMethod.Get, McpApplication.McpPath);
        request.Headers.Authorization = new("Bearer", "correct-token");

        var response = await app.GetTestClient().SendAsync(request);

        Assert.NotEqual(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    private static Microsoft.AspNetCore.Builder.WebApplication CreateTestHttpApp(string environmentName, string? token = null)
    {
        var configuration = token is null
            ? null
            : new Dictionary<string, string?> { [McpApplication.TokenConfigurationKey] = token };

        return McpApplication.CreateHttpApp(
            [],
            environmentName,
            webHost => webHost.UseTestServer(),
            configuration);
    }
}
