using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AoC.AoC2022.Common
{
    public class FileHelper
    {
        public List<string> GetLinesFromFile(string filePath)
        {
            var rows = new List<string>();
            using var r = new StreamReader(filePath);
            string line;
            while ((line = r.ReadLine()) != null)
                rows.Add(line);
            return rows;
        }
    }
}


/*
 *
    public static List<string> GetLinesFromFile(this string fileLocation)
        {
            var lines = new List<string>();
            using var r = new StreamReader(fileLocation);
            string line;
            while ((line = r.ReadLine()) != null)
                lines.Add(line);

            return lines;
        }

        public static List<int> GetNumbersFromFile(this string fileLocation)
        {
            var numbers = new List<int>();
            using var r = new StreamReader(fileLocation);
            string line;
            while ((line = r.ReadLine()) != null)
                if (int.TryParse(line, out var num))
                    numbers.Add(num);

            return numbers;
        }

        public static List<int> GetNumbersFromLine(this string textLine, string separator)
        {
            var numbers =
                (from stringNumber in textLine.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).ToList()
                 select int.Parse(stringNumber)).ToList();

            return numbers;
        }

        public static string[] GetWords(this string textLine, string[] separatingStrings)
        {
            return textLine.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
        }
 
 */