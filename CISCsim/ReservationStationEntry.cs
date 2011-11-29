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
        public int robTag;

        /// <summary>
        /// The instruction waiting to be executed
        /// This is only included here for ease of use with the Execution Stage
        /// </summary>
        public Instruction instr;
        
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
            this.robTag = -1;
        }

        /// <summary>
        /// Constructor using initializers
        /// </summary>
        public ReservationStationEntry(int op1, bool valid1, int op2, bool valid2, int robTag, Instruction instr)
        {
            this.op1 = op1;
            this.valid1 = valid1;
            this.op2 = op2;
            this.valid2 = valid2;
            this.robTag = robTag;
            this.instr = instr;

            this.ready = (this.valid1 && this.valid2);

            this.busy = true;
        }
    }
}
