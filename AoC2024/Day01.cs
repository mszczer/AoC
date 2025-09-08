namespace AoC.AoC2024;

public class Day01 : AoC<List<string>, int, int>
{
    private readonly List<int> _leftLocations = [];
    private readonly List<int> _rightLocations = [];

    public Day01(string dayName) : base(dayName)
    {
        ParseLocations();
    }

    private void ParseLocations()
    {
        foreach (var locationPair in InputData)
        {
            var locations = locationPair.Split();

            if (locations.Length < 4)
                throw new FormatException("Each input line must contain at least 4 elements.");

            if (!int.TryParse(locations[0], out var left) || !int.TryParse(locations[3], out var right))
                throw new FormatException($"Invalid integer values in line: '{locationPair}'");

            _leftLocations.Add(int.Parse(locations[0]));
            _rightLocations.Add(int.Parse(locations[3]));
        }
    }

    public override int CalculatePart1()
    {
        var sortedLeft = _leftLocations.OrderBy(x => x).ToList();
        var sortedRight = _rightLocations.OrderBy(x => x).ToList();

        var totalDistance = 0;
        for (var i = 0; i < sortedLeft.Count; i++)
            totalDistance += Math.Abs(sortedLeft[i] - sortedRight[i]);

        return totalDistance;
    }

    public override int CalculatePart2()
    {
        var locationFrequency = new Dictionary<int, int>();
        foreach (var location in _leftLocations)
            locationFrequency[location] = locationFrequency.GetValueOrDefault(location) + 1;

        var similarityScore = 0;
        foreach (var location in _rightLocations)
            if (locationFrequency.TryGetValue(location, out var frequency))
                similarityScore += location * frequency;

        return similarityScore;
    }
}