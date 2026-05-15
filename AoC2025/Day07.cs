using AoC.Common;

namespace AoC.AoC2025;

public class Day07 : AoC<List<string>, int, long>
{
    private const char StartSymbol = 'S';
    private const char SplitterSymbol = '^';
    private const char EmptySymbol = '.';
    private const char BeamSymbol = '|';

    private char[,] _tachyonManifold;
    private int _rows;
    private int _columns;

    public Day07(string dayName) : base(dayName)
    {
        ParseInputData();
    }

    public Day07(string dayName, List<string> inputData) : base(dayName, inputData)
    {
        ParseInputData();
    }

    private void ParseInputData()
    {
        if (InputData == null || InputData.Count == 0)
            throw new InvalidOperationException("InputData is empty or not initialized.");

        _rows = InputData.Count;
        _columns = InputData[0].Length;

        _tachyonManifold = new char[_rows, _columns];

        for (var row = 0; row < _rows; row++)
        for (var col = 0; col < _columns; col++)
        {
            var currentCell = InputData[row][col];

            if (currentCell == StartSymbol)
            {
                _tachyonManifold[row, col] = StartSymbol;
                if (row + 1 < _rows)
                    _tachyonManifold[row + 1, col] = BeamSymbol;
            }
            else if (currentCell == SplitterSymbol)
            {
                _tachyonManifold[row, col] = SplitterSymbol;
            }

            if (row > 0 && _tachyonManifold[row - 1, col] == BeamSymbol)
            {
                if (currentCell == EmptySymbol)
                {
                    _tachyonManifold[row, col] = BeamSymbol;
                }
                else if (currentCell == SplitterSymbol)
                {
                    if (col > 0)
                        _tachyonManifold[row, col - 1] = BeamSymbol;
                    if (col < _columns - 1)
                        _tachyonManifold[row, col + 1] = BeamSymbol;
                }
            }
        }
    }

    public override int CalculatePart1()
    {
        var splitCount = 0;

        for (var row = 1; row < _rows; row++)
            for (var col = 0; col < _columns; col++)
            {
                var isBeamFromAbove = _tachyonManifold[row - 1, col] == BeamSymbol;
                var isSplitterAtCurrent = _tachyonManifold[row, col] == SplitterSymbol;

                if (isBeamFromAbove && isSplitterAtCurrent)
                    splitCount++;
            }

        return splitCount;
    }

    public override long CalculatePart2()
    {
        long pathCount = 0;

        for (var row = 0; row < _rows; row++)
            for (var col = 0; col < _columns; col++)
                if (_tachyonManifold[row, col] == StartSymbol)
                    pathCount = CountPathsFrom(row, col);

        return pathCount;
    }

    private long CountPathsFrom(int row, int col)
    {
        var memo = new Dictionary<(int, int), long>();
        return CountPathsRecursive(row + 1, col, memo);
    }

    private long CountPathsRecursive(int row, int col, Dictionary<(int, int), long> memo)
    {
        if (col < 0 || col >= _columns)
            return 0;

        if (row >= _rows)
            return 1;

        if (memo.TryGetValue((row, col), out var cachedCount))
            return cachedCount;

        long pathCount;
        var currentCell = InputData[row][col];

        if (currentCell == SplitterSymbol)
            pathCount = CountPathsRecursive(row + 1, col - 1, memo) +
                        CountPathsRecursive(row + 1, col + 1, memo);
        else
            pathCount = CountPathsRecursive(row + 1, col, memo);

        memo[(row, col)] = pathCount;
        return pathCount;
    }
}