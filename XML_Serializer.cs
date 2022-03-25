using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using XML_Serializer.XML;

namespace Combined_XML_Program
{
    class XML_Serializer
    {
        public static void Start()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            string[] fileEntries = Directory.GetFiles(basePath + "\\XML\\", "*.xml");
            List<XmlTypes> xmlTypes = GetXmlTypes(fileEntries);

            foreach (var xmlType in xmlTypes)
            {
                switch (xmlType.Root)
                {
                    case "GALAXY":
                        SpawnListXML(basePath, "Spawn", xmlType.Path);
                        break;
                    default:
                        Console.WriteLine($"{xmlType} is not recognised");
                        break;
                }
            }
            //InventoryXML(basePath, "Inventory"); //Broken Somehow?

            //RepairKitsXML(basePath, "Repair Kits");

            //StationKitsXML(basePath, "Station Kits");

            //SpawnListXML(basePath, "Spawn", xmlTypes);

        }

        private static List<XmlTypes> GetXmlTypes(string[] fileEntries)
        {
            List<XmlTypes> valueCollection = new List<XmlTypes>();
            foreach (var fileEntry in fileEntries)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileEntry);
                string Xml = doc.InnerXml;
                using (var reader = XmlReader.Create(new StringReader(Xml)))
                {
                    reader.MoveToContent();
                    if (reader.Name == "GALAXY")
                    {
                        Console.WriteLine(reader);
                        valueCollection.Add(new XmlTypes(fileEntry, reader.Name, Xml));
                    }
                    else
                    {
                        Console.WriteLine($"{reader.Name}: is not a recognised Root Node");
                    }
                }
            }
            return valueCollection;
        }
        private static void InventoryXML(string? basePath, string? typePath)
        {
            string[] fileEntries = Directory.GetFiles(basePath + typePath + @"/", "*.xml");
            foreach (var entry in fileEntries)
            {
                var Xml = SS_Serializer_Inventory.Inventory.LoadFromFile(entry);
                var Path = basePath + @"\" + typePath + @"\text.txt";
                var Names = basePath + @"\" + typePath + @"\names.txt";

                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }

                if (File.Exists(Names))
                {
                    File.Delete(Names);
                }

                Console.WriteLine();
                foreach (var s in Xml.SHIP)
                {
                    Console.WriteLine(s.name);
                    foreach (var item in s.ITEMLIST)
                    {
                        bool b = Convert.ToBoolean(item.eqp);
                        Console.WriteLine($"Name: {item.nm} / Equipped: {b} / ID: {item.id} / Mods: {item.m}");
                    }
                }
            }
        }

        private static void RepairKitsXML(string? basePath, string? typePath)
        {
            string[] fileEntries = Directory.GetFiles(basePath + typePath + @"/", "*.xml");
            foreach (var entry in fileEntries)
            {
                var Xml = SS_Serializer_RepairKits.MISCLIST.LoadFromFile(entry);
                var Path = basePath + @"\" + typePath + @"\text.txt";
                var Names = basePath + @"\" + typePath + @"\names.txt";

                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }

                if (File.Exists(Names))
                {
                    File.Delete(Names);
                }

                Console.WriteLine();

                foreach (var s in Xml.MISC)
                {
                    Console.WriteLine(s.@class);
                    foreach (var i in s.INIT)
                    {
                        Console.WriteLine($"Name: {i.NAME}, Tech Level: {s.VALUES[0].TECHLEVEL}");
                        File.AppendAllText(Path,
                            $"Name, {i.NAME}, Tech Level, {s.VALUES[0].TECHLEVEL} {Environment.NewLine}");
                        File.AppendAllText(Names, $"{i.NAME} {Environment.NewLine}");
                    }
                }
            }
        }

        private static void StationKitsXML(string? basePath, string? typePath)
        {

            string[] fileEntries = Directory.GetFiles(basePath + typePath + @"\", "*.xml");
            foreach (var entry in fileEntries)
            {
                var filename = entry;
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

                var Xml = SS_Serializer_StationKits.BASELIST.LoadFromFile(entry);
                var Path = basePath + @"\" + typePath + @$"\{(filename)}-text" + ".txt";
                var Names = basePath + @"\" + typePath + @$"\{(filename)}-names" + ".txt";

                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }

                if (File.Exists(Names))
                {
                    File.Delete(Names);
                }

                Console.WriteLine();

                foreach (var s in Xml.BASE)
                {
                    Console.WriteLine(s.@class);
                    foreach (var i in s.INIT)
                    {
                        Console.WriteLine($"Name: {i.NAME}, Tech Level: {s.VALUES[0].TECHLEVEL}");
                        File.AppendAllText(Path,
                            $"Name, {i.NAME}, Tech Level, {s.VALUES[0].TECHLEVEL} {Environment.NewLine}");
                        File.AppendAllText(Names, $"{i.NAME} {Environment.NewLine}");
                    }
                }
            }
        }

        private static void SpawnListXML(string? basePath, string? typePath, string? path)
        {

            var Xml = SS_Serializer_SpawnList.GALAXY.LoadFromFile(path);
            var s1 = path.Replace(basePath, "");
            var s2 = s1.Replace(typePath, "");
            var s3 = s2.Replace('\\', ' ');
            s3 = s3.Trim();
            var filename = s3.Remove(s3.IndexOf('.'));
            var Combined = basePath + typePath + $@"\Combined.txt";
            var Path = basePath + typePath + $@"\{filename}-text.txt";
            var Names = basePath + typePath + $@"\{filename}-names.txt";

            if (File.Exists(Path))
            {
                File.Delete(Path);
            }

            if (File.Exists(Names))
            {
                File.Delete(Names);
            }


            //this needs to be made to work for various schema
            foreach (var g in Xml.SPAWNLIST)
            {
                foreach (var s in g.SPAWNOBJ)
                {
                    foreach (var i in s.SPAWNAMBUSH)
                    {
                        foreach (var j in i.SPAWNER)
                        {
                            if (j.LOCKOUTNAME?.Length > 0)
                            {
                                Console.WriteLine($"Name: {j.NAMEDMOB}, Lockout Name: {j.LOCKOUTNAME}");
                                File.AppendAllText(Path,
                                    $"Name, {j.NAMEDMOB}, Lockout Name, {j.LOCKOUTNAME} {Environment.NewLine}");
                                File.AppendAllText(Names, $"{j.NAMEDMOB} {Environment.NewLine}");
                                foreach (var ambushers in i.AMBUSHERS_CONFIGURATION)
                                {
                                    foreach (var ambusher in ambushers.AMBUSHER)
                                    {
                                        Console.WriteLine(ambusher.HULL_CONFIGURATION);
                                        File.AppendAllText(Names,
                                            $"{ambusher.HULL_CONFIGURATION} {Environment.NewLine}");
                                    }
                                }
                            }
                        }
                    }

                    foreach (var i in s.SPAWNBOSS)
                    {
                        foreach (var j in i.SPAWNER)
                        {
                            if (j.LOCKOUTNAME?.Length > 0)
                            {
                                Console.WriteLine(
                                    $"Name: {j.NAMEDMOB}, Lockout Name: {j.LOCKOUTNAME}, Lockout Time: {j.period}|{j.LOCKOUTFIXED}, Ship Config: {j.SHIPCONFIG}");
                                File.AppendAllText(Path,
                                    $"Name, {j.NAMEDMOB}, Lockout Name, {j.LOCKOUTNAME}, Lockout Time, {j.period}, Ship Config, {j.SHIPCONFIG} {Environment.NewLine}");
                                File.AppendAllText(Names, $"{j.NAMEDMOB} {Environment.NewLine}");
                                File.AppendAllText(Combined,
                                    $"Name, {j.NAMEDMOB}, Lockout Name, {j.LOCKOUTNAME}, Lockout Time, {j.period}, Ship Config, {j.SHIPCONFIG} {Environment.NewLine}");
                            }
                        }
                    }

                    foreach (var j in s.SPAWNER)
                    {
                        if (j.LOCKOUTNAME?.Length > 0)
                        {
                            Console.WriteLine(
                                $"Name: {j.NAMEDMOB}, Lockout Name: {j.LOCKOUTNAME}, Lockout Time: {j.period}|{j.LOCKOUTFIXED}, Ship Config: {j.SHIPCONFIG}");
                            File.AppendAllText(Path,
                                $"Name, {j.NAMEDMOB}, Lockout Name, {j.LOCKOUTNAME}, Lockout Time, {j.period}, Ship Config, {j.SHIPCONFIG} {Environment.NewLine}");
                            File.AppendAllText(Names, $"{j.NAMEDMOB} {Environment.NewLine}");
                            File.AppendAllText(Combined,
                                $"Name, {j.NAMEDMOB}, Lockout Name, {j.LOCKOUTNAME}, Lockout Time, {j.period}, Ship Config, {j.SHIPCONFIG} {Environment.NewLine}");
                        }

                    }
                }
            }
        }
    }
}
