using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
