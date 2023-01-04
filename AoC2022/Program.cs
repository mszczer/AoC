using System;
using System.Configuration;
using AoC.AoC2022.Common;

namespace AoC.AoC2022
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var inputFilePath = ConfigurationManager.AppSettings.Get(GlobalConstants.InputDataPath) + "Day01.txt";
            var fileHelper = new FileHelper();

            Console.ReadKey();
        }
    }
}