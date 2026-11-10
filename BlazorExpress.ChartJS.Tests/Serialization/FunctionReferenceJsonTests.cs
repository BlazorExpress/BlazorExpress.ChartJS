using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Serialization;

/// <summary>
/// Locks the JSON form of <see cref="ChartFunction" /> references (rule "consumer JS function references").
/// </summary>
public class FunctionReferenceJsonTests
{
    #region Methods

    #region Public Methods

    [Fact]
    public void ChartFunction_WritesTheMarker() =>
        Assert.Equal("{\"$chartFunction\":\"formatCurrency\"}", ChartTestData.Serialize(new ChartFunction("formatCurrency")).GetRawText());

    [Fact]
    public void ChartFunction_WritesTheNameAsData() =>
        Assert.Equal("{\"$chartFunction\":\"a\\u0022b\"}", ChartTestData.Serialize(new ChartFunction("a\"b")).GetRawText());

    [Fact]
    public void ChartFunction_IsNeverRead() =>
        Assert.Throws<NotSupportedException>(() => JsonSerializer.Deserialize<ChartFunction>("{\"$chartFunction\":\"x\"}", ChartTestData.InteropJsonOptions));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void ChartFunction_RejectsABlankName(string name) =>
        Assert.Throws<ArgumentException>(() => new ChartFunction(name));

    [Fact]
    public void ChartFunction_RejectsANullName() =>
        Assert.Throws<ArgumentNullException>(() => new ChartFunction(null!));

    [Fact]
    public void ChartFunction_ExposesItsName()
    {
        var function = new ChartFunction("myApp.label");

        Assert.Equal("myApp.label", function.Name);
        Assert.Equal("myApp.label", function.ToString());
    }

    #endregion

    #endregion
}
