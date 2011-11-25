using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Defines the fetch stage.
    /// Has an array of instructions called fetchBuffer.
    /// </summary>
    class FetchStage
    {
        public Instruction[] fetchBuffer;

        public FetchStage()
        {
        }

        public FetchStage(int superScalarWidth)
        {
            this.fetchBuffer = new Instruction[superScalarWidth];
        }
    }
}
