using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Serialization;

/// <summary>
/// Locks the JSON of the options added in 1.3.0: each new property is sent under its Chart.js name when set, and left out when unset.
/// </summary>
public class OptionsCoverageJsonTests
{
    #region Fields and Constants

    private static readonly Dictionary<string, (Action<ChartAxes> Set, string Json)> axisCases = new()
    {
        ["axis"] = (a => a.Axis = "y", "\"y\""),
        ["backgroundColor"] = (a => a.BackgroundColor = "#eee", "\"#eee\""),
        ["bounds"] = (a => a.Bounds = "data", "\"data\""),
        ["display"] = (a => a.Display = false, "false"),
        ["grace"] = (a => a.Grace = "5%", "\"5%\""),
        ["offset"] = (a => a.Offset = true, "true"),
        ["position"] = (a => a.Position = "right", "\"right\""),
        ["reverse"] = (a => a.Reverse = true, "true"),
        ["stack"] = (a => a.Stack = "demo", "\"demo\""),
        ["stackWeight"] = (a => a.StackWeight = 2, "2"),
        ["weight"] = (a => a.Weight = 1, "1"),
    };

    private static readonly Dictionary<string, (Action<ChartOptions> Set, string Json)> chartLevelCases = new()
    {
        ["aspectRatio"] = (o => o.AspectRatio = 1.5, "1.5"),
        ["devicePixelRatio"] = (o => o.DevicePixelRatio = 2, "2"),
        ["elements"] = (o => o.Elements = new ChartElements
        {
            Arc = new ChartElementsArc { BorderAlign = "inner", BorderWidth = 4 },
            Bar = new ChartElementsBar { BorderRadius = 6, InflateAmount = "auto" },
            Line = new ChartElementsLine { BorderDash = [4, 2], CapBezierPoints = false },
            Point = new ChartElementsPoint { Radius = 0, PointStyle = "star" }
        }, "{\"arc\":{\"borderAlign\":\"inner\",\"borderWidth\":4},\"bar\":{\"borderRadius\":6,\"inflateAmount\":\"auto\"},\"line\":{\"borderDash\":[4,2],\"capBezierPoints\":false},\"point\":{\"pointStyle\":\"star\",\"radius\":0}}"),
        ["events"] = (o => o.Events = ["click"], "[\"click\"]"),
        ["hover"] = (o => o.Hover = new ChartHover { Mode = InteractionMode.Index, Intersect = false, Axis = "x", IncludeInvisible = true }, "{\"axis\":\"x\",\"includeInvisible\":true,\"intersect\":false,\"mode\":\"index\"}"),
        ["resizeDelay"] = (o => o.ResizeDelay = 200, "200"),
    };

    private static readonly Dictionary<string, (Func<IChartDataset> Create, Action<IChartDataset> Set, string Name, string Json)> datasetCases = new()
    {
        ["bar.base"] = (() => new BarChartDataset(), d => ((BarChartDataset)d).Base = 2, "base", "2"),
        ["bar.borderRadiusCorners"] = (() => new BarChartDataset(), d => ((BarChartDataset)d).BorderRadiusCorners = new ChartBorderRadius { TopLeft = 6, TopRight = 6 }, "borderRadius", "{\"topLeft\":6,\"topRight\":6}"),
        ["bar.hoverBorderRadiusCorners"] = (() => new BarChartDataset(), d => ((BarChartDataset)d).HoverBorderRadiusCorners = new ChartBorderRadius { BottomLeft = 2 }, "hoverBorderRadius", "{\"bottomLeft\":2}"),
        ["bar.inflateAmount"] = (() => new BarChartDataset(), d => ((BarChartDataset)d).InflateAmount = 1, "inflateAmount", "1"),
        ["bar.pointStyle"] = (() => new BarChartDataset(), d => ((BarChartDataset)d).PointStyle = "rectRounded", "pointStyle", "\"rectRounded\""),
        ["bubble.stack"] = (() => new BubbleChartDataset(), d => ((BubbleChartDataset)d).Stack = "s1", "stack", "\"s1\""),
        ["doughnut.spacingMode"] = (() => new DoughnutChartDataset(), d => ((DoughnutChartDataset)d).SpacingMode = "parallel", "spacingMode", "\"parallel\""),
        ["line.clipSides"] = (() => new LineChartDataset(), d => ((LineChartDataset)d).ClipSides = new ChartClip { Top = 10 }, "clip", "{\"bottom\":0,\"left\":0,\"right\":0,\"top\":10}"),
        ["line.maxSpanGap"] = (() => new LineChartDataset(), d => ((LineChartDataset)d).MaxSpanGap = 2, "spanGaps", "2"),
        ["bubble.xAxisID"] = (() => new BubbleChartDataset(), d => ((BubbleChartDataset)d).XAxisID = "x1", "xAxisID", "\"x1\""),
        ["bubble.yAxisID"] = (() => new BubbleChartDataset(), d => ((BubbleChartDataset)d).YAxisID = "y1", "yAxisID", "\"y1\""),
        ["pie.spacingMode"] = (() => new PieChartDataset(), d => ((PieChartDataset)d).SpacingMode = "proportional", "spacingMode", "\"proportional\""),
        ["polarArea.spacing"] = (() => new PolarAreaChartDataset(), d => ((PolarAreaChartDataset)d).Spacing = 8, "spacing", "8"),
        ["polarArea.spacingMode"] = (() => new PolarAreaChartDataset(), d => ((PolarAreaChartDataset)d).SpacingMode = "angular", "spacingMode", "\"angular\""),
        ["radar.stack"] = (() => new RadarChartDataset(), d => ((RadarChartDataset)d).Stack = "s1", "stack", "\"s1\""),
        ["scatter.segment"] = (() => new ScatterChartDataset(), d => ((ScatterChartDataset)d).Segment = new { borderDash = new[] { 6, 6 } }, "segment", "{\"borderDash\":[6,6]}"),
        ["scatter.stack"] = (() => new ScatterChartDataset(), d => ((ScatterChartDataset)d).Stack = "s1", "stack", "\"s1\""),
    };

