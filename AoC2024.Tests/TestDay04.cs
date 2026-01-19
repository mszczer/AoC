namespace AoC.AoC2024.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay04
{
    private const string TestDayNumber = "TestDay04";
    private Day04 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day04(TestDayNumber);
    }

    [Test]
    public void Day04_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(18));
    }

    [Test]
    public void Day04_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(9));
    }

    [Test]
    public void Day04_EmptyInput_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            var day04 = new Day04("EmptyDay", []);
        });
    }

    [Test]
    public void Day04_NoPatternFound_Part1_ReturnsZero()
    {
        var input = new List<string> { "AAAA", "AAAA", "AAAA", "AAAA" };
        var day = new Day04("NoPatternDay", input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(0));
    }

    [Test]
    public void Day04_NoPatternFound_Part2_ReturnsZero()
    {
        var input = new List<string> { "AAAA", "AAAA", "AAAA", "AAAA" };
        var day = new Day04("NoPatternDay", input);
        Assert.That(day.CalculatePart2(), Is.EqualTo(0));
    }

    [Test]
    public void Day04_SingleRow_Part1_ReturnsExpected()
    {
        var inputRow = new List<string> { "XMAS" };
        var dayRow = new Day04("SingleRowDay", inputRow);
        Assert.That(dayRow.CalculatePart1(), Is.EqualTo(1));
    }

    [Test]
    public void Day04_SingleColumn_Part1_ReturnsExpected()
    {
        var inputCol = new List<string> { "X", "M", "A", "S" };
        var dayCol = new Day04("SingleColDay", inputCol);
        Assert.That(dayCol.CalculatePart1(), Is.EqualTo(1));
    }
}