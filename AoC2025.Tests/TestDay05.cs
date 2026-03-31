namespace AoC.AoC2025.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestDay05
{
    private const string TestDayNumber = "TestDay05";

    [Test]
    public void Day05_Part1_EndToEnd()
    {
        var day = new Day05(TestDayNumber);
        Assert.That(day.CalculatePart1(), Is.EqualTo(3));
    }

    [Test]
    public void Day05_Part2_EndToEnd()
    {
        var day = new Day05(TestDayNumber);
        Assert.That(day.CalculatePart2(), Is.EqualTo(14));
    }

    [Test]
    public void Day05_Part1_AdjacentRanges_MergesCorrectly()
    {
        var inputData = new List<string> { "1-5", "6-10", "11-15", "", "7" };
        var day = new Day05("TestDay05_AdjacentRanges", inputData);
        Assert.That(day.CalculatePart1(), Is.EqualTo(1),
            "Adjacent ranges 1-5, 6-10, 11-15 should merge into 1-15, and ID 7 should be fresh");
    }

    [Test]
    public void Day05_Part2_AdjacentRanges_TotalCoverage()
    {
        var inputData = new List<string> { "1-5", "6-10", "11-15", "", "7" };
        var day = new Day05("TestDay05_AdjacentRanges", inputData);
        Assert.That(day.CalculatePart2(), Is.EqualTo(15),
            "Adjacent ranges should merge into 1-15 with total coverage of 15");
    }

    [Test]
    public void Day05_Part1_OverlappingRanges_MergesCorrectly()
    {
        var inputData = new List<string> { "1-10", "3-7", "", "5" };
        var day = new Day05("TestDay05_OverlappingRanges", inputData);
        Assert.That(day.CalculatePart1(), Is.EqualTo(1),
            "Overlapping ranges 1-10 and 3-7 should merge, ID 5 should be fresh");
    }

    [Test]
    public void Day05_Part2_OverlappingRanges_TotalCoverage()
    {
        var inputData = new List<string> { "1-10", "3-7", "", "5" };
        var day = new Day05("TestDay05_OverlappingRanges", inputData);
        Assert.That(day.CalculatePart2(), Is.EqualTo(10),
            "Overlapping ranges 1-10 and 3-7 should merge into 1-10 with coverage of 10");
    }

    [Test]
    public void Day05_Part1_NoFreshIngredients_ReturnsZero()
    {
        var inputData = new List<string> { "5-10", "15-20", "", "1", "12", "25" };
        var day = new Day05("TestDay05_NoFreshIngredients", inputData);
        Assert.That(day.CalculatePart1(), Is.Zero,
            "IDs 1, 12, 25 are all outside ranges 5-10 and 15-20");
    }

    [Test]
    public void Day05_Part2_NoFreshIngredients_StillCalculatesTotalCoverage()
    {
        var inputData = new List<string> { "5-10", "15-20", "", "1", "12", "25" };
        var day = new Day05("TestDay05_NoFreshIngredients", inputData);
        Assert.That(day.CalculatePart2(), Is.EqualTo(12),
            "Ranges 5-10 (6) and 15-20 (6) have total coverage of 12");
    }

    [Test]
    public void Day05_Part1_AllFreshIngredients_ReturnsAll()
    {
        var inputData = new List<string> { "1-100", "", "1", "50", "100" };
        var day = new Day05("TestDay05_AllFreshIngredients", inputData);
        Assert.That(day.CalculatePart1(), Is.EqualTo(3),
            "All IDs 1, 50, 100 are within range 1-100");
    }

    [Test]
    public void Day05_Part2_AllFreshIngredients_LargeRangeCoverage()
    {
        var inputData = new List<string> { "1-100", "", "1", "50", "100" };
        var day = new Day05("TestDay05_AllFreshIngredients", inputData);
        Assert.That(day.CalculatePart2(), Is.EqualTo(100),
            "Single range 1-100 has coverage of 100");
    }

    [Test]
    public void Day05_Part1_SingleValueRange_ExactMatch()
    {
        var inputData = new List<string> { "100-100", "", "100" };
        var day = new Day05("TestDay05_SingleValueRange", inputData);
        Assert.That(day.CalculatePart1(), Is.EqualTo(1),
            "ID 100 exactly matches range 100-100");
    }

    [Test]
    public void Day05_Part2_SingleValueRange_CoverageOfOne()
    {
        var inputData = new List<string> { "100-100", "", "100" };
        var day = new Day05("TestDay05_SingleValueRange", inputData);
        Assert.That(day.CalculatePart2(), Is.EqualTo(1),
            "Range 100-100 has coverage of 1");
    }

    [Test]
    public void Day05_Part1_MergeMultipleOverlapping_ComplexMerge()
    {
        var inputData = new List<string> { "10-20", "5-15", "1-8", "", "5", "10", "15", "20" };
        var day = new Day05("TestDay05_MergeMultiple", inputData);
        Assert.That(day.CalculatePart1(), Is.EqualTo(4),
            "Ranges 10-20, 5-15, 1-8 merge into 1-20; all IDs 5,10,15,20 are fresh");
    }

    [Test]
    public void Day05_Part2_MergeMultipleOverlapping_SingleMergedRange()
    {
        var inputData = new List<string> { "10-20", "5-15", "1-8", "", "5", "10", "15", "20" };
        var day = new Day05("TestDay05_MergeMultiple", inputData);
        Assert.That(day.CalculatePart2(), Is.EqualTo(20),
            "Multiple overlapping ranges merge into 1-20 with coverage of 20");
    }

    [Test]
    public void Day05_Part1_BoundaryValues_IncludesStartAndEnd()
    {
        var inputData = new List<string> { "5-10", "", "5", "6", "10" };
        var day = new Day05("TestDay05_BoundaryValues", inputData);
        Assert.That(day.CalculatePart1(), Is.EqualTo(3),
            "Range boundaries 5 and 10, plus interior value 6, should all be counted as fresh");
    }

    [Test]
    public void Day05_Part2_BoundaryValues_CorrectCoverage()
    {
        var inputData = new List<string> { "5-10", "", "5", "6", "10" };
        var day = new Day05("TestDay05_BoundaryValues", inputData);
        Assert.That(day.CalculatePart2(), Is.EqualTo(6),
            "Range 5-10 has coverage of 6 (inclusive of both boundaries)");
    }

    [Test]
    public void Day05_Part1_EmptyIngredients_ReturnsZero()
    {
        var inputData = new List<string> { "5-10", "15-20", "" };
        var day = new Day05("TestDay05_EmptyIngredients", inputData);
        Assert.That(day.CalculatePart1(), Is.Zero,
            "No ingredient IDs to check, should return 0");
    }

    [Test]
    public void Day05_Part2_EmptyIngredients_StillCalculatesCoverage()
    {
        var inputData = new List<string> { "5-10", "15-20", "" };
        var day = new Day05("TestDay05_EmptyIngredients", inputData);
        Assert.That(day.CalculatePart2(), Is.EqualTo(12),
            "Ranges 5-10 (6) and 15-20 (6) have coverage of 12 even with no IDs");
    }

    [Test]
    public void Day05_Part1_DisjointRanges_NoMerging()
    {
        var inputData = new List<string> { "1-3", "5-7", "10-12", "15-20", "", "2", "6", "11", "18" };
        var day = new Day05("TestDay05_DisjointRanges", inputData);
        Assert.That(day.CalculatePart1(), Is.EqualTo(4),
            "All IDs 2, 6, 11, 18 match disjoint ranges 1-3, 5-7, 10-12, 15-20");
    }

    [Test]
    public void Day05_Part2_DisjointRanges_SumOfSeparateRanges()
    {
        var inputData = new List<string> { "1-3", "5-7", "10-12", "15-20", "", "2", "6", "11", "18" };
        var day = new Day05("TestDay05_DisjointRanges", inputData);
        Assert.That(day.CalculatePart2(), Is.EqualTo(15),
            "Disjoint ranges: (3)+(3)+(3)+(6) = 15 total coverage");
    }

    [Test]
    public void Day05_Part1_LargeNumbers_HandlesLargeLongs()
    {
        var inputData = new List<string> { "1000000-1000010", "2000000-2000005", "", "1000005", "2000002", "3000000" };
        var day = new Day05("TestDay05_LargeNumbers", inputData);
        Assert.That(day.CalculatePart1(), Is.EqualTo(2),
            "IDs 1000005 and 2000002 are within their ranges; 3000000 is not");
    }

    [Test]
    public void Day05_Part2_LargeNumbers_CorrectCoverageCalculation()
    {
        var inputData = new List<string> { "1000000-1000010", "2000000-2000005", "", "1000005", "2000002", "3000000" };
        var day = new Day05("TestDay05_LargeNumbers", inputData);
        Assert.That(day.CalculatePart2(), Is.EqualTo(17),
            "Ranges 1000000-1000010 (11) and 2000000-2000005 (6) have coverage of 17");
    }

    [Test]
    public void Day05_Part1_ChainedAdjacent_MergesAll()
    {
        var inputData = new List<string> { "1-2", "2-3", "3-4", "4-5", "", "3" };
        var day = new Day05("TestDay05_ChainedAdjacent", inputData);
        Assert.That(day.CalculatePart1(), Is.EqualTo(1),
            "Chained adjacent ranges 1-2, 2-3, 3-4, 4-5 merge into 1-5; ID 3 is fresh");
    }

    [Test]
    public void Day05_Part2_ChainedAdjacent_SingleMergedCoverage()
    {
        var inputData = new List<string> { "1-2", "2-3", "3-4", "4-5", "", "3" };
        var day = new Day05("TestDay05_ChainedAdjacent", inputData);
        Assert.That(day.CalculatePart2(), Is.EqualTo(5),
            "Chained adjacent ranges merge into 1-5 with coverage of 5");
    }

    [Test]
    public void Day05_Part1_ReversedRange_NoMatch()
    {
        var inputData = new List<string> { "20-10", "", "15" };
        var day = new Day05("TestDay05_ReversedRange", inputData);
        Assert.That(day.CalculatePart1(), Is.Zero,
            "Reversed range 20-10 (start > end) should not match any ID");
    }

    [Test]
    public void Day05_Part2_ReversedRange_NegativeCoverage()
    {
        var inputData = new List<string> { "20-10", "", "15" };
        var day = new Day05("TestDay05_ReversedRange", inputData);
        Assert.That(day.CalculatePart2(), Is.EqualTo(-9),
            "Reversed range 20-10 has coverage of (10-20+1) = -9");
    }
}