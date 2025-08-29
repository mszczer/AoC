using AoC.Common;

namespace AoC.AoC2024;

public class Day06 : AoC<List<string>, int, int>
{
    private char[,] _map;
    private readonly struct Position(int row, int col)
    { 
        public int Row { get; } = row;
        public int Col { get; } = col;
    }
    private Position _startPosition;
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
            for (var col = 0; col < numberOfColumns; col++)
            {
                _map[row, col] = InputData[row][col];
                if (_map[row, col] == '^') _startPosition = new Position(row, col);
            }
    }

    private void MarkPath(char[,] map)
    {
        var numberOfRows = map.GetLength(0);
        var numberOfColumns = map.GetLength(1);

        var row = _startPosition.Row;
        var col = _startPosition.Col;
        var direction = Direction.Up;
        
        while (row >= 0 && row < numberOfRows && col >= 0 && col < numberOfColumns)
        {
            map[row, col] = 'X';

            var (dRow, dCol) = Movement[direction];
            var nextRow = row + dRow;
            var nextCol = col + dCol;

            if (nextRow < 0 || nextRow >= numberOfRows || nextCol < 0 || nextCol >= numberOfColumns)
                break;

            if (map[nextRow, nextCol] == '#')
            {
                direction = RotateClockwise(direction);
            }
            else
            {
                row = nextRow;
                col = nextCol;
            }
        }
    }

    private static char[,] CloneMap(char[,] map)
    {
        var rows = map.GetLength(0);
        var cols = map.GetLength(1);
        var newMap = new char[rows, cols];
        for (var i = 0; i < rows; i++)
            for (var j = 0; j < cols; j++)
                newMap[i, j] = map[i, j];
        return newMap;
    }

    private static Direction RotateClockwise(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => direction
        };
    }

    public override int CalculatePart1()
    {
        var mapWithPath =  CloneMap(_map);

        MarkPath(mapWithPath);
        var distinctPositions = 0;
        var numberOfRows = mapWithPath.GetLength(0);
        var numberOfColumns = mapWithPath.GetLength(1);

        for (var i = 0; i < numberOfRows; i++)
            for (var j = 0; j < numberOfColumns; j++)
                if (mapWithPath[i, j] == 'X')
                    distinctPositions++;

        return distinctPositions;
    }

    public override int CalculatePart2()
    {
        var numberOfRows = _map.GetLength(0);
        var numberOfColumns = _map.GetLength(1);

        var row = _startPosition.Row;
        var col = _startPosition.Col;
        var direction = Direction.Up;
        
        var loopPositions = 0;

        while (row >= 0 && row < numberOfRows && col >= 0 && col < numberOfColumns)
        {
            var (dRow, dCol) = Movement[direction];
            var nextRow = row + dRow;
            var nextCol = col + dCol;

            if (nextRow < 0 || nextRow >= numberOfRows || nextCol < 0 || nextCol >= numberOfColumns)
                break;
            if (_map[nextRow, nextCol] == '#')
            {
                direction = RotateClockwise(direction);
            }
            else
            {
                var mapCopy = CloneMap(_map);
                mapCopy[nextRow, nextCol] = '#';
                if (CanReturnToStart(mapCopy, new Position(row, col), direction))
                        loopPositions++;
                row = nextRow;
                col = nextCol;
            }
        }

        return loopPositions;
    }

    private bool CanReturnToStart(char[,] mapCopy, Position position, Direction direction)
    {
        var numberOfRows = mapCopy.GetLength(0);
        var numberOfColumns = mapCopy.GetLength(1);

        var row = position.Row;
        var col = position.Col;
        direction = RotateClockwise(direction);

        while (row >= 0 && row < numberOfRows && col >= 0 && col < numberOfColumns)
        {
            var (dRow, dCol) = Movement[direction];
            var nextRow = row + dRow;
            var nextCol = col + dCol;

            if (nextRow < 0 || nextRow >= numberOfRows || nextCol < 0 || nextCol >= numberOfColumns)
                return false;
            if (mapCopy[nextRow, nextCol] == '#')
            {
                direction = RotateClockwise(direction);
            }
            else
            {
                if (nextRow == position.Row && nextCol == position.Col)
                        return true;
                row = nextRow;
                col = nextCol;
            }
        }

        return false;
    }
}