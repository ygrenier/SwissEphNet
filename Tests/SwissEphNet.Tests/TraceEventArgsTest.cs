using System;
using Xunit;

namespace SwissEphNet.Tests
{
    public class TraceEventArgsTest
    {
        [Fact]
        public void TestCreate() {
            var target = new TraceEventArgs("message");
            Assert.Equal("message", target.Message);
        }
    }
}
