namespace AoC.AoC2024;

public class Day08 : AoC<List<string>, int, int>
{
    private char[,] _antennaMap;
    private readonly HashSet<Cell> _antinodes = [];
    
    private readonly record struct Cell(int Row, int Col);

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

        ProcessMatchingPairs(HandleAddAntinodes);

        return _antinodes.Count;
    }

    public override int CalculatePart2()
    {
        _antinodes.Clear();

        ProcessMatchingPairs(HandleAddHarmonicAntinodes);

        return _antinodes.Count;
    }

    private void ProcessMatchingPairs(Action<Cell, Cell> handler)
    {
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
                            handler(new Cell(r, c), new Cell(searchedRow, searchedColumn));
                }
            }
    }

    private void HandleAddAntinodes(Cell first, Cell second) =>
        AddAntinodes(first, second);

    private void HandleAddHarmonicAntinodes(Cell first, Cell second) =>
        AddHarmonicAntinodes(first, second);

    private void AddAntinodes(Cell firstLocation, Cell secondLocation)
    {
        var dRow = secondLocation.Row - firstLocation.Row;
        var dCol = secondLocation.Col - firstLocation.Col;

        if (dRow == 0 && dCol == 0)
            return;

        var firstAntinode = new Cell(secondLocation.Row + dRow, secondLocation.Col + dCol);
        var secondAntinode = new Cell(firstLocation.Row - dRow, firstLocation.Col - dCol);

        if (IsCellValid(firstAntinode))
            _antinodes.Add(firstAntinode);

        if (IsCellValid(secondAntinode))
            _antinodes.Add(secondAntinode);
    }

    private bool IsCellValid(Cell antinode)
    {
        var rows = _antennaMap.GetLength(0);
        var columns = _antennaMap.GetLength(1);

        return antinode.Row >= 0 && antinode.Row < rows && antinode.Col >= 0 && antinode.Col < columns;
    }


    private void AddHarmonicAntinodes(Cell firstLocation, Cell secondLocation)
    {
        var dRow = secondLocation.Row - firstLocation.Row;
        var dCol = secondLocation.Col - firstLocation.Col;

        if (dRow == 0 && dCol == 0)
            return;

        var firstHarmonicLocation = firstLocation;
        var secondHarmonicLocation = secondLocation;

        if (IsCellValid(firstLocation))
            _antinodes.Add(firstLocation);

        if (IsCellValid(secondLocation))
            _antinodes.Add(secondLocation);

        while (IsCellValid(secondHarmonicLocation))
        {
            var firstAntinode = new Cell(secondHarmonicLocation.Row + dRow, secondHarmonicLocation.Col + dCol);
            if (IsCellValid(firstAntinode))
                _antinodes.Add(firstAntinode);
            secondHarmonicLocation = firstAntinode;
        }

        firstHarmonicLocation = firstLocation;
        secondHarmonicLocation = secondLocation;

        while (IsCellValid(firstHarmonicLocation))
        {
            var secondAntinode = new Cell(firstHarmonicLocation.Row - dRow, firstHarmonicLocation.Col - dCol);
            if (IsCellValid(secondAntinode))
                _antinodes.Add(secondAntinode);
            firstHarmonicLocation = secondAntinode;
        }
    }
}