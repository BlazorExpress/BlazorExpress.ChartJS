using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace BlazorExpress.ChartJS.MCP;

public sealed class ProjectIntegrationService
{
    private const string PackageReference = "BlazorExpress.ChartJS";
    private const string PackageVersion = "1.2.3";

    private readonly ChartExampleGenerator generator;

    public ProjectIntegrationService(ChartExampleGenerator generator)
    {
        this.generator = generator;
    }

    public IntegrationPlan Preview(PreviewIntegrationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var project = ResolveProject(request.TargetProjectPath);
        var generated = generator.Generate(request.Chart);
        var hostModel = DetectHostModel(project);
        var edits = new List<FileEdit>();
        var manualSteps = new List<string>();

        AddProjectReferenceEdit(project, edits);
        AddImportsEdit(project.RootDirectory, edits);
        AddScriptEdit(project, hostModel, generated.RequiredScripts, edits, manualSteps);
        AddPageEdit(project, hostModel, generated, edits);
        AddNavigationEdit(project.RootDirectory, generated.Route, request.Chart.Title ?? generated.ChartType + " Chart", edits, manualSteps);

        var plan = new IntegrationPlan
        {
            TargetProjectRoot = project.RootDirectory,
            ProjectFilePath = project.ProjectFilePath,
            DetectedHostModel = hostModel,
            Edits = edits,
            ManualSteps = manualSteps,
        };

        return plan with { PlanHash = ComputePlanHash(plan) };
    }

    public ApplyIntegrationResult Apply(IntegrationPlan plan)
    {
        ArgumentNullException.ThrowIfNull(plan);

        var expectedHash = ComputePlanHash(plan with { PlanHash = "" });
        if (!string.Equals(plan.PlanHash, expectedHash, StringComparison.Ordinal))
            throw new InvalidOperationException("The integration plan hash is stale or invalid. Run preview_project_integration again.");

        var root = Path.GetFullPath(plan.TargetProjectRoot);
        var writtenFiles = new List<string>();

        foreach (var edit in plan.Edits)
        {
            var fullPath = Path.GetFullPath(edit.Path);
            if (!IsPathInside(root, fullPath))
                throw new InvalidOperationException($"Refusing to write outside the target project root: {edit.Path}");

            if (File.Exists(fullPath))
            {
                var currentHash = HashText(File.ReadAllText(fullPath));
                if (!string.Equals(edit.OriginalHash, currentHash, StringComparison.Ordinal))
                    throw new InvalidOperationException($"Refusing to overwrite changed file. Re-run preview_project_integration: {fullPath}");
            }
            else if (!string.IsNullOrEmpty(edit.OriginalHash))
            {
                throw new InvalidOperationException($"Refusing to update a file that no longer exists: {fullPath}");
            }

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
            File.WriteAllText(fullPath, edit.NewContent, Encoding.UTF8);
            writtenFiles.Add(fullPath);
        }

        return new ApplyIntegrationResult(plan.PlanHash, writtenFiles, plan.ManualSteps);
    }

    public static string ComputePlanHash(IntegrationPlan plan)
    {
        var normalized = plan with { PlanHash = "" };
        return HashText(JsonSerializer.Serialize(normalized, Json.SerializerOptions));
    }

    private static ProjectContext ResolveProject(string targetProjectPath)
    {
        if (string.IsNullOrWhiteSpace(targetProjectPath))
            throw new ArgumentException("A target project path is required.", nameof(targetProjectPath));

        var path = Path.GetFullPath(targetProjectPath);
        if (File.Exists(path) && string.Equals(Path.GetExtension(path), ".csproj", StringComparison.OrdinalIgnoreCase))
            return new ProjectContext(path, Path.GetDirectoryName(path)!);

        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Target project path was not found: {path}");

        var projectFiles = Directory.GetFiles(path, "*.csproj", SearchOption.TopDirectoryOnly);
        if (projectFiles.Length == 0)
            throw new FileNotFoundException($"No .csproj file was found in {path}.");
        if (projectFiles.Length > 1)
            throw new InvalidOperationException($"Multiple .csproj files were found in {path}. Pass the exact project file path.");

        return new ProjectContext(projectFiles[0], path);
    }

