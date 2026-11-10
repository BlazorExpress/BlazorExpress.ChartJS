using System.Text.RegularExpressions;
using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Serialization;

/// <summary>
/// Locks where every <see cref="ChartFunction" /> property of 26110205 is sent, that a function sibling wins over its value,
/// and that with no function set every touched type sends the JSON it sent before (captured from the models before the change).
/// </summary>
public class FunctionPropertiesJsonTests
{
    #region Fields and Constants

    private const string Marker = "{\"$chartFunction\":\"f\"}";

    private static readonly string[] datasetColorProperties = ["BackgroundColor", "BorderColor", "HoverBackgroundColor", "HoverBorderColor"];

    private static readonly string[] pointColorProperties = ["PointBackgroundColor", "PointBorderColor", "PointHoverBackgroundColor", "PointHoverBorderColor"];

    private static readonly string[] segmentProperties = ["BackgroundColor", "BorderCapStyle", "BorderColor", "BorderDash", "BorderDashOffset", "BorderJoinStyle", "BorderWidth"];

    private static readonly string[] tooltipCallbackProperties =
    [
        "AfterBody", "AfterFooter", "AfterLabel", "AfterTitle", "BeforeBody", "BeforeFooter", "BeforeLabel", "BeforeTitle",
        "Footer", "Label", "LabelColor", "LabelPointStyle", "LabelTextColor", "Title"
    ];

