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
            List<string> configs = Program.config.GetConfigs();
            //var baseGearConfigs = Program.basePath + "Spawn" + $@"\BaseGear Configs.txt";
           /* foreach (var config in configs)
            {
                
            }
          //  var baseGearFilterList = FilterHulls.GetBaseGearValues(baseGearConfigs);
           // ExtractNamesForFilter();
          //  ChangeBaseGearWarping(Program.fileEntries,baseGearFilterList);
           */
        }

        private static List<string> ExtractNamesForFilter(List<Item> baseGearFilterList)
        {
            throw new NotImplementedException();
        }

        private static void ChangeBaseGearWarping(string[] fileEntries)
        {
           /* List<string> _filter = new();
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
                    select Gear;*/

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


