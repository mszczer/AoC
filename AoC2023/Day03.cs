using System.Drawing;
using System.Text;

namespace AoC.AoC2023;

internal class Day03 : AoC<List<string>, int, int>
{
    private readonly List<PartNumber> _partNumbers = [];
    private readonly List<Symbol> _symbols = [];

    public Day03(string dayName) : base(dayName)
    {
        InitializeEngineParts();
    }

    private record PartNumber(int Value, List<Point> Positions);

    private record Symbol(char Character, Point Position);

    private void InitializeEngineParts()
    {
        if (InputData == null || InputData.Count == 0) return;

        var numberBuilder = new StringBuilder();
        var coordinates = new List<Point>();

        for (var row = 0; row < InputData.Count; row++)
        {
            var line = InputData[row] ?? string.Empty;
            for (var column = 0; column < line.Length; column++)
            {
                var currentChar = line[column];
                var currentPoint = new Point(column, row);

                if (!char.IsDigit(currentChar))
                {
                    if (numberBuilder.Length > 0) SavePartNumberAndCoordinates(numberBuilder, coordinates);

                    if (currentChar != '.') _symbols.Add(new Symbol(currentChar, currentPoint));
                }
                else
                {
                    numberBuilder.Append(currentChar);
                    coordinates.Add(currentPoint);
                }
            }

            // At end of each line, it's possible a number spans to EOL — numbers are bound by non-digit or EOL.
            if (numberBuilder.Length > 0) SavePartNumberAndCoordinates(numberBuilder, coordinates);
        }
    }

    private void SavePartNumberAndCoordinates(StringBuilder numberBuilder, List<Point> coordinates)
    {
        if (numberBuilder.Length == 0) return;

        var partNumber = int.Parse(numberBuilder.ToString());
        _partNumbers.Add(new PartNumber(partNumber, [..coordinates]));

        numberBuilder.Clear();
        coordinates.Clear();
    }

    public override int CalculatePart1()
    {
        return _partNumbers
            .Where(IsValidPartNumber)
            .Sum(p => p.Value);
    }

    private bool IsValidPartNumber(PartNumber part)
    {
        return _symbols.Any(symbol => IsAdjacent(symbol, part));
    }

    private static bool IsAdjacent(Point a, Point b)
    {
        return Math.Abs(a.X - b.X) <= 1 && Math.Abs(a.Y - b.Y) <= 1;
    }

    public override int CalculatePart2()
    {
        var result = 0;

        foreach (var symbol in _symbols)
        {
            if (symbol.Character != '*') continue;

            var adjacentParts = new List<int>();

            foreach (var part in _partNumbers)
            {
                if (adjacentParts.Count >= 3) break; // defensive cap
                if (IsAdjacent(symbol, part)) adjacentParts.Add(part.Value);
            }

            if (adjacentParts.Count == 2) result += adjacentParts[0] * adjacentParts[1];
        }

        return result;
    }

    private static bool IsAdjacent(Symbol symbol, PartNumber part)
    {
        return part.Positions.Any(p => IsAdjacent(symbol.Position, p));
    }
}