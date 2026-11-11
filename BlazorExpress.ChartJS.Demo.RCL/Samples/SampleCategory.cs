namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// Gallery groups. They follow the Chart.js sample groups, plus groups for features Chart.js has no sample group for.
/// </summary>
public enum SampleCategory
{
    [Description("Bar")]
    Bar,

    [Description("Line")]
    Line,

    [Description("Area")]
    Area,

    [Description("Other charts")]
    OtherCharts,

    [Description("Scales")]
    Scales,

    [Description("Scale options")]
    ScaleOptions,

    [Description("Legend")]
    Legend,

    [Description("Title")]
    Title,

    [Description("Tooltip")]
    Tooltip,

    [Description("Animations")]
    Animations,

    [Description("Advanced")]
    Advanced,

    [Description("Instance API")]
    InstanceApi,

    [Description("Time axis")]
    TimeAxis,

    [Description("JS functions")]
    JsFunctions,

    [Description("Playground")]
    Playground
}

public static class SampleCategoryExtensions
{
    #region Methods

    #region Public Methods

    /// <summary>Finds the category whose <see cref="ToSlug" /> value is <paramref name="slug" />.</summary>
    public static SampleCategory? FromSlug(string? slug) =>
        string.IsNullOrWhiteSpace(slug)
            ? null
            : Enum.GetValues<SampleCategory>().Cast<SampleCategory?>().FirstOrDefault(c => c!.Value.ToSlug() == slug);

    public static string GetDisplayName(this SampleCategory category) =>
        typeof(SampleCategory).GetField(category.ToString())!
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .Cast<DescriptionAttribute>()
            .FirstOrDefault()?.Description ?? category.ToString();

    /// <summary>The value used in the gallery's <c>category</c> query string, for example <c>scale-options</c>.</summary>
    public static string ToSlug(this SampleCategory category) =>
        category.GetDisplayName().ToLowerInvariant().Replace(' ', '-');

    #endregion

    #endregion
}
