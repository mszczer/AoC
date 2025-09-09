namespace AoC.AoC2024;

public class Day05 : AoC<List<string>, int, int>
{
    private HashSet<(int, int)> _pageOrderingRules;
    private List<List<int>> _updates;


    public Day05(string dayName, string inputDirectory) : base(dayName, inputDirectory)
    {
        ParseInputData();
    }

    public Day05(string dayName) : base(dayName)
    {
        ParseInputData();
    }

    public Day05(string dayName, List<string> inputData) : base(dayName, inputData)
    {
        ParseInputData();
    }

    private void ParseInputData()
    {
        _pageOrderingRules = [];
        _updates = [];

        var pageOrderingSection = true;

        foreach (var line in InputData)
            if (string.IsNullOrWhiteSpace(line))
            {
                pageOrderingSection = false;
            }
            else if (pageOrderingSection)
            {
                var parts = line.Split('|');
                if (parts.Length == 2 && int.TryParse(parts[0], out var first) &&
                    int.TryParse(parts[1], out var second)) _pageOrderingRules.Add((first, second));
            }
            else
            {
                var parts = line.Split(',');
                var pageNumbers = new List<int>();
                foreach (var part in parts)
                    if (int.TryParse(part, out var num))
                        pageNumbers.Add(num);
                if (pageNumbers.Count > 0)
                    _updates.Add(pageNumbers);
            }
    }

    public override int CalculatePart1()
    {
        var pageOrderingNumber = 0;

        foreach (var update in _updates)
            if (IsCorrectOrder(update, _pageOrderingRules))
                pageOrderingNumber += GetMiddlePageNumber(update);

        return pageOrderingNumber;
    }

    private static bool IsCorrectOrder(IReadOnlyList<int> update, HashSet<(int, int)> pageOrderingRules)
    {
        for (var firstPage = 0; firstPage < update.Count - 1; firstPage++)
            for (var secondPage = firstPage + 1; secondPage < update.Count; secondPage++)
                if (!pageOrderingRules.Contains((update[firstPage], update[secondPage])))
                    return false;

        return true;
    }

    public override int CalculatePart2()
    {
        var correctedOrderingNumber = 0;

        foreach (var update in _updates)
            if (!IsCorrectOrder(update, _pageOrderingRules))
                correctedOrderingNumber += GetMiddlePageNumber(SortUpdateToCorrectOrder(update, _pageOrderingRules));

        return correctedOrderingNumber;
    }

    private static int GetMiddlePageNumber(IReadOnlyList<int> update)
    {
        return update[update.Count / 2];
    }

    private static IReadOnlyList<int> SortUpdateToCorrectOrder(IEnumerable<int> update,
        HashSet<(int, int)> pageOrderingRules)
    {
        var sortedUpdate = update.ToList();
        var swapped = false;

        do
        {
            swapped = false;
            for (var i = 0; i < sortedUpdate.Count - 1; i++)
                if (!pageOrderingRules.Contains((sortedUpdate[i], sortedUpdate[i + 1])) &&
                    pageOrderingRules.Contains((sortedUpdate[i + 1], sortedUpdate[i])))
                {
                    (sortedUpdate[i], sortedUpdate[i + 1]) = (sortedUpdate[i + 1], sortedUpdate[i]);
                    swapped = true;
                }
        } while (swapped);

        return sortedUpdate;
    }
}