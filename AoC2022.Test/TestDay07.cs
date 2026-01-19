using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestDay07
    {
        private const string TestDayNumber = "TestDay07";
        private Day07 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day07(TestDayNumber);
        }

        [Test]
        public void Day07_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(95437));
        }

        [Test]
        public void Day07_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo(24933642));
        }
    }
}