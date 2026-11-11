namespace BlazorExpress.ChartJS;

/// <summary>
/// The Chart.js update mode, which selects the animation an update uses.
/// <para>
///     <see href="https://www.chartjs.org/docs/latest/developers/api.html#update-mode" />
/// </para>
/// </summary>
[AddedVersion("2.0.0")]
[Description("The Chart.js update mode, which selects the animation an update uses.")]
public enum ChartUpdateMode
{
    /// <summary>
    /// Uses the default animation (calls Chart.js <c>update()</c> without a mode).
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("Uses the default animation.")]
    Default,

    /// <summary>
    /// Uses the <c>active</c> animation.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("Uses the active animation.")]
    Active,

    /// <summary>
    /// Uses the <c>hide</c> animation.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("Uses the hide animation.")]
    Hide,

    /// <summary>
    /// Applies the update without animation.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("Applies the update without animation.")]
    None,

    /// <summary>
    /// Uses the <c>reset</c> animation.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("Uses the reset animation.")]
    Reset,

    /// <summary>
    /// Uses the <c>resize</c> animation.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("Uses the resize animation.")]
    Resize,

    /// <summary>
    /// Uses the <c>show</c> animation.
    /// </summary>
    [AddedVersion("2.0.0")]
    [Description("Uses the show animation.")]
    Show
}
