using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022Test;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay15
{
    private const string TestDayNumber = "Test_Day15";
    private Day15 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day15(TestDayNumber);
    }

    [Test]
    public void Day15_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(26));
    }

    [Test]
    public void Day15_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(140));
    }
}