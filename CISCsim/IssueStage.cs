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
                // TODO: This can return null - what can? the peek? I check before to make sure .Count > 0
                // So I changed this from a .Peek() to a FirstOrDefault because as long as instructions are ready
                // in the reservation station, there's no reason to make them wait behind other instructions
                ReservationStationEntry entry;
                entry = this.integerStation.buffer.FirstOrDefault(_entry => _entry.ready == true);

                if (entry != null)
                {
                    if (CPU.executeStage.canIssue(entry))
                    {
                        // It could execute, remove it from the reservation station
                        entry = this.integerStation.buffer.Dequeue();
                        CPU.executeStage.acceptIssue(entry); // now move the actual item
                    }
                    else
                    {
                        Statistics.integerExecutionUnitsFull++;
                    }
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
                ReservationStationEntry entry;
                entry = this.integerStation.buffer.FirstOrDefault(_entry => _entry.ready == true);

                if (entry != null)
                {
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
                ReservationStationEntry entry;
                entry = this.integerStation.buffer.FirstOrDefault(_entry => _entry.ready == true);

                if (entry != null)
                {
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
                ReservationStationEntry entry;
                entry = this.integerStation.buffer.FirstOrDefault(_entry => _entry.ready == true);

                if (entry != null)
                {
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
                ReservationStationEntry entry;
                entry = this.integerStation.buffer.FirstOrDefault(_entry => _entry.ready == true);

                if (entry != null)
                {
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
            }
            else
            {
                Statistics.branchExecutionUnitsEmpty++;
            }
        }

    }
}
