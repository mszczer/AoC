namespace AoC.AoC2022;

internal class Day13 : AoC<List<string>, int, int>
{
    private List<(object, object)> _packets;
    private List<object> _sortedPackets;
    private readonly List<string> _dividers;

    public Day13(string dayName) : base(dayName)
    {
        GetDistressSignal();
        _dividers = ["[[2]]", "[[6]]"];
        GetDistressSignal(_dividers);
    }

    private void GetDistressSignal()
    {
        _packets = [];

        List<object> leftValue = null;
        List<object> rightValue = null;

        var lineIdx = 0;

        if (InputData == null) return;

        foreach (var pocket in InputData)
        {
            lineIdx++;
            var pocketIdx = lineIdx % 3;
            switch (pocketIdx)
            {
                case 0:
                    break;
                case 1:
                    leftValue = ParsePocket(pocket);
                    break;
                case 2:
                    rightValue = ParsePocket(pocket);
                    _packets.Add((leftValue, rightValue));
                    break;
                default:
                    throw new InvalidOperationException("Unexpected value in input file");
            }
        }
    }

    private void GetDistressSignal(List<string> dividers)
    {
        _sortedPackets = [];

        if (InputData != null)
            foreach (var pocket in InputData.Where(pocket => pocket != ""))
            {
                var pocketValue = ParsePocket(pocket);
                _sortedPackets.Add(pocketValue);
            }

        foreach (var divider in dividers)
        {
            var pocketValue = ParsePocket(divider);
            _sortedPackets.Add(pocketValue);
        }

        _sortedPackets.Sort(ComparePockets);
        _sortedPackets.Reverse();
    }

    /*
     * Credits: https://www.honingjs.com/challenges/adventofcode/2022/day-13
     */
    public static List<object> ParsePocket(string inputValue)
    {
        if (string.IsNullOrEmpty(inputValue))
            throw new ArgumentException("Input value is null or empty", nameof(inputValue));

        var stack = new Stack<List<object>>();
        List<object> root = null;

        for (var i = 0; i < inputValue.Length; i++)
        {
            var c = inputValue[i];

            if (c == '[')
            {
                var newList = new List<object>();
                if (stack.Count > 0)
                    stack.Peek().Add(newList);
                else
                    root = newList;

                stack.Push(newList);
            }
            else if (c == ']')
            {
                if (stack.Count == 0)
                    throw new FormatException("Invalid input: Unbalanced brackets (extra closing bracket)");

                stack.Pop();
            }
            else if (c == ',')
            {
                // Skip comma
            }
            else if (char.IsDigit(c))
            {
                var startIndex = i;
                while (i < inputValue.Length && char.IsDigit(inputValue[i]))
                    i++;

                var numberString = inputValue.Substring(startIndex, i - startIndex);
                if (!int.TryParse(numberString, out var number))
                    throw new FormatException($"Invalid input: Failed to parse number: {numberString}");

                if (stack.Count == 0)
                    throw new FormatException("Invalid input: Number found outside any list");

                stack.Peek().Add(number);
                i--; // compensate for outer loop increment
            }
            else if (char.IsWhiteSpace(c))
            {
                // ignore whitespace
            }
            else
            {
                throw new FormatException($"Invalid input: Unexpected character: {c}");
            }
        }

        if (root == null)
            throw new FormatException("Invalid input: No list detected");

        if (stack.Count != 0)
            throw new FormatException("Invalid input: Unbalanced brackets (missing closing bracket)");

        return root;
    }


    public override int CalculatePart1()
    {
        var sumOfIndices = 0;

        if (_packets == null) return 0;

        for (var idx = 0; idx < _packets.Count; idx++)
            if (ComparePockets(_packets[idx].Item1, _packets[idx].Item2) == 1)
                sumOfIndices += idx + 1;

        return sumOfIndices;
    }

    private static int ComparePockets(object leftValue, object rightValue)
    {
        if (leftValue is int leftInt && rightValue is int rightInt)
        {
            if (leftInt < rightInt) return 1;
            if (leftInt > rightInt) return -1;
        }
        else if (leftValue is List<object> list && rightValue is List<object> value)
        {
            for (var idx = 0; idx < list.Count; idx++)
            {
                if (idx >= value.Count) return -1;
                var result = ComparePockets(list[idx], value[idx]);
                if (result != 0) return result;
            }

            if (list.Count < value.Count) return 1;
        }
        else
        {
            var leftList = leftValue is int ? new List<object> { leftValue } : leftValue as List<object>;
            var rightList = rightValue is int ? new List<object> { rightValue } : rightValue as List<object>;

            return ComparePockets(leftList, rightList);
        }

        return 0;
    }

    public override int CalculatePart2()
    {
        var decoderKey = 1;

        if (_sortedPackets == null || _dividers == null || _dividers.Count == 0) return 0;

        var dividerObjects = _dividers.Select(ParsePocket).Cast<object>().ToList();

        for (var idx = 0; idx < _sortedPackets.Count; idx++)
            foreach (var divider in dividerObjects)
                if (ComparePockets(_sortedPackets[idx], divider) == 0)
                    decoderKey *= idx + 1;

        return decoderKey;
    }
}