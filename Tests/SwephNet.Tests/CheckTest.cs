using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwephNet.Tests
{
    /// <summary>
    /// Description résumée pour CheckTest
    /// </summary>
    [TestClass]
    public class CheckTest
    {
        
        [TestMethod]
        public void TestArgumentNotNull()
        {
            Check.ArgumentNotNull(123, "name");
            Check.ArgumentNotNull(123, "name", "message");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes=false)]
        public void TestArgumentNotNull_Null_WithoutMessage()
        {
            Check.ArgumentNotNull(null, "name");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes=false)]
        public void TestArgumentNotNull_Null_WithMessage()
        {
            Check.ArgumentNotNull(null, "name", "message");
        }

    }
}
