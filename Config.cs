using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combined_XML_Program
{
    class Config
    {
        public string combined = XML_Serializer.basePath + "Spawn" + $@"\Combined.txt";
        public string shipConfigs = XML_Serializer.basePath + "Spawn" + $@"\Ship Configs.txt";
        public string hullConfigs = XML_Serializer.basePath + "Spawn" + $@"\Hull Configs.txt";
        public string droneConfigs = XML_Serializer.basePath + "Spawn" + $@"\Drone Configs.txt";
        public string baseGearConfigs = XML_Serializer.basePath + "Spawn" + $@"\BaseGear Configs.txt";

        public bool shouldDeleteOldConfigs = true;

        public bool hasClearedGal = false;
        public bool hasClearedShip = false;
        public bool hasClearedDrone = false;
        public bool hasClearedBaseGear = false;
    }
}