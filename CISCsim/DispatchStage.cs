using System;
using System.Collections;
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
        private Instruction[] dispatchBuffer;
        private BitArray dispatchBufferIndexValid;

        /// <summary>
        /// Default Constructor
        /// Initializes the dispatch buffer array
        /// </summary>
        public DispatchStage()
        {
            dispatchBuffer = new Instruction[Config.superScalerFactor];
            dispatchBufferIndexValid = new BitArray(Config.superScalerFactor, false);
        }

        /// <summary>
        /// Perform the Dispatch Stage
        /// </summary>
        public void runCycle(DecodeStage decodeStage, IssueStage issueStage, RenameRegisterFile rrf)
        {
            // 1) Get the Instructions from the decode Buffer
            this.getInstructionsFromDecode(decodeStage);

            // 2) Process each instruction in the dispatch buffer
            //search through each instruction and dispatch it if possible
            for (int i = 0; i < Config.superScalerFactor; i++)
            {
                //If this entry contains an instruction...
                if (dispatchBufferIndexValid[i] == true)
                {
                    Instruction instr = dispatchBuffer[i];

                    // Moved the check for if there's room in a reservation station out of
                    // systemReadyForInstruction() to here -btf
                    if (this.isReservationStationAvaialble(instr, issueStage) == false)
                    {
                        continue;
                    }

                    // Break if there is no more room in the RRF or ROB
                    if (this.systemReadyForInstruction(instr, issueStage, rrf) == false)
                    {
                        break;
                    }

                    dispatchBufferIndexValid[i] = false; // Remove the instruction from the dispatch buffer

                    // TODO: We found that we're able to dispatch this instruction.
                    // Move the Instruction into its reservation station, into the reorder buffer,
                    // and into the rename register file if needed
                }
            }
        }

        /// <summary>
        /// Gets the number of instructions in the Dispatch buffer
        /// </summary>
        private int getDispatchBufferCount()
        {
            int count = 0;
            for (int i = 0; i < dispatchBufferIndexValid.Count; i++)
            {
                if (dispatchBufferIndexValid[i] == true)
                {
                    count++;
                }
            }
            return count;
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
            if (Config.superScalerFactor - this.getDispatchBufferCount() > 0)
            {
                for (int i = 0; i < Config.superScalerFactor; i++)
                {
                    if (this.dispatchBufferIndexValid[i] == false)
                    {
                        dispatchBuffer[i] = instr;
                        dispatchBufferIndexValid[i] = true;
                        break;
                    }
                }
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
            if (Config.superScalerFactor - this.getDispatchBufferCount() < 0)
            {
                System.Console.WriteLine("ERROR: Dispatch Buffer has too many elements!!");
            }
            return Config.superScalerFactor - this.getDispatchBufferCount();
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

            // Took this out of here; We still need to check if a reservation station
            // is available, but we shouldn't stop checking if other instructions could
            // be dispatched if there's no reservation station available for this instr. -btf
            /*
            // Check to see if there is space available in the reservation stations
            if (this.isReservationStationAvaialble(instr, issueStage) == false)
            {
                Statistics.reservationStationFull++;
                return false;
            }
            */

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
