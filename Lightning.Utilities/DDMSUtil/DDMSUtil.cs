using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Lightning.Utilities
{
    /// <summary>
    /// Dynamic DataModel Serialiser (DDMS) Utilities
    /// 
    /// 2021-03-14
    /// 
    /// Utilities that support the Dynamic Datamodel Serialiser
    /// </summary>
    public static class DDMSUtil
    {
        public XmlNode XmlReaderToNode(XmlReader XR)
        {

            // HACKHACK
            // Create a dummy document
            XmlDocument XD = new XmlDocument();
            XmlNode XNA = XD.CreateElement(XR.Name);
            
            // DO More of this

            return XNA;

        }
    }
}