    /// <summary>
    /// JSON the models sent before 26110205, with <c>oid</c> removed.
    /// </summary>
    private static readonly Dictionary<string, string> snapshots = new()
    {
        { "bar.colors", """{"backgroundColor":["a"],"barPercentage":0.9,"borderColor":["b"],"borderSkipped":"start","categoryPercentage":0.8,"datalabels":{"anchor":null,"backgroundColor":null,"borderColor":null,"borderWidth":2},"grouped":true,"hoverBackgroundColor":["c"],"hoverBorderColor":["d"],"skipNull":false,"hidden":false,"order":0,"type":"bar"}""" },
        { "bubble.colors", """{"backgroundColor":["a"],"borderColor":["b"],"borderWidth":3,"datalabels":{"anchor":null,"backgroundColor":null,"borderColor":null,"borderWidth":2},"hoverBackgroundColor":["c"],"hoverBorderColor":["d"],"hidden":false,"order":0}""" },
        { "doughnut.colors", """{"backgroundColor":["a"],"borderColor":["b"],"borderDashOffset":0,"datalabels":{"anchor":null,"backgroundColor":null,"borderColor":null,"borderWidth":2},"hoverBackgroundColor":["c"],"hoverBorderColor":["d"],"spacing":0,"weight":1,"hidden":false,"order":0}""" },
        { "line.colors", """{"backgroundColor":"a","borderCapStyle":"butt","borderColor":"b","borderDashOffset":0,"borderJoinStyle":"miter","borderWidth":3,"cubicInterpolationMode":"default","datalabels":{"anchor":null,"backgroundColor":null,"borderColor":null,"borderWidth":2},"fill":false,"hoverBackgroundColor":"c","hoverBorderColor":"d","hoverBorderJoinStyle":"miter","indexAxis":"x","pointBackgroundColor":["e"],"pointBorderColor":["f"],"pointHoverBackgroundColor":["g"],"pointHoverBorderColor":["h"],"segment":{"borderDash":[6,6]},"showLine":true,"stepped":false,"tension":0,"hidden":false,"order":0,"type":"line"}""" },
        { "pie.colors", """{"backgroundColor":["a"],"borderColor":["b"],"borderDashOffset":0,"datalabels":{"anchor":null,"backgroundColor":null,"borderColor":null,"borderWidth":2},"hoverBackgroundColor":["c"],"hoverBorderColor":["d"],"spacing":0,"weight":1,"hidden":false,"order":0}""" },
        { "polarArea.colors", """{"backgroundColor":["a"],"borderColor":["b"],"borderDashOffset":0,"circular":true,"datalabels":{"anchor":null,"backgroundColor":null,"borderColor":null,"borderWidth":2},"hoverBackgroundColor":["c"],"hoverBorderColor":["d"],"hidden":false,"order":0}""" },
        { "radar.colors", """{"backgroundColor":"a","borderCapStyle":"butt","borderColor":"b","borderDashOffset":0,"borderJoinStyle":"miter","borderWidth":3,"datalabels":{"anchor":null,"backgroundColor":null,"borderColor":null,"borderWidth":2},"fill":false,"hoverBackgroundColor":"c","hoverBorderColor":"d","hoverBorderJoinStyle":"miter","pointBackgroundColor":["e"],"pointBorderColor":["f"],"pointHoverBackgroundColor":["g"],"pointHoverBorderColor":["h"],"tension":0,"hidden":false,"order":0}""" },
        { "scatter.colors", """{"backgroundColor":"a","borderCapStyle":"butt","borderColor":"b","borderDashOffset":0,"borderJoinStyle":"miter","borderWidth":3,"cubicInterpolationMode":"default","datalabels":{"anchor":null,"backgroundColor":null,"borderColor":null,"borderWidth":2},"fill":false,"hoverBackgroundColor":"c","hoverBorderColor":"d","hoverBorderJoinStyle":"miter","showLine":false,"stepped":false,"tension":0,"hidden":false,"order":0}""" },
        { "line.default", """{"backgroundColor":"rgba(0, 0, 0, 0.1)","borderCapStyle":"butt","borderColor":"rgba(0, 0, 0, 0.1)","borderDashOffset":0,"borderJoinStyle":"miter","borderWidth":3,"cubicInterpolationMode":"default","datalabels":{"anchor":null,"backgroundColor":null,"borderColor":null,"borderWidth":2},"fill":false,"hoverBorderJoinStyle":"miter","indexAxis":"x","showLine":true,"stepped":false,"tension":0,"hidden":false,"order":0,"type":"line"}""" },
        { "radar.default", """{"backgroundColor":"rgba(0, 0, 0, 0.1)","borderCapStyle":"butt","borderColor":"rgba(0, 0, 0, 0.1)","borderDashOffset":0,"borderJoinStyle":"miter","borderWidth":3,"datalabels":{"anchor":null,"backgroundColor":null,"borderColor":null,"borderWidth":2},"fill":false,"hoverBorderJoinStyle":"miter","tension":0,"hidden":false,"order":0}""" },
        { "scatter.default", """{"backgroundColor":"rgba(0, 0, 0, 0.1)","borderCapStyle":"butt","borderColor":"rgba(0, 0, 0, 0.1)","borderDashOffset":0,"borderJoinStyle":"miter","borderWidth":3,"cubicInterpolationMode":"default","datalabels":{"anchor":null,"backgroundColor":null,"borderColor":null,"borderWidth":2},"fill":false,"hoverBorderJoinStyle":"miter","showLine":false,"stepped":false,"tension":0,"hidden":false,"order":0}""" },
        { "dl.bar", """{"color":"white","font":{"weight":"bold"}}""" },
        { "dl.bubble", """{"borderRadius":4,"color":"white","font":{"weight":"bold"},"padding":6}""" },
        { "dl.doughnut", """{"borderColor":"white","borderRadius":25,"borderWidth":2,"color":"white","font":{"weight":"bold"},"padding":6}""" },
        { "dl.line", """{"borderRadius":4,"color":"white","font":{"weight":"bold"},"padding":6}""" },
        { "dl.pie", """{"borderColor":"white","borderRadius":25,"borderWidth":2,"color":"white","font":{"weight":"bold"},"padding":6}""" },
        { "dl.polarArea", """{"borderColor":"white","borderRadius":25,"borderWidth":2,"color":"white","font":{"weight":"bold"},"padding":6}""" },
        { "dl.radar", """{"borderRadius":4,"color":"white","font":{"weight":"bold"},"padding":6}""" },
        { "dl.scatter", """{"borderRadius":4,"color":"white","font":{"weight":"bold"},"padding":6}""" },
        { "dsdatalabels", """{"anchor":null,"backgroundColor":null,"borderColor":null,"borderWidth":2}""" },
        { "animation", """{"delay":null,"duration":1000,"easing":"easeOutQuart","loop":null}""" },
        { "ticks", """{"display":true}""" },
        { "legend", """{"align":"center","display":true,"fullSize":true,"labels":{"useBorderRadius":false,"usePointStyle":false},"position":"top","reverse":false,"rtl":false}""" },
        { "tooltip", """{"backgroundColor":"rgba(0, 0, 0, 0.8)","bodyAlign":"left","bodyColor":"#fff","bodySpacing":2,"caretPadding":2,"caretSize":5,"displayColors":true,"enabled":true,"footerAlign":"left","footerColor":"#fff","footerFont":{"lineHeight":1.2,"size":12,"weight":"bold"},"footerMarginTop":6,"footerSpacing":2,"titleAlign":"left","titleColor":"#fff","titleFont":{"lineHeight":1.2,"size":12,"weight":"bold"},"titleMarginBottom":6,"titleSpacing":2,"xAlign":null,"yAlign":null}""" },
        { "options.base", """{"maintainAspectRatio":true,"responsive":true}""" },
        { "line.options.full", """{"interaction":{"mode":"nearest","intersect":true},"layout":{"autoPadding":true,"padding":0},"plugins":{"datalabels":{"borderRadius":4,"color":"white","font":{"weight":"bold"},"padding":6},"legend":{"align":"center","display":true,"fullSize":true,"labels":{"useBorderRadius":false,"usePointStyle":false},"position":"top","reverse":false,"rtl":false},"title":{"align":"center","color":"black","display":false},"tooltip":{"backgroundColor":"rgba(0, 0, 0, 0.8)","bodyAlign":"left","bodyColor":"#fff","bodySpacing":2,"caretPadding":2,"caretSize":5,"displayColors":true,"enabled":true,"footerAlign":"left","footerColor":"#fff","footerFont":{"lineHeight":1.2,"size":12,"weight":"bold"},"footerMarginTop":6,"footerSpacing":2,"titleAlign":"left","titleColor":"#fff","titleFont":{"lineHeight":1.2,"size":12,"weight":"bold"},"titleMarginBottom":6,"titleSpacing":2,"xAlign":null,"yAlign":null}},"scales":{"x":{"beginAtZero":true,"stacked":false,"ticks":{"display":true}},"y":{"beginAtZero":true,"stacked":false}},"animation":{"delay":null,"duration":1000,"easing":"easeOutQuart","loop":null},"maintainAspectRatio":true,"responsive":true}""" },
    };

