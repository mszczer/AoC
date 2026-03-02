using AoC.Common;

namespace AoC.AoC2025;

public class Day03 : AoC<List<string>, int, int>
{
    private readonly List<string> _batteryBanks;

    public Day03(string dayName) : base(dayName)
    {
        _batteryBanks = ParseInputData(InputData);
    }

    public Day03(string dayName, IEnumerable<string> inputData) : base(dayName)
    {
        ArgumentNullException.ThrowIfNull(inputData);
        _batteryBanks = ParseInputData(inputData as IReadOnlyCollection<string> ?? inputData.ToList());
    }

    private static List<string> ParseInputData(IReadOnlyCollection<string>? inputData)
    {
        if (inputData == null || inputData.Count == 0)
            throw new InvalidOperationException("InputData is empty or not initialized.");

        return [..inputData];
    }

    public override int CalculatePart1()
    {
        return _batteryBanks.Sum(GetLargestVoltage);
    }

    private static int GetLargestVoltage(string bank)
    {
        if (bank.Length < 2)
            throw new ArgumentException("Bank must contain at least two digits.", nameof(bank));

        var firstIdx = 0;
        var firstMax = bank[0];
        for (var i = 0; i < bank.Length - 1; i++)
        {
            if (bank[i] > firstMax)
            {
                firstMax = bank[i];
                firstIdx = i;
            }

            if (firstMax == '9')
                break;
        }

        var secondIdx = firstIdx + 1;
        var secondMax = bank[secondIdx];
        for (var i = secondIdx; i < bank.Length; i++)
        {
            if (bank[i] > secondMax)
            {
                secondMax = bank[i];
                secondIdx = i;
            }

            if (secondMax == '9')
                break;
        }

        var d1 = firstMax - '0';
        var d2 = secondMax - '0';
        return d1 * 10 + d2;
    }

    public override int CalculatePart2()
    {
        throw new NotImplementedException();
    }
}