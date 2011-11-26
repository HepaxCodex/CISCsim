using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Keeps the statistics on the CPU
    /// </summary>
    static class Statistics
    {
        public static int totalCycles            = 0;
        public static int level1CacheMisses      = 0;
        public static int level2CacheMisses      = 0;
        public static int instructionsExecuted   = 0;
    }
}
