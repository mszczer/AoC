using System.Text;
using System.Text.RegularExpressions;

namespace AoC.AoC2022;

internal class Day05 : AoC<List<string>, string, string>
{
    private List<int[]> _rearrangement;
    private int _stacksNumber;
    private int _stacksIdRowNumber;

    public Day05(string dayName) : base(dayName)
    {
        IdentifyStackNumbers();
        GetRearrangementProcedure();
    }

    private void IdentifyStackNumbers()
    {
        _stacksIdRowNumber = 0;
        _stacksNumber = 0;

        if (InputData == null) return;

        var index = 0;
        foreach (var line in InputData)
        {
            index++;
            if (string.IsNullOrEmpty(line)) continue;

            // look for a line that starts with space then digits representing stack ids e.g. " 1   2   3"
            if (line.TrimStart().StartsWith("1"))
            {
                _stacksIdRowNumber = index;
                // find last digit in the line as the stack count
                var matches = Regex.Matches(line, @"\d+");
                if (matches.Count > 0 && int.TryParse(matches[^1].Value, out var parsed))
                    _stacksNumber = parsed;
                break;
            }
        }
    }

    private Stack<char>[] GetInitialStacks()
    {
        if (_stacksNumber <= 0) return [];

        var stacks = new Stack<char>[_stacksNumber];
        for (var i = 0; i < _stacksNumber; i++)
            stacks[i] = new Stack<char>();

        if (InputData == null || _stacksIdRowNumber <= 1) return stacks;

        for (var lvl = _stacksIdRowNumber - 2; lvl >= 0; lvl--)
        {
            var stackNumber = 0;

            for (var idx = 1; idx <= _stacksNumber * 4; idx++)
                if (idx % 4 == 0)
                {
                    var ch = InputData[lvl][idx - 3];
                    if (ch != ' ')
                        stacks[stackNumber].Push(ch);
                    stackNumber++;
                }
        }

        return stacks;
    }

    private void GetRearrangementProcedure()
    {
        _rearrangement = [];

        if (InputData == null) return;

        foreach (var line in InputData)
            if (!string.IsNullOrWhiteSpace(line) && line.StartsWith("move"))
            {
                var parts = line.Split(' ');
                if (parts.Length >= 6 &&
                    int.TryParse(parts[1], out var q) &&
                    int.TryParse(parts[3], out var s) &&
                    int.TryParse(parts[5], out var t))
                    _rearrangement.Add([q, s, t]);
            }
    }

    private int GetStacksNumber()
    {
        if (InputData == null) return 0;

        foreach (var line in InputData)
            if (!string.IsNullOrEmpty(line) && line.StartsWith(" 1"))
                if (int.TryParse(line.Substring(line.Length - 2, 1), out var val))
                    return val;

        return 0;
    }

    public override string CalculatePart1()
    {
        var stacks = GetInitialStacks();

        foreach (var step in _rearrangement)
            MoveCrates(stacks, step[0], step[1], step[2]);

        return GetAllTops(stacks);
    }

    private static string GetAllTops(IEnumerable<Stack<char>> stacks)
    {
        var sb = new StringBuilder();
        foreach (var st in stacks) 
            sb.Append(st.Count > 0 ? st.Peek() : ' ');

        return sb.ToString();
    }

    private static void MoveCrates(IReadOnlyList<Stack<char>> stacks, int quantity, int source, int target)
    {
        if (stacks == null || stacks.Count == 0) return;
        if (source <= 0 || source > stacks.Count || target <= 0 || target > stacks.Count) return;

        for (var i = 0; i < quantity; i++)
            stacks[target - 1].Push(stacks[source - 1].Pop());
    }

    public override string CalculatePart2()
    {
        var stacks = GetInitialStacks();

        foreach (var step in _rearrangement)
            MoveMultipleCrates(stacks, step[0], step[1], step[2]);

        return GetAllTops(stacks);
    }

    private static void MoveMultipleCrates(IReadOnlyList<Stack<char>> stacks, int quantity, int source, int target)
    {
        if (stacks == null || stacks.Count == 0) return;
        if (source <= 0 || source > stacks.Count || target <= 0 || target > stacks.Count) return;

        var tempStack = new Stack<char>();

        for (var i = 0; i < quantity; i++)
            tempStack.Push(stacks[source - 1].Pop());

        for (var i = 0; i < quantity; i++)
            stacks[target - 1].Push(tempStack.Pop());
    }
}