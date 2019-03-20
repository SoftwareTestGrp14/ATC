using System;
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
        private ITrack _fakeTrack;
        private ICalculator _fakeCalculator;



        [SetUp]
        public void Setup()
        {
            _fakeAirSpace = Substitute.For<IAirSpace>();
            _fakeAirSpaceTracker = Substitute.For<IAirSpaceTracker>();

            _fakeCurrentSeparations = Substitute.For<List<ISeparationCondition>>();
            _fakeTracks = Substitute.For<List<ITrack>>();
            _fakeFile = Substitute.For<FileLog>();
            _fakeConsole = Substitute.For<ConsoleLog>();
            _fakeTrack = Substitute.For<ITrack>();
            _fakeCalculator = Substitute.For<ICalculator>();

            _uut =new PlaneTracker(_fakeAirSpace, _fakeAirSpaceTracker, _fakeCurrentSeparations, _fakeTracks, _fakeConsole, _fakeFile, _fakeCalculator);
        }

        [Test]
        public void Test_IsInAirSpaceCalled_OnUpdate()
        {
            string data1 = "ATR423; 39045; 12932; 14000; 20151006213456789";
            string data2 = "ATR423; 39045; 13500; 14000; 20151006213656789";

            //ITrack track = new Track("ATR423", 39045, 12932, 14000, 1, 1, DateTime.Now);
           // _fakeAirSpaceTracker.IsInAirSpace(_fakeAirSpace, _fakeTrack).Returns(true);

            _uut.Update(data1); 
            _uut.Update(data2);

            _fakeAirSpaceTracker.Received(3).IsInAirSpace(Arg.Any<IAirSpace>(), Arg.Any<ITrack>());
        }


        [Test]
        public void Test_fLog_write_called_OnUpdate()
        {
            string data1 = "ATR423; 39045; 12932; 14000; 20151006213456789";
            string data2 = "ATR423; 39045; 13500; 14000; 20151006213656789";

            ITrack fakeTrack1 = Substitute.For<ITrack>();
            ITrack fakeTrack2 = Substitute.For<ITrack>();

            _fakeCalculator.IsSeparation(fakeTrack1,fakeTrack2).Returns(true);
            
            _uut.Update(data1);
            _uut.Update(data2);

            _fakeFile.Received().Write(Arg.Any<string>());
            

        }

        [Test]
        public void Test_cLog_write_called_OnUpdate()
        {
            string data1 = "ATR423; 39045; 12932; 14000; 20151006213456789";
            string data2 = "ATR423; 39045; 13500; 14000; 20151006213656789";

            _uut.Update(data1);
            _uut.Update(data2);

            _fakeFile.Received().Write(Arg.Any<string>());

        }



    }
}