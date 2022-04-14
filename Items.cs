using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Combined_XML_Program
{
    class Items
    {
        public string Name { get; set; }
        public bool Decay { get; set; }
        public bool PreventWarp { get; set; }
        public Items(string name)
        {
            Name = name;
        }
        public Items(string name, bool decay, bool preventWarp)
        {
            Name = name;
            Decay = decay;
            PreventWarp = preventWarp;
        }
    }
}
