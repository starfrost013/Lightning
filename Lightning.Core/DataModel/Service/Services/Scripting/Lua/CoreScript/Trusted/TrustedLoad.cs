using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// TrustedLoad
    /// 
    /// October 17, 2021
    /// 
    /// Defines a trusted script that dummies out load after it has been used.
    /// </summary>
    public class TrustedLoad : TrustedScript
    {
        internal override string ClassName => "TrustedLoad";

        internal override string ProtectedContent => "load = nil;";
    }
}
