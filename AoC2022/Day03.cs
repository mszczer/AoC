using System.Collections.Generic;
using AoC.AoC2022.Common;

namespace AoC.AoC2022
{
    internal class Day03 : AoC<List<string>, int, int>
    {
        public Day03(string dayName) : base(dayName)
        {
        }

        public override int CalculatePart1()
        {
            return CalculatePrioritiesTotalSum();
        }

        private int CalculatePrioritiesTotalSum()
        {
            var allDuplicates = new List<char>();

            foreach (var rucksack in InputData)
                allDuplicates.AddRange(FindDuplicatesPerRucksack(rucksack));

            return CalculatePrioritiesTotal(allDuplicates);
        }

        private static int CalculatePrioritiesTotal(List<char> allDuplicates)
        {
            var sum = 0;

            foreach (var element in allDuplicates)
                sum += element < 97 ? element - 38 : element - 96;

            return sum;
        }

        private static IEnumerable<char> FindDuplicatesPerRucksack(string rucksack)
        {
            var duplicatesPerRucksack = new List<char>();

            var compartmentLimit = rucksack.Length / 2;
            var firstCompartment = rucksack.Substring(0, compartmentLimit);
            var secondCompartment = rucksack.Substring(compartmentLimit, compartmentLimit);

            foreach (var c1 in firstCompartment)
            foreach (var c2 in secondCompartment)
                if (c1 == c2 && !duplicatesPerRucksack.Contains(c1))
                    duplicatesPerRucksack.Add(c1);

            return duplicatesPerRucksack;
        }

        public override int CalculatePart2()
        {
            return CalculatePrioritiesTotal(FindBadges());
        }

        private List<char> FindBadges()
        {
            var badges = new List<char>();

            for (var i = 0; i < InputData.Count; i++)
                if ((i + 1) % 3 == 0)
                    foreach (var c in InputData[i])
                        if (InputData[i - 1].Contains(c) && InputData[i - 2].Contains(c))
                        {
                            badges.Add(c);
                            break;
                        }

            return badges;
        }
    }
}