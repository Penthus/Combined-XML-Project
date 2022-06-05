using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combined_XML_Program
{
    public static class StateManager
    {
        public static int Start()
        {
            Console.WriteLine("Please select a program to start:");
            Console.WriteLine("[1] Xml Parser");
            Console.WriteLine("[2] Filtering");
            Console.WriteLine("[3] XML Value Replacement");
            int output;
            var isValid = int.TryParse(Console.ReadLine(), out output);

            return output;
        }
        public static bool GetUserInput(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Y/N");
            var input = Console.ReadKey();
            if (input.Key==ConsoleKey.Y)
            {
                return true;
            }

            return false;
        }

        public static bool WantToContinue()
        {
            bool output = GetUserInput("Do you wish to continue the program?");
            if (output)
            {
                Program.Selection = 0;
                Console.WriteLine();
                return true;
            }

            return false;
        }
    }
}
