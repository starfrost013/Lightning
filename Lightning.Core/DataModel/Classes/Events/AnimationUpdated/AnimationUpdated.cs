using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// AnimationUpdated
    ///
    /// August 21, 2021
    /// 
    /// Defines the animation update
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="EventArgs">The event arguments - see <see cref="AnimationUpdatedEventArgs"/></param>
    public delegate void AnimationUpdated
    (
        object Sender,
        AnimationUpdatedEventArgs EventArgs
    );
}
