using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ControllableObject
    /// 
    /// April 13, 2021
    /// 
    /// Defines an object that can be controlled.
    /// </summary>
    public class ControllableObject : PhysicalObject
    {
        internal override string ClassName => "ControllableObject";
        internal override InstanceTags Attributes => 0;
        public virtual void OnKeyDown(Control Control)
        {
            MessageBox.Show($"You pressed {Control.KeyCode.ToString()}!");
        }

        /// <summary>
        /// Runs on a key stopping being pressed.
        /// </summary>
        /// <param name="Control"></param>
        public virtual void OnKeyUp(Control Control)
        {

        }
    }
}
