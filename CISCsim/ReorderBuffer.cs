using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    class ReorderBuffer
    {
        private Queue<RobEntry> buffer;

        public ReorderBuffer()
        {
            buffer = new Queue<RobEntry>();
        }


        /// <summary>
        /// Checks to see if the ROB is full or not
        /// </summary>
        /// <returns>true if full, false otherwise</returns>
        public bool isFull()
        {
            return (Config.numReorderBufferEntries - this.buffer.Count > 0 );
        }

        /// <summary>
        /// Add an entry to the reorder buffer
        /// </summary>
        /// <param name="instr">the instruction you need to add an entry for</param>
        public void addEntry(Instruction instr)
        {
            if (Config.numReorderBufferEntries - this.buffer.Count > 0)
            {
                RobEntry newEntry = new RobEntry();
                newEntry.busy = true;
                newEntry.finished = false;
                newEntry.instruction = instr;
                newEntry.valid = this.isNewEntryValid();
                newEntry.renameReg = CPU.rrf.findFirstEmptySlot();
            }
        }

        /// <summary>
        /// Check to see if the new entry will be valid when you put it into the ROB
        /// </summary>
        /// <returns>True if the entry will be valid, false otherwise</returns>
        public bool isNewEntryValid()
        {
            // Check if a branch instruction exists.
            if (buffer.First(entry => entry.instruction.executionType == Instruction.ExecutionType.Branch) == null)
                return true;
            else
                return false;
        }


    }
}
