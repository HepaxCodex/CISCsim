﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    static class AndrewTest
    {
        public static void RunTest()
        {
            // First Cycle
            CPU.issueStage.runCycle();
            CPU.dispatchStage.runCycle();
            CPU.decodeStage.runCycle();
            CPU.fetchStage.Fetch();


            // Second Cycle
            CPU.issueStage.runCycle();
            CPU.dispatchStage.runCycle();
            CPU.decodeStage.runCycle();
            CPU.fetchStage.Fetch();

            // Third Cycle
            CPU.issueStage.runCycle();
            CPU.dispatchStage.runCycle();
            CPU.decodeStage.runCycle();
            CPU.fetchStage.Fetch();

            // Fourth Cycle
            CPU.issueStage.runCycle();
            CPU.dispatchStage.runCycle();
            CPU.decodeStage.runCycle();
            CPU.fetchStage.Fetch();


            /*
            testDecode.testRemoveInstruction(); //
            testDecode.testRemoveInstruction(); // Represents Decode.RunTest
            testDecode.testRemoveInstruction(); //

            
            fetchStage.Fetch();
            //CycleCleanup.fetch2Decode(testFetch, testDecode);

            testDecode.testRemoveInstruction(); // Represents Decode.RunTest
            testDecode.testRemoveInstruction(); //

            
            fetchStage.Fetch();
            //CycleCleanup.fetch2Decode(testFetch, testDecode);

            testDecode.testRemoveInstruction(); //

            
            fetchStage.Fetch();
            //CycleCleanup.fetch2Decode(testFetch, testDecode);

            testDecode.testRemoveInstruction(); //
            testDecode.testRemoveInstruction(); // Represents Decode.RunTest
            testDecode.testRemoveInstruction(); //

            
            fetchStage.Fetch();
            //CycleCleanup.fetch2Decode(testFetch, testDecode);
             * */
        }
    }
}
