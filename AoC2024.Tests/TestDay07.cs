namespace AoC.AoC2024.Tests;

public class TestDay07
{
    private const string TestDayNumber = "TestDay07";
    private Day07 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day07(TestDayNumber);
    }

    [Test]
    public void Day07_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(3749));
    }

    [Test]
    public void Day07_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(11387));
    }
}
