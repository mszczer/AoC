namespace AoC.AoC2025.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay05
{
    private const string TestDayNumber = "TestDay05";

    [Test]
    public void Day05_Part1_EndToEnd()
    {
        var day = new Day05(TestDayNumber);
        Assert.That(day.CalculatePart1(), Is.EqualTo(3));
    }

    //[Test]
    //public void Day05_Part2_EndToEnd()
    //{
    //    var day = new Day05(TestDayNumber);
    //    Assert.That(day.CalculatePart2(), Is.EqualTo(long.MaxValue));
    //}
}