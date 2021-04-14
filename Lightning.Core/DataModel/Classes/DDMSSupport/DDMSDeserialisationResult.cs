using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Lightning.Core
{
    /// <summary>
    /// DDMSDeserialisationResult (originally DDMSSerialisationResult)
    /// 
    /// March 21, 2021 (modified April 14, 2021: renamed) 
    /// 
    /// Defines a result class for DDMS deserialisation (loading
    /// </summary>
    public class DDMSDeserialisationResult : IResult
    {
        /// <summary>
        /// The DataModel that has been deserialised from XML.
        /// </summary>
        public DataModel DataModel { get; set; }

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
