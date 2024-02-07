using AoC.Common;

namespace AoC.AoC2023;

internal class Day02 : AoC<List<string>, int, int>
{
    private readonly Dictionary<int, Draw> _gameRecords;
    private const int RedCubes = 12;
    private const int GreenCubes = 13;
    private const int BlueCubes = 14;


    public Day02(string dayName) : base(dayName)
    {
        _gameRecords = GetGameRecords();
    }

    private Dictionary<int, Draw> GetGameRecords()
    {
        var gameRecords = new Dictionary<int, Draw>();
        var separator = new[] { " ", ":", ",", ";" };
        
        foreach (var game in InputData)
        {
            var maxRed = 0;
            var maxGreen = 0;
            var maxBlue = 0;

            var gameData = game.GetWords(separator);

            for (var i = 2; i < gameData.Length; i += 2)
            {
                var quantity = int.Parse(gameData[i]);

                switch (gameData[i + 1])
                {
                    case "blue":
                        maxBlue = quantity > maxBlue ? quantity : maxBlue;
                        break;
                    case "red":
                        maxRed = quantity > maxRed ? quantity : maxRed;
                        break;
                    case "green":
                        maxGreen = quantity > maxGreen ? quantity : maxGreen;
                        break;
                }
            }

            var maxDraw = new Draw
            {
                Blue = maxBlue,
                Red = maxRed,
                Green = maxGreen
            };

            gameRecords.Add(int.Parse(gameData[1]), maxDraw);
        }

        return gameRecords;
    }

    public override int CalculatePart1()
    {
        return _gameRecords.Where(game => 
                game.Value is { Red: <= RedCubes, Green: <= GreenCubes, Blue: <= BlueCubes })
                .Sum(game => game.Key);
    }

    public override int CalculatePart2()
    {
        return _gameRecords.Sum(game => 
            (game.Value.Blue * game.Value.Green * game.Value.Red));
    }

    private struct Draw
    {
        public int Blue;
        public int Red;
        public int Green;
    }
}