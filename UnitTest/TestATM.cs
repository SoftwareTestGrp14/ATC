using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ATC;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace UnitTest
{
    public class TestATM
    {

        private ATM _uut;
        private ITransponderReceiver _fakeTransponderReceiver;

        private IPlaneTracker _fakePlaneTracker;

        [SetUp]
        public void Setup()
        {

            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();

            _fakePlaneTracker = Substitute.For<IPlaneTracker>();

            _uut = new ATM(_fakePlaneTracker, _fakeTransponderReceiver);
        }

        [TestCase("ATR423;39045;12932;14000;20151006213456789")]
        public void TransponderDataChanged_DifferentArguments_CurrentReceiverDataIsCorrect(string receiverData)
        {
            var fakeList = new List<string>();
            fakeList.Add(receiverData);

            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeList));

           _fakePlaneTracker.Received(1).Update(receiverData);
        }




    }
}
