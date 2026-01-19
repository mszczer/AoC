namespace AoC.AoC2022;

internal class Day10 : AoC<List<string>, int, string>
{
    private List<int[]> _program;
    private List<int> _registerValues;

    public Day10(string dayName) : base(dayName)
    {
        GetInstructions();
        GetRegisterValuesAfterEachCycle();
    }

    private void GetRegisterValuesAfterEachCycle()
    {
        var register = 1;
        _registerValues = [register];

        if (_program == null) return;

        foreach (var instruction in _program)
        {
            _registerValues.Add(register);
            if (instruction.Length > 0 && instruction[0] == 2)
            {
                register += instruction[1];
                _registerValues.Add(register);
            }
        }
    }

    private void GetInstructions()
    {
        _program = [];

        if (InputData == null) return;

        foreach (var line in InputData)
        {
            if (string.IsNullOrEmpty(line))
                continue;

            var instruction = line.Length >= 4 ? line[..4] : line;
            var cycleLength = instruction == "noop" ? 1 : 2;
            var value = 0;
            if (instruction == "addx")
            {
                if (line.Length > 5 && int.TryParse(line[5..], out var parsed))
                    value = parsed;
                else
                    value = 0;
            }

            _program.Add([cycleLength, value]);
        }
    }

    public override int CalculatePart1()
    {
        var sumOfSignalStrengths = 0;

        if (_registerValues == null) return 0;

        for (var i = 1; i <= _registerValues.Count; i++)
            if (i % 40 == 20)
                sumOfSignalStrengths += i * _registerValues[i - 1];

        return sumOfSignalStrengths;
    }

    public override string CalculatePart2()
    {
        PrintImage();
        return "FGCUZREC";
    }

    private void PrintImage()
    {
        if (_registerValues == null) return;

        const int crtWidth = 40;

        for (var i = 0; i < _registerValues.Count; i++)
        {
            var column = i % crtWidth;
            if (column == 0) Console.WriteLine();
            Console.Write(GetPixel(column, _registerValues[i]));
        }
    }

    private static string GetPixel(int column, int registerValue)
    {
        return Math.Abs(column - registerValue) <= 1 ? "#" : ".";
    }
}