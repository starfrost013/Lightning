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
    /// Defines NuRender Event Args
    /// </summary>
    public class NREventArgs : EventArgs
    {
        /// <summary>
        /// The sender of this object.
        /// </summary>
        public object Sender { get; set; }

        
    }
}
