using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

#pragma warning disable
namespace XML_Serializer.XML
{
    public class SS_Serializer_PillboxList
    {
        [Serializable]
        public class PILLBOXLIST
        {
            private static XmlSerializer _serializerXml;

            [XmlElement("PILLBOX")]
            public List<PILLBOX> PILLBOX { get; set; }

            public PILLBOXLIST()
            {
                PILLBOX = new List<PILLBOX>();
            }

            private static XmlSerializer SerializerXml
            {
                get
                {
                    if ((_serializerXml == null))
                    {
                        _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(PILLBOXLIST));
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
            public static bool Deserialize(string input, out PILLBOXLIST obj, out Exception exception)
            {
                exception = null;
                obj = default(PILLBOXLIST);
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

            public static bool Deserialize(string input, out PILLBOXLIST obj)
            {
                Exception exception = null;
                return Deserialize(input, out obj, out exception);
            }

            public static PILLBOXLIST Deserialize(string input)
            {
                StringReader stringReader = null;
                try
                {
                    stringReader = new StringReader(input);
                    return ((PILLBOXLIST)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
                }
                finally
                {
                    if ((stringReader != null))
                    {
                        stringReader.Dispose();
                    }
                }
            }

            public static PILLBOXLIST Deserialize(Stream s)
            {
                return ((PILLBOXLIST)(SerializerXml.Deserialize(s)));
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
            public static bool LoadFromFile(string fileName, out PILLBOXLIST obj, out Exception exception)
            {
                exception = null;
                obj = default(PILLBOXLIST);
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

            public static bool LoadFromFile(string fileName, out PILLBOXLIST obj)
            {
                Exception exception = null;
                return LoadFromFile(fileName, out obj, out exception);
            }

            public static PILLBOXLIST LoadFromFile(string fileName)
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
        public class PILLBOX
        {
            [XmlElement("INIT")] public List<INIT> INIT { get; set; }

            [XmlElement("VALUES")] public List<VALUES> VALUES { get; set; }

            [XmlAttribute]
            public string @class { get; set; }

            public PILLBOX()
            {
                INIT = new();
                VALUES = new();
            }
        }

        [Serializable]
        public class INIT
        {
            [XmlElement] public string NAME { get; set; }
            [XmlElement] public bool TSLENABLED { get; set; }
        }
        [Serializable]
        public class VALUES
        {
            [XmlElement] public string TECHLEVEL { get; set; }
            [XmlElement] public bool PREVENTWARP { get; set; }
        }
    }
}