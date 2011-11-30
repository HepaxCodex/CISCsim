using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    class CompleteStage
    {

        public void runCycle()
        {
            if (CPU.rob.buffer.Count != 0)
            {
                while (CPU.rob.buffer.Peek().finished == true)
                {
                    RobEntry robEntry = CPU.rob.buffer.Dequeue();
                    foreach (ArfEntry arfEntry in CPU.arf.regFile)
                    {
                        if (arfEntry.tag == robEntry.renameReg)
                        {
                            arfEntry.busy = false;
                        }
                        CPU.rrf.rrfTable[robEntry.renameReg].busy = false;
                    }
                }
            }
        }

    }
}
