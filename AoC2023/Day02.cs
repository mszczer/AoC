namespace AoC.AoC2023;

internal class Day02 : AoC<List<string>, int, int>
{
    private readonly Dictionary<int, Draw> _gameRecords;
    private const int RedCubes = 12;
    private const int GreenCubes = 13;
    private const int BlueCubes = 14;

    public Day02(string dayName) : base(dayName)
    {
        _gameRecords = ParseGameRecords();
    }

    private Dictionary<int, Draw> ParseGameRecords()
    {
        var gameRecords = new Dictionary<int, Draw>();
        var separators = new[] { " ", ":", ",", ";" };

        foreach (var game in InputData ?? Enumerable.Empty<string>())
        {
            var tokens = game.GetWords(separators);
            if (tokens.Length < 2) continue;

            if (!int.TryParse(tokens[1], out var id)) continue;

            var maxRed = 0;
            var maxGreen = 0;
            var maxBlue = 0;

            for (var i = 2; i + 1 < tokens.Length; i += 2)
            {
                if (!int.TryParse(tokens[i], out var quantity)) continue;
                var color = tokens[i + 1];

                switch (color)
                {
                    case "blue":
                        maxBlue = Math.Max(quantity, maxBlue);
                        break;
                    case "red":
                        maxRed = Math.Max(quantity, maxRed);
                        break;
                    case "green":
                        maxGreen = Math.Max(quantity, maxGreen);
                        break;
                }
            }

            gameRecords[id] = new Draw(maxBlue, maxRed, maxGreen);
        }

        return gameRecords;
    }

    public override int CalculatePart1()
    {
        return _gameRecords
            .Where(kv => kv.Value is { Red: <= RedCubes, Green: <= GreenCubes, Blue: <= BlueCubes })
            .Sum(kv => kv.Key);
    }

    public override int CalculatePart2()
    {
        return _gameRecords.Sum(kv => kv.Value.Blue * kv.Value.Green * kv.Value.Red);
    }

    private readonly record struct Draw(int Blue, int Red, int Green);
}