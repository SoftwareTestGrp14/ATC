using ATC;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    public class TestTrack
    {
        private ITrack _uut;

        [SetUp]
        public void Setup()
        {

            _uut = new Track("“ATR423", 39045, 12932, 14000, 500, 120, new DateTime(2019,3,19));
        }


        [Test]
        public void CreateTrack_InstantiateTrack_TagSet()
        {
            Assert.That(_uut._tag.Equals("ATR423"));
        }

        [Test]
        public void CreateTrack_InstantiateTrack_xCordSet()
        {
            Assert.That(_uut._xCord.Equals(39045));
        }

        [Test]
        public void CreateTrack_InstantiateTrack_yCordSet()
        {
            Assert.That(_uut._yCord.Equals(12932));
        }

        [Test]
        public void CreateTrack_InstantiateTrack_AltitudeSet()
        {
            Assert.That(_uut._alt.Equals(14000));
        }


        [Test]
        public void CreateTrack_InstantiateTrack_VelocitySet()
        {
            Assert.That(_uut._velocity.Equals(500));
        }


        [Test]
        public void CreateTrack_InstantiateTrack_CourseSet()
        {
            Assert.That(_uut._course.Equals(120));
        }




        [Test]
        public void CompareTrakcs_InstantiateTrackAndCompare_NotEqual()
        {
            var newTrack = new Track("“ATR11", 14241, 22223, 14000, 500, 120, new DateTime(2019, 3, 19));

            Assert.That(_uut._course.Equals(120));
        }
        


    }
}
