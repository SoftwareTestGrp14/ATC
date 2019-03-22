using System;
using System.Collections.Generic;
using System.Text;
using ATC;
using NUnit.Framework;

namespace UnitTest
{
    public class TestCalculator
    {
        private DateTime date1;
        private DateTime date2;
        private Calculator _uut;

        [SetUp]
        public void Setup()
        {
            date1 = new DateTime(2019, 3, 19, 13, 0, 0);
            date2 = new DateTime(2019, 3, 19, 14, 0, 0);
            _uut = new Calculator();
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
            Assert.That(_uut.CalcVelocity(x1,x2,y1,y2,date1,date2), Is.EqualTo(expectedResult).Within(0.01));
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
            Assert.That(_uut.CalcCourse(x1, x2, y1, y2), Is.EqualTo(expectedResult));
        }

        [TestCase(10000,4999,2000,false)]
        [TestCase(10000, 5001, 2000, true)]
        [TestCase(10000, 7500, 1699, false)]
        [TestCase(10000, 7500, 2301, false)]

        [TestCase(10000, 7500, 1701, true)]
        [TestCase(10000, 8000, 2299, true)]
        [TestCase(5500, 8000, 2000, true)]
        [TestCase(5500, 7000, 2000, false)]
        public void seperation_raisecondition(int x, int y, int z, bool expectedResult)
        {
            ITrack t1 = new Track("testInput", x, y, z,3,90, date1);
            ITrack t2 = new Track("ToCheckAgainst", 10000,10000,2000,3,90,date1);

            Assert.That(_uut.IsSeparation(t1, t2), Is.EqualTo(expectedResult));
        }
    }
}

