using System;
using System.Collections.Generic; 
using System.Text;
using System.Xml.Linq; 

namespace NuCore.Utilities
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

        /// <summary>
        /// Checks if a type is valid for instantiation. Type must be in the System (base only) namespace or the Lightning.* namespace.
        /// </summary>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public static bool CheckIfValidTypeForInstantiation(string TypeName)
        {
            string[] TypeNameNamespaceDots = TypeName.Split('.');

            // If it is in the System namespace, but not any child namespace...
            if (TypeName.Contains("System") && TypeNameNamespaceDots.Length == 2)
            {
                return true;
            }
            else if (TypeName.Contains("Lightning")
                || TypeName.Contains("Polaris")
                || TypeName.Contains("NuCore") // "Nu" might be too generic and allow other namespaces (2021/12/11) 
                || TypeName.Contains("NuRender")) // If it's in the Lightning, Polaris, NuCore or NuRender namespaces...
            {
                return true; // allow it
            }
            else
            {
                return false;
            }
        }
    }
}
