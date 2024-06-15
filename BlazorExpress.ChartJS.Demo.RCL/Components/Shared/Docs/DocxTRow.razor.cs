namespace BlazorExpress.ChartJS.Demo.RCL;

public partial class DocxTRow<TItem> : ComponentBase
{
    [Parameter]
    public string? PropertyName { get; set; }

    [Parameter]
    public string? DefaultValue { get; set; }

    [Parameter]
    public bool Required { get; set; }

    [Parameter]
    public string? Description { get; set; }

    [Parameter]
    public string? AddedVersion { get; set; }


}
