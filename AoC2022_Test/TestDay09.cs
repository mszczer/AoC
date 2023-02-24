using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022Test
{
    [TestFixture]
    public class TestDay09
    {
        private const string TestDayNumber = "Test_Day09";
        private Day09 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day09(TestDayNumber);
        }

        [Test]
        public void Day09_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(13));
        }

        [Test]
        public void Day09_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo(8));
        }
    }
}