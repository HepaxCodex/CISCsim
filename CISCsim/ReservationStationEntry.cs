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

        /// <summary>
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
        /// Entry in the Reorder Buffer
        /// </summary>
        public int tag;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ReservationStationEntry()
        {
            this.busy = false;
            this.op1 = -1;
            this.op2 = -1;
            this.valid1 = false;
            this.valid2 = false;
            this.ready = false;
            this.tag = -1;
        }

        /// <summary>
        /// Constructor using initializers
        /// </summary>
        public ReservationStationEntry(int blah)
        {
            // TODO: create this constructor having initializers for all fields
        }
    }
}