    private static string DetectHostModel(ProjectContext project)
    {
        var projectText = File.ReadAllText(project.ProjectFilePath);

        if (projectText.Contains("<UseMaui>true</UseMaui>", StringComparison.OrdinalIgnoreCase)
            || projectText.Contains("-ios", StringComparison.OrdinalIgnoreCase)
            || projectText.Contains("-android", StringComparison.OrdinalIgnoreCase)
            || projectText.Contains("-maccatalyst", StringComparison.OrdinalIgnoreCase))
            return "MauiBlazorHybrid";

        if (projectText.Contains("Microsoft.NET.Sdk.BlazorWebAssembly", StringComparison.OrdinalIgnoreCase)
            || projectText.Contains("Microsoft.AspNetCore.Components.WebAssembly", StringComparison.OrdinalIgnoreCase))
            return "BlazorWebAssembly";

        if (File.Exists(Path.Combine(project.RootDirectory, "Components", "App.razor"))
            || File.Exists(Path.Combine(project.RootDirectory, "App.razor")))
            return "BlazorWebApp";

        if (Directory.Exists(Path.Combine(project.RootDirectory, "wwwroot")))
            return "BlazorWebAssembly";

        return "UnknownBlazor";
    }

    private static void AddProjectReferenceEdit(ProjectContext project, List<FileEdit> edits)
    {
        var content = File.ReadAllText(project.ProjectFilePath);
        if (content.Contains($"Include=\"{PackageReference}\"", StringComparison.OrdinalIgnoreCase)
            || content.Contains($"Include='{PackageReference}'", StringComparison.OrdinalIgnoreCase))
            return;

        var document = XDocument.Parse(content, LoadOptions.PreserveWhitespace);
        var projectElement = document.Root ?? throw new InvalidOperationException("Project file has no root element.");
        var itemGroup = new XElement("ItemGroup",
            new XElement("PackageReference",
                new XAttribute("Include", PackageReference),
                new XAttribute("Version", PackageVersion)));
        projectElement.Add(Environment.NewLine, "  ", itemGroup, Environment.NewLine);

        AddReplaceEdit(project.ProjectFilePath, content, document.ToString(SaveOptions.DisableFormatting), "Add BlazorExpress.ChartJS package reference.", edits);
    }

    private static void AddImportsEdit(string root, List<FileEdit> edits)
    {
        var importsPath = FindFirst(root, "_Imports.razor")
            ?? Path.Combine(root, "_Imports.razor");
        var content = File.Exists(importsPath) ? File.ReadAllText(importsPath) : "";

        if (content.Contains("@using BlazorExpress.ChartJS", StringComparison.Ordinal))
            return;

        var newContent = AppendLine(content, "@using BlazorExpress.ChartJS");
        AddReplaceEdit(importsPath, content, newContent, "Add BlazorExpress.ChartJS using to _Imports.razor.", edits);
    }

    private static void AddScriptEdit(ProjectContext project, string hostModel, IReadOnlyList<string> scripts, List<FileEdit> edits, List<string> manualSteps)
    {
        var candidatePaths = hostModel switch
        {
            "BlazorWebApp" => new[]
            {
                Path.Combine(project.RootDirectory, "Components", "App.razor"),
                Path.Combine(project.RootDirectory, "Pages", "_Host.cshtml"),
                Path.Combine(project.RootDirectory, "App.razor"),
            },
            _ => new[]
            {
                Path.Combine(project.RootDirectory, "wwwroot", "index.html"),
            }
        };

        var scriptFile = candidatePaths.FirstOrDefault(File.Exists);
        if (scriptFile is null)
        {
            manualSteps.Add("Add Chart.js, optional chartjs-plugin-datalabels, and _content/BlazorExpress.ChartJS/blazorexpress.chartjs.js script references to the host page.");
            return;
        }

        var content = File.ReadAllText(scriptFile);
        var newContent = content;
        foreach (var script in scripts)
        {
            if (newContent.Contains(script, StringComparison.OrdinalIgnoreCase))
                continue;

            newContent = InsertBeforeBodyEnd(newContent, $"    <script src=\"{script}\"></script>");
        }

        if (!string.Equals(content, newContent, StringComparison.Ordinal))
            AddReplaceEdit(scriptFile, content, newContent, "Add BlazorExpress.ChartJS script references.", edits);
    }

