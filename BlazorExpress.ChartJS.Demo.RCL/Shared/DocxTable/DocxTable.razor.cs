namespace BlazorExpress.ChartJS.Demo.RCL;

public partial class DocxTable<TItem> : ComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public DocType DocType { get; set; } = DocType.Parameters;
}


