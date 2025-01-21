using System.Text;
using System.Text.RegularExpressions;
using AoC.Common;

namespace AoC.AoC2024;

internal class Day04 : AoC<List<string>, int, int>
{
    private char[,] _searchSheet;
    private List<string> _searchSentences;

    public Day04(string dayName, string inputDirectory) : base(dayName, inputDirectory)
    {
        InitializeSearchData();
    }

    public Day04(string dayName) : base(dayName)
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
        var rows = InputData.Count;
        var columns = InputData[0].Length;

        _searchSheet = new char[rows, columns];

        for (var row = 0; row < rows; row++)
            for (var column = 0; column < columns; column++)
                _searchSheet[row, column] = InputData[row][column];
    }

    private void GenerateSearchSentences()
    {
        _searchSentences = new List<string>();

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
            var sentence = new StringBuilder();

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
            var sentence = new StringBuilder();
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
            occurrences += CountPatternOccurences(sentence, "(?=(XMAS|SAMX))");

        return occurrences;
    }

    private static int CountPatternOccurences(string sentence, string pattern)
    {
        return Regex.Matches(sentence, pattern).Count;
    }

    public override int CalculatePart2()
    {
        return int.MinValue;
    }
}