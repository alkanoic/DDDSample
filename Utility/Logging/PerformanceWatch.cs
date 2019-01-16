using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Logging
{
    public class PerformanceWatch
    {

        private readonly Stopwatch sw;

        public PerformanceWatch()
        {
            sw = new Stopwatch();
            sw.Start();
        }

        public string GetElapsedMilliseconds()
        {
            sw.Stop();
            return $"{sw.ElapsedMilliseconds}[msec]";
        }

    }
}
