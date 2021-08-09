using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// OnCollision EndEvent
    /// 
    /// August 9, 2021
    /// 
    /// Defines an event fired at the end of a collision.
    /// </summary>
    public delegate void CollisionEndEvent
    (
        object Sender,
        CollisionEventArgs e
    );
}
