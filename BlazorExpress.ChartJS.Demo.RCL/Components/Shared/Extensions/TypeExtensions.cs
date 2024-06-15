namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// Various extension methods for <see cref="Type" />.
/// </summary>
public static class TypeExtensions
{
    #region Methods



    /// <summary>
    /// Get property type name.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="propertyName"></param>
    /// <returns>string</returns>
    public static string GetMethodName(this Type type, string methodName)
    {
        if (type is null || string.IsNullOrWhiteSpace(methodName))
            return string.Empty;

        var _methods = type.GetMethods();
        var _methodName = type.GetMethodName(methodName);
        if (_methodName is null)
            return string.Empty;

        return _methodName;
    }

    /// <summary>
    /// Get property type name.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="propertyName"></param>
    /// <returns>string</returns>
    public static string GetPropertyTypeName(this Type type, string propertyName)
    {
        if (type is null || string.IsNullOrWhiteSpace(propertyName))
            return string.Empty;

        var propertyType = type.GetProperty(propertyName)?.PropertyType;
        if (propertyType is null)
            return string.Empty;

        var propertyTypeName = propertyType?.ToString();
        if (string.IsNullOrWhiteSpace(propertyTypeName))
            return string.Empty;

        if (propertyTypeName.Contains(StringConstants.PropertyTypeNameInt16, StringComparison.InvariantCulture))
            propertyTypeName = StringConstants.PropertyTypeNameInt16CSharpTypeKeyword;

        else if (propertyTypeName.Contains(StringConstants.PropertyTypeNameInt32, StringComparison.InvariantCulture))
            propertyTypeName = StringConstants.PropertyTypeNameInt32CSharpTypeKeyword;

        else if (propertyTypeName.Contains(StringConstants.PropertyTypeNameInt64, StringComparison.InvariantCulture))
            propertyTypeName = StringConstants.PropertyTypeNameInt64CSharpTypeKeyword;

        else if (propertyTypeName.Contains(StringConstants.PropertyTypeNameChar, StringComparison.InvariantCulture))
            propertyTypeName = StringConstants.PropertyTypeNameCharCSharpTypeKeyword;

        else if (propertyTypeName.Contains(StringConstants.PropertyTypeNameString, StringComparison.InvariantCulture))
            propertyTypeName = StringConstants.PropertyTypeNameStringCSharpTypeKeyword;

        else if (propertyTypeName.Contains(StringConstants.PropertyTypeNameSingle, StringComparison.InvariantCulture)) // float
            propertyTypeName = StringConstants.PropertyTypeNameSingleCSharpTypeKeyword;

        else if (propertyTypeName.Contains(StringConstants.PropertyTypeNameDecimal, StringComparison.InvariantCulture))
            propertyTypeName = StringConstants.PropertyTypeNameDecimalCSharpTypeKeyword;

        else if (propertyTypeName.Contains(StringConstants.PropertyTypeNameDouble, StringComparison.InvariantCulture))
            propertyTypeName = StringConstants.PropertyTypeNameDoubleCSharpTypeKeyword;

        else if (propertyTypeName.Contains(StringConstants.PropertyTypeNameDateOnly, StringComparison.InvariantCulture))
            propertyTypeName = StringConstants.PropertyTypeNameDateOnlyCSharpTypeKeyword;

        else if (propertyTypeName.Contains(StringConstants.PropertyTypeNameDateTime, StringComparison.InvariantCulture))
            propertyTypeName = StringConstants.PropertyTypeNameDateTimeCSharpTypeKeyword;

        else if (propertyTypeName.Contains(StringConstants.PropertyTypeNameBoolean, StringComparison.InvariantCulture))
            propertyTypeName = StringConstants.PropertyTypeNameBooleanCSharpTypeKeyword;

        else if (propertyType!.IsEnum)
            propertyTypeName = StringConstants.PropertyTypeNameEnumCSharpTypeKeyword;

        else if (propertyTypeName.Contains(StringConstants.PropertyTypeNameGuid, StringComparison.InvariantCulture))
            propertyTypeName = StringConstants.PropertyTypeNameGuidCSharpTypeKeyword;

        return propertyType!.IsNullableType() ? $"{propertyTypeName}?" : propertyTypeName;
    }

    /// <summary>
    /// Get property type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="propertyName"></param>
    /// <returns>Type?</returns>
    public static Type? GetPropertyType(this Type type, string propertyName)
    {
        if (type is null || string.IsNullOrWhiteSpace(propertyName))
            return null;

        return type.GetProperty(propertyName)?.PropertyType;
    }

    #endregion
}
