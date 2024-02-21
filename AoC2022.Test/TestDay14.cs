using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestDay14
    {
        private const string TestDayNumber = "Test_Day14";
        private Day14 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day14(TestDayNumber);
        }

        [Test]
        public void Day14_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(24));
        }

        [Test]
        public void Day14_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo(93));
        }
    }
}