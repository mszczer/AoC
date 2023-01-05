using System;
using System.IO;

namespace AoC.AoC2022.Common
{
    public abstract class AoC<T, TResult1, TResult2> : IAoC<TResult1, TResult1> where T : new()
    {
        private readonly string _dayName;
        protected T InputData;

        protected AoC(string dayName)
        {
            _dayName = dayName;
            InputData = ParseInputFile();
        }

        public abstract TResult1 CalculatePart1();
        public abstract TResult1 CalculatePart2();

        public void PrintResults()
        {
            Console.WriteLine($"{_dayName} part1 answer: {CalculatePart1()}");
            Console.WriteLine($"{_dayName} part2 answer: {CalculatePart2()}");
        }

        private T ParseInputFile()
        {
            //var inputFile = ConfigurationManager.AppSettings.Get(GlobalConstants.InputDataPath) + $"{_dayName}.txt"; //ConfigurationManager.AppSettings maja z tym problem
            var inputFile = @$"InputData\{_dayName}.txt";
            var inputText = File.ReadAllLines(inputFile);
            return (T)Activator.CreateInstance(typeof(T), new object[] { inputText });
        }
    }
}