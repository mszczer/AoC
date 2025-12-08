namespace AoC.AoC2024.Tests;

public class Test_Day08
{
    private const string TestDayNumber = "Test_Day08";
    private Day08 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day08(TestDayNumber);
    }

    [Test]
    public void Day08_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(14));
    }

    [Test]
    public void Day08_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(34));
    }
}