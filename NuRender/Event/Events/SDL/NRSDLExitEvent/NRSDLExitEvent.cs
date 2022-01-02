using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// NRSDLExitEvent
    /// 
    /// NuRender SDL Exit Event, passed to all NuRender objects at the moment of window exit. Also passed up to the 
    /// NuRender client application.
    /// </summary>
    /// <param name="NREA">The <see cref="NREventArgs"/> of this event.</param>
    public delegate void NRSDLExitEvent
    (   
        object Sender,
        NREvent Event
    );

}
