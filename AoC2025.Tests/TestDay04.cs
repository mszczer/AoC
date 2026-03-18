namespace AoC.AoC2025.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay04
{
    private const string TestDayNumber = "TestDay04";

    [Test]
    public void Day04_Part1_EndToEnd()
    {
        var day = new Day04(TestDayNumber);
        Assert.That(day.CalculatePart1(), Is.EqualTo(13));
    }

    private static IEnumerable<TestCaseData> Part1TestCases()
    {
        yield return new TestCaseData(new List<string> { "@" }, 1)
            .SetName("SingleCellAccessible");

        yield return new TestCaseData(new List<string> { "." }, 0)
            .SetName("SingleDotNotAccessible");

        yield return new TestCaseData(new List<string> { "@@" }, 2)
            .SetName("TwoCellsInRow");

        yield return new TestCaseData(new List<string> { "@@@" }, 3)
            .SetName("ThreeCellsInRow");

        yield return new TestCaseData(new List<string> { "@@@@" }, 4)
            .SetName("FourCellsInRow");

        yield return new TestCaseData(new List<string> { "...", ".@.", "..." }, 1)
            .SetName("IsolatedCell");

        yield return new TestCaseData(new List<string> { ".@.", "@@@", ".@." }, 4)
            .SetName("CellWith4Neighbors_NotAccessible");

        yield return new TestCaseData(new List<string> { ".@.", "@@.", ".@." }, 4)
            .SetName("CellWith3Neighbors_Accessible");

        yield return new TestCaseData(new List<string> { "@@@", "@@@", "@@@" }, 4)
            .SetName("CellWith8Neighbors_NotAccessible");

        yield return new TestCaseData(new List<string> { "@.@", "...", "@.@" }, 4)
            .SetName("CornerCells");

        yield return new TestCaseData(new List<string> { "...", "...", "..." }, 0)
            .SetName("AllDots");

        yield return new TestCaseData(new List<string> { "@.@", ".@.", "@.@" }, 4)
            .SetName("DiagonalNeighbors");

        yield return new TestCaseData(new List<string> { ".....", ".@@@.", ".@@@.", ".@@@.", "....." }, 4)
            .SetName("EdgeCells");
    }

    [Test]
    [TestCaseSource(nameof(Part1TestCases))]
    public void Day04_Part1_ParameterizedTests(List<string> input, int expected)
    {
        var day = new Day04(TestDayNumber, input);
        Assert.That(day.CalculatePart1(), Is.EqualTo(expected));
    }

    [Test]
    public void Day04_Part2_EndToEnd()
    {
        var day = new Day04(TestDayNumber);
        Assert.That(day.CalculatePart2(), Is.EqualTo(43));
    }

    [Test]
    [TestCaseSource(nameof(Part2TestCases))]
    public void Day04_Part2_ParameterizedTests(List<string> input, int expected)
    {
        var day = new Day04(TestDayNumber, input);
        Assert.That(day.CalculatePart2(), Is.EqualTo(expected));
    }

    private static IEnumerable<TestCaseData> Part2TestCases()
    {
        // Single iteration cases
        yield return new TestCaseData(new List<string> { "@" }, 1)
            .SetName("SingleCell");

        yield return new TestCaseData(new List<string> { "@@@@" }, 4)
            .SetName("SingleRow");

        yield return new TestCaseData(new List<string> { "@.@", "...", "@.@" }, 4)
            .SetName("IsolatedCells_Part1EqualsPart2");

        // Multi-iteration cases
        yield return new TestCaseData(new List<string> { "@@", "@@" }, 4)
            .SetName("TwoByTwo_AllAccessible");

        yield return new TestCaseData(new List<string> { "@@@", "@@@", "@@@" }, 9)
            .SetName("ThreeByThree_LayerByLayer");

        yield return new TestCaseData(new List<string> { ".@.", "@@@", ".@." }, 5)
            .SetName("CrossPattern_CenterExposed");

        yield return new TestCaseData(new List<string> { "...", "...", "..." }, 0)
            .SetName("EmptyGrid");
    }

    [Test]
    public void Day04_Part2_DoesNotMutatePart1Results()
    {
        var input = new List<string> { "@@@", "@@@", "@@@" };
        var day = new Day04(TestDayNumber, input);

        var part1Before = day.CalculatePart1();
        day.CalculatePart2();
        var part1After = day.CalculatePart1();

        Assert.That(part1After, Is.EqualTo(part1Before));
    }
}