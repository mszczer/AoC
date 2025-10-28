namespace AoC.AoC2024;

public class Day07 : AoC<List<string>, long, long>
{
    private readonly List<List<long>> _calibrations = [];

    public Day07(string day) : base(day)
    {
        ParseInputData();
    }

    private void ParseInputData()
    {
        _calibrations.Clear();

        foreach (var reportLine in InputData)
        {
            var numbers = reportLine
                .Split([' ', ':'], StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToList();
            _calibrations.Add(numbers);
        }
    }

    public override long CalculatePart1()
    {
        return _calibrations
            .Where(CanProduceTestValue)
            .Sum(calibration => calibration[0]);
    }

    private static bool CanProduceTestValue(List<long> calibration)
    {
        if (calibration.Count == 2 && calibration[1] == calibration[0]) return true;
        if (calibration.Count < 2) return false;

        var n = calibration.Count - 1;
        var target = calibration[0];
        var totalCombos = Math.Pow(2, n - 1);

        for (var comboIdx = 0; comboIdx < totalCombos; comboIdx++)
        {
            var operators = new List<string>();
            var temp = comboIdx;
            for (var op = 0; op < n - 1; op++)
            {
                operators.Add(temp % 2 == 0 ? "+" : "*");
                temp /= 2;
            }

            var result = calibration[1];
            for (var i = 0; i < operators.Count; i++)
                if (operators[i] == "+")
                    result += calibration[i + 2];
                else
                    result *= calibration[i + 2];

            if (result == target)
                return true;
        }

        return false;
    }


    public override long CalculatePart2()
    {
        throw new NotImplementedException();
    }
}