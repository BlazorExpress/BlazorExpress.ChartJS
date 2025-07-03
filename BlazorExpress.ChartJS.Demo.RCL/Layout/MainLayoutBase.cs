namespace BlazorExpress.ChartJS.Demo.RCL;

public class MainLayoutBase : LayoutComponentBase
{
    private string version = default!;
    private string homeUrl = default!;
    private string docsUrl = default!;
    private string blogUrl = default!;
    private string githubUrl = default!;
    private string twitterUrl = default!;
    private string linkedInUrl = default!;
    private string openCollectiveUrl = default!;
    private string githubIssuesUrl = default!;
    private string githubDiscussionsUrl = default!;
    private string stackoverflowUrl = default!;

    internal Sidebar sidebar = default!;
    internal IEnumerable<NavItem> navItems = default!;

    [Inject] public IConfiguration _configuration { get; set; } = default!;

    protected override void OnInitialized()
    {
        version = $"v{_configuration["version"]}"; // example: v0.6.1
        homeUrl = $"{_configuration["urls:homeUrl"]}";
        docsUrl = $"{_configuration["urls:docs"]}";
        blogUrl = $"{_configuration["urls:blog"]}";
        githubUrl = $"{_configuration["urls:github"]}";
        twitterUrl = $"{_configuration["urls:twitter"]}";
        linkedInUrl = $"{_configuration["urls:linkedin"]}";
        openCollectiveUrl = $"{_configuration["urls:opencollective"]}";
        githubIssuesUrl = $"{_configuration["urls:github_issues"]}";
        githubDiscussionsUrl = $"{_configuration["urls:github_discussions"]}";
        stackoverflowUrl = $"{_configuration["urls:stackoverflow"]}";
        base.OnInitialized();
    }

    internal virtual async Task<SidebarDataProviderResult> SidebarDataProvider(SidebarDataProviderRequest request)
    {
        if (navItems is null)
            navItems = GetNavItems();

        return await Task.FromResult(request.ApplyTo(navItems));
    }

    internal virtual IEnumerable<NavItem> GetNavItems() => new List<NavItem>();

    public string Version => version;
    public string HomeUrl => homeUrl;
    public string DocsUrl => docsUrl;
    public string BlogUrl => blogUrl;
    public string GithubUrl => githubUrl;
    public string TwitterUrl => twitterUrl;
    public string LinkedInUrl => linkedInUrl;
    public string OpenCollectiveUrl => openCollectiveUrl;
    public string GithubIssuesUrl => githubIssuesUrl;
    public string GithubDiscussionsUrl => githubDiscussionsUrl;
    public string StackoverflowUrl => stackoverflowUrl;
}
