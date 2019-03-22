using ATC;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    public class TestFileLog
    {
        private ILogger _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new FileLog();
            
        }


        [Test]
        public void WriteLog_WriteString_StringAppendToLog()
        {
            _uut.Write("Test string");
           // Assert.That()
        }


    }
}
