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
}