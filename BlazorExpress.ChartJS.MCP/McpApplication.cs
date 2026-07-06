using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.AspNetCore;
using ModelContextProtocol.Server;

namespace BlazorExpress.ChartJS.MCP;

public static class McpApplication
{
    public const string DefaultHttpUrl = "http://localhost:5000";
    public const string McpPath = "/mcp";
    public const string HealthPath = "/health";
    public const string TokenConfigurationKey = "CHARTJS_MCP_TOKEN";

    public static Task RunAsync(string[] args, CancellationToken cancellationToken = default)
    {
        var options = ParseOptions(args);

        return options.Transport == McpTransportMode.Stdio
            ? RunStdioAsync(options.RemainingArgs, cancellationToken)
            : RunHttpAsync(options.RemainingArgs, cancellationToken);
    }

    public static McpHostOptions ParseOptions(IReadOnlyList<string> args)
    {
        var transport = McpTransportMode.Http;
        var remainingArgs = new List<string>();

        for (var i = 0; i < args.Count; i++)
        {
            var arg = args[i];

            if (string.Equals(arg, "--stdio", StringComparison.OrdinalIgnoreCase))
            {
                transport = McpTransportMode.Stdio;
                continue;
            }

            if (string.Equals(arg, "--http", StringComparison.OrdinalIgnoreCase))
            {
                transport = McpTransportMode.Http;
                continue;
            }

            if (string.Equals(arg, "--transport", StringComparison.OrdinalIgnoreCase))
            {
                if (i + 1 >= args.Count)
                    throw new ArgumentException("Missing value for --transport. Use 'http' or 'stdio'.");

                transport = ParseTransport(args[++i]);
                continue;
            }

            if (arg.StartsWith("--transport=", StringComparison.OrdinalIgnoreCase))
            {
                transport = ParseTransport(arg["--transport=".Length..]);
                continue;
            }

            remainingArgs.Add(arg);
        }

        return new McpHostOptions(transport, remainingArgs.ToArray());
    }

    public static WebApplication CreateHttpApp(
        string[] args,
        string? environmentName = null,
        Action<IWebHostBuilder>? configureWebHost = null,
        IEnumerable<KeyValuePair<string, string?>>? configuration = null)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = args,
            EnvironmentName = environmentName,
        });

        if (configuration is not null)
            builder.Configuration.AddInMemoryCollection(configuration);

        var useDefaultHttpUrl = !HasUrlOverride(args);

        configureWebHost?.Invoke(builder.WebHost);

        RegisterCoreServices(builder.Services);
        builder.Services
            .AddMcpServer()
            .WithHttpTransport(options =>
            {
                options.Stateless = true;
            })
            .AddAuthorizationFilters()
            .WithToolsFromAssembly();

        var token = builder.Configuration[TokenConfigurationKey];
        if (!builder.Environment.IsDevelopment() && string.IsNullOrWhiteSpace(token))
            throw new InvalidOperationException($"{TokenConfigurationKey} must be configured when HTTP MCP is running outside Development.");

        var app = builder.Build();

        if (useDefaultHttpUrl)
            app.Urls.Add(DefaultHttpUrl);

        app.MapGet(HealthPath, () => Results.Ok(new { status = "healthy" }));

        if (!app.Environment.IsDevelopment())
            app.UseBearerTokenProtection(token!);

        app.MapMcp(McpPath);

        return app;
    }

    private static async Task RunHttpAsync(string[] args, CancellationToken cancellationToken)
    {
        var app = CreateHttpApp(args);
        await app.RunAsync(cancellationToken);
    }

    private static async Task RunStdioAsync(string[] args, CancellationToken cancellationToken)
    {
        var builder = Host.CreateEmptyApplicationBuilder(new HostApplicationBuilderSettings { Args = args });

        RegisterCoreServices(builder.Services);
        builder.Services
            .AddMcpServer()
            .WithStdioServerTransport()
            .WithToolsFromAssembly();

        await builder.Build().RunAsync(cancellationToken);
    }

    private static void RegisterCoreServices(IServiceCollection services)
    {
        services
            .AddSingleton<ChartExampleGenerator>()
            .AddSingleton<ProjectIntegrationService>();
    }

    private static McpTransportMode ParseTransport(string value) =>
        value.ToLowerInvariant() switch
        {
            "http" => McpTransportMode.Http,
            "stdio" => McpTransportMode.Stdio,
            _ => throw new ArgumentException($"Unsupported MCP transport '{value}'. Use 'http' or 'stdio'."),
        };

    private static bool HasUrlOverride(IEnumerable<string> args) =>
        args.Any(arg =>
            string.Equals(arg, "--urls", StringComparison.OrdinalIgnoreCase)
            || arg.StartsWith("--urls=", StringComparison.OrdinalIgnoreCase))
        || !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ASPNETCORE_URLS"));
}

internal static class McpApplicationBuilderExtensions
{
    public static IApplicationBuilder UseBearerTokenProtection(this IApplicationBuilder app, string token) =>
        app.Use(async (context, next) =>
        {
            if (!context.Request.Path.StartsWithSegments(McpApplication.McpPath))
            {
                await next(context);
                return;
            }

            var expectedHeader = $"Bearer {token}";
            var actualHeader = context.Request.Headers.Authorization.ToString();

            if (!string.Equals(actualHeader, expectedHeader, StringComparison.Ordinal))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await next(context);
        });
}

public sealed record McpHostOptions(McpTransportMode Transport, string[] RemainingArgs);

public enum McpTransportMode
{
    Http,
    Stdio,
}
