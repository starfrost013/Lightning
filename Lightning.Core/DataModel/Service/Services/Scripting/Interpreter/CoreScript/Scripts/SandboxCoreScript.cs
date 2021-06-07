using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ImportOverrideCoreScript
    /// 
    /// June 6, 2021
    /// 
    /// Defines a corescript that overrides the Lua import function to prevent importation of non-trusted assemblies.
    /// </summary>
    public class SandboxCoreScript : CoreScript 
    {
        internal override string ClassName => "ImportOverrideCoreScript";

        /// <summary>
        /// Protected corescript content. 
        /// 
        /// Restricts global environment to safe objects.
        /// </summary>
        internal override string ProtectedContent => 
            "Print = print;\n" +
            "_G = {print};";
    }
}
