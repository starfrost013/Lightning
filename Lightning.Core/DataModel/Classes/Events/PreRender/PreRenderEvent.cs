using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// PreRenderEvent
    /// 
    /// January 9, 2022
    /// 
    /// Defines an event fired when rendering is about to begin. 
    /// </summary>
    public delegate void PreRenderEvent
    (
        object Sender,
        PreRenderEventArgs EventArgs
    );
}
