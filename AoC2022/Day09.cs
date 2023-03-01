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
            var positions = new List<Point>();
            var head = new Point(0, 0);
            var tail = new Point(0, 0);
            positions.Add(tail);

            foreach (var motion in InputData)
            {
                var command = motion.Split(" ");
                var direction = command[0];
                var steps = int.Parse(command[1]);

                for (var i = 0; i < steps; i++)
                {
                    MoveHead(direction, ref head);
                    if (!PointsTouching(head, tail)) FollowHead(head, ref tail, direction);
                    if (!positions.Contains(tail)) positions.Add(tail);
                }
            }

            return positions.Count;
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
            var tailPositions = new List<Point>();
            var knotsPositions = new List<Point>(new Point[10]);
            tailPositions.Add(knotsPositions[9]);

            foreach (var motion in InputData)
            {
                var command = motion.Split(" ");
                var direction = command[0];
                var steps = int.Parse(command[1]);

                for (var i = 0; i < steps; i++)
                {
                    var head = knotsPositions[0];
                    MoveHead(direction, ref head);
                    knotsPositions[0] = head;

                    for (var knot = 0; knot < 9; knot++)
                    {
                        var tmpHead = knotsPositions[knot];
                        var tmpTail = knotsPositions[knot + 1];
                        if (!PointsTouching(tmpHead, tmpTail))
                            FollowHead(tmpHead, ref tmpTail, direction);
                        knotsPositions[knot + 1] = tmpTail;
                    }

                    if (!tailPositions.Contains(knotsPositions[9]))
                        tailPositions.Add(knotsPositions[9]);
                }
            }

            return tailPositions.Count;
        }
    }
}