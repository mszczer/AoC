namespace AoC.AoC2025.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay06
{
    private const string TestDayNumber = "TestDay06";


    [Test]
    public void Day06_Part1_EndToEnd()
    {
        var day = new Day06(TestDayNumber);
        Assert.That(day.CalculatePart1(), Is.EqualTo(4277556));
    }

    [Test]
    public void Part1_EmptyInput_ThrowsException()
    {
        var input = new List<string>();
        Assert.Throws<InvalidOperationException>(() => new Day06(TestDayNumber, input));
    }

    [Test]
    public void Part1_SingleRowOnly_ThrowsException()
    {
        var input = new List<string> { "+ *" };
        Assert.Throws<InvalidOperationException>(() => new Day06(TestDayNumber, input));
    }

    [Test]
    public void Part1_InvalidNumber_ThrowsException()
    {
        var input = new List<string>
        {
            "1 abc 3",
            "4 5 6",
            "+ + +"
        };
        Assert.Throws<FormatException>(() => new Day06(TestDayNumber, input));
    }

    [Test]
    public void Part1_UnknownOperation_ThrowsException()
    {
        var input = new List<string>
        {
            "1 2",
            "3 4",
            "+ -"
        };
        var day = new Day06(TestDayNumber, input);
        Assert.Throws<InvalidOperationException>(() => day.CalculatePart1());
    }

    [Test]
    public void Part1_MismatchedColumns_ThrowsException()
    {
        var input = new List<string>
        {
            "1 2 3",
            "4 5",
            "+ + +"
        };
        var day = new Day06(TestDayNumber, input);
        Assert.Throws<IndexOutOfRangeException>(() => day.CalculatePart1());
    }

    [Test]
    public void Day06_Part2_EndToEnd()
    {
        var day = new Day06(TestDayNumber);
        Assert.That(day.CalculatePart2(), Is.EqualTo(3263827));
    }
}