using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combined_XML_Program
{
    public static class StateMachine
    {
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
    }
}
