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
            FetchStage testFetch = new FetchStage(4, "..\\..\\InputFiles\\fpppp.tra");

            testFetch.Fetch();
        }
    }
}
