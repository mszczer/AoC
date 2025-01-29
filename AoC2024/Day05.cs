using AoC.Common;

namespace AoC.AoC2024;

public class Day05 : AoC<List<string>, int, int>
{
    private List<(int, int)> _pageOrderingRules;
    private List<List<int>> _updates;


    public Day05(string dayName, string inputDirectory) : base(dayName, inputDirectory)
    {
        InitializeUpdatesData();
    }

    public Day05(string dayName) : base(dayName)
    {
        InitializeUpdatesData();
    }

    private void InitializeUpdatesData()
    {
        _pageOrderingRules = new List<(int, int)>();
        _updates = new List<List<int>>();

        var pageOrderingSection = true;

        foreach (var line in InputData)
            if (line == "")
            {
                pageOrderingSection = false;
            }
            else if (pageOrderingSection)
            {
                var parts = line.Split('|');
                var firstPage = int.Parse(parts[0]);
                var secondPage = int.Parse(parts[1]);

                _pageOrderingRules.Add((firstPage, secondPage));
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
            if (IsRightOrder(update, _pageOrderingRules))
                pageOrderingNumber += getMiddlePageNumber(update);

        return pageOrderingNumber;
    }

    private static bool IsRightOrder(IReadOnlyList<int> update, ICollection<(int, int)> pageOrderingRules)
    {
        for (var firstPage = 0; firstPage < update.Count - 1; firstPage++)
            for (var secondPage = firstPage + 1; secondPage < update.Count; secondPage++)
                if (pageOrderingRules.Contains((update[firstPage], update[secondPage])))
                    continue;
                else
                    return false;

        return true;
    }

    private static int getMiddlePageNumber(IReadOnlyList<int> update)
    {
        return update[update.Count / 2];
    }


    public override int CalculatePart2()
    {
        return int.MaxValue;
    }
}