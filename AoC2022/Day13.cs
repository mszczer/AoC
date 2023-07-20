using System;
using System.Collections.Generic;
using AoC.AoC2022.Common;

namespace AoC.AoC2022
{
    internal class Day13 : AoC<List<string>, int, int>
    {
        private List<(object, object)> _packets;

        public Day13(string dayName) : base(dayName)
        {
            GetDistressSignal();
        }

        private void GetDistressSignal()
        {
            _packets = new List<(object, object)>();

            List<object> leftValue = null;
            List<object> rightValue = null;

            var lineIdx = 0;

            foreach (var pocket in InputData)
            {
                lineIdx++;
                var pocketIdx = lineIdx % 3;
                switch (pocketIdx)
                {
                    case 0:
                        break;
                    case 1:
                        leftValue = ParsePocket(pocket);
                        break;
                    case 2:
                        rightValue = ParsePocket(pocket);
                        _packets.Add((leftValue, rightValue));
                        break;
                    default:
                        throw new InvalidOperationException("Unexpected value in input file");
                }
            }
        }

        /*
         * Credits: https://www.honingjs.com/challenges/adventofcode/2022/day-13
         */
        public static List<object> ParsePocket(string inputValue)
        {
            var tmpStack = new Stack<List<object>>();
            var packet = new List<object>();
            tmpStack.Push(packet);

            for (var i = 0; i < inputValue.Length; i++)
            {
                var c = inputValue[i];

                if (c == '[')
                {
                    var newList = new List<object>();
                    packet.Add(newList);
                    tmpStack.Push(newList);
                    packet = newList;
                }
                else if (c == ']')
                {
                    tmpStack.Pop();
                    if (tmpStack.Count == 0)
                    {
                        Console.WriteLine("Invalid input: Unbalanced brackets");
                        return null;
                    }

                    packet = tmpStack.Peek();
                }
                else if (c == ',') // Skip comma
                {
                }
                else if (char.IsDigit(c))
                {
                    var startIndex = i;
                    while (i < inputValue.Length && char.IsDigit(inputValue[i]))
                        i++;

                    var numberString = inputValue.Substring(startIndex, i - startIndex);
                    if (!int.TryParse(numberString, out var number))
                    {
                        Console.WriteLine($"Invalid input: Failed to parse number: {numberString}");
                        return null;
                    }

                    packet.Add(number);
                    i--; // Move back one position since the outer loop will increment it again
                }
                else
                {
                    Console.WriteLine($"Invalid input: Unexpected character: {c}");
                    return null;
                }
            }

            if (tmpStack.Count > 1)
            {
                Console.WriteLine("Invalid input: Unbalanced brackets");
                return null;
            }

            return packet;
        }


        public override int CalculatePart1()
        {
            var sumOfIndices = 0;

            for (var idx = 0; idx < _packets.Count; idx++)
                if (ComparePockets(_packets[idx].Item1, _packets[idx].Item2) == 1)
                    sumOfIndices += idx + 1;

            return sumOfIndices;
        }

        private static int ComparePockets(object leftValue, object rightValue)
        {
            if (leftValue is int leftInt && rightValue is int rightInt)
            {
                if (leftInt < rightInt) return 1;
                if (leftInt > rightInt) return -1;
            }
            else if (leftValue is List<object> list && rightValue is List<object> value)
            {
                for (var idx = 0; idx < list.Count; idx++)
                {
                    if (idx >= value.Count) return -1;
                    var result = ComparePockets(list[idx], value[idx]);
                    if (result != 0) return result;
                }

                if (list.Count < value.Count) return 1;
            }
            else
            {
                var leftList = leftValue is int ? new List<object> { leftValue } : leftValue as List<object>;
                var rightList = rightValue is int ? new List<object> { rightValue } : rightValue as List<object>;

                return ComparePockets(leftList, rightList);
            }

            return 0;
        }

        public override int CalculatePart2()
        {
            return 0;
        }
    }
}