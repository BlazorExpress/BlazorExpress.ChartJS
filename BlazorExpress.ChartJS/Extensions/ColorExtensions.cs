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

    public static string ToRgbaString(this Color c, double alpha = 0.2)
    {
        return $"RGBA({c.R}, {c.G}, {c.B}, {alpha})";
    }

    public static string ToRgbString(this Color c)
    {
        return $"RGB({c.R}, {c.G}, {c.B})";
    }

    #endregion
}
