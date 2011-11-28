using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Represents our CPU.
    /// TODO: May have to rethink how this is structured.
    /// If it has the stages and, say, an RRF, and one of the stages uses the RRF,
    /// how does that work? Maybe it shouldn't be static?
    /// </summary>
    static class CPU
    {
        /// <summary>
        /// The program counter is updated by fetch when we
        /// look at the address of the instruction
        /// </summary>
        public static int pc;

        /// <summary>
        /// The CPU's rename register file
        /// </summary>
        public static RenameRegisterFile rrf = new RenameRegisterFile();

        // TODO: figure out if we want all the stages in here? Seems like it would
        // make sense to have this static CPU that you call CPU.fetchStage.runCycle(), etc.

        /// <summary>
        /// The CPU's pipeline stages
        /// </summary>
        public static FetchStage fetchStage = new FetchStage();
        public static DecodeStage decodeStage = new DecodeStage();
        public static IssueStage issueStage = new IssueStage();
    }
}
