using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestDay03
    {
        private const string TestDayNumber = "Test_Day03";
        private Day03 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day03(TestDayNumber);
        }

        [Test]
        public void Day03_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(157));
        }

        [Test]
        public void Day03_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo(70));
        }
    }
}