namespace AoC.AoC2022;

internal class Day06(string dayName) : AoC<List<string>, int, int>(dayName)
{
    public override int CalculatePart1()
    {
        if (InputData == null || InputData.Count == 0 || string.IsNullOrEmpty(InputData[0])) return 0;
        return StartOfPacketMarker(InputData[0], 4);
    }

    private static int StartOfPacketMarker(string dataBuffer, int markerLength)
    {
        if (string.IsNullOrEmpty(dataBuffer) || markerLength <= 0 || markerLength > dataBuffer.Length) return 0;

        for (var i = 0; i + markerLength <= dataBuffer.Length; i++)
            if (AllCharactersDifferent(dataBuffer, i, markerLength))
                return i + markerLength;
        return 0;
    }

    private static bool AllCharactersDifferent(string sequence, int startIndex, int length)
    {
        var seen = new bool[256];
        for (var i = 0; i < length; i++)
        {
            var c = sequence[startIndex + i];
            var idx = (int)c;
            if (seen[idx]) return false;
            seen[idx] = true;
        }

        return true;
    }

    public override int CalculatePart2()
    {
        if (InputData == null || InputData.Count == 0 || string.IsNullOrEmpty(InputData[0])) return 0;
        return StartOfPacketMarker(InputData[0], 14);
    }
}