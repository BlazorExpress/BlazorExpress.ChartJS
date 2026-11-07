namespace BlazorExpress.ChartJS;

/// <summary>
/// Writes an enum as its camelCase name, for example <see cref="InteractionMode.Nearest" /> as <c>"nearest"</c>.
/// </summary>
internal sealed class CamelCaseEnumConverter<TEnum> : JsonStringEnumConverter<TEnum> where TEnum : struct, Enum
{
    #region Constructors and Finalizers

    public CamelCaseEnumConverter() : base(JsonNamingPolicy.CamelCase, allowIntegerValues: false) { }

    #endregion
}
