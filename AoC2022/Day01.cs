using System.Linq;

namespace AoC.AoC2022
{
    internal class Day01 : AoC<List<string>, int, int>
    {
        public Day01(string dayName) : base(dayName)
        {
        }

        public override int CalculatePart1()
        {
            return CalculateMaxCaloriesCarried();
        }

        public override int CalculatePart2()
        {
            var caloriesList = CalculateCaloriesPerElf();
            return caloriesList.OrderByDescending(i => i).Take(3).Sum();
        }

        private int CalculateMaxCaloriesCarried()
        {
            var maxCalories = 0;
            var elfCalories = 0;

            foreach (var calorie in InputData)
                if (calorie != "")
                {
                    int.TryParse(calorie, out var num);
                    elfCalories += num;

                    if (elfCalories > maxCalories) maxCalories = elfCalories;
                }
                else
                {
                    elfCalories = 0;
                }

            return maxCalories;
        }

        private IEnumerable<int> CalculateCaloriesPerElf()
        {
            var caloriesCarried = new List<int>();
            var elfCalories = 0;

            foreach (var calorie in InputData)
                if (calorie != "")
                {
                    int.TryParse(calorie, out var num);
                    elfCalories += num;
                }
                else
                {
                    caloriesCarried.Add(elfCalories);
                    elfCalories = 0;
                }

            caloriesCarried.Add(elfCalories);

            return caloriesCarried;
        }
    }
}