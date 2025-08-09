namespace BlazorExpress.ChartJS.Demo.RCL;

public partial class MainLayout : MainLayoutBase
{
    #region Fields and Constants

    private bool isSidebarVisible = false;
    //private HashSet<LinkGroup> linkGroups = new();
    private Menu menuRef = default!;

    #endregion

    #region Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await JS.InvokeVoidAsync("initializeTheme");

        await base.OnAfterRenderAsync(firstRender);
    }

    #endregion
}
