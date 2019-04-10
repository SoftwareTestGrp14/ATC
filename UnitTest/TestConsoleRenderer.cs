using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATC;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace UnitTest
{
    class TestConsoleRenderer
    {

        private ConsoleRenderer _uut;
        private ILogger _fakeConsoleLogger;


        [SetUp]
        public void Setup()
        {

            _fakeConsoleLogger = Substitute.For<ILogger>();

            _uut = new ConsoleRenderer(_fakeConsoleLogger);
        }


        [Test]
        public void Test_RenderCalledWithTrackList_WriteCalled()
        {
            List<ITrack> tracks = new List<ITrack>();
            Track track1 = new Track("ABC123", Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<double>(), Arg.Any<double>(), DateTime.Now);
            Track track2 = new Track("DEF456", Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<double>(), Arg.Any<double>(), DateTime.Now);

            tracks.Add(track1);
            tracks.Add(track2);

            _uut.Render(tracks);


            _fakeConsoleLogger.Received().Write(Arg.Any<string>());

        }

        [Test]
        public void Test_RenderCalledWithTrackList_WriteCalledCountPlus1Times()
        {
            List<ITrack> tracks = new List<ITrack>();
            Track track1 = new Track("ABC123", Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<double>(), Arg.Any<double>(), DateTime.Now);
            Track track2 = new Track("DEF456", Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<double>(), Arg.Any<double>(), DateTime.Now);

            tracks.Add(track1);
            tracks.Add(track2);

            _uut.Render(tracks);


            _fakeConsoleLogger.Received(tracks.Count+1).Write(Arg.Any<string>());

        }


        [Test]
        public void Test_RenderCalledWithSeparationConditionList_WriteCalled()
        {
            List<ISeparationCondition> separations = new List<ISeparationCondition>();

            _uut.Render(separations);


            _fakeConsoleLogger.Received().Write(Arg.Any<string>());

        }


        [Test]
        public void Test_RenderCalledWithSeparationConditionList_WriteCalledCountPlus1Times()
        {
            List<ISeparationCondition> separations = new List<ISeparationCondition>();

            Track track1 = new Track("ABC123", Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<double>(), Arg.Any<double>(), DateTime.Now);
            Track track2 = new Track("DEF456", Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<double>(), Arg.Any<double>(), DateTime.Now);

            SeparationCondition sep1 = new SeparationCondition(track1, track2);
            SeparationCondition sep2 = new SeparationCondition(track1, track2);

            separations.Add(sep1);
            separations.Add(sep2);

            _uut.Render(separations);


            _fakeConsoleLogger.Received(separations.Count).Write(Arg.Any<string>());

        }
    }
}
