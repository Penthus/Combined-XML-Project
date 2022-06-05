using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XML_Serializer.XML;

namespace Combined_XML_Program
{
    class FilterHulls
    {
        public static void Start()
        {
            var filteredResultsPath = Program.basePath + "Spawn" + $@"\Filtered.txt";

            //var xmls = GetHullList();

            var hullConfigs = Program.basePath + "Spawn" + $@"\Hull Configs.txt";

            var filters = ProcessShipFiltersList(hullConfigs);
            if (File.Exists(filteredResultsPath))
            {
                File.Delete(filteredResultsPath);
            }
            //GetFiltered(xmls, filteredResultsPath, filters);
        }
/*
        private static List<string> GetHullList()
        {
            string xml = "C:\\SS\\dev\\content\\data\\items\\balancesheets3\\hulls_perky.xml";
            string xml1 = "C:\\SS\\dev\\content\\data\\items\\hullai.xml";
            string xml2 = "C:\\SS\\dev\\content\\data\\items\\hullalien.xml";
            string xml3 = "C:\\SS\\dev\\content\\data\\items\\hullfreighters.xml";
            string xml4 = "C:\\SS\\dev\\content\\data\\items\\hullmissions.xml";
            string xml5 = "C:\\SS\\dev\\content\\data\\items\\hulltest.xml";

            List<string> xmls = new List<string>();
            xmls.Add(xml);
            xmls.Add(xml1);
            xmls.Add(xml2);
            xmls.Add(xml3);
            xmls.Add(xml4);
            xmls.Add(xml5);
            return xmls;
        }
*/
        public static List<Item> GetBaseGearValues(string path)
        {
            List<Item> valuesCollection = new();

            using var sr = new StreamReader(path);
            string line = string.Empty;

            while ((line = sr.ReadLine()) != null)
            {
                valuesCollection.Add(new Item(line));
            }

            return valuesCollection;
        }

        public static List<string> ProcessFilter(string path)
        {
            List<string> valuesCollection = new();
            using var sr = new StreamReader(path);
            string l = string.Empty;
            while ((l = sr.ReadLine()) != null)
            {
                valuesCollection.Add(l);
            }

            return valuesCollection;
        }

        public static List<ShipFilter> ProcessShipFiltersList(string path)
        {
            List<ShipFilter> valuesCollection = new();
            using var sr = new StreamReader(path);
            string l = string.Empty;
            while ((l = sr.ReadLine()) != null)
            {
                var parts = l.Split(',');
                valuesCollection.Add(new ShipFilter(parts[0], parts[1]));
            }

            return valuesCollection;
        }

        public static void GetFiltered(List<string> xmls, string filteredPath, List<ShipFilter> filterList)
        {

        //    List<string> filter = new List<string>();

        //    foreach (var filters in filterList)
        //    {
        //        filter.Add(filters.ShipHull);
        //    }

        //    foreach (var xml in xmls)
        //    {
        //        var Xml = SS_Serializer_HullList.HULLLIST.LoadFromFile(xml);
        //        foreach (var hull in Xml.HULL)
        //        {
        //            foreach (var name in hull.INIT)
        //            {
        //                if (filter.Any(s => (name.NAME.Equals(s))))
        //                {
        //                    Console.WriteLine(
        //                        $"{name.NAME}:{hull.VALUES[0].TECHLEVEL} is in the filters list of ship names");
        //                    File.AppendAllText(filteredPath,
        //                        $"{name.NAME},{hull.VALUES[0].TECHLEVEL}{Environment.NewLine}");
        //                }
        //            }
        //        }
        //    }
        }
    }
}

