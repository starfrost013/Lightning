using NuCore.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// FindFontResult
    /// 
    /// July 2, 2021
    /// 
    /// Result class for finding Fonts.
    /// </summary>
    public class FindFontResult : IResult
    {
        public Font Font { get; set; }
        public bool Successful { get; set; }
        public string FailureReason { get; set; }
    }
}
