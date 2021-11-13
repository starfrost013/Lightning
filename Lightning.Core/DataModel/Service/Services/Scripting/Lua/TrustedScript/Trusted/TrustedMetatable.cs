using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// TrustedMetatable
    /// 
    /// October 16, 2021
    /// 
    /// Implements trusted metatables - protects the string metatable.
    /// </summary>
    public class TrustedMetatable : TrustedScript
    {
        internal override string ClassName => "TrustedLoad";

        internal override string ProtectedContent =>
        "getmetatable(\"\").__metatable = \"Sandbox Enforcement: Cannot access the string metatable!\"";
    }
}
