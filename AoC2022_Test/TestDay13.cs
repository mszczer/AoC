using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022_Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestDay13
    {
        private const string TestDayNumber = "Test_Day13";
        private Day13 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day13(TestDayNumber);
        }

        [Test]
        public void Day13_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(13));
        }

        [Test]
        public void Day13_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo(140));
        }
    }
}