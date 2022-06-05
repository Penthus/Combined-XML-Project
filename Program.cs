using System;
using System.IO;

namespace Combined_XML_Program
{
    class Program
    {
        public static Config config = new();
        public static string basePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string folderPath = "\\XML\\";
        public static string[] fileEntries = Directory.GetFiles(Program.basePath + Program.folderPath, "*.xml", SearchOption.AllDirectories);

        public static int Selection;

        static void Main()
        {
            bool isValid = false;
            bool wantToContinue = true;
             //Selection = StateManager.Start();
            do
            {
                Selection = StateManager.Start();
                if (Selection == 1)
                {
                    XmlParser.Start();
                    wantToContinue = StateManager.WantToContinue();
                }
                else if (Selection == 2)
                {
                    FilterHulls.Start();
                }
                else if (Selection == 3)
                {
                    XmlValueReplacement.Start();
                }
                else
                {
                    Console.WriteLine("Your selection is invalid");
                    Console.WriteLine("Try again");
                }
            } while (!isValid && wantToContinue);
        }
    }
}
