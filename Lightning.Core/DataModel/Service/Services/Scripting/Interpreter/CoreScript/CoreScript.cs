using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// possibly should be an interface
    /// 
    /// CoreScript
    /// 
    /// June 6, 2021
    /// 
    /// Defines a core script. A core script is a script that is executed by Lightning at the initialisation of Lua. 
    /// 
    /// Cannot be instantiated by XML
    /// </summary>
    public class CoreScript : Script
    {
        internal override string ClassName => "CoreScript";
        internal override InstanceTags Attributes => InstanceTags.Instantiable | InstanceTags.Destroyable;

        internal virtual string ProtectedContent => "Override me!";
    }
}
