using System.Text.RegularExpressions;
using AoC.Common;

namespace AoC.AoC2024;

public class Day03 : AoC<List<string>, int, int>
{
    private const string InstructionPattern = @"do\(\)|don't\(\)|mul\((\d{1,3}),(\d{1,3})\)";
    private const string NumberPattern = @"\d{1,3}";
    private List<string> _instructions;

    public Day03(string dayName, string inputDirectory) : base(dayName, inputDirectory)
    {
        ParseInstructions();
    }

    public Day03(string dayName) : base(dayName)
    {
        ParseInstructions();
    }

    private void ParseInstructions()
    {
        _instructions = new List<string>();

        foreach (var memoryDump in InputData)
        {
            var matchingInstructions = Regex.Matches(memoryDump, InstructionPattern);

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
        var numbers = Regex.Matches(instruction, NumberPattern);

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
}