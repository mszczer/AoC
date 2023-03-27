using System;
using System.Collections.Generic;
using System.Linq;
using AoC.AoC2022.Common;

namespace AoC.AoC2022
{
    internal class Day11 : AoC<List<string>, long, long>
    {
        private readonly List<Monkey> _monkeys = new List<Monkey>();

        public Day11(string dayName) : base(dayName)
        {
        }

        public override long CalculatePart1()
        {
            _monkeys.Clear();
            GetMonkeys();
            InspectAndThrowTheItems(20, 3);
            return LevelOfMonkeyBusiness();
        }

        public override long CalculatePart2()
        {
            _monkeys.Clear();
            GetMonkeys();
            InspectAndThrowTheItems(10000, 1);
            return LevelOfMonkeyBusiness();
        }

        private void GetMonkeys()
        {
            var worryLevels = new List<long>();
            var multiplyOperation = 0;
            var addOperation = 0;
            var divisibleByValue = 0;
            var throwToIfTrue = 0;
            var throwToIfFalse = 0;
            var inputCount = 0;

            foreach (var line in InputData)
            {
                inputCount++;
                var monkeyDetail = inputCount % 7;

                switch (monkeyDetail)
                {
                    case 0:
                    case 1:
                        break;
                    case 2:
                    {
                        var startingItems = line[18..].Split(',');
                        worryLevels = GetWorryLevels(startingItems);
                        break;
                    }
                    case 3:
                    {
                        var parts = line.Split(" ");
                        multiplyOperation = parts[6] == "*" && parts[7] != "old" ? int.Parse(parts[7]) : 0;
                        addOperation = parts[6] == "+" && parts[7] != "old" ? int.Parse(parts[7]) : 0;
                        break;
                    }
                    case 4:
                        divisibleByValue = int.Parse(line[21..]);
                        break;
                    case 5:
                        throwToIfTrue = int.Parse(line[29..]);
                        break;
                    case 6:
                    {
                        throwToIfFalse = int.Parse(line[30..]);

                        var tmpMonkey = new Monkey(new List<long>(worryLevels), multiplyOperation, addOperation,
                            divisibleByValue, throwToIfTrue, throwToIfFalse);
                        _monkeys.Add(tmpMonkey);

                        worryLevels.Clear();
                        multiplyOperation = 0;
                        addOperation = 0;
                        break;
                    }
                    default:
                        throw new InvalidOperationException("Unexpected value in input file");

                }
            }
        }

        private static List<long> GetWorryLevels(IEnumerable<string> startingItems)
        {
            //foreach (var item in startingItems) worryLevels.Add(int.Parse(item));
            return startingItems.Select(long.Parse).ToList();
        }

        private long LevelOfMonkeyBusiness()
        {
            var mostActiveMonkeys = _monkeys.OrderByDescending(i => i.Inspections).Take(2).ToList();
            return mostActiveMonkeys[0].Inspections * mostActiveMonkeys[1].Inspections;
        }

        /* Common multiple of all divisibility checks:
         * Take the modulo of the new worry level with that common multiple before throwing the item.
         * This will not affect any future divisibility checks.
         */
        private long GetCommonMultipleOfDivisibilityChecks()
        {
            return _monkeys.Aggregate(1, (current, monkey) => current * monkey.DivisibleByValue);
        }

        /*
         * It doesn’t matter if we first apply operation multiply and then take the modulo (Part 1)
         * or take the modulo and then apply operation (Part 2).
         */
        private void InspectAndThrowTheItems(int totalRounds, int worryLevelDivider)
        {
            var commonMultiply = GetCommonMultipleOfDivisibilityChecks();

            for (var round = 0; round < totalRounds; round++)
                foreach (var monkey in _monkeys)
                {
                    monkey.InspectTheItem(worryLevelDivider);

                    foreach (var worryLevel in monkey.WorryLevels)
                    {
                        var newWorryLevel = worryLevel % commonMultiply;

                        var toMonkey = monkey.IdentifyThrowToMonkey(newWorryLevel);
                        _monkeys[toMonkey].AddWorryLevel(newWorryLevel);
                        monkey.Inspections++;
                    }

                    monkey.WorryLevels.Clear();
                }
        }
    }

    internal class Monkey
    {
        public List<long> WorryLevels { get; }
        public long Inspections { get; set; }
        public int DivisibleByValue { get; }
        private readonly int _multiplyOperation;
        private readonly int _addOperation;
        private readonly int _trueRecipient;
        private readonly int _falseRecipient;

        public Monkey(List<long> worryLevels, int multiplyOperation, int addOperation,
            int divisibleByValue, int trueRecipient, int falseRecipient)
        {
            WorryLevels = worryLevels;
            _multiplyOperation = multiplyOperation;
            _addOperation = addOperation;
            DivisibleByValue = divisibleByValue;
            _trueRecipient = trueRecipient;
            _falseRecipient = falseRecipient;
            Inspections = 0;
        }

        public void InspectTheItem(int worryLevelDivider)
        {
            for (var i = 0; i < WorryLevels.Count; i++)
                WorryLevels[i] = ApplyOperation(WorryLevels[i]) / worryLevelDivider;
        }

        public long ApplyOperation(long old)
        {
            if (_multiplyOperation != 0) return old * _multiplyOperation;
            if (_addOperation != 0) return old + _addOperation;
            return old * old;
        }

        public int IdentifyThrowToMonkey(long item)
        {
            return item % DivisibleByValue == 0 ? _trueRecipient : _falseRecipient;
        }

        public void AddWorryLevel(long level)
        {
            WorryLevels.Add(level);
        }
    }
}