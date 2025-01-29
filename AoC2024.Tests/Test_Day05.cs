using AoC.AoC2024;

namespace AoC.AoC2024.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class Test_Day05
{
    private const string TestDayNumber = "Test_Day05";
    private Day05 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day05(TestDayNumber);
    }

    [Test]
    public void Day05_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(143));
    }

    [Test]
    public void Day05_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(123));
    }
}