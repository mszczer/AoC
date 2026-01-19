namespace AoC.AoC2023.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay01
{
    private const string TestDayNumber = "TestDay01-1";
    private const string TestDayNumberPart2 = "TestDay01-2";
    private Day01 _day;
    private Day01 _dayPart2;

    [SetUp]
    public void Setup()
    {
        _day = new Day01(TestDayNumber);
        _dayPart2 = new Day01(TestDayNumberPart2);
    }

    [Test]
    public void Day01_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(142));
    }

    [Test]
    public void Day01_Part2_EndToEnd()
    {
        Assert.That(_dayPart2.CalculatePart2(), Is.EqualTo(281));
    }
}