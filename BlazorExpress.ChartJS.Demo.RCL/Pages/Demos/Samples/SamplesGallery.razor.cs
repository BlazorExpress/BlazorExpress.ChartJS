namespace BlazorExpress.ChartJS.Demo.RCL.Pages.Demos.Samples;

/// <summary>
/// The samples gallery: every sample as a card, grouped by category, with a search box and chart, category and tag filters.
/// The filters live in the query string (<c>q</c>, <c>chart</c>, <c>category</c>, <c>tag</c>), so a filtered view can be shared.
/// </summary>
public partial class SamplesGallery : ComponentBase
{
    #region Fields and Constants

    private const string CategoryParameter = "category";
    private const string ChartParameter = "chart";
    private const string metaDescription = "Browse every BlazorExpress.ChartJS sample: bar, line, area, pie, doughnut, polar area, radar, scatter and bubble charts, with search and filters. Each sample has a live demo and its source.";
    private const string metaTitle = "Blazor Chart Samples Gallery";
    private const string pageDescription = "Every sample in one place. Search by name, or filter by chart, category or tag. Each card opens a page with the live demo and its source.";
    private const string pageUrl = DemoRouteConstants.Demos_Samples;
    private const string QueryParameter = "q";
    private const string TagParameter = "tag";

    private static readonly IReadOnlyList<SampleCategory> categories = Enum.GetValues<SampleCategory>();
    private static readonly IReadOnlyList<string> tags = SampleCatalog.All.SelectMany(s => s.Tags).Append(SampleInfo.NewIn130Tag).Distinct().Order().ToList();

    private List<SampleInfo> shownSamples = new();
    private List<UnavailableSample> shownUnavailable = new();

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override void OnParametersSet()
    {
        var terms = (Query ?? string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var category = SampleCategoryExtensions.FromSlug(Category);
        var hasCategory = !string.IsNullOrWhiteSpace(Category);

        shownSamples = SampleCatalog.All
            .Where(s => string.IsNullOrWhiteSpace(Chart) || s.ChartKey == Chart)
            .Where(s => !hasCategory || s.Category == category)
            .Where(s => string.IsNullOrWhiteSpace(Tag) || s.HasTag(Tag))
            .Where(s => terms.All(term => Matches(s, term)))
            .ToList();

        // Not-yet-available samples belong to no chart page and have no tags, so a chart or tag filter hides them.
        shownUnavailable = !string.IsNullOrWhiteSpace(Chart) || !string.IsNullOrWhiteSpace(Tag)
            ? new()
            : SampleCatalog.Unavailable
                .Where(s => !hasCategory || s.Category == category)
                .Where(s => terms.All(term => Contains(s.Title, term) || Contains(s.Description, term) || Contains(s.WaitingFor, term)))
                .ToList();
    }

    #endregion

    #region Private Methods

    private void ClearFilters() => NavigationManager.NavigateTo(pageUrl, replace: true);

    private static bool Contains(string text, string term) => text.Contains(term, StringComparison.OrdinalIgnoreCase);

    private static bool Matches(SampleInfo sample, string term) =>
        Contains(sample.Title, term)
        || Contains(sample.Description, term)
        || Contains(sample.Category.GetDisplayName(), term)
        || Contains(SampleCatalog.GetChart(sample.ChartKey)?.Name ?? string.Empty, term)
        || sample.Tags.Any(tag => Contains(tag, term))
        || (sample.IsNewIn130 && Contains(SampleInfo.NewIn130Tag, term));

    private void OnSearchInput(ChangeEventArgs e) => SetFilter(QueryParameter, e.Value);

    // Keeps the filters in the query string. The values are only compared with catalog data and written as text.
    private void SetFilter(string name, object? value)
    {
        var text = value?.ToString();
        var uri = NavigationManager.GetUriWithQueryParameter(name, string.IsNullOrEmpty(text) ? null : text);
        NavigationManager.NavigateTo(uri, replace: true);
    }

    #endregion

    #endregion

    #region Properties, Indexers

    [SupplyParameterFromQuery(Name = CategoryParameter)] public string? Category { get; set; }

    [SupplyParameterFromQuery(Name = ChartParameter)] public string? Chart { get; set; }

    private bool HasFilter => !string.IsNullOrEmpty(Query) || !string.IsNullOrEmpty(Chart) || !string.IsNullOrEmpty(Category) || !string.IsNullOrEmpty(Tag);

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [SupplyParameterFromQuery(Name = QueryParameter)] public string? Query { get; set; }

    // The tag list entry the filter uses. The query value is matched without case, so the box shows the listed spelling.
    private string SelectedTag => tags.FirstOrDefault(tag => string.Equals(tag, Tag, StringComparison.OrdinalIgnoreCase)) ?? string.Empty;

    [SupplyParameterFromQuery(Name = TagParameter)] public string? Tag { get; set; }

    #endregion
}
