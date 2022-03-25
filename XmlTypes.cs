using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combined_XML_Program
{
    class XmlTypes
    {
        public string Path { get; set; }
        public string Root { get; set; }
        public string Xml { get; set; }

        public XmlTypes(string path, string root, string xml)
        {
            Path = path;
            Root = root;
            Xml = xml;
        }

    }
}
