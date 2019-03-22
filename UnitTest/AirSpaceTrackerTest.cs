using System;
using System.Collections.Generic;
using System.Text;
using ATC;
using NUnit.Framework;
using NSubstitute;


namespace UnitTest
{
    [TestFixture]
    class AirSpaceTrackerTest
    {
        private AirSpaceTracker _uut;
        private ITrack track;

        [SetUp]
        public void SetUp()
        {
            track = Substitute.For<ITrack>();
            _uut=new AirSpaceTracker();
        }

        [TestCase(1000,20,500)]
        [TestCase(500, 0, 0)]
        [TestCase(20000, 80000, 80000)]
        [TestCase(645, 4359, 654)]
        [TestCase(17867, 560, 75663)]
        public void IsInAirSpace_TracksAreInsideAirSpace_ReturnsTrue(int trackAlt, int trackX, int trackY)
        {
            track._alt.Returns(trackAlt);
            
            track._xCord.Returns(trackX);
            
            track._yCord.Returns(trackY);
            
            Assert.That(_uut.IsInAirSpace(track), Is.EqualTo(true));
        }

        [TestCase(100000,453, 567)]
        [TestCase(599, -1, -1)]
        [TestCase(20001, 80001, 80001)]
        [TestCase(0, 89001, 654)]
        [TestCase(500, 0, 80001)]
        public void IsInAirSpace_TracksAreOutsideAirSpace_ReturnsFalse(int trackAlt, int trackX, int trackY)
        {
            track._alt.Returns(trackAlt);
            
            track._xCord.Returns(trackX);
            
            track._yCord.Returns(trackY);
            
            Assert.That(_uut.IsInAirSpace(track), Is.EqualTo(false));
        }

      


    }
}
