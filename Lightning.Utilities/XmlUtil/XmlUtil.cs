using System;
using System.Collections.Generic; 
using System.Text;
using System.Xml.Linq; 

namespace Lightning.Utilities
{
    public static class XmlUtil
    {
        public static bool CheckForValidXmlElementContent(XElement XmlElement)
        {
            // Set a string variable to the XML value.

            string XVal = XmlElement.Value;

            // If it's null, return false...
            if (XVal == null)
            {
                return false;
            }
            else
            {
                // if it's zero length, return false
                if (XVal.Length == 0)
                {
                    return false;
                }
                else // return true...
                {
                    return true;
                }
            }
        }


    }
}
