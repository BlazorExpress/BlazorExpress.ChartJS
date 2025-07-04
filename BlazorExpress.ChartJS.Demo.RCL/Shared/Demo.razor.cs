namespace BlazorExpress.ChartJS.Demo.RCL;

public partial class Demo : BulmaComponentBase
{
    #region Fields and Constants

    private string? clipboardTooltipTitle = "Copy to clipboard";

    private string? clipboardTooltipIconName = "bi bi-clipboard";

    private string? codeSnippet;

    private float codeSnippetWidth;

    /// <summary>
    /// A reference to this component instance for use in JavaScript calls.
    /// </summary>
    private DotNetObjectReference<Demo> objRef = default!;

    #endregion

    #region Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !IsFirstRenderComplete)
        {
            await JSRuntime.InvokeVoidAsync("highlightCode");
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnInitializedAsync()
    {
        objRef ??= DotNetObjectReference.Create(this);
        await base.OnInitializedAsync();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (codeSnippet is null)
        {
            var resourceName = Type.FullName + ".razor";

            using (var stream = Type.Assembly.GetManifestResourceStream(resourceName)!)
            {
                try
                {
                    if (stream is null)
                        return;

                    using (var reader = new StreamReader(stream))
                    {
                        codeSnippet = await reader.ReadToEndAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    /// <summary>
    /// Handles a copy error event from JavaScript.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    [JSInvokable]
    public void OnCopyFailJS(string errorMessage)
    {
        // TODO: show message
    }

    /// <summary>
    /// Handles a copy success event from JavaScript.
    /// </summary>
    [JSInvokable]
    public void OnCopySuccessJS()
    {
        clipboardTooltipTitle = "Copied!";
        clipboardTooltipIconName = "bi bi-check2 has-text-primary";
        StateHasChanged();
    }

    /// <summary>
    /// Handles a copy status reset event from JavaScript.
    /// </summary>
    [JSInvokable]
    public void ResetCopyStatusJS()
    {
        clipboardTooltipTitle = "Copy to clipboard";
        clipboardTooltipIconName = "bi bi-clipboard";
        StateHasChanged();
    }

    private async Task CopyToClipboardAsync()
        => await JSRuntime.InvokeVoidAsync("copyToClipboard", codeSnippet, objRef);

    #endregion

    #region Properties, Indexers

    protected override string? ClassNames
        => BuildClassNames(Class, ("be-bulma-doc-example", true));

    [Parameter] public LanguageCode LanguageCode { get; set; } = LanguageCode.Razor;

    [Parameter] public bool ShowCodeOnly { get; set; }

    [Parameter] public bool Tabs { get; set; } = false;

    [Parameter] public Type Type { get; set; } = default!;

    #endregion
}
