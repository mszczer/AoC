using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022_Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestDay05
    {
        private const string TestDayNumber = "Test_Day05";
        private Day05 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day05(TestDayNumber);
        }

        [Test]
        public void Day05_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo("CMZ"));
        }

        [Test]
        public void Day05_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo("MCD"));
        }
    }
}