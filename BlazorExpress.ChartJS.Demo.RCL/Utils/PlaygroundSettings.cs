namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// The settings every playground panel shares: legend, locale and right-to-left.
/// Each value comes from a fixed list or a checkbox, so no free text reaches the chart options.
/// </summary>
public sealed class PlaygroundSettings
{
    #region Fields and Constants

    public const string DefaultLegendPosition = "top";

    public static readonly IReadOnlyList<string> LegendPositions = ["top", "left", "bottom", "right"];

    // The first entry leaves Locale unset, so Chart.js uses the browser's locale.
    public static readonly IReadOnlyList<(string Code, string Name)> Locales =
    [
        (string.Empty, "Browser default"),
        ("en-US", "English (United States)"),
        ("en-IN", "English (India)"),
        ("de-DE", "German (Germany)"),
        ("fr-FR", "French (France)"),
        ("pt-BR", "Portuguese (Brazil)"),
        ("hi-IN", "Hindi (India)"),
        ("ar-EG", "Arabic (Egypt)"),
        ("ja-JP", "Japanese (Japan)"),
    ];

    #endregion

    #region Methods

    #region Public Methods

    /// <summary>Applies the shared settings to a chart's options and plugins, and records the ones that differ from their defaults.</summary>
    public void Apply(ChartOptions options, ChartPlugins plugins, PlaygroundCode code)
    {
        // Only a locale from the fixed list is passed on.
        var locale = Locales.Any(l => l.Code == Locale) ? Locale : string.Empty;
        if (locale.Length > 0)
        {
            options.Locale = locale;
            code.Option("Locale", PlaygroundCode.Literal(locale));
        }

        if (!LegendDisplay)
        {
            plugins.Legend.Display = false;
            code.Option("Plugins.Legend.Display", "false");
        }

        var position = LegendPositions.Contains(LegendPosition) ? LegendPosition : DefaultLegendPosition;
        if (position != DefaultLegendPosition)
        {
            plugins.Legend.Position = position;
            code.Option("Plugins.Legend.Position", PlaygroundCode.Literal(position));
        }

        if (RightToLeft)
        {
            // Right to left: the legend and the tooltip put their colour boxes on the right and align their text to it.
            plugins.Legend.Rtl = true;
            plugins.Legend.TextDirection = "rtl";
            plugins.Tooltip = new ChartPluginsTooltip { Rtl = true, TextDirection = "rtl" };
            code.Option("Plugins.Legend.Rtl", "true")
                .Option("Plugins.Legend.TextDirection", PlaygroundCode.Literal("rtl"))
                .Option("Plugins.Tooltip", "new ChartPluginsTooltip { Rtl = true, TextDirection = \"rtl\" }");
        }
    }

    /// <summary>For charts with x and y axes: right to left also reverses the x-axis and moves the y-axis to the right.</summary>
    public void ApplyToAxes(Scales scales, PlaygroundCode code)
    {
        if (!RightToLeft)
            return;

        scales.X!.Reverse = true;
        scales.Y!.Position = "right";
        code.Option("Scales.X!.Reverse", "true")
            .Option("Scales.Y!.Position", PlaygroundCode.Literal("right"));
    }

    #endregion

    #endregion

    #region Properties, Indexers

    public bool LegendDisplay { get; set; } = true;

    public string LegendPosition { get; set; } = DefaultLegendPosition;

    public string Locale { get; set; } = string.Empty;

    public bool RightToLeft { get; set; }

    #endregion
}
