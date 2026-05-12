namespace AoC.AoC2025.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay07
{
    private const string TestDayNumber = "TestDay07";

    [Test]
    public void Day07_Part1_EndToEnd()
    {
        var day = new Day07(TestDayNumber);
        Assert.That(day.CalculatePart1(), Is.EqualTo(21));
    }

    [Test]
    public void Day07_Part1_SingleBeamSingleSplitter()
    {
        var input = new List<string>
        {
            "S",
            ".",
            "^"
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(1));
    }

    [Test]
    public void Day07_Part1_StartOnLastRow_DoesNotCrash()
    {
        var input = new List<string>
        {
            ".",
            "S"
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart1(), Is.Zero);
    }

    [Test]
    public void Day07_Part1_BeamWithNoSplitter()
    {
        var input = new List<string>
        {
            "S",
            ".",
            "."
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart1(), Is.Zero);
    }

    [Test]
    public void Day07_Part1_SplitterWithNoBeam()
    {
        var input = new List<string>
        {
            ".",
            "^"
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart1(), Is.Zero);
    }

    [Test]
    public void Day07_Part1_CascadingSplits()
    {
        var input = new List<string>
        {
            ".S.",
            "...",
            ".^.",
            "...",
            "^.^"
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(3));
    }

    [Test]
    public void Day07_Part1_MultipleStartSymbols()
    {
        var input = new List<string>
        {
            "S.S",
            "...",
            "^.^"
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(2));
    }

    [Test]
    public void Day07_Part1_EmptyInput_ThrowsException()
    {
        var input = new List<string>();

        Assert.Throws<InvalidOperationException>(() => new Day07(TestDayNumber, input));
    }

    [Test]
    public void Day07_Part1_SplitterOnFirstRow()
    {
        var input = new List<string>
        {
            "^",
            "."
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart1(), Is.Zero);
    }

    [Test]
    public void Day07_Part1_HorizontalBeamPropagation()
    {
        var input = new List<string>
        {
            "..S..",
            ".....",
            "..^..",
            ".....",
            "^...^"
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(1));
    }

    //[Test]
    //public void Day07_Part2_EndToEnd()
    //{
    //    var day = new Day07(TestDayNumber);
    //    Assert.That(day.CalculatePart2(), Is.EqualTo(40));
    //}
}