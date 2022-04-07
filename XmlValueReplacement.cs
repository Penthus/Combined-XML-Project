using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            // ChangeProductInfo(xmlSuperItems,dronesList);
        }

        private static void PrintDroneInfo(XElement xmlDrones, List<Drones> dronesList, string writePath)
        {
            List<string> _filter = new();
            foreach (var p in dronesList)
            {
                _filter.Add(p.NAME);
            }

            // var query = SuperItems.Where(x => _filter.Any(x => (SuperItems.Elements("ITEM").Attributes("name").Contains(x))));

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

        private static void ChangeProductInfo(XElement xmlSuperItems, List<SuperItems> superItemFilterList)
        {
            List<string> _filter = new();
            foreach (var p in superItemFilterList)
            {
                _filter.Add(p.NAME);
            }

            var SuperItems =
                from SuperItem in xmlSuperItems.Descendants("MISC").Descendants("INIT")
                where _filter.Any(s => ((string)SuperItem.Element("NAME") ?? "").Contains(s))
                select SuperItem;

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


