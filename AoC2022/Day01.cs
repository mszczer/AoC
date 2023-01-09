using AoC.AoC2022.Common;
using System.Collections.Generic;

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
            caloriesList.Sort();
            caloriesList.Reverse();
            var sumTopThree = 0;
            for (var i = 0; i < 3; i++)
                sumTopThree += caloriesList[i];

            return sumTopThree;
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

        private List<int> CalculateCaloriesPerElf()
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