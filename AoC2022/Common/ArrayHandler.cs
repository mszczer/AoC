namespace AoC.AoC2022.Common
{
    public static class ArrayHandler
    {
        public static T[,] GetTwoDimensionalArray<T>(int rows, int columns, T initValue)
        {
            var type = typeof(T);
            var array = new T[rows, columns];
            for (var r = 0; r < rows; r++)
            for (var c = 0; c < columns; c++)
                array[r, c] = initValue;
            return array;
        }
    }
}