    private static readonly Dictionary<string, (Action<ChartPlugins> Set, string Path, string Json)> pluginCases = new()
    {
        ["colors"] = (p => p.Colors = new ChartPluginsColors { Enabled = true, ForceOverride = true }, "colors", "{\"enabled\":true,\"forceOverride\":true}"),
        ["filler"] = (p => p.Filler = new ChartPluginsFiller { Propagate = false, DrawTime = "beforeDraw" }, "filler", "{\"drawTime\":\"beforeDraw\",\"propagate\":false}"),
        ["legend.labels.textAlign"] = (p => p.Legend.Labels = new ChartPluginsLegendLabels { TextAlign = "left" }, "legend.labels.textAlign", "\"left\""),
        ["subtitle"] = (p => p.Subtitle = new ChartPluginsSubtitle { Display = true, Text = "Q1" }, "subtitle", "{\"align\":\"center\",\"color\":\"black\",\"display\":true,\"text\":\"Q1\"}"),
        ["title.fullSize"] = (p => p.Title!.FullSize = false, "title.fullSize", "false"),
        ["title.padding"] = (p => p.Title!.Padding = new ChartPadding { Top = 10, Bottom = 30 }, "title.padding", "{\"top\":10,\"bottom\":30}"),
        ["title.position"] = (p => p.Title!.Position = "bottom", "title.position", "\"bottom\""),
        ["tooltip.borderColor"] = (p => Tooltip(p).BorderColor = "red", "tooltip.borderColor", "\"red\""),
        ["tooltip.borderWidth"] = (p => Tooltip(p).BorderWidth = 1, "tooltip.borderWidth", "1"),
        ["tooltip.boxHeight"] = (p => Tooltip(p).BoxHeight = 8, "tooltip.boxHeight", "8"),
        ["tooltip.boxPadding"] = (p => Tooltip(p).BoxPadding = 3, "tooltip.boxPadding", "3"),
        ["tooltip.boxWidth"] = (p => Tooltip(p).BoxWidth = 8, "tooltip.boxWidth", "8"),
        ["tooltip.cornerRadius"] = (p => Tooltip(p).CornerRadius = 2, "tooltip.cornerRadius", "2"),
        ["tooltip.intersect"] = (p => Tooltip(p).Intersect = false, "tooltip.intersect", "false"),
        ["tooltip.mode"] = (p => Tooltip(p).Mode = InteractionMode.Index, "tooltip.mode", "\"index\""),
        ["tooltip.multiKeyBackground"] = (p => Tooltip(p).MultiKeyBackground = "#000", "tooltip.multiKeyBackground", "\"#000\""),
        ["tooltip.padding"] = (p => Tooltip(p).Padding = 12, "tooltip.padding", "12"),
        ["tooltip.position"] = (p => Tooltip(p).Position = "nearest", "tooltip.position", "\"nearest\""),
        ["tooltip.rtl"] = (p => Tooltip(p).Rtl = true, "tooltip.rtl", "true"),
        ["tooltip.textDirection"] = (p => Tooltip(p).TextDirection = "rtl", "tooltip.textDirection", "\"rtl\""),
        ["tooltip.usePointStyle"] = (p => Tooltip(p).UsePointStyle = true, "tooltip.usePointStyle", "true"),
    };

