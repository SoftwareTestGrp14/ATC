using System;
using System.Collections.Generic;
using ATC;
using NUnit.Framework;
using NSubstitute;

namespace UnitTest
{
    public class TestPlaneTracker
    {
        private PlaneTracker _uut;
        private IAirSpaceTracker _fakeAirSpaceTracker;
        private List<ISeparationCondition> _fakeCurrentSeparations;
        private List<ITrack> _fakeTracks;
        private ConsoleLog _fakeConsole;
        private FileLog _fakeFile;
        private ITrack _fakeTrack;
        private ICalculator _fakeCalculator;
        private ITracksManager _fakeTracksManager;
        private ISeparationManager _fakeSeparationManager;



        [SetUp]
        public void Setup()
        {
            _fakeAirSpaceTracker = Substitute.For<IAirSpaceTracker>();

            _fakeCurrentSeparations = Substitute.For<List<ISeparationCondition>>();
            _fakeTracks = Substitute.For<List<ITrack>>();
            _fakeFile = Substitute.For<FileLog>();
            _fakeConsole = Substitute.For<ConsoleLog>();
            _fakeTrack = Substitute.For<ITrack>();
            _fakeCalculator = Substitute.For<ICalculator>();
            _fakeTracksManager = Substitute.For<ITracksManager>();
            _fakeSeparationManager = Substitute.For<ISeparationManager>();

            _uut =new PlaneTracker(_fakeAirSpaceTracker, _fakeSeparationManager,_fakeCurrentSeparations, _fakeTracks, _fakeConsole, _fakeFile, _fakeCalculator, _fakeTracksManager);
        }

        [Test]
        public void Test_IsInAirSpaceCalled_OnUpdate()
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";


            _uut.Update(data1); 
            _uut.Update(data2);

            _fakeAirSpaceTracker.Received(3).IsInAirSpace(Arg.Any<ITrack>());
        }


        [Test]
        public void Test_fLog_write_called_OnUpdate()
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";

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
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";

            _uut.Update(data1);
            _uut.Update(data2);

            _fakeFile.Received().Write(Arg.Any<string>());

        }

        [Test]
        public void Test_IsSeparation_Called_OnUpdate()
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";

            string data3 = "ABC123;10000;5000;10000;20151006213456789";
            string data4 = "ABC123;10000;5000;10000;20151006213656789";

            _fakeAirSpaceTracker.IsInAirSpace(Arg.Any<ITrack>()).Returns(true);

            _uut.Update(data1);
            _uut.Update(data2);

            _uut.Update(data3);
            _uut.Update(data4);

            _fakeCalculator.Received().IsSeparation(Arg.Any<ITrack>(), Arg.Any<ITrack>());

        }

        [Test]
        public void Test_CalcVelocity_Called_OnUpdate()
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";

          
            _uut.Update(data1);
            _uut.Update(data2);

            _fakeCalculator.Received().CalcVelocity(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(),
                Arg.Any<DateTime>(), Arg.Any<DateTime>());

        }

        [Test]
        public void Test_CalcCourse_Called_OnUpdate()
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";

    

            _uut.Update(data1);
            _uut.Update(data2);

            _fakeCalculator.Received().CalcCourse(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>());

        }

        [Test]
        public void Test_RemoveTrackNotInAirspace_ListEmpty()
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";

            //The track is in airspace
            _fakeAirSpaceTracker.IsInAirSpace(Arg.Any<ITrack>()).Returns(true);

            //Data sendt
            _uut.Update(data1);
            _uut.Update(data2);

            //Track is not in airspace
            _fakeAirSpaceTracker.IsInAirSpace(Arg.Any<ITrack>()).Returns(false);
            _uut.Update(data1);

            Assert.That(_uut._tracks.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_OverwriteExcistingTrackInAirspace_()
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";
            string data3 = "ATR423;40000;13500;14000;20151006213656789";

            //The track is in airspace
            _fakeAirSpaceTracker.IsInAirSpace(Arg.Any<ITrack>()).Returns(true);

            //Data sendt
            _uut.Update(data1);
            _uut.Update(data2);

            //Data to overwrite with
            _uut.Update(data3);

            Assert.That(_uut._tracks[0]._xCord, Is.EqualTo(40000));
        }

        [Test]
        public void Test_SeparationAddedToListWhen_IsSeparation()
        {

            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";

            string data3 = "ABC123;10000;5000;10000;20151006213456789";
            string data4 = "ABC123;10000;5000;10000;20151006213656789";
            
            //They are in airspace
            _fakeAirSpaceTracker.IsInAirSpace(Arg.Any<ITrack>()).Returns(true);

            //There is separation
            _fakeCalculator.IsSeparation(Arg.Any<ITrack>(), Arg.Any<ITrack>()).Returns(true);

            _uut.Update(data1);
            _uut.Update(data2);

            _uut.Update(data3);
            _uut.Update(data4);

            _fakeSeparationManager.Received().AddSeparation(Arg.Any<List<ISeparationCondition>>(), Arg.Any<ISeparationCondition>());
            //Assert.That(_uut._currentSeparations.Count, Is.EqualTo(1));
        }

        
        [Test]
        public void Test_SeparationRemoveFromListWhen_NotIsSeparation()
        {

            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";

            string data3 = "ABC123;10000;5000;10000;20151006213456789";
            string data4 = "ABC123;10000;5000;10000;20151006213656789";

            //They are in airspace
            _fakeAirSpaceTracker.IsInAirSpace(Arg.Any<ITrack>()).Returns(true);

            //There is separation
            _fakeCalculator.IsSeparation(Arg.Any<ITrack>(), Arg.Any<ITrack>()).Returns(true);

            _uut.Update(data1);
            _uut.Update(data2);

            _uut.Update(data3);
            _uut.Update(data4);

            //There is not separation
            _fakeCalculator.IsSeparation(Arg.Any<ITrack>(), Arg.Any<ITrack>()).Returns(false);
            _uut.Update(data4);

            _fakeSeparationManager.Received().RemoveAt(Arg.Any<List<ISeparationCondition>>(), Arg.Any<int>());
            //Assert.That(_uut._currentSeparations.Count, Is.EqualTo(0));
        }

    

        [Test]
        public void Test_OverwriteSeparationWhen_IsSeparation()
        {

            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";

            string data3 = "ABC123;10000;5000;13900;20151006213456789";
            string data4 = "ABC123;10000;5000;13900;20151006213656789";

            string data5 = "ABC123;10000;5000;13900;20161006213656789";

            //They are in airspace
            _fakeAirSpaceTracker.IsInAirSpace(Arg.Any<ITrack>()).Returns(true);

            //There is separation
            _fakeCalculator.IsSeparation(Arg.Any<ITrack>(), Arg.Any<ITrack>()).Returns(true);

            _uut.Update(data1);
            _uut.Update(data2);

            _uut.Update(data3);
            _uut.Update(data4);


            _uut.Update(data5);

            _fakeSeparationManager.ReceivedWithAnyArgs()
                .RemoveAt(Arg.Any<List<ISeparationCondition>>(), Arg.Any<int>());

            _fakeSeparationManager.ReceivedWithAnyArgs().AddSeparation(Arg.Any<List<ISeparationCondition>>(), Arg.Any<ISeparationCondition>());

            //Assert.That(_uut._currentSeparations[0].Timestamp.Year, Is.EqualTo(2016));
        }

        [Test]
        public void Test_DefaultConstructor_called()
        {
            _uut = new PlaneTracker();

            Assert.That(_uut, Is.Not.EqualTo(null));
        }

    }
}