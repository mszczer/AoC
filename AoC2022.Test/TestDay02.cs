using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestDay02
    {
        private const string TestDayNumber = "Test_Day02";
        private Day02 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day02(TestDayNumber);
        }

        [Test]
        public void Day02_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(15));
        }

        [Test]
        public void Day02_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo(12));
        }
    }
}