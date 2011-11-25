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

            while (testDecode.getEmptySlots() > 0 && !testFetch.isEmpty())
            {
                testDecode.addInstructionToBuffer(testFetch.getInstruction());
            }

            System.Console.WriteLine(testDecode.getEmptySlots());

            testDecode.testRemoveInstruction();
            testDecode.testRemoveInstruction();

            System.Console.WriteLine(testDecode.getEmptySlots());

            testFetch.Fetch();

            while (testDecode.getEmptySlots() > 0 && !testFetch.isEmpty())
            {
                testDecode.addInstructionToBuffer(testFetch.getInstruction());
            }

            System.Console.WriteLine(testDecode.getEmptySlots());

            testDecode.testRemoveInstruction();
            testDecode.testRemoveInstruction();
            testDecode.testRemoveInstruction();
            testDecode.testRemoveInstruction();

            System.Console.WriteLine(testDecode.getEmptySlots());

            testFetch.Fetch();

            while (testDecode.getEmptySlots() > 0 && !testFetch.isEmpty())
            {
                testDecode.addInstructionToBuffer(testFetch.getInstruction());
            }

            System.Console.WriteLine(testDecode.getEmptySlots());

            testDecode.testRemoveInstruction();
            testDecode.testRemoveInstruction();
            testDecode.testRemoveInstruction();
            testDecode.testRemoveInstruction();
            testDecode.testRemoveInstruction();

            System.Console.WriteLine(testDecode.getEmptySlots());

            testFetch.Fetch();

            System.Console.WriteLine(testDecode.getEmptySlots());

        }

    }
}
