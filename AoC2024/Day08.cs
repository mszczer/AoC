using System.Drawing;

namespace AoC.AoC2024;

public class Day08 : AoC<List<string>, int, int>
{
    private char[,] _antennaMap;
    private readonly List<Point> _antinodes = [];


    public Day08(string dayName) : base(dayName)
    {
        if (InputData is { Count: > 0 })
            ParseInputData();
    }

    private void ParseInputData()
    {
        if (InputData == null || InputData.Count == 0 || InputData[0].Length == 0)
            throw new InvalidOperationException("InputData is empty or not initialized.");

        var rows = InputData.Count;
        var columns = InputData[0].Length;

        _antennaMap = new char[rows, columns];

        for (var row = 0; row < rows; row++)
        for (var column = 0; column < columns; column++)
            _antennaMap[row, column] = InputData[row][column];
    }

    public override int CalculatePart1()
    {
        _antinodes.Clear();

        var rows = _antennaMap.GetLength(0);
        var columns = _antennaMap.GetLength(1);

        for (var r = 0; r < rows; r++)
            for (var c = 0; c < columns; c++)
            {
                if (_antennaMap[r, c] == '.')
                    continue;

                var antenna = _antennaMap[r, c];

                for (var searchedRow = r; searchedRow < rows; searchedRow++)
                {
                    var startColumn = searchedRow == r ? c + 1 : 0;
                    for (var searchedColumn = startColumn; searchedColumn < columns; searchedColumn++)
                        if (_antennaMap[searchedRow, searchedColumn] == antenna)
                            AddAntinodes(new Point(r, c), new Point(searchedRow, searchedColumn), _antinodes);
                }
            }

        return _antinodes.Count;
    }

    private void AddAntinodes(Point firstLocation, Point secondLocation, List<Point> antinodes)
    {
        var dx = secondLocation.X - firstLocation.X;
        var dy = secondLocation.Y - firstLocation.Y;

        if (dx == 0 && dy == 0)
            return;

        var firstAntinode = new Point(secondLocation.X + dx, secondLocation.Y + dy);
        var secondAntinode = new Point(firstLocation.X - dx, firstLocation.Y - dy);

        if (IsPointValid(firstAntinode) && !antinodes.Contains(firstAntinode))
            antinodes.Add(firstAntinode);

        if (IsPointValid(secondAntinode) && !antinodes.Contains(secondAntinode))
            antinodes.Add(secondAntinode);
    }

    private bool IsPointValid(Point antinode)
    {
        var rows = _antennaMap.GetLength(0);
        var columns = _antennaMap.GetLength(1);

        return antinode.X >= 0 && antinode.X < rows && antinode.Y >= 0 && antinode.Y < columns;
    }

    public override int CalculatePart2()
    {
        _antinodes.Clear();

        var rows = _antennaMap.GetLength(0);
        var columns = _antennaMap.GetLength(1);

        for (var r = 0; r < rows; r++)
            for (var c = 0; c < columns; c++)
            {
                if (_antennaMap[r, c] == '.')
                    continue;

                var antenna = _antennaMap[r, c];

                for (var searchedRow = r; searchedRow < rows; searchedRow++)
                {
                    var startColumn = searchedRow == r ? c + 1 : 0;
                    for (var searchedColumn = startColumn; searchedColumn < columns; searchedColumn++)
                        if (_antennaMap[searchedRow, searchedColumn] == antenna)
                            AddHarmonicAntinodes(new Point(r, c), new Point(searchedRow, searchedColumn), _antinodes);
                }
            }

        return _antinodes.Count;
    }

    private void AddHarmonicAntinodes(Point firstLocation, Point secondLocation, List<Point> antinodes)
    {
        var dx = secondLocation.X - firstLocation.X;
        var dy = secondLocation.Y - firstLocation.Y;

        if (dx == 0 && dy == 0)
            return;

        var firstHarmonicLocation = firstLocation;
        var secondHarmonicLocation = secondLocation;

        if (IsPointValid(firstLocation) && !antinodes.Contains(firstLocation))
            antinodes.Add(firstLocation);

        if (IsPointValid(secondLocation) && !antinodes.Contains(secondLocation))
            antinodes.Add(secondLocation);

        while (IsPointValid(secondHarmonicLocation))
        {
            var firstAntinode = new Point(secondHarmonicLocation.X + dx, secondHarmonicLocation.Y + dy);
            if (IsPointValid(firstAntinode) && !antinodes.Contains(firstAntinode))
                antinodes.Add(firstAntinode);
            secondHarmonicLocation = firstAntinode;
        }

        firstHarmonicLocation = firstLocation;
        secondHarmonicLocation = secondLocation;

        while (IsPointValid(firstHarmonicLocation))
        {
            var secondAntinode = new Point(firstHarmonicLocation.X - dx, firstHarmonicLocation.Y - dy);
            if (IsPointValid(secondAntinode) && !antinodes.Contains(secondAntinode))
                antinodes.Add(secondAntinode);
            firstHarmonicLocation = secondAntinode;
        }
    }
}