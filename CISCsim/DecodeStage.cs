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
        /// </summary>
        public void runCycle()
        {
            // 1) Check to see if there is a branch mispredict
            // Note that this is easier to perform here than in the fetch stage

            
            // 2) Get the Instructions from the Fetch Buffer
            this.getInstructionsFromFetch();
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
