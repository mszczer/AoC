using System.Collections.Generic;
using System.Linq;
using AoC.AoC2022.Common;

namespace AoC.AoC2022
{
    internal class Day11 : AoC<List<string>, long, int>
    {
        private readonly List<Monkey> _monkeys = new List<Monkey>();

        private readonly List<long> _inspections = new List<long>(); 
            // is property in Monkey object better (to verify after part 2)?

        public Day11(string dayName) : base(dayName)
        {
            GetMonkeys();
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

                        _inspections.Add(0); // is property in Monkey object better (to verify after part 2)?

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
            InspectAndThrowTheItems(20);
            return LevelOfMonkeyBusiness();
        }

        private long LevelOfMonkeyBusiness()
        {
            var mostActiveInspections = _inspections.OrderByDescending(i => i).Take(2).ToList();
            return mostActiveInspections[0] * mostActiveInspections[1];
        }

        private void InspectAndThrowTheItems(int totalRounds)
        {
            for (var round = 0; round < totalRounds; round++)
                for (var i = 0; i < _monkeys.Count; i++)
                {
                    _monkeys[i].InspectTheItem();

                    foreach (var worryLevel in _monkeys[i].WorryLevels)
                    {
                        var monkeyId = _monkeys[i].IdentifyThrowToMonkey(worryLevel);
                        _monkeys[monkeyId].AddWorryLevel(worryLevel);

                        _inspections[i]++; // is property in Monkey object better (to verify after part 2)?
                    }

                    _monkeys[i].WorryLevels.Clear();
                }
        }

        public override int CalculatePart2()
        {
            return -2;
        }
    }

    internal class Monkey
    {
        public List<long> WorryLevels { get; }
        private readonly int _multiplyOperation;
        private readonly int _addOperation;
        private readonly int _divisibleByValue;
        private readonly int _throwToIfTrue;
        private readonly int _throwToIfFalse;

        public Monkey(List<long> worryLevels, int multiplyOperation, int addOperation,
            int divisibleByValue, int throwToIfTrue, int throwToIfFalse)
        {
            WorryLevels = worryLevels;
            _multiplyOperation = multiplyOperation;
            _addOperation = addOperation;
            _divisibleByValue = divisibleByValue;
            _throwToIfTrue = throwToIfTrue;
            _throwToIfFalse = throwToIfFalse;
        }

        public void InspectTheItem()
        {
            for (var i = 0; i < WorryLevels.Count; i++)
                WorryLevels[i] = ApplyOperation(WorryLevels[i]) / 3;
        }

        private long ApplyOperation(long old)
        {
            if (_multiplyOperation != 0) return old * _multiplyOperation;
            if (_addOperation != 0) return old + _addOperation;
            return old * old;
        }

        public int IdentifyThrowToMonkey(long oldItem)
        {
            return oldItem % _divisibleByValue == 0 ? _throwToIfTrue : _throwToIfFalse;
        }

        public void AddWorryLevel(long level)
        {
            WorryLevels.Add(level);
        }
    }
}