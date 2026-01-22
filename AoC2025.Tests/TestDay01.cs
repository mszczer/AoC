using System.Collections;
using System.Reflection;

namespace AoC.AoC2025.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay01
{
    private const string TestDayNumber = "TestDay01";
    private Day01 _day;

    [SetUp]
    public void Setup()
    {
        _day = new Day01(TestDayNumber);
    }

    private static void SetRotations(Day01 day, params (char Direction, int Distance)[] rotations)
    {
        var field = typeof(Day01).GetField("_rotations", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?? throw new InvalidOperationException("Private field '_rotations' not found.");
        var list = (IList?)field.GetValue(day) ??
                   throw new InvalidOperationException("Private field '_rotations' is null.");
        list.Clear();
        foreach (var r in rotations)
            list.Add(ValueTuple.Create(r.Direction, r.Distance));
    }

    [Test]
    public void Day01_Part1_EndToEnd()
    {
        Assert.That(_day.CalculatePart1(), Is.EqualTo(3));
    }

    [Test]
    public void Day01_Part2_EndToEnd()
    {
        Assert.That(_day.CalculatePart2(), Is.EqualTo(6));
    }

    [Test]
    public void Day01_Part2_Forward_LandsExactlyOnZero()
    {
        var day = new Day01(TestDayNumber);
        SetRotations(day, ('R', 50));
        Assert.That(day.CalculatePart2(), Is.EqualTo(1));
    }

    [Test]
    public void Day01_Part1_Forward_LandsExactlyOnZero()
    {
        var day = new Day01(TestDayNumber);
        SetRotations(day, ('R', 50));
        Assert.That(day.CalculatePart1(), Is.EqualTo(1));
    }

    [Test]
    public void Day01_Part2_Backward_LandsExactlyOnZero()
    {
        var day = new Day01(TestDayNumber);
        SetRotations(day, ('L', 50));
        Assert.That(day.CalculatePart2(), Is.EqualTo(1));
    }

    [Test]
    public void Day01_Part2_Forward_MultipleWraps()
    {
        var day = new Day01(TestDayNumber);
        SetRotations(day, ('R', 250)); // start 50 -> 300 (passes 100,200,300)
        Assert.That(day.CalculatePart2(), Is.EqualTo(3));
    }

    [Test]
    public void Day01_Part2_Backward_MultipleWraps()
    {
        var day = new Day01(TestDayNumber);
        SetRotations(day, ('L', 350)); // start 50 -> -300 (passes 0,-100,-200,-300)
        Assert.That(day.CalculatePart2(), Is.EqualTo(4));
    }

    [Test]
    public void Day01_Part2_NoMovement()
    {
        var day = new Day01(TestDayNumber);
        SetRotations(day, ('R', 0));
        Assert.That(day.CalculatePart2(), Is.EqualTo(0));
    }

    [Test]
    public void Day01_Part2_CrossWithoutLandingOnZero()
    {
        var day = new Day01(TestDayNumber);
        SetRotations(day, ('R', 60)); // start 50 -> 110 (passes 100 once), final 10
        Assert.That(day.CalculatePart2(), Is.EqualTo(1));
    }

    [Test]
    public void Day01_Part2_MultipleSequentialRotations_CountsAllCrossings()
    {
        var day = new Day01(TestDayNumber);
        // Start 50:
        // R60 -> 110 (passes 100 once) -> dial 10
        // L70 -> -60 (passes 0 once) -> dial 40
        SetRotations(day, ('R', 60), ('L', 70));
        Assert.That(day.CalculatePart2(), Is.EqualTo(2));
    }
}