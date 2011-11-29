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

        /// <summary>
        /// Runs an IssueStage cycle
        /// </summary>
        public void runCycle()
        {
            // Attempt issue of an instruction in each of the reservation stations
            AttemptIntegerIssue();
            AttemptFloatingIssue();
            AttemptMemoryIssue();
            AttemptMultDivIssue();
            AttemptBranchIssue();
        }

        // TODO: ugh, these can all be a single AttemptIssue function that's passed a station...
        // I'm tired though, so we can do that later if we want

        /// <summary>
        /// Attempts to issue integer reservation station entry to execution unit
        /// <returns>true if it could execute, false if no execution unit was available</returns>
        /// </summary>
        private void AttemptIntegerIssue()
        {
            if (this.integerStation.buffer.Count > 0)
            {
                ReservationStationEntry entry = this.integerStation.buffer.Peek();
                if (CPU.executeStage.canIssue(entry))
                {
                    // It could execute, remove it from the reservation station
                    entry = this.integerStation.buffer.Dequeue();
                }
                else
                {
                    Statistics.integerExecutionUnitsFull++;
                }
            }
            else
            {
                Statistics.integerExecutionUnitsEmpty++;
            }
        }

        /// <summary>
        /// Attempts to issue floating point reservation station entry to execution unit
        /// <returns>true if it could execute, false if no execution unit was available</returns>
        /// </summary>
        private void AttemptFloatingIssue()
        {
            if (this.fpStation.buffer.Count > 0)
            {
                ReservationStationEntry entry = this.fpStation.buffer.Peek();
                if (CPU.executeStage.canIssue(entry))
                {
                    // It could execute, remove it from the reservation station
                    entry = this.fpStation.buffer.Dequeue();
                }
                else
                {
                    Statistics.floatingExecutionUnitsFull++;
                }
            }
            else
            {
                Statistics.floatingExecutionUnitsEmpty++;
            }
        }

        /// <summary>
        /// Attempts to issue memory reservation station entry to execution unit
        /// <returns>true if it could execute, false if no execution unit was available</returns>
        /// </summary>
        private void AttemptMemoryIssue()
        {
            if (this.memStation.buffer.Count > 0)
            {
                ReservationStationEntry entry = this.memStation.buffer.Peek();
                if (CPU.executeStage.canIssue(entry))
                {
                    // It could execute, remove it from the reservation station
                    entry = this.memStation.buffer.Dequeue();
                }
                else
                {
                    Statistics.memoryExecutionUnitsFull++;
                }
            }
            else
            {
                Statistics.memoryExecutionUnitsEmpty++;
            }
        }

        /// <summary>
        /// Attempts to issue mult/div reservation station entry to execution unit
        /// <returns>true if it could execute, false if no execution unit was available</returns>
        /// </summary>
        private void AttemptMultDivIssue()
        {
            if (this.multDivStation.buffer.Count > 0)
            {
                ReservationStationEntry entry = this.multDivStation.buffer.Peek();
                if (CPU.executeStage.canIssue(entry))
                {
                    // It could execute, remove it from the reservation station
                    entry = this.multDivStation.buffer.Dequeue();
                }
                else
                {
                    Statistics.multDivExecutionUnitsFull++;
                }
            }
            else
            {
                Statistics.multDivExecutionUnitsEmpty++;
            }
        }

        /// <summary>
        /// Attempts to issue branch reservation station entry to execution unit
        /// <returns>true if it could execute, false if no execution unit was available</returns>
        /// </summary>
        private void AttemptBranchIssue()
        {
            if (this.branchStation.buffer.Count > 0)
            {
                ReservationStationEntry entry = this.branchStation.buffer.Peek();
                if (CPU.executeStage.canIssue(entry))
                {
                    // It could execute, remove it from the reservation station
                    entry = this.branchStation.buffer.Dequeue();
                }
                else
                {
                    Statistics.branchExecutionUnitsFull++;
                }
            }
            else
            {
                Statistics.branchExecutionUnitsEmpty++;
            }
        }

    }
}
