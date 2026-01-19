namespace AoC.AoC2024;

public partial class Day03 : AoC<List<string>, int, int>
{
    private static readonly Regex InstructionRegex =
        InstructionPattern();

    private static readonly Regex NumberRegex = NumberPattern();
    private List<string> _instructions;

    public Day03(string dayName) : base(dayName)
    {
        ParseInstructions();
    }

    public Day03(string dayName, List<string> inputData) : base(dayName, inputData)
    {
        ParseInstructions();
    }

    private void ParseInstructions()
    {
        _instructions = [];

        foreach (var memoryDump in InputData ?? Enumerable.Empty<string>())
        {
            if (string.IsNullOrEmpty(memoryDump)) continue;

            var matchingInstructions = InstructionRegex.Matches(memoryDump);
            foreach (Match match in matchingInstructions)
                _instructions.Add(match.Value);
        }
    }

    public override int CalculatePart1()
    {
        var mulResults = 0;

        foreach (var instruction in _instructions)
            if (!IsControlInstruction(instruction))
                mulResults += ExecuteInstruction(instruction);

        return mulResults;
    }

    private static bool IsControlInstruction(string instruction)
    {
        return instruction is "do()" or "don't()";
    }

    private static int ExecuteInstruction(string instruction)
    {
        var numbers = NumberRegex.Matches(instruction);
        if (numbers.Count != 2)
            throw new FormatException($"Instruction '{instruction}' does not contain two numbers.");

        return int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
    }

    public override int CalculatePart2()
    {
        var isEnabled = true;
        var mulResults = 0;

        foreach (var instruction in _instructions)
            if (instruction == "do()") isEnabled = true;
            else if (instruction == "don't()") isEnabled = false;
            else if (isEnabled)
                mulResults += ExecuteInstruction(instruction);

        return mulResults;
    }

    [GeneratedRegex(@"do\(\)|don't\(\)|mul\((\d{1,3}),(\d{1,3})\)", RegexOptions.Compiled)]
    private static partial Regex InstructionPattern();
    [GeneratedRegex(@"\d{1,3}", RegexOptions.Compiled)]
    private static partial Regex NumberPattern();
}