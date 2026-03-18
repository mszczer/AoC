using AoC.Common;

namespace AoC.AoC2025;

public class Day04 : AoC<List<string>, int, int>
{
    private const int MaxNeighboursForAccessibility = 4;
    private char[,] _paperGrid;

    public Day04(string dayName) : base(dayName)
    {
        ParseInputData();
    }

    public Day04(string dayName, List<string> inputData) : base(dayName, inputData)
    {
        ParseInputData();
    }

    private void ParseInputData()
    {
        var data = InputData ?? throw new InvalidOperationException("InputData is empty or not initialized.");
        if (data.Count == 0 || string.IsNullOrEmpty(data[0]))
            throw new InvalidOperationException("InputData is empty or not initialized.");

        var rows = data.Count;
        var columns = data[0].Length;

        if (data.Any(r => r.Length != columns))
            throw new FormatException("All input rows must have the same length.");

        _paperGrid = new char[rows, columns];

        for (var row = 0; row < rows; row++)
            for (var column = 0; column < columns; column++)
                _paperGrid[row, column] = data[row][column];
    }

    public override int CalculatePart1()
    {
        var rows = _paperGrid.GetLength(0);
        var columns = _paperGrid.GetLength(1);

        var accessible = 0;

        for (var row = 0; row < rows; row++)
            for (var column = 0; column < columns; column++)
                if (_paperGrid[row, column] == '@' &&
                    CountNeighbours(_paperGrid, row, column) < MaxNeighboursForAccessibility)
                    accessible++;

        return accessible;
    }

    private static int CountNeighbours(char[,] paperGrid, int row, int column)
    {
        var neighbours = 0;
        var maxRow = paperGrid.GetLength(0);
        var maxCol = paperGrid.GetLength(1);

        // Determine bounds for this cell
        var startRow = Math.Max(0, row - 1);
        var endRow = Math.Min(maxRow - 1, row + 1);
        var startCol = Math.Max(0, column - 1);
        var endCol = Math.Min(maxCol - 1, column + 1);

        for (var r = startRow; r <= endRow; r++)
            for (var c = startCol; c <= endCol; c++)
            {
                if (r == row && c == column)
                    continue;
                if (paperGrid[r, c] == '@')
                    neighbours++;
            }

        return neighbours;
    }

    public override int CalculatePart2()
    {
        throw new NotImplementedException();
    }
}