using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Combined_XML_Program
{
    class LINQ_to_XML_Test
    {
        private static List<Filter> filterChoice = new();

        public static void Start()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            bool isContinue = false;
            int filter = 0;

            filterChoice = GetFilterOptions(basePath, "Filters");

            filter = GetUserInputFilters(isContinue, filterChoice, filter);

            var filterList = GetFilters(filterChoice,filter);

            var xmlFiles = GetXMLFiles(basePath, "XML");

            GetExtractors(xmlFiles, filterList);
        }

        private static List<Filter> GetFilterOptions(string basePath, string typePath)
        {
            string path = (basePath + typePath + @"\");
            List<Filter> valuesCollection = new List<Filter>();
            var files = Directory.GetFiles(path, "*.txt");
            foreach (var file in files)
            {
                valuesCollection.Add(new Filter(path, file.Remove(0, path.Length)));
            }

            return valuesCollection;
        }

        private static int GetUserInputFilters(bool isContinue, List<Filter> filterChoice, int filter)
        {
            do
            {
                filter = 0;
                isContinue = false;
                bool IsCorrectSelection = false;

                Console.WriteLine("Please Select Filter Type:");
                for (int i = 0; i < filterChoice.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {filterChoice[i].Filename}");
                }

                while (!IsCorrectSelection)
                {
                    var success = int.TryParse(Console.ReadLine(), out filter);
                    if (success && Enumerable.Range(1, filterChoice.Count).Contains(filter))
                    {
                        Console.WriteLine($"{filterChoice[filter - 1].Filename} selected.");
                        IsCorrectSelection = true;
                        filter--;
                    }
                    else
                    {
                        Console.WriteLine("You have entered an incorrect value, select again");
                    }
                }
            } while (isContinue);

            return filter;
        }

        private static List<string> GetFilters(List<Filter> filterChoice, int filter)
        {
            /* List<string> _filters = new List<string>();
             _filters.Add(filterChoice[filter].Filename);
             return _filters;*/
            List<string> valuesCollection = new List<string>();

            using (var sr = new StreamReader(filterChoice[filter].Path+filterChoice[filter].Filename))
            {
                string l = string.Empty;

                while ((l = sr.ReadLine()) != null)
                {
                    var result1=l.Replace(',', ' ');
                    var result2=result1.Trim();
                    valuesCollection.Add(result2);
                }

                return valuesCollection;
            }
        }

        private static List<string> GetValues(string path)
        {
            List<string> valuesCollection = new List<string>();

            using (var sr = new StreamReader(path))
            {
                string l = string.Empty;

                while ((l = sr.ReadLine()) != null)
                {
                    valuesCollection.Add(l);
                }

                return valuesCollection;
            }
        }

        private static List<XmlFiles> GetXMLFiles(string basePath, string typePath)
        {
            string path = (basePath + typePath + @"\");
            string[] files = Directory.GetFiles(basePath + typePath + @"\", "*.xml");
            List<XmlFiles> valuesCollection = new List<XmlFiles>();
            foreach (var file in files)
            {
                var filename = file;
                int index = filename.LastIndexOf(@"\") + 1;
                if (index >= 0)
                {
                    filename = filename.Substring(index, filename.Length - index);
                }

                int index1 = filename.LastIndexOf(@".");
                if (index1 >= 0)
                {
                    filename = filename.Substring(0, index1);
                }

                var PathFile = basePath + @"\" + typePath + @$"\{(filename)}-text" + ".txt";
                var NameFile = basePath + @"\" + typePath + @$"\{(filename)}-names" + ".txt";

                if (File.Exists(PathFile))
                {
                    File.Delete(PathFile);
                }

                if (File.Exists(NameFile))
                {
                    File.Delete(NameFile);
                }

                valuesCollection.Add(new XmlFiles(path, file.Remove(0, path.Length), PathFile, NameFile));
            }

            return valuesCollection;
        }

        public static void GetExtractors(List<XmlFiles> xmlFiles, List<string> filterList)
        {
            foreach (var entry in xmlFiles)
            {


                XElement BASE = XElement.Load(entry.Path+entry.Filename);

                //pointing at wrong xml trying to feed it the extractor one when it's looking for a blueprint
                IEnumerable<XElement> Drones =
                    from items in BASE.Descendants("PILLBOX").Descendants("INIT")
                    where filterList.Any(s => ((string) items.Element("NAME") ?? "").Contains(s))
                    select items;


                foreach (var i in Drones)
                {
                    Console.WriteLine(i);
                    var results = i.Element("NAME").Value;

                    File.AppendAllText(entry.NameFile,
                        ($"{i.Element("NAME").Value}{Environment.NewLine}"));
                }
            }
        }
    }

    internal class XmlFiles
    {
        public string Path { get; set; }
        public string Filename { get; set; }
        public string PathFile { get; set; }
        public string NameFile { get; set; }  

        public XmlFiles(string path, string filename,string pathFile,string nameFile)
        {
            Path = path;
            Filename = filename;
            PathFile = pathFile;
            NameFile = nameFile;
        }
    }


    internal class Filter
    {
        public string Path { get; set; }
        public string Filename { get; set; }

        public Filter(string path, string filename)
        {
            Path = path;
            Filename = filename;
        }
    }
}

