﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combined_XML_Program
{
    public class SuperItems
    {
        public string NAME { get; set; }
        public int CHARGINGTIME { get; set; }

        public SuperItems(string name, int chargingtime)
        {
            NAME = name;
            CHARGINGTIME = chargingtime;
        }
    }
}