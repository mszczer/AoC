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

    [Test]
    public void Day07_Part2_EndToEnd()
    {
        var day = new Day07(TestDayNumber);
        Assert.That(day.CalculatePart2(), Is.EqualTo(40L));
    }

    [Test]
    public void Day07_Part2_NoSplitters_OnePath()
    {
        var input = new List<string>
        {
            "S",
            ".",
            ".",
            "."
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart2(), Is.EqualTo(1L));
    }

    [Test]
    public void Day07_Part2_SingleSplitter_TwoPaths()
    {
        var input = new List<string>
        {
            "..S..",
            ".....",
            "..^..",
            "....."
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart2(), Is.EqualTo(2L));
    }

    [Test]
    public void Day07_Part2_CascadingSplitters_ExponentialGrowth()
    {
        var input = new List<string>
        {
            "..S..",
            ".....",
            "..^..",
            ".....",
            ".^.^."
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart2(), Is.EqualTo(4L));
    }

    [Test]
    public void Day07_Part2_PathGoesOutOfBounds_CountsZero()
    {
        var input = new List<string>
        {
            "S.",
            "..",
            "^."
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart2(), Is.EqualTo(1L));
    }

    [Test]
    public void Day07_Part2_BothPathsGoOutOfBounds_CountsZero()
    {
        var input = new List<string>
        {
            "S",
            ".",
            "^"
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart2(), Is.Zero);
    }

    [Test]
    public void Day07_Part2_StartOnLastRow_OnePath()
    {
        var input = new List<string>
        {
            ".",
            "S"
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart2(), Is.EqualTo(1L));
    }

    [Test]
    public void Day07_Part2_ComplexMergingPaths()
    {
        var input = new List<string>
        {
            "...S...",
            ".......",
            "...^...",
            ".......",
            "..^.^..",
            ".......",
            ".^...^."
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart2(), Is.GreaterThan(0L));
    }

    [Test]
    public void Day07_Part2_WideSplitterPattern()
    {
        var input = new List<string>
        {
            ".....S.....",
            "...........",
            ".....^.....",
            "...........",
            "....^.^....",
            "...........",
            "...^...^..."
        };

        var day = new Day07(TestDayNumber, input);
        Assert.That(day.CalculatePart2(), Is.EqualTo(6L));
    }
}