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
                while (CPU.rob.buffer.Count > 0 && CPU.rob.buffer.Peek().finished == true )
                {
                    RobEntry robEntry = CPU.rob.buffer.Dequeue();
                    foreach (ArfEntry arfEntry in CPU.arf.regFile)
                    {
                        if (arfEntry.tag == robEntry.renameReg)
                        {
                            arfEntry.busy = false;
                            
                        }
                    }
                    //TODO: Make sure that the reservation stations are updated
                    CPU.rrf.rrfTable[robEntry.renameReg].busy = false;
                    Statistics.instructionsCompleted++;
                    if (robEntry.instruction.isLastInstruction)
                        CPU.lastInstructionCompleted = true;
                }
            }
        }

    }
}
