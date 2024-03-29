﻿using AoC.AoC2023;

namespace AoC.AoC2023.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Test_Day01
    {
        private const string TestDayNumber = "Test_Day01-1";
        private const string TestDayNumber_Part2 = "Test_Day01-2";
        private Day01 _day;
        private Day01 _day_Part2;

        [SetUp]
        public void Setup()
        {
            _day = new Day01(TestDayNumber);
            _day_Part2 = new Day01(TestDayNumber_Part2);
        }

        [Test]
        public void Day01_Part1_EndToEnd()
        {
            Assert.That(_day.CalculatePart1(), Is.EqualTo(142));
        }

        [Test]
        public void Day01_Part2_EndToEnd()
        {
            Assert.That(_day_Part2.CalculatePart2(), Is.EqualTo(281));
        }
    }
}