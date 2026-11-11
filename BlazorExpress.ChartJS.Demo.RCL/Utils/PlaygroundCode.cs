using System.Globalization;
using System.Text;

namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// Builds the C# a playground panel shows: one statement for each setting that differs from its default,
/// written with the library's own property names so it can be pasted into an app.
/// </summary>
public sealed class PlaygroundCode
{
    #region Fields and Constants

    private readonly List<string> datasetLines = new();
    private readonly string datasetType;
    private readonly List<string> optionLines = new();
    private readonly string optionsType;

    #endregion

    #region Constructors and Finalizers

    /// <param name="optionsType">The options class name, for example <c>LineChartOptions</c>.</param>
    /// <param name="datasetType">The dataset class name, for example <c>LineChartDataset</c>.</param>
    public PlaygroundCode(string optionsType, string datasetType)
    {
        this.optionsType = optionsType;
        this.datasetType = datasetType;
    }

    #endregion

    #region Methods

    #region Public Methods

    /// <summary>Adds <c>dataset.&lt;property&gt; = &lt;literal&gt;;</c>.</summary>
    public PlaygroundCode Dataset(string property, string literal)
    {
        datasetLines.Add($"dataset.{property} = {literal};");

        return this;
    }

    public static string List(double value) => $"[{Literal(value)}]";

    public static string List(string value) => $"[{Literal(value)}]";

    public static string Literal(double value) => value.ToString("0.##", CultureInfo.InvariantCulture);

    public static string Literal(string value) => $"\"{value.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"";

    /// <summary>Adds <c>options.&lt;path&gt; = &lt;literal&gt;;</c>.</summary>
    public PlaygroundCode Option(string path, string literal)
    {
        optionLines.Add($"options.{path} = {literal};");

        return this;
    }

    public override string ToString()
    {
        var text = new StringBuilder();
        text.AppendLine(optionLines.Count == 0 && datasetLines.Count == 0
            ? "// Every setting is at its default."
            : "// Only the settings that differ from their defaults are listed.");
        text.AppendLine($"var options = new {optionsType} {{ Responsive = true }};");

        foreach (var line in optionLines)
            text.AppendLine(line);

        if (datasetLines.Count > 0)
        {
            text.AppendLine();
            text.AppendLine($"// On each {datasetType}:");

            foreach (var line in datasetLines)
                text.AppendLine(line);
        }

        return text.ToString().TrimEnd();
    }

    #endregion

    #endregion
}
