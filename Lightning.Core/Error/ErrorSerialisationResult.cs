using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Result class for a collection of errors serialised from Global Error XML.
    /// </summary>
    public class ErrorSerialisationResult : IResult
    {
        /// <summary>
        /// The ErrorCollection that has been 
        /// </summary>
        public ErrorCollection ErrorCollection { get; set; }
        
        /// <summary>
        /// A reason for failure, if it exists.
        /// </summary>
        public string FailureReason { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Successful { get; set; }
        
    }
}
