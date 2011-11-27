using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Perform dispatching to the Issue Stage
    /// </summary>
    class DispatchStage
    {
        /// <summary>
        /// Holds the Instructions 
        /// </summary>
        private Queue<Instruction> dispatchBuffer;

        /// <summary>
        /// Default Constructor
        /// Initializes the dispatch buffer Queue
        /// </summary>
        public DispatchStage()
        {
            dispatchBuffer = new Queue<Instruction>();
        }

        /// <summary>
        /// Perform the Dispatch Stage
        /// </summary>
        public void runCycle(DecodeStage decodeStage, IssueStage issueStage, RenameRegisterFile rrf)
        {
            // 1) Get the Instructions from the decode Buffer
            this.getInstructionsFromDecode(decodeStage);

            // 2) Process each instruction in the decode buffer
            bool stop = false; // Stop once we reach an instruction that can't fit
            while (this.dispatchBuffer.Count > 0 && stop == false)
            {
                Instruction instr = this.dispatchBuffer.Peek();

                // Break if there is no more room in the system
                if (this.systemReadyForInstruction(instr, issueStage, rrf) == false)
                {
                    stop = true;
                    continue;
                }

                instr = this.dispatchBuffer.Dequeue(); // Remove the instruction from the dispatch buffer

                // TODO: We found that we're able to dispatch this instruction.
                // Move the Instruction into its reservation station, into the reorder buffer,
                // and into the rename register file if needed

            }
        }

        /// <summary>
        /// Moves instructions from the decode Stage buffer to the dispatch Stage buffer
        /// </summary>
        /// <param name="decodeStage"></param>
        private void getInstructionsFromDecode(DecodeStage decodeStage)
        {
            while (this.getEmptySlots() > 0 && !decodeStage.isEmpty())
            {
                this.addInstructionToBuffer(decodeStage.getInstruction());
            }
        }

        /// <summary>
        /// Adds and instruction to the dispatch buffer
        /// </summary>
        /// <param name="instr"></param>
        /// <returns></returns>
        private bool addInstructionToBuffer(Instruction instr)
        {
            if (Config.superScalerFactor - this.dispatchBuffer.Count > 0)
            {
                this.dispatchBuffer.Enqueue(instr);
                return true;
            }
            else
            {
                System.Console.WriteLine("ERROR Tried to add instruction to dispatch buffer when it was full!");
                return false;
            }
        }

        /// <summary>
        /// Checks to see how many slots are available in the Decode Buffer
        /// </summary>
        /// <returns>The number of available slots in the Decode Instruction Bffer</returns>
        private int getEmptySlots()
        {
            if (Config.superScalerFactor - this.dispatchBuffer.Count < 0)
            {
                System.Console.WriteLine("ERROR: Dispatch Buffer has too many elements!!");
            }
            return Config.superScalerFactor - this.dispatchBuffer.Count;
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
    }
}
