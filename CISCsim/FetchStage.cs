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
        private Instruction[] fetchBuffer;

        /// <summary>
        /// Array that tells which slots in the fetchBuffer are in use
        /// </summary>
        private BitArray fetchBufferIndexValid;

        /// <summary>
        /// Holds the superscalar width used in construction
        /// </summary>
        private int ssw;

        /// <summary>
        /// Holds the stream reader used to read from the instruction trace file
        /// </summary>
        private StreamReader traceReader;

        public FetchStage()
        {
        }

        /// <summary>
        /// Creates a new FetchStage with a fetchBuffer of superScalarWidth width.
        /// 
        /// </summary>
        public FetchStage(int superScalarWidth, string instructionTraceFilename)
        {
            this.ssw = superScalarWidth;
            this.fetchBuffer = new Instruction[superScalarWidth];
            this.fetchBufferIndexValid = new BitArray(superScalarWidth);
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
            int[] openSlotIndices;
            String line;

            openSlotIndices = new int[this.fetchBufferIndexValid.Count];

            // Check to see how many open slots there are
            // and keep track of which ones are open
            for(int i = 0; i < this.fetchBufferIndexValid.Count; i++)
            {
                if (this.fetchBufferIndexValid[i] == false)
                {
                    openSlotIndices[numSlotsOpen] = i;
                    numSlotsOpen++;
                }
            }
            
            // Read in numSlotsOpen instructions from the trace file.
            for(int i = 0; i < numSlotsOpen; i++)
            {
                line = this.traceReader.ReadLine();
                if (line != null)
                {
                    numInstructionsRead++;

                    fetchBuffer[openSlotIndices[i]] = new Instruction(line);
                }
                else
                {
                    // TODO: end of the file - do something now?
                }
            }
            return numInstructionsRead;
        } //end Fetch()
    }
}
