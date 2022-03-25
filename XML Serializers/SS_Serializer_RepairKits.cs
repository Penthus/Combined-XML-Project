using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XML_Serializer.XML
{
    public class SS_Serializer_RepairKits
    {
        [Serializable]
        public class MISCLIST
        {
            private static XmlSerializer _serializerXml;

            [XmlElement("MISC")]
            public List<misclistMISC> MISC { get; set; }

            public MISCLIST()
            {
                MISC = new List<misclistMISC>();
            }

            private static XmlSerializer SerializerXml
            {
                get
                {
                    if ((_serializerXml == null))
                    {
                        _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(MISCLIST));
                    }
                    return _serializerXml;
                }
            }

            #region Serialize/Deserialize
            /// <summary>
            /// Serialize MISCLIST object
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
            /// Deserializes MISCLIST object
            /// </summary>
            /// <param name="input">string to deserialize</param>
            /// <param name="obj">Output MISCLIST object</param>
            /// <param name="exception">output Exception value if deserialize failed</param>
            /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
            public static bool Deserialize(string input, out MISCLIST obj, out Exception exception)
            {
                exception = null;
                obj = default(MISCLIST);
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

            public static bool Deserialize(string input, out MISCLIST obj)
            {
                Exception exception = null;
                return Deserialize(input, out obj, out exception);
            }

            public static MISCLIST Deserialize(string input)
            {
                StringReader stringReader = null;
                try
                {
                    stringReader = new StringReader(input);
                    return ((MISCLIST)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
                }
                finally
                {
                    if ((stringReader != null))
                    {
                        stringReader.Dispose();
                    }
                }
            }

            public static MISCLIST Deserialize(Stream s)
            {
                return ((MISCLIST)(SerializerXml.Deserialize(s)));
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
            /// Deserializes xml markup from file into an MISCLIST object
            /// </summary>
            /// <param name="fileName">File to load and deserialize</param>
            /// <param name="obj">Output MISCLIST object</param>
            /// <param name="exception">output Exception value if deserialize failed</param>
            /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
            public static bool LoadFromFile(string fileName, out MISCLIST obj, out Exception exception)
            {
                exception = null;
                obj = default(MISCLIST);
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

            public static bool LoadFromFile(string fileName, out MISCLIST obj)
            {
                Exception exception = null;
                return LoadFromFile(fileName, out obj, out exception);
            }

            public static MISCLIST LoadFromFile(string fileName)
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
    }

    [Serializable]
    public class misclistMISC
    {
        [XmlElement("INIT")] public List<misclistMISCINIT> INIT { get; set; }

        [XmlElement("VALUES")] public List<misclistMISCVALUES> VALUES { get; set; }

        [XmlAttribute]
        public string @class { get; set; }

        public misclistMISC()
        {
            INIT = new List<misclistMISCINIT>();
            VALUES = new List<misclistMISCVALUES>();
        }
    }

    [Serializable]
    public class misclistMISCINIT
    {

        [XmlElement] public string NAME { get; set; }
        [XmlElement] public string DESCRIPTION { get; set; }
        [XmlElement] public string RARITY { get; set; }
        [XmlElement] public int COST { get; set; }
        [XmlElement] public int WEIGHT { get; set; }
        [XmlElement] public int SPACE { get; set; }
        [XmlElement] public string TYPE { get; set; }


    }
    [Serializable]
    public class misclistMISCVALUES
    {

        [XmlElement] public int TECHLEVEL { get; set; }
        [XmlElement] public int JUICE { get; set; }
    }

}

