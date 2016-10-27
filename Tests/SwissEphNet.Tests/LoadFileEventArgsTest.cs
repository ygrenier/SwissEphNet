using System;
using Xunit;

namespace SwissEphNet.Tests
{
    
    public class LoadFileEventArgsTest
    {
        [Fact]
        public void TestCreate() {
            var target = new LoadFileEventArgs("file");
            Assert.Equal("file", target.FileName);
            Assert.Null(target.File);

            var stream = new System.IO.MemoryStream();
            target.File = stream;

            Assert.Same(stream, target.File);

        }
    }
}
