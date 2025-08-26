using System.Drawing;
using AoC.Common;

namespace AoC.AoC2024;

public class Day06 : AoC<List<string>, int, int>
{
    private char[,] _map;
    private Point _startPosition;
    private enum Direction { Up, Down, Left, Right }
    
    private static readonly Dictionary<Direction, (int dRow, int dCol)> Movement = new()
    {
        [Direction.Up] = (-1, 0),
        [Direction.Down] = (1, 0),
        [Direction.Left] = (0, -1),
        [Direction.Right] = (0, 1)
    };

    public Day06(string dayName) : base(dayName)
    {
        SetMap();
    }
    private void SetMap()
    {
        var numberOfRows = InputData.Count;
        var numberOfColumns = InputData[0].Length;

        _map = new char[numberOfRows, numberOfColumns];

        for (var row = 0; row < numberOfRows; row++)
            for (var column = 0; column < numberOfColumns; column++)
            {
                _map[row, column] = InputData[row][column];
                if (_map[row, column] == '^') _startPosition = new Point(row, column);
        }
    }

    private void MarkPath()
    {
        var numberOfRows = _map.GetLength(0);
        var numberOfColumns = _map.GetLength(1);

        var row = _startPosition.X;
        var col = _startPosition.Y;
        var direction = Direction.Up;



        while (row >= 0 && row < numberOfRows && col >= 0 && col < numberOfColumns)
        {
            _map[row, col] = 'X';

            var (dRow, dCol) = Movement[direction];
            var nextRow = row + dRow;
            var nextCol = col + dCol;

            if (nextRow < 0 || nextRow >= numberOfRows || nextCol < 0 || nextCol >= numberOfColumns)
                break;

            if (_map[nextRow, nextCol] == '#')
            {
                direction = direction switch
                {
                    Direction.Up => Direction.Right,
                    Direction.Right => Direction.Down,
                    Direction.Down => Direction.Left,
                    Direction.Left => Direction.Up,
                    _ => direction
                };
            }
            else
            {
                row = nextRow;
                col = nextCol;
            }
        }
    }

    public override int CalculatePart1()
    {
        MarkPath();
        var distinctPositions = 0;
        var numberOfRows = _map.GetLength(0);
        var numberOfColumns = _map.GetLength(1);

        for (var i = 0; i < numberOfRows; i++)
            for (var j = 0; j < numberOfColumns; j++)
                if (_map[i, j] == 'X')
                    distinctPositions++;

        return distinctPositions;
    }

    public override int CalculatePart2()
    {
        throw new NotImplementedException();
    }

    
}