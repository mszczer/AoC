namespace AoC.AoC2023.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay02
{
    private const string TestDayNumber = "TestDay02";
    private Day02 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day02(TestDayNumber);
    }

    [Test]
    public void Day02_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(8));
    }

    [Test]
    public void Day02_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(2286));
    }

    [Test]
    public void Day02_WellFormedInput_Part1_ParsesAndComputesCorrectly()
    {
        var day = new Day02("TestDay02_WellFormed");
        Assert.That(day.CalculatePart1(), Is.EqualTo(10));
    }

    [Test]
    public void Day02_WellFormedInput_Part2_ParsesAndComputesCorrectly()
    {
        var day = new Day02("TestDay02_WellFormed");
        Assert.That(day.CalculatePart2(), Is.EqualTo(30));
    }

    [Test]
    public void Day02_MalformedLines_Part1_AreIgnored_NoException()
    {
        var day = new Day02("TestDay02_Malformed");
        Assert.That(day.CalculatePart1(), Is.EqualTo(10));
    }

    [Test]
    public void Day02_MalformedLines_Part2_AreIgnored_NoException()
    {
        var day = new Day02("TestDay02_Malformed");
        Assert.That(day.CalculatePart2(), Is.EqualTo(30));
    }
}