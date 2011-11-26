using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World\n");

            //BenTest.RunTest();
            AndrewTest.RunTest();

            System.Console.WriteLine("Press Any Key To Exit\n");
            System.Console.Read();
        }


        /// <summary>
        /// runCycle Prototype ... Not Complete
        /// </summary>
        public static void runCycle()
        {
            //fetchStage.Fetch(); //TODO: This should be fetchStage.RunCycle();
            //decodeStage.RunCycle();
            //issueStage.RunCycle();
            //executeStage.RunCycle();
            //completeStage.RunCycle();
            //writeBageStage.RunCycle();

            // CycleCleanup.fetch2decode();
            // CycleCleanup.decode2issue();
            // CycleCleanup.issue2execute();

        }


    }
}
