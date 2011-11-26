using System;
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
        /// Array that tells which slots in the fetchBuffer are in use
        /// </summary>
        //private BitArray fetchBufferIndexValid;

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

        public FetchStage()
        {
        }

        /// <summary>
        /// Creates a new FetchStage with a fetchBuffer of superScalarWidth width.
        /// 
        /// </summary>
        public FetchStage(string instructionTraceFilename)
        {
            this.cacheMissed = false;
            this.cacheMissCountdown = 0;
            this.fetchBuffer = new Queue<Instruction>();

            //this.fetchBufferIndexValid = new BitArray(superScalarWidth);
            try
            {
                this.traceReader = new StreamReader(instructionTraceFilename);
            }
            catch (Exception e)
            {
                Console.WriteLine("FetchStage: The file \"{0}\" could not be read:", instructionTraceFilename);
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
                if (rand.Next(100) < Config.level1CacheMissPercent)
                {
                    Statistics.level1CacheMisses++;

                    //We did miss cache; set cacheMiss = true and the appropriate cacheMissCountdown
                    this.cacheMissed = true;
                    this.cacheMissCountdown = Config.level1CacheMissPenalty;

                    //Check for Level 2 Cache miss
                    if (rand.Next(100) < Config.level2CacheMissPercent)
                    {
                        Statistics.level2CacheMisses++;

                        //Update cache miss countdown to be main memory access
                        this.cacheMissCountdown = Config.level2CacheMissPenalty;
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

                    fetchBuffer.Enqueue(new Instruction(line));
                }
                else
                {
                    // TODO: end of the file - do something now?
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

    }
}
