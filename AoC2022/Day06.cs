namespace AoC.AoC2022
{
    internal class Day06 : AoC<List<string>, int, int>
    {
        public Day06(string dayName) : base(dayName)
        {
        }

        public override int CalculatePart1()
        {
            return StartOfPacketMarker(InputData[0], 4);
        }

        private static int StartOfPacketMarker(string dataBuffer, int markerLength)
        {
            for (var i = 0; i < dataBuffer.Length; i++)
                if (AllCharactersDifferent(dataBuffer.Substring(i, markerLength)))
                    return i + markerLength;
            return 0;
        }

        private static bool AllCharactersDifferent(string sequence)
        {
            var markers = new List<char>();

            foreach (var marker in sequence)
            {
                if (markers.Contains(marker)) return false;
                markers.Add(marker);
            }

            return true;
        }

        public override int CalculatePart2()
        {
            return StartOfPacketMarker(InputData[0], 14);
        }
    }
}