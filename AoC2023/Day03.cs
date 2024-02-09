using System.Drawing;
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

        var number = "";
        var coordinates = new List<Point>();

        for (var row = 0; row < InputData.Count; row++)
            for (var column = 0; column < InputData[0].Length; column++)
            {
                var currentChar = InputData[row][column];
                var currentPosition = new Point(column, row);

                if (!char.IsDigit(currentChar))
                {
                    if (!string.IsNullOrEmpty(number)) // Save number and its coordinates
                    {
                        var partnumber = int.Parse(number);
                        _partNumbers.Add(new Tuple<int, List<Point>>(partnumber, new List<Point>(coordinates)));

                        number = "";
                        coordinates.Clear();
                    }

                    if (currentChar != '.') // Add symbol and its coordinates to the list
                        _symbols.Add(new Tuple<char, Point>(currentChar, currentPosition));
                }
                else // Convert digit characters to number string
                {
                    number += currentChar;
                    coordinates.Add(currentPosition);
                }
            }
    }

    public override int CalculatePart1()
    {
        return _partNumbers.Where(part
                => IsValidPartNumber(part, _symbols))
            .Sum(part => part.Item1);
    }

    private static bool IsValidPartNumber(Tuple<int, List<Point>> part, List<Tuple<char, Point>> symbols)
    {
        const bool status = false;

        foreach (var partPosition in part.Item2)
            foreach (var symbol in symbols)
                if (IsAdjacent(partPosition, symbol.Item2))
                    return true;

        return status;
    }

    private static bool IsAdjacent(Point firstPoint, Point secondPoint)
    {
        return Math.Abs(firstPoint.X - secondPoint.X) <= 1 && Math.Abs(firstPoint.Y - secondPoint.Y) <= 1;
    }

    public override int CalculatePart2()
    {
        return -1;
    }
}