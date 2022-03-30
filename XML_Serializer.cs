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

            //string[] fileEntries = Directory.GetFiles(basePath + "\\XML\\", "*.xml");
            string[] fileEntries = Directory.GetFiles("C:\\SS\\dev\\content\\data\\customgals2", "*.xml",SearchOption.AllDirectories);
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
                        //Console.WriteLine(reader);
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

        private static void SpawnListXML(string? basePath, string? typePath, string? path)
        {

            var Xml = SS_Serializer_SpawnList.GALAXY.LoadFromFile(path);
            var filename = System.IO.Path.GetFileNameWithoutExtension(path);
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
                            ProcessXmlData(j, Path, Names, Combined);
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

                    foreach (var i in s.SPAWNBOSS)
                    {
                        foreach (var j in i.SPAWNER)
                        {
                            ProcessXmlData(j, Path, Names, Combined);
                        }
                    }

                    foreach (var j in s.SPAWNER)
                    {
                        ProcessXmlData(j, Path, Names, Combined);
                    }
                }
            }
        }

        private static void ProcessXmlData(SS_Serializer_SpawnList.SPAWNER j, string Path, string Names, string Combined)
        {
            if (j.LOCKOUTNAME?.Length > 0 && j.LOCKOUTFIXED?.period.Length > 0)
            {
                Console.WriteLine(
                    $"Name: {j.NAMEDMOB}, Lockout Name: {j.LOCKOUTNAME}, Lockout Time: {j.LOCKOUTFIXED.period}, Ship Config: {j.SHIPCONFIG} Level: {j.SPAWNBASE.LEVEL}");
                File.AppendAllText(Combined,
                    $"ID, {GLOBALCOUNTER}, Name, {j.NAMEDMOB}, Lockout Name, {j.LOCKOUTNAME}, Lockout Time, {j.LOCKOUTFIXED.period}, Ship Config, {j.SHIPCONFIG}, Level, {j.SPAWNBASE.LEVEL} {Environment.NewLine}");
                GLOBALCOUNTER++;
            }
        }
    }
}
