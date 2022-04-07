using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using XML_Serializer.XML;

namespace Combined_XML_Program
{
    class XML_Serializer
    {
        public static int GLOBALCOUNTER = 0;
        public static void Start()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            //string[] fileEntries = Directory.GetFiles(basePath + "\\XML\\Drones\\", "*.xml");


            //string[] customGalFileEntries = Directory.GetFiles("C:\\SS\\dev\\content\\data\\customgals2", "*.xml", SearchOption.AllDirectories);
            //string[] shipyardFileEntries = Directory.GetFiles("C:\\SS\\dev\\content\\data\\ships", "*.xml", SearchOption.AllDirectories);
            //string[] combinedFileEntries = customGalFileEntries.Union(shipyardFileEntries).ToArray();
            //List<XmlTypes> xmlTypes = GetXmlTypes(combinedFileEntries);

            string[] fileEntries = Directory.GetFiles(basePath + "\\XML\\Bases\\", "*.xml");
            List<XmlTypes> xmlTypes = GetXmlTypes(fileEntries);

            bool hasClearedGal = false;
            bool hasClearedShip = false;
            bool hasClearedDrone = false;
            bool hasClearedBaseGear = false;
            var combined = basePath + "Spawn" + $@"\Combined.txt";
            var shipConfigs = basePath + "Spawn" + $@"\Ship Configs.txt";
            var hullConfigs = basePath + "Spawn" + $@"\Hull Configs.txt";
            var droneConfigs = basePath + "Spawn" + $@"\Drone Configs.txt";
            var baseGearConfigs = basePath + "Spawn" + $@"\BaseGear Configs.txt";

            DecideXmlToUse(xmlTypes, hasClearedGal, combined, shipConfigs, hasClearedShip, hullConfigs, hasClearedDrone, droneConfigs, hasClearedBaseGear, baseGearConfigs);
        }

        private static void DecideXmlToUse(List<XmlTypes> xmlTypes, bool hasClearedGal, string combined, string shipConfigs,
            bool hasClearedShip, string hullConfigs, bool hasClearedDrone, string droneConfigs, bool hasClearedBaseGear, string baseGearConfigs)
        {
            foreach (var xmlType in xmlTypes)
            {
                switch (xmlType.Root)
                {
                    case "GALAXY":
                        if (!hasClearedGal)
                        {
                            if (File.Exists(combined))
                            {
                                File.Delete(combined);
                            }

                            if (File.Exists(shipConfigs))
                            {
                                File.Delete(shipConfigs);
                            }

                            hasClearedGal = true;
                        }

                        SpawnListXML(shipConfigs, combined, xmlType.Path);
                        break;

                    case "HULLLIST":
                        if (!hasClearedShip)
                        {
                            if (File.Exists(hullConfigs))
                            {
                                File.Delete(hullConfigs);
                            }

                            hasClearedShip = true;
                        }

                        ShipyardXML(shipConfigs, hullConfigs, xmlType.Path);
                        break;

                    case "PILLBOXLIST":
                        if (!hasClearedDrone)
                        {
                            if (File.Exists(droneConfigs))
                            {
                                //File.Delete(droneConfigs);
                            }

                            hasClearedShip = true;
                        }

                        DroneXML(droneConfigs, xmlType.Path);
                        break;

                    case "BASELIST":
                        if (!hasClearedDrone)
                        {
                            if (File.Exists(baseGearConfigs))
                            {
                                //File.Delete(baseGearConfigs);
                            }

                            hasClearedBaseGear = true;
                        }

                        BaseGearXML(baseGearConfigs, xmlType.Path);
                        break;
                    default:
                        Console.WriteLine($"{xmlType} is not recognised");
                        break;
                }
            }
        }