    private static void AddPageEdit(ProjectContext project, string hostModel, GeneratedChartExample generated, List<FileEdit> edits)
    {
        var pagesDirectory = hostModel switch
        {
            "BlazorWebApp" when Directory.Exists(Path.Combine(project.RootDirectory, "Components", "Pages")) => Path.Combine(project.RootDirectory, "Components", "Pages"),
            _ when Directory.Exists(Path.Combine(project.RootDirectory, "Pages")) => Path.Combine(project.RootDirectory, "Pages"),
            _ => Path.Combine(project.RootDirectory, "Pages"),
        };

        var pagePath = Path.Combine(pagesDirectory, $"{generated.PageName}.razor");
        var original = File.Exists(pagePath) ? File.ReadAllText(pagePath) : "";
        AddReplaceEdit(pagePath, original, generated.Code, $"Create or update generated {generated.ChartType} chart page.", edits);
    }

    private static void AddNavigationEdit(string root, string route, string title, List<FileEdit> edits, List<string> manualSteps)
    {
        var navFiles = Directory.GetFiles(root, "NavMenu.razor", SearchOption.AllDirectories)
            .Where(x => !x.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)
                && !x.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (navFiles.Count != 1)
        {
            manualSteps.Add($"Add a navigation link to {route} ({title}) in your app navigation.");
            return;
        }

        var navPath = navFiles[0];
        var content = File.ReadAllText(navPath);
        if (content.Contains($"href=\"{route.TrimStart('/')}\"", StringComparison.OrdinalIgnoreCase)
            || content.Contains($"href=\"{route}\"", StringComparison.OrdinalIgnoreCase))
            return;

        var navLink = $"        <NavLink class=\"nav-link\" href=\"{route.TrimStart('/')}\">{title}</NavLink>";
        var lines = content.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n').ToList();
        var lastNavLinkIndex = lines.FindLastIndex(x => x.Contains("<NavLink", StringComparison.OrdinalIgnoreCase));

        if (lastNavLinkIndex < 0)
        {
            manualSteps.Add($"Add a navigation link to {route} ({title}) in {navPath}.");
            return;
        }

        lines.Insert(lastNavLinkIndex + 1, navLink);
        var newContent = string.Join(Environment.NewLine, lines);
        AddReplaceEdit(navPath, content, newContent, "Add generated chart page to NavMenu.razor.", edits);
    }

    private static string InsertBeforeBodyEnd(string content, string line)
    {
        var bodyEnd = content.LastIndexOf("</body>", StringComparison.OrdinalIgnoreCase);
        if (bodyEnd >= 0)
            return content.Insert(bodyEnd, line + Environment.NewLine);

        return AppendLine(content, line);
    }

    private static string AppendLine(string content, string line)
    {
        if (string.IsNullOrEmpty(content))
            return line + Environment.NewLine;

        return content.EndsWith(Environment.NewLine, StringComparison.Ordinal)
            ? content + line + Environment.NewLine
            : content + Environment.NewLine + line + Environment.NewLine;
    }

    private static string? FindFirst(string root, string fileName) =>
        Directory.GetFiles(root, fileName, SearchOption.AllDirectories)
            .Where(x => !x.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)
                && !x.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
            .OrderBy(x => x.Length)
            .FirstOrDefault();

    private static void AddReplaceEdit(string path, string originalContent, string newContent, string description, List<FileEdit> edits)
    {
        if (string.Equals(originalContent, newContent, StringComparison.Ordinal))
            return;

        edits.Add(new FileEdit
        {
            Path = Path.GetFullPath(path),
            Operation = File.Exists(path) ? "replace" : "create",
            OriginalHash = File.Exists(path) ? HashText(originalContent) : null,
            NewContent = newContent,
            Description = description,
        });
    }

    private static bool IsPathInside(string root, string path)
    {
        var rootWithSeparator = root.EndsWith(Path.DirectorySeparatorChar)
            ? root
            : root + Path.DirectorySeparatorChar;
        return path.StartsWith(rootWithSeparator, StringComparison.OrdinalIgnoreCase)
            || string.Equals(root, path, StringComparison.OrdinalIgnoreCase);
    }

    private static string HashText(string text)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }

    private sealed record ProjectContext(string ProjectFilePath, string RootDirectory);
}
