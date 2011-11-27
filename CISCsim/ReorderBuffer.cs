using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    class ReorderBuffer
    {
        private Queue<RobEntry> buffer;

        public ReorderBuffer()
        {
            buffer = new Queue<RobEntry>();
        }


        /// <summary>
        /// Checks to see if the ROB is full or not
        /// </summary>
        /// <returns>true if full, false otherwise</returns>
        public bool isFull()
        {
            return (Config.numReorderBufferEntries - this.buffer.Count >0 );
        }



    }
}
