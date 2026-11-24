namespace BlazorExpress.ChartJS;

public static class ColorExtensions
{
    #region Methods

    public static Color ToColor(this string hex)
    {
        return ColorTranslator.FromHtml(hex);
    }

    public static string ToHexaString(this Color c)
    {
        return $"#{c.R:X2}{c.G:X2}{c.B:X2}{c.A:X2}";
    }

    public static string ToHexString(this Color c)
    {
        return $"#{c.R:X2}{c.G:X2}{c.B:X2}";
    }

    /// <summary>
    /// Returns the colour as a CSS <c>RGBA(r, g, b, alpha)</c> string, for example <c>RGBA(255, 99, 132, 0.5)</c>.
    /// The alpha value always uses a decimal point, whatever the current culture, because CSS and Chart.js read only that form.
    /// </summary>
    /// <param name="c">The colour.</param>
    /// <param name="alpha">The opacity, from 0 (transparent) to 1 (opaque).</param>
    /// <returns>The CSS colour string.</returns>
    public static string ToRgbaString(this Color c, double alpha = 0.2)
    {
        return string.Create(CultureInfo.InvariantCulture, $"RGBA({c.R}, {c.G}, {c.B}, {alpha})");
    }

    public static string ToRgbString(this Color c)
    {
        return $"RGB({c.R}, {c.G}, {c.B})";
    }

    #endregion
}
