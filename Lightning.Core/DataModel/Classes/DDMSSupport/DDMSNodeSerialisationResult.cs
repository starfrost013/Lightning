using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// DDMSNodeSerialisationResult
    /// 
    /// April 14, 2021
    /// 
    /// Defines a result class for DDMS node serialisation. 
    /// </summary>
    public class DDMSNodeSerialisationResult : IResult
    {
        /// <summary>
        /// The XML node that has been created as a result of the serialisation operation.
        /// </summary>
        public XmlNode XNode { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FailureReason { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Successful { get; set; }
    }
}
