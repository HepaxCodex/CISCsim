using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Contains the Config details
    /// </summary>
    static class Config
    {
        public static string traceFilename              = "..\\..\\InputFiles\\fpppp.tra";

        public static int superScalerFactor             = 5;
        public static int numReservationStationEntries  = 10;
        public static int numFunctionalUnits            = 4; // Applies to everything except branch units
        public static int numRenamingTableEntries       = 10;
        public static int numReorderBufferEntries       = 10;

        public static int level1CacheMissPercent        = 1;
        public static int level2CacheMissPercent        = 20;
        public static int level1CacheMissPenalty        = 5;
        public static int level2CacheMissPenalty        = 200;
    }
}
