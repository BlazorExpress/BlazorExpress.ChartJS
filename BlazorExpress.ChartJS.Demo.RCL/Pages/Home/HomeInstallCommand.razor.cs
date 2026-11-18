namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The package install command on the home page with a copy button (story 26111801 AC-02).
/// A successful copy shows and announces "Copied"; a failed copy resets quietly and the command stays selectable as text.
/// </summary>
public partial class HomeInstallCommand : ComponentBase, IDisposable
{
    #region Fields and Constants

    /// <summary>The text the copy button puts on the clipboard.</summary>
    public const string Command = "dotnet add package BlazorExpress.ChartJS";

    private const string CopiedText = "Copied";

    private DotNetObjectReference<HomeInstallCommand>? dotNetRef;
    private bool isCopied;

    #endregion

    #region Methods

    #region Public Methods

    public void Dispose() => dotNetRef?.Dispose();

    [JSInvokable]
    public void OnCopyFailJS(object? error) => SetCopied(false);

    [JSInvokable]
    public void OnCopySuccessJS() => SetCopied(true);

    [JSInvokable]
    public void ResetCopyStatusJS() => SetCopied(false);

    #endregion

    #region Private Methods

    // Uses the existing clipboard helper of the demo site, which reports back through the three callbacks above.
    private async Task CopyAsync()
    {
        dotNetRef ??= DotNetObjectReference.Create(this);
        await JS.TryInvokeVoidAsync("copyToClipboard", Command, dotNetRef);
    }

    private void SetCopied(bool copied)
    {
        isCopied = copied;
        StateHasChanged();
    }

    #endregion

    #endregion

    #region Properties, Indexers

    [Inject] private IJSRuntime JS { get; set; } = default!;

    #endregion
}
