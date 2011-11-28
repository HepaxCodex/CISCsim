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
        public void ReceiveInstruction(Instruction instr)
        {

        }
    }
}
