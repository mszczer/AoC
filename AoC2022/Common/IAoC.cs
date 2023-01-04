namespace AoC.AoC2022.Common
{
    public interface IAoC<out TPart1, out TPart2>
    {
        TPart1 CalculatePart1();
        TPart1 CalculatePart2();
        void PrintResults();
    }
}