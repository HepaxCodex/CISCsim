using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace CISCsim
{
    class DecodeStage
    {

        private Queue<Instruction> decodeBuffer;

        public DecodeStage()
        {
            decodeBuffer = new Queue<Instruction>();
        }

        public int getEmptySlots()
        {
            if (Config.superScalerFactor - this.decodeBuffer.Count < 0)
            {
                System.Console.WriteLine("ERROR: Decide Buffer has too many elements!!");
            }
            return Config.superScalerFactor - this.decodeBuffer.Count;
        }

        public bool addInstructionToBuffer(Instruction instr)
        {
            if (Config.superScalerFactor - this.decodeBuffer.Count > 0)
            {
                this.decodeBuffer.Enqueue(instr);
                return true;
            }
            else
            {
                System.Console.WriteLine("ERROR Tried to add instruction to decode buffer when it was full!");
                return false;
            }
        }

        public void testRemoveInstruction()
        {
            this.decodeBuffer.Dequeue();
        }


    }
}
