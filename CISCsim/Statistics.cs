using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Keeps the statistics on the CPU
    /// </summary>
    [Serializable()] 
    static class Statistics
    {
        public static int totalCycles = 0;
        public static int level1DataCacheMisses = 0;
        public static int level1InstrCacheMisses = 0;
        public static int level2CacheMisses = 0;
        public static int instructionsExecuted = 0;

        public static int registerRenameFileFull = 0;
        public static int reservationStationFull = 0;
        public static int reorderBufferFull = 0;
        public static int branchMispredicts = 0;
        public static int totalBranchPredictions = 0;

        public static int integerExecutionUnitsFull = 0;
        public static int floatingExecutionUnitsFull = 0;
        public static int multDivExecutionUnitsFull = 0;
        public static int memoryExecutionUnitsFull = 0;
        public static int branchExecutionUnitsFull = 0;

        public static int integerExecutionUnitsEmpty = 0;
        public static int floatingExecutionUnitsEmpty = 0;
        public static int multDivExecutionUnitsEmpty = 0;
        public static int memoryExecutionUnitsEmpty = 0;
        public static int branchExecutionUnitsEmpty = 0;

        public static int instructionsCompleted = 0;

        public static void reset()
        {
        Statistics.totalCycles            = 0;
        Statistics.level1DataCacheMisses  = 0;
        Statistics.level1InstrCacheMisses = 0;
        Statistics.level2CacheMisses      = 0;
        Statistics.instructionsExecuted   = 0;

        Statistics.registerRenameFileFull = 0;
        Statistics.reservationStationFull = 0;
        Statistics.reorderBufferFull      = 0;
        Statistics.branchMispredicts      = 0;
        Statistics.totalBranchPredictions = 0;

        Statistics.integerExecutionUnitsFull  = 0;
        Statistics.floatingExecutionUnitsFull = 0;
        Statistics.multDivExecutionUnitsFull  = 0;
        Statistics.memoryExecutionUnitsFull   = 0;
        Statistics.branchExecutionUnitsFull   = 0;

        Statistics.integerExecutionUnitsEmpty  = 0;
        Statistics.floatingExecutionUnitsEmpty = 0;
        Statistics.multDivExecutionUnitsEmpty  = 0;
        Statistics.memoryExecutionUnitsEmpty   = 0;
        Statistics.branchExecutionUnitsEmpty   = 0;

        Statistics.instructionsCompleted = 0;
        }

    }

    


}
