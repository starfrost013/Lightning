using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// AnimationUpdatedEventArgs
    /// 
    /// August 21, 2021
    /// 
    /// Defines the event arguments for the <see cref="AnimationUpdated"/> event. 
    /// </summary>
    public class AnimationUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// The current animation frame. 
        /// </summary>
        public AnimationFrame CurrentFrame { get; set; }
    }
}
