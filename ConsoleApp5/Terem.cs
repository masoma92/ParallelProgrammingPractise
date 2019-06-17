using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    static class Terem
    {
        static public SemaphoreSlim sem = new SemaphoreSlim(10);

        static public object o = new object();

        static int tetelSzam = 0;

        static public int getTetel()
        {
            lock(o)
                return ++tetelSzam;
        }
    }
}
