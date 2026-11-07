using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Serialization;

/// <summary>
/// Locks how the union value types are written to JSON, and that they are never read from JSON.
/// </summary>
public class ConverterJsonTests
{
    #region Methods

    #region Public Methods

    [Fact]
    public void BarChartDataValue_WritesNumbersRangesAndNulls()
    {
        List<BarChartDataValue?> values = [5, (2, 8), null, BarChartDataValue.Range(-1.5, 3)];

        Assert.Equal("[5,[2,8],null,[-1.5,3]]", ChartTestData.Serialize(values).GetRawText());
    }

    [Fact]
    public void BarChartDataValue_ExposesItsParts()
    {
        BarChartDataValue plain = 4;
        BarChartDataValue range = (1, 6);

        Assert.False(plain.IsRange);
        Assert.Equal(4, plain.Value);
        Assert.True(range.IsRange);
        Assert.Equal((1d, 6d), (range.Start!.Value, range.End!.Value));
        Assert.Null(range.Value);
    }

    [Fact]
    public void ChartBorderRadius_WritesOnlyTheCornersThatAreSet() =>
        Assert.Equal("{\"topLeft\":4,\"topRight\":8}", ChartTestData.Serialize(new ChartBorderRadius { TopLeft = 4, TopRight = 8 }).GetRawText());

    [Fact]
    public void ChartClip_WritesEverySide() =>
        Assert.Equal("{\"bottom\":0,\"left\":5,\"right\":-2,\"top\":0}", ChartTestData.Serialize(new ChartClip { Left = 5, Right = -2 }).GetRawText());

    [Theory]
    [InlineData(80d, "80")]
    [InlineData(0.5d, "0.5")]
    public void ChartDimension_WritesANumber(double value, string expected) =>
        Assert.Equal(expected, ChartTestData.Serialize((ChartDimension)value).GetRawText());

    [Fact]
    public void ChartDimension_WritesAString()
    {
        ChartDimension dimension = "50%";

        Assert.Equal("\"50%\"", ChartTestData.Serialize(dimension).GetRawText());
        Assert.Equal("50%", dimension.Text);
        Assert.Null(dimension.Number);
    }

    [Fact]
    public void ChartDimension_FromNullStringIsNull()
    {
        ChartDimension? dimension = (string?)null;

        Assert.Null(dimension);
    }

    [Fact]
    public void ChartPadding_WritesANumberWhenEverySideIsEqual()
    {
        ChartPadding padding = 10;

        Assert.Equal("10", ChartTestData.Serialize(padding).GetRawText());
        Assert.Equal("10", ChartTestData.Serialize(new ChartPadding { Top = 10, Right = 10, Bottom = 10, Left = 10 }).GetRawText());
    }

    [Fact]
    public void ChartPadding_WritesTheSidesThatAreSet()
    {
        Assert.Equal("{\"top\":10,\"bottom\":4}", ChartTestData.Serialize(new ChartPadding { Top = 10, Bottom = 4 }).GetRawText());
        Assert.Equal("{}", ChartTestData.Serialize(new ChartPadding()).GetRawText());
    }

    [Fact]
    public void UnionValueTypes_CannotBeReadFromJson()
    {
        Assert.Throws<NotSupportedException>(() => JsonSerializer.Deserialize<ChartDimension>("1", ChartTestData.InteropJsonOptions));
        Assert.Throws<NotSupportedException>(() => JsonSerializer.Deserialize<ChartPadding>("1", ChartTestData.InteropJsonOptions));
        Assert.Throws<NotSupportedException>(() => JsonSerializer.Deserialize<BarChartDataValue>("1", ChartTestData.InteropJsonOptions));
    }

    #endregion

    #endregion
}
