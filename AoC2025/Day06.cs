using AoC.Common;

namespace AoC.AoC2025;

public class Day06 : AoC<List<string>, long, long>
{
    private readonly List<List<int>> _numberRows = [];
    private readonly List<List<int>> _numberColumns = [];
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

        var nonEmptyLines = InputData.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();

        if (nonEmptyLines.Count < 2)
            throw new InvalidOperationException("InputData must contain at least 2 rows (numbers and operations).");

        for (var i = 0; i < nonEmptyLines.Count - 1; i++)
        {
            var numberRow = ParseNumberRow(nonEmptyLines[i]);
            _numberRows.Add(numberRow);
        }

        for (var i = 0; i < _numberRows[0].Count; i++) _numberColumns.Add(new List<int>());

        foreach (var row in _numberRows)
            for (var i = 0; i < row.Count; i++)
                _numberColumns[i].Add(row[i]);

        var operationsRow = nonEmptyLines[^1];
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
        return _numberColumns[columnIndex].Sum();
    }

    private long MultiplyColumn(int columnIndex)
    {
        return _numberColumns[columnIndex].Aggregate(1L, (product, value) => product * value);
    }

    public override long CalculatePart2()
    {
        var nonEmptyLines = InputData.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
        var numberRowStrings = nonEmptyLines.Take(nonEmptyLines.Count - 1).ToList();
        var maxLength = numberRowStrings.Max(s => s.Length);

        var problems = new List<List<long>>();
        for (var i = 0; i < _operations.Count; i++) problems.Add([]);

        var currentProblemIndex = _operations.Count - 1;

        for (var charPos = maxLength - 1; charPos >= 0; charPos--)
        {
            var columnDigits = "";
            var hasDigit = false;

            foreach (var row in numberRowStrings)
                if (charPos < row.Length)
                {
                    var ch = row[charPos];
                    if (char.IsDigit(ch))
                    {
                        columnDigits += ch;
                        hasDigit = true;
                    }
                }

            if (hasDigit)
            {
                problems[currentProblemIndex].Add(long.Parse(columnDigits));
            }
            else if (problems[currentProblemIndex].Count > 0)
            {
                currentProblemIndex--;
                if (currentProblemIndex < 0) break;
            }
        }

        long result = 0;
        for (var i = 0; i < _operations.Count; i++)
        {
            var operation = _operations[i];
            var numbers = problems[i];

            if (numbers.Count == 0) continue;

            var problemResult = operation switch
            {
                "+" => numbers.Sum(),
                "*" => numbers.Aggregate(1L, (product, value) => product * value),
                _ => throw new InvalidOperationException($"Unknown operation: {operation}")
            };

            result += problemResult;
        }

        return result;
    }
}