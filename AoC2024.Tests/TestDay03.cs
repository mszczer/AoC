namespace AoC.AoC2024.Tests;

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
        Assert.That(_day.CalculatePart1(), Is.EqualTo(161));
    }

    [Test]
    public void Day03_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(48));
    }

    [Test]
    public void Day03_EmptyInput_ReturnsZero()
    {
        var day = new Day03("EmptyInput", []);
        Assert.That(day.CalculatePart1(), Is.EqualTo(0));
        Assert.That(day.CalculatePart2(), Is.EqualTo(0));
    }

    [Test]
    public void Day03_OnlyControlInstructions_ReturnsZero()
    {
        var input = new List<string> { "do()", "don't()", "do()", "don't()" };
        var day = new Day03("ControlOnly", input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(0));
        Assert.That(day.CalculatePart2(), Is.EqualTo(0));
    }

    [Test]
    public void Day03_InvalidInstruction_Ignored_ReturnsZero()
    {
        var input = new List<string> { "mul(1)", "mul(1,2,3)", "mul(a,b)" };
        var day = new Day03("InvalidInstructions", input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(0));
    }

    [Test]
    public void Day03_MixedInstructions_Part1_CorrectlyProcessesValidOnes()
    {
        var input = new List<string> { "do()", "mul(2,3)", "don't()", "mul(4,5)", "mul(1,2)" };
        var day = new Day03("Mixed", input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(2 * 3 + 4 * 5 + 1 * 2));
    }

    [Test]
    public void Day03_MixedInstructions_Part2_CorrectlyProcessesValidOnes()
    {
        var input = new List<string> { "do()", "mul(2,3)", "don't()", "mul(4,5)", "mul(1,2)" };
        var day = new Day03("Mixed", input);
        Assert.That(day.CalculatePart2(), Is.EqualTo(2 * 3));
    }
}