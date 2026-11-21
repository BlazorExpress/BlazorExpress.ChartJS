namespace BlazorExpress.ChartJS.Tests.Readme;

/// <summary>
/// The quick start both READMEs show (work item 26112001, AC-07): its code is <see cref="QuickStartBarChart" />, so it compiles
/// with every build, and these tests keep the README copies equal to it and check that it draws a chart.
/// </summary>
public class ReadmeQuickStartTests : BunitContext
{
    #region Fields and Constants

    private const string BeginMarker = "<!-- quick-start:begin -->";
    private const string EndMarker = "<!-- quick-start:end -->";

    #endregion

    #region Methods

    #region Public Methods

    [Theory]
    [InlineData("README.md")]
    [InlineData("nuget/README.md")]
    public void Readme_QuickStartCode_MatchesTheCompiledComponent(string readmePath)
    {
        var root = FindRepositoryRoot();
        var component = Normalize(File.ReadAllText(Path.Combine(root, "BlazorExpress.ChartJS.Tests", "Readme", "QuickStartBarChart.razor")));
        // The README block starts after the component's own @using line, which the README shows in _Imports.razor instead.
        var expected = Normalize(string.Join('\n', component.Split('\n').SkipWhile(line => line.StartsWith("@using", StringComparison.Ordinal) || line.Length == 0)));

        var readme = Normalize(File.ReadAllText(Path.Combine(root, readmePath)));
        var begin = readme.IndexOf(BeginMarker, StringComparison.Ordinal);
        var end = readme.IndexOf(EndMarker, StringComparison.Ordinal);
        Assert.True(begin >= 0 && end > begin, $"{readmePath} has no quick start block between {BeginMarker} and {EndMarker}.");

        var block = readme[(begin + BeginMarker.Length)..end];
        var fenceStart = block.IndexOf("```razor\n", StringComparison.Ordinal);
        var fenceEnd = block.LastIndexOf("```", StringComparison.Ordinal);
        Assert.True(fenceStart >= 0 && fenceEnd > fenceStart, $"{readmePath} quick start block has no ```razor code fence.");

        var code = Normalize(block[(fenceStart + "```razor\n".Length)..fenceEnd]);
        Assert.Equal(expected, code);
    }

    [Fact]
    public void QuickStartBarChart_Renders_AndInitializesTheChart()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;

        var component = Render<QuickStartBarChart>();

        Assert.NotNull(component.Find("canvas"));
        Assert.Contains(JSInterop.Invocations, invocation => invocation.Identifier.EndsWith("initialize", StringComparison.Ordinal));
    }

    #endregion

    #region Private Methods

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "BlazorExpress.ChartJS.slnx")))
            directory = directory.Parent;

        return directory?.FullName ?? throw new InvalidOperationException("The repository root (BlazorExpress.ChartJS.slnx) was not found.");
    }

    private static string Normalize(string text) => text.Replace("\r\n", "\n").Trim();

    #endregion

    #endregion
}
