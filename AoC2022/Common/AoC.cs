using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AoC.AoC2022.Common
{
    public abstract class AoC<T, TResult1, TResult2> : IAoC<TResult1, TResult1> where T : new()
    {
        private readonly string _dayName;
        protected T InputData;
        protected Settings Settings;

        protected AoC(string dayName)
        {
            _dayName = dayName;

            // Build a config object, using env vars and JSON providers.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            // Get values from the config given their key and their target type.
            Settings = config.GetRequiredSection("Settings").Get<Settings>();

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
            var inputFile = $"{Settings.InputDataPath}{_dayName}.txt";
            var inputText = File.ReadAllLines(inputFile);
            return (T)Activator.CreateInstance(typeof(T), new object[] { inputText });
        }
    }
}