using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    class DecodeStage
    {
        /// <summary>
        /// 
        /// </summary>
        private Queue<Instruction> decodeBuffer;

        /// <summary>
        /// 
        /// </summary>
        public DecodeStage()
        {
            decodeBuffer = new Queue<Instruction>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void runCycle()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int getEmptySlots()
        {
            if (Config.superScalerFactor - this.decodeBuffer.Count < 0)
            {
                System.Console.WriteLine("ERROR: Decide Buffer has too many elements!!");
            }
            return Config.superScalerFactor - this.decodeBuffer.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instr"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        public void testRemoveInstruction()
        {
            this.decodeBuffer.Dequeue();
        }


    }
}
