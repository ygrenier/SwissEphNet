using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SwissEphNet.Tests
{

    public class CTest
    {
    
        [Fact]
        public void TestAtof() {
            Assert.Equal(0.0, C.atof(null));
            Assert.Equal(0.0, C.atof(""));
            Assert.Equal(0.0, C.atof("test"));
            Assert.Equal(0.0, C.atof("0"));
            Assert.Equal(0.0, C.atof("0.0"));
            Assert.Equal(1.0, C.atof("1"));
            Assert.Equal(1.2, C.atof("1.2"));
            Assert.Equal(1.0, C.atof("+1"));
            Assert.Equal(1.2, C.atof("+1.2"));
            Assert.Equal(-1.0, C.atof("-1"));
            Assert.Equal(-1.2, C.atof("-1.2"));
        }

        [Fact]
        public void TestFmod()
        {
            Assert.Equal(1.0, C.fmod(3, 2), 8);
            Assert.Equal(1.3, C.fmod(5.3, 2), 8);
            Assert.Equal(1.7, C.fmod(18.5, 4.2), 8);
            Assert.Equal(0.5, C.fmod(18.5, 1), 8);
            Assert.Equal(0.5, C.fmod(5.7, 1.3), 8);
        }

    }
}
