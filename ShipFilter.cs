using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combined_XML_Program
{
   public class ShipFilter
    {
        public string ShipHull { get; set; }
        public string ShipConfig { get; set; }

        public ShipFilter(string shipConfig, string shipHull)
        {
            ShipConfig = shipConfig;
            ShipHull = shipHull;
        }
    }
}