        public static List<XmlTypes> GetXmlTypes(string[] fileEntries)
        {
            List<XmlTypes> valueCollection = new();
            foreach (var fileEntry in fileEntries)
            {
                XmlDocument doc = new();
                doc.Load(fileEntry);
                string Xml = doc.InnerXml;
                using var reader = XmlReader.Create(new StringReader(Xml));
                reader.MoveToContent();
                switch (reader.Name)
                {
                    case "GALAXY":
                        valueCollection.Add(new(fileEntry, reader.Name, Xml));
                        break;

                    case "HULLLIST":
                        valueCollection.Add(new(fileEntry, reader.Name, Xml));
                        break;

                    case "PILLBOXLIST":
                        valueCollection.Add(new(fileEntry, reader.Name, Xml));
                        break;

                    case "BASELIST":
                        valueCollection.Add(new(fileEntry, reader.Name, Xml));
                        break;

                    default:
                        Console.WriteLine($"{reader.Name}: is not a recognised Root Node");
                        break;
                }
            }
            return valueCollection;
        }

        private static void SpawnListXML(string? ShipConfigs, string? Combined, string? path)
        {

            var Xml = SS_Serializer_SpawnList.GALAXY.LoadFromFile(path);
            var filename = System.IO.Path.GetFileNameWithoutExtension(path);

            foreach (var g in Xml.SPAWNLIST)
            {
                foreach (var s in g.SPAWNOBJ)
                {
                    foreach (var i in s.SPAWNAMBUSH)
                    {
                        foreach (var j in i.SPAWNER)
                        {
                            string hull = GetHullConfiguration(i);
                            ProcessXmlData(j, ShipConfigs, Combined, hull);
                        }
                    }

                    foreach (var i in s.SPAWNBOSS)
                    {
                        foreach (var j in i.SPAWNER)
                        {
                            ProcessXmlData(j, ShipConfigs, Combined);
                        }
                    }

                    foreach (var j in s.SPAWNER)
                    {
                        ProcessXmlData(j, ShipConfigs, Combined);
                    }
                }
            }
        }

        private static void ShipyardXML(string? shipConfigs, string? hullConfigs, string? path)
        {
            var filters = Filtering.ProcessFilter(shipConfigs);
            var Xml = SS_Serializer_Shipyard.SHIPYARD.LoadFromFile(path);

            foreach (var ship in Xml.SHIP)
            {
                if (filters.Any(s => (ship.name.Equals(s))))
                {
                    Console.WriteLine($"{ship.HULL}:{ship.name} is in the filters list of ship names");
                    File.AppendAllText(hullConfigs, $"{ship.name},{ship.HULL}{Environment.NewLine}");
                }
            }
        }
        private static void DroneXML(string? droneConfigs, string? path)
        {

            var filters = Filtering.ProcessFilter(droneConfigs);

            // Serialization approach

            var Xml = SS_Serializer_PillboxList.PILLBOXLIST.LoadFromFile(path);

            foreach (var pillbox in Xml.PILLBOX)
            {
                foreach (var init in pillbox.INIT)
                {
                    if (filters.Any(s => init.NAME.Equals(s)))
                    {
                        Console.WriteLine($"{init.NAME}");
                        Console.WriteLine(init.TSLENABLED);
                        init.TSLENABLED = !init.TSLENABLED;
                        Console.WriteLine(init.TSLENABLED);
                        Console.WriteLine(pillbox.VALUES[0].PREVENTWARP);
                        Console.WriteLine(pillbox.VALUES[0].TECHLEVEL);
                        // init.Element("VALUES").SetElementValue();
                    }
                    //File.AppendAllText(hullConfigs, $"{ship.name},{ship.HULL}{Environment.NewLine}");
                }
            }

            // LINQ approach

            XElement pillboxXml = XElement.Load(path);

            var SuperItems = pillboxXml.Descendants("PILLBOX")
                .Descendants("INIT")
                .Where(Pillbox => filters.Any(s => ((string)Pillbox.Element("NAME") ?? "").Equals(s)));

            foreach (var superItem in SuperItems)
            {
                Console.WriteLine(superItem);
                if (superItem.HasElements.Equals("TSLENABLED"))
                {
                    Console.WriteLine($"______----------______________ {superItem.Descendants("NAME").ToString()}");
                }
            }

        }

