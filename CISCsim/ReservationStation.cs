using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Represents a Reservation Station
    /// </summary>
    class ReservationStation
    {
        private int maxQueueSize;

        public Queue<ReservationStationEntry> buffer;

        /// <summary>
        /// Default Constructor: Creates a ReservationStation in the size defined by the Config
        /// </summary>
        public ReservationStation()
        {
            this.buffer = new Queue<ReservationStationEntry>();
            this.maxQueueSize = Config.numReservationStationEntries;
        }

        /// <summary>
        /// This Constructor is used to specify a number of Entries
        /// </summary>
        /// <param name="numReservationStations">Desired number of Entries</param>
        public ReservationStation(int numReservationStations)
        {
            this.maxQueueSize = numReservationStations;
        }

        /// <summary>
        /// Determines if the ReservationStation on Full or not
        /// </summary>
        /// <returns>True if full, false otherwise</returns>
        public bool isFull()
        {
            return (this.maxQueueSize - this.buffer.Count == 0);

            /*This method is technically correct but unnecessary in the implimentation
            bool full = true; // Start off true until we find a non-"busy" entry
            foreach (ReservationStationEntry entry in this.buffer)
            {
                if (entry.busy == false)
                {
                    full = false;
                }
            }
             
            return full;
             * */
        }

        /// <summary>
        /// Puts the instruction into the reservation station, filling the entries
        /// Note this does not check if ResStation is full, as that check is done at a higher level
        /// </summary>
        public void ReceiveInstruction(Instruction instr, int robTag)
        {
            // The reservation station entry we're creating
            ReservationStationEntry thisEntry;
            
            // The fields in the entry
            int op1 = 0, op2 = 0;
            bool valid1 = false, valid2 = false;

            SetUpOp(ref op1, ref valid1, instr.source1, instr.source1Imm);
            SetUpOp(ref op2, ref valid2, instr.source2, instr.source2Imm);

            thisEntry = new ReservationStationEntry(op1, valid1, op2, valid2, robTag);

            this.buffer.Enqueue(thisEntry);
        } // End ReceiveInstruction

        /// <summary>
        /// Sets the arguments op and valid for the reservation station entry
        /// </summary>
        private void SetUpOp(ref int op, ref bool valid, int source, bool imm)
        {
            // 1) If source is immediate, set it as op and valid = true
            // 2) Else, check ARF for source
            //    If ARF entry Busy = false, data is valid, put it in as opN, set validN true
            // 3) Else ARF entry Busy = true, take ARF tag to check RRF entry.
            // 4) If RRF entry valid = true, data is valid, put it in as opN, set validN true
            // 5) Else RRF entry valid = false, opN gets the RRF entry tag, set validN false

            // If the source is immediate, use it
            if (imm == true)
            {
                op = source;
                valid = true;

                return;
            }
            else
            {
                // The source is a register, so if that register is busy, check the tag, otherwise use it
                if (CPU.arf.regFile[source].busy == true)
                {
                    // This reg is busy, look for the data in the rename reg file
                    int rrfTag = CPU.arf.regFile[source].tag;

                    // If the rrf entry is valid, use it, otherwise, use rrfTag as the op
                    if (CPU.rrf.rrfTable[rrfTag].valid == true)
                    {
                        op = CPU.rrf.rrfTable[rrfTag].data;
                        valid = true;
                        return;
                    }
                    else
                    {
                        op = rrfTag;
                        valid = false;
                        return;
                    }
                }
                else
                {
                    op = CPU.arf.regFile[source].data;
                    valid = true;
                    return;
                }
            }
        } // End SetUpOp
    }
}
