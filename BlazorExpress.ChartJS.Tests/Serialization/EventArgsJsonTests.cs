namespace BlazorExpress.ChartJS.Tests.Serialization;

/// <summary>
/// Checks that the payloads the JavaScript module sends deserialize into the event models, and the enum mappings it relies on.
/// </summary>
public class EventArgsJsonTests
{
    #region Fields and Constants

    // Blazor JS interop uses the web defaults (camelCase, case-insensitive).
    private static readonly JsonSerializerOptions webOptions = new(JsonSerializerDefaults.Web);

    #endregion

    #region Methods

    #region Public Methods

    [Fact]
    public void ActiveElement_SerializesCamelCaseIndexes()
    {
        var json = JsonSerializer.Serialize(new ChartActiveElement(1, 3), webOptions);

        using var document = JsonDocument.Parse(json);
        Assert.Equal(1, document.RootElement.GetProperty("datasetIndex").GetInt32());
        Assert.Equal(3, document.RootElement.GetProperty("index").GetInt32());
    }

    [Fact]
    public void HoverEventArgs_DeserializesHoveredItem()
    {
        var args = JsonSerializer.Deserialize<ChartHoverEventArgs>(
            """{"datasetIndex":1,"datasetLabel":"Costs","index":2,"label":"Mar","value":38}""", webOptions)!;

        Assert.Equal(1, args.DatasetIndex);
        Assert.Equal("Costs", args.DatasetLabel);
        Assert.Equal(2, args.Index);
        Assert.Equal("Mar", args.Label);
        Assert.Equal(38, args.Value!.Value.GetInt32());
    }

    [Fact]
    public void HoverEventArgs_DeserializesLeaveNotification()
    {
        var args = JsonSerializer.Deserialize<ChartHoverEventArgs>(
            """{"datasetIndex":null,"datasetLabel":null,"index":null,"label":null,"value":null}""", webOptions)!;

        Assert.Null(args.DatasetIndex);
        Assert.Null(args.Index);
        Assert.Null(args.Label);
    }

    [Theory]
    [InlineData(ChartImageType.Png, "image/png", "png")]
    [InlineData(ChartImageType.Jpeg, "image/jpeg", "jpg")]
    [InlineData(ChartImageType.Webp, "image/webp", "webp")]
    public void ImageType_MapsToMimeTypeAndExtension(ChartImageType imageType, string mimeType, string extension)
    {
        Assert.Equal(mimeType, imageType.ToMimeType());
        Assert.Equal(extension, imageType.ToFileExtension());
    }

    [Fact]
    public void LegendClickEventArgs_DeserializesArcPayload()
    {
        var args = JsonSerializer.Deserialize<ChartLegendClickEventArgs>(
            """{"datasetIndex":null,"index":2,"text":"East","hidden":true}""", webOptions)!;

        Assert.Null(args.DatasetIndex);
        Assert.Equal(2, args.Index);
        Assert.Equal("East", args.Text);
        Assert.True(args.Hidden);
    }

    [Fact]
    public void LegendClickEventArgs_DeserializesDatasetPayload()
    {
        var args = JsonSerializer.Deserialize<ChartLegendClickEventArgs>(
            """{"datasetIndex":1,"index":null,"text":"Costs","hidden":false}""", webOptions)!;

        Assert.Equal(1, args.DatasetIndex);
        Assert.Null(args.Index);
        Assert.Equal("Costs", args.Text);
        Assert.False(args.Hidden);
    }

    [Theory]
    [InlineData(ChartUpdateMode.Default, null)]
    [InlineData(ChartUpdateMode.Active, "active")]
    [InlineData(ChartUpdateMode.Hide, "hide")]
    [InlineData(ChartUpdateMode.None, "none")]
    [InlineData(ChartUpdateMode.Reset, "reset")]
    [InlineData(ChartUpdateMode.Resize, "resize")]
    [InlineData(ChartUpdateMode.Show, "show")]
    public void UpdateMode_MapsToChartJsMode(ChartUpdateMode mode, string? expected) =>
        Assert.Equal(expected, mode.ToChartJsMode());

    #endregion

    #endregion
}
