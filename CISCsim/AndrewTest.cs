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
            DecodeStage decodeStage = new DecodeStage();
            FetchStage fetchStage = new FetchStage(Config.traceFilename);
            IssueStage issueStage = new IssueStage();
            RenameRegisterFile rrf = new RenameRegisterFile();

            // First Cycle
            decodeStage.runCycle(fetchStage, issueStage, rrf);
            fetchStage.Fetch();
            

            // Second Cycle
            decodeStage.runCycle(fetchStage, issueStage, rrf);


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
