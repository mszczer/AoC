using AoC.Common;

namespace AoC.AoC2024;

public class Day05 : AoC<List<string>, int, int>
{
    private List<(int, int)> _pageOrderingRules;
    private List<List<int>> _updates;


    public Day05(string dayName, string inputDirectory) : base(dayName, inputDirectory)
    {
        ParseInputData();
    }

    public Day05(string dayName) : base(dayName)
    {
        ParseInputData();
    }

    private void ParseInputData()
    {
        _pageOrderingRules = new List<(int, int)>();
        _updates = new List<List<int>>();

        var pageOrderingSection = true;

        foreach (var line in InputData)
            if (string.IsNullOrWhiteSpace(line))
            {
                pageOrderingSection = false;
            }
            else if (pageOrderingSection)
            {
                var parts = line.Split('|');

                _pageOrderingRules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
            }
            else
            {
                var parts = line.Split(',');
                var pageNumbers = new List<int>();

                foreach (var part in parts)
                    pageNumbers.Add(int.Parse(part));

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

    private static bool IsCorrectOrder(IReadOnlyList<int> update, ICollection<(int, int)> pageOrderingRules)
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
            {
                var updateSorted = SortUpdateToCorrectOrder(update, _pageOrderingRules);

                correctedOrderingNumber += GetMiddlePageNumber(updateSorted);
            }

        return correctedOrderingNumber;
    }

    private static int GetMiddlePageNumber(IReadOnlyList<int> update)
    {
        return update[update.Count / 2];
    }

    private static IReadOnlyList<int> SortUpdateToCorrectOrder(IEnumerable<int> update,
        ICollection<(int, int)> pageOrderingRules)
    {
        var updateSorted = update.ToList();

        for (var firstPage = 0; firstPage < updateSorted.Count - 1; firstPage++)
            for (var secondPage = firstPage + 1; secondPage < updateSorted.Count; secondPage++)
                if (!pageOrderingRules.Contains((updateSorted[firstPage], updateSorted[secondPage])))
                    if (pageOrderingRules.Contains((updateSorted[secondPage], updateSorted[firstPage])))
                    {
                        (updateSorted[firstPage], updateSorted[secondPage]) =
                            (updateSorted[secondPage], updateSorted[firstPage]);

                        firstPage = 0;
                        secondPage = 0;
                    }

        return updateSorted;
    }
}