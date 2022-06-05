using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combined_XML_Program
{
    class Backup_Legacy_Serialization_code
    {
        /*            #region Serialize/Deserialize
            /// <summary>
            /// Serialize BASELIST object
            /// </summary>
            /// <returns>XML value</returns>
            public virtual string Serialize()
            {
                StreamReader streamReader = null;
                MemoryStream memoryStream = null;
                try
                {
                    memoryStream = new();
                    XmlWriterSettings xmlWriterSettings = new();
                    XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                    SerializerXml.Serialize(xmlWriter, this);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    streamReader = new(memoryStream);
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
            /// Deserializes BASELIST object
            /// </summary>
            /// <param name="input">string to deserialize</param>
            /// <param name="obj">Output BASELIST object</param>
            /// <param name="exception">output Exception value if deserialize failed</param>
            /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
            public static bool Deserialize(string input, out BASELIST obj, out Exception exception)
            {
                exception = null;
                obj = default;
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

            public static bool Deserialize(string input, out BASELIST obj)
            {
                return Deserialize(input, out obj, out _);
            }

            public static BASELIST Deserialize(string input)
            {
                StringReader stringReader = null;
                try
                {
                    stringReader = new(input);
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

            public static BASELIST Deserialize(Stream s)
            {
                return ((BASELIST)(SerializerXml.Deserialize(s)));
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
                    FileInfo outputFile = new(fileName);
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
            /// Deserializes xml markup from file into an BASELIST object
            /// </summary>
            /// <param name="fileName">File to load and deserialize</param>
            /// <param name="obj">Output BASELIST object</param>
            /// <param name="exception">output Exception value if deserialize failed</param>
            /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
            public static bool LoadFromFile(string fileName, out BASELIST obj, out Exception exception)
            {
                exception = null;
                obj = default;
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

            public static bool LoadFromFile(string fileName, out BASELIST obj)
            {
                return LoadFromFile(fileName, out obj, out _);
            }

            public static BASELIST LoadFromFile(string fileName)
            {
                FileStream file = null;
                StreamReader sr = null;
                try
                {
                    file = new(fileName, FileMode.Open, FileAccess.Read);
                    sr = new(file);
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
            }*/
    }
}
