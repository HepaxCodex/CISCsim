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
        public ReservationStationEntry[] buffer;

        /// <summary>
        /// Default Constructor: Creates a ReservationStation in the size defined by the Config
        /// </summary>
        public ReservationStation()
        {
            this.buffer = new ReservationStationEntry[Config.numReservationStations];
            for (int i = 0; i < Config.numReservationStations; i++)
                this.buffer[i] = new ReservationStationEntry();
        }

        /// <summary>
        /// This Constructor is used to specify a number of Entries
        /// </summary>
        /// <param name="numReservationStations">Desired number of Entries</param>
        public ReservationStation(int numReservationStations)
        {
            this.buffer = new ReservationStationEntry[numReservationStations];
            for (int i = 0; i < numReservationStations; i++)
                this.buffer[i] = new ReservationStationEntry();
        }

        /// <summary>
        /// Determines if the ReservationStation on Full or not
        /// </summary>
        /// <returns>True if full, false otherwise</returns>
        public bool isFull()
        {
            bool full = true; // Start off true until we find a non-"busy" entry
            foreach (ReservationStationEntry entry in this.buffer)
            {
                if (entry.busy == false)
                {
                    full = false;
                }
            }
            return full;
        }



    }
}
