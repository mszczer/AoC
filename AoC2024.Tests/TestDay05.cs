namespace AoC.AoC2024.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay05
{
    private const string TestDayNumber = "TestDay05";
    private Day05 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day05(TestDayNumber);
    }

    [Test]
    public void Day05_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(143));
    }

    [Test]
    public void Day05_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(123));
    }

    [Test]
    public void Day05_EmptyInput_Part1_ReturnsZero()
    {
        var day = new Day05("EmptyInput", new List<string>());
        Assert.That(day.CalculatePart1(), Is.EqualTo(0));
    }

    [Test]
    public void Day05_EmptyInput_Part2_ReturnsZero()
    {
        var day = new Day05("EmptyInput", new List<string>());
        Assert.That(day.CalculatePart2(), Is.EqualTo(0));
    }

    [Test]
    public void Day05_MalformedInput_Part1_DoesNotThrow()
    {
        var malformedInput = new List<string>
        {
            "not|a|rule",
            "",
            "not,a,number"
        };
        var day = new Day05("MalformedInput", malformedInput);
        Assert.DoesNotThrow(() => day.CalculatePart1());
    }

    [Test]
    public void Day05_MalformedInput_Part2_DoesNotThrow()
    {
        var malformedInput = new List<string>
        {
            "not|a|rule",
            "",
            "not,a,number"
        };
        var day = new Day05("MalformedInput", malformedInput);
        Assert.DoesNotThrow(() => day.CalculatePart2());
    }

    [Test]
    public void Day05_SinglePageUpdate_Part1_ReturnsMiddlePage()
    {
        var input = new List<string>
        {
            "1|2",
            "",
            "42"
        };
        var day = new Day05("SinglePageUpdate", input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(42));
    }

    [Test]
    public void Day05_MultipleSwapsRequired_Part2_GreaterThanZero()
    {
        var input = new List<string>
        {
            "1|2",
            "2|3",
            "",
            "3,2,1"
        };
        var day = new Day05("MultipleSwaps", input);
        Assert.That(day.CalculatePart2(), Is.GreaterThan(0));
    }
}