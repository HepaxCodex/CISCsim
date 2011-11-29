using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Represents a Generic Execution Unit
    /// 
    /// Holds an instruction and counts down until finished
    /// </summary>
    class ExecutionUnit
    {
        /// <summary>
        /// Instruction currently Being operated on
        /// </summary>
        public Instruction instr;

        /// <summary>
        /// The remaining number of cycles before the instruction is completed
        /// </summary>
        private int remainingCycles;


        public ExecutionUnit(Instruction _instr)
        {
            this.instr = _instr;
            Random rand = new Random();

            switch (instr.executionType)
            {
                case Instruction.ExecutionType.Branch :
                    this.remainingCycles = Config.branchExeLatency;
                    break;
                case Instruction.ExecutionType.FloatingPoint :
                    this.remainingCycles = Config.fpExeLatency;
                    break;
                case Instruction.ExecutionType.Logical :
                case Instruction.ExecutionType.Integer :
                    this.remainingCycles = Config.intExeLatency;
                    break;
                case Instruction.ExecutionType.Mem :
                    this.remainingCycles = Config.memExeLatency;
                    break;
                case Instruction.ExecutionType.MultDiv :
                    this.remainingCycles = Config.mulDivExeLatency;
                    break;
                default :
                    System.Console.WriteLine("ERROR:Execution unit constructor, unknown Execution Type");
                    break;
            }
            

            // Check to see if it is a memory instruction so we can add the delays
            if (instr.executionType == Instruction.ExecutionType.Mem)
            {
                // Check to see if we missed level 1
                if (rand.Next(100) < Config.level1CacheDataMissPercent)
                {
                    Statistics.level1DataCacheMisses++;
                    this.remainingCycles += Config.level1CacheMissPenalty;
                    // Check to see if we missed level 2
                    if (rand.Next(100) < Config.level2CacheMissPercent)
                    {
                        Statistics.level2CacheMisses++;
                        this.remainingCycles += Config.level2CacheMissPenalty;
                    }
                }
            }
        }

        /// <summary>
        /// run a Cycle
        /// 
        /// Counts down the remaining cycles
        /// </summary>
        /// <returns>True if the instruction completes in this cycle</returns>
        public bool runCycle()
        {
            this.remainingCycles--;
            if (this.remainingCycles == 0)
                return true;
            else
                return false;
        }

        



    }
}
