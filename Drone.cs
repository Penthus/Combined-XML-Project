namespace Combined_XML_Program
{
    public class Drone
    {
        public string NAME { get; set; }
        public int JUICE { get; set; }
        public string[] DEPLOYMENTLIST { get; set; }

        public Drone(string name, int juice, string[] deploymentlist)
        {
            NAME = name;
            JUICE = juice;
            DEPLOYMENTLIST = deploymentlist;
        }

        public Drone(string name, int juice)
        {
            NAME = name;
            JUICE = juice;
        }
    }
}
