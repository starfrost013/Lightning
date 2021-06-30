using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ClickEventArgs
    /// 
    /// June 29, 2021
    /// 
    /// Event args for the UI click event.
    /// </summary>
    public class ClickEventArgs
    {
        public Vector2 RelativePosition { get; set; }

        public bool Repeated { get; set; }
    }
}
