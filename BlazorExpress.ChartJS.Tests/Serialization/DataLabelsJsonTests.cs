using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Serialization;

/// <summary>
/// Locks how data label alignment and anchor reach Chart.js (work item 26112101, AC-03): every alignment is sent under the
/// chartjs-plugin-datalabels name of the same value, and an anchor that was never set is not sent.
/// </summary>
public class DataLabelsJsonTests
{
    #region Methods

    #region Public Methods

    [Theory]
    [InlineData(DataLabelAlignment.Center, "center")]
    [InlineData(DataLabelAlignment.Start, "start")]
    [InlineData(DataLabelAlignment.End, "end")]
    [InlineData(DataLabelAlignment.Left, "left")]
    [InlineData(DataLabelAlignment.Top, "top")]
    [InlineData(DataLabelAlignment.Right, "right")]
    [InlineData(DataLabelAlignment.Bottom, "bottom")]
    public void Alignment_IsSentUnderItsDatalabelsName(DataLabelAlignment alignment, string expected)
    {
        var json = ChartTestData.Serialize(new ChartDatasetDataLabels { Alignment = alignment });

        Assert.Equal(expected, json.GetProperty("align").GetString());
    }

    [Fact]
    public void Anchor_WhenNeverSet_IsNotSent()
    {
        var json = ChartTestData.Serialize(new ChartDatasetDataLabels());

        Assert.False(json.TryGetProperty("anchor", out _));
    }

    [Theory]
    [InlineData(DataLabelAnchor.Start, "start")]
    [InlineData(DataLabelAnchor.Center, "center")]
    [InlineData(DataLabelAnchor.End, "end")]
    public void Anchor_WhenSet_IsSent(DataLabelAnchor anchor, string expected)
    {
        var json = ChartTestData.Serialize(new ChartDatasetDataLabels { Anchor = anchor });

        Assert.Equal(expected, json.GetProperty("anchor").GetString());
    }

    #endregion

    #endregion
}
