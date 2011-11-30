using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Represents the Rename Register File in a SuperScaler CPU
    /// </summary>
    class RenameRegisterFile
    {

        public RRFEntry[] rrfTable;

        public RenameRegisterFile()
        {
            this.rrfTable = new RRFEntry[Config.numRenamingTableEntries];
            for (int i = 0; i < Config.numRenamingTableEntries; i++)
                rrfTable[i] = new RRFEntry();
        }


        /// <summary>
        /// Check to see if there is an available entry
        /// </summary>
        /// <returns>True if there is a slot avialable, false otherwise</returns>
        public bool spaceAvailable()
        {
            foreach (RRFEntry entry in this.rrfTable)
            {
                if (entry.busy == false)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Adds an entry to the rrfTable
        /// </summary>
        /// <param name="instr">Instruction to be added to the rrfTable</param>
        /// <returns>The index (tag) of where it was added</returns>
        public int addEntry(Instruction instr)
        {
            int rrfTag = -1;
            // We had to create one here because we use emptySlot below
            RRFEntry emptySlot = new RRFEntry();

            // Note we're guaranteed an entry is open because we called spaceAvailable
            // at a higher level
            for (int i = 0; i < Config.numRenamingTableEntries; i++)
            {
                if (rrfTable[i].busy == false)
                {
                    emptySlot = rrfTable[i];
                    rrfTag = i;
                }
            }

            emptySlot.busy = true;
            emptySlot.valid = false;
            // There must be a destination if this instruction requires an RRF entry
            emptySlot.destReg = instr.dest;
            // Data stays null until it's updated by an execution result

            return rrfTag;
        }


        public int findFirstEmptySlot()
        {
            return Array.IndexOf(rrfTable, rrfTable.First(entry => entry.busy == false));
        }

        public void executionFinished(int rrfIndex)
        {
            this.rrfTable[rrfIndex].valid = true;
        }

        // TODO : Update Busy after Complete
        // Put thedata into the ARF
        // set rrf entry to not busy
    }
}
