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


            Instruction foo = new Instruction("4204312 addiu r9,r9,-1");



            System.Console.WriteLine("Press Any Key To Exit\n");
            System.Console.Read();
        }
    }
}
