using System.Collections.Generic;
using System.IO;

namespace AoC.AoC2022.Common
{
    public class FileHelper
    {
        public List<string> GetLinesFromFile(string filePath)
        {
            var rows = new List<string>();
            using var r = new StreamReader(filePath);
            // ReSharper disable once MoveVariableDeclarationInsideLoopCondition
            string line;
            while ((line = r.ReadLine()) != null)
                rows.Add(line);
            return rows;
        }
    }
}