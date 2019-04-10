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

            _uut.Render(tracks);


            _fakeConsoleLogger.Received().Write(Arg.Any<string>());

        }


        [Test]
        public void Test_RenderCalledWithSeparationConditionList_WriteCalled()
        {
            List<ISeparationCondition> separations = new List<ISeparationCondition>();

            _uut.Render(separations);


            _fakeConsoleLogger.Received().Write(Arg.Any<string>());

        }
    }
}
