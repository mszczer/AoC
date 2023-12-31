﻿using System;

namespace AoC.AoC2022
{
    internal class Day10 : AoC<List<string>, int, string>
    {
        private List<int[]> _program;
        private List<int> _registerValues;

        public Day10(string dayName) : base(dayName)
        {
            GetInstructions();
            GetRegisterValuesAfterEachCycle();
        }

        private void GetRegisterValuesAfterEachCycle()
        {
            var register = 1;
            _registerValues = new List<int> { register };

            foreach (var instruction in _program)
            {
                _registerValues.Add(register);
                if (instruction[0] == 2)
                {
                    register += instruction[1];
                    _registerValues.Add(register);
                }
            }
        }

        private void GetInstructions()
        {
            _program = new List<int[]>();
            foreach (var line in InputData)
            {
                var instruction = line[..4];
                var cycleLength = instruction == "noop" ? 1 : 2;
                var value = instruction == "addx" ? int.Parse(line[5..]) : 0;
                _program.Add(new[] { cycleLength, value });
            }
        }

        public override int CalculatePart1()
        {
            var sumOfSignalStrengths = 0;

            for (var i = 1; i <= _registerValues.Count; i++)
                if (i % 40 == 20)
                    sumOfSignalStrengths += i * _registerValues[i - 1];

            return sumOfSignalStrengths;
        }

        public override string CalculatePart2()
        {
            PrintImage();
            return "FGCUZREC";
        }

        private void PrintImage()
        {
            const int crtWidth = 40;

            for (var i = 0; i < _registerValues.Count; i++)
            {
                var column = i % crtWidth;
                if (column == 0) Console.WriteLine();
                Console.Write(GetPixel(column, _registerValues[i]));
            }
        }

        private static string GetPixel(int column, int registerValue)
        {
            return Math.Abs(column - registerValue) <= 1 ? "#" : ".";
        }
    }
}