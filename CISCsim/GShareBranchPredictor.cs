using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    class GShareBranchPredictor
    {
        /// <summary>
        /// Number of Predictor State machines
        /// Defined by the Homework Specification
        /// </summary>
        private int numPredictorStateMachines = 512;

        /// <summary>
        /// Variable - Number of entries in the Branch Target Buffer
        /// </summary>
        public int NumBtbEntries;
    }
}
