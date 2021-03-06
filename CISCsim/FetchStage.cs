﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace CISCsim
{
    /// <summary>
    /// Defines the fetch stage.
    /// Has an array of instructions called fetchBuffer.
    /// </summary>
    class FetchStage
    {
        // TODO: When the fetchBuffer gets copied to the decode stage, we need to check
        // which ones can move and update fetchBufferIndexValid appropriately

        /// <summary>
        /// Holds the instructions fetched from the instruction trace
        /// </summary>
        //private Instruction[] fetchBuffer;
        private Queue<Instruction> fetchBuffer;

        /// <summary>
        /// Holds the stream reader used to read from the instruction trace file
        /// </summary>
        private StreamReader traceReader;

        /// <summary>
        /// How many more cycles to wait before fetching because we missed cache.
        /// </summary>
        private int cacheMissCountdown;

        /// <summary>
        /// Tells if we missed cache on the last time we tried to fetch
        /// </summary>
        private bool cacheMissed;

        /// <summary>
        /// Creates a new FetchStage with a fetchBuffer of superScalarWidth width.
        /// </summary>
        public FetchStage()
        {
            this.cacheMissed = false;
            this.cacheMissCountdown = 0;
            this.fetchBuffer = new Queue<Instruction>();

            //this.fetchBufferIndexValid = new BitArray(superScalarWidth);
            try
            {
                this.traceReader = new StreamReader(Config.traceFilename);
            }
            catch (Exception e)
            {
                Console.WriteLine("FetchStage: The file \"{0}\" could not be read:", Config.traceFilename);
                Console.WriteLine("FetchStage: " + e.Message);
            }
        }

        /// <summary>
        /// Fetches as many instructions from the instruction trace as it can process
        /// up to super scalar width. Returns how many instructions it fetched.
        /// </summary>
        public int Fetch()
        {
            int numInstructionsRead = 0;
            int numSlotsOpen = 0;
            String line;
            Random rand = new Random();

            if (this.cacheMissed == false)
            {
                // We didn't miss the cache last time, so we have to see if we miss this time
                if (rand.Next(100) < Config.level1CacheInstrMissPercent)
                {
                    Statistics.level1InstrCacheMisses++;

                    //We did miss cache; set cacheMiss = true and the appropriate cacheMissCountdown
                    this.cacheMissed = true;
                    this.cacheMissCountdown = Config.level1CacheMissPenalty;

                    //Check for Level 2 Cache miss
                    if (rand.Next(100) < Config.level2CacheMissPercent)
                    {
                        Statistics.level2CacheMisses++;

                        //Update cache miss countdown to be main memory access plus the L2 access
                        this.cacheMissCountdown += Config.level2CacheMissPenalty;
                    }
                    return 0;
                }
            }
            else
            {
                // We did miss cache. If cacheMissCountdown is > 0, we have to wait more cycles, so we return.
                // Otherwise, we can move on and fetch.
                if (this.cacheMissCountdown > 0)
                {
                    this.cacheMissCountdown--;
                    return 0;
                }
            }

            numSlotsOpen = Config.superScalerFactor - this.fetchBuffer.Count;
            
            // Read in numSlotsOpen instructions from the trace file.
            for(int i = 0; i < numSlotsOpen; i++)
            {
                line = this.traceReader.ReadLine();
                if (line != null)
                {
                    numInstructionsRead++;
                    Instruction newInstruction = new Instruction(line);
                    CPU.pc = newInstruction.address;
                    CPU.pc_count++;
                    if(newInstruction.executionType != Instruction.ExecutionType.Nop)
                        fetchBuffer.Enqueue(newInstruction);
                }
                else
                {
                    // ensures that this is onle performed onces
                    if (CPU.lastInstructionFetched == false)
                    {
                        if (this.fetchBuffer.Count > 0)
                        {
                            fetchBuffer.Last().isLastInstruction = true;
                        }
                        else
                        {
                            CPU.decodeStage.decodeBuffer.Last().isLastInstruction = true;
                        }
                        CPU.lastInstructionFetched = true;
                    }
                }
            }

            // Reset cache miss
            this.cacheMissed = false;

            return numInstructionsRead;
        } //end Fetch()


        /// <summary>
        /// Empties out the Fetch Queue
        /// </summary>
        public void Clear()
        {
            while (fetchBuffer.Count > 0)
            {
                fetchBuffer.Dequeue();
            }
        }

        public bool isEmpty()
        {
            return (this.fetchBuffer.Count == 0);
        }

        public Instruction getInstruction()
        {
            return this.fetchBuffer.Dequeue();
        }

        public Instruction peekInstruction()
        {
            return this.fetchBuffer.Peek();
        }

        /// <summary>
        /// Checks to see if there is a branch mispredict
        /// </summary>
        /// <returns>true if there is was mispredict</returns>
        public bool isBranchMispredict()
        {
            if (this.fetchBuffer.Count >= 2)
            {
                Instruction instr = this.fetchBuffer.Peek();

                bool branchPrediction = CPU.branchPredictor.predictBranch(instr.address);
                bool actualResult;
                Instruction next = this.fetchBuffer.First(entry => entry != instr);

                // if next pc == first pc +8 , branch not taken
                if (next.address == instr.address + 8)
                {
                    actualResult = false;
                }
                else
                {
                    actualResult = true;
                }

                CPU.branchPredictor.updateBranchSM(instr.address, branchPrediction, actualResult);

                return (branchPrediction == actualResult);
            }
            return false;
        }

        /// <summary>
        /// Checks against two instructions to see if there is a branch mispredict
        /// </summary>
        /// <returns>true if there is was mispredict</returns>
        public bool isBranchMispredict(Instruction first, Instruction next)
        {
            if (!first.isABranch())
            {
                return false;
            }

            // If we got here, instr is a branch instruction
            bool branchPrediction = CPU.branchPredictor.predictBranch(first.address);
            bool actualResult;

            // if next pc == first pc +8 , branch not taken
            if (next.address == first.address + 8)
            {
                actualResult = false;
            }
            else
            {
                actualResult = true;
            }

            CPU.branchPredictor.updateBranchSM(first.address, branchPrediction, actualResult);
            
            // Fixed this...it's an isBranchMISpredict function,
            // so if branchPrediction != actualResult, you mispredicted--that is, return true
            return (branchPrediction != actualResult);
        }

    }
}
