namespace AoC.AoC2023.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay03
{
    private const string TestDayNumber = "TestDay03";
    private Day03 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day03(TestDayNumber);
    }

    [Test]
    public void Day03_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(4361));
    }

    [Test]
    public void Day03_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(467835));
    }

    [Test]
    public void Day03_EmptyInput_Part1_ReturnsZero()
    {
        var day = new Day03("TestDay03_Empty");
        Assert.That(day.CalculatePart1(), Is.Zero);
    }

    [Test]
    public void Day03_EmptyInput_Part2_ReturnsZero()
    {
        var day = new Day03("TestDay03_Empty");
        Assert.That(day.CalculatePart2(), Is.Zero);
    }

    [Test]
    public void Day03_TrailingNumber_Part1_CapturedAndProcessed()
    {
        var day = new Day03("TestDay03_TrailingNumber");
        Assert.That(day.CalculatePart1(), Is.EqualTo(12 + 34));
    }

    [Test]
    public void Day03_TrailingNumber_Part2_CapturedAndProcessed()
    {
        var day = new Day03("TestDay03_TrailingNumber");
        Assert.That(day.CalculatePart2(), Is.EqualTo(12 * 34));
    }

    [Test]
    public void Day03_SymbolsOnly_Part1_ReturnsZero()
    {
        var day = new Day03("TestDay03_SymbolsOnly");
        Assert.That(day.CalculatePart1(), Is.Zero);
    }

    [Test]
    public void Day03_SymbolsOnly_Part2_ReturnsZero()
    {
        var day = new Day03("TestDay03_SymbolsOnly");
        Assert.That(day.CalculatePart2(), Is.Zero);
    }

    [Test]
    public void Day03_SingleAdjacentSymbol_Part1_ReturnsSinglePartValue()
    {
        var day = new Day03("TestDay03_SingleAdjacent");
        Assert.That(day.CalculatePart1(), Is.EqualTo(12));
    }

    [Test]
    public void Day03_SingleAdjacentSymbol_Part2_DoesNotProduceProduct()
    {
        var day = new Day03("TestDay03_SingleAdjacent");
        Assert.That(day.CalculatePart2(), Is.Zero);
    }
}