    private static readonly Dictionary<string, (Action<ChartRadialAxes> Set, string Json)> radialCases = new()
    {
        ["angleLines"] = (r => r.AngleLines = new ChartAxesAngleLines { Color = "red", Display = true, LineWidth = 2, BorderDash = [4, 4], BorderDashOffset = 1 }, "{\"borderDash\":[4,4],\"borderDashOffset\":1,\"color\":\"red\",\"display\":true,\"lineWidth\":2}"),
        ["backgroundColor"] = (r => r.BackgroundColor = "#eee", "\"#eee\""),
        ["display"] = (r => r.Display = false, "false"),
        ["grid"] = (r => r.Grid = new ChartAxesGrid { Circular = true }, "{\"circular\":true,\"display\":true,\"drawOnChartArea\":true,\"drawTicks\":true,\"lineWidth\":1,\"offset\":false}"),
        ["max"] = (r => r.Max = 100, "100"),
        ["min"] = (r => r.Min = 10, "10"),
        ["pointLabels"] = (r => r.PointLabels = new ChartAxesPointLabels { BackdropColor = "#fff", BackdropPadding = 4, BorderRadius = 2, CenterPointLabels = true, Color = "blue", Display = true, Padding = 8 }, "{\"backdropColor\":\"#fff\",\"backdropPadding\":4,\"borderRadius\":2,\"centerPointLabels\":true,\"color\":\"blue\",\"display\":true,\"padding\":8}"),
        ["reverse"] = (r => r.Reverse = true, "true"),
        ["startAngle"] = (r => r.StartAngle = 45, "45"),
        ["suggestedMax"] = (r => r.SuggestedMax = 90, "90"),
        ["suggestedMin"] = (r => r.SuggestedMin = 5, "5"),
        ["ticks"] = (r => r.Ticks = new ChartAxesTicks { StepSize = 20, ShowLabelBackdrop = false }, "{\"display\":true,\"showLabelBackdrop\":false,\"stepSize\":20}"),
        ["type"] = (r => r.Type = ChartAxesType.RadialLinear, "\"radialLinear\""),
    };

    private static readonly Dictionary<string, (Action<ChartAxesTicks> Set, string Json)> tickCases = new()
    {
        ["autoSkip"] = (t => t.AutoSkip = false, "false"),
        ["autoSkipPadding"] = (t => t.AutoSkipPadding = 10, "10"),
        ["count"] = (t => t.Count = 5, "5"),
        ["crossAlign"] = (t => t.CrossAlign = "far", "\"far\""),
        ["font"] = (t => t.Font = new ChartFont { Size = 14 }, "{\"lineHeight\":1.2,\"size\":14,\"weight\":\"bold\"}"),
        ["includeBounds"] = (t => t.IncludeBounds = false, "false"),
        ["labelOffset"] = (t => t.LabelOffset = 4, "4"),
        ["maxRotation"] = (t => t.MaxRotation = 90, "90"),
        ["maxTicksLimit"] = (t => t.MaxTicksLimit = 4, "4"),
        ["minRotation"] = (t => t.MinRotation = 45, "45"),
        ["mirror"] = (t => t.Mirror = true, "true"),
        ["precision"] = (t => t.Precision = 1, "1"),
        ["sampleSize"] = (t => t.SampleSize = 10, "10"),
        ["stepSize"] = (t => t.StepSize = 5, "5"),
        ["z"] = (t => t.Z = 1, "1"),
    };

    #endregion

    #region Methods

    #region Public Methods

    [Theory]
    [MemberData(nameof(AxisCaseNames))]
    public void Axes_SendsEachNewOptionOnlyWhenSet(string name)
    {
        var options = new LineChartOptions();

        Assert.False(Scale(options, "y").TryGetProperty(name, out _));

        axisCases[name].Set(options.Scales.Y!);

        Assert.Equal(axisCases[name].Json, Scale(options, "y").GetProperty(name).GetRawText());
    }

