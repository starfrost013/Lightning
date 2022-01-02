using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// NREvent
    /// 
    /// September 9, 2021
    /// 
    /// Defines a NuRender event.
    /// </summary>
    public class NREvent
    {
        public NREventArgs EventArgs { get; set; }

        public NREvent()
        {
            EventArgs = new NREventArgs(); 
        }
    }
}
