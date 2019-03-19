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
        [TestCase(200, 1200, 200, 200, 0.27)]
        [TestCase(200, 200, 1200, 200, 0.27)]
        [TestCase(1200, 200, 200, 200, 0.27)]

        [TestCase(200, 1200, 200, 1200, 0.39)]
        [TestCase(1200, 200, 1200, 200, 0.39)]
        [TestCase(1200, 200, 200, 1200, 0.39)]
        [TestCase(200, 1200, 1200, 200, 0.39)]
        public void velocity_CorrectCalculation(int x1, int x2, int y1, int y2, double expectedResult)
        {
            Assert.That(Calculator.CalcVelocity(x1,x2,y1,y2,date1,date2), Is.EqualTo(expectedResult).Within(0.01));
        }

        [TestCase(200, 200, 200, 1200, 0)]
        [TestCase(200, 1200, 200, 200, 90)]
        [TestCase(200, 200, 1200, 200, 180)]
        [TestCase(1200, 200, 200, 200, 270)]

        [TestCase(200, 1200, 200, 1200, 45)]
        [TestCase(1200, 200, 1200, 200, 225)]
        [TestCase(1200, 200, 200, 1200, 315)]
        [TestCase(200, 1200, 1200, 200, 135)]
        public void course_correctCalculation(int x1, int x2, int y1, int y2, double expectedResult)
        {
            Assert.That(Calculator.CalcCourse(x1, x2, y1, y2), Is.EqualTo(expectedResult).Within(1));
        }

    }
}

