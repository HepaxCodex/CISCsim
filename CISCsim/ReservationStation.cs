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
        //TODO: Add Entries to the REservation Station
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
        /// </summary>
        public void ReceiveInstruction(Instruction instr, int robTag)
        {
            // TODO: 1) Create res station entry by checking ARF for instr's src1 and src2
            // If ARF entry Busy = false, data is valid, put it in as opN, set validN true
            // Else ARF entry Busy = true, take ARF tag to check RRF entry.
            // If RRF entry valid = true, data is valid, put it in as opN, set validN true
            // Else RRF entry valid = false, opN gets the RRF entry tag, set validN false
            int src1 = instr.source1;
            int src2 = instr.source2;

            //instr.executionType.

            //CPU.arf.regFile
        }
    }
}
