<p align="center">
  <a href="https://chartjs.blazorexpress.com">
    <img src="https://raw.githubusercontent.com/BlazorExpress/BlazorExpress.ChartJS/refs/heads/main/nuget/logo.png" alt="Blazor Express logo" width="200" height="200">
  </a>
</p>

<h3 align="center">BlazorExpress.ChartJs component library</h3>
<p align="center">
    An open-source, production-ready Blazor charts component library built on the <b>Blazor</b> and <b>Chart.js</b> JavaScript library.
    <br>
    <a href="https://chartjs.blazorexpress.com/charts/getting-started"><strong>Getting Started �</strong></a>
    <br>
</p>

## Status

[![NuGet](https://img.shields.io/nuget/vpre/BlazorExpress.ChartJS)](https://www.nuget.org/packages/BlazorExpress.ChartJS/absoluteLatest)
[![NuGet](https://img.shields.io/nuget/dt/BlazorExpress.ChartJS.svg)](https://www.nuget.org/packages/BlazorExpress.ChartJS/absoluteLatest)

## Table of contents

- [Quick start](#quick-start)
- [Online Demos](#online-demos)
- [Components](#components)
- [Creators](#creators)
- [Copyright and license](#copyright-and-license)

## Quick start

Get started any way you want

- Clone the repo: `git clone https://github.com/BlazorExpress/BlazorExpress.ChartJS`
- Install with [NuGet](https://www.nuget.org/): `Install-Package BlazorExpress.ChartJS -Version 1.2.2`

## Online Demos

- [Demo Website](https://chartjs.blazorexpress.com)

## Components

| Component Name | Demos | Docs |
|:--|:--|:--|
| Charts: Bar chart | [Demos](https://chartjs.blazorexpress.com/demos/bar-chart) | [Docs](https://chartjs.blazorexpress.com/docs/bar-chart) |
| Charts: Bubble chart | [Demos](https://chartjs.blazorexpress.com/demos/bubble-chart) | [Docs](https://chartjs.blazorexpress.com/docs/bubble-chart) |
| Charts: Doughnut chart | [Demos](https://chartjs.blazorexpress.com/demos/doughnut-chart) | [Docs](https://chartjs.blazorexpress.com/docs/doughnut-chart) |
| Charts: Line chart | [Demos](https://chartjs.blazorexpress.com/demos/line-chart) | [Docs](https://chartjs.blazorexpress.com/docs/line-chart) |
| Charts: Pie chart | [Demos](https://chartjs.blazorexpress.com/demos/pie-chart) | [Docs](https://chartjs.blazorexpress.com/docs/pie-chart) |
| Charts: PolarArea chart | [Demos](https://chartjs.blazorexpress.com/demos/polar-area-chart) | [Docs](https://chartjs.blazorexpress.com/docs/polar-area-chart) |
| Charts: Radar chart | [Demos](https://chartjs.blazorexpress.com/demos/radar-chart) | [Docs](https://chartjs.blazorexpress.com/docs/radar-chart) |
| Charts: Scatter chart | [Demos](https://chartjs.blazorexpress.com/demos/scatter-chart) | [Docs](https://chartjs.blazorexpress.com/docs/scatter-chart) |

## Combo bar/line

`BarChart` and `LineChart` both support mixed bar/line datasets. Add `BarChartDataset` and `LineChartDataset` instances to the same `ChartData.Datasets` collection and initialize either chart component as the root chart.

```razor
<BarChart @ref="chart" Width="600" />

@code {
  private BarChart chart = default!;
  private readonly BarChartOptions options = new()
  {
    Responsive = true,
    Interaction = new Interaction { Mode = InteractionMode.Index, Intersect = false },
  };

  private readonly ChartData chartData = new()
  {
    Labels = new List<string> { "January", "February", "March" },
    Datasets = new List<IChartDataset>
    {
      new BarChartDataset
      {
        Label = "Revenue",
        Data = new List<double?> { 65, 59, 80 },
      },
      new LineChartDataset
      {
        Label = "Target",
        Data = new List<double?> { 50, 55, 60 },
      },
    },
  };

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
      await chart.InitializeAsync(chartData, options);
  }
}
```

Use `LineChart` the same way when you want the line configuration to be the root chart type.

More components coming...

## Creators

**Vikram Reddy**

## Copyright and license

Code and documentation copyright 2026 [Blazor Express](https://blazorexpress.com/) Code released under the [Apache-2.0 License](https://github.com/BlazorExpress/BlazorExpress.ChartJS/blob/main/LICENSE).

## Supported Tech Stack

| BlazorExpress.ChartJS | .NET | Chart.js | chartjs-plugin-datalabels |
|:--|:--|:--|:--|
| 1.2.2 | 8, 9, 10 | 4.4.1 | 2.2.0 |
| 1.1.0+ | 8 | 4.4.1 | 2.2.0 |
| 1.0.0 | 8 | 4.0.1 | 2.2.0 |
