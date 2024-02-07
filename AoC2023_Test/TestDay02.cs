using AoC.AoC2023;

namespace AoC.AoC2023_Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay02
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
        Assert.That(_day.CalculatePart1(), Is.EqualTo(8));
    }

    [Test]
    public void Day02_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(2286));
    }
}