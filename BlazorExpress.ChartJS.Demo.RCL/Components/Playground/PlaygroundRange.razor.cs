using System.Globalization;

namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>A labelled plain HTML range input for a playground panel. The value is kept between <see cref="Min" /> and <see cref="Max" />.</summary>
public partial class PlaygroundRange : ComponentBase
{
    #region Fields and Constants

    private readonly string id = $"be-playground-{Guid.NewGuid():N}";

    #endregion

    #region Methods

    #region Private Methods

    private async Task OnChangeAsync(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            await ValueChanged.InvokeAsync(Math.Clamp(value, Min, Max));
    }

    private static string Text(double value) => value.ToString("0.##", CultureInfo.InvariantCulture);

    #endregion

    #endregion

    #region Properties, Indexers

    [Parameter, EditorRequired] public string Label { get; set; } = default!;

    [Parameter] public double Max { get; set; } = 100;

    [Parameter] public double Min { get; set; }

    [Parameter] public double Step { get; set; } = 1;

    /// <summary>Text shown after the value, for example <c>%</c>.</summary>
    [Parameter] public string? Unit { get; set; }

    [Parameter] public double Value { get; set; }

    [Parameter] public EventCallback<double> ValueChanged { get; set; }

    #endregion
}
