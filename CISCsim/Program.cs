using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using System.IO;

namespace CISCsim
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World\n");

            System.IO.StreamWriter statsOut = new System.IO.StreamWriter("statsOut.Write.csv");
            writeStarter(statsOut);
            for (int k = 0; k < 5; k++)
            {
            //int k = 4;
                for (int i = 10; i <= 30; i++)
                {
                    switch (k)
                    {
                        case 0: Config.superScalerFactor = i; break;
                        case 1: Config.numReservationStationEntries = i; break;
                        case 2: Config.numFunctionalUnits = i; break;
                        case 3: Config.numRenamingTableEntries = i; break;
                        case 4: Config.numReorderBufferEntries = i; break;
                    }

                    CPU.reset();

                    Stopwatch sw = Stopwatch.StartNew();

                    while (CPU.lastInstructionCompleted == false)
                    {
                        CPU.completeStage.runCycle();
                        CPU.executeStage.runCycle();
                        CPU.issueStage.runCycle();
                        CPU.dispatchStage.runCycle();
                        CPU.decodeStage.runCycle();
                        CPU.fetchStage.Fetch();
                        //System.Console.WriteLine("PC {0}", CPU.pc_count);
                        Statistics.totalCycles++;
                    }

                    sw.Stop();
                    Statistics.avgInstructionsPerCycle = (float) Statistics.instructionsCompleted / (float)Statistics.totalCycles;
                    System.Console.WriteLine("Time taken: {0} ms", sw.Elapsed.TotalMilliseconds);
                    writeData(statsOut);
                    CPU.reset();
                    Config.reset();
                    Statistics.reset();
                }
            }
        
            statsOut.Close();

            System.Console.WriteLine("Press Any Key To Exit\n");
            System.Console.Read();
        }

        public static void writeStarter(StreamWriter statsOut)
        {
            statsOut.Write("superScalerFactor             ,");
            statsOut.Write("numReservationStationEntries  ,");
            statsOut.Write("numFunctionalUnits            ,"); // Applies to everything except branch units
            statsOut.Write("numRenamingTableEntries       ,");
            statsOut.Write("numReorderBufferEntries       ,");

            statsOut.Write(" totalCycles            ,");
            statsOut.Write(" level1DataCacheMisses  ,");
            statsOut.Write(" level1InstrCacheMisses ,");
            statsOut.Write(" level2CacheMisses      ,");
            statsOut.Write(" avgInstructionsPerCycle   ,");

            statsOut.Write(" registerRenameFileFull ,");
            statsOut.Write(" reservationStationFull ,");
            statsOut.Write(" reorderBufferFull      ,");
            statsOut.Write(" branchMispredicts      ,");
            statsOut.Write(" totalBranchPredictions ,");

            statsOut.Write(" integerExecutionUnitsFull  ,");
            statsOut.Write(" floatingExecutionUnitsFull ,");
            statsOut.Write(" multDivExecutionUnitsFull  ,");
            statsOut.Write(" memoryExecutionUnitsFull   ,");
            statsOut.Write(" branchExecutionUnitsFull   ,");

            statsOut.Write(" integerExecutionUnitsEmpty  ,");
            statsOut.Write(" floatingExecutionUnitsEmpty ,");
            statsOut.Write(" multDivExecutionUnitsEmpty  ,");
            statsOut.Write(" memoryExecutionUnitsEmpty   ,");
            statsOut.Write(" branchExecutionUnitsEmpty   ,");
            statsOut.Write(" instructionsCompleted \n");
        }

        public static void writeData( StreamWriter statsOut)
        {
            statsOut.Write(Config.superScalerFactor             );statsOut.Write(",");
            statsOut.Write(Config.numReservationStationEntries  );statsOut.Write(",");
            statsOut.Write(Config.numFunctionalUnits            );statsOut.Write(","); // Applies to everything except branch units
            statsOut.Write(Config.numRenamingTableEntries       );statsOut.Write(",");
            statsOut.Write(Config.numReorderBufferEntries); statsOut.Write(",");


            statsOut.Write(Statistics.totalCycles            ); statsOut.Write(",");
            statsOut.Write(Statistics.level1DataCacheMisses  ); statsOut.Write(",");
            statsOut.Write(Statistics.level1InstrCacheMisses ); statsOut.Write(",");
            statsOut.Write(Statistics.level2CacheMisses      ); statsOut.Write(",");
            statsOut.Write(Statistics.avgInstructionsPerCycle   ); statsOut.Write(",");

            statsOut.Write(Statistics.registerRenameFileFull ); statsOut.Write(",");
            statsOut.Write(Statistics.reservationStationFull ); statsOut.Write(",");
            statsOut.Write(Statistics.reorderBufferFull      ); statsOut.Write(",");
            statsOut.Write(Statistics.branchMispredicts      ); statsOut.Write(",");
            statsOut.Write(Statistics.totalBranchPredictions ); statsOut.Write(",");

            statsOut.Write(Statistics.integerExecutionUnitsFull  ); statsOut.Write(",");
            statsOut.Write(Statistics.floatingExecutionUnitsFull ); statsOut.Write(",");
            statsOut.Write(Statistics.multDivExecutionUnitsFull  ); statsOut.Write(",");
            statsOut.Write(Statistics.memoryExecutionUnitsFull   ); statsOut.Write(",");
            statsOut.Write(Statistics.branchExecutionUnitsFull   ); statsOut.Write(",");

            statsOut.Write(Statistics.integerExecutionUnitsEmpty  ); statsOut.Write(",");
            statsOut.Write(Statistics.floatingExecutionUnitsEmpty ); statsOut.Write(",");
            statsOut.Write(Statistics.multDivExecutionUnitsEmpty  ); statsOut.Write(",");
            statsOut.Write(Statistics.memoryExecutionUnitsEmpty   ); statsOut.Write(",");
            statsOut.Write(Statistics.branchExecutionUnitsEmpty   ); statsOut.Write(",");
            statsOut.Write(Statistics.instructionsCompleted); statsOut.Write("\n");

        }

    }

}


