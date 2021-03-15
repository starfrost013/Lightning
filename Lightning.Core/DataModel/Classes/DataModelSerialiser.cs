using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Lightning.Core
{
    public class DataModelSerialiser : Instance
    {
        /// <summary>
        /// Change?
        /// 
        /// The XML schema version. 
        /// </summary>
        public static string XMLSCHEMA_VERSION = "0.2.0.0001";
        /// <summary>
        /// DDMS (Dynamic DataModel Serialiser)
        /// 
        /// Transform an XML document to a DataModel.
        /// 
        /// 2021-03-11
        /// </summary>
        /// <param name="Schema">The schema to utilise for serialisation.</param>
        /// <param name="Path">The path to the XML document to serialise. </param>
        /// <returns></returns>
        public DataModel DDMS_Serialise(LightningXMLSchema Schema, string Path)
        {
            Logging.Log($"DDMS: Reading {Path} and transforming to DataModel...");

            string ScPath = Schema.Path;

            XmlReaderSettings XRS = new XmlReaderSettings(); 

            XmlReader XR = XmlReader.Create(Path, XRS);

            while (XR.Read())
            {
                switch (XR.NodeType)
                {
                    case XmlNodeType.Whitespace:
                    case XmlNodeType.SignificantWhitespace:
                    case XmlNodeType.Comment:
                        // continue and skip useless nodes
                        continue;
                    case XmlNodeType.Element:

                        switch (XR.Name)
                        {
                            case "InstanceTree":
                            case "Settings":
                            case "Metadata":
                            default:
                                continue;
                            
                        }
                        
                }

            }

            throw new NotImplementedException();
            
        }

        private DDMSValidateResult DDMS_Validate(LightningXMLSchema Schema)
        {
            throw new NotImplementedException();
        }
    }
}
