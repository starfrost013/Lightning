using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// GetScriptMethodResult
    /// 
    /// April 24, 2021
    /// 
    /// Result class for acquiring script methods.
    /// </summary>
    public class GetScriptMethodResult : IResult
    {
        public string FailureReason { get; set; }
        public ScriptMethod Method { get; set; }
        public bool Successful { get; set; }
    }
}
