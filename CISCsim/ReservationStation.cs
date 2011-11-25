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
            buffer = new ReservationStationEntry[Config.numReservationStations];
        }

        /// <summary>
        /// Determines if the ReservationStation on Full or not
        /// </summary>
        /// <returns>True if full, false otherwise</returns>
        public bool isFull()
        {
            bool full = true; // Start off true until we find a non-"busy" entry
            foreach (ReservationStationEntry entry in buffer)
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
