using System;
using System.Collections.Generic;
using System.Drawing;
using AoC.AoC2022.Common;

namespace AoC.AoC2022
{
    internal class Day09 : AoC<List<string>, int, int>
    {
        private readonly List<Point> _positions;
        private Point _head;
        private Point _tail;

        public Day09(string dayName) : base(dayName)
        {
            _positions = new List<Point>();
            _head = new Point(0, 0);
            _tail = new Point(0, 0);
            _positions.Add(_tail);
        }

        public override int CalculatePart1()
        {
            foreach (var motion in InputData)
            {
                var command = motion.Split(" ");
                var direction = command[0];
                var steps = int.Parse(command[1]);

                MoveHead(direction, steps, ref _head, ref _tail);
            }

            return _positions.Count;
        }

        private void MoveHead(string direction, int steps, ref Point head, ref Point tail)
        {
            for (var i = 0; i < steps; i++)
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

                if (!PointsTouching(head, tail)) FollowHead(head, ref _tail, direction);

                if (!_positions.Contains(tail)) _positions.Add(tail);
            }
        }

        private static void FollowHead(Point head, ref Point tail, string direction)
        {
            if (head.Y == tail.Y && (direction == "R" || direction == "L"))
            {
                if (direction == "R") tail.X++;
                else tail.X--;
            }
            else if (head.X == tail.X && (direction == "U" || direction == "D"))
            {
                if (direction == "U") tail.Y++;
                else tail.Y--;
            }
            else if (head.X > tail.X && head.Y > tail.Y)
            {
                tail.X++;
                tail.Y++;
            }
            else if (head.X > tail.X && head.Y < tail.Y)
            {
                tail.X++;
                tail.Y--;
            }
            else if (head.X < tail.X && head.Y > tail.Y)
            {
                tail.X--;
                tail.Y++;
            }
            else if (head.X < tail.X && head.Y < tail.Y)
            {
                tail.X--;
                tail.Y--;
            }
        }

        private static bool PointsTouching(Point head, Point tail)
        {
            return (head.X == tail.X && Math.Abs(head.Y - tail.Y) <= 1) || (head.Y == tail.Y &&
                                                                            Math.Abs(head.X - tail.X) <= 1)
                                                                        || (head.X != tail.X && head.Y != tail.Y &&
                                                                            Math.Abs(head.X - tail.X) == 1 &&
                                                                            Math.Abs(head.Y - tail.Y) == 1);
        }

        public override int CalculatePart2()
        {
            return -2;
        }
    }
}