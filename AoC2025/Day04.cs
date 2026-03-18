using AoC.Common;

namespace AoC.AoC2025;

public class Day04 : AoC<List<string>, int, int>
{
    private const int MaxNeighborsForAccessibility = 4;
    private const char OccupiedCell = '@';
    private const char EmptyCell = '.';

    private readonly char[,] _paperGrid;
    private readonly int _rows;
    private readonly int _columns;

    public Day04(string dayName) : base(dayName)
    {
        var data = InputData ?? throw new InvalidOperationException("InputData is empty or not initialized.");
        (_paperGrid, _rows, _columns) = ParseInputData(data);
    }

    public Day04(string dayName, List<string> inputData) : base(dayName, inputData)
    {
        var data = InputData ?? throw new InvalidOperationException("InputData is empty or not initialized.");
        (_paperGrid, _rows, _columns) = ParseInputData(data);
    }

    private static (char[,] grid, int rows, int columns) ParseInputData(List<string> data)
    {
        if (data.Count == 0 || string.IsNullOrEmpty(data[0]))
            throw new InvalidOperationException("InputData is empty or not initialized.");

        var rows = data.Count;
        var columns = data[0].Length;

        if (data.Any(r => r.Length != columns))
            throw new FormatException("All input rows must have the same length.");

        var grid = new char[rows, columns];

        for (var row = 0; row < rows; row++)
            for (var column = 0; column < columns; column++)
                grid[row, column] = data[row][column];

        return (grid, rows, columns);
    }

    public override int CalculatePart1()
    {
        var accessibleCells = FindAccessibleCells(_paperGrid);
        return accessibleCells.Count;
    }

    public override int CalculatePart2()
    {
        var workingGrid = (char[,])_paperGrid.Clone();
        var totalAccessible = 0;

        while (true)
        {
            var accessibleCells = FindAccessibleCells(workingGrid);

            if (accessibleCells.Count == 0)
                break;

            RemoveCellsFromGrid(workingGrid, accessibleCells);
            totalAccessible += accessibleCells.Count;
        }

        return totalAccessible;
    }

    private List<(int Row, int Column)> FindAccessibleCells(char[,] grid)
    {
        var accessibleCells = new List<(int Row, int Column)>();

        for (var row = 0; row < _rows; row++)
            for (var column = 0; column < _columns; column++)
                if (grid[row, column] == OccupiedCell &&
                    CountNeighbors(grid, row, column) < MaxNeighborsForAccessibility)
                    accessibleCells.Add((row, column));

        return accessibleCells;
    }

    private int CountNeighbors(char[,] grid, int row, int column)
    {
        var neighbors = 0;
        var startRow = Math.Max(0, row - 1);
        var endRow = Math.Min(_rows - 1, row + 1);
        var startCol = Math.Max(0, column - 1);
        var endCol = Math.Min(_columns - 1, column + 1);

        for (var r = startRow; r <= endRow; r++)
            for (var c = startCol; c <= endCol; c++)
            {
                if (r == row && c == column)
                    continue;
                if (grid[r, c] == OccupiedCell)
                    neighbors++;
            }

        return neighbors;
    }

    private static void RemoveCellsFromGrid(char[,] grid, List<(int Row, int Column)> cells)
    {
        foreach (var (row, column) in cells)
            grid[row, column] = EmptyCell;
    }
}