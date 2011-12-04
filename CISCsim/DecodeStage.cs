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
        public Queue<Instruction> decodeBuffer;

        private Instruction lastInstruction;
        private bool lastInstructionWasBranch;
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
        /// Checks to see if there is a stall on reading more instructions
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
            if(CPU.fetchStage.isEmpty() || this.isFull())
            {
                // Nothing to do here, fetch stage empty or we're full
                return;
            }

            // Make sure that we are not in a stall state waiting on
            // a branch instruction to finish executing
            if (CPU.branchMispredictionStall == false)
            {
                // Was the last instruction on the last cycle a branch? If yes, we need to check against our
                // member variable holding the info from that last instruction
                if (this.lastInstructionWasBranch == true)
                {
                    Instruction firstInstr = CPU.fetchStage.peekInstruction();
                    if (true == CPU.fetchStage.isBranchMispredict(this.lastInstruction, firstInstr))
                    {
                        //branch misprediction!
                        CPU.branchMispredictionStall = true;
                    }
                    else
                    {
                        // No misprediction between fetches, we're good to continue as normal
                    }
                    // Reset this
                    this.lastInstructionWasBranch = false;
                }

                // What we want here is to check each instruction one at a time to see if it's a branch instruction.
                // If it isn't, read it from the fetch buffer into the decode buffer.
                // If it is, is it the last instruction? If it's the last instruction, set our member variables
                // accordingly so we can check next time around
                while (!CPU.fetchStage.isEmpty())
                {
                    Instruction currInstr = CPU.fetchStage.getInstruction();
                    this.addInstructionToBuffer(currInstr);

                    if (!currInstr.isABranch())
                    {
                        if (this.isFull())
                            break;
                    }
                    else
                    {
                        // It's a branch; if it's the last instruction in the fetch buffer, we'll need to check
                        // for a mispredict next time around
                        if (CPU.fetchStage.isEmpty())
                        {
                            this.lastInstruction = currInstr;
                            this.lastInstructionWasBranch = true;
                        }
                        else
                        {
                            // It's not the last instruction, check for mispredict
                            if (true == CPU.fetchStage.isBranchMispredict(currInstr, CPU.fetchStage.peekInstruction()))
                            {
                                //branch misprediction!
                                CPU.branchMispredictionStall = true;
                            }

                            // We may be full now; if we are, break out
                            if (this.isFull())
                                break;
                        }
                    }
                }
                // We should have all the instructions from the fetch buffer now in the decode buffer
            } // End if(CPU.branchMispredictionStall == false)
        } // End DecodeStage runCycle()

        public Instruction getInstruction()
        {
            return this.decodeBuffer.Dequeue();
        }

        public bool isEmpty()
        {
            return (this.decodeBuffer.Count == 0);
        }

        public bool isFull()
        {
            return ( (Config.superScalerFactor - this.decodeBuffer.Count) == 0 );
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
