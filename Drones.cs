using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combined_XML_Program
{
    public class Drones
    {
        public string NAME { get; set; }
        public int JUICE { get; set; }
        public string[] DEPLOYMENTLIST { get; set; }

        public Drones(string name, int juice, string[] deploymentlist)
        {
            NAME = name;
            JUICE = juice;
            DEPLOYMENTLIST = deploymentlist;
        }

        public Drones(string name, int juice)
        {
            NAME = name;
            JUICE = juice;
        }
    }
}
