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

    private static bool CanProduceTestValue(List<long> calibration) =>
        CanProduceWithOperators(calibration, ["+", "*"]);

    public override long CalculatePart2()
    {
        return _calibrations
            .Where(CanConcatenatedNumbersProduceTestValue)
            .Sum(calibration => calibration[0]);
    }

    private static bool CanConcatenatedNumbersProduceTestValue(List<long> calibration) =>
        CanProduceWithOperators(calibration, ["+", "*", "||"]);

    private static bool CanProduceWithOperators(List<long> calibration, string[] operatorSet)
    {
        if (calibration.Count == 2 && calibration[1] == calibration[0]) return true;
        if (calibration.Count < 2) return false;

        var n = calibration.Count - 1;
        var target = calibration[0];
        var opSlots = n - 1;
        var totalCombos = (int)Math.Pow(operatorSet.Length, opSlots);

        for (var comboIdx = 0; comboIdx < totalCombos; comboIdx++)
        {
            var temp = comboIdx;
            var result = calibration[1];

            for (var pos = 0; pos < opSlots; pos++)
            {
                var op = operatorSet[temp % operatorSet.Length];
                temp /= operatorSet.Length;

                var next = calibration[pos + 2];
                result = ApplyOperator(result, next, op);
            }

            if (result == target)
                return true;
        }

        return false;
    }

    private static long ApplyOperator(long result, long next, string op) =>
        op switch
        {
            "+" => result + next,
            "*" => result * next,
            "||" => Concat(result, next),
            _ => throw new InvalidOperationException("Unexpected operator value")
        };

    private static long Concat(long a, long b) =>
        long.Parse(a.ToString() + b.ToString());
}