using System.Collections;
using System.Linq;
using System.Text;

namespace AoC.AoC2022
{
    internal class Day05 : AoC<List<string>, string, string>
    {
        private List<int[]> _rearrangement;
        private int _stacksNumber;
        private int _stacksIdRowNumber;


        public Day05(string dayName) : base(dayName)
        {
            IdentifyStackNumbers();
            GetRearrangementProcedure();
        }

        private void IdentifyStackNumbers()
        {
            foreach (var line in InputData)
            {
                _stacksIdRowNumber++;
                if (line.StartsWith(" 1"))
                {
                    _stacksNumber = int.Parse(line.Substring(line.Length - 2, 1));
                    break;
                }
            }
        }

        private Stack[] GetInitialStacks()
        {
            var stacks = new Stack[_stacksNumber];
            for (var i = 0; i < _stacksNumber; i++)
                stacks[i] = new Stack(); // Stack represents a last-in, first out collection of object

            for (var lvl = _stacksIdRowNumber - 2; lvl >= 0; lvl--)
            {
                var stackNumber = 0;

                for (var idx = 1; idx <= _stacksNumber * 4; idx++)
                    if (idx % 4 == 0)
                    {
                        if (InputData[lvl][idx - 3] != ' ') stacks[stackNumber].Push(InputData[lvl][idx - 3]);
                        stackNumber++;
                    }
            }

            return stacks;
        }

        private void GetRearrangementProcedure()
        {
            _rearrangement = new List<int[]>();

            foreach (var line in InputData)
                if (line.StartsWith("move"))
                {
                    var parts = line.Split(' ');
                    _rearrangement.Add(new[] { int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[5]) });
                }
        }

        private int GetStacksNumber()
        {
            /*
             * foreach (var line in InputData)
             *  if (line.StartsWith(" 1"))
             *      return int.Parse(line.Substring(line.Length - 2, 1));
             *  return 0;
             */
            return (from line in InputData
                where line.StartsWith(" 1")
                select int.Parse(line.Substring(line.Length - 2, 1))).FirstOrDefault();
        }

        public override string CalculatePart1()
        {
            var stacks = GetInitialStacks();

            foreach (var step in _rearrangement)
                MoveCrates(stacks, step[0], step[1], step[2]);

            return GetAllTops(stacks);
        }

        private static string GetAllTops(IEnumerable<Stack> stacks)
        {
            var sb = new StringBuilder();
            foreach (var st in stacks)
                sb.Append(st.Peek());

            return sb.ToString();
        }

        private static void MoveCrates(IReadOnlyList<Stack> stacks, int quantity, int source, int target)
        {
            for (var i = 0; i < quantity; i++)
                stacks[target - 1].Push(stacks[source - 1].Pop());
        }

        public override string CalculatePart2()
        {
            var stacks = GetInitialStacks();

            foreach (var step in _rearrangement)
                MoveMultipleCrates(stacks, step[0], step[1], step[2]);

            return GetAllTops(stacks);
        }

        private static void MoveMultipleCrates(IReadOnlyList<Stack> stacks, int quantity, int source, int target)
        {
            var tempStack = new Stack();

            for (var i = 0; i < quantity; i++)
                tempStack.Push(stacks[source - 1].Pop());

            for (var i = 0; i < quantity; i++)
                stacks[target - 1].Push(tempStack.Pop());
        }
    }
}