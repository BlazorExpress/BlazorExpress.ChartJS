namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>A labelled plain HTML checkbox for a playground panel.</summary>
public partial class PlaygroundCheckbox : ComponentBase
{
    #region Properties, Indexers

    [Parameter, EditorRequired] public string Label { get; set; } = default!;

    [Parameter] public bool Value { get; set; }

    [Parameter] public EventCallback<bool> ValueChanged { get; set; }

    #endregion
}
