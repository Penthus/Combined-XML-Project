using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml;

namespace Combined_XML_Program
{
    class XmlValueReplacement
    {
        public static void Start()
        {
            //  var xmlFilename = "superitems.xml";
            //  var txtFilename = "superitems Info.txt";

            //var xmlFilename = "drones_permanent.xml";
            //var txtFilename = "drones Info.txt";
            //var writeFilename = "Write.txt";
            //var currentDirectory = Directory.GetCurrentDirectory();
            //var xmlPath = Path.Combine(currentDirectory, xmlFilename);
            //var txtPath = Path.Combine(currentDirectory, txtFilename);
            //var writePath = Path.Combine(currentDirectory, writeFilename);
            //XElement xmlSuperItems = XElement.Load(xmlPath);
            //var superItemFilterList = Filtering.GetDroneValues(txtPath);

            // ChangeSuperItemChargingTime(xmlSuperItems,dronesList);
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] fileEntries = Directory.GetFiles(basePath + "\\XML\\Bases\\Gear\\", "*.xml");
            var baseGearConfigs = basePath + "Spawn" + $@"\BaseGear Configs.txt";
            var baseGearFilterList = Filtering.GetBaseGearValues(baseGearConfigs);
            ChangeBaseGearWarping(fileEntries,baseGearFilterList);

        }

        private static void PrintDroneInfo(XElement xmlDrones, List<Drone> dronesList, string writePath)
        {
            List<string> _filter = new();
            foreach (var p in dronesList)
            {
                _filter.Add(p.NAME);
            }

            // var query = SuperItem.Where(x => _filter.Any(x => (SuperItem.Elements("ITEM").Attributes("name").Contains(x))));

            var Drones =
                from name in xmlDrones.Descendants("PILLBOXLIST").Descendants("VALUES").Descendants("DEPLOYMENTLIST")
                where _filter.Any(s => (name.Elements("ITEM").Select(e => e.Attribute("name").Value ?? "").Contains(s)))
                select name;

            foreach (var drone in Drones)
            {
                Console.WriteLine(drone);
                File.AppendAllText(writePath,
                    ($"{drone.Parent.Parent.Element("INIT").Element("NAME").Value}{Environment.NewLine}"));
                foreach (var attributes in drone.Elements("ITEM").Attributes("name"))
                {
                    File.AppendAllText(writePath,
                        ($"{attributes}{Environment.NewLine}"));
                    Console.WriteLine(attributes);
                }
            }
            Console.WriteLine();
        }
        private static void ChangeBaseGearWarping(string[] fileEntries, List<Item> baseGearFilterList)
        {
            List<string> _filter = new();
            foreach (var items in baseGearFilterList)
            {
                _filter.Add(items.Name);
            }

            //TODO Fix this
            foreach (var xmlPath in fileEntries)
            {
                XElement xmlBaseGear = XElement.Load(xmlPath);

                var testCollection = from test in xmlBaseGear.Nodes()
                    select test;

                var itemCollection = testCollection.Select(o => (o.Descendants().ElementAt(0), o.Descendants().ElementAt(1)));

                //var items = xmlBaseGear.Select(o => (o.Descendants().ElementAt(0), o.Descendants().ElementAt(1).ToList()));

                foreach (var superItem in itemCollection)
                {
                    Console.WriteLine($"Item 1:{superItem.Item1}, Item 2:{superItem.Item2}");
                }

                var BaseGear =
                    from Gear in xmlBaseGear.Descendants("MISC").Descendants("INIT")
                    where _filter.Any(s => ((string) Gear.Element("NAME") ?? "").Contains(s))
                    select Gear;

                //foreach (var superitem in BaseGear)
                //{
                //    Console.WriteLine("------------Change Product Information------------");
                //    Console.WriteLine("Old Product Information");
                //    Console.WriteLine(superitem);

                //    int index = -1;
                //    for (int i = 0; i < baseGearFilterList.Count; i++)
                //    {
                //        if (baseGearFilterList[i].NAME == superitem.Element("NAME")?.Value)
                //        {
                //            index = i;
                //        }
                //    }

                //    Console.WriteLine($"Name:{baseGearFilterList[index].NAME}");
                //    XElement value = superitem.Parent?.Element("VALUES");
                //    Console.WriteLine($"Charging Time:{value.Element("CHARGINGTIME")?.Value}");
                //    value.SetElementValue("CHARGINGTIME", baseGearFilterList[index].CHARGINGTIME);
                //    Console.WriteLine("New Product Information");
                //    Console.WriteLine($"Name:{baseGearFilterList[index].NAME}");
                //    Console.WriteLine($"Charging Time:{value.Element("CHARGINGTIME")?.Value}");
                //}

                //xmlBaseGear.Save("dif.xml");
            }
        }
        private static void ChangeSuperItemChargingTime(XElement xmlSuperItems, List<SuperItem> superItemFilterList)
        {
            List<string> _filter = new();
            foreach (var p in superItemFilterList)
            {
                _filter.Add(p.NAME);
            }

            var SuperItems =
                from SuperItem in xmlSuperItems.Descendants()
                select SuperItem;



            //var SuperItem =
            //    from SuperItem in xmlSuperItems.Descendants("MISC").Descendants("INIT")
            //    where _filter.Any(s => ((string)SuperItem.Element("NAME"))?.Contains(s) ?? false)
            //    select SuperItem;

            foreach (var superitem in SuperItems)
            {
                Console.WriteLine("------------Change Product Information------------");
                Console.WriteLine("Old Product Information");
                Console.WriteLine(superitem);

                int index = -1;
                for (int i = 0; i < superItemFilterList.Count; i++)
                {
                    if (superItemFilterList[i].NAME == superitem.Element("NAME")?.Value)
                    {
                        index = i;
                    }
                }

                Console.WriteLine($"Name:{superItemFilterList[index].NAME}");
                XElement value = superitem.Parent?.Element("VALUES");
                Console.WriteLine($"Charging Time:{value.Element("CHARGINGTIME")?.Value}");
                value.SetElementValue("CHARGINGTIME", superItemFilterList[index].CHARGINGTIME);
                Console.WriteLine("New Product Information");
                Console.WriteLine($"Name:{superItemFilterList[index].NAME}");
                Console.WriteLine($"Charging Time:{value.Element("CHARGINGTIME")?.Value}");
            }

            xmlSuperItems.Save("dif.xml");

        }
    }
}