    [Theory]
    [MemberData(nameof(CartesianChartTypes))]
    public void Axes_SendAdditionalAxesUnderTheirIds(string chartType)
    {
        var options = CartesianOptions(chartType);
        options.Scales.AdditionalAxes = new() { ["y1"] = new ChartAxes { Position = "right" }, ["percent"] = new ChartAxes { Axis = "y", Max = 100 } };

        var scales = ChartTestData.Serialize(options.Options).GetProperty("scales");

        Assert.Equal(["x", "y", "y1", "percent"], scales.EnumerateObject().Select(p => p.Name));
        Assert.Equal("right", scales.GetProperty("y1").GetProperty("position").GetString());
        Assert.Equal("y", scales.GetProperty("percent").GetProperty("axis").GetString());
        Assert.Equal(100, scales.GetProperty("percent").GetProperty("max").GetDouble());
    }

    [Fact]
    public void Axes_LeaveOutANullDefaultAxis()
    {
        var options = new LineChartOptions();
        options.Scales.Y = null;
        options.Scales.AdditionalAxes = new() { ["y"] = new ChartAxes { Position = "right" } };

        var scales = ChartTestData.Serialize(options).GetProperty("scales");

        Assert.Equal(["x", "y"], scales.EnumerateObject().Select(p => p.Name));
        Assert.Equal("right", scales.GetProperty("y").GetProperty("position").GetString());
    }

    [Fact]
    public void AxisTitle_SendsPaddingOnlyWhenSet()
    {
        var options = new LineChartOptions();
        options.Scales.Y!.Title = new ChartAxesTitle { Text = "Sales" };

        Assert.False(Scale(options, "y").GetProperty("title").TryGetProperty("padding", out _));

        options.Scales.Y.Title.Padding = new ChartPadding { Top = 4, Bottom = 8 };

        Assert.Equal("{\"top\":4,\"bottom\":8}", Scale(options, "y").GetProperty("title").GetProperty("padding").GetRawText());
    }

    [Fact]
    public void AxisType_HasRadialLinear() =>
        Assert.Equal("radialLinear", ChartAxesType.RadialLinear);

    [Theory]
    [MemberData(nameof(ChartLevelCases))]
    public void ChartLevel_SendsEachNewOptionOnlyWhenSet(string chartType, string name)
    {
        var options = (ChartOptions)ChartTestData.CreateOptions(chartType);

        Assert.False(ChartTestData.Serialize(options).TryGetProperty(name, out _));

        chartLevelCases[name].Set(options);

        Assert.Equal(chartLevelCases[name].Json, ChartTestData.Serialize(options).GetProperty(name).GetRawText());
    }

    [Fact]
    public void Animation_SendsArcAnimationOptionsOnlyWhenSet()
    {
        var options = new DoughnutChartOptions { Animation = new ChartAnimation() };

        Assert.False(ChartTestData.Serialize(options).GetProperty("animation").TryGetProperty("animateRotate", out _));

        options.Animation.AnimateRotate = false;
        options.Animation.AnimateScale = true;

        var animation = ChartTestData.Serialize(options).GetProperty("animation");
        Assert.False(animation.GetProperty("animateRotate").GetBoolean());
        Assert.True(animation.GetProperty("animateScale").GetBoolean());
    }

    [Theory]
    [MemberData(nameof(CartesianChartTypes))]
    public void Interaction_SendsAxisAndIncludeInvisibleOnlyWhenSet(string chartType)
    {
        var (options, _) = CartesianOptions(chartType);
        var interaction = (Interaction)options.GetType().GetProperty("Interaction")!.GetValue(options)!;

        Assert.Equal(["intersect", "mode"], ChartTestData.Serialize(options).GetProperty("interaction").EnumerateObject().Select(p => p.Name).Order());

        interaction.Axis = "xy";
        interaction.IncludeInvisible = true;

        var sent = ChartTestData.Serialize(options).GetProperty("interaction");
        Assert.Equal("xy", sent.GetProperty("axis").GetString());
        Assert.True(sent.GetProperty("includeInvisible").GetBoolean());
    }

    [Theory]
    [MemberData(nameof(DatasetCaseNames))]
    public void Datasets_SendEachNewOptionOnlyWhenSet(string name)
    {
        var (create, set, jsonName, json) = datasetCases[name];
        var dataset = create();
        var before = ChartTestData.Serialize(dataset);

        Assert.True(!before.TryGetProperty(jsonName, out var unset) || unset.ValueKind == JsonValueKind.Null);

        set(dataset);

        Assert.Equal(json, ChartTestData.Serialize(dataset).GetProperty(jsonName).GetRawText());
    }

