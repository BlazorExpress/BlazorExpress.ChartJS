using System.ComponentModel;
using System.Reflection;

namespace BlazorExpress.ChartJS.Demo.RCL;

/// <summary>
/// Extension methods for <see cref="MethodInfo" />.
/// </summary>
public static class MethodInfoExtensions
{
    /// <summary>
    /// Get added version of a method.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <returns>string</returns>
    public static string GetMethodAddedVersion(this MethodInfo methodInfo)
    {
        var addedVersionAttribute = methodInfo.GetCustomAttributes(typeof(AddedVersionAttribute), false).FirstOrDefault() as AddedVersionAttribute;
        return addedVersionAttribute?.Version ?? string.Empty;
    }

    /// <summary>
    /// Get method description.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <returns>string</returns>
    public static string GetMethodDescription(this MethodInfo methodInfo)
    {
        var descriptionAttribute = methodInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
        return descriptionAttribute?.Description ?? string.Empty;
    }

    public static string GetMethodParameters(this MethodInfo methodInfo)
    {
        var parameters = methodInfo.GetParameters();
        if (parameters.Length == 0)
            return string.Empty;

        var parametersWithType = new HashSet<string>();
        foreach (var parameter in parameters)
            parametersWithType.Add($"{parameter.ParameterType.GetCSharpTypeName()} {parameter.Name}");

        return string.Join(",", parametersWithType);
    }

    /// <summary>
    /// Get method return type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <returns>string</returns>
    public static string GetMethodReturnType(this MethodInfo methodInfo)
        => methodInfo.ReturnType.GetCSharpTypeName();
}
