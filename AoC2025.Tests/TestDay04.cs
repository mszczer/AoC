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

    //[Test]
    //public void Day04_Part2_EndToEnd()
    //{
    //    var day = new Day04(TestDayNumber);
    //    Assert.That(day.CalculatePart2(), Is.EqualTo(int.MaxValue));
    //}
}