using System;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using AoC.AoC2022;
using AoC.AoC2022.Common;
using System.Configuration;

namespace AoC.AoC2022Test
{
    public class TestDay01
    {
        private const string TestDayNumber = "Test_Day01";

        [SetUp]
        public void Setup()
        {
            var day = new Day01(TestDayNumber);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}