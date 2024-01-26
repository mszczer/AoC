using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022_Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestDay04
    {
        private const string TestDayNumber = "Test_Day04";
        private Day04 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day04(TestDayNumber);
        }

        [Test]
        public void Day04_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(2));
        }

        [Test]
        public void Day04_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo(4));
        }
    }
}