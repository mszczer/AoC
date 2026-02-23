namespace AoC.AoC2025.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay02
{
    private const string TestDayNumber = "TestDay02";

    [Test]
    public void Day02_Part1_EndToEnd()
    {
        var day = new Day02(TestDayNumber);
        Assert.That(day.CalculatePart1(), Is.EqualTo(1227775554));
    }

    [Test]
    public void CalculatePart1_SingleMatchingTwoDigitId_ReturnsId()
    {
        var day = new Day02(TestDayNumber, new List<(long Start, long End)> { (11, 11) });
        Assert.That(day.CalculatePart1(), Is.EqualTo(11));
    }

    [Test]
    public void CalculatePart1_SingleNonMatchingTwoDigitId_ReturnsZero()
    {
        var day = new Day02(TestDayNumber, new List<(long Start, long End)> { (12, 12) });
        Assert.That(day.CalculatePart1(), Is.EqualTo(0));
    }

    [Test]
    public void CalculatePart1_OddLengthId_IsIgnored()
    {
        var day = new Day02(TestDayNumber, new List<(long Start, long End)> { (101, 101) });
        Assert.That(day.CalculatePart1(), Is.EqualTo(0));
    }

    [Test]
    public void CalculatePart1_RangeMultipleMatchingTwoDigitIds_ReturnsSum()
    {
        var day = new Day02(TestDayNumber, new List<(long Start, long End)> { (10, 99) });
        Assert.That(day.CalculatePart1(), Is.EqualTo(495));
    }

    [Test]
    public void CalculatePart1_StartGreaterThanEnd_ReturnsZero()
    {
        var day = new Day02(TestDayNumber, new List<(long Start, long End)> { (50, 10) });
        Assert.That(day.CalculatePart1(), Is.EqualTo(0));
    }

    [Test]
    public void Day02_Part2_EndToEnd()
    {
        var day = new Day02(TestDayNumber);
        Assert.That(day.CalculatePart2(), Is.EqualTo(4174379265));
    }

    [Test]
    public void CalculatePart2_SingleRepeatingTwoDigitId_ReturnsId()
    {
        var day = new Day02(TestDayNumber, new List<(long Start, long End)> { (11, 11) });
        Assert.That(day.CalculatePart2(), Is.EqualTo(11));
    }

    [Test]
    public void CalculatePart2_SingleNonRepeatingTwoDigitId_ReturnsZero()
    {
        var day = new Day02(TestDayNumber, new List<(long Start, long End)> { (12, 12) });
        Assert.That(day.CalculatePart2(), Is.EqualTo(0));
    }

    [Test]
    public void CalculatePart2_OddLengthRepeated_ReturnsId()
    {
        var day = new Day02(TestDayNumber, new List<(long Start, long End)> { (111, 111) });
        Assert.That(day.CalculatePart2(), Is.EqualTo(111));
    }

    [Test]
    public void CalculatePart2_OddLengthNonRepeated_ReturnsZero()
    {
        var day = new Day02(TestDayNumber, new List<(long Start, long End)> { (101, 101) });
        Assert.That(day.CalculatePart2(), Is.EqualTo(0));
    }

    [Test]
    public void CalculatePart2_RangeMultipleMatchingIds_ReturnsSum()
    {
        var day = new Day02(TestDayNumber, new List<(long Start, long End)> { (10, 99) });
        Assert.That(day.CalculatePart2(), Is.EqualTo(495));
    }

    [Test]
    public void CalculatePart2_StartGreaterThanEnd_ReturnsZero()
    {
        var day = new Day02(TestDayNumber, new List<(long Start, long End)> { (50, 10) });
        Assert.That(day.CalculatePart2(), Is.EqualTo(0));
    }

    [Test]
    public void CalculatePart2_MultipleSpecificIds_ReturnsSum()
    {
        var ranges = new List<(long Start, long End)>
        {
            (11, 11),        // two-digit repeat
            (123123, 123123),// 3-digit pattern repeated twice (length 6)
            (111, 111)       // odd-length all same digit
        };
        var day = new Day02(TestDayNumber, ranges);
        Assert.That(day.CalculatePart2(), Is.EqualTo(11 + 123123 + 111));
    }
}