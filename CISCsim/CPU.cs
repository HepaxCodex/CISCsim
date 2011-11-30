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

        /// <summary>
        /// The CPU's reorder buffer
        /// </summary>
        public static ReorderBuffer rob = new ReorderBuffer();

        /// <summary>
        /// The CPU's architecture register file
        /// </summary>
        public static ArchRegFile arf = new ArchRegFile();

        /// <summary>
        /// The CPU's Global Share 2-bit Branch Predictor
        /// </summary>
        public static GShareBranchPredictor branchPredictor = new GShareBranchPredictor();

        // TODO: figure out if we want all the stages in here? Seems like it would
        // make sense to have this static CPU that you call CPU.fetchStage.runCycle(), etc.

        /// <summary>
        /// The CPU's pipeline stages
        /// </summary>
        public static FetchStage fetchStage = new FetchStage();
        public static DecodeStage decodeStage = new DecodeStage();
        public static DispatchStage dispatchStage = new DispatchStage();
        public static IssueStage issueStage = new IssueStage();
        public static ExecutionStage executeStage = new ExecutionStage();
        public static CompleteStage completeStage = new CompleteStage();


        public static bool branchMispredictionStall = false;

        /// <summary>
        /// Tracks whether the last instruction in a trace file has been fetched
        /// </summary>
        public static bool lastInstructionFetched = false;

        /// <summary>
        /// Tracks whether the last instruction in a trace file has made it all the way
        /// through the CPU
        /// </summary>
        public static bool lastInstructionCompleted = false;

    }
}
