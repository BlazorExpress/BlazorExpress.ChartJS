namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>A labelled plain HTML select for a playground panel. Only a value from <see cref="Options" /> is passed on.</summary>
public partial class PlaygroundSelect : ComponentBase
{
    #region Fields and Constants

    private readonly string id = $"be-playground-{Guid.NewGuid():N}";

    #endregion

    #region Methods

    #region Private Methods

    private async Task OnChangeAsync(ChangeEventArgs e)
    {
        var value = e.Value?.ToString() ?? string.Empty;
        if (Options.Any(o => o.Value == value))
            await ValueChanged.InvokeAsync(value);
    }

    #endregion

    #endregion

    #region Properties, Indexers

    [Parameter, EditorRequired] public string Label { get; set; } = default!;

    /// <summary>The choices, as value and display text.</summary>
    [Parameter, EditorRequired] public IReadOnlyList<(string Value, string Text)> Options { get; set; } = [];

    [Parameter] public string Value { get; set; } = string.Empty;

    [Parameter] public EventCallback<string> ValueChanged { get; set; }

    #endregion
}
