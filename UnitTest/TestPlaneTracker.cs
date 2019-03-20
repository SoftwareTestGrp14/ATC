using ATC;
using NUnit.Framework;
using NSubstitute;

namespace UnitTests
{
    public class TestPlaneTracker
    {
        private PlaneTracker _uut;
        private IAirSpace _fakeAirSpace;
        private IAirSpaceTracker _fakeAirSpaceTracker;




        [SetUp]
        public void Setup()
        {
            _fakeAirSpace = Substitute.For<IAirSpace>();
            _fakeAirSpaceTracker = Substitute.For<IAirSpaceTracker>();

            _uut=new PlaneTracker(_fakeAirSpace, _fakeAirSpaceTracker);
        }

        [Test]
        public void TestUpdateWithDataOutsideAirspace()
        {
            string data = "ATR423; 39045; 12932; 14000; 20151006213456789";

            _uut.Update(data);
            //Assert.That();
        }
    }
}