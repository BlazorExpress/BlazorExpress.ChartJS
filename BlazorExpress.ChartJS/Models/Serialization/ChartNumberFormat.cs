namespace BlazorExpress.ChartJS;

/// <summary>
/// <c>Intl.NumberFormat</c> options for the tick labels of a numeric axis (<see cref="ChartAxesTicks.Format" />), for example
/// <c>new ChartNumberFormat { Style = "currency", Currency = "USD" }</c>. Only the options that are set are sent; the values are
/// passed to <c>Intl.NumberFormat</c> with <see cref="ChartOptions.Locale" />.
/// <para>
/// See <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" /> for more information.
/// </para>
/// </summary>
public sealed class ChartNumberFormat
{
    #region Properties, Indexers

    /// <summary>
    /// Gets or sets how compact notation is shown: <c>"short"</c> (for example 1.2K) or <c>"long"</c> (1.2 thousand).
    /// Used only when <see cref="Notation" /> is <c>"compact"</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets how compact notation is shown: \"short\" or \"long\". Used only when Notation is \"compact\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? CompactDisplay { get; set; }

    /// <summary>
    /// Gets or sets the ISO 4217 currency code, for example <c>"USD"</c> or <c>"EUR"</c>. Required when <see cref="Style" /> is <c>"currency"</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the ISO 4217 currency code, for example \"USD\" or \"EUR\". Required when Style is \"currency\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Currency { get; set; }

    /// <summary>
    /// Gets or sets how the currency is shown: <c>"symbol"</c>, <c>"narrowSymbol"</c>, <c>"code"</c> or <c>"name"</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets how the currency is shown: \"symbol\", \"narrowSymbol\", \"code\" or \"name\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? CurrencyDisplay { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of fraction digits, from 0 to 100.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the maximum number of fraction digits, from 0 to 100.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? MaximumFractionDigits { get; set; }

    /// <summary>
    /// Gets or sets the minimum number of fraction digits, from 0 to 100.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the minimum number of fraction digits, from 0 to 100.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? MinimumFractionDigits { get; set; }

    /// <summary>
    /// Gets or sets the minimum number of integer digits, from 1 to 21; shorter numbers are padded with leading zeros.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the minimum number of integer digits, from 1 to 21; shorter numbers are padded with leading zeros.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("int?")]
    public int? MinimumIntegerDigits { get; set; }

    /// <summary>
    /// Gets or sets the notation: <c>"standard"</c>, <c>"scientific"</c>, <c>"engineering"</c> or <c>"compact"</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the notation: \"standard\", \"scientific\", \"engineering\" or \"compact\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Notation { get; set; }

    /// <summary>
    /// Gets or sets when the sign is shown: <c>"auto"</c>, <c>"always"</c>, <c>"exceptZero"</c>, <c>"negative"</c> or <c>"never"</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets when the sign is shown: \"auto\", \"always\", \"exceptZero\", \"negative\" or \"never\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? SignDisplay { get; set; }

    /// <summary>
    /// Gets or sets the formatting style: <c>"decimal"</c>, <c>"currency"</c>, <c>"percent"</c> or <c>"unit"</c>.
    /// With <c>"percent"</c>, 0.25 is shown as 25%.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the formatting style: \"decimal\", \"currency\", \"percent\" or \"unit\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Style { get; set; }

    /// <summary>
    /// Gets or sets the unit, for example <c>"kilometer-per-hour"</c> or <c>"celsius"</c>. Required when <see cref="Style" /> is <c>"unit"</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets the unit, for example \"kilometer-per-hour\" or \"celsius\". Required when Style is \"unit\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? Unit { get; set; }

    /// <summary>
    /// Gets or sets how the unit is shown: <c>"short"</c>, <c>"narrow"</c> or <c>"long"</c>.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets how the unit is shown: \"short\", \"narrow\" or \"long\".")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("string?")]
    public string? UnitDisplay { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether grouping separators, such as thousands separators, are used.
    /// <para>
    /// Default value is <see langword="null" />.
    /// </para>
    /// <see href="https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat#options" />
    /// </summary>
    [AddedVersion("2.0.0")]
    [DefaultValue(null)]
    [Description("Gets or sets a value indicating whether grouping separators, such as thousands separators, are used.")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [ParameterTypeName("bool?")]
    public bool? UseGrouping { get; set; }

    #endregion
}
