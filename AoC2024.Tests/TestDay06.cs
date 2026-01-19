using AoC.AoC2024;

namespace AoC.AoC2024.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]

public class TestDay06
{
    private const string TestDayNumber = "TestDay06";
    private Day06 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day06(TestDayNumber);
    }

    [Test]
    public void Day06_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(41));
    }

    [Test]
    public void Day06_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(6));
    }
}
