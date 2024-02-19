using System.Drawing;
using System.Text;
using AoC.Common;

namespace AoC.AoC2023;

internal class Day03 : AoC<List<string>, int, int>
{
    private List<Tuple<int, List<Point>>> _partNumbers;
    private List<Tuple<char, Point>> _symbols;

    public Day03(string dayName) : base(dayName)
    {
        InitializeEngineParts();
    }

    private void InitializeEngineParts()
    {
        _partNumbers = new List<Tuple<int, List<Point>>>();
        _symbols = new List<Tuple<char, Point>>();

        var number = new StringBuilder();
        var coordinates = new List<Point>();

        for (var row = 0; row < InputData.Count; row++)
        for (var column = 0; column < InputData[0].Length; column++)
        {
            var currentChar = InputData[row][column];
            var currentPosition = new Point(column, row);

            if (!char.IsDigit(currentChar))
            {
                if (number.Length > 0) // Save number and its coordinates
                    SavePartNumberAndCoordinates(number, coordinates);

                if (currentChar != '.') // Add symbol and its coordinates to the list
                    _symbols.Add(new Tuple<char, Point>(currentChar, currentPosition));
            }
            else // Convert digit characters to number string
            {
                number.Append(currentChar);
                coordinates.Add(currentPosition);
            }
        }
    }

    private void SavePartNumberAndCoordinates(StringBuilder number, List<Point> coordinates)
    {
        var partnumber = int.Parse(number.ToString());
        _partNumbers.Add(new Tuple<int, List<Point>>(partnumber, new List<Point>(coordinates)));

        number.Clear();
        coordinates.Clear();
    }

    public override int CalculatePart1()
    {
        return _partNumbers.Where(part
                => IsValidPartNumber(part, _symbols))
            .Sum(part => part.Item1);
    }

    private static bool IsValidPartNumber(Tuple<int, List<Point>> part, IEnumerable<Tuple<char, Point>> symbols)
    {
        return symbols.Any(symbol
            => IsAdjacent(symbol, part));
    }

    private static bool IsAdjacent(Point firstPoint, Point secondPoint)
    {
        return Math.Abs(firstPoint.X - secondPoint.X) <= 1 && Math.Abs(firstPoint.Y - secondPoint.Y) <= 1;
    }


    public override int CalculatePart2()
    {
        var result = 0;

        foreach (var symbol in _symbols)
            if (symbol.Item1 == '*')
            {
                var gearParts = new List<int>();

                foreach (var part in _partNumbers)
                    if (IsAdjacent(symbol, part))
                    {
                        gearParts.Add(part.Item1);
                        if (gearParts.Count > 2)
                            continue;
                    }

                if (gearParts.Count == 2)
                    result += gearParts[0] * gearParts[1];
            }

        return result;
    }

    private static bool IsAdjacent(Tuple<char, Point> symbol, Tuple<int, List<Point>> part)
    {
        return part.Item2.Any(partDigitPosition 
            => IsAdjacent(symbol.Item2, partDigitPosition));
    }
}