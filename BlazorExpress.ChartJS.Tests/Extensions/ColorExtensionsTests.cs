using System.Drawing;
using System.Globalization;

namespace BlazorExpress.ChartJS.Tests.Extensions;

/// <summary>
/// Locks the colour strings sent to Chart.js (quickfix QF-26112402, Blazor Bootstrap issue #924): the alpha value of
/// <see cref="ColorExtensions.ToRgbaString" /> always uses a decimal point, so a decimal-comma culture cannot turn the colour black.
/// </summary>
public class ColorExtensionsTests
{
    #region Methods

    #region Public Methods

    [Theory]
    [InlineData("en-US")]
    [InlineData("it-IT")]
    [InlineData("de-DE")]
    [InlineData("fr-FR")]
    [InlineData("")]
    public void ToRgbaString_InAnyCulture_UsesADecimalPoint(string cultureName)
    {
        var rgba = InCulture(cultureName, () => Color.FromArgb(255, 99, 132).ToRgbaString(0.5));

        Assert.Equal("RGBA(255, 99, 132, 0.5)", rgba);
    }

    [Theory]
    [InlineData("en-US")]
    [InlineData("it-IT")]
    public void ToRgbaString_WithDefaultAlpha_UsesADecimalPoint(string cultureName)
    {
        var rgba = InCulture(cultureName, () => Color.FromArgb(54, 162, 235).ToRgbaString());

        Assert.Equal("RGBA(54, 162, 235, 0.2)", rgba);
    }

    [Fact]
    public void ToRgbString_InADecimalCommaCulture_IsUnchanged()
    {
        var rgb = InCulture("it-IT", () => Color.FromArgb(255, 99, 132).ToRgbString());

        Assert.Equal("RGB(255, 99, 132)", rgb);
    }

    #endregion

    #region Private Methods

    private static string InCulture(string cultureName, Func<string> action)
    {
        var originalCulture = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
            return action();
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    #endregion

    #endregion
}
