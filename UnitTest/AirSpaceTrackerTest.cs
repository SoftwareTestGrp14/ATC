using System;
using System.Collections.Generic;
using System.Text;
using ATC;
using NUnit.Framework;
using NSubstitute;


namespace UnitTests
{
    [TestFixture]
    class AirSpaceTrackerTest
    {
        private AirSpaceTracker _uut;
        private IAirSpace airSpace;
        private ITrack track;

        [SetUp]
        public void SetUp()
        {
            airSpace = Substitute.For<IAirSpace>();
            track = Substitute.For<ITrack>();
            _uut=new AirSpaceTracker();
        }

        [Test]
        public void IsInAirSpace_GetXEndPointIsCalled()
        {
            track._alt.Returns(2);
            airSpace.MinAltitude.Returns(1);
            airSpace.MaxAltitude.Returns(3);
            track._xCord.Returns(1);
            airSpace.XStartPoint.Returns(0);
            _uut.IsInAirSpace(airSpace, track);
            airSpace.Received(1).GetXEndPoint();
        }

        [Test]
        public void IsInAirSpace_GetYEndPointIsCalled()
        {
            track._alt.Returns(2);
            airSpace.MinAltitude.Returns(1);
            airSpace.MaxAltitude.Returns(3);
            track._xCord.Returns(1);
            airSpace.XStartPoint.Returns(0);
            airSpace.GetXEndPoint().Returns(2);
            track._yCord.Returns(1);
            airSpace.YStartPoint.Returns(0);

            _uut.IsInAirSpace(airSpace, track);
            airSpace.Received(1).GetYEndPoint();
        }

        [TestCase(1000,20,500)]
        [TestCase(500, 0, 0)]
        [TestCase(20000, 80000, 80000)]
        [TestCase(645, 4359, 654)]
        [TestCase(17867, 560, 75663)]
        public void IsInAirSpace_TracksAreInsideAirSpace_ReturnsTrue(int trackAlt, int trackX, int trackY)
        {
            track._alt.Returns(trackAlt);
            airSpace.MinAltitude.Returns(500);
            airSpace.MaxAltitude.Returns(20000);

            track._xCord.Returns(trackX);
            airSpace.XStartPoint.Returns(0);
            airSpace.GetXEndPoint().Returns(80000);

            track._yCord.Returns(trackY);
            airSpace.YStartPoint.Returns(0);
            airSpace.GetYEndPoint().Returns(80000);

            Assert.That(_uut.IsInAirSpace(airSpace, track), Is.EqualTo(true));
        }

        [TestCase(100000,453, 567)]
        [TestCase(599, -1, -1)]
        [TestCase(20001, 80001, 80001)]
        [TestCase(0, 89001, 654)]
        [TestCase(500, 0, 80001)]
        public void IsInAirSpace_TracksAreOutsideAirSpace_ReturnsFalse(int trackAlt, int trackX, int trackY)
        {
            track._alt.Returns(trackAlt);
            airSpace.MinAltitude.Returns(500);
            airSpace.MaxAltitude.Returns(20000);

            track._xCord.Returns(trackX);
            airSpace.XStartPoint.Returns(0);
            airSpace.GetXEndPoint().Returns(80000);

            track._yCord.Returns(trackY);
            airSpace.YStartPoint.Returns(0);
            airSpace.GetYEndPoint().Returns(80000);

            Assert.That(_uut.IsInAirSpace(airSpace, track), Is.EqualTo(false));
        }

      


    }
}
