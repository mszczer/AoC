using AoC.AoC2023;

namespace AoC.AoC2023.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]

public class Test_Day435
{
    private const string TestDayNumber = "Test_Day435";
    private DayNo _day;

    [SetUp]
    public void Setup()
    {
        _day = new DayNo(TestDayNumber);
    }

    [Test]
    public void DayNo_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(int.MaxValue));
    }

    [Test]
    public void DayNo_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(int.MaxValue));
    }
}