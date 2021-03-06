﻿using System;
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

            this.branchUnits = new ExecutionUnit[1];
            this.branchUnits[0] = new ExecutionUnit();

            this.memUnits = new ExecutionUnit[Config.numFunctionalUnits];
            this.intUnits = new ExecutionUnit[Config.numFunctionalUnits];
            this.fpUnits = new ExecutionUnit[Config.numFunctionalUnits];
            this.multDivUnits = new ExecutionUnit[Config.numFunctionalUnits];

            for (int i = 0; i < Config.numFunctionalUnits; i++)
            {
                this.memUnits[i] = new ExecutionUnit();
                this.intUnits[i] = new ExecutionUnit();
                this.fpUnits[i] = new ExecutionUnit();
                this.multDivUnits[i] = new ExecutionUnit();
            }
        }

        // For every busy Unit do a RunCycle.  If it returns true the instruciton is finished
        public void runCycle()
        {
            foreach (ExecutionUnit unit in this.memUnits)     { handleUnitCycle(unit); }
            foreach (ExecutionUnit unit in this.intUnits)     { handleUnitCycle(unit); }
            foreach (ExecutionUnit unit in this.fpUnits)      { handleUnitCycle(unit); }
            foreach (ExecutionUnit unit in this.multDivUnits) { handleUnitCycle(unit); }
            foreach (ExecutionUnit unit in this.branchUnits)
            {
                if(handleUnitCycle(unit))
                    CPU.branchMispredictionStall = false;
            }

        }

        /// <summary>
        /// Checks the Execution unit to see if it is finished, if so moves the instruction 
        /// and clears the unit.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>True if the unit is finished, false otherwise</returns>
        private bool handleUnitCycle(ExecutionUnit unit)
        {
            // First make sure that there is an instruction in there
            if (unit.busy)
            {
                // Then run the cycle and see if it finishes
                if (unit.runCycle())
                {
                    // TODO: Move the Cycle / Update the RRF
                    // The the reservation station entry should hold the RRF entry index so taht we know which one to update
                    int rrfEntryIndex = CPU.rob.executionFinished(unit.entry);
                    CPU.rrf.executionFinished(rrfEntryIndex);

                    // Update the Reservation Stations
                    updateReservationStation(rrfEntryIndex, CPU.issueStage.branchStation);
                    updateReservationStation(rrfEntryIndex, CPU.issueStage.fpStation);
                    updateReservationStation(rrfEntryIndex, CPU.issueStage.integerStation);
                    updateReservationStation(rrfEntryIndex, CPU.issueStage.memStation);
                    updateReservationStation(rrfEntryIndex, CPU.issueStage.multDivStation);

                    // Tell Everyone that the reservation station entry is now available
                    unit.entry.busy = false;
                    return true;
                }
            }
            return false;
        }

        private void updateReservationStation(int rrfEntryIndex, ReservationStation resStation)
        {
            foreach (ReservationStationEntry rsEntry in resStation.buffer)
            {
                if (rsEntry.valid1 == false && rsEntry.op1 == rrfEntryIndex)
                {
                    rsEntry.op1 = rsEntry.instr.source1;
                    rsEntry.valid1 = true;
                }
                if (rsEntry.valid2 == false && rsEntry.op2 == rrfEntryIndex)
                {
                    rsEntry.op2 = rsEntry.instr.source2;
                    rsEntry.valid2 = true;
                }
                if (rsEntry.valid1 == true && rsEntry.valid2 == true)
                {
                    rsEntry.ready = true;
                }
            }


        }

        /// <summary>
        /// Takes a reservation station entry and checks to see if it can be issued
        /// 
        /// This is competed by checking the appropriate list of execution units to
        /// see if there is one available
        /// <returns>true if the instruction can be issued and false if not</returns>
        /// </summary>
        public bool canIssue(ReservationStationEntry resStationEntry)
        {

            ExecutionUnit[] targetUnitArray = this.getTargetUnitArray(resStationEntry);
            for (int i = 0; i < targetUnitArray.Length; i++)
            {
                // if there is room return true
                if (targetUnitArray[i].busy == false)
                    return true;
            }

            // If we check all of the exeuction units, and they are all busy == true, then there is no room
            markReservationStationFull(resStationEntry);
            return false;
        }

        public void acceptIssue(ReservationStationEntry resStationEntry)
        {
            ExecutionUnit[] targetUnitArray = this.getTargetUnitArray(resStationEntry);
            for (int i = 0; i < targetUnitArray.Length; i++)
            {
                if (targetUnitArray[i].busy == false)
                {
                    targetUnitArray[i].LoadInstruction(resStationEntry);
                    break;
                }
            }

        }

        private ExecutionUnit[] getTargetUnitArray(ReservationStationEntry resStationEntry)
        {
            ExecutionUnit[] targetUnitArray;

            switch (resStationEntry.instr.executionType)
            {
                case Instruction.ExecutionType.Branch:
                    targetUnitArray = this.branchUnits;
                    break;
                case Instruction.ExecutionType.FloatingPoint:
                    targetUnitArray = this.fpUnits;
                    break;
                case Instruction.ExecutionType.Logical:
                case Instruction.ExecutionType.Integer:
                    targetUnitArray = this.intUnits;
                    break;
                case Instruction.ExecutionType.Mem:
                    targetUnitArray = this.memUnits;
                    break;
                case Instruction.ExecutionType.MultDiv:
                    targetUnitArray = this.multDivUnits;
                    break;
                default:
                    System.Console.WriteLine("ERROR:ExecutionStage.canIssue, unknown Execution Type");
                    targetUnitArray = null;
                    break;
            }
            return targetUnitArray;
        }

        private void markReservationStationFull(ReservationStationEntry resStationEntry)
        {
            switch (resStationEntry.instr.executionType)
            {
                case Instruction.ExecutionType.Branch:
                    Statistics.branchExecutionUnitsFull++;
                    break;
                case Instruction.ExecutionType.FloatingPoint:
                    Statistics.floatingExecutionUnitsFull++;
                    break;
                case Instruction.ExecutionType.Logical:
                case Instruction.ExecutionType.Integer:
                    Statistics.integerExecutionUnitsFull++;
                    break;
                case Instruction.ExecutionType.Mem:
                    Statistics.memoryExecutionUnitsFull++;
                    break;
                case Instruction.ExecutionType.MultDiv:
                    Statistics.multDivExecutionUnitsFull++;
                    break;
                default:
                    System.Console.WriteLine("ERROR:ExecutionStage.canIssue, unknown Execution Type");
                    break;
            }
        }

    }
}
