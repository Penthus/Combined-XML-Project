namespace XML_Serializer.XML
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public class SS_Serializer_Inventory
    {

        [Serializable]
        public class Inventory
        {
            private static XmlSerializer _serializerXml;

            [XmlElement("SHIP")]
            public List<inventorySHIP> SHIP { get; set; }

            public Inventory()
            {
                SHIP = new List<inventorySHIP>();
            }

            private static XmlSerializer SerializerXml
            {
                get
                {
                    if ((_serializerXml == null))
                    {
                        _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(Inventory));
                    }

                    return _serializerXml;
                }
            }

            #region Serialize/Deserialize

            /// <summary>
            /// Serialize inventory object
            /// </summary>
            /// <returns>XML value</returns>
            public virtual string Serialize()
            {
                StreamReader streamReader = null;
                MemoryStream memoryStream = null;
                try
                {
                    memoryStream = new MemoryStream();
                    System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                    System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                    SerializerXml.Serialize(xmlWriter, this);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    streamReader = new StreamReader(memoryStream);
                    return streamReader.ReadToEnd();
                }
                finally
                {
                    if ((streamReader != null))
                    {
                        streamReader.Dispose();
                    }

                    if ((memoryStream != null))
                    {
                        memoryStream.Dispose();
                    }
                }
            }

            /// <summary>
            /// Deserializes inventory object
            /// </summary>
            /// <param name="input">string to deserialize</param>
            /// <param name="obj">Output inventory object</param>
            /// <param name="exception">output Exception value if deserialize failed</param>
            /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
            public static bool Deserialize(string input, out Inventory obj, out Exception exception)
            {
                exception = null;
                obj = default(Inventory);
                try
                {
                    obj = Deserialize(input);
                    return true;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    return false;
                }
            }

            public static bool Deserialize(string input, out Inventory obj)
            {
                Exception exception = null;
                return Deserialize(input, out obj, out exception);
            }

            public static Inventory Deserialize(string input)
            {
                StringReader stringReader = null;
                try
                {
                    stringReader = new StringReader(input);
                    return ((Inventory)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
                }
                finally
                {
                    if ((stringReader != null))
                    {
                        stringReader.Dispose();
                    }
                }
            }

            public static Inventory Deserialize(Stream s)
            {
                return ((Inventory)(SerializerXml.Deserialize(s)));
            }

            #endregion


            public bool SaveToFile(string fileName, out Exception exception)
            {
                exception = null;
                try
                {
                    SaveToFile(fileName);
                    return true;
                }
                catch (Exception e)
                {
                    exception = e;
                    return false;
                }
            }

            public void SaveToFile(string fileName)
            {
                StreamWriter streamWriter = null;
                try
                {
                    string dataString = Serialize();
                    FileInfo outputFile = new FileInfo(fileName);
                    streamWriter = outputFile.CreateText();
                    streamWriter.WriteLine(dataString);
                    streamWriter.Close();
                }
                finally
                {
                    if ((streamWriter != null))
                    {
                        streamWriter.Dispose();
                    }
                }
            }

            /// <summary>
            /// Deserializes xml markup from file into an inventory object
            /// </summary>
            /// <param name="fileName">File to load and deserialize</param>
            /// <param name="obj">Output inventory object</param>
            /// <param name="exception">output Exception value if deserialize failed</param>
            /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
            public static bool LoadFromFile(string fileName, out Inventory obj, out Exception exception)
            {
                exception = null;
                obj = default(Inventory);
                try
                {
                    obj = LoadFromFile(fileName);
                    return true;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    return false;
                }
            }

            public static bool LoadFromFile(string fileName, out Inventory obj)
            {
                Exception exception = null;
                return LoadFromFile(fileName, out obj, out exception);
            }

            public static Inventory LoadFromFile(string fileName)
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
        }

        [Serializable]
        public class inventorySHIP
        {

            [XmlArrayItemAttribute("ITEM", IsNullable = false)]
            public List<inventorySHIPITEM> ITEMLIST { get; set; }

            [XmlAttribute] public string name { get; set; }

            public inventorySHIP()
            {
                ITEMLIST = new List<inventorySHIPITEM>();
            }
        }


        [Serializable]
        public class inventorySHIPITEM
        {

            [XmlAttribute] public string nm { get; set; }
            [XmlAttribute] public bool eqp { get; set; }
            [XmlAttribute] public uint id { get; set; }
            [XmlAttribute] public uint m { get; set; }

        }
    }

}