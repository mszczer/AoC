using System;
using System.Drawing;

namespace AoC.AoC2022
{
    internal class Day09 : AoC<List<string>, int, int>
    {
        private List<Tuple<string, int>> _commands;

        public Day09(string dayName) : base(dayName)
        {
            GetCommands();
        }

        private void GetCommands()
        {
            _commands = new List<Tuple<string, int>>();
            foreach (var motion in InputData)
            {
                var command = motion.Split(" ");
                var direction = command[0];
                var steps = int.Parse(command[1]);
                _commands.Add(new Tuple<string, int>(direction, steps));
            }
        }

        public override int CalculatePart1()
        {
            return CountTailPositions(_commands, 2);
        }

        private static int CountTailPositions(List<Tuple<string, int>> instructions, int numberOfKnots)
        {
            var tailPositions = new List<Point>();
            var knotsPositions = new List<Point>(new Point[numberOfKnots]);
            tailPositions.Add(knotsPositions[numberOfKnots - 1]);

            foreach (var motion in instructions)
                for (var i = 0; i < motion.Item2; i++)
                {
                    var head = knotsPositions[0];
                    MoveHead(motion.Item1, ref head);
                    knotsPositions[0] = head;

                    for (var knot = 0; knot < numberOfKnots - 1; knot++)
                    {
                        var tmpHead = knotsPositions[knot];
                        var tmpTail = knotsPositions[knot + 1];
                        if (!PointsTouching(tmpHead, tmpTail))
                            FollowHead(tmpHead, ref tmpTail);
                        knotsPositions[knot + 1] = tmpTail;
                    }

                    if (!tailPositions.Contains(knotsPositions[numberOfKnots - 1]))
                        tailPositions.Add(knotsPositions[numberOfKnots - 1]);
                }

            return tailPositions.Count;
        }

        private static void MoveHead(string direction, ref Point head)
        {
            switch (direction)
            {
                case "R":
                    head.X++;
                    break;
                case "U":
                    head.Y++;
                    break;
                case "L":
                    head.X--;
                    break;
                case "D":
                    head.Y--;
                    break;
                default:
                    throw new InvalidOperationException("Unexpected value 'direction' = " + direction);
            }
        }

        private static void FollowHead(Point head, ref Point tail)
        {
            if (head.X > tail.X) tail.X++;
            if (head.X < tail.X) tail.X--;

            if (head.Y > tail.Y) tail.Y++;
            if (head.Y < tail.Y) tail.Y--;
        }

        private static bool PointsTouching(Point head, Point tail)
        {
            return Math.Abs(head.Y - tail.Y) <= 1 && Math.Abs(head.X - tail.X) <= 1;
        }

        public override int CalculatePart2()
        {
            return CountTailPositions(_commands, 10);
        }
    }
}