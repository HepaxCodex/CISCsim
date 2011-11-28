using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    class IssueStage
    {
        public ReservationStation integerStation;
        public ReservationStation fpStation;
        public ReservationStation memStation;
        public ReservationStation multDivStation;
        public ReservationStation branchStation;


        /// <summary>
        /// Default Constructor
        /// </summary>
        public IssueStage()
        {
            this.integerStation = new ReservationStation();
            this.fpStation = new ReservationStation();
            this.memStation = new ReservationStation(); // Per the Specification
            this.multDivStation = new ReservationStation();
            //I think the number of entries is the same for all the RSs.
            //The thing that changes with branch is the # of functional units. -btf
            this.branchStation = new ReservationStation();
        }

    }
}
