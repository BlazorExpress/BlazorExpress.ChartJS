namespace BlazroExpress.ChartJS.Demo.RCL;

public partial class EmptyLayout : LayoutComponentBase
{
    private string version = default!;
    private string docsUrl = default!;
    private string blogUrl = default!;
    private string githubUrl = default!;
    private string twitterUrl = default!;
    private string linkedInUrl = default!;
    private string openCollectiveUrl = default!;
    private string githubIssuesUrl = default!;
    private string githubDiscussionsUrl = default!;
    private string stackoverflowUrl = default!;

    [Inject] public IConfiguration Configuration { get; set; } = default!;

    protected override void OnInitialized()
    {
        version = $"v{Configuration["version"]}"; // example: v0.6.1
        docsUrl = $"{Configuration["urls:docs"]}";
        blogUrl = $"{Configuration["urls:blog"]}";
        githubUrl = $"{Configuration["urls:github"]}";
        twitterUrl = $"{Configuration["urls:twitter"]}";
        linkedInUrl = $"{Configuration["urls:linkedin"]}";
        openCollectiveUrl = $"{Configuration["urls:opencollective"]}";
        githubIssuesUrl = $"{Configuration["urls:github_issues"]}";
        githubDiscussionsUrl = $"{Configuration["urls:github_discussions"]}";
        stackoverflowUrl = $"{Configuration["urls:stackoverflow"]}";

        base.OnInitialized();
    }
}
