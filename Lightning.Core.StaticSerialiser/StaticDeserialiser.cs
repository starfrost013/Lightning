using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Lightning.Core.StaticSerialiser
{
    public partial class StaticSerialiser
    {
        /// <summary>
        /// Deserialises <paramref name="fileName"/> to the static class <paramref name="staticClass"/>. 
        /// 
        /// Needs to be rewritten.
        /// </summary>
        /// <param name="staticClass"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static StaticSerialisationResult Deserialize(Type staticClass, string fileName)
        {
            StaticSerialisationResult SSR = new StaticSerialisationResult();

            XmlReader xmlReader = null;

            try
            {
                xmlReader = new XmlTextReader(fileName);

                Deserialize(staticClass, xmlReader);

                SSR.Successful = true;
                return SSR;
            }
            catch (Exception ex)
            {
                SSR.Successful = false;
                SSR.Message = ex.Message;
                return SSR;
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
            }
        }

        public static object Deserialize(string name, Type type, XmlReader xmlReader)
        {
            XmlAttributeOverrides xmlAttributeOverrides = new XmlAttributeOverrides();
            XmlAttributes xmlAttributes = new XmlAttributes();
            xmlAttributes.XmlRoot = new XmlRootAttribute(name);
            xmlAttributeOverrides.Add(type, xmlAttributes);
            XmlSerializer xmlSerializer = new XmlSerializer(type, xmlAttributeOverrides);

            return xmlSerializer.Deserialize(xmlReader);
        }

        public static bool Deserialize(Type staticClass, XmlReader xmlReader)
        {
            FieldInfo[] fieldArray = staticClass.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            string currentElement = null;

            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    // Ever so slightly increases speed as it does not have to iterate through useless nodes.
                    case XmlNodeType.EndElement:
                    case XmlNodeType.Whitespace:
                    case XmlNodeType.SignificantWhitespace:
                        continue;
                    case XmlNodeType.Element:
                        currentElement = xmlReader.Name;

                        foreach (FieldInfo fieldInfo in fieldArray)
                        {
                            string fieldName = fieldInfo.Name;
                            Type fieldType = fieldInfo.FieldType;
                            object fieldObject = fieldInfo.GetValue(fieldType);

                            if (fieldInfo.IsNotSerialized)
                                continue;

                            string FinalFieldName = null;

                            string[] FieldInfo_ExtractNamePre = fieldInfo.Name.Split('>');

                            if (FieldInfo_ExtractNamePre.Length < 2)
                            {
                                return false;
                            }
                            else
                            {
                                string[] Pre0 = FieldInfo_ExtractNamePre[0].Split('<');

                                if (FieldInfo_ExtractNamePre.Length < 2) return false;

                                FinalFieldName = Pre0[1];
                            }

                            if (FinalFieldName == currentElement)
                            {
                                if (typeof(IDictionary).IsAssignableFrom(fieldType) || typeof(IList).IsAssignableFrom(fieldType))
                                {
                                    fieldObject = Deserialize(fieldName, fieldType, xmlReader);

                                    fieldInfo.SetValueDirect(__makeref(fieldObject), fieldObject);
                                }
                                // todo: tmrw: load some of this stuff
                                else if (xmlReader.NodeType == XmlNodeType.Text)
                                {
                                    TypeConverter typeConverter = TypeDescriptor.GetConverter(fieldType);
                                    object value = typeConverter.ConvertFromString(xmlReader.Value);

                                    fieldInfo.SetValue(fieldObject, value);
                                }

                            }
                        }

                        continue; 
                }
            }
            return true;
        }
    }
}
