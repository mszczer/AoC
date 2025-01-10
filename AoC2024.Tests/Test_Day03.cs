using AoC.AoC2024;

namespace AoC.AoC2024.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class Test_Day03
{
    private const string TestDayNumber = "Test_Day03";
    private Day03 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day03(TestDayNumber);
    }

    [Test]
    public void Day03_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(161));
    }

    [Test]
    public void Day03_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(48));
    }
}