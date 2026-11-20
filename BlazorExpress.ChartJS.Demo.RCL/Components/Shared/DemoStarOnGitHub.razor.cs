using Microsoft.Extensions.Configuration;

namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The "give it a star" banner under each page. It reads the package name and the GitHub link from configuration, as the
/// BlazorExpress.Bulma.Docx <c>StarOnGitHub</c> component does.
/// </summary>
public partial class DemoStarOnGitHub : ComponentBase
{
    #region Fields and Constants

    private string githubUrl = default!;
    private string nugetPackageName = default!;

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override void OnInitialized()
    {
        nugetPackageName = $"{Configuration["NugetPackageName"]}";
        githubUrl = $"{Configuration["urls:github"]}";
    }

    #endregion

    #endregion

    #region Properties, Indexers

    [Inject] private IConfiguration Configuration { get; set; } = default!;

    #endregion
}
