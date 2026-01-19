namespace AoC.AoC2022;

internal class Day01(string dayName) : AoC<List<string>, int, int>(dayName)
{
    public override int CalculatePart1()
    {
        return CalculateMaxCaloriesCarried();
    }

    public override int CalculatePart2()
    {
        var caloriesList = CalculateCaloriesPerElf().ToList();
        return caloriesList.OrderByDescending(i => i).Take(3).Sum();
    }

    private int CalculateMaxCaloriesCarried()
    {
        if (InputData == null || InputData.Count == 0) return 0;

        var maxCalories = 0;
        var elfCalories = 0;

        foreach (var calorie in InputData)
            if (!string.IsNullOrEmpty(calorie))
            {
                if (int.TryParse(calorie, out var num))
                    elfCalories += num;

                if (elfCalories > maxCalories) maxCalories = elfCalories;
            }
            else
            {
                elfCalories = 0;
            }

        // Ensure last elf group considered (already handled during accumulation but keep defensively)
        maxCalories = Math.Max(maxCalories, elfCalories);

        return maxCalories;
    }

    private IEnumerable<int> CalculateCaloriesPerElf()
    {
        var caloriesCarried = new List<int>();

        if (InputData == null || InputData.Count == 0) return caloriesCarried;

        var elfCalories = 0;

        foreach (var calorie in InputData)
            if (!string.IsNullOrEmpty(calorie))
            {
                if (int.TryParse(calorie, out var num))
                    elfCalories += num;
            }
            else
            {
                caloriesCarried.Add(elfCalories);
                elfCalories = 0;
            }

        // Add final group
        caloriesCarried.Add(elfCalories);

        return caloriesCarried;
    }
}