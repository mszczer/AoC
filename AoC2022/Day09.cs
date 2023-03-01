using System;
using System.Collections.Generic;
using System.Drawing;
using AoC.AoC2022.Common;

namespace AoC.AoC2022
{
    internal class Day09 : AoC<List<string>, int, int>
    {
        public Day09(string dayName) : base(dayName)
        {
        }

        public override int CalculatePart1()
        {
            return CountTailPositions(InputData, 2);
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
            }
        }

        private static void FollowHead(Point head, ref Point tail, string direction)
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
            return CountTailPositions(InputData, 10);
        }

        private static int CountTailPositions(List<string> instructions, int numberOfKnots)
        {
            var tailPositions = new List<Point>();
            var knotsPositions = new List<Point>(new Point[numberOfKnots]);
            tailPositions.Add(knotsPositions[numberOfKnots - 1]);

            foreach (var motion in instructions)
            {
                var command = motion.Split(" ");
                var direction = command[0];
                var steps = int.Parse(command[1]);

                for (var i = 0; i < steps; i++)
                {
                    var head = knotsPositions[0];
                    MoveHead(direction, ref head);
                    knotsPositions[0] = head;

                    for (var knot = 0; knot < numberOfKnots - 1; knot++)
                    {
                        var tmpHead = knotsPositions[knot];
                        var tmpTail = knotsPositions[knot + 1];
                        if (!PointsTouching(tmpHead, tmpTail))
                            FollowHead(tmpHead, ref tmpTail, direction);
                        knotsPositions[knot + 1] = tmpTail;
                    }

                    if (!tailPositions.Contains(knotsPositions[numberOfKnots - 1]))
                        tailPositions.Add(knotsPositions[numberOfKnots - 1]);
                }
            }

            return tailPositions.Count;
        }
    }
}