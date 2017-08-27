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
            Assert.Null(target.Encoding);

            var stream = new System.IO.MemoryStream();
            target.File = stream;
            target.Encoding = System.Text.Encoding.UTF8;

            Assert.Same(stream, target.File);
            Assert.Same(System.Text.Encoding.UTF8, target.Encoding);
        }
    }
}