    #endregion

    #region Methods

    #region Public Methods

    [Fact]
    public void ChartOptions_SendsTheEventFunctions()
    {
        var sent = ChartTestData.Serialize(new BarChartOptions { OnClick = F(), OnHover = F(), OnResize = F() });

        Assert.Equal(Marker, sent.GetProperty("onClick").GetRawText());
        Assert.Equal(Marker, sent.GetProperty("onHover").GetRawText());
        Assert.Equal(Marker, sent.GetProperty("onResize").GetRawText());
    }

    [Fact]
    public void ChartAnimation_SendsTheCallbacksBesideTheDefaults()
    {
        var sent = ChartTestData.Serialize(new ChartAnimation { OnComplete = F(), OnProgress = F() });

        Assert.Equal(Marker, sent.GetProperty("onComplete").GetRawText());
        Assert.Equal(Marker, sent.GetProperty("onProgress").GetRawText());
        Assert.Equal(1000, sent.GetProperty("duration").GetDouble());
        Assert.Equal("easeOutQuart", sent.GetProperty("easing").GetString());
    }

    [Fact]
    public void ChartAxesTicks_SendsTheCallback() =>
        Assert.Equal(Marker, ChartTestData.Serialize(new ChartAxesTicks { Callback = F() }).GetProperty("callback").GetRawText());

    [Fact]
    public void ChartAxesTicks_SendsTheNumberFormat() =>
        Assert.Equal("{\"currency\":\"USD\",\"style\":\"currency\"}",
            ChartTestData.Serialize(new ChartAxesTicks { Format = new ChartNumberFormat { Style = "currency", Currency = "USD" } }).GetProperty("format").GetRawText());

    [Fact]
    public void ChartNumberFormat_SendsEveryOptionCamelCased()
    {
        var format = new ChartNumberFormat
        {
            CompactDisplay = "short", Currency = "EUR", CurrencyDisplay = "code", MaximumFractionDigits = 2, MinimumFractionDigits = 1,
            MinimumIntegerDigits = 3, Notation = "compact", SignDisplay = "always", Style = "unit", Unit = "celsius", UnitDisplay = "long", UseGrouping = false
        };

        Assert.Equal(
            "{\"compactDisplay\":\"short\",\"currency\":\"EUR\",\"currencyDisplay\":\"code\",\"maximumFractionDigits\":2,\"minimumFractionDigits\":1,"
            + "\"minimumIntegerDigits\":3,\"notation\":\"compact\",\"signDisplay\":\"always\",\"style\":\"unit\",\"unit\":\"celsius\",\"unitDisplay\":\"long\",\"useGrouping\":false}",
            ChartTestData.Serialize(format).GetRawText());
    }

