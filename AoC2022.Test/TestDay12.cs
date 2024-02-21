using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestDay12
    {
        private const string TestDayNumber = "Test_Day12";
        private Day12 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day12(TestDayNumber);
        }

        [Test]
        public void Day12_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(31));
        }

        [Test]
        public void Day12_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo(29));
        }
    }
}