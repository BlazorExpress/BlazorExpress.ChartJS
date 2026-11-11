namespace BlazorExpress.ChartJS;

/// <summary>
/// A reference to a JavaScript function the app registered with the library by name.
/// Register the function once in your own JavaScript file, then reference it from C#:
/// <code>
/// // wwwroot/js/chart-functions.js, loaded after blazorexpress.chartjs.js
/// blazorexpress.chartjs.registerFunction('formatCurrency', value => '$' + value);
///
/// // C#
/// options.Scales.Y.Ticks = new() { Callback = new ChartFunction("formatCurrency") };
/// </code>
/// Only registered functions are used: a name that is not registered, such as <c>"window.open"</c>, never resolves.
/// The chart logs one console error and the option keeps its Chart.js default. The name is never evaluated as code.
/// <para>
/// See <see href="https://www.chartjs.org/docs/latest/general/options.html#scriptable-options" /> for more information.
/// </para>
/// </summary>
[JsonConverter(typeof(ChartFunctionJsonConverter))]
public sealed class ChartFunction
{
    #region Constructors and Finalizers

    /// <summary>
    /// Creates a reference to the JavaScript function registered under <paramref name="name" />.
    /// </summary>
    /// <param name="name">The name the function was registered with.</param>
    /// <exception cref="ArgumentException"><paramref name="name" /> is <see langword="null" />, empty or white space.</exception>
    [AddedVersion("2.0.0")]
    [Description("Creates a reference to the JavaScript function registered under the given name.")]
    public ChartFunction(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
    }

    #endregion

    #region Methods

    #region Public Methods

    /// <summary>
    /// Returns the registered name.
    /// </summary>
    /// <returns>The registered name.</returns>
    [AddedVersion("2.0.0")]
    [Description("Returns the registered name.")]
    public override string ToString() => Name;

    #endregion

    #endregion

    #region Properties, Indexers

    /// <summary>
    /// Gets the name the JavaScript function was registered with.
    /// <para>
    /// Default value is none: the constructor always sets the name.
    /// </para>
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets the name the JavaScript function was registered with.")]
    [ParameterTypeName("string")]
    public string Name { get; }

    #endregion
}

/// <summary>
/// Writes a <see cref="ChartFunction" /> as <c>{"$chartFunction":"&lt;name&gt;"}</c>, the marker the library's JavaScript
/// swaps for the registered function. Reading is not supported.
/// </summary>
internal sealed class ChartFunctionJsonConverter : JsonConverter<ChartFunction>
{
    #region Fields and Constants

    internal const string MarkerPropertyName = "$chartFunction";

    #endregion

    #region Methods

    #region Public Methods

    public override ChartFunction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        throw new NotSupportedException($"{nameof(ChartFunction)} is only written to JSON.");

    public override void Write(Utf8JsonWriter writer, ChartFunction value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString(MarkerPropertyName, value.Name);
        writer.WriteEndObject();
    }

    #endregion

    #endregion
}
