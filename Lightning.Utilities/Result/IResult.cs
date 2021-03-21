using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Utilities
{
    /// <summary>
    /// Lightning Utilities 
    /// 
    /// Handles the Result class.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// A boolean that indicates if the operation was successful.
        /// </summary>
        bool Successful { get; set; }
    }
}
