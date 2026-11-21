# BlazorExpress.ChartJS 2.0.0: social posts

Release date: 21 November 2026. Links:

- Release notes: https://chartjs.blazorexpress.com/blog/releases/2-0-0
- Upgrading to 2.0.0 (breaking changes, before and after code): https://chartjs.blazorexpress.com/blog/releases/2-0-0-breaking-changes
- Demos (samples gallery): https://chartjs.blazorexpress.com/demos/samples
- GitHub: https://github.com/BlazorExpress/BlazorExpress.ChartJS
- NuGet: https://www.nuget.org/packages/BlazorExpress.ChartJS/2.0.0

Media in this folder:

| File | Size | Use |
| --- | --- | --- |
| `release-video.mp4` | 1920 × 1080, 42 s, H.264, with an original synthesized soundtrack (AAC stereo) | LinkedIn, X and Facebook video post |
| `thumbnail.png` | 1280 × 720 | Video cover (LinkedIn, Facebook, YouTube), or the image for X |
| `poster.png` | 1080 × 1350 (4:5) | Image post on LinkedIn and Facebook |

Post the video with the thumbnail as its cover, or the poster as an image post. The site pages go live with the production deploy, so post after it.

## LinkedIn

BlazorExpress.ChartJS 2.0.0 is out 🎉

Chart.js charts for Blazor, written in C#. This is the biggest release yet:

📅 Time axes with no date-adapter script. Use DateOnly, DateTime or DateTimeOffset, and .NET date formats for ticks and tooltips.
🧩 JavaScript functions from C#. Register a function by name and use it for tick labels, tooltips, legend filters and more. Nothing is evaluated as code.
🖱️ Chart methods and events: OnClick, OnHover, OnLegendClick, ShowAsync, HideAsync, ToBase64ImageAsync, DownloadImageAsync and more.
🎞️ Animated updates that move from the current values, as native Chart.js does.
📊 Wider Chart.js 4.6 options: floating bars, half-doughnut gauges, extra axes, subtitles, gradient fills and much more.
🟣 Targets .NET 8, 9, 10 and 11.

Code compiled against 1.2.4 keeps working, but some charts look or behave differently without a code change. The upgrade post shows the 1.2.4 code and the 2.0.0 code for each change.

dotnet add package BlazorExpress.ChartJS --version 2.0.0

📝 Release notes: https://chartjs.blazorexpress.com/blog/releases/2-0-0
⬆️ Upgrading: https://chartjs.blazorexpress.com/blog/releases/2-0-0-breaking-changes
▶️ Demos: https://chartjs.blazorexpress.com/demos/samples
⭐ GitHub: https://github.com/BlazorExpress/BlazorExpress.ChartJS
📦 NuGet: https://www.nuget.org/packages/BlazorExpress.ChartJS/2.0.0

#Blazor #dotnet #csharp #chartjs #dataviz

## X

One post (275 of 280 characters as X counts them: each link 23, each emoji 2):

BlazorExpress.ChartJS 2.0.0 is out: Chart.js charts for #Blazor in C#.

📅 Time axes
🧩 JS functions from C#
🖱️ Chart methods and events
🟣 .NET 8–11

Notes: https://chartjs.blazorexpress.com/blog/releases/2-0-0
Demos: https://chartjs.blazorexpress.com/demos/samples
GitHub: https://github.com/BlazorExpress/BlazorExpress.ChartJS
NuGet: https://www.nuget.org/packages/BlazorExpress.ChartJS/2.0.0

Optional replies for a thread:

1/ Time axes need only Chart.js and our script. Assign DateOnly, DateTime or DateTimeOffset and use .NET date formats like "dd MMM yyyy". #dotnet

2/ Some charts change without a code change, like radar and bubble axes starting at zero. Each change, with the 1.2.4 and 2.0.0 code:
https://chartjs.blazorexpress.com/blog/releases/2-0-0-breaking-changes

3/ Try every chart in the samples gallery, each with copyable C#:
https://chartjs.blazorexpress.com/demos/samples

4/ dotnet add package BlazorExpress.ChartJS --version 2.0.0

## Facebook

BlazorExpress.ChartJS 2.0.0 is here 🎉

Build Chart.js charts in Blazor with C#. New in 2.0.0:

• Time axes with no date-adapter script
• JavaScript functions for tick labels, tooltips and legends, set from C#
• Chart methods and events: OnClick, OnHover, OnLegendClick, ShowAsync, HideAsync, image export
• Animated updates that move from the current values
• Floating bars, half-doughnut gauges, extra axes, gradient fills and more Chart.js 4.6 options
• .NET 8, 9, 10 and 11

Upgrading from 1.x? Some charts look or behave differently without a code change, so read the breaking changes first. Each one shows the old and the new code.

👉 Release notes: https://chartjs.blazorexpress.com/blog/releases/2-0-0
▶️ Live demos: https://chartjs.blazorexpress.com/demos/samples
⭐ GitHub: https://github.com/BlazorExpress/BlazorExpress.ChartJS
📦 NuGet: https://www.nuget.org/packages/BlazorExpress.ChartJS/2.0.0

#Blazor #dotnet #csharp #chartjs

## Alt text

- **Video:** A 42-second video with an instrumental soundtrack and no speech. It opens on the BlazorExpress.ChartJS logo and "2.0.0, released 21 November 2026", then shows each new feature beside its C# code: a time axis line chart, a bar chart with currency tick labels from a JavaScript function, bars animating between values with UpdateAsync, a doughnut chart where clicking a slice hides it, and floating bars, a half-doughnut gauge, a two-axis line chart and a gradient fill. It ends with the install command, the supported .NET versions and chartjs.blazorexpress.com.
- **Poster:** BlazorExpress.ChartJS 2.0.0, released 21 November 2026. Chart.js charts for Blazor, in C#, now with time axes, JavaScript functions, chart methods and events. Four sample charts: a time axis line chart, floating bars, a half-doughnut gauge at 72%, and quarterly bars with currency labels. Install with dotnet add package BlazorExpress.ChartJS --version 2.0.0. Supports .NET 8, 9, 10 and 11. chartjs.blazorexpress.com.
- **Thumbnail:** What's new in BlazorExpress.ChartJS 2.0.0: time axes, JavaScript functions and events. Chart.js for Blazor on .NET 8 to 11, beside a line chart of visitors and sign-ups over November.

## Regenerate

From the repository root, run `python branding/release-media/2.0.0/serve.py`, open http://127.0.0.1:5098/release-media/2.0.0/generator.html and press the buttons. The server writes `release-video.mp4`, `poster.png` and `thumbnail.png` into this folder. The soundtrack is composed in the page with Web Audio (pad, arpeggio, bass and drums in A minor at 120 BPM, a whoosh on each scene change, a click and two chimes) and encoded as AAC, so it has no licensing restrictions. The page needs a Chromium browser (WebCodecs) and loads Chart.js, mp4-muxer and the Sora and JetBrains Mono fonts from public CDNs. Without the server, the buttons download the files instead.
