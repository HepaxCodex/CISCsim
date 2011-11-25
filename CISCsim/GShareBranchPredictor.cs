using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    // TODO: What happens when the BTB runs out of room?
    // TODO: For now the BTB is left as variable size (grows/shrinks automatically)
    // TODO: Decide if the BTB tabe is necessary (it was required by the spec, but not required by G-Share Branch Predction
    // TODO: Add the UPDATE code to GShareBranchPredictor

    /// <summary>
    /// A Global Share Branch Predictor for Super Scaler CPUs
    /// 
    /// The BTB table is uncorrelated from the Branch State Machine Array
    /// </summary>
    class GShareBranchPredictor
    {
        /////////////////
        // Public Data //
        /////////////////

        /// <summary>
        /// Number of Predictor State machines
        /// Defined by the Homework Specification
        /// </summary>
        private int numPredictorStateMachines = 512;

        //////////////////
        // Private Data //
        //////////////////

        //TODO: What type should this be?  
        /// <summary>
        /// Represents the Path History to the current branch
        /// </summary>
        private byte branchHistoryShiftReg;

        /// <summary>
        /// Contains the Branch Target relationships
        /// The calling PC is the Key and the branch Target is the Value
        /// </summary>
        private Dictionary<int, int> branchTargetBuffer;


        private BranchPredictionSM[] branchPredictorTable = new BranchPredictionSM[512];


        //////////////////////
        // Public Functions //
        //////////////////////

        /// <summary>
        /// Default Constrctor (Should not be used)
        /// Sets NumBtbEntries = 64
        /// Initializes branchTargetBuffer Dictionary
        /// </summary>
        public GShareBranchPredictor()
        {
            // numBtbEntries = 64;  // Only needed if we want a fixed size BTB
            branchTargetBuffer = new Dictionary<int, int>();

            // Initialize the branch predictor table
            branchPredictorTable.Initialize();
        }



        /// <summary>
        /// Prefered Constructor
        /// Initializes the branch Target Buffer to _numBrbEntries 
        /// (This is Configurable in the specification)
        /// </summary>
        /// <param name="_numBtbEntries">Number of desired entries in the Branch Target Buffer (config by spec) </param>
        public GShareBranchPredictor(int _numBtbEntries)
        {
            // Create the branch Target Buffer Dictionary
            branchTargetBuffer = new Dictionary<int, int>();

            // Initialize the Branch Prediction table
            branchPredictorTable.Initialize();
        }


        // NOTES from specification:
        // if branch taken ... Get address from BTB, if not available predict as not taken
        // on a mispredict wait for branc to finish execution before fetching

        /// <summary>
        /// Determines if the branch found at the current PC should be predicted as taken or not
        /// </summary>
        /// <param name="_currentPC">current PC address (where the branch is found)</param>
        /// <param name="branchTarget">The Target address of the branch</param>
        /// <returns>True if the branch is predicted as taken, false otherwise</returns>
        public bool predictBranch(int _currentPC, int _branchTarget)
        {
            // Check to see if the currentPC is in the Branch Target Buffer or not
            if (branchTargetBuffer.ContainsKey(_currentPC))
            {
                bool prediction; // used to hold the resulting prediciton

                // Hash the BHSW with the Fetch Addr
                byte targetLSB = (BitConverter.GetBytes(_branchTarget))[0]; // Get the First Byte (LSB)
                byte hash = BitConverter.GetBytes(this.branchHistoryShiftReg ^ targetLSB)[0];
               
                // Get the Ask the SM at the hash what the prediction is.
                prediction = this.branchPredictorTable[hash].GetPredction();

                return prediction;
            }
            else // If the BTB does not contain the branch at the Current PC
            {
                //Add the current PC/Target to the table
                branchTargetBuffer.Add(_currentPC, _branchTarget);

                // Return as not predicted
                return false; 
            }
        }

        // TODO: Add update branches (this should also update BHSR)


        ///////////////////////
        // Private Functions //
        ///////////////////////





    }
}
