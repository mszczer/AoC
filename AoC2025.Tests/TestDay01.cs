namespace AoC.AoC2025.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay01
{
    private const string TestDayNumber = "TestDay01";
    private Day01 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day01(TestDayNumber);
    }

    [Test]
    public void Day01_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(3));
    }

    [Test]
    public void Day01_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(6));
    }

}