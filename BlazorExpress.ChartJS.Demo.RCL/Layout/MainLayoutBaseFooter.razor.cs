namespace BlazorExpress.ChartJS.Demo.RCL;

public partial class MainLayoutBaseFooter : ComponentBase
{
    #region Fields and Constants

    /// <summary>
    /// Other open-source .NET libraries by the same author, shown in the footer's libraries panel.
    /// The names, descriptions and links follow each package's own NuGet metadata.
    /// </summary>
    private static readonly IReadOnlyList<FooterLibrary> libraries =
    [
        new("Blazor Bootstrap", "Blazor components built on the Bootstrap 5 CSS framework.", "https://docs.blazorbootstrap.com/", DemoImageSrcConstants.Library_BlazorBootstrap),
        new("BlazorExpress.Bulma", "Enterprise-class Blazor components built on the Bulma CSS framework.", "https://bulma.blazorexpress.com/", DemoImageSrcConstants.Library_BlazorExpressBulma),
        new("UnlimitedPDF", "A pure .NET library to create, merge and split PDFs, and convert HTML to PDF.", "https://unlimitedpdf.dev/", DemoImageSrcConstants.Library_UnlimitedPdf),
    ];

    #endregion

    #region Methods

    #region Private Methods

    /// <summary>
    /// Splits a dotted name such as <c>BlazorExpress.ChartJS</c> after each dot, so a narrow column wraps it at a dot instead of mid-word.
    /// </summary>
    private static IEnumerable<string> GetNameSegments(string name)
    {
        var start = 0;
        for (var dot = name.IndexOf('.'); dot >= 0; dot = name.IndexOf('.', start))
        {
            yield return name[start..(dot + 1)];
            start = dot + 1;
        }

        yield return name[start..];
    }

    #endregion

    #endregion

    #region Properties, Indexers

    [Parameter] public string? BlogUrl { get; set; }
    [Parameter] public string? DocsUrl { get; set; }
    [Parameter] public string? DotNetVersion { get; set; }
    [Parameter] public string? GithubDiscussionsUrl { get; set; }
    [Parameter] public string? GithubIssuesUrl { get; set; }
    [Parameter] public string? GithubUrl { get; set; }
    [Parameter] public string? LinkedInUrl { get; set; }
    [Parameter] public string? NugetUrl { get; set; }
    [Parameter] public string? OpenCollectiveUrl { get; set; }
    [Parameter] public string? StackoverflowUrl { get; set; }
    [Parameter] public string? TwitterUrl { get; set; }
    [Parameter] public string? Version { get; set; }

    #endregion

    #region Nested Types

    private sealed record FooterLibrary(string Name, string Description, string Url, string LogoSrc);

    #endregion
}
