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
