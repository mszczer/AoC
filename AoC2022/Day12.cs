using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Security.Policy;
using AoC.AoC2022.Common;

namespace AoC.AoC2022
{
    internal class Day12 : AoC<List<string>, int, int>
    {
        private int[,] _heightsMap;
        private Point _startPoint;
        private Point _endPoint;

        public Day12(string dayName) : base(dayName)
        {
            GetHeightsMap();
        }

        private void GetHeightsMap()
        {
            var numberOfRows = InputData.Count;
            var numberOfColumns = InputData[0].Length;

            _heightsMap = new int[numberOfRows, numberOfColumns];
            for (var row = 0; row < numberOfRows; row++)
                for (var column = 0; column < numberOfColumns; column++)
                    if (InputData[row][column] == 'S')
                    {
                        _startPoint = new Point(row, column);
                        _heightsMap[row, column] = 0;
                    }
                    else if (InputData[row][column] == 'E')
                    {
                        _endPoint = new Point(row, column);
                        _heightsMap[row, column] = 26;
                    }
                    else
                    {
                        _heightsMap[row, column] = InputData[row][column] - 'a';
                    }
        }

        public override int CalculatePart1()
        {
            return GetDistanceToEnd(_heightsMap, _startPoint, _endPoint);
        }

        /// <summary>
        /// 1. Start at the end point and change its path distance to 0;
        /// 2. Add the end point to a 'todo' queue of points to process.
        /// 3. Get the point and distance off the front of the queue; find its immediate neighboring points that could flow into it.
        /// 4. If the neighboring point has not been visited before (the height is maximum available), add its location and current distance +1 to the back of the todo queue.
        /// 5. Repeat from step 2 until we reach the start point.
        /// </summary>
        private static int GetDistanceToEnd(int[,] heightsMap, Point startPoint, Point endPoint)
        {
            const int maxDist = int.MaxValue;
            var distances =
                ArrayHandler.GetTwoDimensionalArray(heightsMap.GetLength(0), heightsMap.GetLength(1), maxDist);
            distances[endPoint.X, endPoint.Y] = 0;

            var pointsToCheck = new Queue<Point>();
            pointsToCheck.Enqueue(endPoint);

            while (pointsToCheck.Count > 0)
            {
                var currentHill = pointsToCheck.Dequeue();

                var neighborsWhichCanVisit =
                    GetNeighborsWhichCanVisitCurrentHill(currentHill.X, currentHill.Y, heightsMap);

                foreach (var neighbor in neighborsWhichCanVisit)
                    if (distances[neighbor.X, neighbor.Y] == maxDist)
                    {
                        distances[neighbor.X, neighbor.Y] = distances[currentHill.X, currentHill.Y] + 1;
                        pointsToCheck.Enqueue(new Point(neighbor.X, neighbor.Y));
                    }

                if (currentHill == startPoint)
                    return distances[startPoint.X, startPoint.Y];
            }

            return maxDist;
        }

        private static List<Point> GetNeighborsWhichCanVisitCurrentHill(int row, int column, int[,] heightsMap)
        {
            var neighborsWhichCanVisit = new List<Point>();
            var currentHeight = heightsMap[row, column];
            var rowsNumber = heightsMap.GetLength(0);
            var columnsNumber = heightsMap.GetLength(1);

            if (row > 0 && currentHeight <= heightsMap[row - 1, column] + 1) // Can be reached from the top?
                neighborsWhichCanVisit.Add(new Point(row - 1, column));
            if (column < columnsNumber - 1 &&
                currentHeight <= heightsMap[row, column + 1] + 1) // Can be reached from the right?
                neighborsWhichCanVisit.Add(new Point(row, column + 1));
            if (row < rowsNumber - 1 &&
                currentHeight <= heightsMap[row + 1, column] + 1) // Can be reached from the bottom?
                neighborsWhichCanVisit.Add(new Point(row + 1, column));
            if (column > 0 && currentHeight <= heightsMap[row, column - 1] + 1) // Can be reached from the left?
                neighborsWhichCanVisit.Add(new Point(row, column - 1));

            return neighborsWhichCanVisit;
        }

        public override int CalculatePart2()
        {
            var lowestPoints = new List<Point>();

            var row = _heightsMap.GetLength(0);
            var col = _heightsMap.GetLength(1);

            for(var r=0;r<row;r++)
                for (var c=0;c<col;c++)
                    if (_heightsMap[r,c]==0)
                        lowestPoints.Add(new Point(r,c));
            
            var theFewestSteps = Int32.MaxValue;
            foreach (var pointToCheck in lowestPoints)
            {
                var distance = GetDistanceToEnd(_heightsMap, pointToCheck, _endPoint);
                if(distance < theFewestSteps) theFewestSteps = distance;
            }

            return theFewestSteps;
            
        }
    }
}

/*
 * ToDo: Dijkstra, A* or other generalized graph searching algorithms:
 * https://www.codeproject.com/Articles/1221034/Pathfinding-Algorithms-in-Csharp
 */