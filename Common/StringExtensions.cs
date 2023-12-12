namespace AoC.Common
{
    public static class StringExtensions
    {
        public static string[] GetWords(this string str, string[] separatingStrings)
        {
            return str.Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}