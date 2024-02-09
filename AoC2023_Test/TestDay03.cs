using AoC.AoC2023;

namespace AoC.AoC2023_Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay03
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
        Assert.That(_day.CalculatePart1(), Is.EqualTo(4361));
    }

    [Test]
    public void Day32_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(2286));
    }
}