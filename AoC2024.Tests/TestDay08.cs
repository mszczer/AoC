using System.Reflection;

namespace AoC.AoC2024.Tests;

public class TestDay08
{
    private const string TestDayNumber = "TestDay08";
    private Day08 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day08(TestDayNumber);
    }

    [Test]
    public void Day08_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(14));
    }

    [Test]
    public void Day08_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(34));
    }

    // Helper to set private _antennaMap via reflection
    private static void SetAntennaMap(Day08 instance, string[] rows)
    {
        var rowsCount = rows.Length;
        var cols = rowsCount > 0 ? rows[0].Length : 0;
        var grid = new char[rowsCount, cols];

        for (var r = 0; r < rowsCount; r++)
            for (var c = 0; c < cols; c++)
                grid[r, c] = rows[r][c];

        var field = typeof(Day08).GetField("_antennaMap", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?? throw new InvalidOperationException("_antennaMap field not found.");
        field.SetValue(instance, grid);
    }

    [Test]
    public void Part1_SimpleRow_AddsSingleAntinode()
    {
        SetAntennaMap(_day, ["AA."]);
        Assert.That(_day.CalculatePart1(), Is.EqualTo(1));
    }

    [Test]
    public void Part2_SimpleRow_HarmonicAntinodesExtendToBoundary()
    {
        SetAntennaMap(_day, ["AA.."]);
        Assert.That(_day.CalculatePart2(), Is.EqualTo(4));
    }

    [Test]
    public void Part1_DiagonalPair_ProducesDiagonalAntinode()
    {
        SetAntennaMap(_day, [
            "A..",
            ".A.",
            "..."
        ]);

        Assert.That(_day.CalculatePart1(), Is.EqualTo(1));
    }

    [Test]
    public void Part1_IgnoreOnlyDots_ReturnZero()
    {
        SetAntennaMap(_day, [
            "....",
            "....",
            "...."
        ]);

        Assert.That(_day.CalculatePart1(), Is.Zero);
    }

    [Test]
    public void Part2_IgnoreOnlyDots_ReturnZero()
    {
        SetAntennaMap(_day, [
            "....",
            "....",
            "...."
        ]);

        Assert.That(_day.CalculatePart2(), Is.Zero);
    }
}