    [Theory]
    [MemberData(nameof(PluginCaseNames))]
    public void Plugins_SendEachNewOptionOnlyWhenSet(string name)
    {
        var options = new LineChartOptions();
        options.Plugins.Tooltip = new ChartPluginsTooltip();
        options.Plugins.Legend.Labels = new ChartPluginsLegendLabels();
        var (set, path, json) = pluginCases[name];

        Assert.Null(Find(ChartTestData.Serialize(options).GetProperty("plugins"), path));

        set(options.Plugins);

        Assert.Equal(json, Find(ChartTestData.Serialize(options).GetProperty("plugins"), path)!.Value.GetRawText());
    }

    [Theory]
    [MemberData(nameof(ArcChartTypes))]
    public void ArcOptions_SendCutoutRadiusRotationAndCircumferenceOnlyWhenSet(string chartType)
    {
        dynamic options = ChartTestData.CreateOptions(chartType);
        var names = new[] { "circumference", "cutout", "radius", "rotation" };

        Assert.DoesNotContain(((JsonElement)ChartTestData.Serialize((object)options)).EnumerateObject(), p => names.Contains(p.Name));

        options.Cutout = (ChartDimension)"60%";
        options.Radius = (ChartDimension)90;
        options.Rotation = -90;
        options.Circumference = 180;

        JsonElement sent = ChartTestData.Serialize((object)options);
        Assert.Equal("\"60%\"", sent.GetProperty("cutout").GetRawText());
        Assert.Equal("90", sent.GetProperty("radius").GetRawText());
        Assert.Equal(-90, sent.GetProperty("rotation").GetDouble());
        Assert.Equal(180, sent.GetProperty("circumference").GetDouble());
    }

    [Theory]
    [MemberData(nameof(AllChartTypes))]
    public void EveryChart_CreatesPluginsInteractionAndLayout(string chartType)
    {
        dynamic options = ChartTestData.CreateOptions(chartType);

        options.Plugins.Title.Text = "Sales";
        options.Plugins.Title.Display = true;
        options.Interaction.Mode = InteractionMode.Index;
        options.Layout.AutoPadding = false;

        JsonElement sent = ChartTestData.Serialize((object)options);
        Assert.Equal("Sales", sent.GetProperty("plugins").GetProperty("title").GetProperty("text").GetString());
        Assert.Equal("index", sent.GetProperty("interaction").GetProperty("mode").GetString());
        Assert.False(sent.GetProperty("layout").GetProperty("autoPadding").GetBoolean());
    }

    [Theory]
    [InlineData("bubble", "x")]
    [InlineData("bubble", "y")]
    [InlineData("radar", "r")]
    public void RadarAndBubbleAxes_StartAtZeroByDefault(string chartType, string axis) =>
        Assert.True(ChartTestData.Serialize(ChartTestData.CreateOptions(chartType)).GetProperty("scales").GetProperty(axis).GetProperty("beginAtZero").GetBoolean());

    [Theory]
    [InlineData("bubble")]
    [InlineData("radar")]
    public void RadarAndBubblePlugins_SendTheSameKeysAsTheOtherCharts(string chartType) =>
        Assert.Equal(["datalabels", "legend", "title"], ChartTestData.Serialize(ChartTestData.CreateOptions(chartType)).GetProperty("plugins").EnumerateObject().Select(p => p.Name).Order());

    [Fact]
    public void TooltipLine_IsLeftOutUntilSet()
    {
        var options = new LineChartOptions();

        Assert.False(ChartTestData.Serialize(options).GetProperty("plugins").TryGetProperty("tooltipLine", out _));

        options.Plugins.TooltipLine = new ChartPluginsTooltipLine { Display = false };

        Assert.Equal("{\"color\":\"grey\",\"dash\":[5,5],\"display\":false,\"width\":2}", ChartTestData.Serialize(options).GetProperty("plugins").GetProperty("tooltipLine").GetRawText());

        options.Plugins.TooltipLine = new ChartPluginsTooltipLine { Color = "red", Width = 1, Dash = [] };

        Assert.Equal("{\"color\":\"red\",\"dash\":[],\"display\":true,\"width\":1}", ChartTestData.Serialize(options).GetProperty("plugins").GetProperty("tooltipLine").GetRawText());
    }

