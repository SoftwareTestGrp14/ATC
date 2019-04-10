using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

        private IConsoleRenderer _fakeConsoleRenderer;

        private IFileRenderer _fakeFileRenderer;
       // private ISeparationCondition _fakeSeparationCondition;



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
            _fakeConsoleRenderer = Substitute.For<IConsoleRenderer>();
            _fakeFileRenderer = Substitute.For<IFileRenderer>();


            _uut = new PlaneTracker(_fakeAirSpaceTracker, _fakeCurrentSeparations, _fakeTracks, _fakeCalculator,_fakeFileRenderer, _fakeConsoleRenderer);
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
        public void Test_IsInAirSpaceCalled_IsCalledWithRightParameters()
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";


            _uut.Update(data1); 
            _uut.Update(data2);

            _fakeAirSpaceTracker.Received().IsInAirSpace(Arg.Is<ITrack>(x=>x._tag== "ATR423"));
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
        public void Test_IsSeparationWithParameters_Called_OnUpdate()
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";

            string data3 = "ABC123;10000;5000;10000;20151006213456789";
            string data4 = "ABC123;10000;5000;10000;20151006213656789";

            _fakeCalculator.CalcCourse(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns(1);
            _fakeCalculator.CalcVelocity(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(),
                Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(1);

            double velocity = _fakeCalculator.CalcVelocity(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<DateTime>(), Arg.Any<DateTime>());

            double course = _fakeCalculator.CalcCourse(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>());

            Track track1 = new Track("ATR423", 39045, 13500,14000, velocity,course, DateTime.Now);
            Track track2 = new Track("ABC123", 10000, 5000, 10000, velocity, course, DateTime.Now);

            _fakeAirSpaceTracker.IsInAirSpace(Arg.Any<ITrack>()).Returns(true);

            _uut.Update(data1);
            _uut.Update(data2);

            _uut.Update(data3);
            _uut.Update(data4);

            _fakeCalculator.Received().IsSeparation(Arg.Is<ITrack>(x=>x._tag==track1._tag), Arg.Is<ITrack>(y=>y._tag==track2._tag));

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

        [TestCase(39045, 39045, 12932, 13500)]
        public void Test_CalcVelocity_Called_OnUpdate(int x1, int x2, int y1, int y2)
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";


            _uut.Update(data1);
            _uut.Update(data2);

            _fakeCalculator.Received().CalcVelocity(x1,x2,y1,y2,
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

        [TestCase(39045, 39045, 12932, 13500)]
        
        public void Test_CalcCourse_Called_OnUpdate(int x1, int x2, int y1, int y2)
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";


            _uut.Update(data1);
            _uut.Update(data2);

            _fakeCalculator.Received().CalcCourse(x1,x2,y1,y2);

        }

        [Test]
        public void Test_RemoveTrackNotInAirspace_IsInAirSpaceCalledThreeTimes()
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

            //Tester på hvor mange gange funktionen IsInAirSpace burde blive kaldt i den situation
            _fakeAirSpaceTracker.Received(3).IsInAirSpace(Arg.Any<ITrack>());
        }


        [Test]
        public void Test_OverwriteExcistingTrackInAirspace_IsInAirSpaceCalledFourTimes()
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

            //Tester på hvor mange gange funktionen IsInAirSpace burde blive kaldt i den situation
            _fakeAirSpaceTracker.Received(4).IsInAirSpace(Arg.Any<ITrack>());
        }

        
        [Test]
        public void Test_IsSeparation_IsSeparationCalledOnceWhenTwoTracksInList()
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

            _fakeCalculator.Received(1).IsSeparation(Arg.Any<ITrack>(), Arg.Any<ITrack>());
          
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

            _fakeCalculator.Received(2).IsSeparation(Arg.Any<ITrack>(), Arg.Any<ITrack>());
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

            _fakeCalculator.Received(2).IsSeparation(Arg.Any<ITrack>(), Arg.Any<ITrack>());
        }



        [Test]
        public void Test_TrackInAirspace_RenderCalled()
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";

            //The track is in airspace
            _fakeAirSpaceTracker.IsInAirSpace(Arg.Any<ITrack>()).Returns(true);

            //Data sendt
            _uut.Update(data1);
            _uut.Update(data2);


            //_fakeConsoleRenderer.Received().Render(Arg.Is<List<ITrack>>(x=>x.Count==1));
            _fakeConsoleRenderer.Received().Render(Arg.Any<List<ITrack>>());
            

        }


        [Test]
        public void Test_TrackInAirspace_RenderCalledWithListCount_1()
        {
            string data1 = "ATR423;39045;12932;14000;20151006213456789";
            string data2 = "ATR423;39045;13500;14000;20151006213656789";

            //The track is in airspace
            _fakeAirSpaceTracker.IsInAirSpace(Arg.Any<ITrack>()).Returns(true);

            //Data sendt
            _uut.Update(data1);
            _uut.Update(data2);


            _fakeConsoleRenderer.Received().Render(Arg.Is<List<ITrack>>(x=>x.Count==1));

        }


        [Test]
        public void Test_RemoveTrackNotInAirspace_RenderCalled()
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

            _fakeConsoleRenderer.Received().Render(Arg.Any<List<ITrack>>());
        }


        [Test]
        public void Test_RemoveTrackNotInAirspace_RenderCalledWithListCount_0()
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

            _fakeConsoleRenderer.Received().Render(Arg.Is<List<ITrack>>(x=>x.Count==0));
        }



        [Test]
        public void Test_OverwriteExcistingTrackInAirspace_RenderCalled()
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

            _fakeConsoleRenderer.Received().Render(Arg.Any<List<ITrack>>());
        }

        [Test]
        public void Test_OverwriteExcistingTrackInAirspace_RenderCalledWithListCount_1()
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

            _fakeConsoleRenderer.Received().Render(Arg.Is<List<ITrack>>(x=>x.Count==1));
        }


        [Test]
        public void Test_IsSeparation_TestConsoleRenderCalledWithSepList()
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

            _fakeConsoleRenderer.Received().Render(Arg.Any<List<ISeparationCondition>>());

        }

        [Test]
        public void Test_IsSeparation_TestConsoleRenderCalledWithSepListCount_1()
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

            _fakeConsoleRenderer.Received().Render(Arg.Is<List<ISeparationCondition>>(x=>x.Count==1));

        }

        [Test]
        public void Test_IsSeparation_TestFileRenderCalled()
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

            _fakeFileRenderer.Received().Render(Arg.Any<ISeparationCondition>());

        }



        [Test]
        public void Test_SeparationRemoveFromListWhen_ConsoleRenderCalledWithSepCondList()
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

            _fakeConsoleRenderer.Received().Render(Arg.Any<List<ISeparationCondition>>());


        }

        public void Test_SeparationRemoveFromListWhen_ConsoleRenderCalledWithSepCondListCount_0()
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

            _fakeConsoleRenderer.Received().Render(Arg.Is<List<ISeparationCondition>>(x=>x.Count==0));


        }


        [Test]
        public void Test_OverwriteSeparationWhen_ConsoleRenderCalledWithSepCondList()
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

            _fakeConsoleRenderer.Received().Render(Arg.Any<List<ISeparationCondition>>());
        }

        [Test]
        public void Test_OverwriteSeparationWhen_ConsoleRenderCalledWithSepCondListCount_0()
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

            _fakeConsoleRenderer.Received().Render(Arg.Is<List<ISeparationCondition>>(x => x.Count == 1));
        }

    }
}