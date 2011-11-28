using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Performs Instruction Decoding
    /// </summary>
    class DecodeStage
    {
        #region Data
        /// <summary>
        /// Holds the Instructions 
        /// </summary>
        private Queue<Instruction> decodeBuffer;
        #endregion

        #region Public Functions
        /// <summary>
        /// Default Constructor
        /// Initializes the decode buffer Queue
        /// </summary>
        public DecodeStage()
        {
            decodeBuffer = new Queue<Instruction>();
        }

        /// <summary>
        /// Perform the Decode Stage
        /// 
        /// CHecks to see if there is a stall on reading more instructions
        /// from the fetch stage due to a previously detected Branch Misprediction
        /// that has not completed the execute stage yet.
        /// 
        /// Then it checks the last two instructions in the fetch stage to see 
        /// if there is branch, and if it was predicted correctly.  This Enables
        /// a stall if necessary
        /// 
        /// Otherwise, copy instructions from the fetch stage
        /// 
        /// </summary>
        public void runCycle()
        {
            // Make sure that we are not in a stall state waiting on
            // a branch instruction to finish executing
            if (CPU.branchMispredictionStall == false)
            {
                // Detect a branch mispredictions
                if (CPU.fetchStage.isBranchMispredict())
                {
                    this.addInstructionToBuffer(CPU.fetchStage.getInstruction()); // get the branch instruction in question
                    CPU.branchMispredictionStall = true;
                }
                else
                {
                    // 2) Get the Instructions from the Fetch Buffer
                    this.getInstructionsFromFetch();
                }
            }
        }

        public Instruction getInstruction()
        {
            return this.decodeBuffer.Dequeue();
        }

        public bool isEmpty()
        {
            return (this.decodeBuffer.Count == 0);
        }


        #endregion

        #region Private Functions

        /// <summary>
        /// Debugging Function
        /// Lets you remove an instruction to test its functionalty
        /// </summary>
        public void testRemoveInstruction()
        {
            this.decodeBuffer.Dequeue();
        }

        /// <summary>
        /// Moves instructions from the fetch Stage buffer to the decode Stage buffer
        /// </summary>
        /// <param name="fetchStage"></param>
        /// <param name="decodeStage"></param>
        private void getInstructionsFromFetch()
        {
            while (this.getEmptySlots() > 0 && !CPU.fetchStage.isEmpty())
            {
                this.addInstructionToBuffer(CPU.fetchStage.getInstruction());
            }
        }

        /// <summary>
        /// Adds and instruction to the decode buffer
        /// </summary>
        /// <param name="instr"></param>
        /// <returns></returns>
        private bool addInstructionToBuffer(Instruction instr)
        {
            if (Config.superScalerFactor - this.decodeBuffer.Count > 0)
            {
                this.decodeBuffer.Enqueue(instr);
                return true;
            }
            else
            {
                System.Console.WriteLine("ERROR Tried to add instruction to decode buffer when it was full!");
                return false;
            }
        }

        /// <summary>
        /// Checks to see how many slots are available in the Decode Buffer
        /// </summary>
        /// <returns>The number of available slots in the Decode Instruction Bffer</returns>
        private int getEmptySlots()
        {
            if (Config.superScalerFactor - this.decodeBuffer.Count < 0)
            {
                System.Console.WriteLine("ERROR: Decode Buffer has too many elements!!");
            }
            return Config.superScalerFactor - this.decodeBuffer.Count;
        }

#endregion

    }
}
