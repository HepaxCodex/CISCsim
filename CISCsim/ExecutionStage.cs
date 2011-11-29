using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    class ExecutionStage
    {

        public ExecutionUnit[] memUnits;
        public ExecutionUnit[] branchUnits;
        public ExecutionUnit[] intUnits;
        public ExecutionUnit[] fpUnits;
        public ExecutionUnit[] multDivUnits;

        public ExecutionStage()
        {
            this.memUnits = new ExecutionUnit[Config.numFunctionalUnits];
            this.memUnits.Initialize();

            this.branchUnits = new ExecutionUnit[1];
            this.branchUnits.Initialize();

            this.intUnits = new ExecutionUnit[Config.numFunctionalUnits];
            this.intUnits.Initialize();

            this.fpUnits = new ExecutionUnit[Config.numFunctionalUnits];
            this.fpUnits.Initialize();

            this.multDivUnits = new ExecutionUnit[Config.numFunctionalUnits];
            this.fpUnits.Initialize();
        }


        public void runCycle()
        {


        }
    }
}
