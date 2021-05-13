using Lightning.Core.API;
using System;
using System.Xml;

namespace Polaris.Core
{
    /// <summary>
    /// PolarisState
    /// 
    /// May 13, 2021
    /// 
    /// Defines the global state for Polaris.
    /// </summary>
    public class PolarisState
    {
        /// <summary>
        /// The Lightning DataModel.
        /// </summary>
        public DataModel DataModel { get; set; }

        /// <summary>
        /// The XML document that will be used.
        /// </summary>
        public XmlDocument XmlDocument { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        public string FileName { get; set; }
        public PolarisState()
        {
            XmlDocument = new XmlDocument();

        }
    }
}
