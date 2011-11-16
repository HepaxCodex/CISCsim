using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    class BranchPredictionSM
    {
        /// <summary>
        /// Current State of the Branch Predictor
        /// </summary>
        private struct branchState
        {
            /// <summary>
            /// This is the LAST result a brach 
            /// </summary>
            public bool mostRecentResult;
            /// <summary>
            /// This is the second to last result of a branch
            /// </summary>
            public bool secondMostRecentResult;
        }

        /// <summary>
        /// Current State of the State Machine
        /// GET - Current State value
        /// </summary>
        private branchState currentState;


        /// <summary>
        /// Generic Constructor for the Branch predictor
        ///  - currentState = {false;false}
        ///  - isBranchPredicted = false;
        /// </summary>
        public BranchPredictionSM() {}


        /// <summary>
        /// Checks the current state an returns a branch prediction
        /// </summary>
        /// <returns> True if a branch is predicted, False otherwise</returns>
        public bool GetPredction()
        {
            if (     this.currentState.mostRecentResult == true  && this.currentState.secondMostRecentResult == true) // TT
                return true;
            else if (this.currentState.mostRecentResult == false && this.currentState.secondMostRecentResult == true) // TN
                return true; 
            else if (this.currentState.mostRecentResult == true  && this.currentState.secondMostRecentResult == false) // NT
                return true;
            else if (this.currentState.mostRecentResult == false && this.currentState.secondMostRecentResult == false) // NN
                return false;
            else
                Console.WriteLine("GetPrediction:: current Branch state is invalid");
            return false;
        }


        /// <summary>
        /// Updates the state machine with the
        /// result of a branch.
        /// </summary>
        /// <param name="_branchWasTaken">True if a branch was taken at the current location</param>
        public void Update(bool _branchWasTaken)
        {
            // Shift the old result
            this.currentState.secondMostRecentResult = this.currentState.mostRecentResult;
            // Set the most reset result
            this.currentState.mostRecentResult = _branchWasTaken;
     
        }


    }
}
