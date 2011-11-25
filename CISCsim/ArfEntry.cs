using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{

    /// <summary>
    /// Represents a Single Entry in the ARF
    /// </summary>
    class ArfEntry
    {
        /// <summary>
        /// Actual Data Value
        /// </summary>
        public int data;

        /// <summary>
        /// If the current data is busy, it is NOT valid
        /// </summary>
        public bool busy;

        /// <summary>
        /// Entry in the Register Rename File 
        /// </summary>
        public int tag;

    }
}
