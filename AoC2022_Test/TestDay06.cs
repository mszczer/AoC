using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022Test
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestDay06
    {
        private const string TestDayNumber = "Test_Day06";
        private Day06 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day06(TestDayNumber);
        }

        [Test]
        public void Day06_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(7));
        }

        [Test]
        public void Day06_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo(19));
        }
    }
}