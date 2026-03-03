using AoC.Common;

namespace AoC.AoC2025;

public class Day03 : AoC<List<string>, long, long>
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

        return inputData.ToList();
    }

    public override long CalculatePart1()
    {
        return _batteryBanks.Sum(bank => GetLargestVoltage(bank, 2));
    }

    public override long CalculatePart2()
    {
        return _batteryBanks.Sum(bank => GetLargestVoltage(bank, 12));
    }

    private static long GetLargestVoltage(string bank, int numberOfBatteries)
    {
        if (numberOfBatteries <= 0)
            throw new ArgumentException("Number of batteries must be positive.", nameof(numberOfBatteries));

        if (string.IsNullOrEmpty(bank))
            throw new ArgumentException("Bank string must not be null or empty.", nameof(bank));

        if (bank.Length < numberOfBatteries)
            throw new ArgumentException($"Bank must contain at least {numberOfBatteries} digits.", nameof(bank));

        if (bank.Length == numberOfBatteries)
            return long.Parse(bank);

        var bankLength = bank.Length;
        var startIdx = 0;
        var voltage = new char[numberOfBatteries];

        for (var picked = 0; picked < numberOfBatteries; picked++)
        {
            var remainingToPick = numberOfBatteries - picked;
            var remainingChars = bankLength - startIdx;

            // if remaining characters equals needed digits, copy the rest and finish
            if (remainingChars == remainingToPick)
            {
                for (var j = 0; j < remainingChars; j++)
                    voltage[picked + j] = bank[startIdx + j];

                return long.Parse(new string(voltage));
            }

            var maxAllowedIndex = bankLength - remainingToPick;
            var maxChar = bank[startIdx];
            var maxIdx = startIdx;

            // search for the maximum digit in the allowed window
            for (var i = startIdx; i <= maxAllowedIndex; i++)
            {
                var digit = bank[i];
                if (digit > maxChar)
                {
                    maxChar = digit;
                    maxIdx = i;
                    if (maxChar == '9') // early exit for best possible digit
                        break;
                }
            }

            voltage[picked] = maxChar;
            startIdx = maxIdx + 1;
        }

        return long.Parse(new string(voltage));
    }
}