using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Defines a Single Instruction
    /// </summary>
    class Instruction
    {
        /// <summary>
        /// All possible types of Execution
        /// </summary>
        public enum ExecutionType {Integer, FloatingPoint, Logical, Mem, MultDiv, Branch, Nop}; // TODO: BeSure to USE ALL ExecutionTypes!!

        /// <summary>
        /// Address of the Instruction
        /// </summary>
        public int address;
        
        /// <summary>
        /// Instrauction name i.e. add, mov, j
        /// </summary>
        public string instruction;

        public string source1;
        public string source2;

        public string dest;

        /// <summary>
        /// The Type of the Instruction (used for Exeuction Stage)
        /// </summary>
        public ExecutionType executionType;

        public Instruction()
        {
        }

        public Instruction(string traceLine)
        {
            string[] tokens = traceLine.Split(' ');
            
            this.address = int.Parse(tokens[0]);
            this.instruction = tokens[1];

            this.executionType = getExecutionType(tokens[1]);
            this.setArguments(tokens[2]);


        }


        /// <summary>
        /// Figures out what type of Exeuction Unit is appliciable for the instruction
        /// </summary>
        /// <param name="instructionName">The instruction name i.e. addi, mov, j</param>
        /// <returns>The Execution Type of the INstruciotn</returns>
        private ExecutionType getExecutionType(string instructionName){
            ExecutionType result;

            switch (instructionName)
            {
                case "j":
                case "jal":
                case "jr":
                case "jalr":
                case "beq":
                case "bne":
                case "blez":
                case "bgtz":
                case "bltz":
                case "bgez":
                case "bc1f":
                case "bc1t":
                    result = ExecutionType.Branch;
                    break;
                case "lb":
                case "lbu":
                case "lh":
                case "lhu":
                case "lw":
                case "l.s":
                case "l.d":
                case "sb":
                case "sh":
                case "sw":
                case "s.s":
                case "s.d":
                    result = ExecutionType.Mem;
                    break;
                case "add":
                case "addi":
                case "addu":
                case "addiu":
                case "sub":
                case "subu":
                case "mult":
                case "div":
                case "divu":
                case "mfhi":
                case "mflo":
                case "lui":
                case "mfc1":
                case "dmfc1":
                case "mtc1":
                case "dmtc1":
                    result =  ExecutionType.Integer;
                    break;
                case "and":
                case "andi":
                case "or":
                case "ori":
                case "xor":
                case "xori":
                case "nor":
                case "sll":
                case "sllv":
                case "srl":
                case "srlv":
                case "sra":
                case "srav":
                case "slt":
                case "slti":
                case "sltu":
                case "sltiu":
                    result = ExecutionType.Logical;
                    break;
                case "add.s":
                case "add.d":
                case "sub.s":
                case "sub.d":
                case "mul.s":
                case "mul.d":
                case "div.d":
                case "mov.d":
                case "neg.d":
                case "cvt.s.d":
                case "cvt.s.w":
                case "cvt.d.s":
                case "cvt.d.w":
                case "cvt.w.d":
                case "c.eq.d":
                case "c.lt.d":
                case "c.le.d":
                case "sqrt.d":
                    result =  ExecutionType.FloatingPoint;
                    break;
                case "syscall":
                case "nop":
                    result = ExecutionType.Nop;
                    break;
                default :
                    System.Console.WriteLine("ERROR: Unknown Instruction Type \"{0}\"", instructionName);
                    System.Console.WriteLine("       Using Execution Type of \"Nop\"");
                    System.Console.WriteLine("       ... press any key to continue ... ");
                    System.Console.Read();
                    result = ExecutionType.Nop;
                    break;
            }

            return result;

        }


        private void setArguments(string args)
        {

        }



        
    }
}
