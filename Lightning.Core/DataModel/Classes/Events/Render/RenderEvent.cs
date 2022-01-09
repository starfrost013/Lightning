using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// RenderEvent
    /// 
    /// August 16, 2021
    /// 
    /// Defines an event fired on the rendering of a PhysicalInstance.
    /// </summary>
    public delegate void RenderEvent
    (
        object Sender,
        RenderEventArgs EventArgs
    );
}
