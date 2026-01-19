namespace AoC.AoC2023;

internal class Day01(string dayName) : AoC<List<string>, int, int>(dayName)
{
    public override int CalculatePart1()
    {
        return CalculateCalibrationFromDigits(InputData);
    }

    private static int CalculateCalibrationFromDigits(IEnumerable<string>? calibrationDocument)
    {
        return (calibrationDocument ?? []).Sum(ExtractDigits);
    }

    private static int ExtractDigits(string value)
    {
        if (string.IsNullOrEmpty(value)) return 0;

        var digits = value.Where(char.IsDigit).ToArray();
        if (digits.Length == 0) return 0;

        // Use first and last digit to form a two-digit number (robust to single-digit strings)
        var left = digits[0];
        var right = digits[^1];
        var twoDigit = new string(new[] { left, right });
        return int.Parse(twoDigit);
    }

    public override int CalculatePart2()
    {
        return CalculateCalibrationFromDigitsAndWords(InputData);
    }

    private static int CalculateCalibrationFromDigitsAndWords(IEnumerable<string>? calibrationDocument)
    {
        var wordToDigit = InitializeWordToDigitMapping();
        return (calibrationDocument ?? []).Sum(line =>
            ExtractDigitsAndWords(line, wordToDigit));
    }

    private static int ExtractDigitsAndWords(string value, IReadOnlyDictionary<string, string> wordToDigitMapping)
    {
        if (string.IsNullOrEmpty(value)) return 0;

        // Track occurrences per word (case-insensitive keys)
        var occurrences = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);
        var minIndex = int.MaxValue;
        var maxIndex = int.MinValue;
        string? minWord = null;
        string? maxWord = null;

        foreach (var word in wordToDigitMapping.Keys)
        {
            if (string.IsNullOrEmpty(word)) continue;

            var searchIndex = 0;
            while ((searchIndex = value.IndexOf(word, searchIndex, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                if (!occurrences.TryGetValue(word, out var list))
                {
                    list = [];
                    occurrences[word] = list;
                }

                list.Add(searchIndex);

                if (searchIndex < minIndex)
                {
                    minIndex = searchIndex;
                    minWord = word;
                }

                if (searchIndex > maxIndex)
                {
                    maxIndex = searchIndex;
                    maxWord = word;
                }

                // move forward to allow overlapping occurrences of different lengths safely
                searchIndex += Math.Max(word.Length, 1);
            }
        }

        if (minWord != null && maxWord != null)
        {
            var leftDigit = wordToDigitMapping.GetValueOrDefault(minWord, "0");
            var rightDigit = wordToDigitMapping.GetValueOrDefault(maxWord, "0");
            if (int.TryParse(leftDigit + rightDigit, out var result))
                return result;
        }

        return 0;
    }

    private static Dictionary<string, string> InitializeWordToDigitMapping()
    {
        // small, case-insensitive mapping from words and digit-strings to their numeric char
        var m = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
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

            // include numeric representations to allow direct matches to digits in text
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

        return m;
    }
}