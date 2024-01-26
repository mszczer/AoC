using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022_Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestDay09
    {
        private const string TestDayNumber = "Test_Day09";
        private const string TestDayNumberPart2 = "Test_Day09_Part2";
        private Day09 _day;
        private Day09 _dayPart2;

        [SetUp]
        public void Setup()
        {
            _day = new Day09(TestDayNumber);
            _dayPart2 = new Day09(TestDayNumberPart2);
        }

        [Test]
        public void Day09_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(13));
        }

        [Test]
        public void Day09_Part2_EndToEnd()
        {
            Assert.That(_dayPart2.CalculatePart2(), Is.EqualTo(36));
        }
    }
}