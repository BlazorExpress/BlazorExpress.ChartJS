namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The layout of a playground panel: the option controls beside the chart, and below them the C# for the current settings with a copy button.
/// </summary>
public partial class PlaygroundPanel : ComponentBase, IDisposable
{
    #region Fields and Constants

    private const string CopiedText = "Copied";
    private const string CopyFailedText = "Copy failed";
    private const string CopyText = "Copy";

    private readonly string codeLabelId = $"be-playground-code-{Guid.NewGuid():N}";
    private string copyText = CopyText;
    private DotNetObjectReference<PlaygroundPanel>? dotNetRef;

    #endregion

    #region Methods

    #region Public Methods

    public void Dispose() => dotNetRef?.Dispose();

    [JSInvokable]
    public void OnCopyFailJS(object? error) => SetCopyText(CopyFailedText);

    [JSInvokable]
    public void OnCopySuccessJS() => SetCopyText(CopiedText);

    [JSInvokable]
    public void ResetCopyStatusJS() => SetCopyText(CopyText);

    #endregion

    #region Private Methods

    // Uses the existing clipboard helper of the demo site, which reports back through the three callbacks above.
    private async Task CopyAsync()
    {
        dotNetRef ??= DotNetObjectReference.Create(this);
        await JS.InvokeVoidAsync("copyToClipboard", Code, dotNetRef);
    }

    private void SetCopyText(string text)
    {
        copyText = text;
        StateHasChanged();
    }

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>The chart the controls change.</summary>
    [Parameter] public RenderFragment? Chart { get; set; }

    /// <summary>The C# for the current settings.</summary>
    [Parameter] public string Code { get; set; } = string.Empty;

    /// <summary>The option controls.</summary>
    [Parameter] public RenderFragment? Controls { get; set; }

    [Inject] private IJSRuntime JS { get; set; } = default!;

    #endregion
}
