namespace AoC.AoC2025.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay03
{
    private const string TestDayNumber = "TestDay03";

    [Test]
    public void Day03_Part1_EndToEnd()
    {
        var day = new Day03(TestDayNumber);
        Assert.That(day.CalculatePart1(), Is.EqualTo(357));
    }

    [Test]
    public void Day03_Part1_InMemoryInputs()
    {
        var inputs = new List<string> { "79", "1234", "991" };
        var day = new Day03("InMemory", inputs);
        // "79" -> 79, "1234" -> 34, "991" -> 99 => sum = 212
        Assert.That(day.CalculatePart1(), Is.EqualTo(212));
    }

    [Test]
    public void Day03_Part1_SingleBank_DirectCtor()
    {
        var day = new Day03("Single", new[] { "97" });
        Assert.That(day.CalculatePart1(), Is.EqualTo(97));
    }

    [Test]
    public void Day03_Constructor_Throws_OnEmptyInjectedInput()
    {
        Assert.Throws<InvalidOperationException>(() => new Day03("EmptyInjected", []));
    }

    [Test]
    public void Day03_Part2_EndToEnd()
    {
        var day = new Day03(TestDayNumber);
        Assert.That(day.CalculatePart2(), Is.EqualTo(3121910778619));
    }

    [Test]
    public void Day03_Constructor_Throws_OnNullInjectedInput()
    {
        Assert.Throws<ArgumentNullException>(() => new Day03("NullInjected", null!));
    }

    [Test]
    public void Day03_Part1_Handles_LeadingZeros()
    {
        var inputs = new List<string> { "009" };
        var day = new Day03("LeadingZeros", inputs);
        // best 2-digit selection from "009" is "09" -> parsed as 9
        Assert.That(day.CalculatePart1(), Is.EqualTo(9));
    }

    [Test]
    public void Day03_Part2_SingleBank_ExactLength()
    {
        var bank = "123456789012"; // exactly 12 chars
        var day = new Day03("Exact12", new[] { bank });
        Assert.That(day.CalculatePart2(), Is.EqualTo(long.Parse(bank)));
    }

    [Test]
    public void Day03_Part2_Throws_OnShortBank()
    {
        var inputs = new[] { "123" }; // shorter than 12
        var day = new Day03("ShortBank", inputs);
        Assert.Throws<ArgumentException>(() => day.CalculatePart2());
    }
}