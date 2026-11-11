namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// A live thumbnail of a sample (story 26110206 R-04). When the card scrolls into view, the sample's first demo is rendered
/// in an invisible but laid-out host, its first chart is captured as an image, and the host is removed again.
/// The image is kept by the page script per sample and theme. Until a capture succeeds, the child content (the card's icon) shows.
/// </summary>
public partial class SampleThumbnail : ComponentBase, IAsyncDisposable
{
    #region Fields and Constants

    private const int CaptureTimeoutMilliseconds = 5000;

    // One capture at a time, so a page of cards never draws many charts at once.
    private static readonly SemaphoreSlim captureQueue = new(1, 1);

    private ElementReference container;
    private DotNetObjectReference<SampleThumbnail>? dotNetRef;
    private bool hasImage;
    private ElementReference host;
    private TaskCompletionSource? hostRendered;
    private bool isCapturing;
    private bool isDisposed;
    private bool isQueued;

    #endregion

    #region Methods

    #region Protected Override Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            dotNetRef = DotNetObjectReference.Create(this);

            try
            {
                // True when the script already holds this sample's image for the current theme.
                hasImage = await JS.InvokeAsync<bool>("demoThumbnails.observe", container, dotNetRef, Sample.Url);
            }
            catch (JSException)
            {
                // The script could not watch the card; the icon stays.
                hasImage = false;
            }

            if (hasImage)
                StateHasChanged();
        }

        if (isCapturing)
            hostRendered?.TrySetResult();
    }

    #endregion

    #region Public Methods

    public async ValueTask DisposeAsync()
    {
        isDisposed = true;
        hostRendered?.TrySetResult();

        try
        {
            await JS.InvokeVoidAsync("demoThumbnails.unobserve", container);
        }
        catch (Exception exception) when (exception is JSDisconnectedException or JSException)
        {
            // The page is going away, or the script failed; there is nothing left to stop observing.
        }
        finally
        {
            dotNetRef?.Dispose();
        }
    }

    /// <summary>Called by the script when the theme changed and the kept images were dropped.</summary>
    [JSInvokable]
    public void OnInvalidated()
    {
        hasImage = false;
        StateHasChanged();
    }

    /// <summary>Called by the script when the card scrolls into view and no image is kept for it.</summary>
    [JSInvokable]
    public async Task OnVisibleAsync()
    {
        if (isQueued || isDisposed)
            return;

        isQueued = true;
        await captureQueue.WaitAsync();

        try
        {
            if (isDisposed)
                return;

            hostRendered = new TaskCompletionSource();
            isCapturing = true;
            StateHasChanged();
            await hostRendered.Task;

            // False when no chart could be captured in time; the icon then stays.
            if (!isDisposed)
                hasImage = await JS.InvokeAsync<bool>("demoThumbnails.captureInto", container, host, Sample.Url, CaptureTimeoutMilliseconds);
        }
        catch (JSException)
        {
            hasImage = false;
        }
        finally
        {
            isCapturing = false;
            isQueued = false;
            captureQueue.Release();

            if (!isDisposed)
                StateHasChanged();
        }
    }

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>Shown until the thumbnail is ready, and kept when the capture fails.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Inject] private IJSRuntime JS { get; set; } = default!;

    [Parameter, EditorRequired] public SampleInfo Sample { get; set; } = default!;

    #endregion
}
