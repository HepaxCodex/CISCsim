using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Contains the Config details
    /// </summary>
    static class Config
    {
        public int superScalerFactor          = 5;
        public int numReservationStations     = 10;
        public int numFunctionalUnits         = 4; // Applies to everything except branch units
        public int numRenamingTableEntries    = 10;
        public int numReaderBufferEntries     = 10;

    }
}
