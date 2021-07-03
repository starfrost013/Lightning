using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Tools.ErrorConvert
{
    /// <summary>
    /// GetLaunchArgument
    /// 
    /// July 2, 2021
    /// 
    /// VERY QUICK AND DIRTY acquire launch args reuslt class for old to new error conversion tool
    /// </summary>
    public class GetLaunchArgumentResult : IResult
    {
        public LaunchArgs Arguments { get; set; }
        public string FailureReason { get; set; }
        public bool Successful { get; set; }
        public GetLaunchArgumentResult()
        {
            Arguments = new LaunchArgs(); 
        }
    }
}
