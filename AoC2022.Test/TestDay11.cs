using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022.Tests
{
    [TestFixture]
    public class TestDay11
    {
        private const string TestDayNumber = "Test_Day11";
        private Day11 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day11(TestDayNumber);
        }

        [Test]
        public void Day11_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(10605));
        }

        [Test]
        public void Day11_Part2_EndToEnd()
        {
            Assert.That(_day.CalculatePart2(), Is.EqualTo(2713310158));
        }
    }
}