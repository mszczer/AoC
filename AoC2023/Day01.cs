using AoC.Common;

namespace AoC.AoC2023;

internal class Day01 : AoC<List<string>, int, int>
{
    private List<int> _calibrationValues;

    public Day01(string dayName) : base(dayName)
    {
        GetCalibrationValues();
    }

    private void GetCalibrationValues()
    {
        _calibrationValues = new List<int>();

        foreach (var line in InputData)
        {
            var number = ExtractAndConcatenateAlphanumeric(line);
            _calibrationValues.Add(number);
        }
    }

    private static int ExtractAndConcatenateAlphanumeric(string input)
    {
        var alphanumericChars = input.Where(char.IsDigit).ToArray();

        var firstChar = alphanumericChars[0];
        var lastChar = alphanumericChars[^1];

        var twoDigitNumber = string.Concat(firstChar, lastChar);

        return int.Parse(twoDigitNumber);
    }


    public override int CalculatePart1()
    {
        return CalculateCalibration(_calibrationValues);
    }

    private static int CalculateCalibration(IEnumerable<int> calibrationValues)
    {
        return calibrationValues.Sum();
    }

    public override int CalculatePart2()
    {
        return -1;
    }
}