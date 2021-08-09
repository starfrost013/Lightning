using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// OnCollisionStartEvent
    /// 
    /// August 9, 2021
    /// 
    /// Defines an event fired at the start of a collision.
    /// </summary>
    public delegate void CollisionStartEvent
    (
        object Sender,
        CollisionEventArgs e
    );
}
