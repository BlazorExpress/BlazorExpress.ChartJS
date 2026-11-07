using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Serialization;

/// <summary>
/// Existing assignments to properties that gained a wider sibling in 1.3.0 still send the JSON they sent at <c>2f475dd</c>,
/// and the wider sibling wins when both are set.
/// </summary>
public class CompatibilityJsonTests
{
    #region Methods

    #region Public Methods

    [Fact]
    public void BarBorderRadius_SendsTheSameValuesAsBefore()
    {
        Assert.Equal("[4]", ChartTestData.Serialize(new BarChartDataset { BorderRadius = [4] }).GetProperty("borderRadius").GetRawText());
        Assert.False(ChartTestData.Serialize(new BarChartDataset()).TryGetProperty("borderRadius", out _));
        Assert.Equal("[2]", ChartTestData.Serialize(new BarChartDataset { HoverBorderRadius = [2] }).GetProperty("hoverBorderRadius").GetRawText());
        Assert.False(ChartTestData.Serialize(new BarChartDataset()).TryGetProperty("hoverBorderRadius", out _));
    }

    [Fact]
    public void BarBorderRadiusCorners_IsSentInsteadOfBorderRadius()
    {
        var dataset = new BarChartDataset { BorderRadius = [4], BorderRadiusCorners = new ChartBorderRadius { TopLeft = 8 }, HoverBorderRadius = [2], HoverBorderRadiusCorners = new ChartBorderRadius { TopRight = 3 } };
        var sent = ChartTestData.Serialize(dataset);

        Assert.Equal("{\"topLeft\":8}", sent.GetProperty("borderRadius").GetRawText());
        Assert.Equal("{\"topRight\":3}", sent.GetProperty("hoverBorderRadius").GetRawText());
    }

    [Fact]
    public void Clip_SendsTheSameStringAsBefore()
    {
        Assert.Equal("\"5\"", ChartTestData.Serialize(new BarChartDataset { Clip = "5" }).GetProperty("clip").GetRawText());
        Assert.False(ChartTestData.Serialize(new BarChartDataset()).TryGetProperty("clip", out _));
        Assert.Equal("{\"bottom\":0,\"left\":1,\"right\":0,\"top\":0}", ChartTestData.Serialize(new BarChartDataset { Clip = "5", ClipSides = new ChartClip { Left = 1 } }).GetProperty("clip").GetRawText());
    }

    [Fact]
    public void LineSegmentAndStepped_SendTheSameValuesAsBefore()
    {
        Assert.Equal("{\"borderDash\":[6,6]}", ChartTestData.Serialize(new LineChartDataset { Segment = new { borderDash = new[] { 6, 6 } } }).GetProperty("segment").GetRawText());
        Assert.True(ChartTestData.Serialize(new LineChartDataset { Stepped = true }).GetProperty("stepped").GetBoolean());
        Assert.False(ChartTestData.Serialize(new LineChartDataset()).GetProperty("stepped").GetBoolean());
    }

    [Fact]
    public void SpanGaps_SendsTheSameValueAsBefore()
    {
        Assert.True(ChartTestData.Serialize(new LineChartDataset { SpanGaps = true }).GetProperty("spanGaps").GetBoolean());
        Assert.False(ChartTestData.Serialize(new LineChartDataset()).TryGetProperty("spanGaps", out _));
        Assert.Equal("3", ChartTestData.Serialize(new LineChartDataset { SpanGaps = true, MaxSpanGap = 3 }).GetProperty("spanGaps").GetRawText());
    }

    [Fact]
    public void Stack_IsStillSentForBarAndLine()
    {
        Assert.Equal("s1", ChartTestData.Serialize(new BarChartDataset { Stack = "s1" }).GetProperty("stack").GetString());
        Assert.Equal("s1", ChartTestData.Serialize(new LineChartDataset { Stack = "s1" }).GetProperty("stack").GetString());
        Assert.False(ChartTestData.Serialize(new BarChartDataset()).TryGetProperty("stack", out _));
    }

    [Fact]
    public void LayoutPadding_SendsTheSameNumberAsBefore()
    {
        var layout = ChartTestData.Serialize(new BarChartOptions { Layout = new ChartLayout { Padding = 10 } }).GetProperty("layout");

        Assert.Equal("{\"autoPadding\":true,\"padding\":10}", layout.GetRawText());
    }

    [Fact]
    public void LayoutPadding_DefaultIsStillZero() =>
        Assert.Equal("{\"autoPadding\":true,\"padding\":0}", ChartTestData.Serialize(new LineChartOptions()).GetProperty("layout").GetRawText());

    [Fact]
    public void LayoutPaddingSides_IsSentInsteadOfPadding()
    {
        var options = new BarChartOptions { Layout = new ChartLayout { Padding = 10, PaddingSides = new ChartPadding { Left = 5 } } };

        Assert.Equal("{\"left\":5}", ChartTestData.Serialize(options).GetProperty("layout").GetProperty("padding").GetRawText());
    }

    #endregion

    #endregion
}
