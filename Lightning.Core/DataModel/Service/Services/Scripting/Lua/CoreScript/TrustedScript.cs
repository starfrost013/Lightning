using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// TrustedScript
    /// 
    /// October 16, 2021 (formerly June 6, 2021 as CoreScript)
    /// 
    /// Defines a trusted script that can be used to enforce sandboxing.
    /// </summary>
    public class TrustedScript : Script
    {
        internal override string ClassName => "TrustedScript";
        internal override InstanceTags Attributes => InstanceTags.Instantiable | InstanceTags.Destroyable;

        internal virtual string ProtectedContent => $"print('Attempted to call TrustedScript {ClassName} that has not been overridden!')";

         
    }
}
