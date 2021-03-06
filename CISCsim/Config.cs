﻿using System;
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
        //public static string traceFilename = "..\\..\\InputFiles\\fpppp.tra";
        //public static string traceFilename = "..\\..\\InputFiles\\toast.tra";
        public static string traceFilename = "..\\..\\InputFiles\\mpeg2d.tra";

        public static int superScalerFactor             = 10;
        public static int numReservationStationEntries  = 18;
        public static int numFunctionalUnits            = 10; // Applies to everything except branch units
        public static int numRenamingTableEntries       = 17;
        public static int numReorderBufferEntries       = 25;

        public static int level1CacheInstrMissPercent   = 1;
        public static int level1CacheDataMissPercent    = 3;
        public static int level2CacheMissPercent        = 20;
        public static int level1CacheMissPenalty        = 5;
        public static int level2CacheMissPenalty        = 200;

        public static int intExeLatency    = 1;
        public static int mulDivExeLatency = 4;
        public static int fpExeLatency     = 5;
        public static int memExeLatency    = 2;
        public static int branchExeLatency = 1;


        public static void reset()
        {
            Config.superScalerFactor = 10;
            Config.numReservationStationEntries = 18;
            Config.numFunctionalUnits = 10; // Applies to everything except branch units
            Config.numRenamingTableEntries = 17;
            Config.numReorderBufferEntries = 25;
        }
    }
}
