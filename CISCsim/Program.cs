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
            //AndrewTest.RunTest();

            while (CPU.lastInstructionCompleted == false)
            {
                CPU.completeStage.runCycle();
                CPU.executeStage.runCycle();
                CPU.issueStage.runCycle();
                CPU.dispatchStage.runCycle();
                CPU.decodeStage.runCycle();
                CPU.fetchStage.Fetch();
                System.Console.WriteLine("PC {0}", CPU.pc_count);
            }


            System.Console.WriteLine("Press Any Key To Exit\n");
            System.Console.Read();
        }




    }
}