    [Fact]
    public void ChartNumberFormat_SendsNothingWhenEmpty() =>
        Assert.Equal("{}", ChartTestData.Serialize(new ChartNumberFormat()).GetRawText());

    [Theory]
    [MemberData(nameof(TooltipCallbackNames))]
    public void TooltipCallbacks_SendEachFunctionUnderItsName(string propertyName)
    {
        var callbacks = new ChartPluginsTooltipCallbacks();
        typeof(ChartPluginsTooltipCallbacks).GetProperty(propertyName)!.SetValue(callbacks, F());

        var sent = ChartTestData.Serialize(new ChartPluginsTooltip { Callbacks = callbacks }).GetProperty("callbacks");

        Assert.Equal("{\"" + Camel(propertyName) + "\":" + Marker + "}", sent.GetRawText());
    }

    [Fact]
    public void TooltipCallbacks_SendNothingWhenEmpty() =>
        Assert.Equal("{}", ChartTestData.Serialize(new ChartPluginsTooltipCallbacks()).GetRawText());

    [Fact]
    public void Tooltip_SendsTheFunctions()
    {
        var sent = ChartTestData.Serialize(new ChartPluginsTooltip { External = F(), Filter = F(), ItemSort = F() });

        Assert.Equal(Marker, sent.GetProperty("external").GetRawText());
        Assert.Equal(Marker, sent.GetProperty("filter").GetRawText());
        Assert.Equal(Marker, sent.GetProperty("itemSort").GetRawText());
    }

    [Fact]
    public void TooltipCallbacks_AreSentInsideTheChartOptions()
    {
        var options = new LineChartOptions();
        options.Plugins.Tooltip = new ChartPluginsTooltip { Callbacks = new ChartPluginsTooltipCallbacks { Label = F() } };

        Assert.Equal(Marker, ChartTestData.Serialize(options).GetProperty("plugins").GetProperty("tooltip").GetProperty("callbacks").GetProperty("label").GetRawText());
    }

    [Fact]
    public void Legend_SendsTheFunctions()
    {
        var sent = ChartTestData.Serialize(new ChartPluginsLegend
        {
            OnClick = F(), OnHover = F(), OnLeave = F(),
            Labels = new ChartPluginsLegendLabels { Filter = F(), GenerateLabels = F(), Sort = F() }
        });

        Assert.Equal(Marker, sent.GetProperty("onClick").GetRawText());
        Assert.Equal(Marker, sent.GetProperty("onHover").GetRawText());
        Assert.Equal(Marker, sent.GetProperty("onLeave").GetRawText());
        Assert.Equal(Marker, sent.GetProperty("labels").GetProperty("filter").GetRawText());
        Assert.Equal(Marker, sent.GetProperty("labels").GetProperty("generateLabels").GetRawText());
        Assert.Equal(Marker, sent.GetProperty("labels").GetProperty("sort").GetRawText());
    }

    [Theory]
    [MemberData(nameof(ChartTypeNames))]
    public void ChartDataLabels_SendTheFormatter(string chartType)
    {
        var options = ChartTestData.CreateOptions(chartType);
        var plugins = options.GetType().GetProperty("Plugins")!.GetValue(options)!;
        var dataLabels = plugins.GetType().GetProperty("Datalabels")!.GetValue(plugins)!;
        dataLabels.GetType().GetProperty("Formatter")!.SetValue(dataLabels, F());

        Assert.Equal(Marker, ChartTestData.Serialize(options).GetProperty("plugins").GetProperty("datalabels").GetProperty("formatter").GetRawText());
    }

    [Theory]
    [MemberData(nameof(ChartTypeNames))]
    public void DatasetDataLabels_SendTheFormatter(string chartType)
    {
        var dataset = ChartTestData.CreateDataset(chartType);
        var dataLabels = (ChartDatasetDataLabels)dataset.GetType().GetProperty("Datalabels")!.GetValue(dataset)!;
        dataLabels.Formatter = F();

        Assert.Equal(Marker, ChartTestData.Serialize(dataset).GetProperty("datalabels").GetProperty("formatter").GetRawText());
    }

    [Theory]
    [MemberData(nameof(DatasetColorFunctionCases))]
    public void DatasetColorFunction_IsSentUnderTheColorKey(string chartType, string propertyName)
    {
        var dataset = ChartTestData.CreateDataset(chartType);
        dataset.GetType().GetProperty(propertyName + "Function")!.SetValue(dataset, F());

        Assert.Equal(Marker, ChartTestData.Serialize(dataset).GetProperty(Camel(propertyName)).GetRawText());
    }

