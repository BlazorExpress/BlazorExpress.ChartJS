using System.Globalization;

namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The demo site's colour-blind-safe palette: the eight Okabe–Ito colours, with grey in place of black so every colour
/// stays visible on a dark page. The playground panels use it directly. Sample sources write the same values as
/// literals, so a copied sample compiles without this class.
/// </summary>
public static class DemoPalette
{
    #region Fields and Constants

    // Blue, orange, bluish green, reddish purple, sky blue, vermillion, yellow, grey.
    private static readonly (int R, int G, int B)[] values =
    [
        (0, 114, 178), (230, 159, 0), (0, 158, 115), (204, 121, 167), (86, 180, 233), (213, 94, 0), (240, 228, 66), (153, 153, 153),
    ];

    #endregion

    #region Methods

    #region Public Methods

    /// <summary>The colour at <paramref name="index" />, as <c>rgb(r, g, b)</c>. The palette repeats after eight colours.</summary>
    public static string Get(int index)
    {
        var (r, g, b) = values[index % values.Length];

        return $"rgb({r}, {g}, {b})";
    }

    /// <summary>The colour at <paramref name="index" /> with an opacity, as <c>rgba(r, g, b, a)</c>.</summary>
    public static string GetTransparent(int index, double alpha = 0.5)
    {
        var (r, g, b) = values[index % values.Length];

        return $"rgba({r}, {g}, {b}, {alpha.ToString("0.##", CultureInfo.InvariantCulture)})";
    }

    #endregion

    #endregion
}
