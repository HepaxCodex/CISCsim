using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    class RRFEntry
    {
        /// <summary>
        /// Denotes that the Current Entry is busy (i.e. do not write over it)
        /// </summary>
        public bool busy;

        /// <summary>
        /// The Data result
        /// </summary>
        public int data;

        /// <summary>
        /// Notes if the data is valid or not
        /// </summary>
        public bool valid;

        /// <summary>
        /// Where the data needs to go
        /// </summary>
        public int destReg;
    }
}