    [Theory]
    [MemberData(nameof(DatasetColorFunctionCases))]
    public void DatasetColorFunction_WinsOverTheValue(string chartType, string propertyName)
    {
        var dataset = ChartTestData.CreateDataset(chartType);
        var value = dataset.GetType().GetProperty(propertyName)!;
        value.SetValue(dataset, value.PropertyType == typeof(string) ? "red" : new List<string> { "red" });
        dataset.GetType().GetProperty(propertyName + "Function")!.SetValue(dataset, F());

        var sent = ChartTestData.Serialize(dataset);

        Assert.Equal(Marker, sent.GetProperty(Camel(propertyName)).GetRawText());
        Assert.Single(sent.EnumerateObject(), p => p.Name == Camel(propertyName));
    }

    [Theory]
    [MemberData(nameof(DatasetColorFunctionCases))]
    public void DatasetColorFunction_IsOnlyAJsonSibling(string chartType, string propertyName)
    {
        var sent = ChartTestData.Serialize(ChartTestData.CreateDataset(chartType));

        Assert.False(sent.TryGetProperty(Camel(propertyName) + "Function", out _));
        Assert.False(sent.TryGetProperty(Camel(propertyName) + "Value", out _));
    }

    [Theory]
    [MemberData(nameof(SnapshotNames))]
    public void WithoutFunctions_TouchedTypesSendTheSameJsonAsBefore(string name) =>
        Assert.Equal(snapshots[name], WithoutOid(ChartTestData.Serialize(Create(name)).GetRawText()));

    [Fact]
    public void LineSegment_StillSendsTheUntypedObject() =>
        Assert.Equal("{\"borderDash\":[6,6]}", ChartTestData.Serialize(new LineChartDataset { Segment = new { borderDash = new[] { 6, 6 } } }).GetProperty("segment").GetRawText());

    [Fact]
    public void LineSegmentStyle_WinsOverSegment()
    {
        var dataset = new LineChartDataset { Segment = new { borderDash = new[] { 6, 6 } }, SegmentStyle = new ChartLineSegment { BorderDash = [3, 3], BorderWidth = 2 } };

        Assert.Equal("{\"borderDash\":[3,3],\"borderWidth\":2}", ChartTestData.Serialize(dataset).GetProperty("segment").GetRawText());
    }

    [Fact]
    public void LineSegment_IsNotSentWhenUnset() =>
        Assert.False(ChartTestData.Serialize(new LineChartDataset()).TryGetProperty("segment", out _));

    [Fact]
    public void ChartLineSegment_SendsTheValues()
    {
        var segment = new ChartLineSegment
        {
            BackgroundColor = "red", BorderCapStyle = "round", BorderColor = "blue", BorderDash = [6, 6], BorderDashOffset = 1, BorderJoinStyle = "bevel", BorderWidth = 2
        };

        Assert.Equal(
            "{\"backgroundColor\":\"red\",\"borderCapStyle\":\"round\",\"borderColor\":\"blue\",\"borderDash\":[6,6],\"borderDashOffset\":1,\"borderJoinStyle\":\"bevel\",\"borderWidth\":2}",
            ChartTestData.Serialize(segment).GetRawText());
        Assert.Equal("{}", ChartTestData.Serialize(new ChartLineSegment()).GetRawText());
    }

    [Theory]
    [MemberData(nameof(SegmentPropertyNames))]
    public void ChartLineSegment_FunctionWinsOverTheValue(string propertyName)
    {
        var segment = new ChartLineSegment();
        var value = typeof(ChartLineSegment).GetProperty(propertyName)!;
        value.SetValue(segment, SampleValue(value.PropertyType));
        typeof(ChartLineSegment).GetProperty(propertyName + "Function")!.SetValue(segment, F());

        Assert.Equal("{\"" + Camel(propertyName) + "\":" + Marker + "}", ChartTestData.Serialize(new LineChartDataset { SegmentStyle = segment }).GetProperty("segment").GetRawText());
    }

    #endregion

    #region Private Methods

    private static string Camel(string name) => char.ToLowerInvariant(name[0]) + name[1..];

