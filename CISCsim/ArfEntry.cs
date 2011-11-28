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
        /// Normal register entries only use data.
        /// HILO will be the only one that uses data_lo. hi is stored in data
        /// </summary>
        public int data_lo;

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
