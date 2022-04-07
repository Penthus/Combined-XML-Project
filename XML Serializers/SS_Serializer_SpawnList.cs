using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

#pragma warning disable
namespace XML_Serializer.XML
{
    public class SS_Serializer_SpawnList
    {
        [Serializable]
        public class GALAXY
        {
            private static XmlSerializer _serializerXml;

            [XmlElement("SPAWNLIST")]
            public List<SPAWNLIST> SPAWNLIST { get; set; }

            public GALAXY()
            {
                SPAWNLIST = new List<SPAWNLIST>();
            }

            private static XmlSerializer SerializerXml
            {
                get
                {
                    if ((_serializerXml == null))
                    {
                        _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(GALAXY));
                    }

                    return _serializerXml;
                }
            }
            #region Serialize/Deserialize

            /// <summary>
            /// Serialize GALAXY object
            /// </summary>
            /// <returns>XML value</returns>
            public virtual string Serialize()
            {
                StreamReader streamReader = null;
                MemoryStream memoryStream = null;
                try
                {
                    memoryStream = new MemoryStream();
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
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
            /// Deserializes GALAXY object
            /// </summary>
            /// <param name="input">string to deserialize</param>
            /// <param name="obj">Output GALAXY object</param>
            /// <param name="exception">output Exception value if deserialize failed</param>
            /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
            public static bool Deserialize(string input, out GALAXY obj, out Exception exception)
            {
                exception = null;
                obj = default(GALAXY);
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

            public static bool Deserialize(string input, out GALAXY obj)
            {
                Exception exception = null;
                return Deserialize(input, out obj, out exception);
            }

            public static GALAXY Deserialize(string input)
            {
                StringReader stringReader = null;
                try
                {
                    stringReader = new StringReader(input);
                    return ((GALAXY)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
                }
                finally
                {
                    if ((stringReader != null))
                    {
                        stringReader.Dispose();
                    }
                }
            }

            public static GALAXY Deserialize(Stream s)
            {
                return ((GALAXY)(SerializerXml.Deserialize(s)));
            }

            #endregion

            #region Save/Load
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
            /// Deserializes xml markup from file into an GALAXY object
            /// </summary>
            /// <param name="fileName">File to load and deserialize</param>
            /// <param name="obj">Output GALAXY object</param>
            /// <param name="exception">output Exception value if deserialize failed</param>
            /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
            public static bool LoadFromFile(string fileName, out GALAXY obj, out Exception exception)
            {
                exception = null;
                obj = default(GALAXY);
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

            public static bool LoadFromFile(string fileName, out GALAXY obj)
            {
                Exception exception = null;
                return LoadFromFile(fileName, out obj, out exception);
            }

            public static GALAXY LoadFromFile(string fileName)
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
        #endregion


        [Serializable]
        public class SPAWNLIST
        {
            // private static XmlSerializer _serializerXml;

            [XmlElement("SPAWNOBJ")]
            public List<SPAWNOBJ> SPAWNOBJ { get; set; }

            public SPAWNLIST()
            {
                SPAWNOBJ = new List<SPAWNOBJ>();
            }
        }

        [Serializable]
        public class SPAWNOBJ
        {

            [XmlElement("SPAWNAMBUSH")]
            public List<SPAWNAMBUSH> SPAWNAMBUSH { get; set; }

            [XmlElement("SPAWNNASTY")]
            public List<SPAWNNASTY> SPAWNNASTY { get; set; }

            [XmlElement("SPAWNBOSS")]
            public List<SPAWNBOSS> SPAWNBOSS { get; set; }

            [XmlElement("SPAWNER")]
            public List<SPAWNER> SPAWNER { get; set; }

            [XmlAttribute] public string typename { get; set; }

            public SPAWNOBJ()
            {
                SPAWNAMBUSH = new List<SPAWNAMBUSH>();
                SPAWNNASTY = new List<SPAWNNASTY>();
                SPAWNBOSS = new List<SPAWNBOSS>();
                SPAWNER = new List<SPAWNER>();
            }
        }


        [Serializable]
        public class SPAWNAMBUSH
        {

            [XmlElement("SPAWNER")]
            public List<SPAWNER> SPAWNER { get; set; }
            [XmlElement("AMBUSHERS_CONFIGURATION")]
            public List<AMBUSHERS_CONFIGURATION> AMBUSHERS_CONFIGURATION { get; set; }

            public SPAWNAMBUSH()
            {
                SPAWNER = new List<SPAWNER>();
                AMBUSHERS_CONFIGURATION = new List<AMBUSHERS_CONFIGURATION>();
            }
        }


        [Serializable]
        public class SPAWNNASTY
        {

            [XmlElement("SPAWNER")]
            public List<SPAWNER> SPAWNER { get; set; }

            public SPAWNNASTY()
            {
                SPAWNER = new List<SPAWNER>();
            }
        }

        [Serializable]
        public class SPAWNBOSS
        {

            [XmlElement("SPAWNER")]
            public List<SPAWNER> SPAWNER { get; set; }

            public SPAWNBOSS()
            {
                SPAWNER = new List<SPAWNER>();
            }
        }


        public class SPAWNER
        {
            [XmlElement] public string NAMEDMOB { get; set; }
            [XmlElement] public string LOCKOUTNAME { get; set; }
            [XmlElement] public LOCKOUTFIXED LOCKOUTFIXED { get; set; }
            [XmlElement] public string SHIPCONFIG { get; set; }
            [XmlElement] public SPAWNBASE SPAWNBASE { get; set; }

            //[XmlElement] public string RARITY { get; set; }
            //[XmlElement] public int COST { get; set; }
            //[XmlElement] public int WEIGHT { get; set; }
            //[XmlElement] public int SPACE { get; set; }
            //[XmlElement] public string TYPE { get; set; }
        }

        [Serializable]
        public class LOCKOUTFIXED
        {
            [XmlAttribute] public string period { get; set; }
        }

        [Serializable]
        public class SPAWNBASE
        {
            [XmlElement] public string LEVEL { get; set; }
        }

        [Serializable]
        public class AMBUSHERS_CONFIGURATION
        {
            [XmlElement("AMBUSHER")]
            public List<AMBUSHER> AMBUSHER { get; set; }

            public AMBUSHERS_CONFIGURATION()
            {
                AMBUSHER = new List<AMBUSHER>();
            }
        }

        [Serializable]
        public class AMBUSHER
        {
            [XmlElement] public string HULL_CONFIGURATION { get; set; }
            [XmlElement] public string AI_TYPE { get; set; }
            [XmlElement] public string NUMBER { get; set; }
        }
    }
}