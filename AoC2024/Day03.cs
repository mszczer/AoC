using System.Text.RegularExpressions;
using AoC.Common;

namespace AoC.AoC2024;

public class Day03 : AoC<List<string>, int, int>
{
    private List<string> _instructions;

    public Day03(string dayName, string inputDirectory) : base(dayName, inputDirectory)
    {
    }

    public Day03(string dayName) : base(dayName)
    {
        ParseMemoryDump();
    }

    private void ParseMemoryDump()
    {
        _instructions = new List<string>();

        const string pattern = @"do\(\)|don't\(\)|mul\((\d{1,3}),(\d{1,3})\)";

        foreach (var memoryDump in InputData)
        {
            var matchingInstructions = Regex.Matches(memoryDump, pattern);

            foreach (Match match in matchingInstructions)
                _instructions.Add(match.Value);
        }
    }

    public override int CalculatePart1()
    {
        var mulResults = 0;

        foreach (var instruction in _instructions)
            if (instruction != "do()" && instruction != "don't()")
                mulResults += ExecuteInstruction(instruction);

        return mulResults;
    }

    private static int ExecuteInstruction(string instruction)
    {
        const string pattern = @"\d{1,3}";

        var numbers = Regex.Matches(instruction, pattern);

        return int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
    }

    public override int CalculatePart2()
    {
        var enableFlag = true;

        var mulResults = 0;

        foreach (var instruction in _instructions)
            if (instruction == "do()") enableFlag = true;
            else if (instruction == "don't()") enableFlag = false;
            else if (enableFlag)
                mulResults += ExecuteInstruction(instruction);

        return mulResults;
    }
}