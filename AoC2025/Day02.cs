using AoC.Common;

namespace AoC.AoC2025;

public class Day02 : AoC<List<string>, long, long>
{
    private readonly List<(long Start, long End)> _idRanges = []; // Store ranges as numeric start/end pairs

    public Day02(string dayName) : base(dayName)
    {
        ParseInputData();
    }

    internal Day02(string dayName, IEnumerable<(long Start, long End)> ranges) : base(dayName)
    {
        ArgumentNullException.ThrowIfNull(ranges);
        _idRanges.AddRange(ranges);
    }

    private void ParseInputData()
    {
        if (InputData == null || InputData.Count == 0)
            throw new InvalidOperationException("InputData is empty or not initialized.");

        var ranges = InputData[0]
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        foreach (var range in ranges)
        {
            var parts = range.Split('-', StringSplitOptions.TrimEntries);
            long.TryParse(parts[0], out var firstId);
            long.TryParse(parts[1], out var lastId);
            _idRanges.Add((firstId, lastId));
        }
    }

    public override long CalculatePart1()
    {
        long invalidIds = 0;

        foreach (var (start, end) in _idRanges)
            for (var id = start; id <= end; id++)
            {
                var s = id.ToString();

                if ((s.Length & 1) != 0)
                    continue;

                var mid = s.Length / 2;
                var firstHalf = s[..mid];
                var secondHalf = s[mid..];

                if (firstHalf == secondHalf)
                    invalidIds += id;
            }

        return invalidIds;
    }

    public override long CalculatePart2()
    {
        throw new NotImplementedException();
    }
}