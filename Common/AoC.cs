namespace AoC.Common;

public abstract class AoC<T, TResult1, TResult2> : IAoC<TResult1, TResult2> where T : new()
{
    private readonly string _dayName;
    private readonly string _inputDirectory;
    protected T InputData;

    protected AoC(string dayName, string inputDirectory)
    {
        _dayName = dayName;
        _inputDirectory = inputDirectory;

        InputData = ParseInputFile();
    }

    protected AoC(string dayName) : this(dayName, "InputData")
    {
    }
    protected AoC(string dayName, T inputData)
    {
        _dayName = dayName;
        _inputDirectory = string.Empty;
        InputData = inputData;
    }

    public abstract TResult1 CalculatePart1();
    public abstract TResult2 CalculatePart2();

    public void PrintResults()
    {
        Console.WriteLine($"{_dayName} part1 answer: {CalculatePart1()}");
        Console.WriteLine($"{_dayName} part2 answer: {CalculatePart2()}");
    }

    private T? ParseInputFile()
    {
        var inputFile = Path.Combine($"{_inputDirectory}", $"{_dayName}.txt");
        var inputText = File.ReadAllLines(inputFile);
        return (T)Activator.CreateInstance(typeof(T), new object[] { inputText });
    }
}