using AoC.Common;

namespace AoC.AoC2025;

public class Day05 : AoC<List<string>, long, long>
{
    private readonly List<(long Start, long End)> _idRanges = [];
    private readonly List<(long Start, long End)> _mergedRanges = [];
    private readonly List<long> _ingredientIDs = [];

    public Day05(string dayName) : base(dayName)
    {
        ParseInputData();
    }

    public Day05(string dayName, List<string> inputData) : base(dayName, inputData)
    {
        ParseInputData();
    }

    private void ParseInputData()
    {
        if (InputData == null || InputData.Count == 0)
            throw new InvalidOperationException("InputData is empty or not initialized.");

        var parsingRanges = true;
        foreach (var row in InputData)
        {
            if (string.IsNullOrWhiteSpace(row))
            {
                parsingRanges = false;
                continue;
            }

            if (parsingRanges)
            {
                var parts = row.Split('-', StringSplitOptions.TrimEntries);
                if (parts.Length == 2 &&
                    long.TryParse(parts[0], out var start) &&
                    long.TryParse(parts[1], out var end))
                    _idRanges.Add((start, end));
            }
            else
            {
                if (long.TryParse(row, out var id))
                    _ingredientIDs.Add(id);
            }
        }

        _mergedRanges.AddRange(MergeRanges());
    }

    private IEnumerable<(long Start, long End)> MergeRanges()
    {
        if (_idRanges.Count == 0)
            return [];

        var sortedRanges = _idRanges.OrderBy(r => r.Start).ToList();

        var merged = new List<(long Start, long End)> { sortedRanges[0] };

        for (var i = 1; i < sortedRanges.Count; i++)
        {
            var current = sortedRanges[i];
            var last = merged[^1];
            if (current.Start <= last.End + 1)
                merged[^1] = (last.Start, Math.Max(last.End, current.End));
            else
                merged.Add(current);
        }

        return merged;
    }

    public override long CalculatePart1()
    {
        return _ingredientIDs.Count(IsFreshIngredient);
    }

    private bool IsFreshIngredient(long id)
    {
        return _mergedRanges.Any(range => id >= range.Start && id <= range.End);
    }

    public override long CalculatePart2()
    {
        return _mergedRanges.Sum(range => range.End - range.Start + 1);
    }
}