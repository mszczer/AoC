using AoC.Common;

namespace AoC.AoC2025;

public class Day01 : AoC<List<string>, int, int>
{
    private readonly List<(char, int)> _rotations = [];

    public Day01(string dayName) : base(dayName)
    {
        ParseInputData();
    }

    private void ParseInputData()
    {
        if (InputData == null || InputData.Count == 0 || InputData[0].Length == 0)
            throw new InvalidOperationException("InputData is empty or not initialized.");

        _rotations.Clear();

        foreach (var rotation in InputData)
        {
            var direction = rotation[0];
            var distance = int.Parse(rotation[1..]);

            _rotations.Add((direction, distance));
        }
    }

    public override int CalculatePart1()
    {
        var decoy = 0;
        var dialPoint = 50;

        foreach (var rotation in _rotations)
        {
            dialPoint = rotation.Item1 == 'L' ? dialPoint - rotation.Item2 : dialPoint + rotation.Item2;
            dialPoint = (dialPoint % 100 + 100) % 100; // normalize to 0..99 even for negative dialPoint

            if (dialPoint == 0) decoy++;
        }

        return decoy;
    }

    public override int CalculatePart2()
    {
        var decoy = 0;
        var dialPoint = 50;

        foreach (var rotation in _rotations)
        {
            var start = dialPoint; // normalized 0..99
            var delta = rotation.Item1 == 'L' ? -rotation.Item2 : rotation.Item2;
            var endRaw = start + delta;

            var countZeros = 0;
            if (delta > 0)
            {
                countZeros = endRaw / 100 - start / 100;
            }
            else if (delta < 0)
            {
                var startFloor = start == 0 ? -1 : 0;

                var endMinusZero = endRaw - 1;
                int endFloor;
                if (endMinusZero >= 0)
                    endFloor = endMinusZero / 100;
                else
                    endFloor = -((-endMinusZero + 99) / 100);

                countZeros = startFloor - endFloor;
            }

            decoy += countZeros;

            dialPoint = (endRaw % 100 + 100) % 100; // normalize to 0..99 even for negative dialPoint
        }

        return decoy;
    }
}