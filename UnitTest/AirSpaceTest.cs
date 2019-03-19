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
        public void GetXStartPoint_XStartPointEq0()
        {
            Assert.That(_uut.XStartPoint, Is.EqualTo(0));
        }

        [Test]
        public void GetYStartPoint_YStartPointEq0()
        {
            Assert.That(_uut.YStartPoint, Is.EqualTo(0));
        }


    }
}
