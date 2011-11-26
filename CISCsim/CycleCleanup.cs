using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    static class CycleCleanup
    {

        /// <summary>
        /// Moves instructions from the fetch Stage buffer  to the decode Stage buffer
        /// </summary>
        /// <param name="fetchStage"></param>
        /// <param name="decodeStage"></param>
        public static void fetch2Decode(FetchStage fetchStage, DecodeStage decodeStage)
        {
            while (decodeStage.getEmptySlots() > 0 && !fetchStage.isEmpty())
            {
                decodeStage.addInstructionToBuffer(fetchStage.getInstruction());
            }
        }


    }
}
