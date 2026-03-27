using AoC.Common;

namespace AoC.AoC2025;

public class Day05 : AoC<List<string>, long, long>
{
    private readonly List<(long Start, long End)> _idRanges = [];
    private readonly List<long> _ingredientIDs = [];

    public Day05(string dayName) : base(dayName)
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
                if (long.TryParse(row, out var rangeId)) 
                    _ingredientIDs.Add(rangeId);
            }
        }
    }

    public override long CalculatePart1()
    {
        return _ingredientIDs.Count(IsFreshIngredient);
    }

    private bool IsFreshIngredient(long id)
    {
        return _idRanges.Any(range => id >= range.Start && id <= range.End);
    }

    public override long CalculatePart2()
    {
        throw new NotImplementedException();
    }
}