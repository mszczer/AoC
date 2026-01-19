namespace AoC.AoC2022;

internal class Day03(string dayName) : AoC<List<string>, int, int>(dayName)
{
    public override int CalculatePart1()
    {
        if (InputData == null || InputData.Count == 0) return 0;
        return CalculatePrioritiesTotalSum();
    }

    private int CalculatePrioritiesTotalSum()
    {
        var allDuplicates = new List<char>();

        foreach (var rawRucksack in InputData)
        {
            if (string.IsNullOrEmpty(rawRucksack)) continue;
            var rucksack = rawRucksack.Trim();
            allDuplicates.AddRange(FindDuplicatesPerRucksack(rucksack));
        }

        return CalculatePrioritiesTotal(allDuplicates);
    }

    private static int CalculatePrioritiesTotal(IEnumerable<char> allDuplicates)
    {
        var sum = 0;

        foreach (var element in allDuplicates)
            sum += element < 'a' ? element - 38 : element - 96;

        return sum;
    }

    private static IEnumerable<char> FindDuplicatesPerRucksack(string rucksack)
    {
        if (string.IsNullOrEmpty(rucksack)) yield break;

        var compartmentLimit = rucksack.Length / 2;
        var firstCompartment = rucksack[..compartmentLimit];
        var secondCompartment = rucksack[compartmentLimit..]; // remainder if odd length

        var firstSet = new HashSet<char>(firstCompartment);
        var yielded = new HashSet<char>();

        foreach (var c in secondCompartment)
            if (firstSet.Contains(c) && yielded.Add(c))
                yield return c;
    }

    public override int CalculatePart2()
    {
        if (InputData == null || InputData.Count < 3) return 0;
        return CalculatePrioritiesTotal(FindBadges());
    }

    private IEnumerable<char> FindBadges()
    {
        if (InputData == null) yield break;

        for (var i = 0; i + 2 < InputData.Count; i += 3)
        {
            var s0 = InputData[i] ?? string.Empty;
            var s1 = InputData[i + 1] ?? string.Empty;
            var s2 = InputData[i + 2] ?? string.Empty;

            var set0 = new HashSet<char>(s0);
            var set1 = new HashSet<char>(s1);

            foreach (var c in s2)
                if (set0.Contains(c) && set1.Contains(c))
                {
                    yield return c;
                    break; // only first common char per group
                }
        }
    }
}