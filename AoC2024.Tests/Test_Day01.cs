using AoC.AoC2024;

namespace AoC.AoC2024.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class Test_Day01
{
    private const string TestDayNumber = "Test_Day01";
    private Day01 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day01(TestDayNumber);
    }

    [Test]
    public void Day01_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(11));
    }
}