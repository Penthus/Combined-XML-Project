using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combined_XML_Program
{
    class Config
    {
        public string combined = Program.basePath + "Spawn" + $@"\Combined.txt";
        public string shipConfigs = Program.basePath + "Spawn" + $@"\Ship Configs.txt";
        public string hullConfigs = Program.basePath + "Spawn" + $@"\Hull Configs.txt";
        public string droneConfigs = Program.basePath + "Spawn" + $@"\Drone Configs.txt";
        public string baseGearConfigs = Program.basePath + "Spawn" + $@"\BaseGear Configs.txt";


        public bool shouldDeleteOldConfigs = true;

        public bool hasClearedGal = false;
        public bool hasClearedShip = false;
        public bool hasClearedDrone = false;
        public bool hasClearedBaseGear = false;

        public List<string> GetConfigs()
        {
            List<string> configs = new List<string>();

            configs.Add(combined);
            configs.Add(shipConfigs);
            configs.Add(hullConfigs);
            configs.Add(droneConfigs);
            configs.Add(baseGearConfigs);

            return configs;
        }
    }
}