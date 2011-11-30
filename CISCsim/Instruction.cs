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
        public enum ExecutionType {Integer, FloatingPoint, Logical, Mem, MultDiv, Branch, Nop}; // NOTE : 

        /// <summary>
        /// Address of the Instruction
        /// </summary>
        public int address;
        
        /// <summary>
        /// Instruction name i.e. add, mov, j
        /// </summary>
        public string instruction;

        /// <summary>
        /// These bools tell whether source1 and source2 are registers or immediates
        /// </summary>
        public bool source1Imm;
        public bool source2Imm;
        public bool source3Imm;

        public int source1;
        public string source1String;
        public int source2;
        public string source2String;
        //store instructions have 3 sources
        public int source3;
        public string source3String;

        public int dest;
        public string destString;

        /// <summary>
        /// The Type of the Instruction (used for Execution Stage)
        /// </summary>
        public ExecutionType executionType;

        /// <summary>
        /// DO NOT USE! Default Constructor (made private so it's not used)
        /// </summary>
        private Instruction()
        {
        }


        /// <summary>
        /// Prefered Constructor
        /// </summary>
        /// <param name="traceLine">The Line in the trace file containing the instruction</param>
        public Instruction(string traceLine)
        {
            string[] tokens = traceLine.Split(' ');
            
            this.address = int.Parse(tokens[0]);
            this.instruction = tokens[1];

            this.executionType = getExecutionType(tokens[1]);
            
            // Nop instructions have no arguments
            if (this.executionType != ExecutionType.Nop)
                this.setArguments(tokens[2]);


        }

        /// <summary>
        /// Returns true if the instruction is an actual branch (not a jump)
        /// </summary>
        public bool isABranch()
        {
            if (this.executionType != ExecutionType.Branch)
            {
                return false;
            }

            return !(this.instruction == "j" || this.instruction == "jal" ||
                    this.instruction == "jr" || this.instruction == "jalr");
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
                case "mult":
                case "div":
                    result = ExecutionType.MultDiv;
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

        /// <summary>
        /// This takes arguments like "r1,r2,r3" or "1234" or "r1,133" or "r1,r2(100)" and parses them
        /// </summary>
        /// <param name="args">an instruction argument string like "r1,r2,r3" or "1234" or "r1,133" or "r1,r2(100)"</param>
        private void setArguments(string args)
        {
            string[] tokens = args.Split(',');
            switch (tokens.Length)
            {
                case 1:
                    #region One-argument instructions
                    switch(this.instruction)
                    {
                        case "bc1f":
                        case "bc1t":
                            //source1 is the immediate provided
                            this.source1 = getIntFromRegisterString(tokens[0]);
                            this.source1String = tokens[0];
                            this.source1Imm = true;
                            //source2 is FCC
                            this.source2 = 64;
                            this.source2String = "fcc";
                            this.source2Imm = false;
                            //br instrs have no dest
                            this.dest = -1;
                            break;
                        case "mfhi":
                        case "mflo":
                            //source1 is hi_lo
                            this.source1 = 63;
                            this.source1String = "hi_lo";
                            this.source1Imm = false;
                            //dest is the given reg
                            this.dest = getIntFromRegisterString(tokens[0]);
                            this.destString = tokens[0];
                            break;
                        default:
                            //all the other 1-arg instrs use a reg or immediate as source w/out dests
                            this.source1 = getIntFromRegisterString(tokens[0]);
                            this.source1String = tokens[0];
                            this.source1Imm = !(this.source1String.StartsWith("r") || this.source1String.StartsWith("f"));
                            this.dest = -1;
                            break;
                    }
                    break;
                    #endregion
                case 2:
                    if (tokens[1].Contains('(')) // Actually has 3 arguments but in r1,10(r3) form
                    {
                        #region Three-argument instructions that are in the form X,Y(Z)
                        string[] lastargs = tokens[1].Split(new char[] { '(', ')' });

                        switch (this.instruction)
                        {
                            case "sb":
                            case "sh":
                            case "sw":
                            case "s.s":
                            case "s.d":
                                //store words have 3 sources
                                this.source1 = getIntFromRegisterString(tokens[0]);
                                this.source1String = tokens[0];
                                this.source1Imm = false;
                                this.source2 = getIntFromRegisterString(lastargs[0]);
                                this.source2String = lastargs[0];
                                this.source2Imm = true;
                                this.source3 = getIntFromRegisterString(lastargs[1]);
                                this.source3String = lastargs[1];
                                this.source3Imm = false;
                                //store words have no register destination
                                this.dest = -1;
                                break;
                            default:
                                //the default is normal: dest,src1(src2)
                                this.dest = getIntFromRegisterString(tokens[0]);
                                this.destString = tokens[0];
                                this.source1 = getIntFromRegisterString(lastargs[0]);
                                this.source1String = lastargs[0];
                                this.source1Imm = !(this.source1String.StartsWith("r") || this.source1String.StartsWith("f"));
                                this.source2 = getIntFromRegisterString(lastargs[1]);
                                this.source2String = lastargs[1];
                                this.source2Imm = !(this.source2String.StartsWith("r") || this.source2String.StartsWith("f"));
                                break;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Two-argument instructions
                        switch (this.instruction)
                        {
                            case "blez":
                            case "bgtz":
                            case "bltz":
                            case "bgez":
                                //source1 is the register, source2 is the immediate
                                this.source1 = getIntFromRegisterString(tokens[0]);
                                this.source1String = tokens[0];
                                this.source1Imm = false;
                                this.source2 = getIntFromRegisterString(tokens[1]);
                                this.source2String = tokens[1];
                                this.source2Imm = true;
                                //branch instrs don't have a dest
                                this.dest = -1;
                                break;
                            case "mult":
                            case "div":
                            case "divu":
                                //dest is hi_log, source1 is first reg, source2 is second reg
                                this.dest = 63;
                                this.destString = "hi_lo";
                                this.source1 = getIntFromRegisterString(tokens[0]);
                                this.source1String = tokens[0];
                                this.source1Imm = false;
                                this.source2 = getIntFromRegisterString(tokens[1]);
                                this.source2String = tokens[1];
                                this.source2Imm = false;
                                break;
                            case "mtc1":
                            case "dmtc1":
                                //dest is floating point reg provide in tokens[1], source1 is the reg provided
                                this.dest = getIntFromRegisterString(tokens[1]);
                                this.destString = tokens[1];
                                this.source1 = getIntFromRegisterString(tokens[0]);
                                this.source1String = tokens[0];
                                this.source1Imm = false;
                                break;
                            case "c.eq.d":
                            case "c.lt.d":
                            case "c.le.d":
                                //dest is fcc, source1 is f reg1, source2 is f reg2
                                this.dest = 64;
                                this.destString = "fcc";
                                this.source1 = getIntFromRegisterString(tokens[0]);
                                this.source1String = tokens[0];
                                this.source1Imm = false;
                                this.source2 = getIntFromRegisterString(tokens[1]);
                                this.source2String = tokens[1];
                                this.source2Imm = false;
                                break;
                            default:
                                //default case is the normal kind: instr dst,src
                                this.dest = getIntFromRegisterString(tokens[0]);
                                this.destString = tokens[0];
                                this.source1 = getIntFromRegisterString(tokens[1]);
                                this.source1String = tokens[1];
                                this.source1Imm = !(this.source1String.StartsWith("r") || this.source1String.StartsWith("f"));
                                break;
                        }
                        #endregion
                    }
                    break;
                case 3:
                    #region Three-argument instructions in the form X,Y,Z
                    switch (this.instruction)
                    {
                        case "beq":
                        case "bne":
                            //these branches have 3 sources
                            this.source1 = getIntFromRegisterString(tokens[0]);
                            this.source1String = tokens[0];
                            this.source1Imm = false;
                            this.source2 = getIntFromRegisterString(tokens[1]);
                            this.source2String = tokens[1];
                            this.source2Imm = false;
                            this.source3 = getIntFromRegisterString(tokens[2]);
                            this.source3String = tokens[2];
                            this.source3Imm = true;
                            //branch instrs don't have a dest
                            this.dest = -1;
                            break;
                        default:
                            //the default is the norm: dst,src1,src2
                            this.dest = getIntFromRegisterString(tokens[0]);
                            this.destString = tokens[0];
                            this.source1 = getIntFromRegisterString(tokens[1]);
                            this.source1String = tokens[1];
                            this.source1Imm = !(this.source1String.StartsWith("r") || this.source1String.StartsWith("f"));
                            this.source2 = getIntFromRegisterString(tokens[2]);
                            this.source2String = tokens[2];
                            this.source2Imm = !(this.source2String.StartsWith("r") || this.source2String.StartsWith("f"));
                            break;
                    }
                    break;
                    #endregion
                default:
                    System.Console.WriteLine("ERROR: Unknown instruction argument format found: \"{0}\"", args);
                    System.Console.WriteLine("       leaving everything unitialized");
                    System.Console.WriteLine("       ... press any key to continue ... ");
                    break;
            }
        }

        /// <summary>
        /// This takes arguments like "rX" or "fX" or "X" and retrieves the X as an int
        /// </summary>
        private int getIntFromRegisterString(string regString)
        {
            int regValue;
            if (regString.StartsWith("r"))
            {
                regValue = Convert.ToInt32(regString.Substring(1));
            }
            else if (regString.StartsWith("f"))
            {
                regValue = Convert.ToInt32(regString.Substring(1)) + 32;
            }
            else
            {
                regValue = Convert.ToInt32(regString);
            }
            return regValue;
        }
    }
}
