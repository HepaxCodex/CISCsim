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
            FetchStage testFetch = new FetchStage(4, "..\\..\\InputFiles\\fpppp.tra");
            DecodeStage testDecode = new DecodeStage();

            testFetch.Fetch();
            CycleCleanup.fetch2Decode(testFetch,testDecode);

            
            


        }






    }
}
