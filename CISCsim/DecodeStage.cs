using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Performs Instruction Decoding and Dispatching to the Issue Stage
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
        public void runCycle(FetchStage fetchStage, IssueStage issueStage, RenameRegisterFile rrf)
        {
            // 1) Get the Instructions from teh Fetch Buffer
            this.getInstructionsFromFetch(fetchStage);

            // 2) Process each instruction in the decode buffer
            bool stop = false; // Stop once we reach an instruction that can't fit
            while( this.decodeBuffer.Count > 0 && stop == false)
            {
                Instruction instr = this.decodeBuffer.Peek();

                // Break if there is no more room in the system
                if (this.systemReadyForInstruction(instr, issueStage, rrf) == false)
                {
                    stop = true;
                    continue;
                }

                instr = this.decodeBuffer.Dequeue(); // Remove the instruction from the decode buffer

                // TODO: Move the Instruction into the other buffers

            }

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
        private void getInstructionsFromFetch(FetchStage fetchStage)
        {
            while (this.getEmptySlots() > 0 && !fetchStage.isEmpty())
            {
                this.addInstructionToBuffer(fetchStage.getInstruction());
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
                System.Console.WriteLine("ERROR: Decide Buffer has too many elements!!");
            }
            return Config.superScalerFactor - this.decodeBuffer.Count;
        }

        
        /// <summary>
        /// Determines the Instruction type and checks the appropriate Reservation Station for space
        /// </summary>
        /// <param name="instr">The Instrucion that needs moved</param>
        /// <param name="issueStage">The Issue Stage that will handle the instruction</param>
        /// <returns>True if there is space available, false otherwise</returns>
        private bool isReservationStationAvaialble(Instruction instr, IssueStage issueStage)
        {
           switch (instr.executionType)
                {
                    case Instruction.ExecutionType.Logical:
                    case Instruction.ExecutionType.Integer:
                        if (issueStage.integerStation.isFull()) return false;
                        break;
                    case Instruction.ExecutionType.FloatingPoint:
                        if (issueStage.fpStation.isFull()) return false;
                        break;
                    case Instruction.ExecutionType.Mem:
                        if (issueStage.memStation.isFull()) return false;
                        break;
                    case Instruction.ExecutionType.MultDiv:
                        if (issueStage.multDivStation.isFull()) return false;
                        break;
                    case Instruction.ExecutionType.Branch:
                        if (issueStage.branchStation.isFull()) return false;
                        break;
                    case Instruction.ExecutionType.Nop:
                        //TODO: Decide what to do with Nop in Decode Stage
                        break;
                    default:
                        System.Console.WriteLine("Decode Stage found Unknown Instruction Execution Type");
                        break;
                }
           return true;

        }


        /// <summary>
        /// Checks to see if there is space in all appropriate buffers for the instruction
        /// </summary>
        /// <param name="instr">Instruction to be moved</param>
        /// <param name="issueStage">Issue Stage for instruction</param>
        /// <param name="rrf">Rename Register File</param>
        /// <returns>true if the system is ready, false otherwise</returns>
        private bool systemReadyForInstruction(Instruction instr, IssueStage issueStage, RenameRegisterFile rrf)
        {
            //TODO: Check ROB

            // Check to see if there is space available in the reservation stations
            if (this.isReservationStationAvaialble(instr, issueStage) == false)
            {
                Statistics.reservationStationFull++;
                return false;
            }

            // Check to see if there is space available in the rrf
            if (rrf.spaceAvailable() == false)
            {
                Statistics.registerRenameFileFull++;
                return false;
            }

            return true;
        }

#endregion


    }
}
