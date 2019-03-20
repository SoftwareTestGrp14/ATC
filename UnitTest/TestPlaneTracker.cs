using System.Collections.Generic;
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
        private List<ISeparationCondition> _fakeCurrentSeparations;
        private List<ITrack> _fakeTracks;
        private ConsoleLog _fakeConsole;
        private FileLog _fakeFile;



        [SetUp]
        public void Setup()
        {
            _fakeAirSpace = Substitute.For<IAirSpace>();
            _fakeAirSpaceTracker = Substitute.For<IAirSpaceTracker>();

            _fakeCurrentSeparations = Substitute.For<List<ISeparationCondition>>();
            _fakeTracks = Substitute.For<List<ITrack>>();
            _fakeFile = Substitute.For<FileLog>();
            _fakeConsole = Substitute.For<ConsoleLog>();

            _uut =new PlaneTracker(_fakeAirSpace, _fakeAirSpaceTracker, _fakeCurrentSeparations, _fakeTracks, _fakeConsole, _fakeFile);
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