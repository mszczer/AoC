using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestDay08
    {
        private const string TestDayNumber = "Test_Day08";
        private Day08 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day08(TestDayNumber);
        }

        [Test]
        public void Day08_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(21));
        }

        [Test]
        public void Day08_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo(8));
        }
    }
}