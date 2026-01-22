using AoC.Common;

namespace AoC.AoC2025;

public class Day01 : AoC<List<string>, int, int>
{
    private readonly List<(char Direction, int Distance)> _rotations = [];

    public Day01(string dayName) : base(dayName)
    {
        ParseInputData();
    }

    private void ParseInputData()
    {
        if (InputData == null || InputData.Count == 0)
            throw new InvalidOperationException("InputData is empty or not initialized.");

        _rotations.Clear();

        foreach (var rotation in InputData)
        {
            if (string.IsNullOrEmpty(rotation) || rotation.Length < 2)
                throw new FormatException($"Invalid rotation entry: \"{rotation}\"");

            var direction = rotation[0];
            var distanceText = rotation[1..];

            if (!int.TryParse(distanceText, out var distance))
                throw new FormatException($"Invalid distance in rotation entry: \"{rotation}\"");

            if (direction != 'L' && direction != 'R')
                throw new FormatException($"Invalid direction in rotation entry: \"{rotation}\"");

            _rotations.Add((direction, distance));
        }
    }

    public override int CalculatePart1()
    {
        var zeroCount = 0;
        var dialPoint = 50;

        foreach (var rotation in _rotations)
        {
            var delta = rotation.Direction == 'L' ? -rotation.Distance : rotation.Distance;
            dialPoint = ((dialPoint + delta) % 100 + 100) % 100; // normalize to 0..99 even for negatives

            if (dialPoint == 0)
                zeroCount++;
        }

        return zeroCount;
    }

    public override int CalculatePart2()
    {
        var zeroCount = 0;
        var dialPoint = 50;

        foreach (var rotation in _rotations)
        {
            var start = dialPoint; // normalized 0..99
            var delta = rotation.Direction == 'L' ? -rotation.Distance : rotation.Distance;
            var endRaw = start + delta;

            var crossings = 0;
            if (delta > 0)
                // Count multiples of 100 strictly greater than start and <= endRaw
                crossings = (int)Math.Floor(endRaw / 100.0) - (int)Math.Floor(start / 100.0);
            else if (delta < 0)
                // Count multiples of 100 >= endRaw and < start.
                // Using (x - 1) trick turns the half-open interval into closed intervals for floor arithmetic.
                crossings = (int)Math.Floor((start - 1) / 100.0) - (int)Math.Floor((endRaw - 1) / 100.0);

            zeroCount += Math.Abs(crossings);

            dialPoint = (endRaw % 100 + 100) % 100; // normalize back to 0..99
        }

        return zeroCount;
    }
}