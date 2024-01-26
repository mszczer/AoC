using AoC.AoC2022;
using NUnit.Framework;

namespace AoC.AoC2022_Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestDay10
    {
        private const string TestDayNumber = "Test_Day10";
        private Day10 _day;

        [SetUp]
        public void Setup()
        {
            _day = new Day10(TestDayNumber);
        }

        [Test]
        public void Day10_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(13140));
        }

    }
}