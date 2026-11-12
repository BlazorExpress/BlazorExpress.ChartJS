namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// Guarded JS interop for calls the page works without, such as the theme, clipboard copy and head-tag cleanup.
/// </summary>
public static class DemoJSRuntimeExtensions
{
    #region Methods

    /// <summary>
    /// Calls a JavaScript function and keeps the page running if the call fails.
    /// A lost Blazor Server connection (<see cref="JSDisconnectedException" />) is ignored; a JavaScript error
    /// (<see cref="JSException" />) is written to the browser console. Use it only for calls whose failure the page can live with.
    /// </summary>
    /// <param name="js">The JS runtime.</param>
    /// <param name="identifier">The JavaScript function to call, for example <c>setTheme</c>.</param>
    /// <param name="args">The arguments to pass.</param>
    public static async Task TryInvokeVoidAsync(this IJSRuntime js, string identifier, params object?[]? args)
    {
        try
        {
            await js.InvokeVoidAsync(identifier, args);
        }
        catch (JSDisconnectedException)
        {
            // The page is gone; there is nothing left to update.
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"BlazorExpress.ChartJS demo: the JavaScript call '{identifier}' failed: {ex.Message}");
        }
    }

    #endregion
}
