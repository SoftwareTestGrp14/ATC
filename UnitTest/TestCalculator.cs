using System;
using System.Collections.Generic;
using System.Text;
using ATC;
using NUnit.Framework;

namespace UnitTests
{
    public class TestCalculator
    {
        private DateTime date1;
        private DateTime date2;

        [SetUp]
        public void Setup()
        {
            date1 = new DateTime(2019, 3, 19, 13, 0, 0);
            date2 = new DateTime(2019, 3, 19, 14, 0, 0);
        }

        [TestCase(200, 200, 200, 1200, 0.27)]
        public void velocity_CorrectCalculation(int x1, int x2, int y1, int y2, double expectedResult)
        {
            Assert.That(Calculator.CalcVelocity(x1,x2,y1,y2,date1,date2), Is.EqualTo(expectedResult).Within(0.01));
        }

    }
}

