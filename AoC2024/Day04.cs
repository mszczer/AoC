using System.Text;

namespace AoC.AoC2024;

internal class Day04 : AoC<List<string>, int, int>
{
    private char[,] _searchSheet;
    private List<string> _searchSentences;
    private static readonly Regex Part1Regex = new("(?=(XMAS|SAMX))", RegexOptions.Compiled);
    private static readonly Regex Part2Regex = new("MAS|SAM", RegexOptions.Compiled);

    public Day04(string dayName, string inputDirectory) : base(dayName, inputDirectory)
    {
        InitializeSearchData();
    }

    public Day04(string dayName) : base(dayName)
    {
        InitializeSearchData();
    }

    public Day04(string dayName, List<string> inputData) : base(dayName, inputData)
    {
        InitializeSearchData();
    }

    private void InitializeSearchData()
    {
        InitializeSearchSheet();
        GenerateSearchSentences();
    }

    private void InitializeSearchSheet()
    {
        if (InputData == null || InputData.Count == 0 || InputData[0].Length == 0)
            throw new InvalidOperationException("InputData is empty or not initialized.");

        var rows = InputData.Count;
        var columns = InputData[0].Length;

        _searchSheet = new char[rows, columns];

        for (var row = 0; row < rows; row++)
            for (var column = 0; column < columns; column++)
                _searchSheet[row, column] = InputData[row][column];
    }

    private void GenerateSearchSentences()
    {
        _searchSentences = [];

        AddRowSentences();
        AddColumnSentences();
        AddDiagonalSentences(true); // Top-left to bottom-right
        AddDiagonalSentences(false); // Top-right to bottom-left
    }

    private void AddRowSentences()
    {
        var rows = _searchSheet.GetLength(0);
        var columns = _searchSheet.GetLength(1);

        for (var row = 0; row < rows; row++)
        {
            var sentence = new StringBuilder(columns);

            for (var column = 0; column < columns; column++)
                sentence.Append(_searchSheet[row, column]);
            _searchSentences.Add(sentence.ToString());
        }
    }

    private void AddColumnSentences()
    {
        var rows = _searchSheet.GetLength(0);
        var columns = _searchSheet.GetLength(1);

        for (var column = 0; column < columns; column++)
        {
            var sentence = new StringBuilder(rows);
            for (var row = 0; row < rows; row++)
                sentence.Append(_searchSheet[row, column]);
            _searchSentences.Add(sentence.ToString());
        }
    }

    private void AddDiagonalSentences(bool topLeftToBottomRight)
    {
        var rows = _searchSheet.GetLength(0);
        var columns = _searchSheet.GetLength(1);
        var totalDiagonals = rows + columns - 1;


        for (var diag = 0; diag < totalDiagonals; diag++)
        {
            var sentence = new StringBuilder();
            int startRow, startCol;

            if (topLeftToBottomRight)
            {
                startRow = diag < rows ? rows - 1 - diag : 0;
                startCol = diag < rows ? 0 : diag - (rows - 1);
            }
            else
            {
                startRow = diag < columns ? 0 : diag - (columns - 1);
                startCol = diag < columns ? diag : columns - 1;
            }

            while (startRow < rows && startCol >= 0 && startCol < columns)
            {
                sentence.Append(_searchSheet[startRow, startCol]);
                startRow++;
                startCol += topLeftToBottomRight ? 1 : -1;
            }

            _searchSentences.Add(sentence.ToString());
        }
    }

    public override int CalculatePart1()
    {
        var occurrences = 0;

        foreach (var sentence in _searchSentences)
            occurrences += Part1Regex.Matches(sentence).Count;

        return occurrences;
    }

    public override int CalculatePart2()
    {
        var occurrences = 0;

        var rows = _searchSheet.GetLength(0);
        var columns = _searchSheet.GetLength(1);

        for (var row = 1; row < rows - 1; row++)
            for (var column = 1; column < columns - 1; column++)
                if (_searchSheet[row, column] == 'A')
                {
                    var topLeftToBottomRight = ExtractDiagonal(row, column, -1, -1, 1, 1);
                    var bottomLeftToTopRight = ExtractDiagonal(row, column, 1, -1, -1, 1);

                    if (Part2Regex.IsMatch(topLeftToBottomRight) && Part2Regex.IsMatch(bottomLeftToTopRight))
                        occurrences++;
                }

        return occurrences;
    }

    private string ExtractDiagonal(int centerRow, int centerColumn, int startRowOffset, int startColOffset,
        int endRowOffset, int endColOffset)
    {
        var rows = _searchSheet.GetLength(0);
        var columns = _searchSheet.GetLength(1);

        var startRow = centerRow + startRowOffset;
        var startCol = centerColumn + startColOffset;
        var endRow = centerRow + endRowOffset;
        var endCol = centerColumn + endColOffset;

        if (startRow < 0 || startRow >= rows || startCol < 0 || startCol >= columns ||
            endRow < 0 || endRow >= rows || endCol < 0 || endCol >= columns)
            return string.Empty;

        var sentence = new StringBuilder();
        sentence.Append(_searchSheet[startRow, startCol]);
        sentence.Append(_searchSheet[centerRow, centerColumn]);
        sentence.Append(_searchSheet[endRow, endCol]);
        return sentence.ToString();
    }
}