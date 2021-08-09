using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class CollisionEventArgs : EventArgs
    {
        public Manifold Manifold { get; set; }
    }
}
