using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    static class AndrewTest
    {
        public static void RunTest()
        {
            FetchStage fetchStage = new FetchStage();
            DecodeStage decodeStage = new DecodeStage();
            DispatchStage dispatchStage = new DispatchStage();
            IssueStage issueStage = new IssueStage();
            RenameRegisterFile rrf = new RenameRegisterFile();

            // First Cycle
            dispatchStage.runCycle();
            decodeStage.runCycle();
            fetchStage.Fetch();


            // Second Cycle
            dispatchStage.runCycle();
            decodeStage.runCycle();
            fetchStage.Fetch();

            // Third Cycle
            dispatchStage.runCycle();


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
