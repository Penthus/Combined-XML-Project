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
        static void Start()
        {
            //  var xmlFilename = "superitems.xml";
            //  var txtFilename = "superitems Info.txt";
            var xmlFilename = "drones_permanent.xml";
            var txtFilename = "drones Info.txt";
            var writeFilename = "Write.txt";
            var currentDirectory = Directory.GetCurrentDirectory();
            var xmlPath = Path.Combine(currentDirectory, xmlFilename);
            var txtPath = Path.Combine(currentDirectory, txtFilename);
            var writePath = Path.Combine(currentDirectory, writeFilename);
            XElement xmlProducts = XElement.Load(xmlPath);
            var productsList = GetValues(txtPath);
            //GetProductInfo(xmlProducts);
            // ChangeProductInfo(xmlProducts,productsList);
            PrintDroneInfo(xmlProducts, productsList, writePath);
        }

        private static List<Drones> GetValues(string path)
        {
            List<Drones> valuesCollection = new List<Drones>();

            using (var sr = new StreamReader(path))
            {
                string l = string.Empty;

                while ((l = sr.ReadLine()) != null)
                {
                    var parts = l.Split(',');
                    valuesCollection.Add(new Drones(parts[0], Convert.ToInt32(parts[1])));
                }

                return valuesCollection;
            }
        }

        private static void GetProductInfo(XElement xmlProducts)
        {
            Console.WriteLine("------------Get Product Information------------");
            var Drones =
                from name in xmlProducts.Descendants("PILLBOX").Descendants("INIT")
                    //orderby (int)name?.Parent.Element("VALUES").Element("CHARGINGTIME")
                select name;

            foreach (var e in Drones)
            {
                Console.WriteLine(e);

            }
        }

        private static void PrintDroneInfo(XElement xmlProducts, List<Drones> productsList, string writePath)
        {
            List<string> _filter = new List<string>();
            foreach (var p in productsList)
            {
                _filter.Add(p.NAME);
            }

            /*var Products =
                from name in xmlProducts.Descendants("DRONE").Descendants("VALUES").Descendants("DEPLOYMENTLIST")
                where _filter.Any(s => ((string) name.Element("ITEM").Attribute("name") ?? "").Contains(s))
                select name;*/

            var Products =
                from name in xmlProducts.Descendants("PILLBOX").Descendants("VALUES").Descendants("DEPLOYMENTLIST")
                where _filter.Any(s => (name.Elements("ITEM").Select(e => e.Attribute("name").Value ?? "").Contains(s)))
                select name;

            // var query = Products.Where(x => _filter.Any(x => (Products.Elements("ITEM").Attributes("name").Contains(x))));

            foreach (var product in Products)
            {
                Console.WriteLine(product);
                File.AppendAllText(writePath,
                    ($"{product.Parent.Parent.Element("INIT").Element("NAME").Value}{Environment.NewLine}"));
                foreach (var attributes in product.Elements("ITEM").Attributes("name"))
                {
                    File.AppendAllText(writePath,
                        ($"{attributes}{Environment.NewLine}"));
                    Console.WriteLine(attributes);
                }
            }

            Console.WriteLine();
        }

        private static void ChangeProductInfo(XElement xmlProducts, List<Products> productsList)
        {
            List<string> _filter = new List<string>();
            foreach (var p in productsList)
            {
                _filter.Add(p.NAME);
            }

            var Products =
                from name in xmlProducts.Descendants("MISC").Descendants("INIT")
                where _filter.Any(s => ((string)name.Element("NAME") ?? "").Contains(s))
                select name;

            foreach (var e in Products)
            {
                Console.WriteLine("------------Change Product Information------------");
                Console.WriteLine("Old Product Information");
                Console.WriteLine(e);

                int index = -1;
                for (int i = 0; i < productsList.Count; i++)
                {
                    if (productsList[i].NAME == e.Element("NAME").Value)
                    {
                        index = i;
                    }
                }

                Console.WriteLine($"Name:{productsList[index].NAME}");
                XElement value = e.Parent.Element("VALUES");
                Console.WriteLine($"Charging Time:{value.Element("CHARGINGTIME").Value}");
                value.SetElementValue("CHARGINGTIME", productsList[index].CHARGINGTIME);
                Console.WriteLine("New Product Information");
                Console.WriteLine($"Name:{productsList[index].NAME}");
                Console.WriteLine($"Charging Time:{value.Element("CHARGINGTIME").Value}");
            }

            xmlProducts.Save("dif.xml");

        }
    }
}


