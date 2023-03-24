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
                }
            }
        }

        private static List<long> GetWorryLevels(IEnumerable<string> startingItems)
        {
            //foreach (var item in startingItems) worryLevels.Add(int.Parse(item));
            return startingItems.Select(long.Parse).ToList();
        }

        public override long CalculatePart1()
        {
            _monkeys.Clear();
            GetMonkeys();
            InspectAndThrowTheItems(20, 3);
            return LevelOfMonkeyBusiness();
        }

        private long LevelOfMonkeyBusiness()
        {
            var mostActiveMonkeys = _monkeys.OrderByDescending(i => i.Inspections).Take(2).ToList();
            return mostActiveMonkeys[0].Inspections * mostActiveMonkeys[1].Inspections;
        }

        private long GetCommonMultipleOfDivisibilityChecks()
        {
            return _monkeys.Aggregate(1, (current, monkey) => current * monkey.DivisibleByValue);
        }

        private void InspectAndThrowTheItems(int totalRounds, int worryLevelDivider)
        {
            for (var round = 0; round < totalRounds; round++)
                for (var monkeyId = 0; monkeyId < _monkeys.Count; monkeyId++)
                {
                    _monkeys[monkeyId].InspectTheItem(worryLevelDivider);

                    foreach (var worryLevel in _monkeys[monkeyId].WorryLevels)
                    {
                        var toMonkey = _monkeys[monkeyId].IdentifyThrowToMonkey(worryLevel);
                        _monkeys[toMonkey].AddWorryLevel(worryLevel);
                        _monkeys[monkeyId].Inspections++;
                    }

                    _monkeys[monkeyId].WorryLevels.Clear();
                }
        }

        private void InspectAndThrowTheItems(int totalRounds)
        {
            var commonMultiply = GetCommonMultipleOfDivisibilityChecks();

            for (var round = 0; round < totalRounds; round++)
                for (var i = 0; i < _monkeys.Count; i++)
                {
                    _monkeys[i].InspectTheItem(1);

                    foreach (var worryLevel in _monkeys[i].WorryLevels)
                    {
                        var newWorryLevel = worryLevel % commonMultiply;

                        var monkeyId = _monkeys[i].IdentifyThrowToMonkey(newWorryLevel);
                        _monkeys[monkeyId].AddWorryLevel(newWorryLevel);
                        _monkeys[i].Inspections++;
                    }

                    _monkeys[i].WorryLevels.Clear();
                }
        }

        public override long CalculatePart2()
        {
            _monkeys.Clear();
            GetMonkeys();

            InspectAndThrowTheItems(10000);

            return LevelOfMonkeyBusiness();
        }
    }

    internal class Monkey
    {
        public List<long> WorryLevels { get; }
        public long Inspections { get; set; }
        private readonly int _multiplyOperation;
        private readonly int _addOperation;
        public int DivisibleByValue { get; }
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