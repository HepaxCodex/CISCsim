using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    class RobEntry
    {
        /// <summary>
        /// Us the current location in use
        /// </summary>
        public bool busy;

        /// <summary>
        /// The Instruction being reordered
        /// </summary>
        public Instruction instruction;

        /// <summary>
        /// Is the current bit valid based off of branches?
        /// </summary>
        public bool valid;

        /// <summary>
        /// The Rename Register Entry Number associated with this ROB entry
        /// </summary>
        public int renameReg;

        /// <summary>
        /// Is the current INstruction finsihed?
        /// </summary>
        public bool finished;


    }
}
