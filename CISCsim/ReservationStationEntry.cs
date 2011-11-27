using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    class ReservationStationEntry
    {
        /// <summary>
        /// is the Current Entry in Use?
        /// </summary>
        public bool busy;

        /// <psummary>
        /// operand 1
        /// </summary>
        public int op1;

        /// <summary>
        /// is operand 1 valid?
        /// </summary>
        public bool valid1;

        /// <summary>
        /// operand 2
        /// </summary>
        public int op2;

        /// <summary>
        /// is operand 2 valid?
        /// </summary>
        public bool valid2;

        /// <summary>
        /// is the current entry ready to be executed? 
        /// </summary>
        public bool ready;

        /// <summary>
        /// Entry in the Register Rename File
        /// </summary>
        public int tag;
        
    }
}