    [Theory]
    [MemberData(nameof(RadialChartTypes))]
    public void RadialScale_SendsOnlyR(string chartType)
    {
        var scales = ChartTestData.Serialize(ChartTestData.CreateOptions(chartType)).GetProperty("scales");

        Assert.Equal(["r"], scales.EnumerateObject().Select(p => p.Name));
        Assert.Equal("{\"beginAtZero\":true}", scales.GetProperty("r").GetRawText());
    }

    [Theory]
    [MemberData(nameof(RadialCaseNames))]
    public void RadialScale_SendsEachOptionOnlyWhenSet(string name)
    {
        foreach (var (options, scales) in new (object Options, RadialScales Scales)[] { Pair(new RadarChartOptions(), o => o.Scales), Pair(new PolarAreaChartOptions(), o => o.Scales) })
        {
            Assert.False(Scale(options, "r").TryGetProperty(name, out _));

            radialCases[name].Set(scales.R!);

            Assert.Equal(radialCases[name].Json, Scale(options, "r").GetProperty(name).GetRawText());
        }
    }

    [Fact]
    public void RadarDataset_SendsRAxisIdOnlyWhenSet()
    {
        var dataset = new RadarChartDataset();

        Assert.False(ChartTestData.Serialize(dataset).TryGetProperty("rAxisID", out _));

        dataset.RAxisID = "r";

        Assert.Equal("r", ChartTestData.Serialize(dataset).GetProperty("rAxisID").GetString());
    }

    [Theory]
    [MemberData(nameof(TickCaseNames))]
    public void Ticks_SendsEachNewOptionOnlyWhenSet(string name)
    {
        var options = new LineChartOptions();
        options.Scales.Y!.Ticks = new ChartAxesTicks();

        Assert.False(Scale(options, "y").GetProperty("ticks").TryGetProperty(name, out _));

        tickCases[name].Set(options.Scales.Y.Ticks);

        Assert.Equal(tickCases[name].Json, Scale(options, "y").GetProperty("ticks").GetProperty(name).GetRawText());
    }

    #endregion

    #region Private Methods

    private static (object Options, Scales Scales) CartesianOptions(string chartType) =>
        chartType switch
        {
            "bar" => Pair(new BarChartOptions(), o => o.Scales),
            "bubble" => Pair(new BubbleChartOptions(), o => o.Scales),
            "line" => Pair(new LineChartOptions(), o => o.Scales),
            "scatter" => Pair(new ScatterChartOptions(), o => o.Scales),
            _ => throw new ArgumentOutOfRangeException(nameof(chartType), chartType, null)
        };

    private static (object Options, TScales Scales) Pair<T, TScales>(T options, Func<T, TScales> scales) where T : notnull => (options, scales(options));

    private static JsonElement? Find(JsonElement element, string path)
    {
        foreach (var part in path.Split('.'))
        {
            if (element.ValueKind != JsonValueKind.Object || !element.TryGetProperty(part, out element))
                return null;
        }

        return element;
    }

    private static JsonElement Scale(object options, string id) =>
        ChartTestData.Serialize(options).GetProperty("scales").GetProperty(id);

    private static ChartPluginsTooltip Tooltip(ChartPlugins plugins) => plugins.Tooltip!;

    #endregion

    #endregion

    #region Properties, Indexers

    public static TheoryData<string> AllChartTypes => new() { "bar", "bubble", "doughnut", "line", "pie", "polarArea", "radar", "scatter" };

    public static TheoryData<string> ArcChartTypes => new() { "doughnut", "pie" };

    public static TheoryData<string> AxisCaseNames => new(axisCases.Keys);

    public static TheoryData<string> CartesianChartTypes => new() { "bar", "bubble", "line", "scatter" };

    public static TheoryData<string, string> ChartLevelCases
    {
        get
        {
            var data = new TheoryData<string, string>();

            foreach (var chartType in new[] { "bar", "bubble", "doughnut", "line", "pie", "polarArea", "radar", "scatter" })
                foreach (var name in chartLevelCases.Keys)
                    data.Add(chartType, name);

            return data;
        }
    }

    public static TheoryData<string> DatasetCaseNames => new(datasetCases.Keys);

    public static TheoryData<string> PluginCaseNames => new(pluginCases.Keys);

    public static TheoryData<string> RadialCaseNames => new(radialCases.Keys);

    public static TheoryData<string> RadialChartTypes => new() { "polarArea", "radar" };

    public static TheoryData<string> TickCaseNames => new(tickCases.Keys);

    #endregion
}
