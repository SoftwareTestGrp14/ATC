using ATC;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    class AirSpaceTest
    {
        private AirSpace _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new AirSpace();
        }

        [Test]
        public void GetXEndPoint_XEndPointEq80000()
        {
            Assert.That(_uut.GetXEndPoint(), Is.EqualTo(80000));
        }

        [Test]
        public void GetYEndPoint_YEndPointEq80000()
        {
            Assert.That(_uut.GetYEndPoint(), Is.EqualTo(80000));
        }

        [Test]
        public void GetMinAltitude_MinAltitudeEq500()
        {
            Assert.That(_uut.MinAltitude, Is.EqualTo(500));
        }

        [Test]
        public void GetMaxAltitude_MaxAltitudeEq20000()
        {
            Assert.That(_uut.MaxAltitude, Is.EqualTo(20000));
        }

    }
}
