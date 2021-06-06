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
    public class ImportOverrideCoreScript : CoreScript 
    {
        internal override string ClassName => "ImportOverrideCoreScript";

        internal override string ProtectedContent => 
            "function import()\n" +
            "end";
    }
}
