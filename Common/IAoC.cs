namespace AoC.Common
{
    public interface IAoC<out TPart1, out TPart2>
    {
        TPart1 CalculatePart1();
        TPart2 CalculatePart2();
        void PrintResults();
    }
}