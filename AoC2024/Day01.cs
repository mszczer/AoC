using AoC.Common;

namespace AoC.AoC2024;

public class Day01 : AoC<List<string>, int, int>
{
    private readonly List<int> _leftLocations = new();
    private readonly List<int> _rightLocations = new();

    public Day01(string dayName) : base(dayName)
    {
        ParseLocations();
    }

    private void ParseLocations()
    {
        foreach (var locationPair in InputData)
        {
            var locations = locationPair.Split();
            _leftLocations.Add(int.Parse(locations[0]));
            _rightLocations.Add(int.Parse(locations[3]));
        }
    }

    public override int CalculatePart1()
    {
        var result = 0;

        _leftLocations.Sort();
        _rightLocations.Sort();

        for (var i = 0; i < _leftLocations.Count; i++)
            result += int.Abs(_leftLocations[i] - _rightLocations[i]);

        return result;
    }

    public override int CalculatePart2()
    {
        return -100;
    }
}