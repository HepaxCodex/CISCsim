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
        public void runCycle()
        {
            // 1) Get the Instructions from the decode Buffer
            this.getInstructionsFromDecode();

            // 2) Process each instruction in the dispatch buffer
            bool stop = false; // Stop once we reach an instruction that can't fit
            while (this.dispatchBuffer.Count > 0 && stop == false)
            {
                Instruction instr = this.dispatchBuffer.Peek();

                // Break if there is no more room in the RRF or ROB or ResStation
                if (this.systemReadyForInstruction(instr) == false)
                {
                    stop = true;
                    continue;
                }

                instr = this.dispatchBuffer.Dequeue(); // Remove the instruction from the dispatch buffer

                // We found that we're able to dispatch this instruction.
                // Move the Instruction into its reservation station, into the reorder buffer,
                // and into the rename register file if needed
                int robTag;
                
                robTag = dispatchToReorderBuffer(instr);
                dispatchToReservationStation(instr, robTag);

                // TODO: check to see if this instruction needs a rename register file entry
                // and dispatch it to the rrf if it does.
            }
        }


        /// <summary>
        /// Puts the instruction into the reorder buffer
        /// </summary>
        ///  <returns>The tag of the entry used in the reorder buffer</returns>
        private int dispatchToReorderBuffer(Instruction instr)
        {
            CPU.rob.addEntry(instr);

            // TODO: We're using a queue for the ROB, so we don't really have a tag
            // to the entries within it. Do we have to change this?
            return -1;
        }

        /// <summary>
        /// Puts the instruction into the appropriate reservation station (located in the issue stage)
        /// </summary>
        private void dispatchToReservationStation(Instruction instr, int robTag)
        {
            switch (instr.executionType)
            {
                case Instruction.ExecutionType.Branch:
                    CPU.issueStage.branchStation.ReceiveInstruction(instr, robTag);
                    break;
                case Instruction.ExecutionType.FloatingPoint:
                    CPU.issueStage.fpStation.ReceiveInstruction(instr, robTag);
                    break;
                case Instruction.ExecutionType.Integer:
                    CPU.issueStage.integerStation.ReceiveInstruction(instr, robTag);
                    break;
                case Instruction.ExecutionType.Mem:
                    CPU.issueStage.memStation.ReceiveInstruction(instr, robTag);
                    break;
                case Instruction.ExecutionType.MultDiv:
                    CPU.issueStage.multDivStation.ReceiveInstruction(instr, robTag);
                    break;
                case Instruction.ExecutionType.Nop:
                    break;
            }
        }

        /// <summary>
        /// Moves instructions from the decode Stage buffer to the dispatch Stage buffer
        /// </summary>
        /// <param name="decodeStage"></param>
        private void getInstructionsFromDecode()
        {
            while (this.getEmptySlots() > 0 && !CPU.decodeStage.isEmpty())
            {
                this.addInstructionToBuffer(CPU.decodeStage.getInstruction());
            }
        }

        /// <summary>
        /// Adds and instruction to the dispatch buffer
        /// </summary>
        /// <param name="instr"></param>
        /// <returns></returns>
        private bool addInstructionToBuffer(Instruction instr)
        {
            if (Config.superScalerFactor - this.dispatchBuffer.Count() > 0)
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
            if (Config.superScalerFactor - this.dispatchBuffer.Count() < 0)
            {
                System.Console.WriteLine("ERROR: Dispatch Buffer has too many elements!!");
            }
            return Config.superScalerFactor - this.dispatchBuffer.Count();
        }

        /// <summary>
        /// Determines the Instruction type and checks the appropriate Reservation Station for space
        /// </summary>
        /// <param name="instr">The Instrucion that needs moved</param>
        /// <param name="issueStage">The Issue Stage that will handle the instruction</param>
        /// <returns>True if there is space available, false otherwise</returns>
        private bool isReservationStationAvaialble(Instruction instr)
        {
            switch (instr.executionType)
            {
                case Instruction.ExecutionType.Logical:
                case Instruction.ExecutionType.Integer:
                    if (CPU.issueStage.integerStation.isFull()) return false;
                    break;
                case Instruction.ExecutionType.FloatingPoint:
                    if (CPU.issueStage.fpStation.isFull()) return false;
                    break;
                case Instruction.ExecutionType.Mem:
                    if (CPU.issueStage.memStation.isFull()) return false;
                    break;
                case Instruction.ExecutionType.MultDiv:
                    if (CPU.issueStage.multDivStation.isFull()) return false;
                    break;
                case Instruction.ExecutionType.Branch:
                    if (CPU.issueStage.branchStation.isFull()) return false;
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
        private bool systemReadyForInstruction(Instruction instr)
        {
            //TODO: Check ROB
            if (CPU.rob.isFull())
            {
                Statistics.reorderBufferFull++;
                return false;
            }
            
            // Check to see if there is space available in the reservation stations
            if (this.isReservationStationAvaialble(instr) == false)
            {
                Statistics.reservationStationFull++;
                return false;
            }

            // Check to see if there is space available in the rrf
            if (CPU.rrf.spaceAvailable() == false)
            {
                Statistics.registerRenameFileFull++;
                return false;
            }

            return true;
        }
    }
}
