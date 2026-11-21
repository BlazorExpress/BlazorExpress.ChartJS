using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Serialization;

/// <summary>
/// Locks how chart and dataset animation reach Chart.js (work item 26112101, AC-05): <see cref="ChartAnimation.Disabled" />
/// is sent as <c>false</c>, any other animation as its usual object, and an unset animation not at all.
/// </summary>
public class AnimationJsonTests
{
    #region Methods

    #region Public Methods

    [Fact]
    public void ChartOptions_WithDisabledAnimation_SendsFalse()
    {
        var json = ChartTestData.Serialize(new LineChartOptions { Animation = ChartAnimation.Disabled });

        Assert.Equal(JsonValueKind.False, json.GetProperty("animation").ValueKind);
    }

    [Fact]
    public void Dataset_WithDisabledAnimation_SendsFalse()
    {
        var json = ChartTestData.Serialize(new LineChartDataset { Label = "Sales", Animation = ChartAnimation.Disabled });

        Assert.Equal(JsonValueKind.False, json.GetProperty("animation").ValueKind);
    }

    [Fact]
    public void ChartOptions_WithAnimationSettings_SendsTheObject()
    {
        var json = ChartTestData.Serialize(new LineChartOptions { Animation = new ChartAnimation { Duration = 500, Easing = "linear" } });

        var animation = json.GetProperty("animation");
        Assert.Equal(JsonValueKind.Object, animation.ValueKind);
        Assert.Equal(500, animation.GetProperty("duration").GetDouble());
        Assert.Equal("linear", animation.GetProperty("easing").GetString());
    }

    [Fact]
    public void ChartOptions_WithoutAnimation_SendsNoAnimationKey()
    {
        var json = ChartTestData.Serialize(new LineChartOptions());

        Assert.False(json.TryGetProperty("animation", out _));
    }

    [Fact]
    public void Disabled_ReturnsANewValueEachTime() =>
        Assert.NotSame(ChartAnimation.Disabled, ChartAnimation.Disabled);

    #endregion

    #endregion
}
