using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ATC;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    class TestFileRender
    {
        private FileRenderer _uut;
        private ILogger logger;
        private ISeparationCondition separation;

        [SetUp]
        public void SetUp()
        {
            separation = Substitute.For<ISeparationCondition>();
            logger = Substitute.For<ILogger>();
            _uut = new FileRenderer(logger);
        }

        [Test]
        public void RenderTest_RenderCalled_WriteInLoggerCalled()
        {
            _uut.Render(separation);
            logger.Received(1).Write(Arg.Any<string>());
        }

        [Test]
        public void RenderTest_RenderCalled_WriteInLoggerCalled_WithRightParameter()
        {
            ISeparationCondition newSeparationCondition = new SeparationCondition(
                new Track("ATR423", 0,0, 500, 1,1,DateTime.Now), 
                new Track("ABC123", 0, 0, 500, 1, 1, DateTime.Now));

            _uut.Render(newSeparationCondition);

            logger.Received(1).Write(Arg.Is<string>(x=>x.Contains("ATR423") && x.Contains("ABC123")));
        }
    }
}
