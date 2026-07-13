using BlazorExpress.ChartJS;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace BlazorExpress.ChartJS.MCP.Tests;

public class ChartClickEventArgsTests
{
    [Fact]
    public async Task HandleClickAsync_Invokes_Configured_Callback_With_Selected_Item_Metadata()
    {
        var component = new TestChartComponent();
        ChartClickEventArgs? receivedEventArgs = null;
        component.ConfigureOnClick(EventCallback.Factory.Create<ChartClickEventArgs>(new object(), eventArgs => receivedEventArgs = eventArgs));

        using var document = JsonDocument.Parse("""{"value":42}""");
        var eventArgs = new ChartClickEventArgs
        {
            DatasetIndex = 1,
            DatasetLabel = "Revenue",
            Index = 2,
            Label = "March",
            Value = document.RootElement.Clone()
        };

        await component.HandleClickAsync(eventArgs);

        Assert.NotNull(receivedEventArgs);
        Assert.Equal(1, receivedEventArgs.DatasetIndex);
        Assert.Equal("Revenue", receivedEventArgs.DatasetLabel);
        Assert.Equal(2, receivedEventArgs.Index);
        Assert.Equal("March", receivedEventArgs.Label);
        Assert.Equal(42, receivedEventArgs.Value?.GetProperty("value").GetInt32());
    }

    [Fact]
    public async Task HandleClickAsync_Without_Callback_Completes_Without_Throwing()
    {
        var component = new TestChartComponent();

        var exception = await Record.ExceptionAsync(() => component.HandleClickAsync(new ChartClickEventArgs
        {
            DatasetIndex = 0,
            Index = 0,
            Value = null
        }));

        Assert.Null(exception);
    }

    [Fact]
    public void JavaScript_Click_Bridge_Maps_Selected_Item_And_Protects_No_Item_Path()
    {
        var source = File.ReadAllText(GetRepositoryFile("BlazorExpress.ChartJS", "wwwroot", "blazorexpress.chartjs.js"));

        Assert.Equal(9, source.Split("initialize: (elementId, type, data, options, plugins, dotNetObjectReference)").Length - 1);
        Assert.Contains("chart.getElementsAtEventForMode(event, 'nearest', { intersect: true }, true)", source);
        Assert.Contains("if (!activeElements || activeElements.length === 0) return;", source);
        Assert.Contains("dotNetObjectReference.invokeMethodAsync('HandleClickAsync'", source);
        Assert.Contains("datasetIndex: activeElement.datasetIndex", source);
        Assert.Contains("datasetLabel: dataset?.label ?? null", source);
        Assert.Contains("index: activeElement.index", source);
        Assert.Contains("label: chart.data.labels?.[activeElement.index] ?? null", source);
        Assert.Contains("value: dataset?.data?.[activeElement.index] ?? null", source);
        Assert.DoesNotContain("options.onClick", source);
    }

    private static string GetRepositoryFile(params string[] pathSegments)
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "BlazorExpress.ChartJS.sln")))
                return Path.Combine([directory.FullName, .. pathSegments]);

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("The ChartJS repository root could not be located.");
    }

    private sealed class TestChartComponent : ChartComponentBase
    {
        public void ConfigureOnClick(EventCallback<ChartClickEventArgs> onClick) => OnClick = onClick;
    }
}
