using BlazorExpress.ChartJS.Tests.Infrastructure;

namespace BlazorExpress.ChartJS.Tests.Serialization;

/// <summary>
/// Locks the JSON of the date types, time axis options and date-typed chart data (rule "date values and time zones"):
/// wall-clock values are sent without an offset, instants in UTC with <c>Z</c>, and unset options are left out.
/// </summary>
public class TimeAxisJsonTests
{
    #region Methods

    #region Public Methods

    [Fact]
    public void ChartTimeValue_DateOnly_IsAWallClockDate() =>
        Assert.Equal("\"2026-01-05\"", Json((ChartTimeValue)new DateOnly(2026, 1, 5)));

    [Fact]
    public void ChartTimeValue_UnspecifiedDateTime_IsAWallClockTime() =>
        Assert.Equal("\"2026-01-05T09:00:00.000\"", Json((ChartTimeValue)new DateTime(2026, 1, 5, 9, 0, 0, DateTimeKind.Unspecified)));

    [Fact]
    public void ChartTimeValue_UtcDateTime_IsAnInstant() =>
        Assert.Equal("\"2026-01-05T09:00:00.250Z\"", Json((ChartTimeValue)new DateTime(2026, 1, 5, 9, 0, 0, 250, DateTimeKind.Utc)));

    [Fact]
    public void ChartTimeValue_LocalDateTime_IsConvertedToUtc()
    {
        var local = new DateTime(2026, 7, 1, 12, 30, 0, DateTimeKind.Local);
        var expected = $"\"{local.ToUniversalTime():yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff}Z\"";

        Assert.Equal(expected, Json((ChartTimeValue)local));
    }

    [Theory]
    [InlineData(0, "\"2026-01-05T09:00:00.000Z\"")]
    [InlineData(5.5, "\"2026-01-05T03:30:00.000Z\"")]
    [InlineData(-5, "\"2026-01-05T14:00:00.000Z\"")]
    public void ChartTimeValue_DateTimeOffset_IsAnInstantInUtc(double offsetHours, string expected) =>
        Assert.Equal(expected, Json((ChartTimeValue)new DateTimeOffset(2026, 1, 5, 9, 0, 0, TimeSpan.FromHours(offsetHours))));