    private static object Create(string name)
    {
        switch (name)
        {
            case "bar.colors": return new BarChartDataset { BackgroundColor = ["a"], BorderColor = ["b"], HoverBackgroundColor = ["c"], HoverBorderColor = ["d"] };
            case "bubble.colors": return new BubbleChartDataset { BackgroundColor = ["a"], BorderColor = ["b"], HoverBackgroundColor = ["c"], HoverBorderColor = ["d"] };
            case "doughnut.colors": return new DoughnutChartDataset { BackgroundColor = ["a"], BorderColor = ["b"], HoverBackgroundColor = ["c"], HoverBorderColor = ["d"] };
            case "line.colors":
                return new LineChartDataset
                {
                    BackgroundColor = "a", BorderColor = "b", HoverBackgroundColor = "c", HoverBorderColor = "d", PointBackgroundColor = ["e"], PointBorderColor = ["f"],
                    PointHoverBackgroundColor = ["g"], PointHoverBorderColor = ["h"], Segment = new { borderDash = new[] { 6, 6 } }
                };
            case "pie.colors": return new PieChartDataset { BackgroundColor = ["a"], BorderColor = ["b"], HoverBackgroundColor = ["c"], HoverBorderColor = ["d"] };
            case "polarArea.colors": return new PolarAreaChartDataset { BackgroundColor = ["a"], BorderColor = ["b"], HoverBackgroundColor = ["c"], HoverBorderColor = ["d"] };
            case "radar.colors":
                return new RadarChartDataset
                {
                    BackgroundColor = "a", BorderColor = "b", HoverBackgroundColor = "c", HoverBorderColor = "d", PointBackgroundColor = ["e"], PointBorderColor = ["f"],
                    PointHoverBackgroundColor = ["g"], PointHoverBorderColor = ["h"]
                };
            case "scatter.colors": return new ScatterChartDataset { BackgroundColor = "a", BorderColor = "b", HoverBackgroundColor = "c", HoverBorderColor = "d" };
            case "line.default": return new LineChartDataset();
            case "radar.default": return new RadarChartDataset();
            case "scatter.default": return new ScatterChartDataset();
            case "dl.bar": return new BarChartDataLabels();
            case "dl.bubble": return new BubbleChartDataLabels();
            case "dl.doughnut": return new DoughnutChartDataLabels();
            case "dl.line": return new LineChartDataLabels();
            case "dl.pie": return new PieChartDataLabels();
            case "dl.polarArea": return new PolarAreaChartDataLabels();
            case "dl.radar": return new RadarChartDataLabels();
            case "dl.scatter": return new ScatterChartDataLabels();
            case "dsdatalabels": return new ChartDatasetDataLabels();
            case "animation": return new ChartAnimation();
            case "ticks": return new ChartAxesTicks();
            case "legend": return new ChartPluginsLegend { Labels = new ChartPluginsLegendLabels() };
            case "tooltip": return new ChartPluginsTooltip();
            case "options.base": return new ChartOptions();
            case "line.options.full":
                var options = new LineChartOptions { Animation = new ChartAnimation() };
                options.Plugins.Tooltip = new ChartPluginsTooltip();
                options.Plugins.Legend.Labels = new ChartPluginsLegendLabels();
                options.Scales.X!.Ticks = new ChartAxesTicks();
                return options;
            default: throw new ArgumentOutOfRangeException(nameof(name), name, null);
        }
    }

    private static ChartFunction F() => new("f");

    private static object SampleValue(Type type) =>
        type == typeof(string) ? "x"
        : type == typeof(List<double>) ? new List<double> { 1 }
        : type == typeof(double?) ? 1d
        : throw new ArgumentOutOfRangeException(nameof(type), type, null);

    private static string WithoutOid(string json) => Regex.Replace(json, "\"oid\":\"[0-9a-f-]+\",", string.Empty);

    #endregion

    #endregion

    #region Properties, Indexers

    public static TheoryData<string> ChartTypeNames => ChartTestData.ChartTypes;

    public static TheoryData<string, string> DatasetColorFunctionCases
    {
        get
        {
            var data = new TheoryData<string, string>();

            foreach (var chartType in new[] { "bar", "bubble", "doughnut", "line", "pie", "polarArea", "radar", "scatter" })
            {
                foreach (var property in datasetColorProperties)
                    data.Add(chartType, property);

                if (chartType is "line" or "radar")
                    foreach (var property in pointColorProperties)
                        data.Add(chartType, property);
            }

            return data;
        }
    }

    public static TheoryData<string> SegmentPropertyNames => new(segmentProperties);

    public static TheoryData<string> SnapshotNames => new(snapshots.Keys);

    public static TheoryData<string> TooltipCallbackNames => new(tooltipCallbackProperties);

    #endregion
}
