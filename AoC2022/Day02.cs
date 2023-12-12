using System.Linq;

namespace AoC.AoC2022
{
    internal class Day02 : AoC<List<string>, int, int>
    {
        private const int Loss = 0;
        private const int Draw = 3;
        private const int Win = 6;

        private const int Rock = 1;
        private const int Paper = 2;
        private const int Scissors = 3;

        public Day02(string dayName) : base(dayName)
        {
        }

        public override int CalculatePart1()
        {
            return CalculateStrategyTotalScore();
        }

        public override int CalculatePart2()
        {
            return CalculateWinLoseDrawStrategy();
        }

        private int CalculateWinLoseDrawStrategy()
        {
            return InputData.Sum(round => CalculateWinLoseDrawRoundScore(round[0], round[2]));
        }

        private static int CalculateWinLoseDrawRoundScore(char opponent, char response)
        {
            if (opponent == 'A' && response == 'X') return Loss + Scissors;
            if (opponent == 'A' && response == 'Y') return Draw + Rock;
            if (opponent == 'A' && response == 'Z') return Win + Paper;
            if (opponent == 'B' && response == 'X') return Loss + Rock;
            if (opponent == 'B' && response == 'Y') return Draw + Paper;
            if (opponent == 'B' && response == 'Z') return Win + Scissors;
            if (opponent == 'C' && response == 'X') return Loss + Paper;
            if (opponent == 'C' && response == 'Y') return Draw + Scissors;
            if (opponent == 'C' && response == 'Z') return Win + Rock;
            return 0;
        }

        private int CalculateStrategyTotalScore()
        {
            //foreach (var round in InputData)
            //    totalScore += CalculateRoundScore(round[0], round[2]);
            return InputData.Sum(round => CalculateRoundScore(round[0], round[2]));
        }

        private static int CalculateRoundScore(char opponent, char response)
        {
            if (opponent == 'A' && response == 'X') return Rock + Draw;
            if (opponent == 'A' && response == 'Y') return Paper + Win;
            if (opponent == 'A' && response == 'Z') return Scissors + Loss;
            if (opponent == 'B' && response == 'X') return Rock + Loss;
            if (opponent == 'B' && response == 'Y') return Paper + Draw;
            if (opponent == 'B' && response == 'Z') return Scissors + Win;
            if (opponent == 'C' && response == 'X') return Rock + Win;
            if (opponent == 'C' && response == 'Y') return Paper + Loss;
            if (opponent == 'C' && response == 'Z') return Scissors + Draw;
            return 0;
        }
    }
}