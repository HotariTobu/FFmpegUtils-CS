using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Class1
    {
        static void Main()
        {
            int i = 0;
            while (true)
            {
                //Console.WriteLine($"{i:000}");
                //Console.Write($"{i:000}\n");
                Console.Write($"{i:000}\t\t\t\t\t\t\t\t\t\t\t\t\t");
                System.Threading.Thread.Sleep(1000);
                i++;
            }
        }
    }
}
