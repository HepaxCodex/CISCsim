using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Testing Class for ben
    /// </summary>
    static class BenTest
    {
        public static void RunTest()
        {
            FetchStage testFetchStage = new FetchStage("..\\..\\InputFiles\\fpppp.tra");
            
            for (int i = 0; i < 200; i++)
            {
                testFetchStage.Fetch();
                testFetchStage.Clear();
            }
        }
    }
}
