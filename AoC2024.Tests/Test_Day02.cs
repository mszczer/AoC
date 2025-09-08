namespace AoC.AoC2024.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class Test_Day02
{
    private const string TestDayNumber = "Test_Day02";
    private Day02 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day02(TestDayNumber);
    }

    [Test]
    public void Day02_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(2));
    }

    [Test]
    public void Day02_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(4));
    }

    [Test]
    public void Day02_CalculatePart1_SingleElementReport_IsSafe()
    {
        var input = new List<string> { "5" };
        var day = new Day02("SingleElement", input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(1));
    }

    [Test]
    public void Day02_CalculatePart2_SingleElementReport_IsSafe()
    {
        var input = new List<string> { "5" };
        var day = new Day02("SingleElement", input);
        Assert.That(day.CalculatePart2(), Is.EqualTo(1));
    }

    [Test]
    public void Day02_MaxAllowedDifference_IsSafe()
    {
        var input = new List<string> { "1 4" };
        var day = new Day02("MaxDiff", input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(1));
    }

    [Test]
    public void Day02_ReportWithRepeatedValues_IsUnsafe()
    {
        var input = new List<string> { "2 2 2" };
        var day = new Day02("RepeatedValues", input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(0));
    }

    [Test]
    public void Day02_ReportWithTwoBadValues_IsNotSafeWithTolerance()
    {
        var input = new List<string> { "1 5 1" };
        var day = new Day02("TwoBadValues", input);
        Assert.That(day.CalculatePart2(), Is.EqualTo(0));
    }
}