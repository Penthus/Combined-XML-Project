using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using XML_Serializer.XML;

namespace Combined_XML_Program.XML_Serializers
{
    public class SS_Serializer_Items
    {
        [Serializable]
        public class HULLLIST
        {
            private static XmlSerializer _serializerXml;

            [XmlElement("HULL")] public List<HULL> HULL { get; set; }

            public HULLLIST()
            {
                HULL = new List<HULL>();
            }
        }
        [Serializable]
        public class HULL
        {
            [XmlElement("INIT")] public List<INIT> INIT { get; set; }

            [XmlElement("VALUES")] public List<VALUES> VALUES { get; set; }

            [XmlAttribute]
            public string @class { get; set; }

            public HULL()
            {
                INIT = new();
                VALUES = new();
            }
        }

        [Serializable]
        public class BASELIST
        {
            private static XmlSerializer _serializerXml;

            [XmlElement("BASE")] public List<BASE> BASE { get; set; }

            public BASELIST()
            {
                BASE = new();
            }

            private static XmlSerializer SerializerXml
            {
                get
                {
                    if ((_serializerXml == null))
                    {
                        _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(BASELIST));
                    }

                    return _serializerXml;
                }
            }

            public static BASELIST LoadFromFile(string fileName)
            {
                FileStream file = null;
                StreamReader sr = null;
                try
                {
                    file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(file);
                    string dataString = sr.ReadToEnd();
                    sr.Close();
                    file.Close();
                    return Deserialize(dataString);
                }
                finally
                {
                    if ((file != null))
                    {
                        file.Dispose();
                    }

                    if ((sr != null))
                    {
                        sr.Dispose();
                    }
                }
            }

            public static BASELIST Deserialize(string input)
            {
                StringReader stringReader = null;
                try
                {
                    stringReader = new StringReader(input);
                    return ((BASELIST)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
                }
                finally
                {
                    if ((stringReader != null))
                    {
                        stringReader.Dispose();
                    }
                }
            }
        }

        [Serializable]
        public class BASE
        {
            [XmlElement("INIT")] public List<INIT> INIT { get; set; }

            [XmlElement("VALUES")] public List<VALUES> VALUES { get; set; }

            [XmlAttribute]
            public string @class { get; set; }

            public BASE()
            {
                INIT = new();
                VALUES = new();
            }
        }

        [Serializable]
        public partial class INIT
        {
            [XmlElement] public string NAME { get; set; }
            [XmlElement] public string DESCRIPTION { get; set; }
            [XmlElement] public string RARITY { get; set; }
            [XmlElement] public double COST { get; set; }
            [XmlElement] public int WEIGHT { get; set; }
            [XmlElement] public int SPACE { get; set; }
            [XmlElement] public string TYPE { get; set; }
        }

        [Serializable]
        public partial class VALUES
        {
            [XmlElement("PRODUCTS")]
            public List<PRODUCTS> PRODUCTS { get; set; }

            [XmlElement] public int TECHLEVEL { get; set; }
            [XmlElement] public int JUICE { get; set; }
            [XmlElement] public string BLUEPRINTITEM { get; set; }
        }

        [Serializable]
        public class PRODUCTS
        {
            [XmlElement]
            public List<ITEM> ITEM { get; set; }
        }

        public class ITEM
        {
            [XmlAttribute] public string name;
        }
    }
}
