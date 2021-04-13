using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
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
        public override string ClassName => "ControllableObject";
        public override InstanceTags Attributes => 0;
        public virtual void OnKeyDown(Control Control)
        {
            MessageBox.Show($"You pressed {Control.KeyCode.ToString()}!");
        }
    }
}
