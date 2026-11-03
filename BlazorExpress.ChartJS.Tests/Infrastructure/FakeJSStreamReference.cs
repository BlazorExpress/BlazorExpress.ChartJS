namespace BlazorExpress.ChartJS.Tests.Infrastructure;

/// <summary>
/// A browser stream stand-in for image export tests. Like the real reference, it refuses a read above the allowed size.
/// </summary>
public sealed class FakeJSStreamReference : IJSStreamReference
{
    #region Fields and Constants

    private readonly byte[] content;

    private readonly Exception? disposeException;

    private readonly Exception? readException;

    #endregion

    #region Constructors and Finalizers

    public FakeJSStreamReference(string text, long? length = null, Exception? readException = null, Exception? disposeException = null)
    {
        content = System.Text.Encoding.UTF8.GetBytes(text);
        Length = length ?? content.Length;
        this.readException = readException;
        this.disposeException = disposeException;
    }

    #endregion

    #region Methods

    #region Public Methods

    public ValueTask DisposeAsync()
    {
        IsDisposed = true;
        return disposeException is null ? ValueTask.CompletedTask : ValueTask.FromException(disposeException);
    }

    public ValueTask<Stream> OpenReadStreamAsync(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
    {
        MaxAllowedSize = maxAllowedSize;

        if (readException is not null)
            throw readException;

        // Same exception as Microsoft.JSInterop's JSStreamReference.
        if (Length > maxAllowedSize)
            throw new ArgumentOutOfRangeException(nameof(maxAllowedSize), $"The incoming data stream of length {Length} exceeds the maximum allowed length {maxAllowedSize}.");

        return ValueTask.FromResult<Stream>(new MemoryStream(content));
    }

    #endregion

    #endregion

    #region Properties, Indexers

    public bool IsDisposed { get; private set; }

    public long Length { get; }

    public long? MaxAllowedSize { get; private set; }

    #endregion
}
