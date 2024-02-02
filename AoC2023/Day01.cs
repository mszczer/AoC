using AoC.Common;

namespace AoC.AoC2023;

internal class Day01 : AoC<List<string>, int, int>
{
    public Day01(string dayName) : base(dayName)
    {
    }

    public override int CalculatePart1()
    {
        return CalculateCalibrationFromDigits(InputData);
    }

    private static int CalculateCalibrationFromDigits(IEnumerable<string> calibrationDocument)
    {
        return calibrationDocument.Sum(ExtractDigits);
    }

    private static int ExtractDigits(string value)
    {
        var alphanumericChars = value.Where(char.IsDigit).ToArray();

        if (alphanumericChars.Length == 0)
            return 0;

        var twoDigitNumber = string.Concat(alphanumericChars[0], alphanumericChars[^1]);
        return int.Parse(twoDigitNumber);
    }


    public override int CalculatePart2()
    {
        return CalculateCalibrationFromDigitsAndWords(InputData);
    }

    private int CalculateCalibrationFromDigitsAndWords(IEnumerable<string> calibrationDocument)
    {
        var wordToDigitMapping = InitializeWordToDigitMapping();

        return calibrationDocument.Sum(value => ExtractDigitsAndWords(value, wordToDigitMapping));
    }

    private static int ExtractDigitsAndWords(string value, Dictionary<string, string> wordToDigitMapping)
    {
        var occurrences = new Dictionary<string, List<int>>();
        var minValue = int.MaxValue;
        var maxValue = int.MinValue;

        foreach (var (word, digit) in wordToDigitMapping)
        {
            var index = 0;
            while ((index = value.IndexOf(word, index, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                if (!occurrences.ContainsKey(digit)) occurrences[digit] = new List<int>();

                occurrences[digit].Add(index);

                // Update min and max values during the loop
                minValue = Math.Min(minValue, index);
                maxValue = Math.Max(maxValue, index);

                // Move to the next character to handle overlapping occurrences
                index += Math.Max(word.Length, 1);
            }
        }

        if (minValue != int.MaxValue && maxValue != int.MinValue)
        {
            var minValueKey = occurrences.First(x => x.Value.Contains(minValue)).Key;
            var maxValueKey = occurrences.First(x => x.Value.Contains(maxValue)).Key;

            var concatenatedDigits = $"{wordToDigitMapping[minValueKey]}{wordToDigitMapping[maxValueKey]}";
            return int.Parse(concatenatedDigits);
        }

        return 0; // Return 0 if no valid concatenation is found
    }

    private static Dictionary<string, string> InitializeWordToDigitMapping()
    {
        var wordToDigitMapping = new Dictionary<string, string>
        {
            { "one", "1" },
            { "two", "2" },
            { "three", "3" },
            { "four", "4" },
            { "five", "5" },
            { "six", "6" },
            { "seven", "7" },
            { "eight", "8" },
            { "nine", "9" },
            { "1", "1" },
            { "2", "2" },
            { "3", "3" },
            { "4", "4" },
            { "5", "5" },
            { "6", "6" },
            { "7", "7" },
            { "8", "8" },
            { "9", "9" }
        };

        return wordToDigitMapping;
    }
}