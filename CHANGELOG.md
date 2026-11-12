# Changelog

All notable changes to BlazorExpress.ChartJS are listed here, newest first.

## 2.0.0

2.0.0 is a major release. Code compiled against 1.2.4 keeps working: package validation against 1.2.4 reports no binary break. Some charts look or behave differently without any code change, though, so read **Breaking changes** before you upgrade. Chart.js 4.6.1 is the documented version.

### Breaking changes

Each item says how to keep the 1.2.4 behaviour where there is a way.

- **Radar and bubble axes start at zero.** Like the other charts, radar and bubble charts now start their axes at zero by default. To keep the previous look, set `options.Scales.R.BeginAtZero = false` (radar) or `options.Scales.X.BeginAtZero = false` and `options.Scales.Y.BeginAtZero = false` (bubble).
- **Radar and bubble data labels match the other charts.** Radar and bubble charts that register the `ChartDataLabels` plugin now get the library's label style: white, bold, 6 px padding and a 4 px corner radius (the new `RadarChartPlugins.Datalabels` and `BubbleChartPlugins.Datalabels` defaults). In 1.2.4 they used the plugin's own grey text. To restore it, set `options.Plugins.Datalabels.Color = "#666"` (the Chart.js default text colour), `options.Plugins.Datalabels.Font.Weight = null`, `options.Plugins.Datalabels.Padding = 4` and `options.Plugins.Datalabels.BorderRadius = 0`.
- **Bar chart data labels use the dataset colour from the first render.** Bar charts that use `ChartDataLabels` show the dataset-coloured label background from the first render, not only after an update. Bubble charts keep the 1.2.4 behaviour.
- **Hidden datasets stay hidden after `UpdateAsync`.** A dataset, slice, bar or point hidden at runtime (from C# or by a legend click) now stays hidden after `UpdateAsync`. In 1.2.4 a legend-hidden dataset reappeared on every update. Items are matched by `ChartDataset.Key` when set, otherwise by label. To show an item again, call `ShowAsync(datasetIndex)`, or `ShowAsync(datasetIndex, dataIndex)` for a slice, bar or point. Setting `Hidden` in C# does not show an item that was hidden at runtime.
- **Animated updates move from the current values.** With animation on, `UpdateAsync` animates each dataset still on the chart from its current values to the new ones, as native Chart.js does. New datasets grow in from their base, and removed datasets disappear. Updates with `ChartUpdateMode.None` or `Reset` apply the new data directly, as before. The final drawing is unchanged. To replay the 1.2.4 grow-in animation, call `ResetAsync()` and then `UpdateAsync(...)`.
- **The built-in date adapter replaces a third-party adapter.** The library registers its own Chart.js date adapter, built on the browser's `Date` and `Intl`. On a page that also loads a third-party adapter (for example `chartjs-adapter-date-fns` or luxon), the library's adapter replaces it. Default tick formats change to the library defaults, and date-fns or luxon tokens in raw Chart.js options are read as .NET date format strings. Remove the third-party adapter script and write .NET date formats such as `"dd MMM yyyy"`.
- **Removed: the undocumented JavaScript function `updateDataValues`** on `window.blazorexpress.chartjs`, `.radar` and `.scatter`. No C# method called it. Use `UpdateAsync`.
- **`blazorexpress.chartjs.js` changed.** `initialize` and `update` take new optional trailing arguments. Apps that load the script from the package (`_content/BlazorExpress.ChartJS/blazorexpress.chartjs.js`) need nothing. Apps that copied the file into their own folder must copy the new one.

### New

- **.NET 11.** The package targets `net11.0` alongside `net8.0`, `net9.0` and `net10.0`, built against `Microsoft.AspNetCore.Components.Web` 11.0.0. Nothing changes for apps on the other targets.
- **Time axes.** `time` and `timeseries` axes work with only Chart.js and `blazorexpress.chartjs.js`; no extra script. Date labels (`ChartData.TimeLabels`), date points on line, bar, scatter and bubble datasets (`TimeData`), `ChartTimeValue` (assign a `DateOnly`, `DateTime` or `DateTimeOffset`), time axis options, and .NET date format strings for tick and tooltip formats, localized with `ChartOptions.Locale`. `DateOnly` and `Unspecified` `DateTime` values are wall-clock values that show the same date and time in every browser time zone; `Utc` and `Local` `DateTime` and `DateTimeOffset` values are exact instants.
- **`AddTimeDataAsync`** on line and bar charts adds a date label with one value for each dataset, the date-label counterpart of `AddDataAsync`. `AddDataAsync` also appends a time point to one dataset's `TimeData` on line, bar, scatter and bubble charts.
- **JavaScript functions.** Options that Chart.js takes as a JavaScript function (tick labels, tooltip text, legend filters and clicks, line segment styles, gradient colours and more) can now be set from C#. Register your function by name in your own JavaScript file with `blazorexpress.chartjs.registerFunction('formatCurrency', fn)`, load that file **after** `blazorexpress.chartjs.js`, and reference it with `new ChartFunction("formatCurrency")`. Value properties such as `BackgroundColor` have a function sibling, such as `BackgroundColorFunction`, which is sent instead when set. A legend `OnClick` function replaces Chart.js's default toggle; the library still raises `OnLegendClick`. Radar datasets have no `SegmentStyle`.
- **`Ticks.Format` (`ChartNumberFormat`)** gives currency, percent and compact axis labels with no JavaScript.
- **Chart methods:** `ToBase64ImageAsync`, `DownloadImageAsync`, `ResetAsync`, `RenderAsync`, `StopAsync`, `ClearAsync` and `UpdateAsync(…, ChartUpdateMode)`.
- **Visibility methods:** `ShowAsync`, `HideAsync`, `SetDatasetVisibilityAsync`, `IsDatasetVisibleAsync`, `ToggleDataVisibilityAsync`, `GetVisibleDatasetCountAsync` and `SetActiveElementsAsync`.
- **Events:** `OnLegendClick` (with `LegendClickToggles`) and `OnHover`.
- **`ChartDataset.Key`** keeps a dataset's identity across updates when its label changes, for example on relabelling or localization.
- **Options on every chart type.** All eight chart types have `Plugins`, `Interaction` and `Layout`, created by default, so `options.Plugins.Title.Text = "Sales"` works on all of them. Radar and polar area charts have a radial `Scales.R` axis, and bubble charts have `Scales.X` and `Scales.Y`.
- **Axis options:** additional axes (`Scales.AdditionalAxes`, for example a right-hand `y1`), `Position`, `Reverse`, `Offset`, `Grace`, `Bounds`, `Weight`, `Stack`, `StackWeight`, `Display`, `BackgroundColor` and `Axis`. Tick options include `StepSize`, `Precision`, `Count`, `MaxTicksLimit`, `AutoSkip`, rotation and `Font`. Axis titles have `Padding`.
- **Chart-level options:** `AspectRatio`, `ResizeDelay`, `DevicePixelRatio`, `Events`, `Hover`, `Elements`, per-side layout padding (`Layout.PaddingSides`), and `Interaction.Axis` and `IncludeInvisible`.
- **Plugin options:** a subtitle plugin; title `FullSize`, `Padding` and `Position`; 14 tooltip options including `Mode`, `Position`, `Padding`, `CornerRadius` and `UsePointStyle`; legend label `TextAlign`; and the colors and filler plugins.
- **Line charts:** the dashed hover line can be styled or turned off with `options.Plugins.TooltipLine` (default unchanged).
- **Doughnut and pie:** `Cutout`, `Radius`, `Rotation` and `Circumference` (for example a half-doughnut gauge), and `Animation.AnimateRotate` and `AnimateScale`.
- **Datasets:** floating bars (`BarChartDataset.FloatingData`, also through `AddDataAsync` with `new BarChartDatasetData(label, start, end)`), bar `Base`, per-corner `BorderRadiusCorners` and `HoverBorderRadiusCorners`, `InflateAmount` and `PointStyle`; line `MaxSpanGap`; per-side `ClipSides`; `SpacingMode` on arc datasets; scatter `Segment`; radar `RAxisID`.

### Changed

- **Chart.js 4.6.1** is the documented version (a new "2.0.0+" row in the version tables). The browser console shows a one-time warning when a Chart.js version below 4.6.0 is loaded.
- `Stack` now lives on the shared `ChartDataset<TData>`, so scatter, bubble and radar datasets have it too. Bar and line code keeps compiling and sends the same JSON.
- Doughnut, pie and polar area charts now also send default `interaction` and `layout` options. They equal the Chart.js defaults, so nothing changes on screen.
- A bar dataset with no `BorderRadius` no longer sends `"borderRadius": null`; it leaves the key out, so `options.Elements.Bar.BorderRadius` can apply. Nothing changes on screen. Code that inspects the serialized dataset JSON sees one key fewer.
- Decimation and the `parsing` and `normalized` options are not included in 2.0.0.

### Fixed

- A chart placed in a container that starts hidden (a tab, accordion, collapsible panel or dialog), or one whose container is resized as an animation ends, could stay blank until the window was resized or the chart was hovered. A responsive chart now always draws at its container's size.
- Charts are destroyed when the component is disposed, so they no longer pile up across navigation. Chart calls after dispose are ignored, and disconnected circuits no longer raise errors.
- `ResizeAsync` now resizes the container and redraws the chart; in 1.2.4 it had no effect.
- The shared `ChartComponentBase` data path includes bubble datasets.
- A registered JavaScript function that throws is logged to the console once, and the chart keeps drawing.

### Security

- A `ChartFunction` names a function you registered with `registerFunction`. It is looked up only in that registry, never on `window` or through globals, and never evaluated as code (no `eval` or `new Function`). An unregistered name logs one console error naming the function and the option, and the option keeps its Chart.js default.

### Docs and demos

- A samples gallery with a page per sample, each with "How to use" steps and copyable source, covering the official Chart.js samples the library can express.
- Each chart's docs page lists its Common Parameters, Events and Methods, so inherited members such as `ResizeAsync` and `OnClick` are documented too.
- Getting started covers time axes and JavaScript functions, including the script order for WebAssembly, Server, Auto and MAUI.
- The Pie chart demo page links to the Pie chart docs, and the README's Getting Started link opens the getting-started page.

### For contributors

- New test project `BlazorExpress.ChartJS.Tests` (xUnit v3 and bUnit). Run it with `dotnet test --solution BlazorExpress.ChartJS.slnx`. The root `global.json` opts in to Microsoft.Testing.Platform, which needs the .NET 10 SDK; older SDKs ignore the setting.
- The demo site deploys to production only when the workflow is run by hand with "Do you want to release to production?" ticked. Pushes to `main` no longer deploy, and pull requests still get preview deploys.
