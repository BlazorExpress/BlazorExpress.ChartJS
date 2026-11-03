using System.Reflection;
using System.Text.RegularExpressions;
using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Interop;

/// <summary>
/// Checks that every public C# interop constant names a function the shipped <c>blazorexpress.chartjs.js</c> defines.
/// </summary>
public partial class InteropConstantsTests
{
    #region Fields and Constants

    private static readonly string script = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Assets", "blazorexpress.chartjs.js"));

    #endregion

    #region Methods

    #region Public Methods

    [Theory]
    [MemberData(nameof(InteropConstants))]
    public void InteropConstant_ResolvesToScriptFunction(string owner, string path)
    {
        Assert.StartsWith(ChartTestData.InteropPrefix, path);

        var segments = path[ChartTestData.InteropPrefix.Length..].Split('.');
        var (ns, function) = segments.Length == 1 ? (null, segments[0]) : (segments[0], segments[1]);

        if (ns is not null)
            Assert.True(ChartTypeModules().Contains(ns), $"{owner}: namespace '{ns}' is not registered in the script.");

        Assert.True(CoreFunctions().Contains(function), $"{owner}: function '{function}' is not defined in the script.");
    }

    [Fact]
    public void Script_DoesNotDefineUpdateDataValues() => Assert.DoesNotContain("updateDataValues", script);

    [Fact]
    public void Script_RegistersEveryChartTypeModule() =>
        Assert.Equal(["bar", "bubble", "doughnut", "line", "pie", "polarArea", "radar", "scatter"], ChartTypeModules().Order());

    #endregion

    #region Private Methods

    [GeneratedRegex(@"window\.blazorexpress\.chartjs\.(\w+) = createChartTypeModule\(")]
    private static partial Regex ChartTypeModuleRegex();

    private static HashSet<string> ChartTypeModules() =>
        ChartTypeModuleRegex().Matches(script).Select(m => m.Groups[1].Value).ToHashSet();

    [GeneratedRegex(@"^\s{8}(\w+): (?:async )?\(", RegexOptions.Multiline)]
    private static partial Regex CoreFunctionRegex();

    private static HashSet<string> CoreFunctions()
    {
        var start = script.IndexOf("const core = {", StringComparison.Ordinal);
        var end = script.IndexOf("\n    };", start, StringComparison.Ordinal);

        return CoreFunctionRegex().Matches(script[start..end]).Select(m => m.Groups[1].Value).ToHashSet();
    }

    #endregion

    #endregion

    #region Properties, Indexers

    public static TheoryData<string, string> InteropConstants
    {
        get
        {
            var data = new TheoryData<string, string>();

            foreach (var type in typeof(ChartInterop).Assembly.GetTypes().Where(t => t.IsPublic && t.Name.EndsWith("Interop", StringComparison.Ordinal)))
                foreach (var constant in type.GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.IsLiteral && f.FieldType == typeof(string)))
                    data.Add($"{type.Name}.{constant.Name}", (string)constant.GetRawConstantValue()!);

            return data;
        }
    }

    #endregion
}
