using Lightning.Core.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// AppMain
    /// 
    /// January 22, 2022
    /// 
    /// Defines main class that GMain must inherit from
    /// </summary>
    public abstract class App : Instance
    {
        internal override string ClassName => "App";

        internal override InstanceTags Attributes => InstanceTags.Instantiable | InstanceTags.Destroyable | InstanceTags.UsesCustomRenderPath | InstanceTags.ParentCanBeNull;

        public virtual void Start()
        {
            

        }

        public virtual void Render()
        {

        }
    }
}
