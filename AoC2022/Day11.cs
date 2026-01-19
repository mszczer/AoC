namespace AoC.AoC2022;

internal class Day11(string dayName) : AoC<List<string>, long, long>(dayName)
{
    private readonly List<Monkey> _monkeys = [];

    public override long CalculatePart1()
    {
        _monkeys.Clear();
        GetMonkeys();
        InspectAndThrowTheItems(20, 3);
        return LevelOfMonkeyBusiness();
    }

    public override long CalculatePart2()
    {
        _monkeys.Clear();
        GetMonkeys();
        InspectAndThrowTheItems(10000, 1);
        return LevelOfMonkeyBusiness();
    }

    private void GetMonkeys()
    {
        _monkeys.Clear();

        if (InputData == null) return;

        // Group input into blocks separated by empty lines
        var blocks = new List<List<string>>();
        var current = new List<string>();
        foreach (var raw in InputData)
        {
            var line = raw ?? string.Empty;
            if (string.IsNullOrWhiteSpace(line))
            {
                if (current.Count > 0)
                {
                    blocks.Add(current);
                    current = [];
                }
            }
            else
            {
                current.Add(line.Trim());
            }
        }

        if (current.Count > 0) blocks.Add(current);

        foreach (var block in blocks)
        {
            // Expected block lines (robust parsing):
            // 0: "Monkey X:" (ignored)
            // items line contains "Starting items" and comma separated numbers
            // operation line contains "Operation" and e.g. "new = old * 19" or "* old"
            // test line contains "divisible by N"
            // if true line contains number
            // if false line contains number
            var worryLevels = new List<long>();
            Func<long, long> operation = old => old;
            var divisibleByValue = 1;
            var throwToIfTrue = 0;
            var throwToIfFalse = 0;

            foreach (var line in block)
                if (line.StartsWith("Starting items", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("Starting items:", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("Starting items ", StringComparison.OrdinalIgnoreCase))
                {
                    var idx = line.IndexOf(':');
                    var itemsPart = idx >= 0 ? line[(idx + 1)..].Trim() : line;
                    var items = itemsPart.Split(',',
                        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    foreach (var it in items)
                        if (long.TryParse(it, out var v))
                            worryLevels.Add(v);
                }
                else if (line.StartsWith("Operation", StringComparison.OrdinalIgnoreCase) ||
                         line.StartsWith("Operation:", StringComparison.OrdinalIgnoreCase) ||
                         line.StartsWith("Operation ", StringComparison.OrdinalIgnoreCase))
                {
                    // e.g. "Operation: new = old * 19" or "Operation: new = old + 6" or "Operation: new = old * old"
                    var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    if (parts.Length >= 2)
                    {
                        var right = parts[1].Trim(); // "old * 19" or "old + 6" or "old * old"
                        var tokens = right.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (tokens.Length == 3 && tokens[0] == "old")
                        {
                            var op = tokens[1];
                            var operand = tokens[2];
                            if (operand == "old")
                            {
                                // square
                                operation = old => old * old;
                            }
                            else if (long.TryParse(operand, out var opVal))
                            {
                                if (op == "*") operation = old => old * opVal;
                                else if (op == "+") operation = old => old + opVal;
                            }
                        }
                    }
                }
                else if (line.StartsWith("Test", StringComparison.OrdinalIgnoreCase) ||
                         line.Contains("divisible by", StringComparison.OrdinalIgnoreCase))
                {
                    // extract last number
                    var digits = new string(line.Where(c => char.IsDigit(c) || c == ' ').ToArray()).Trim();
                    var parts = digits.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0 && int.TryParse(parts.Last(), out var v)) divisibleByValue = v;
                }
                else if (line.StartsWith("If true", StringComparison.OrdinalIgnoreCase) ||
                         line.Contains("If true", StringComparison.OrdinalIgnoreCase))
                {
                    var digits = new string(line.Where(c => char.IsDigit(c) || c == ' ').ToArray()).Trim();
                    var parts = digits.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0 && int.TryParse(parts.Last(), out var v)) throwToIfTrue = v;
                }
                else if (line.StartsWith("If false", StringComparison.OrdinalIgnoreCase) ||
                         line.Contains("If false", StringComparison.OrdinalIgnoreCase))
                {
                    var digits = new string(line.Where(c => char.IsDigit(c) || c == ' ').ToArray()).Trim();
                    var parts = digits.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0 && int.TryParse(parts.Last(), out var v)) throwToIfFalse = v;
                }

            var mono = new Monkey([..worryLevels], operation, divisibleByValue, throwToIfTrue,
                throwToIfFalse);
            _monkeys.Add(mono);
        }
    }

    private static List<long> GetWorryLevels(IEnumerable<string> startingItems)
    {
        return startingItems.Select(long.Parse).ToList();
    }

    private long LevelOfMonkeyBusiness()
    {
        var mostActiveMonkeys = _monkeys.OrderByDescending(i => i.Inspections).Take(2).ToList();
        if (mostActiveMonkeys.Count < 2) return 0;
        return mostActiveMonkeys[0].Inspections * mostActiveMonkeys[1].Inspections;
    }

    private long GetCommonMultipleOfDivisibilityChecks()
    {
        return _monkeys.Aggregate(1L, (current, monkey) => current * monkey.DivisibleByValue);
    }

    private void InspectAndThrowTheItems(int totalRounds, int worryLevelDivider)
    {
        var commonMultiply = GetCommonMultipleOfDivisibilityChecks();

        for (var round = 0; round < totalRounds; round++)
            foreach (var monkey in _monkeys)
            {
                monkey.InspectTheItem(worryLevelDivider);

                foreach (var worryLevel in monkey.WorryLevels.ToList())
                {
                    var newWorryLevel = worryLevel % commonMultiply;

                    var toMonkey = monkey.IdentifyThrowToMonkey(newWorryLevel);
                    _monkeys[toMonkey].AddWorryLevel(newWorryLevel);
                    monkey.Inspections++;
                }

                monkey.WorryLevels.Clear();
            }
    }
}

internal class Monkey
{
    public List<long> WorryLevels { get; }
    public long Inspections { get; set; }
    public int DivisibleByValue { get; }
    private readonly Func<long, long> _operation;
    private readonly int _trueRecipient;
    private readonly int _falseRecipient;

    public Monkey(List<long> worryLevels, Func<long, long> operation, int divisibleByValue, int trueRecipient,
        int falseRecipient)
    {
        WorryLevels = worryLevels ?? [];
        _operation = operation ?? (old => old);
        DivisibleByValue = divisibleByValue;
        _trueRecipient = trueRecipient;
        _falseRecipient = falseRecipient;
        Inspections = 0;
    }

    public void InspectTheItem(int worryLevelDivider)
    {
        for (var i = 0; i < WorryLevels.Count; i++)
            WorryLevels[i] = _operation(WorryLevels[i]) / worryLevelDivider;
    }

    public long ApplyOperation(long old)
    {
        return _operation(old);
    }

    public int IdentifyThrowToMonkey(long item)
    {
        return item % DivisibleByValue == 0 ? _trueRecipient : _falseRecipient;
    }

    public void AddWorryLevel(long level)
    {
        WorryLevels.Add(level);
    }
}