using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNet.Tests
{
    public static class ResourceFileHelpers
    {
        public static Stream OpenResourceFile(string name)
        {
            var asm = typeof(ResourceFileHelpers).GetAssembly();
            String sr = $"SwissEphNet.Tests.files.{name}".Replace("/", ".").Replace("\\", ".");
            return asm.GetManifestResourceStream(sr);
        }
    }
}
