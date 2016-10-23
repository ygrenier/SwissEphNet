using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwissEphNet;

namespace SweMini
{
    public class Program
    {
        public static int Main(string[] args)
        {
#if DEBUG
            Console.Write("\nPress a key to quit...");
            Console.ReadKey();
#endif
            return 0;
        }
        public static void printf(string Format, params object[] Parameters)
        {
            Console.Write(C.sprintf(Format, Parameters));
        }

    }
}
