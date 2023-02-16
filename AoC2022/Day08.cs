using System.Collections.Generic;
using AoC.AoC2022.Common;

namespace AoC.AoC2022
{
    internal class Day08 : AoC<List<string>, int, int>
    {
        private int[,] _grid;

        public Day08(string dayName) : base(dayName)
        {
            SetGrid();
        }

        private void SetGrid()
        {
            var numberOfRows = InputData.Count;
            var numberOfColumns = InputData[0].Length;

            _grid = new int[numberOfRows, numberOfColumns];

            for (var row = 0; row < numberOfRows; row++)
            for (var column = 0; column < numberOfColumns; column++)
                _grid[row, column] = int.Parse(InputData[row][column].ToString());
        }

        public override int CalculatePart1()
        {
            var sumVisible = 0;

            for (var row = 0; row < _grid.GetLength(0); row++)
            for (var column = 0; column < _grid.GetLength(1); column++)
                if (IsVisible(row, column))
                    sumVisible++;
            return sumVisible;
        }

        private bool IsVisible(int row, int column)
        {
            return IsVisible(row, column, "up") ||
                   IsVisible(row, column, "right") ||
                   IsVisible(row, column, "down") ||
                   IsVisible(row, column, "left");
        }

        private bool IsVisible(int row, int column, string edgeDirection)
        {
            switch (edgeDirection)
            {
                case "up":
                {
                    for (var i = row - 1; i >= 0; i--)
                        if (_grid[i, column] >= _grid[row, column])
                            return false;
                    break;
                }
                case "right":
                {
                    for (var i = column + 1; i < _grid.GetLength(1); i++)
                        if (_grid[row, i] >= _grid[row, column])
                            return false;
                    break;
                }
                case "down":
                {
                    for (var i = row + 1; i < _grid.GetLength(0); i++)
                        if (_grid[i, column] >= _grid[row, column])
                            return false;
                    break;
                }
                case "left":
                {
                    for (var i = column - 1; i >= 0; i--)
                        if (_grid[row, i] >= _grid[row, column])
                            return false;
                    break;
                }
            }

            return true;
        }

        public override int CalculatePart2()
        {
            var highestScenicScore = 0;

            for (var row = 0; row < _grid.GetLength(0); row++)
            for (var column = 0; column < _grid.GetLength(1); column++)
            {
                var scenicScore = CalculateScenicScore(row, column);
                if (scenicScore > highestScenicScore) highestScenicScore = scenicScore;
            }

            return highestScenicScore;
        }

        private int CalculateScenicScore(int row, int column)
        {
            //stop if you reach an edge or at the first tree that is the same height
            //or taller than the tree under consideration

            var upScore = 0;
            var rightScore = 0;
            var downScore = 0;
            var leftScore = 0;

            for (var i = row - 1; i >= 0; i--)
            {
                upScore++;
                if (_grid[i, column] >= _grid[row, column]) break;
            }

            for (var i = column + 1; i < _grid.GetLength(1); i++)
            {
                rightScore++;
                if (_grid[row, i] >= _grid[row, column]) break;
            }

            for (var i = row + 1; i < _grid.GetLength(0); i++)
            {
                downScore++;
                if (_grid[i, column] >= _grid[row, column]) break;
            }

            for (var i = column - 1; i >= 0; i--)
            {
                leftScore++;
                if (_grid[row, i] >= _grid[row, column]) break;
            }

            return upScore * rightScore * downScore * leftScore;
        }
    }
}