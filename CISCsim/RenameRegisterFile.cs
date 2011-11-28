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
        public void addEntry(Instruction instr)
        {
            RRFEntry emptySlot;
            emptySlot = rrfTable.First(entry => entry.busy == false);
            emptySlot.busy = true;

            //TODO: Setup other fields
        }

        public int findFirstEmptySlot()
        {
            return Array.IndexOf(rrfTable, rrfTable.First(entry => entry.busy == false));
        }

    }
}
