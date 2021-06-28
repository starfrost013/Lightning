using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// GuiRoot
    /// 
    /// June 28, 2021
    /// 
    /// Defines the root class for all Lightning UI elements and objects.
    /// </summary>
    public class GuiRoot : PhysicalObject
    {
        internal override string ClassName => "GuiRoot";

        /// <summary>
        /// Run when this UI element is clicked.
        /// 
        /// OnCreate()
        /// </summary>
        public virtual void OnClick(Vector2 RelativePosition)
        {
            return; 
        }


    }
}
