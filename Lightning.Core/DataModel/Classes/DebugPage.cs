using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// DebugPage
    /// 
    /// August 24, 2021
    /// 
    /// Defines the root class for debug pages.
    /// </summary>
    public class DebugPage : GuiElement
    {
        internal override string ClassName => "DebugPage";

        internal bool IsOpen { get; set; }

        
    }
}
