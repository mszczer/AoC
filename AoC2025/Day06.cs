using AoC.Common;

namespace AoC.AoC2025;

public class Day06 : AoC<List<string>, long, long>
{
    private readonly List<List<int>> _numberRows = [];
    private readonly List<string> _operations = [];

    public Day06(string dayName) : base(dayName)
    {
        ParseInputData();
    }

    public Day06(string dayName, List<string> inputData) : base(dayName, inputData)
    {
        ParseInputData();
    }

    private void ParseInputData()
    {
        if (InputData == null || InputData.Count == 0)
            throw new InvalidOperationException("InputData is empty or not initialized.");

        if (InputData.Count < 2)
            throw new InvalidOperationException("InputData must contain at least 2 rows (numbers and operations).");

        for (var i = 0; i < InputData.Count - 1; i++)
        {
            var numberRow = ParseNumberRow(InputData[i]);
            _numberRows.Add(numberRow);
        }

        var operationsRow = InputData[^1];
        var operationParts =
            operationsRow.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        _operations.AddRange(operationParts);
    }

    private static List<int> ParseNumberRow(string row)
    {
        var parts = row.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var numbers = new List<int>();

        foreach (var part in parts)
        {
            if (!int.TryParse(part, out var number))
                throw new FormatException($"Failed to parse '{part}' as an integer.");

            numbers.Add(number);
        }

        return numbers;
    }

    public override long CalculatePart1()
    {
        long result = 0;

        for (var columnIndex = 0; columnIndex < _operations.Count; columnIndex++)
        {
            var operation = _operations[columnIndex];
            var columnResult = CalculateColumnOperation(columnIndex, operation);
            result += columnResult;
        }

        return result;
    }

    private long CalculateColumnOperation(int columnIndex, string operation)
    {
        ValidateColumnIndex(columnIndex);

        return operation switch
        {
            "+" => SumColumn(columnIndex),
            "*" => MultiplyColumn(columnIndex),
            _ => throw new InvalidOperationException($"Unknown operation: {operation}")
        };
    }

    private void ValidateColumnIndex(int columnIndex)
    {
        foreach (var row in _numberRows)
            if (columnIndex >= row.Count)
                throw new IndexOutOfRangeException(
                    $"Column index {columnIndex} is out of range for a row with {row.Count} elements.");
    }

    private long SumColumn(int columnIndex)
    {
        return _numberRows.Sum(row => row[columnIndex]);
    }

    private long MultiplyColumn(int columnIndex)
    {
        return _numberRows.Aggregate(1L, (product, row) => product * row[columnIndex]);
    }

    public override long CalculatePart2()
    {
        throw new NotImplementedException();
    }
}