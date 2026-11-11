namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>The controls every playground panel shares: legend, locale and right-to-left.</summary>
public partial class PlaygroundCommonControls : ComponentBase
{
    #region Fields and Constants

    private static readonly IReadOnlyList<(string Value, string Text)> legendPositions =
        PlaygroundSettings.LegendPositions.Select(position => (position, position)).ToList();

    #endregion

    #region Methods

    #region Private Methods

    private async Task SetAsync(Action change)
    {
        change();
        await OnChanged.InvokeAsync();
    }

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>Raised after a setting changed.</summary>
    [Parameter] public EventCallback OnChanged { get; set; }

    [Parameter, EditorRequired] public PlaygroundSettings Settings { get; set; } = default!;

    #endregion
}