        private static void BaseGearXML(string baseGearConfigs, string path)
        {

            var filters = Filtering.ProcessFilter(baseGearConfigs);
            string[] filterA = new[]
            {
                "Arcadian",
                "Apollo",
                "Ares",
                "Adonis",
                "Argonaut",
                "Ambrosia",
                "Andaman",
                "Achilles",
                "Annihilator"
            };
            string[] filterB = new[]
            {
                "X",
                "Y",
                "Z"
            };

            List<string> filtersA = new List<string>(filterA);
            List<string> filtersB = new List<string>(filterB);
            // Serialization approach

            var Xml = SS_Serializer_BaseGearBlueprintsDecay.BASELIST.LoadFromFile(path);

            foreach (var bases in Xml.BASE)
            {
                foreach (var init in bases.INIT)
                {
                    //if (filters.Any(s => init.NAME.Equals(s)))
                    if (MatchingResult(init.NAME, filtersA, filtersB))
                    {
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine(init.NAME);
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine();
                        foreach (var product in bases.VALUES[0].PRODUCTS)
                        {
                            foreach (var item in product.ITEM)
                            {
                                Console.WriteLine(item.name);
                            }
                        }

                        Console.WriteLine();

                        // init.Element("VALUES").SetElementValue();
                    }
                    //File.AppendAllText(hullConfigs, $"{ship.name},{ship.HULL}{Environment.NewLine}");
                }
            }
        }

        private static bool MatchingResult(string input, List<string> filterA, List<string> filterB)
        {
            bool output;
            if (filterA.Any(s => input.Contains(s)))
            {
                if (filterB.Any(s => input.Contains(s)))
                {
                    output = true;
                    return output;
                }

                return false;
            }

            return false;
        }
        private static string GetHullConfiguration(SS_Serializer_SpawnList.SPAWNAMBUSH i)
        {
            foreach (var ambushers in i.AMBUSHERS_CONFIGURATION)
            {
                foreach (var ambusher in ambushers.AMBUSHER)
                {
                    if (ambusher.HULL_CONFIGURATION != null)
                    {
                        Console.WriteLine(ambusher.HULL_CONFIGURATION);
                        return ambusher.HULL_CONFIGURATION;
                    }

                }
            }

            return "";
        }

        private static void ProcessXmlData(SS_Serializer_SpawnList.SPAWNER j, string configs, string combined)
        {
            if (j.LOCKOUTNAME?.Length > 0 && j.LOCKOUTFIXED?.period.Length > 0)
            {
                Console.WriteLine(
                    $"Name: {j.NAMEDMOB}, Lockout Name: {j.LOCKOUTNAME}, Lockout Time: {j.LOCKOUTFIXED.period}, Ship Config: {j.SHIPCONFIG} Level: {j.SPAWNBASE.LEVEL}");
                File.AppendAllText(configs, $"{ j.SHIPCONFIG}{Environment.NewLine}");
                File.AppendAllText(combined,
                    $"ID, {GLOBALCOUNTER}, Name, {j.NAMEDMOB}, Lockout Name, {j.LOCKOUTNAME}, Lockout Time, {j.LOCKOUTFIXED.period}, Ship Config, {j.SHIPCONFIG}, Level, {j.SPAWNBASE.LEVEL} {Environment.NewLine}");
                GLOBALCOUNTER++;
            }
        }

        private static void ProcessXmlData(SS_Serializer_SpawnList.SPAWNER j, string configs, string combined, string hull)
        {
            if (j.LOCKOUTNAME?.Length > 0 && j.LOCKOUTFIXED?.period.Length > 0)
            {
                Console.WriteLine(
                    $"Name: {j.NAMEDMOB}, Lockout Name: {j.LOCKOUTNAME}, Lockout Time: {j.LOCKOUTFIXED.period}, Ship Config: {hull}, Level: {j.SPAWNBASE.LEVEL}");
                File.AppendAllText(configs, $"{hull}{Environment.NewLine}");
                File.AppendAllText(combined,
                    $"ID, {GLOBALCOUNTER}, Name, {j.NAMEDMOB}, Lockout Name, {j.LOCKOUTNAME}, Lockout Time, {j.LOCKOUTFIXED.period}, Ship Config, {hull}, Level, {j.SPAWNBASE.LEVEL} {Environment.NewLine}");
                GLOBALCOUNTER++;
            }
        }
    }
}
