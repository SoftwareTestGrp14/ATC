using ATC;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    public class TestTrack
    {
        private ITrack _uut;

        [SetUp]
        public void Setup()
        {

            _uut = new Track("ATR423", 39045, 12932, 14000, 500, 120, new DateTime(2019,3,19));
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
        public void CreateTrack_InstantiateTrack_DateTimeSet()
        {
            var date = new DateTime(2019, 3, 19);
            Assert.That(_uut._timestamp.Equals(date));
        }


        [Test]
        public void CompareTracks_InstantiateTrackAndCompare_NotEqual()
        {
            var newTrack = new Track("“ATR11", 14241, 22223, 14000, 500, 120, new DateTime(2019, 3, 19));

            Assert.That(_uut.Equals(newTrack), Is.False);
        }

        [Test]
        public void CompareTracks_InstantiateTrackAndCompare_Equal()
        {
            var newTrack = new Track("ATR423", 39045, 12932, 14000, 500, 120, new DateTime(2019, 3, 19));


            Assert.That(_uut.Equals(newTrack), Is.True);
        }

        [Test]
        public void Constructor_InvalidValues_Altitude()
        {
            var newTrack = new Track("invalidFlight", 39045, 12932, 250, 0, -21, new DateTime(2019, 3, 19));


            Assert.That(newTrack._alt, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_InvalidValues_Velocity()
        {
            var newTrack = new Track("invalidFlight", 39045, 12932, 250, 0, -21, new DateTime(2019, 3, 19));


            Assert.That(newTrack._velocity, Is.EqualTo(0));
        }

        [TestCase(-21)]
        [TestCase(421)]
        public void Constructor_InvalidValues_Course(double c)
        {
            var newTrack = new Track("invalidFlight", 39045, 12932, 250, 0, c, new DateTime(2019, 3, 19));


            Assert.That(newTrack._course, Is.EqualTo(0));
        }
    }
}
