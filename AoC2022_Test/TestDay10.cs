using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022Test
{
    [TestFixture]
    public class TestDay10
    {
        private const string TestDayNumber = "Test_Day10";
        private Day10 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day10(TestDayNumber);
        }

        [Test]
        public void Day10_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(13140));
        }

        [Test]
        public void Day10_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo(8));
        }
    }
}