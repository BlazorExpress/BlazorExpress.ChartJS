namespace BlazorExpress.ChartJS.Demo.RCL;

public partial class DocxMethodRow<TItem> : ComponentBase
{
    private string AddedVersion => MethodInfo.GetMethodAddedVersion();

    private string Description => MethodInfo.GetMethodDescription();

    public string ReturnType => MethodInfo.GetMethodReturnType();

    public string MethodNameWithParameters => $"{MethodInfo.Name}({MethodParameters})";

    public string MethodParameters => MethodInfo.GetMethodParameters();

    public string ReturnTypeShortName => ReturnType.Contains(".")
        ? ReturnType.Split('.').Last()
        : ReturnType;

    [Parameter]
    public MethodInfo MethodInfo { get; set; } = default!;
}
