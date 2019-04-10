using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATC;
using NSubstitute;
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


    }
}
