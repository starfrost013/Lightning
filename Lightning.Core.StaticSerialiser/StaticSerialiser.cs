using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection; 
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization; 

namespace Lightning.Core.StaticSerialiser
{
    /// <summary>
    /// 2021-01-21
    /// 
    /// Some code I shamelessly stole from stackoverflow and refactored a bit.
    /// 
    /// 2021-04-02 
    /// 
    /// Moved from TrackMaker Iris to Lightning.
    /// </summary>
    public static partial class StaticSerialiser
    {
        public static StaticSerialisationResult Serialize(Type staticClass, string fileName)
        {
            XmlTextWriter xmlWriter = null;
            StaticSerialisationResult SSR = new StaticSerialisationResult();

            try
            {
                xmlWriter = new XmlTextWriter(fileName, null);

                xmlWriter.Formatting = Formatting.Indented;

                xmlWriter.WriteStartDocument();

                Serialize(staticClass, xmlWriter);

                xmlWriter.WriteEndDocument();

                SSR.Successful = true;

                return SSR;
            }
            catch (Exception ex)
            {
                SSR.Message = ex.Message;
                SSR.Successful = false;
                return SSR;
            }
            finally
            {
                if (xmlWriter != null)
                {
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
        }

        public static void Serialize(string name, object obj, XmlTextWriter xmlWriter)
        {
            Type type = obj.GetType();
            XmlAttributeOverrides xmlAttributeOverrides = new XmlAttributeOverrides();
            XmlAttributes xmlAttributes = new XmlAttributes();
            xmlAttributes.XmlRoot = new XmlRootAttribute(name);
            xmlAttributeOverrides.Add(type, xmlAttributes);
            XmlSerializer xmlSerializer = new XmlSerializer(type, xmlAttributeOverrides);

            xmlSerializer.Serialize(xmlWriter, obj);
        }

        public static bool Serialize(Type staticClass, XmlTextWriter xmlWriter)
        {
            FieldInfo[] fieldArray = staticClass.GetFields(BindingFlags.Static | BindingFlags.Public);

            xmlWriter.WriteStartElement(staticClass.Name);

            foreach (FieldInfo fieldInfo in fieldArray)
            {
                if (fieldInfo.IsNotSerialized)
                    continue;

                string fieldName = fieldInfo.Name;
                string fieldValue = null;
                Type fieldType = fieldInfo.FieldType;
                object fieldObject = fieldInfo.GetValue(fieldType);

                if (fieldObject != null)
                {
                    if (fieldType.GetInterface("IDictionary") != null || fieldType.GetInterface("IList") != null)
                    {
                        Serialize(fieldName, fieldObject, xmlWriter);
                    }
                    else
                    {
                        TypeConverter typeConverter = TypeDescriptor.GetConverter(fieldInfo.FieldType);
                        fieldValue = typeConverter.ConvertToString(fieldObject);

                        xmlWriter.WriteStartElement(fieldName);
                        xmlWriter.WriteString(fieldValue);
                        xmlWriter.WriteEndElement();
                    }
                }
            }

            xmlWriter.WriteEndElement();

            return true;
        }

        
    }
}