    [Fact]
    public void ChartTimeValue_EqualsBySentValue()
    {
        ChartTimeValue a = new DateOnly(2026, 1, 5);
        ChartTimeValue b = new DateOnly(2026, 1, 5);
        ChartTimeValue instant = new DateTimeOffset(2026, 1, 5, 9, 0, 0, TimeSpan.Zero);
        ChartTimeValue sameInstant = new DateTime(2026, 1, 5, 9, 0, 0, DateTimeKind.Utc);

        Assert.True(a == b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.True(instant == sameInstant);
        Assert.True(a != instant);
        Assert.Equal("2026-01-05", a.ToString());
        Assert.Contains(b, new List<ChartTimeValue> { a });
    }

    [Fact]
    public void ChartTimeValue_IsNeverReadFromJson() =>
        Assert.Throws<NotSupportedException>(() => JsonSerializer.Deserialize<ChartTimeValue>("\"2026-01-05\""));

    [Fact]
    public void ChartAxesTime_WritesEveryOptionThatIsSet()
    {
        var time = new ChartAxesTime
        {
            DisplayFormats = new() { Day = "dd MMM", Hour = "HH:mm", Millisecond = "HH:mm:ss.fff", Minute = "t", Month = "MMM yyyy", Quarter = "MMM", Second = "T", Week = "d MMM", Year = "yyyy" },
            IsoWeekday = DayOfWeek.Monday,
            MinUnit = ChartTimeUnit.Hour,
            Round = ChartTimeUnit.Minute,
            TooltipFormat = "ddd, dd MMM yyyy HH:mm",
            Unit = ChartTimeUnit.Day
        };

        Assert.Equal(
            "{\"displayFormats\":{\"day\":\"dd MMM\",\"hour\":\"HH:mm\",\"millisecond\":\"HH:mm:ss.fff\",\"minute\":\"t\",\"month\":\"MMM yyyy\",\"quarter\":\"MMM\",\"second\":\"T\",\"week\":\"d MMM\",\"year\":\"yyyy\"},"
            + "\"isoWeekday\":1,\"minUnit\":\"hour\",\"round\":\"minute\",\"tooltipFormat\":\"ddd, dd MMM yyyy HH:mm\",\"unit\":\"day\"}",
            Json(time));
    }

    [Fact]
    public void ChartAxesTime_LeavesOutUnsetOptions()
    {
        Assert.Equal("{}", Json(new ChartAxesTime()));
        Assert.Equal("{\"unit\":\"quarter\"}", Json(new ChartAxesTime { Unit = ChartTimeUnit.Quarter }));
        Assert.Equal("{}", Json(new ChartAxesTimeDisplayFormats()));
    }

    [Theory]
    [InlineData(ChartTimeUnit.Millisecond, "millisecond")]
    [InlineData(ChartTimeUnit.Second, "second")]
    [InlineData(ChartTimeUnit.Minute, "minute")]
    [InlineData(ChartTimeUnit.Hour, "hour")]
    [InlineData(ChartTimeUnit.Day, "day")]
    [InlineData(ChartTimeUnit.Week, "week")]
    [InlineData(ChartTimeUnit.Month, "month")]
    [InlineData(ChartTimeUnit.Quarter, "quarter")]
    [InlineData(ChartTimeUnit.Year, "year")]
    public void ChartTimeUnit_IsSentInCamelCase(ChartTimeUnit unit, string expected) =>
        Assert.Equal($"{{\"unit\":\"{expected}\"}}", Json(new ChartAxesTime { Unit = unit }));

    [Fact]
    public void ChartAxes_TimeAndTicksSource_AreSentOnlyWhenSet()
    {
        var unset = ChartTestData.Serialize(new ChartAxes());
        var set = ChartTestData.Serialize(new ChartAxes { Type = ChartAxesType.Time, Time = new() { Unit = ChartTimeUnit.Day }, Ticks = new() { Source = "data" } });

        Assert.False(unset.TryGetProperty("time", out _));
        Assert.Equal("{\"unit\":\"day\"}", set.GetProperty("time").GetRawText());
        Assert.Equal("data", set.GetProperty("ticks").GetProperty("source").GetString());
        Assert.False(ChartTestData.Serialize(new ChartAxesTicks()).TryGetProperty("source", out _));
    }

    [Fact]
    public void ChartAxes_DateLimits_AreSentInsteadOfTheNumbers()
    {
        var axes = ChartTestData.Serialize(new ChartAxes
        {
            Max = 10, MaxTime = new DateOnly(2026, 1, 31),
            Min = 1, MinTime = new DateOnly(2026, 1, 1),
            SuggestedMax = 9, SuggestedMaxTime = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
            SuggestedMin = 2, SuggestedMinTime = new DateTimeOffset(2025, 12, 31, 0, 0, 0, TimeSpan.Zero)
        });

        Assert.Equal("2026-01-31", axes.GetProperty("max").GetString());
        Assert.Equal("2026-01-01", axes.GetProperty("min").GetString());
        Assert.Equal("2026-02-01T00:00:00.000Z", axes.GetProperty("suggestedMax").GetString());
        Assert.Equal("2025-12-31T00:00:00.000Z", axes.GetProperty("suggestedMin").GetString());
    }

    [Fact]
    public void ChartAxes_NumericLimits_AreSentAsBefore()
    {
        var axes = ChartTestData.Serialize(new ChartAxes { Max = 10, Min = 1, SuggestedMax = 9, SuggestedMin = 2 });
        var unset = ChartTestData.Serialize(new ChartAxes());

        Assert.Equal(10, axes.GetProperty("max").GetDouble());
        Assert.Equal(1, axes.GetProperty("min").GetDouble());
        Assert.Equal(9, axes.GetProperty("suggestedMax").GetDouble());
        Assert.Equal(2, axes.GetProperty("suggestedMin").GetDouble());
        Assert.Equal("{\"beginAtZero\":true,\"stacked\":false}", unset.GetRawText());
    }

    [Fact]
    public void ChartData_TimeLabels_AreSentInsteadOfLabels()
    {
        var data = ChartTestData.Serialize(new ChartData
        {
            Labels = ["ignored"],
            TimeLabels = [new DateOnly(2026, 1, 1), new DateTime(2026, 1, 2, 9, 30, 0), new DateTimeOffset(2026, 1, 3, 0, 0, 0, TimeSpan.Zero)]
        });

        Assert.Equal("[\"2026-01-01\",\"2026-01-02T09:30:00.000\",\"2026-01-03T00:00:00.000Z\"]", data.GetProperty("labels").GetRawText());
    }

    [Fact]
    public void ChartData_StringLabels_AreSentAsBefore()
    {
        Assert.Equal("{\"labels\":[\"A\",\"B\"]}", Json(new ChartData { Labels = ["A", "B"] }));
        Assert.Equal("{}", Json(new ChartData()));
    }

    [Fact]
    public void LineChartDataset_TimeData_IsSentAsPointsWithGaps()
    {
        var dataset = ChartTestData.Serialize(new LineChartDataset
        {
            Data = [1, 2],
            TimeData = [new(new DateTime(2026, 1, 5, 9, 0, 0), 21.5), new(new DateTime(2026, 1, 5, 9, 7, 0), null)]
        });

        Assert.Equal("[{\"x\":\"2026-01-05T09:00:00.000\",\"y\":21.5},{\"x\":\"2026-01-05T09:07:00.000\",\"y\":null}]", dataset.GetProperty("data").GetRawText());
    }

    [Fact]
    public void ScatterChartDataset_TimeData_IsSentAsPoints()
    {
        var dataset = ChartTestData.Serialize(new ScatterChartDataset { TimeData = [new(new DateOnly(2026, 1, 5), 3)] });

        Assert.Equal("[{\"x\":\"2026-01-05\",\"y\":3}]", dataset.GetProperty("data").GetRawText());
    }

    [Fact]
    public void BubbleChartDataset_TimeData_IsSentAsBubbles()
    {
        var dataset = ChartTestData.Serialize(new BubbleChartDataset
        {
            Data = [new(1, 2, 3)],
            TimeData = [new(new DateTime(2026, 1, 5, 9, 0, 0, DateTimeKind.Utc), 21.5, 6)]
        });

        Assert.Equal("[{\"x\":\"2026-01-05T09:00:00.000Z\",\"y\":21.5,\"r\":6}]", dataset.GetProperty("data").GetRawText());
    }

    [Fact]
    public void BarChartDataset_TimeData_WinsOverFloatingDataAndData()
    {
        var timeData = new List<ChartTimePoint> { new(new DateOnly(2026, 1, 5), 12) };
        var all = ChartTestData.Serialize(new BarChartDataset { Data = [1], FloatingData = [(1, 2)], TimeData = timeData });
        var floating = ChartTestData.Serialize(new BarChartDataset { Data = [1], FloatingData = [(1, 2)] });
        var plain = ChartTestData.Serialize(new BarChartDataset { Data = [1] });

        Assert.Equal("[{\"x\":\"2026-01-05\",\"y\":12}]", all.GetProperty("data").GetRawText());
        Assert.Equal("[[1,2]]", floating.GetProperty("data").GetRawText());
        Assert.Equal("[1]", plain.GetProperty("data").GetRawText());
    }

    [Fact]
    public void DatasetData_TimeConstructors_HoldTimePoints()
    {
        Assert.Equal(new ChartTimePoint(new DateOnly(2026, 3, 1), 30), new LineChartDatasetData("A", new DateOnly(2026, 3, 1), 30).Data);
        Assert.Equal(new ChartTimePoint(new DateOnly(2026, 3, 1), null), new BarChartDatasetData("A", new DateOnly(2026, 3, 1), null).Data);
        Assert.Equal(new ChartTimePoint(new DateOnly(2026, 3, 1), 2), new ScatterChartDatasetData("A", new DateOnly(2026, 3, 1), 2).Data);
        Assert.Equal(new BubbleChartTimePoint(new DateOnly(2026, 3, 1), 2, 5), new BubbleChartDatasetData("A", new DateOnly(2026, 3, 1), 2, 5).Data);
        Assert.Equal(new[] { 1d, 5d }, new BarChartDatasetData("A", 1, 5).Data);
    }

    #endregion

    #region Private Methods

    private static string Json(object value) => ChartTestData.Serialize(value).GetRawText();

    #endregion

    #endregion
}
